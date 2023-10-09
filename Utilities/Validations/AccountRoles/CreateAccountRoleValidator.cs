using API.DTOs.AccountRoles;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
        public CreateAccountRoleValidator()
        {

            /*public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }*/

            RuleFor(a => a.Guid)
                .NotEmpty();

            RuleFor(a => a.AccountGuid)
                .NotEmpty();

            RuleFor(a => a.RoleGuid)
                .NotEmpty();
        }
    }
}
