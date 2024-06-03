using Domain.DTOs.NotificationDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.NotificationService
{
    public interface INotificationService
    {
        Task<PagedResponse<List<GetNotificationDto>>> GetNotificationesAsync(NotificationFilter filter);
        Task<Response<GetNotificationDto>> GetNotificationByIdAsync(int NotificationId);
        Task<Response<string>> CreateNotificationAsync(CreateNotificationDto createNotification);
        Task<Response<string>> UpdateNotificationAsync(UpdateNotificationDto updateNotification);
        Task<Response<bool>> DeleteNotificationAsync(int NotificationId);
    }
}
