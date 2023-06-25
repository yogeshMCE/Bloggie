using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId = "a4fe3ae4-9712-4d8e-9cc0-6e0a13602ef0";
            var AdminRoleId = "d14d9b9e-d31b-415b-ac27-a866131032a7";
            var SuperAdminRoleId = "289db8f8-e754-4fd7-9894-866729785487";
            // SEED ROLES
            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName = "User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                },
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName="Admin",
                    Id=AdminRoleId,
                    ConcurrencyStamp=AdminRoleId

                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=SuperAdminRoleId,
                    ConcurrencyStamp=SuperAdminRoleId
                }
            };
            // insert these roles in builder object
            builder.Entity<IdentityRole>().HasData(roles);

            // seed SuperAdminUser
            var SuperAdminId = "deffa05d-4211-41db-98e6-e4d94c00794c";
            var SuperAdminUser = new IdentityUser
            {
                UserName = "sahiladmin@123.com",
                Email = "yogeshkumar2k19mc140dtu.gmail.com",
                NormalizedEmail = "yogeshkumar2k19mc140dtu.gmail.com".ToUpper(),
                NormalizedUserName = "sahiladmin@123.com".ToUpper(),
                Id = SuperAdminId
            };
            // generate password for superadmin
            SuperAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(SuperAdminUser, "S@hilkumar9873");
            builder.Entity<IdentityUser>().HasData(SuperAdminUser);

            // add all roles to the super admin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {

                new IdentityUserRole<string> {
                  RoleId= AdminRoleId,
                  UserId= SuperAdminId
                },
                new IdentityUserRole<string>
                {
                     RoleId= userRoleId,
                     UserId= SuperAdminId,

                },
                new IdentityUserRole<string>
                {
                     RoleId= SuperAdminRoleId,
                     UserId= SuperAdminId,

                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
