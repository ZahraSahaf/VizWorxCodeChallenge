using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VizWorxCodeChallenge.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MealType OrderedMeal { get; set; }
    }
}