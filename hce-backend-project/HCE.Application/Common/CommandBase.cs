using MediatR;

namespace HCE.Application.Common
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}
