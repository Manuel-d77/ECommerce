using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        public Task AddCartItem(Cart cart, Item item, int quantity);

        public CartItem? GetCartItemById(int cartItemId);

        public IEnumerable<CartItem?> GetCartItemByItemName(string itemName);

        public Task RemoveCartItem(int cartItemId);

        public Task<CartItem> UpdateCartItemQuantity(CartItem cartItem, int quantity);

    }
}
