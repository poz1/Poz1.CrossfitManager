using System;

using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
