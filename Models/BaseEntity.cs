using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class BaseEntity
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required, Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
