using Infrastructure.Data;
using Infrastructure.Permissions;
using Infrastructure.Seed;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.EmailService;
using Infrastructure.Services.FileService;
using Infrastructure.Services.HashService;
using Infrastructure.Services.MeetingService;
using Infrastructure.Services.NotificationService;
using Infrastructure.Services.RoleService;
using Infrastructure.Services.UserRoleService;
using Infrastructure.Services.UserService;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("dgdsgsd");
        services.AddDbContext<DataContext>(configure =>
            configure.UseNpgsql("Server=localhost;Port=5432;Database=Exam12;User Id=postgres;Password=909662643"));

        services.AddScoped<Seeder>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IEmailService, IEmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IMeetingService, MeetingService>();       
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    }
}