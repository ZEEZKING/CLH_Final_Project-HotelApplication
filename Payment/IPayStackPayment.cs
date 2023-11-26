using CLH_Final_Project.Dtos.RequsetModel;

namespace CLH_Final_Project.Payment
{
    public interface IPayStackPayment
    {
        Task<string> InitiatePayment(CreatePaymentRequestModel model, int userId,int bookingId,int? orderId);
        Task<string> GetTransactionRecieptAsync(string transactionReference);
    }
}
