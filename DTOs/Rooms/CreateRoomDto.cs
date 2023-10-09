using API.Models;

namespace API.DTOs.Rooms
{
    public class CreateRoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public static implicit operator Room(CreateRoomDto roomDto)
        {
            return new Room
            {
                Guid = new Guid(),
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
