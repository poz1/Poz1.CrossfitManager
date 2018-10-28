using System;
namespace Poz1.UpdatingNutrition.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public bool IsCoach { get; set; }

        public BloodType BType { get; set; }
        public float IMC { get; set; }

        public Diet Diet { get; set; }

        public User()
        {
        }
    }
}
