using Domain.DTO;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.DBContext;
using RepositorySQL.Models;

namespace RepositorySQL.Queries
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<UserDto>> GetUserList()
        {
            /*
             * Questions of time for this i will use 
             * Pagination.
             */
            var users =_context.Users.Include(u => u.Role).ToListAsync();
            return users.ContinueWith(t => t.Result.Select(user => new UserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = new RolDto() { 
                    RoleId = user.Role.RoleId,
                    Level = user.Role.Level,
                    Name = user.Role.Name
                } 
            }).ToList());
        }

        public async Task SaveUser(UserSaveDto user)
        {
            _context.Users.Add(new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.UtcNow,
                RoleId = user.RoleId,
                DocumentNumber = user.DocumentNumber,   
                DateOfBirth = user.DateOfBirth,
            });
            await _context.SaveChangesAsync();
        }

        async Task<UserDto> IUserRepository.GetUserById(int userId)
        {
            var user= await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            return new UserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            return new UserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Passsword = user.Password,
                Email = user.Email,
                Role = new RolDto() 
                {
                    RoleId = user.Role.RoleId,
                    Level = user.Role.Level,
                    Name = user.Role.Name
                }
            };
        }

        public async Task UpdateUser(UserSaveDto user)
        {
            var existingUser =await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.UpdatedAt = DateTime.UtcNow;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;
                existingUser.DocumentNumber = user.DocumentNumber;
                existingUser.DateOfBirth = user.DateOfBirth;
                await _context.SaveChangesAsync();
            }
        }

        /*
         * Soft delete
         */
        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsDeleted = true;
                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
