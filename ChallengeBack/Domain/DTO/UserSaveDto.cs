using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class UserSaveDto
    {
        public int UserId { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public int DocumentNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
