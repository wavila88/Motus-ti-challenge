using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorySQL.Models
{
    public class Role : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("level")]
        public int Level { get; set; }
    }
}