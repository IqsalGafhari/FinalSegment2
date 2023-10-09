using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    {
        public CreateUniversityValidator()
        {
            RuleFor(e => e.Code)
                .NotEmpty()//tdk boleh kosong
                .MinimumLength(3)//panjang minimum 25
                .MaximumLength(50);//panjang maksimum 100
            RuleFor(e => e.Name)
                .NotEmpty()//tdk boleh kosong
                .MinimumLength(3)//panjang minimum 25
                .MaximumLength(100);//panjang maksimum 100
        }

    }
}
