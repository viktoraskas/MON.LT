using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bukimedia.PrestaSharp.Entities;
using Bukimedia.PrestaSharp.Factories;
using MON.LT.Configuration;
using Xamarin.Forms;

using MON.LT.Models;
using MON.LT.Views;

namespace MON.LT.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadMoreDataCommand { get; set; }
        //public Command LoadMoreDataCommand => new Command(GetNextPageOfData);

        async Task ExecuteLoadMoreDataCommand()
        {
            IsBusy = true;
            ProductFactory productFactory = new ProductFactory(AppConfig.BaseUrl, AppConfig.Account, AppConfig.Password);
            Dictionary<string, string> filter = new Dictionary<string, string>();
            filter.Add("id", "[1,10]");
            List<product> itemsx = await productFactory.GetByFilterAsync(filter, null, null);
            
            for (int i = 0; i < itemsx.Count; i++)
            {
                Item item = new Item()
                    { id = Convert.ToInt32(itemsx[i].id), 
                        name = itemsx[i].name[0].Value, 
                        reference = itemsx[i].reference, 
                        imageId = Convert.ToInt32(itemsx[i].id_default_image) };
                Items.Add(item);
            }

            //return await productFactory.GetByFilterAsync(filter,null,null);
            //await DataStore.LoadMoreItems();
            MessagingCenter.Send(this, "AddItems", Items);
            IsBusy = false;
        }
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadMoreDataCommand = new Command(async () => await ExecuteLoadMoreDataCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemsPage,Item>(this, "AddItems", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);

            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}