using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Services.Interfaces
{
    /// <summary>
    ///     This will be responsible for performing the CRUD operations on a cart
    /// </summary>
    public interface ICartExperienceService
    {
        Task<CartItem> AddItemToCart(string phoneNumber, string itemName, int quantity);
        CartItem? GetCartItem(int cartItemId);
        IEnumerable<CartItem?> GetCartItemByItemName(string itemName);
        Task<CartItem?> RemoveCartItem(int cartItemId);
        IEnumerable<CartItem> GetAllCartItems(string phoneNumber, DateTimeOffset time,
            int quantity, string itemName);

    }
}
