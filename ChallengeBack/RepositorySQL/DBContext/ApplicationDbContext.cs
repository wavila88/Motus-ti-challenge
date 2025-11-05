using Microsoft.AspNetCore.Identity; // Agrega este using
using Microsoft.EntityFrameworkCore;
using RepositorySQL.Models;

namespace RepositorySQL.DBContext
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Permission> Permissions { get; set; } // Agregar DbSet para Permission
        public DbSet<RolePermission> RolePermissions { get; set; } // Agregar DbSet para RolePermission

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Permission>().ToTable("Permissions"); // Mapear la tabla Permission
            modelBuilder.Entity<RolePermission>().ToTable("RolePermissions"); // Mapear la tabla RolePermission

            // Global Query Filters for soft delete
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted.HasValue || u.IsDeleted == false);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted.HasValue || r.IsDeleted == false);
            modelBuilder.Entity<Permission>().HasQueryFilter(p => !p.IsDeleted.HasValue || p.IsDeleted == false);
            modelBuilder.Entity<RolePermission>().HasQueryFilter(rp => !rp.IsDeleted.HasValue || rp.IsDeleted == false);

            modelBuilder.Entity<User>()
              .HasOne(u => u.Role)
              .WithMany()
              .HasForeignKey(u => u.RoleId);

            // Seed of Roles
            //Using Fluent API
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "General Manager", Level = 3, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Role { RoleId = 2, Name = "Sales Lead", Level = 2, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Role { RoleId = 3, Name = "Salesperson", Level = 1, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false }
            );

            // Seed of Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, Name = "/createUser", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Permission { PermissionId = 2, Name = "/ManagerBoard", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Permission { PermissionId = 3, Name = "/SalesLeadBoard", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Permission { PermissionId = 4, Name = "/SalesPersonBoard", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new Permission { PermissionId = 5, Name = "/userEdit", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false }
            );

            // Seed of RolePermissions
            modelBuilder.Entity<RolePermission>().HasData(
                // /createUser: just General Manager (RoleId = 1)
                new RolePermission { RolePermissionId = 1 ,RoleId = 1, PermissionId = 1, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },

                // /ManagerBoard: just General Manager
                new RolePermission { RolePermissionId = 2, RoleId = 1, PermissionId = 2, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },

                // /SalesLeadBoard: SalesLead (2) y General Manager (1)
                new RolePermission { RolePermissionId = 3, RoleId = 1, PermissionId = 3, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new RolePermission { RolePermissionId = 4, RoleId = 2, PermissionId = 3, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },

                // /SalesPersonBoard: Tree roles can access
                new RolePermission { RolePermissionId = 5, RoleId = 1, PermissionId = 4, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new RolePermission { RolePermissionId = 6, RoleId = 2, PermissionId = 4, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new RolePermission { RolePermissionId = 7, RoleId = 3, PermissionId = 4, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },

                // /userEdit: SalesLead (2) y General Manager (1)
                new RolePermission { RolePermissionId = 8, RoleId = 1, PermissionId = 5, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false },
                new RolePermission { RolePermissionId = 9, RoleId = 2, PermissionId = 5, CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = null, IsDeleted = false }

            );

            var adminUser = new User
            {
                UserId = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@gmail.com",
                DocumentNumber = 12345678,
                DateOfBirth = new DateTime(1980, 1, 1),
                RoleId = 1,
                Password = "AQAAAAIAAYagAAAAENIUj5SqM/oi9lmqZeXqSAeTllDpRPgOobsjTzZ6zB0E2OFkcXfRfKJO1dJjsPDi3g==",
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = null,
                IsDeleted = false
            };      
        
            var leadUser = new User
            {
                UserId = 2,
                FirstName = "Lead",
                LastName = "Lead",
                Email = "lead@gmail.com",
                DocumentNumber = 23456789,
                DateOfBirth = new DateTime(1990, 1, 1),
                RoleId = 2,
                Password = "AQAAAAIAAYagAAAAENIUj5SqM/oi9lmqZeXqSAeTllDpRPgOobsjTzZ6zB0E2OFkcXfRfKJO1dJjsPDi3g==", // same as admin
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = null,
                IsDeleted = false
            };

            var salesUser = new User
            {
                UserId = 3,
                FirstName = "Sales",
                LastName = "Person",
                Email = "sales@gmail.com",
                DocumentNumber = 34567890,
                DateOfBirth = new DateTime(1995, 1, 1),
                RoleId = 3,
                Password = "AQAAAAIAAYagAAAAENIUj5SqM/oi9lmqZeXqSAeTllDpRPgOobsjTzZ6zB0E2OFkcXfRfKJO1dJjsPDi3g==", // same as admin
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = null,
                IsDeleted = false
            };

            modelBuilder.Entity<User>().HasData(adminUser, leadUser, salesUser);
        }
    }
}
