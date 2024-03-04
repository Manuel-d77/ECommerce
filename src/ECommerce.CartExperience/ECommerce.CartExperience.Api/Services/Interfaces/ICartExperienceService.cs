using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Services.Interfaces
{
    /// <summary>
    ///     This will be responsible for performing the CRUD operations on a Cart
    /// </summary>
    public interface ICartExperienceService
    {
        Task<CartItem> AddItemToCart(string phoneNumber, string itemName, int quantity);
        CartItem? GetCartItem(int cartItemId);
        Task<bool> RemoveCartItem(int cartItemId);
        Task<CartItemRemovalResponse> ReduceCartItemQuantity(int cartItemId, int quantity);
        IEnumerable<CartItem> GetAllCartItems(string phoneNumber, DateTimeOffset time,
            int quantity, string itemName);

    }
}
