using ComplationService.API.Abstractions;
using ComplationService.API.DTOs;
using ComplationService.API.Models;
using ComplationService.API.Settings;
using MongoDB.Driver;
using SharedService.Identity;
using SharedService.Responses;

namespace ComplationService.API.Services
{
    public class ComplaintService : IComplaintService
    {

        private readonly IMongoCollection<Complaint> _complaintCollection;
        readonly ISharedIdentityService _sharedIdentityService;

        public ComplaintService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _complaintCollection = database.GetCollection<Complaint>(databaseSettings.ComplaintCollectionName);
        }
        public async Task<Response<bool>> AddComplaintAsync(string ComplaintDescription)
        {
            if(ComplaintDescription.Length > 5)
            {
                Complaint complaint = new()
                {
                    ComplaintDescription = ComplaintDescription,
                    UserId = _sharedIdentityService.GetUserIdAsync(),
                    UserName = _sharedIdentityService.GetUserNameAndSurName().Item1,
                    UserSurname = _sharedIdentityService.GetUserNameAndSurName().Item2
                };
               await _complaintCollection.InsertOneAsync(complaint);
                return Response<bool>.Success(true , StatusCodes.Status200OK);
            }
            return Response<bool>.Fail("Description is not added",StatusCodes.Status400BadRequest);
        }

        public async Task<Response<List<ComplaintDTO>>> GetAllComplaints()
        {
         var complaints = await _complaintCollection.Find(x => true).ToListAsync();
         return Response<List<ComplaintDTO>>.Success(complaints.Select(x => new ComplaintDTO
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
             UserSurname = x.UserSurname,
             ComplaintDescription = x.ComplaintDescription,
             ComplaintId = x.Id.ToString(),
                }).ToList() , StatusCodes.Status200OK);
        }

        public async Task<Response<List<ComplaintDTO>>> GetComplaintsByUserId()
        {
            var complaints = await _complaintCollection.Find(x => x.UserId == _sharedIdentityService.GetUserIdAsync()).ToListAsync();
            return Response<List<ComplaintDTO>>.Success(complaints.Select(x => new ComplaintDTO
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserSurname = x.UserSurname,
                ComplaintDescription = x.ComplaintDescription,
                ComplaintId = x.Id.ToString(),
            }).ToList(), StatusCodes.Status200OK);
        }
    }
}
