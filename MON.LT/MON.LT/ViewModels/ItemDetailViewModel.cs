using System;

using MON.LT.Models;

namespace MON.LT.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.name;
            Item = item;
        }
    }
}
