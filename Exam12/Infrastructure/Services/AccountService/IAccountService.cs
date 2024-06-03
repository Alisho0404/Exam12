using Domain.DTOs.AccountDTOs;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AccountService
{
    public interface IAccountService
    {
        Task<Response<string>> Register(RegisterDto model);
        Task<Response<string>> Login(LoginDto model);
        Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<Response<string>> ForgotPasswordCodeGenerator(ForgotPasswordDto forgotPasswordDto);
        Task<Response<string>> ChangePassword(ChangePasswordDto passwordDto, int userId);
        Task<Response<string>> DeleteAccount(int userId);
    }
}
