using API.Models;

namespace API.DTOs.Roles
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
            public static implicit operator Role(CreateRoleDto roleDto)
        {
            return new Role
            {
                Guid = new Guid(),
                Name = roleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
