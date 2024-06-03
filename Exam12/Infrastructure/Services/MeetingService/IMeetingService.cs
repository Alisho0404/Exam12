using Domain.DTOs.MeetingDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MeetingService
{
    public interface IMeetingService
    {
        Task<PagedResponse<List<GetMeetingDto>>> GetMeetingesAsync(MeetingFilter filter);
        Task<PagedResponse<List<GetMeetingDto>>> GetUpcomingMeetingesAsync(MeetingFilter filter);
        Task<Response<GetMeetingDto>> GetMeetingByIdAsync(int MeetingId);
        Task<Response<string>> CreateMeetingAsync(CreateMeetingDto createMeeting);
        Task<Response<string>> UpdateMeetingAsync(UpdateMeetingDto updateMeeting);
        Task<Response<bool>> DeleteMeetingAsync(int MeetingId);
    }
}
