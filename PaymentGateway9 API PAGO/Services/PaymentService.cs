using PaymentGateway.Models;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(PaymentRequest request);
    }

    public class PaymentService : IPaymentService
    {
        public PaymentResponse ProcessPayment(PaymentRequest request)
        {
            return new PaymentResponse
            {
                IsSuccessful = true,
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Payment processed successfully"
            };
        }
    }
}