using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VizWorxCodeChallenge.Models;
using VizWorxCodeChallenge.Services;

namespace VizWorxCodeChallenge.Controllers
{
    [RoutePrefix("api/service")]
    public class OrdersController : ApiController
    {
        private readonly IAnalysisService analysisService;

        public OrdersController(IAnalysisService analysisService)
        {
            this.analysisService = analysisService;
        }

        [HttpPost]
        [Route("bests")]
        public IHttpActionResult CalculateBestService([FromBody] Dictionary<MealType, int> orders)
        {
            var restaurants = analysisService.GetRestaurants();
            var results = analysisService.GetBestService(orders, restaurants);
            bool isOrdersRemained = analysisService.IsOrderRemaining(orders);
            if (results.Count == 0 || isOrdersRemained)
            {
                return BadRequest("No Solution found.");
            }

            return Ok(results);
        }
    }
}