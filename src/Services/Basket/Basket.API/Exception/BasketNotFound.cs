
namespace Basket.API.Exception
{
    public class BasketNotFound : NotFoundException
    {
        public BasketNotFound(string userName) : base($"Basket found.",userName)
        {
        }
    }
}
