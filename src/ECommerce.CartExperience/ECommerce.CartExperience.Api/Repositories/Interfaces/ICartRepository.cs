using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Task<Cart> AddCart(Cart cart);

        public Cart? GetCartByPhoneNumber(string phoneNumber);

        public Cart? GetCartById(int cartId);

    }
}
