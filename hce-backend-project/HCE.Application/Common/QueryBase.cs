using MediatR;

namespace HCE.Application.Common
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}
