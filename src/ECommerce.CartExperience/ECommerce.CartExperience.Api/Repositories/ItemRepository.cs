using ECommerce.CartExperience.Api.Data;
using ECommerce.CartExperience.Api.Models;
using ECommerce.CartExperience.Api.Repositories.Interfaces;

namespace ECommerce.CartExperience.Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _dataContext;
        public ItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Item> AddItem(string name)
        {
            var item = GetItemByName(name);

            if (item == null)
            {
                var newItem = new Item
                {
                    ItemName = name,
                    UnitPrice = GenerateItemPrice()
                };

                var itemTask = await _dataContext.AddAsync(newItem);

                await _dataContext.SaveChangesAsync();

                item = itemTask.Entity;
            }

            return item;
        }

        private Item? GetItemByName(string name)
        {
            return _dataContext.Items.SingleOrDefault(i => i.ItemName == name);
        }

        private decimal GenerateItemPrice()
        {
            var randomPrice = Math.Round(new Random().NextDouble(), 2) + 10;
            return (decimal)randomPrice;
        }

    }
}
