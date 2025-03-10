﻿using Domain.DTOs.RolePermissionDto;
using Domain.Enteties;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsDto> allPermissions, Type policy)
        {
            var nestedTypes = policy.GetNestedTypes(BindingFlags.Public);
            if (nestedTypes.Length > 0)
            {
                foreach (var nested in nestedTypes)
                {
                    FieldInfo[] fields = nested.GetFields(BindingFlags.Static | BindingFlags.Public);

                    foreach (FieldInfo fi in fields)
                    {
                        allPermissions.Add(new RoleClaimsDto("Permissions", fi.GetValue(null).ToString()));
                    }
                }
            }
            else
            {
                FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (FieldInfo fi in fields)
                {
                    allPermissions.Add(new RoleClaimsDto(fi.GetValue(null).ToString(), "Permissions"));
                }
            }
        }



        public static async Task AddPermissionClaim(this DataContext context, Role role, string permission)
        {

            var allClaim = await context.RoleClaims.Where(x => x.RoleId==role.Id).ToListAsync();
            if (!allClaim.Any(x => x.ClaimType == "Permissions" && x.ClaimType == permission))
            {
                await context.RoleClaims.AddAsync(new RoleClaim()
                {
                    ClaimType = "Permissions",
                    Role =role,
                    RoleId = role.Id,
                    ClaimValue = permission,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
