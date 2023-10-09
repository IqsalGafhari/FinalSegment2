using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {
        [Required, Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        [Required, Column("degree", TypeName = "nvarchar(100)")]
        public string Degree { get; set; }
        [Required, Column("gpa")]
        public float GPA { get; set; }
        [Required, Column("university_guid")]
        public Guid UniversityGuid { get; set; }
        public University? University { get; set; }
        public Employee? Employee {  get; set; }

    }
}
