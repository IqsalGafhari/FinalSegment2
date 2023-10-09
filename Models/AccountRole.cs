using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Required, Column("account_guid")]
        public Guid AccountGuid { get; set; }
        [Required, Column("role_guid")]
        public Guid RoleGuid { get; set; }

        public Account? Account { get; set; }
        public Role? Role { get; set; }

    }
}
