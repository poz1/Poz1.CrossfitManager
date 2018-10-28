using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Poz1.UpdatingNutrition.Models;

namespace Poz1.UpdatingNutrition.MobileAppService.Models
{
    public class WODRepository : IRepository<WOD>
    {
        private static ConcurrentDictionary<string, WOD> items =
          new ConcurrentDictionary<string, WOD>();

        public WODRepository()
        {
            Add(new WOD { Id = Guid.NewGuid().ToString(), TrainerName = "Marco", Date = new DateTime(2018, 10, 30), Calories = 2000, Exercises = new List<Exercise>(){
                    new Exercise(){ Name = "Push Press" }, new Exercise(){Name = "Shoulder Press"}, new Exercise(){Name = "Jumping Jacks"}
                } });
            Add(new WOD { Id = Guid.NewGuid().ToString(), TrainerName = "Marco", Date = new DateTime(2018, 10, 26), Calories = 1000 });
            Add(new WOD { Id = Guid.NewGuid().ToString(), TrainerName = "Marco", Date = new DateTime(2018, 10, 20), Calories = 1234 });
        }

        public WOD Get(string id)
        {
            return items[id];
        }

        public IEnumerable<WOD> GetAll()
        {
            return items.Values;
        }

        public void Add(WOD item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public WOD Find(string id)
        {
            WOD item;
            items.TryGetValue(id, out item);

            return item;
        }

        public WOD Remove(string id)
        {
            WOD item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(WOD item)
        {
            items[item.Id] = item;
        }
    }
}
