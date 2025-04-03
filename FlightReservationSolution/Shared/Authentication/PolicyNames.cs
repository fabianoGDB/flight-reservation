using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public static class PolicyNames
    {
        public const string Key = "PolicyName";
        public const string AdminPolicy = "AdminPolicy";
        public const string UserPolicy = "UserPolicy";
        public const string ManagerPolicy = "ManagerPolicy";
        public const string AdminUserPolicy = "AdminUserPolicy";
        public const string AdminManagerPolicy = "AdminManagerPolicy";
    }

    public static class PolicyServiceExtensions
    {
        public static IServiceCollection AddPlicyAuthenticationService(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.AdminPolicy, admin =>
                {
                    admin.RequireAuthenticatedUser().RequireRole(Roles.Admin)
                    .RequireClaim(Permissions.CanCreate, true.ToString())
                    .RequireClaim(Permissions.CanRead, true.ToString())
                    .RequireClaim(Permissions.CanDelete, true.ToString())
                    .RequireClaim(Permissions.CanUpdate, true.ToString());
                }).AddPolicy(PolicyNames.ManagerPolicy, manager =>
                {
                    manager.RequireAuthenticatedUser().RequireRole(Roles.Manager)
                    .RequireClaim(Permissions.CanCreate, true.ToString())
                    .RequireClaim(Permissions.CanRead, true.ToString())
                    .RequireClaim(Permissions.CanDelete, false.ToString())
                    .RequireClaim(Permissions.CanUpdate, true.ToString());
                }).AddPolicy(PolicyNames.UserPolicy, user =>
                {
                    user.RequireAuthenticatedUser().RequireRole(Roles.User)
                    .RequireClaim(Permissions.CanCreate, false.ToString())
                    .RequireClaim(Permissions.CanRead, false.ToString())
                    .RequireClaim(Permissions.CanDelete, false.ToString())
                    .RequireClaim(Permissions.CanUpdate, false.ToString());
                }).AddPolicy(PolicyNames.AdminManagerPolicy, adminManager =>
                {
                    adminManager.RequireAuthenticatedUser().RequireRole(Roles.Admin, Roles.User)
                    .RequireClaim(Permissions.CanCreate, true.ToString())
                    .RequireClaim(Permissions.CanRead, true.ToString())
                    .RequireClaim(Permissions.CanDelete, false.ToString())
                    .RequireClaim(Permissions.CanUpdate, true.ToString());
                }).AddPolicy(PolicyNames.AdminUserPolicy, adminUser =>
                {
                    adminUser.RequireAuthenticatedUser().RequireRole(Roles.Admin, Roles.User);
                });
        }
    }
}
