using System;
using System.Collections.Generic;

namespace Poz1.UpdatingNutrition.Models
{
    public class WOD 
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string TrainerName { get; set; }
        public List<Exercise> Exercises { get; set; } 
        public int Calories { get; set; }

        public WOD()
        {

        }
    }
}
