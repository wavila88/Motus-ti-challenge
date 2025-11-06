using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRolRepository
    {
        Task<List<DTO.RolDto>> GetRolList(int levelAcces);
    }
}
