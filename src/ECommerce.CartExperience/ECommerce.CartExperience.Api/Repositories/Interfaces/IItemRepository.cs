using ECommerce.CartExperience.Api.Models;

namespace ECommerce.CartExperience.Api.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> AddItem(string name);

    }
}
