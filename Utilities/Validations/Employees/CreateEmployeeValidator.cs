using API.DTOs.Employees;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()//konstruktor
        {

            /*public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }*/

        RuleFor(e => e.FirstName)
               .NotEmpty();//FirstName tidak boleh kosong

            RuleFor(e => e.BirthDate)
               .NotEmpty();
            //.LessThanOrEqualTo("01/01/2003"); // 18 years old

            RuleFor(e => e.Gender)
               .NotEmpty()//Gender tidak boleh kosong
               .IsInEnum();//Gender ada di folder Enum

            RuleFor(e => e.HiringDate).NotEmpty();

            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("Tidak Boleh Kosong")//memberi pesan agar email tdk boleh kosong
               .EmailAddress().WithMessage("Format Email Salah");//memberi pesan bahwa format email salah

            RuleFor(e => e.PhoneNumber)
               .NotEmpty()
               .MinimumLength(10)//minimum utk nomor telepphone adalah 10
               .MaximumLength(20);//maksimal utk nomor telephone 20
        }
    }
}
