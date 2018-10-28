using System;
using System.Collections.Generic;

namespace Poz1.UpdatingNutrition.Models
{
    public interface IUserRepository
    {
        void Add(User user);
        bool Login(string email, string psw);
        void Update(User user);
        User Remove(string key);
        User Get(string id);
        IEnumerable<User> GetAll();
    }
}
