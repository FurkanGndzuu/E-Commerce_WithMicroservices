using ComplationService.API.DTOs;
using SharedService.Responses;

namespace ComplationService.API.Abstractions
{
    public interface IComplaintService
    {
        Task<Response<bool>> AddComplaintAsync(string ComplaintDescription);
        Task<Response<List<ComplaintDTO>>> GetAllComplaints();
        Task<Response<List<ComplaintDTO>>> GetComplaintsByUserId();
 

    }
}
