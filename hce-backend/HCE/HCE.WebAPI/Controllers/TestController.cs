using HCE.Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HCE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApiControllerBase
    {
        public TestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("Test")]
        public ActionResult<ResponseResult<string>> Test()
        {
            return Ok(new ResponseResult<string> { IsSuccess = true, Status = HttpStatusCode.OK, Message = "it work." });
        }
    }
}
