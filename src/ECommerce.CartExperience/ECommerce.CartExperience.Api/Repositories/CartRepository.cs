using ECommerce.CartExperience.Api.Data;
using ECommerce.CartExperience.Api.Models;
using ECommerce.CartExperience.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.CartExperience.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _dataContext;

        public CartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            var newCart = await _dataContext.AddAsync(cart);

            await _dataContext.SaveChangesAsync();
            return newCart.Entity;
        }

        public Cart? GetCartById(int cartId)
        {
            return _dataContext.Carts.Include(c => c.CartItems).ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.Id == cartId);
        }

        public Cart? GetCartByPhoneNumber(string phoneNumber)
        {
            return _dataContext.Carts.Include(c => c.CartItems).ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

    }
}
