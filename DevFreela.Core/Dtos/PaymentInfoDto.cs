namespace DevFreela.Core.Dtos
{
    public class PaymentInfoDto(int id, string creditCardNumber, string cvv, string expiresAt, string fullName)
    {
        public int Id { get; set; } = id;
        public string CreditCardNumber { get; set; } = creditCardNumber;
        public string Cvv { get; set; } = cvv;
        public string ExpiresAt { get; set; } = expiresAt;
        public string FullName { get; set; } = fullName;
    }
}
