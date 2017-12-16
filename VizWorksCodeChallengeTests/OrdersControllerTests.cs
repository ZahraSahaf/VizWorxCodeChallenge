using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VizWorxCodeChallenge.Controllers;
using VizWorxCodeChallenge.Models;

namespace VizWorxCodeChallengeTests
{
    [TestClass]
    public class OrdersControllerTests
    {
        [TestMethod]
        public void CalculateBestService_WithValidResults_ShouldReturnResults()
        {
            // Arrange
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>() {
                {MealType.Others, 40 },
                {MealType.Vegetarian, 5},
                {MealType.GlutenFree, 2}
            };

            // A sample of valid result
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
        
            bool isGetBestServicesCalled = false;
            bool isOrderRemainingCalled = false;
            VizWorxCodeChallenge.Services.Fakes.StubIAnalysisService stubIAnalysisService = new VizWorxCodeChallenge.Services.Fakes.StubIAnalysisService()
            {
                GetBestServiceDictionaryOfMealTypeInt32ListOfRestaurant = (restaurants, order) =>
                {
                    isGetBestServicesCalled = true;
                    return expectedResult;
                },
                IsOrderRemainingDictionaryOfMealTypeInt32 = (order) =>
                {
                    isOrderRemainingCalled = true;
                    return false;
                }
               
            };
            
            OrdersController controller = new OrdersController(stubIAnalysisService);

            // Act
            var actionResult = controller.CalculateBestService(orders);

            Assert.IsTrue(isGetBestServicesCalled);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<Result>>));
            Assert.IsTrue(isOrderRemainingCalled);
        }

        [TestMethod]
        public void CalculateBestService_WithEmptyResults_ShouldReturnBadRequest()
        {
            // Arrange
            Dictionary<MealType, int> orders = new Dictionary<MealType, int>()
            {
                {MealType.Others, 40 },
                {MealType.Vegetarian, 5},
                {MealType.GlutenFree, 2}
            };
            bool isGetBestServicesCalled = false;
            VizWorxCodeChallenge.Services.Fakes.StubIAnalysisService stubIAnalysisService = new VizWorxCodeChallenge.Services.Fakes.StubIAnalysisService()
            {
                GetBestServiceDictionaryOfMealTypeInt32ListOfRestaurant = (restaurants, order) =>
                {
                    isGetBestServicesCalled = true;
                    return new List<Result>();
                }
            };
            OrdersController controller = new OrdersController(stubIAnalysisService);

            // Act
            var actionResult = controller.CalculateBestService(orders);

            Assert.IsTrue(isGetBestServicesCalled);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("No Solution found.", ((BadRequestErrorMessageResult)actionResult).Message);

        }

    }
}
