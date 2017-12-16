using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizWorxCodeChallenge.Models;

namespace VizWorxCodeChallenge.Services
{
    public interface IAnalysisService
    {
        List<Result> GetBestService(Dictionary<MealType, int> orders, List<Restaurant> restaurants);
        List<Restaurant> GetRestaurants();
        bool IsOrderRemaining(Dictionary<MealType, int> orders);
    }
}
