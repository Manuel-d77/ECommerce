using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api
{
    public class CartItemRemovalResponse
    {
        public CartItem? CartItem { get; set; }

        public bool IsFoundAndDeleted { get; set; }
    }
}
