using PaymentService.API.DTOs;
using SharedService.Responses;

namespace PaymentService.API.Abstractions
{
    public interface IPaymentService
    {
        Task<Response<bool>> LoadMoney(LoadMoneyDTO dto);
        Task<Response<GetWalletDTO>> GetWallet();
    }
}
