using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace ODataNetCorePOC
    {
    public class WeatherForecastModelConfiguration : IModelConfiguration
        {
        public void Apply (ODataModelBuilder builder, ApiVersion apiVersion)
            {
            builder.EntitySet<WeatherForecast>("WeatherForecast");
            }
        }
    }
