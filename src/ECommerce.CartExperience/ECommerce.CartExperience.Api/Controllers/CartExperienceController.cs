using ECommerce.CartExperience.Api.Models;
using ECommerce.CartExperience.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.CartExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartExperienceController : ControllerBase
    {
        private readonly ICartExperienceService _cartService;

        public CartExperienceController(ICartExperienceService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddItemToCartAsync(string phoneNumber,
            string itemName, int quantity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phoneNumber) || 
                    phoneNumber.Length != 10)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "PhoneNumber must be 10 digits long");
                }

                var newCartItem = await _cartService.AddItemToCart(
                    phoneNumber, itemName, quantity);

                return Ok(newCartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to add cart item: {ex.Message}");
            }
        }

        //HttpDelete to remove an item to the cart
        [HttpDelete]
        [Route("{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCartAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartService.RemoveCartItem(cartItemId);

                if (cartItem == null)
                {
                    return NotFound();
                }

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete cart item: {ex.Message}");
            }

        }

        //HttpGet to retrieve item(s) from the cart
        [HttpGet]
        [Route("allItems")]
        public ActionResult<IEnumerable<CartItem>> GetAllItemsFromCart(string phoneNumber,
            DateTimeOffset time, int quantity, string itemName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var cartItems = _cartService.GetAllCartItems(
                    phoneNumber, time, quantity, itemName);

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve cart items: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("id/{cartItemId}")]
        public ActionResult GetCartItemById(int cartItemId)
        {
            try
            {
                var cartItem =  _cartService.GetCartItem(cartItemId);

                if (cartItem == null)
                    return NotFound();

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve cart item: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{itemName}")]
        public ActionResult GetCartItemByItemName(string itemName)
        {
            try
            {
                var cartItem =  _cartService.GetCartItemByItemName(itemName);

                if (cartItem == null)
                    return NotFound();

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve cart item: {ex.Message}");
            }
        }

    }
}
