using System;
namespace Poz1.UpdatingNutrition.Models
{
    public class Food
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public NutritionFacts NutritionFacts {get; set;}

        public Food()
        {
        }
    }
}
