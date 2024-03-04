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
        public async Task<IActionResult> AddItemToCartAsync([FromBody]CartRequest cartRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(cartRequest.PhoneNumber) ||
                    cartRequest.PhoneNumber.Length != 10)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "PhoneNumber must be 10 digits");
                }

                if (!int.TryParse(cartRequest.PhoneNumber, out _))
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "PhoneNumber must be 10 digits");
                }

                if (string.IsNullOrEmpty(cartRequest.ItemName))
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "Kindly provide the item's name");
                }

                if (cartRequest.Quantity <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "The item's quantity must be a positive whole number");
                }

                var newCartItem = await _cartService.AddItemToCart(
                    cartRequest.PhoneNumber, cartRequest.ItemName, cartRequest.Quantity);

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
                if (cartItemId <= 0 )
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "Kindly provide the correct CartItem Id and the quantity to remove from your cart");
                }
                
                var result = await _cartService.RemoveCartItem(cartItemId);

                if (result == false)
                {
                    return NotFound($"The item with Id:{cartItemId} does not exist in the cart");
                }

                return Ok($"Item with Id: {cartItemId} has been successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete cart item: {ex.Message}");
            }

        }

        //HttpPut to update an item's quantity in the cart
        [HttpPut]
        [Route("reduce/{cartItemId}")]
        public async Task<IActionResult> ReduceItemQuantityInCartAsync(int cartItemId, int quantity)
        {
            try
            {
                if (cartItemId <= 0 || quantity <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "Kindly provide the correct CartItem Id and the quantity to remove from your cart");
                }
                var cartItem = await _cartService.ReduceCartItemQuantity(cartItemId, quantity);

                if (cartItem .IsFoundAndDeleted == false)
                {
                    return NotFound($"The item with Id:{cartItemId} does not exist in the cart");
                }

                return Ok($"The quantity of the item with Id:{cartItemId} has been successfully updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to update cart item's quantity: {ex.Message}");
            }

        }

        //HttpGet to retrieve item(s) from the cart
        [HttpGet]
        [Route("allItems/{phoneNumber}")]
        public ActionResult<IEnumerable<CartItem>> GetAllItemsFromCart(
            string phoneNumber, DateTimeOffset time, int quantity, string? itemName)
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
                    return NotFound($"The item with Id:{cartItemId} does not exist in the cart");

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
