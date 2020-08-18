using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace ODataNetCorePOC.Controllers
    {
    [ApiVersion("1.0")]
    [ODataRoutePrefix("WeatherForecast")]
    public class WeatherForecastController : ODataController
        {
        private Container container;
         
        public WeatherForecastController ()
            {
            var cosmosClient = new CosmosClient("<enterconnectionstring>");
            var db = cosmosClient.CreateDatabaseIfNotExistsAsync("weather").Result.Database;
            container = db.CreateContainerIfNotExistsAsync("weather", "/id").Result.Container;
            }

        [EnableQuery]
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get ()
            {
            return container.GetItemLinqQueryable<WeatherForecast>(true);
            }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] WeatherForecast weather)
            {
            await container.CreateItemAsync(weather);
            return Created(weather);
            }
        }
    }
