using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class RolDto
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }
    }
}
