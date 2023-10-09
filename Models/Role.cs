using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Required, Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public ICollection<AccountRole>? AccountRoles { get; set;}
    }
}
