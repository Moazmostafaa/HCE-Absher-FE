using HCE.Interfaces.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.ResponseModel
{
    public class ResponseResult<TEntity> : IResponseResult<TEntity>
    {
        public ResponseResult()
        { }

        public ResponseResult(TEntity entity) : this(entity, true, HttpStatusCode.OK, null, null)
        { }

        public ResponseResult(TEntity entity, bool isSuccess) : this(entity, isSuccess, HttpStatusCode.OK, null, null)
        { }

        public ResponseResult(TEntity entity, bool isSuccess, string message) : this(entity, isSuccess, HttpStatusCode.OK, message, null)
        { }

        public ResponseResult(TEntity entity, bool isSuccess, string message, List<string> errors) : this(entity, isSuccess, HttpStatusCode.BadRequest, message, errors)
        { }

        public ResponseResult(TEntity entity, bool isSuccess, HttpStatusCode status = HttpStatusCode.OK, string message = null, List<string> errors = null)
        {
            Errors = errors;
            Entity = entity;
            Status = status;
            Message = message;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
        public TEntity Entity { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
