namespace Shopping.Web.Models.Ordering
{
    public record OrderModel
    (
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> OrderItems
    );

    public record AddressDto
    (
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode
    );

    public record OrderItemDto
    (
        Guid OrderId,
        Guid ProductId,
        int Quantity,
        decimal Price
    );

    public record PaymentDto
    (
        string CardName,
        string CardNumber,
        string Expiration,
        string Cvv,
        int PaymentMethod
    );

    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4,
        Completed = 5,
        Draft = 6
    }

    public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);

    public record GetOrderByCustomerResponse(IEnumerable<OrderModel> Orders);

    public record GetOrderByNameResponse(IEnumerable<OrderModel> Orders);
}
