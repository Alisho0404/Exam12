using Domain.DTOs.EmailDTO;
using MimeKit.Text;

namespace Infrastructure.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessageDto model, TextFormat format);
    }
}
