using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VizWorxCodeChallenge.Models
{
    public class Result
    {
        public Restaurant Restaurant { get; set; }
        public Dictionary<MealType, int> ProvidedFoods { get; set; }
    }
}