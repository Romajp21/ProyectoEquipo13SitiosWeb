namespace PaymentGateway.Models
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class PaymentResponse
    {
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
    }
}