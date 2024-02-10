using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Exceptions;
using HCE.Interfaces.Managers;
using HCE.Interfaces.Repositories;
using HCE.Resource;
using HCE.Utility.CommonModels;
using HCE.Utility.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace HCE.Application.Managers
{
    public class OtpManager : IOtpManager
    {
        private readonly AppOtpSettings _otpSettings;
        private readonly IUserManager _userManager;
        private readonly IWriteRepository<Otp> _writeRepo;
        private readonly IReadRepository<Otp> _readRepo;
        private readonly IUnitOfWork _unitOfWork;
        public OtpManager(IOptions<AppOtpSettings> options, IUserManager userManager, IWriteRepository<Otp> writeRepo, IUnitOfWork unitOfWork, IReadRepository<Otp> readRepo)
        {
            _userManager = userManager;
            _writeRepo = writeRepo;
            _unitOfWork = unitOfWork;
            _readRepo = readRepo;
            _otpSettings = options.Value;
        }

        public async Task<bool> SendOtp(string nationalId)
        {
            var code = GenerateRandomCode();

            #region SMS request
            var restClient = new RestClient();
            var webServiceUrl = _otpSettings.BaseUrl; // add your web service URL 
            // Bypass Sll security
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            restClient.BaseUrl = new Uri(webServiceUrl);
            restClient.Timeout = Timeout.Infinite;
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };
            string parameter =
                $"<?xml version =\"1.0\" encoding=\"utf-8\"?><Message ID=\"8140\"><MessageDetails><code>{code}</code><SAMIS_ID>{nationalId}</SAMIS_ID></MessageDetails><ProviderDetail><ID>{_otpSettings.ProviderId}</ID><SecretCode>{_otpSettings.SecretCode}</SecretCode></ProviderDetail></Message>";
            request.AddParameter("xml", parameter);
            Log.Debug("--otp--");
            Log.Debug(parameter);

            #endregion

            try
            {
                var response = await restClient.ExecuteAsync(request);
                Log.Debug(response.Content);
                Log.Debug("status-code: " + response.StatusCode);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = JsonConvert.DeserializeObject<string>(response.Content);
                    var otp = new Otp()
                    {
                        Code = code,
                        NationalId = nationalId,
                        Tries = 0,
                        TcnCode = responseContent
                    };
                    await _writeRepo.AddAsync(otp);
                    await _unitOfWork.CommitAsync();
                    return true;
                }

                return false;
            }
            catch (WebException webEx)
            {
                WebResponse errResp = webEx.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = await reader.ReadToEndAsync();
                    throw new InvalidOperationException(text);
                }
            }
        }

        private string GenerateRandomCode()
        {
            var generator = new Random();
            var code = generator.Next(0, 10000).ToString("D4");
            return code;
        }

        public async Task<bool> VerifyOtp(string nationalId, string code)
        {
            var otp = _readRepo.GetMany(c => c.NationalId == nationalId && (!c.IsExpired || !c.IsUsed)).OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            if (otp == null)
                throw new EntityNotFoundException(Message_Resource.OtpEntity);

            if (otp.Tries == _otpSettings.OtpMaxTriesNumber)
                throw new BusinessException(Message_Resource.MaxNumberOfTriesReached);

            if (otp.Code != code)
            {
                otp.Tries++;
                _writeRepo.Update(otp);
                await _unitOfWork.CommitAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> ResendOtp(string nationalId)
        {
            var oldOtp = _readRepo.GetMany(c => c.NationalId == nationalId && (!c.IsExpired || !c.IsUsed)).OrderByDescending(c => c.CreatedDate).FirstOrDefault();
            if (oldOtp == null)
                throw new BusinessException(Message_Resource.NoOtpWasSentBefore);


            var code = GenerateRandomCode();

            #region SMS request
            var restClient = new RestClient();
            var webServiceUrl = _otpSettings.BaseUrl; // add your web service URL 
            // Bypass Sll security
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            restClient.BaseUrl = new Uri(webServiceUrl);
            restClient.Timeout = Timeout.Infinite;
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };
            string parameter =
                $"<?xml version =\"1.0\" encoding=\"utf-8\"?><Message ID=\"8140\"><MessageDetails><code>{code}</code><SAMIS_ID>{nationalId}</SAMIS_ID></MessageDetails><ProviderDetail><ID>{_otpSettings.ProviderId}</ID><SecretCode>{_otpSettings.SecretCode}</SecretCode></ProviderDetail></Message>";
            request.AddParameter("xml", parameter);

            #endregion
            try
            {
                var response = await restClient.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = JsonConvert.DeserializeObject<string>(response.Content);
                    var otp = new Otp()
                    {
                        Code = code,
                        NationalId = nationalId,
                        Tries = 0,
                        TcnCode = responseContent
                    };
                    await _writeRepo.AddAsync(otp);
                    oldOtp.IsExpired = true;
                    _writeRepo.Update(oldOtp);
                    await _unitOfWork.CommitAsync();
                    return true;
                }

                return false;
            }
            catch (WebException webEx)
            {
                WebResponse errResp = webEx.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = await reader.ReadToEndAsync();
                    throw new InvalidOperationException(text);
                }
            }
        }
    }
}
