using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    /// <summary>
    ///     This will be responsible for creating CartItems and removing
    ///     them from Carts
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        ///     Adds an Item to a Cart
        /// </summary>
        public Task AddCartItem(Cart cart, Item item, int quantity);

        /// <summary>
        ///     Gets an Item from a Cart by the CartItemId
        /// </summary>
        public CartItem? GetCartItemById(int cartItemId);

        /// <summary>
        ///     Gets Item(s) from Cart(s) provided the item's name is present
        /// </summary>
        public IEnumerable<CartItem?> GetCartItemByItemName(string itemName);

        /// <summary>
        ///     Removes an Item from a Cart
        /// </summary>
        public Task<bool> RemoveCartItem(int cartItemId);

        /// <summary>
        ///     Updates the quantity of CartItems in a Cart
        /// </summary>
        public Task<CartItem> UpdateCartItemQuantity(CartItem cartItem, int quantity);

        public Task<CartItem> ReduceCartItemQuantity(CartItem cartItem, int quantity);

    }
}
