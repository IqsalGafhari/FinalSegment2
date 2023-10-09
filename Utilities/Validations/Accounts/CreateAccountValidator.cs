using API.DTOs.Accounts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            /*public Guid Guid { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredTime { get; set; }*/
        RuleFor(e => e.Guid)
                .NotEmpty();//tdk boleh kosong
            RuleFor(e => e.Password)//password
                .NotEmpty().WithMessage("Password Tidak Boleh Kosong")//tidak boleh kosong
                .MinimumLength(10)//panjang minimum 10
                .MaximumLength(20);//panjang maksimum 20
            RuleFor(e => e.Otp)//otp
                .NotEmpty();////tidak boleh kosong
            RuleFor(e => e.ExpiredTime)
                .NotEmpty();//tidak boleh kosong
            RuleFor(e => e.IsUsed)
                .NotEmpty();//tidak boleh kosong
        }
    }
}
