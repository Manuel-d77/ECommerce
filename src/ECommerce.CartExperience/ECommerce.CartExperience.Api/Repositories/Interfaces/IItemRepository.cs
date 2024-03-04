using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    /// <summary>
    ///     This will be responsible for creating Items
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        ///     Creates an Item
        /// </summary>
        Task<Item> AddItem(string name);

    }
}
