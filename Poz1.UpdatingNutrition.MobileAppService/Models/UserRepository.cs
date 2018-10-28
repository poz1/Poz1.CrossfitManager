using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Poz1.UpdatingNutrition.Models
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, User> items =
            new ConcurrentDictionary<string, User>();

        public UserRepository()
        {
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "Piero" });
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "Marco" });
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "Lollo" });
        }

        public User Get(string id)
        {
            return items[id];
        }


        public bool Login(string email, string pwd)
        {
            if (items.ContainsKey(email)  && items[email].Password == pwd)
                return true;
            else
                return false;
        }

        public IEnumerable<User> GetAll()
        {
            return items.Values;
        }

        public void Add(User item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public User Find(string id)
        {
            User item;
            items.TryGetValue(id, out item);

            return item;
        }

        public User Remove(string id)
        {
            User item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(User item)
        {
            items[item.Id] = item;
        }
    }
}
