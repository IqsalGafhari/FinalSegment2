using API.DTOs.Educations;
using API.Models;

namespace API.DTOs.Educations
{
    public class EducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }
               public static implicit operator Education(EducationDto EducationDto)
        {
            return new Education
            {
                Guid = EducationDto.Guid,
                Major = EducationDto.Major,
                Degree = EducationDto.Degree,
                GPA = EducationDto.Gpa,
                UniversityGuid = EducationDto.UniversityGuid,
                ModifiedDate = DateTime.Now

            };
        }
        public static explicit operator EducationDto(Education education)
        {
            return new EducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.GPA,

            };
        }

    }
}
