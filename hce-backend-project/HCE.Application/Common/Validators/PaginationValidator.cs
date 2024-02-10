using HCE.Interfaces.Domain.Request;
using FluentValidation;

namespace HCE.Application.Common.Validators
{
    public class PaginationValidator : AbstractValidator<IPagedRequest>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
