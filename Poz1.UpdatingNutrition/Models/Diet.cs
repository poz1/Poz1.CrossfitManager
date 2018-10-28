using System;
using System.Collections.Generic;

namespace Poz1.UpdatingNutrition.Models
{
    public class Diet
    {
        //Day, FoodID in day, food
        public Dictionary<int, Dictionary<int, Food>> Items { get; set; } 
        public Diet()
        {
            Items = new Dictionary<int, Dictionary<int, Food>>();
        }
    }

    public static class DietFactory{
        public static Diet Defautl(){
            var diet =  new Diet();
            //diet.Items.Add(0, 0, )
            return diet;
        }
    }
}
