using ComplationService.API.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        readonly IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet]
        [Authorize("Read")]
        public async Task<IActionResult> GetAllComplaints() => Ok(await _complaintService.GetAllComplaints());
        [HttpGet("userId")]
        [Authorize("Read")]
        public async Task<IActionResult> GetAllComplaintsByUserId() => Ok(await _complaintService.GetComplaintsByUserId());
        [HttpPost]
        [Authorize("Write")]
        public async Task<IActionResult> CreateComplaint(string Description) => Ok(await _complaintService.AddComplaintAsync(Description));
    }
}
