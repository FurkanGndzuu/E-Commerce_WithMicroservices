using MongoDB.Driver;
using PaymentService.API.Abstractions;
using PaymentService.API.DTOs;
using PaymentService.API.Models;
using PaymentService.API.Settings;
using SharedService.Identity;
using SharedService.Responses;

namespace PaymentService.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMongoCollection<Wallet> _walletCollection;
        private readonly ISharedIdentityService _identityService;
        public PaymentService(IDatabaseSettings databaseSettings, ISharedIdentityService identityService)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _walletCollection = database.GetCollection<Wallet>(databaseSettings.WalletCollectionName);
            _identityService = identityService;
        }
        public async Task<Response<bool>> LoadMoney(LoadMoneyDTO dto)
        {


         Wallet wal =  await _walletCollection.Find(x => x.UserId.Equals(_identityService.GetUserIdAsync())).FirstOrDefaultAsync();
            if(wal is not null)
            {
                wal.Amount = wal.Amount + dto.Amount;
                await _walletCollection.ReplaceOneAsync(x => x.UserId == wal.UserId, wal);
                return Response<bool>.Success(true, StatusCodes.Status200OK);
            }

            Wallet wallet = new()
            {
                Amount = dto.Amount,
                UserId = _identityService.GetUserIdAsync()
            };
            await _walletCollection.InsertOneAsync(wallet);
            return Response<bool>.Success(true, StatusCodes.Status200OK);
        }
        public async Task<Response<GetWalletDTO>> GetWallet()
        {
         var wallet = await _walletCollection.Find(x => x.UserId == _identityService.GetUserIdAsync()).FirstOrDefaultAsync();
           if(wallet is not null)
            {
                GetWalletDTO walletDTO = new()
                {
                    Amount = wallet.Amount,
                    UserId = wallet.UserId
                };
                return Response<GetWalletDTO>.Success(walletDTO, StatusCodes.Status200OK);
            }
            Wallet wal = new()
            {
                UserId = _identityService.GetUserIdAsync(),
                Amount = 0
            };
            await _walletCollection.InsertOneAsync(wal);
            GetWalletDTO walDTO = new()
            {
                Amount = 0,
                UserId = wal.UserId
            };
            return Response<GetWalletDTO>.Success(walDTO, StatusCodes.Status200OK);
        }
    }
}
