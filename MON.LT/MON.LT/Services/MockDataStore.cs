using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MON.LT.Models;
using Bukimedia.PrestaSharp.Factories;
using Bukimedia.PrestaSharp.Entities;
using Xamarin.Forms;
using MON.LT.Configuration;

namespace MON.LT.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        //List<Item> items;
        readonly List<Item> items;
        public MockDataStore()
        {
            items = new List<Item>();
            
            ProductFactory productFactory = new ProductFactory(AppConfig.BaseUrl, AppConfig.Account, AppConfig.Password);
            ImageFactory imageFactory = new ImageFactory(AppConfig.BaseUrl, AppConfig.Account, AppConfig.Password);

            Dictionary<string, string> filter = new Dictionary<string, string>();
            filter.Add("id", "[1,10]");
            
            List<product> itemsx = productFactory.GetByFilter(filter, null, null);

            items = itemsx.Select(x => new Item()
            { id = unchecked((int)x.id), name = x.name[0].Value, reference = x.reference, imageId = Convert.ToInt32(x.id_default_image) }).ToList();
            
            for (int i = 0; i < items.Count; i++)
            {
                    items[i].image = imageFactory.GetProductImage(items[i].id, items[i].imageId);
            }
        
        }

 

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.id == item.id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<byte []> GetImageAsync(Item item)
        {
            ImageFactory imageFactory = new ImageFactory(AppConfig.BaseUrl, AppConfig.Account, AppConfig.Password);
            return await imageFactory.GetProductImageAsync(item.id, item.imageId);
        }
    }
}