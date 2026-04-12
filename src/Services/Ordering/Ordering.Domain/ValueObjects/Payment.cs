namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; set; } = default!;

        public string CardNumber { get; set; } = default!;

        public string Expiration { get; set; } = default!;

        public string CVV { get; set; } = default!;

        public int PaymentMethod { get; set; } = default!;

        protected Payment()
        {
        }

        public Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            var payment = new Payment
            {
                CardName = cardName,
                CardNumber = cardNumber,
                Expiration = expiration,
                CVV = cvv,
                PaymentMethod = paymentMethod
            };
            return payment;
        }
    }
}
