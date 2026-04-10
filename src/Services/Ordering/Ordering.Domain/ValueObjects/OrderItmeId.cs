namespace Ordering.Domain.ValueObjects
{
    public record OrderItmeId
    {
        public Guid Value { get; set; }

        public OrderItmeId(Guid value)
        {
            Value = value;
        }

        public static OrderItmeId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItmeId cannot be empty");
            }
            return new OrderItmeId(value);
        }
    }
}
