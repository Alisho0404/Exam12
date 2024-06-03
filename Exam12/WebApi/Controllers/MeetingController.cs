using Domain.Constants;
using Domain.DTOs.MeetingDTOs;
using Domain.Filters;
using Infrastructure.Permissions;
using Infrastructure.Services.HashService;
using Infrastructure.Services.MeetingService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController(IMeetingService _MeetingService) : ControllerBase
    {
        [HttpGet("Meetings")]
        [PermissionAuthorize(Permissions.Meeting.View)]
        public async Task<IActionResult> GetMeetinges([FromQuery] MeetingFilter filter)
        {
            var res1 = await _MeetingService.GetMeetingesAsync(filter);
            return StatusCode(res1.StatusCode, res1);
        }

        [HttpGet("Upcoming Meetings")]
        [PermissionAuthorize(Permissions.Meeting.View)]
        public async Task<IActionResult> GetUpcomingMeetings([FromQuery] MeetingFilter filter)
        {
            var res1 = await _MeetingService.GetUpcomingMeetingesAsync(filter);
            return StatusCode(res1.StatusCode, res1);
        }

        [HttpGet("{MeetingId:int}")]
        [PermissionAuthorize(Permissions.Meeting.View)]
        public async Task<IActionResult> GetMeetingById(int MeetingId)
        {
            var res1 = await _MeetingService.GetMeetingByIdAsync(MeetingId);
            return StatusCode(res1.StatusCode, res1);
        }

        [HttpPost("create")]
        [PermissionAuthorize(Permissions.Meeting.Create)]
        public async Task<IActionResult> CreateMeeting([FromForm] CreateMeetingDto createMeeting)
        {
            var result = await _MeetingService.CreateMeetingAsync(createMeeting);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        [PermissionAuthorize(Permissions.Meeting.Edit)]
        public async Task<IActionResult> UpdateMeeting([FromForm] UpdateMeetingDto updateMeeting)
        {
            var result = await _MeetingService.UpdateMeetingAsync(updateMeeting);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{MeetingId:int}")]
        [PermissionAuthorize(Permissions.Meeting.Delete)]
        public async Task<IActionResult> DeleteMeeting(int MeetingId)
        {
            var result = await _MeetingService.DeleteMeetingAsync(MeetingId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
