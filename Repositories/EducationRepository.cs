using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;


namespace API.Repositories
{
    public class EducationRepository : GeneralRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
