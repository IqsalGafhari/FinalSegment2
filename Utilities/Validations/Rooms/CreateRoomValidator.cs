using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(25)//panjang minimum
                .MaximumLength(100);//panjang maksimum
            RuleFor(e => e.Floor)
                .NotEmpty();
            RuleFor(e => e.Capacity)
                .NotEmpty();
        }

    }
}
