using API.DTOs.Bookings;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Bookings
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        
        public CreateBookingValidator()
        {
             /*public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }*/
        RuleFor(e => e.StartDate)
                .NotEmpty();//tdk boleh kosong
            RuleFor(e => e.EndDate)
                .NotEmpty();
            RuleFor(e => e.Status)
                .NotEmpty()//tdk boleh kosong
                .IsInEnum();
            RuleFor(e => e.Remarks)
                .NotEmpty();//tdk boleh kosong
            RuleFor(e => e.RoomGuid)
                .NotEmpty();//tdk boleh kosong
            RuleFor(e => e.EmployeeGuid)
                .NotEmpty();
        }
    }
}
