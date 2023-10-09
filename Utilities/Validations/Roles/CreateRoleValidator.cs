using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {

        public CreateRoleValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()//tidak boleh kosong
                .MinimumLength(15)//minimum 
                .MaximumLength(100);//panjang maksimum
        }
    }
}
