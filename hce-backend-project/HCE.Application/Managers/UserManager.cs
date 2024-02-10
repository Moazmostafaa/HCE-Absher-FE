using HCE.Domain.Entities.Identity;
using HCE.Domain.Exceptions;
using HCE.Interfaces.Managers;
using HCE.Interfaces.Models.Dto.User;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Blob;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.HelperOperation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HCE.Utility.CommonModels;
using Microsoft.Extensions.Options;

namespace HCE.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;
        private readonly IReadBlobRepository _blobRepo;
        private readonly AppSetting _appSetting;


        public UserManager(IReadRepository<User> userReadRepository, IWriteRepository<User> userWriteRepository,
            IReadBlobRepository blobRepo, IOptions<AppSetting> options)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _blobRepo = blobRepo;
            _appSetting = options.Value;
        }
        public async Task<UserDto> GetDtoById(Guid id)
        {
            UserDto result = new UserDto();

            var user = await _userReadRepository.GetAsync(x => x.Id == id);
            if (user == null)
                throw new EntityNotFoundException(Message_Resource.UserEntity);

            result = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                UserName = user.UserName,
                ProfileImageId = user.ProfileAttachmentId
            };
            return result;
        }

        public async Task<UserDto> GetDtoByNationalId(string nationalId)
        {
            UserDto result;

            var user = await _userReadRepository.GetAsync(x => x.NationalId == nationalId);
            if (user == null)
                throw new EntityNotFoundException(Message_Resource.UserEntity);

            result = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                UserName = user.UserName,
                ProfileImageId = user.ProfileAttachmentId
            };
            return result;
        }

        public async Task<UserDto> GetDtoByUserNameAndPassword(string userName, string password)
        {
            UserDto result = new UserDto();

            var user = await _userReadRepository.GetAsync(x => x.UserName == userName.Trim());
            if (user == null)
                throw new NotFoundException(Message_Resource.InvalidUserName);
            var passwordHashed = HashHelper.sha256(password.Trim());
            if (passwordHashed != user.Password)
                throw new Exception(Message_Resource.InvalidPassword);

            result = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                UserName = user.UserName,
                ProfileImageId = user.ProfileAttachmentId
            };
            return result;
        }

        public async Task<UserDto> GetDtoByUserNameOrNID(string userNameOrNID)
        {
            if (string.IsNullOrEmpty(userNameOrNID) || string.IsNullOrEmpty(userNameOrNID.Trim()))
                return null;

            var user = await _userReadRepository.GetAsNoTrackingAsync(x => x.UserName.ToLower() == userNameOrNID.Trim().ToLower() || x.NationalId.ToLower() == userNameOrNID.Trim().ToLower());
            if (user == null)
                return null;

            var result = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                UserName = user.UserName,
                ProfileImage = user.ProfileAttachmentId == null ? null : _blobRepo.GetAttachment(user.ProfileAttachmentId.Value)
            };
            return result;
        }



        #region XML Class

        [XmlRoot(ElementName = "value")]
        public class XmlValue
        {

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "record")]
        public class XmlRecords
        {

            [XmlElement(ElementName = "value")]
            public List<XmlValue> Value { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "javaclass")]
            public string Javaclass { get; set; }

            [XmlText]
            public string Text { get; set; }

            [XmlElement(ElementName = "record")]
            public XmlRecords XmlRecord { get; set; }
        }

        [XmlRoot(ElementName = "Values")]
        public class XmlValues
        {

            [XmlElement(ElementName = "record")]
            public XmlRecords XmlRecord { get; set; }

            [XmlAttribute(AttributeName = "version")]
            public double Version { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        #endregion
    }
}
