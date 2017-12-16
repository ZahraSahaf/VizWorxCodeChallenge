using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VizWorxCodeChallenge.Models;

namespace VizWorxCodeChallenge.Services
{
    public class AnalysisService : IAnalysisService
    {
        /// <summary>
        /// This method returns the best restaurant services according to the input orders and their avaiable capacity.
        /// </summary>
        /// <param name="orders">List of orders.</param>
        /// <param name="restaurants">List of restaurants</param>
        /// <returns>Result of how orders are provided by restaurants.</returns>
        public List<Result> GetBestService(Dictionary<MealType, int> orders, List<Restaurant> restaurants)
        {
            var results = new List<Result>();

            if (orders == null || restaurants == null)
            {
                return results;
            }
            
            // sorts restuarants by rate
            var orderedRestaurants = restaurants.OrderByDescending(res => res.Rate).AsQueryable();
            MealType[] orderedMealTypes = new MealType[orders.Count];
            orders.Keys.CopyTo(orderedMealTypes, 0);
            // chooses restaurants by rate ascending
            foreach (var restaurant in orderedRestaurants)
            {
                var result = new Result();
                result.Restaurant = restaurant;
                var meals = new Dictionary<MealType, int>();

                foreach (var mealType in orderedMealTypes)
                {
                    int requestedMeals = orders[mealType];
                    int availableMeals;
                    restaurant.AvailableMeals.TryGetValue(mealType, out availableMeals);

                    if (availableMeals == 0)
                    {
                        continue;
                    }
                    if (availableMeals < requestedMeals)
                    {
                        int providedMeals = requestedMeals - availableMeals;
                        meals.Add(mealType, availableMeals);
                        orders[mealType] = providedMeals;
                        restaurant.AvailableMeals[mealType] = 0;
                    }
                    else
                    {
                        meals.Add(mealType, requestedMeals);
                        orders[mealType] = 0;
                        restaurant.AvailableMeals[mealType] = availableMeals - requestedMeals;
                    }

                }

                if (meals.Count != 0)
                {
                    result.ProvidedFoods = meals;
                    results.Add(result);
                }
            }

            return results;
        }

        /// <summary>
        /// This is a fixed predefined collection of restaurants.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        public List<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = 1,
                    Name = "Restaurant A",
                    Rate = 5, // 5/5
                    AvailableMeals = new Dictionary<MealType, int>()
                    {
                        { MealType.Others,36 },
                        {MealType.Vegetarian,4 }
                    }
                },
                new Restaurant()
                {
                    Id = 2,
                    Name = "Restaurant B",
                    Rate = 3, // 3/5
                    AvailableMeals = new Dictionary<MealType, int>()
                    {
                        { MealType.Others,60 },
                        {MealType.Vegetarian,20 },
                        {MealType.GlutenFree,20 }
                    }
                }
            };
        }

        /// <summary>
        /// This method returns if any order is remained and can not be provided by any of the restaurants.
        /// </summary>
        /// <param name="orders">List of orders.</param>
        /// <returns>Whether any order remained or not.</returns>
        public bool IsOrderRemaining(Dictionary<MealType, int> orders)
        {
            if (orders == null)
                return false;

            return orders.Where(order => order.Value > 0).Count() > 0;
        }
    }
}