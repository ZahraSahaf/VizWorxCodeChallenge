using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VizWorxCodeChallenge.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public Dictionary<MealType, int> AvailableMeals { get; set; }
    }

    public enum MealType
    {
        Others = 1,
        Vegetarian = 2,
        Gluten​Free = 3,
        Nut​Free = 4,
        Fish​Free = 5,
    }
}