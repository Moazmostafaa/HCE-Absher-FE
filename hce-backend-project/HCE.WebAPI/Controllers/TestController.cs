using HCE.Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HCE.Domain.Entities.Identity;
using HCE.Interfaces.Repositories;

namespace HCE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApiControllerBase
    {
        private readonly IReadRepository<User> _userRepository;
        public TestController(IMediator mediator, IReadRepository<User> userRepository) : base(mediator)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("Test")]
        public ActionResult<ResponseResult<string>> Test()
        {
            var name = _userRepository.GetById(new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f")).Name;
            return Ok(new ResponseResult<string> { IsSuccess = true, Status = HttpStatusCode.OK, Message = $"{name} is the proof ef core6 is working" });
        }
    }
}
