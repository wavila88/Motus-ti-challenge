using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorySQL.Models
{
    public class Permission : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}