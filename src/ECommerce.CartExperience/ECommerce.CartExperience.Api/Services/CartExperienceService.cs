using ECommerce.CartExperience.Api.Models;
using ECommerce.CartExperience.Api.Repositories.Interfaces;
using ECommerce.CartExperience.Api.Services.Interfaces;

namespace ECommerce.CartExperience.Api.Services
{
    public class CartExperienceService : ICartExperienceService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;

        public CartExperienceService(ICartItemRepository cartItemRepository,
            ICartRepository cartRepository, IItemRepository itemRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
        }

        public async Task<CartItem> AddItemToCart(string phoneNumber, string itemName, int quantity)
        {
            //get cart by its phoneNumber
            var availableCart = _cartRepository.GetCartByPhoneNumber(phoneNumber);

            //if the cart does not exist, create a new one
            if (availableCart == null)
            {
                availableCart = await _cartRepository.AddCart(new Cart
                {
                    PhoneNumber = phoneNumber,
                });
            }

            if (availableCart?.CartItems == null || !availableCart.CartItems.Any())
            {
                //create the item
                var item = await _itemRepository.AddItem(itemName);

                await _cartItemRepository.AddCartItem(availableCart!, item, quantity);

                return availableCart?.CartItems.FirstOrDefault(c => c.Item.ItemId == item.ItemId)!;
            }

            //in the carts list of items, search for item with the same name
            var existingCartItem = availableCart.CartItems.FirstOrDefault(
                c => c.Item.ItemName == itemName.ToLower());

            if (existingCartItem != null)
            {
                //if found, increase its quantity
                await _cartItemRepository.UpdateCartItemQuantity(
                    existingCartItem, quantity);

                return existingCartItem;
            }

            var newItem = await _itemRepository.AddItem(itemName);

            await _cartItemRepository.AddCartItem(availableCart, newItem, quantity);

            return availableCart!.CartItems.FirstOrDefault(c => c.Item.ItemName == itemName)!;
        }

        public IEnumerable<CartItem> GetAllCartItems(string phoneNumber, DateTimeOffset time,
            int quantity, string itemName)
        {
            var allCartItems = new List<CartItem>();

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return allCartItems;
            }

            var cart = _cartRepository.GetCartByPhoneNumber(phoneNumber);

            if (cart == null)
            {
                return allCartItems;
            }

            allCartItems = cart.CartItems.Where(c => c.TimeAddedToCart == time
                || c.ItemQuantity == quantity || c.Item.ItemName == itemName).ToList();

            if (!allCartItems.Any())
            {
                return cart.CartItems.ToList();
            }

            return allCartItems;
        }

        public CartItem? GetCartItem(int cartItemId)
        {
            if (cartItemId <= 0)
            {
                throw new ArgumentException("Kindly provide the correct cartItem id");
            }

            return _cartItemRepository.GetCartItemById(cartItemId);
        }

        public async Task<bool> RemoveCartItem(int cartItemId)
        {
            var result = false;

            var cartItem =  GetCartItem(cartItemId);

            if (cartItem == null)
            {
                return result;
            }

            return await _cartItemRepository.RemoveCartItem(cartItemId);
        }

        public async Task<CartItemRemovalResponse> ReduceCartItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = GetCartItem(cartItemId);

            var response = new CartItemRemovalResponse();

            if (cartItem == null)
            {
                response.CartItem = null;
                response.IsFoundAndDeleted = false;

                return response;
            }

            if (cartItem.ItemQuantity < quantity)
            {
                throw new ArgumentException($"The number of \"{cartItem.Item.ItemName}\"" +
                    $" in your Cart is {cartItem.ItemQuantity}. CartItem quantity reduction failed");
            }

            if (cartItem.ItemQuantity > quantity && cartItem.ItemQuantity != quantity)
            {
                response.CartItem = await _cartItemRepository.ReduceCartItemQuantity(cartItem, quantity);
                response.IsFoundAndDeleted = true;
                return response;
            }

            response.IsFoundAndDeleted = await _cartItemRepository.RemoveCartItem(cartItemId);

            return response;
        }

    }
}
