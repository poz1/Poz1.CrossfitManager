using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.Services
{

        public interface IUserStore<T>
        {
            Task<bool> AddItemAsync(T item);
            Task<bool> UpdateItemAsync(T item);
            Task<bool> DeleteItemAsync(string id);
            Task<T> GetItemAsync(string id);
            Task<bool> Login(pwd lol);
            Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        }

}
