using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VizWorxCodeChallenge.Models;
using VizWorxCodeChallenge.Services;
using VizWorxCodeChallengeTests;

namespace VizWorksCodeChallengeTests
{
    [TestClass]
    public class AnalysisServiceTests
    {
        [TestMethod]
        public void GetBestService_WithEmptyRestaurants_ShouldReturnEmptyResults()
        {
            // Arrange 
            List<Restaurant> restaurants = new List<Restaurant>();
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>()
            {
                {MealType.Others, 10},
                {MealType.Vegetarian, 5}
            };

            // Act
            AnalysisService analysisService = new AnalysisService();
            List<Result> results = analysisService.GetBestService(orders, restaurants);

            // Assert
            Assert.IsTrue(results.Count == 0, "Results should have been empty.");
        }

        [TestMethod]
        public void GetBestService_WithNullRestaurants_ShouldReturnEmptyResults()
        {
            // Arrange 
            List<Restaurant> restaurants = null;
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>()
            {
                {MealType.Others, 10},
                {MealType.Vegetarian, 5}
            };

            // Act
            AnalysisService analysisService = new AnalysisService();
            List<Result> results = analysisService.GetBestService(orders, restaurants);

            // Assert
            Assert.IsTrue(results.Count == 0, "Results should have been empty.");
        }

        [TestMethod]
        public void GetBestService_WithEmptyOrders_ShouldReturnEmptyResults()
        {
            // Arrange 
            AnalysisService analysisService = new AnalysisService();
            List<Restaurant> restaurants = analysisService.GetRestaurants();
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>();

            // Act
            List<Result> results = analysisService.GetBestService(orders, restaurants);

            // Assert
            Assert.IsTrue(results.Count == 0, "Results should have been empty.");
        }

        [TestMethod]
        public void GetBestService_WithNullOrders_ShouldReturnEmptyResults()
        {
            // Arrange 
            AnalysisService analysisService = new AnalysisService();
            List<Restaurant> restaurants = analysisService.GetRestaurants();
            Dictionary<MealType, int> orders = null;

            // Act
            List<Result> results = analysisService.GetBestService(orders, restaurants);

            // Assert
            Assert.IsTrue(results.Count == 0, "Results should have been empty.");
        }

        [TestMethod]
        public void GetBestService_WithValidOrdersAndRestaurants_ShouldReturnResults()
        {
            // Arrange 
            AnalysisService analysisService = new AnalysisService();
            List<Restaurant> restaurants = analysisService.GetRestaurants();
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>()
            {
                {MealType.Others, 40 },
                {MealType.Vegetarian, 5},
                {MealType.GlutenFree, 2}
            };
            List<Result> expectedResult = new List<Result>()
            {
                new Result(){
                    Restaurant = new Restaurant(){Name="Restaurant A"},
                    ProvidedFoods = new Dictionary<MealType, int>()
                    {
                        {MealType.Others, 36 },
                        {MealType.Vegetarian, 4 },
                    }
                },
                new Result(){
                    Restaurant = new Restaurant(){Name="Restaurant B"},
                    ProvidedFoods = new Dictionary<MealType, int>()
                    {
                        {MealType.Others, 4 },
                        {MealType.Vegetarian, 1 },
                        {MealType.GlutenFree, 2 }
                    }
                }
            };

            // Act
            List<Result> results = analysisService.GetBestService(orders, restaurants);

            // Assert
            Assert.AreEqual(expectedResult.Count, results.Count, String.Format("Length of results should be {0}", expectedResult.Count));
            Assert.AreEqual(expectedResult[0].Restaurant.Name, results[0].Restaurant.Name, String.Format("Name of first restaurant should be {0}", expectedResult[0].Restaurant.Name));
            Assert.AreEqual(expectedResult[1].Restaurant.Name, results[1].Restaurant.Name, String.Format("Name of second restaurant should be {0}", expectedResult[1].Restaurant.Name));
            DictionaryComparer<MealType, int> comparer = new DictionaryComparer<MealType, int>();
            Assert.IsTrue(comparer.Equals(expectedResult[0].ProvidedFoods, results[0].ProvidedFoods), "Results should have been the same");
            Assert.IsTrue(comparer.Equals(expectedResult[1].ProvidedFoods, results[1].ProvidedFoods), "Results should have been the same");
        }

        [TestMethod]
        public void IsOrderRemaining_WithEmptyOrders_ShouldReturnFalse()
        {
            // Arrange
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>();

            // Act
            AnalysisService analysisService = new AnalysisService();
            bool result = analysisService.IsOrderRemaining(orders);

            // Assert
            Assert.IsFalse(result, "Result should have been false.");
        }

        [TestMethod]
        public void IsOrderRemaining_WithRemainedOrders_ShouldReturnFalse()
        {
            // Arrange
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>()
            {
                {MealType.Others, 1}
            };

            // Act
            AnalysisService analysisService = new AnalysisService();
            bool result = analysisService.IsOrderRemaining(orders);

            // Assert
            Assert.IsTrue(result, "Result should have been true.");
        }

    }
}
