using API.Models;
using API.Utilities.Enum;

namespace API.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set;}
        public static implicit operator Booking(CreateBookingDto createBookingDto)
        {
            return new Booking
            {
                Guid = new Guid(),
                StartDate = createBookingDto.StartDate,
                EndDate = createBookingDto.EndDate,
                Status = createBookingDto.Status,
                Remarks = createBookingDto.Remarks,
                RoomGuid = createBookingDto.RoomGuid,
                EmployeeGuid = createBookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
