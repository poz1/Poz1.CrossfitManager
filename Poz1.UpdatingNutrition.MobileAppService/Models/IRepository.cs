using System;
using System.Collections.Generic;

namespace Poz1.UpdatingNutrition.Models
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        T Remove(string key);
        T Get(string id);
        IEnumerable<T> GetAll();
    }
}
