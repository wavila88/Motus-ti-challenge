using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.DBContext;

namespace RepositorySQL.Queries
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasPermission(string userEmail, string permissionName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
                return false;

            var permission = await _context.Permissions
                .FirstOrDefaultAsync(p => p.Name == permissionName);

            if (permission == null)
                return false;

            var hasPermission = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == user.RoleId && rp.PermissionId == permission.PermissionId);

            return hasPermission;
        }
    }
}
