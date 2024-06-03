using Domain.DTOs.MeetingDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;


namespace Infrastructure.Services.MeetingService
{
    public class MeetingService(IFileService fileService, ILogger <MeetingService> logger, DataContext context) : IMeetingService
    {
        public async Task<Response<string>> CreateMeetingAsync(CreateMeetingDto createMeeting)
        {
            try
            {
                logger.LogInformation("Starting method {CreateMeetingAsync} in time:{DateTime} ", "CreateMeetingAsync",
                    DateTimeOffset.UtcNow);
                var newMeeting = new Meeting()
                {
                    Title = createMeeting.Title,
                    Description = createMeeting.Description,
                    StartDateTime = createMeeting.StartDateTime,
                    EndDateTime = createMeeting.EndDateTime,
                    UserId = createMeeting.UserId,
                    CreatedAt = createMeeting.CreatedAt,
                    UpdatedAt = createMeeting.UpdatedAt,

                };

                await context.Meetings.AddAsync(newMeeting);
                await context.SaveChangesAsync();
                logger.LogInformation("Finished method {CreateMeetingAsync} in time:{DateTime} ", "CreateMeetingAsync",
                    DateTimeOffset.UtcNow);
                return new Response<string>($"Successfully created a new Meeting by id:{newMeeting.Id}");
            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteMeetingAsync(int MeetingId)
        {
            try
            {
                logger.LogInformation("Starting method {DeleteMeetingAsync} in time:{DateTime} ", "DeleteMeetingAsync",
                    DateTimeOffset.UtcNow);

                var existing = await context.Meetings.FirstOrDefaultAsync(x => x.Id == MeetingId);
                if (existing == null)
                    return new Response<bool>(HttpStatusCode.BadRequest, $"Meeting not found by id:{MeetingId}");               
                context.Meetings.Remove(existing);
                await context.SaveChangesAsync();

                logger.LogInformation("Finished method {DeleteMeetingAsync} in time:{DateTime} ", "DeleteMeetingAsync",
                    DateTimeOffset.UtcNow);
                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new PagedResponse<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetMeetingDto>> GetMeetingByIdAsync(int MeetingId)
        {
            try
            {
                logger.LogInformation("Starting method {GetMeetingByIdAsync} in time:{DateTime} ", "GetMeetingByIdAsync",
                    DateTimeOffset.UtcNow);
                var existing = await context.Meetings.Where(x => x.Id == MeetingId).Select(x => new GetMeetingDto()
                { 
                    Id=x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    UserId = x.UserId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).FirstOrDefaultAsync();
                if (existing is null)
                {
                    logger.LogWarning("Not found Meeting with id={Id},time={DateTimeNow}", MeetingId, DateTime.UtcNow);
                    return new Response<GetMeetingDto>(HttpStatusCode.BadRequest, "Meeting not found");
                }

                logger.LogInformation("Finished method {GetMeetingByIdAsync} in time:{DateTime} ", "GetMeetingByIdAsync",
                    DateTimeOffset.UtcNow);
                return new Response<GetMeetingDto>(existing);
            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new Response<GetMeetingDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PagedResponse<List<GetMeetingDto>>> GetMeetingesAsync(MeetingFilter filter)
        {
            try
            {
                logger.LogInformation("Starting method {GetMeetingesAsync} in time:{DateTime} ", "GetMeetingesAsync",
                    DateTimeOffset.UtcNow);

                var meetings = context.Meetings.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Description))
                    meetings = meetings.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

                var response = await meetings.Select(x => new GetMeetingDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    UserId = x.UserId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

                var totalRecord = await meetings.CountAsync();

                logger.LogInformation("Finished method {GetMeetingesAsync} in time:{DateTime} ", "GetMeetingesAsync",
                    DateTimeOffset.UtcNow);

                return new PagedResponse<List<GetMeetingDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);
            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new PagedResponse<List<GetMeetingDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PagedResponse<List<GetMeetingDto>>> GetUpcomingMeetingesAsync(MeetingFilter filter)
        {
            try
            {
                logger.LogInformation("Starting method {GetMeetingesAsync} in time:{DateTime} ", "GetMeetingesAsync",
                   DateTimeOffset.UtcNow);
                var upcomings = await (from m in context.Meetings
                                       join n in context.Notifications on m.Id equals n.MeetingId
                                       select new GetMeetingDto()
                                       {
                                           Id = m.Id,
                                           Title = m.Title,
                                           Description = m.Description,
                                           StartDateTime = m.StartDateTime,
                                           EndDateTime = m.EndDateTime,
                                           UserId = m.UserId,
                                           CreatedAt = m.CreatedAt,
                                           UpdatedAt = m.UpdatedAt
                                       }).ToListAsync();
                var response = upcomings.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList();
                var totalRecord=upcomings.Count();

                logger.LogInformation("Finished method {GetMeetingesAsync} in time:{DateTime} ", "GetMeetingesAsync",
                    DateTimeOffset.UtcNow);
                return new PagedResponse<List<GetMeetingDto>>(response,filter.PageNumber,filter.PageSize,totalRecord); 

            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new PagedResponse<List<GetMeetingDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateMeetingAsync(UpdateMeetingDto updateMeeting)
        {
            try
            {
                logger.LogInformation("Starting method {UpdateMeetingAsync} in time:{DateTime} ", "UpdateMeetingAsync",
                    DateTimeOffset.UtcNow);
                var existing = await context.Meetings.FirstOrDefaultAsync(x => x.Id == updateMeeting.Id);
                if (existing is null)
                {
                    logger.LogWarning("Meeting not found by id:{Id},time:{DateTimeNow} ", updateMeeting.Id,
                        DateTimeOffset.UtcNow);
                    return new Response<string>(HttpStatusCode.BadRequest, "Meeting not found");
                }



                existing.Title = updateMeeting.Title;
                existing.Description = updateMeeting.Description;
                existing.StartDateTime = updateMeeting.StartDateTime;
                existing.EndDateTime = updateMeeting.EndDateTime;
                existing.UserId = updateMeeting.UserId;
                existing.UpdatedAt = updateMeeting.UpdatedAt;

                await context.SaveChangesAsync();
                logger.LogInformation("Finished method {UpdateMeetingAsync} in time:{DateTime} ", "UpdateMeetingAsync",
                    DateTimeOffset.UtcNow);
                return new Response<string>($"Successfully updated Meeting by id:{existing.Id}");
            }
            catch (Exception e)
            {
                logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
