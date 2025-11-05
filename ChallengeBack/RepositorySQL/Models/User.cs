using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorySQL.Models
{
    public class User : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("document_number")]
        public int DocumentNumber { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Role")]
        [Column("role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }


    }
}
