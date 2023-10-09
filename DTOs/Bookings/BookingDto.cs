
using API.Models;
using API.Utilities.Enum;

namespace API.DTOs.Bookings
{
    public class BookingDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public static implicit operator Booking(BookingDto createBookingDto)
        {
            return new Booking
            {
                StartDate = createBookingDto.StartDate,
                EndDate = createBookingDto.EndDate,
                Status = createBookingDto.Status,
                Remarks = createBookingDto.Remarks,
                RoomGuid = createBookingDto.RoomGuid,
                EmployeeGuid = createBookingDto.EmployeeGuid,
                ModifiedDate = DateTime.Now

            };
        }
        public static explicit operator BookingDto(Booking booking)
        {
            return new BookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,
            };
        }

    }
}
