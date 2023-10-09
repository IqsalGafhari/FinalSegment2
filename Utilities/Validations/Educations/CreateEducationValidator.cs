using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
    {

        public CreateEducationValidator()
        {
            /*public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }*/
            RuleFor(e => e.UniversityGuid)
                .NotEmpty().WithMessage("Tidak Boleh Kosong");//tidak boleh kosong
            RuleFor(e => e.Major)
                .NotEmpty();//tidak boleh kosong
            RuleFor(e => e.Degree)
                .NotEmpty();//tidak boleh kosong
            RuleFor(e => e.Gpa)
                .NotEmpty();//tidak boleh kosong
            RuleFor(e => e.UniversityGuid)
                .NotEmpty();//tidak boleh kosong
        }
    }
}
