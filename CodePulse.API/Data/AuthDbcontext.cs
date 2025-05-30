using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbcontext : IdentityDbContext
    {
        public AuthDbcontext(DbContextOptions<AuthDbcontext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoles(builder);
            SeedAdminUser(builder);
            SeedAdminUserRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            // create Reader and Writer Role

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = IdentityDefaults.ReaderRoleId,
                    Name = IdentityDefaults.ReaderRole,
                    NormalizedName = IdentityDefaults.ReaderRole.ToUpper(),
                    ConcurrencyStamp = IdentityDefaults.ReaderRoleId
                },
                new IdentityRole
                {
                    Id = IdentityDefaults.WriterRoleId,
                    Name = IdentityDefaults.WriterRole,
                    NormalizedName = IdentityDefaults.WriterRole.ToUpper(),
                    ConcurrencyStamp = IdentityDefaults.WriterRoleId
                }
            };
            // Seed the Roles
            builder.Entity<IdentityRole>()
                .HasData(roles);
        }
        private void SeedAdminUser(ModelBuilder builder)
        {
            // Create an admin User
            var admin = new IdentityUser()
            {
                Id = IdentityDefaults.AdminUserId,
                UserName = IdentityDefaults.AdminEmail,
                NormalizedUserName = IdentityDefaults.AdminEmail.ToUpper(),
                Email = IdentityDefaults.AdminEmail,
                NormalizedEmail = IdentityDefaults.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                AccessFailedCount = 0,
                ConcurrencyStamp = IdentityDefaults.AdminUserId,
                SecurityStamp = Guid.NewGuid().ToString()

            };

            var hasher = new PasswordHasher<IdentityUser>();

            admin.PasswordHash = hasher.HashPassword(admin, IdentityDefaults.AdminPassword);

            builder.Entity<IdentityUser>()
                .HasData(admin);

        }
        private void SeedAdminUserRoles(ModelBuilder builder)
        {
            // Give Roles to the admin user
            var adminRole = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = IdentityDefaults.AdminUserId,
                    RoleId = IdentityDefaults.ReaderRoleId
                },
                new IdentityUserRole<string>()
                {
                    UserId = IdentityDefaults.AdminUserId,
                    RoleId = IdentityDefaults.WriterRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>()
                .HasData(adminRole);
        }
    }
}
