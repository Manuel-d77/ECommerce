using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    /// <summary>
    ///     This will be responsible for creating Carts
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        ///     Creates a Cart
        /// </summary>
        public Task<Cart> AddCart(Cart cart);

        /// <summary>
        ///     Gets a Cart by its Phone number
        /// </summary>
        public Cart? GetCartByPhoneNumber(string phoneNumber);

        /// <summary>
        ///     Gets a Cart by its Id 
        /// </summary>
        public Cart? GetCartById(int cartId);

    }
}
