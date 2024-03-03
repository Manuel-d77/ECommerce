using ECommerce.CartExperience.Api.Data;
using ECommerce.CartExperience.Api.Models;
using ECommerce.CartExperience.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.CartExperience.Api.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _dataContext;

        public CartItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddCartItem(Cart cart, Item item, int quantity)
        {
            var cartItem = new CartItem
            {
                Item = item,
                ItemQuantity = quantity,
                TimeAddedToCart = DateTimeOffset.UtcNow
            };

            var cartItemTask = await _dataContext.AddAsync(cartItem);
            await _dataContext.SaveChangesAsync();

            _dataContext.Update(cart);

            if (cart.CartItems == null || !cart.CartItems.Any())
            {
                cart.CartItems = new List<CartItem> { cartItemTask.Entity };
            }
            else
            {
                cart.CartItems.Add(cartItemTask.Entity);
            }

            await _dataContext.SaveChangesAsync();
        }

        public CartItem? GetCartItemById(int cartItemId)
        {
            return _dataContext.CartItems.Include(ci => ci.Item)
                .SingleOrDefault(c => c.CartItemId == cartItemId);
        }

        public IEnumerable<CartItem?> GetCartItemByItemName(string itemName)
        {
            var res = _dataContext.CartItems.Include(ci => ci.Item)
                .Where(c => c.Item.ItemName == itemName);
            //.SingleOrDefault(c => c.Item.ItemName == itemName);
            return res;
        }

        public async Task RemoveCartItem(int cartItemId)
        {
            var cartItem =  GetCartItemById(cartItemId);

            if (cartItem == null)
            {
                throw new ArgumentException($"The provided cartItemId: " +
                    $"{cartItemId} does not exist");
            }

            _dataContext.CartItems.Remove(cartItem);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<CartItem> UpdateCartItemQuantity(CartItem cartItem, int quantity)
        {
            _dataContext.Update(cartItem);

            cartItem.ItemQuantity += quantity;

            await _dataContext.SaveChangesAsync();

            return cartItem;
        }

    }
}
