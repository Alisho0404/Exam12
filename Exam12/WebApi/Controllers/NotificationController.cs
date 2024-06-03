using Domain.Constants;
using Domain.DTOs.NotificationDTOs;
using Domain.Filters;
using Infrastructure.Permissions;
using Infrastructure.Services.HashService;
using Infrastructure.Services.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController(INotificationService _NotificationService) : ControllerBase
    {
        [HttpGet("Notifications")]
        [PermissionAuthorize(Permissions.Notification.View)]
        public async Task<IActionResult> GetNotificationes([FromQuery] NotificationFilter filter)
        {
            var res1 = await _NotificationService.GetNotificationesAsync(filter);
            return StatusCode(res1.StatusCode, res1);
        }

        [HttpGet("{NotificationId:int}")]
        [PermissionAuthorize(Permissions.Notification.View)]
        public async Task<IActionResult> GetNotificationById(int NotificationId)
        {
            var res1 = await _NotificationService.GetNotificationByIdAsync(NotificationId);
            return StatusCode(res1.StatusCode, res1);
        }

        [HttpPost("create")]
        [PermissionAuthorize(Permissions.Notification.Create)]
        public async Task<IActionResult> CreateNotification([FromForm] CreateNotificationDto createNotification)
        {
            var result = await _NotificationService.CreateNotificationAsync(createNotification);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        [PermissionAuthorize(Permissions.Notification.Edit)]
        public async Task<IActionResult> UpdateNotification([FromForm] UpdateNotificationDto updateNotification)
        {
            var result = await _NotificationService.UpdateNotificationAsync(updateNotification);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{NotificationId:int}")]
        [PermissionAuthorize(Permissions.Notification.Delete)]
        public async Task<IActionResult> DeleteNotification(int NotificationId)
        {
            var result = await _NotificationService.DeleteNotificationAsync(NotificationId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
