using System.Text.Json.Serialization;

namespace ECommerce.CartExperience.Api
{
    public class CartRequest
    {
        public string PhoneNumber { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}
