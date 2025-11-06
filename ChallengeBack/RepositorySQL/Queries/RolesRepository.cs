using Domain.DTO;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.DBContext;
using RepositorySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySQL.Queries
{
    public class RolesRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RolDto>> GetRolList(int levelAcces)
        {
            List<Role> roles = new List<Role>();
            //Just Admin can see his own role
            if (levelAcces == 3)
            {
                roles = await _context.Roles.Where(r => r.Level <= levelAcces).ToListAsync();
            }
            else
            {
                roles = await _context.Roles.Where(r => r.Level < levelAcces).ToListAsync();
            }
                return roles.Select(role => new RolDto()
                {
                    RoleId = role.RoleId,
                    Level = role.Level,
                    Name = role.Name,
                }).ToList();
        }
    }
}
