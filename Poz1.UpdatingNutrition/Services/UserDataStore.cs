using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.Services
{
    public class UserDataStore: IUserStore<User>
        {
            HttpClient client;
            IEnumerable<User> users;

            public UserDataStore()
            {
                client = new HttpClient();
                client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

                users = new List<User>();
            }

            public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
            {
                if (forceRefresh)
                {
                    var json = await client.GetStringAsync($"api/user");
                    users = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User>>(json));
                }

                return users;
            }

            public async Task<User> GetItemAsync(string id)
            {
                if (id != null)
                {
                    var json = await client.GetStringAsync($"api/user/{id}");
                    return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
                }

                return null;
            }

            public async Task<bool> AddItemAsync(User user)
            {
                if (user == null)
                    return false;

                var serializedUser = JsonConvert.SerializeObject(user);

                var response = await client.PostAsync($"api/user", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }

            public async Task<bool> UpdateItemAsync(User user)
            {
                if (user == null || user.Id == null)
                    return false;

                var serializedUser = JsonConvert.SerializeObject(user);
                var buffer = Encoding.UTF8.GetBytes(serializedUser);
                var byteContent = new ByteArrayContent(buffer);

                var response = await client.PutAsync(new Uri($"api/user/{user.Id}"), byteContent);

                return response.IsSuccessStatusCode;
            }

            public async Task<bool> DeleteItemAsync(string id)
            {
                if (string.IsNullOrEmpty(id))
                    return false;

                var response = await client.DeleteAsync($"api/user/{id}");

                return response.IsSuccessStatusCode;
            }

         
            public async Task<bool> Login(pwd lol)
            {
                var serializedUser = JsonConvert.SerializeObject(lol);

                var response = await client.PostAsync($"api/user/login", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }
    }

