using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.Services
{
    public class WODStore : IDataStore<WOD>
    {
        HttpClient client;
        IEnumerable<WOD> items;

        public WODStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<WOD>();
        }

        public async Task<IEnumerable<WOD>> GetItemsAsync(bool forceRefresh = true)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/WOD");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<WOD>>(json));
            }

            return items;
        }

        public async Task<WOD> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/WOD/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<WOD>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(WOD item)
        {
            if (item == null)
                return false;

            var serializedWOD = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/WOD", new StringContent(serializedWOD, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(WOD item)
        {
            if (item == null || item.Id == null)
                return false;

            var serializedWOD = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedWOD);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/WOD/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/WOD/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
