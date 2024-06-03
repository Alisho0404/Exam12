using Domain.Constants;
using Domain.DTOs.RolePermissionDto;
using Domain.Enteties;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Services.HashService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using static Domain.Constants.Permissions;

namespace Infrastructure.Seed
{
    public class Seeder(DataContext context, ILogger<Seeder> logger, IHashService hashService)
    {
        public async Task Initial()
        {
            await SeedRole();
            await SeedClaimsForSuperAdmin();
            await AddAdminPermissions();
            await AddUserPermissions();           
            await DefaultUsers();
        }


      

        private async Task SeedRole()
        {
            try
            {
                var newRoles = new List<Domain.Enteties.Role>()
                {
                    new()
                {
                    Name = Domain.Constants.Roles.SuperAdmin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                    },
                new()
                {
                    Name = Domain.Constants.Roles.Admin,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new()
                {
                    Name = Domain.Constants.Roles.User,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },                
            };

                var existing = await context.Roles.ToListAsync();
                foreach (var role in newRoles)
                {
                    if (existing.Exists(e => e.Name == role.Name) == false)
                    {
                        await context.Roles.AddAsync(role);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
            }
        }

      


       

        private async Task DefaultUsers()
        {
            try
            {
                //super-admin
                var existingSuperAdmin = await context.Users.FirstOrDefaultAsync(x => x.Name == "SuperAdmin");
                if (existingSuperAdmin is null)
                {
                    var superAdmin = new Domain.Enteties.User()
                    {
                        Email = "superadmin@gmail.com",
                        Name = "SuperAdmin",
                        CreatedAt = DateTimeOffset.UtcNow,
                        UpdatedAt = DateTimeOffset.UtcNow,
                        Password = hashService.ConvertToHash("123123123")
                    };
                    await context.Users.AddAsync(superAdmin);
                    await context.SaveChangesAsync();

                    var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Name == "SuperAdmin");
                    var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.SuperAdmin);
                    if (existingUser is not null && existingRole is not null)
                    {
                        var userRole = new Domain.Enteties.UserRole()
                        {
                            RoleId = existingRole.Id,
                            UserId = existingUser.Id,
                            Role = existingRole,
                            User = existingUser,
                            UpdatedAt = DateTimeOffset.UtcNow,
                            CreatedAt = DateTimeOffset.UtcNow
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }

                }


                //admin
                var existingAdmin = await context.Users.FirstOrDefaultAsync(x => x.Name == "Admin");
                if (existingAdmin is null)
                {
                    var admin = new Domain.Enteties.User()
                    {
                        Email = "admin@gmail.com",
                        Name = "Admin",
                        CreatedAt = DateTimeOffset.UtcNow,
                        UpdatedAt = DateTimeOffset.UtcNow,
                        Password = hashService.ConvertToHash("123123")
                    };
                    await context.Users.AddAsync(admin);
                    await context.SaveChangesAsync();

                    var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Name == "Admin");
                    var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.Admin);
                    if (existingUser is not null && existingRole is not null)
                    {
                        var userRole = new Domain.Enteties.UserRole()
                        {
                            RoleId = existingRole.Id,
                            UserId = existingUser.Id,
                            Role = existingRole,
                            User = existingUser,
                            UpdatedAt = DateTimeOffset.UtcNow,
                            CreatedAt = DateTimeOffset.UtcNow
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }

                }

                //user
                var user = await context.Users.FirstOrDefaultAsync(x => x.Name == "Staff");
                if (user is null)
                {
                    var newStaff = new Domain.Enteties.User()
                    {
                        Email = "staff@gmail.com",
                        Name = "Staff",
                        CreatedAt = DateTimeOffset.UtcNow,
                        UpdatedAt = DateTimeOffset.UtcNow,
                        Password = hashService.ConvertToHash("123")
                    };
                    await context.Users.AddAsync(newStaff);
                    await context.SaveChangesAsync();

                    var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Name == "Staff");
                    var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.User);
                    if (existingUser is not null && existingRole is not null)
                    {
                        var userRole = new Domain.Enteties.UserRole()
                        {
                            RoleId = existingRole.Id,
                            UserId = existingUser.Id,
                            Role = existingRole,
                            User = existingUser,
                            UpdatedAt = DateTimeOffset.UtcNow,
                            CreatedAt = DateTimeOffset.UtcNow
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }

                }


            } 
            catch
            {
                throw new Exception();
            }
        }

       



        #region SeedClaimsForSuperAdmin

        private async Task SeedClaimsForSuperAdmin()
        {
            try
            {
                var superAdminRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.SuperAdmin);
                if (superAdminRole == null) return;
                var roleClaims = new List<RoleClaimsDto>();
                roleClaims.GetPermissions(typeof(Domain.Constants.Permissions));
                var existingClaims = await context.RoleClaims.Where(x => x.RoleId == superAdminRole.Id).ToListAsync();
                foreach (var claim in roleClaims)
                {
                    if (existingClaims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value) == false)
                        await context.AddPermissionClaim(superAdminRole, claim.Value);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion

        #region AddAdminPermissions

        private async Task AddAdminPermissions()
        {
            //add claims
            var adminRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.Admin);
            if (adminRole == null) return;
            var userClaims = new List<RoleClaimsDto>()
        {
            new("Permissions", Domain.Constants.Permissions.Meeting.View),
            new("Permissions", Domain.Constants.Permissions.Meeting.Create),
            new("Permissions", Domain.Constants.Permissions.Meeting.Edit),

            new("Permissions", Domain.Constants.Permissions.Notification.View),
            new("Permissions", Domain.Constants.Permissions.Notification.Create),
            new("Permissions", Domain.Constants.Permissions.Notification.Edit),
            

            new("Permissions", Domain.Constants.Permissions.Role.View),
            new("Permissions", Domain.Constants.Permissions.Role.Create),
            new("Permissions", Domain.Constants.Permissions.Role.Edit),

            new("Permissions", Domain.Constants.Permissions.User.View),
            new("Permissions", Domain.Constants.Permissions.User.Create),
            new("Permissions", Domain.Constants.Permissions.User.Edit),

            new("Permissions", Domain.Constants.Permissions.UserRole.View),
            new("Permissions", Domain.Constants.Permissions.UserRole.Create),
            new("Permissions", Domain.Constants.Permissions.UserRole.Edit),

            
        };

            var existingClaim = await context.RoleClaims.Where(x => x.RoleId == adminRole.Id).ToListAsync();
            foreach (var claim in userClaims)
            {
                if (!existingClaim.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
                {
                    await context.AddPermissionClaim(adminRole, claim.Value);
                }
            }
        }

        #endregion

        #region AddStaffPermissions

        private async Task AddUserPermissions()
        {
            //add claims
            var userRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.User);
            if (userRole == null) return;
            var userClaims = new List<RoleClaimsDto>()
        {
            new("Permissions", Domain.Constants.Permissions.Notification.View),
            new("Permissions", Domain.Constants.Permissions.Meeting.View),
            new("Permissions", Domain.Constants.Permissions.Meeting.Create),
            new("Permissions", Domain.Constants.Permissions.Meeting.Edit),            
            new("Permissions", Domain.Constants.Permissions.Role.View),
            new("Permissions", Domain.Constants.Permissions.User.View),
           

        };

            var existingClaim = await context.RoleClaims.Where(x => x.RoleId == userRole.Id).ToListAsync();
            foreach (var claim in userClaims)
            {
                if (!existingClaim.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
                {
                    await context.AddPermissionClaim(userRole, claim.Value);
                }
            }
        }

        #endregion

        
    }
}
