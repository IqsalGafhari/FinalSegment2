using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;


namespace API.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
