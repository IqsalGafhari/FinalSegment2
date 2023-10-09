using API.Models;

namespace API.DTOs.Universities
{
    public class UniversityDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static implicit operator University(UniversityDto universityDTO){
            return new University { 
                Guid = universityDTO.Guid,
                Name = universityDTO.Name,
                Code = universityDTO.Code,
                ModifiedDate = DateTime.Now

                };
         }

        public static explicit operator UniversityDto(University university)
        {
            return new UniversityDto { 
            Guid = university.Guid,
            Name = university.Name,
            Code = university.Code};
        }
    }
}
