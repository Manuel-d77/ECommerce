using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.CartExperience.Api.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }

        public Item Item { get; set; }

        public int ItemQuantity { get; set; }

        public DateTimeOffset? TimeAddedToCart { get; set; }

    }
}
