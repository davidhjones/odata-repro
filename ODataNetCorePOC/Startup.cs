using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;

namespace ODataNetCorePOC
    {
    public class Startup
        {
        public Startup (IConfiguration configuration)
            {
            Configuration = configuration;
            }

        public IConfiguration Configuration
            {
            get;
            }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
            {
            services.AddMvcCore(option => option.EnableEndpointRouting = false);
            services.AddApiVersioning();
            services.AddOData().EnableApiVersioning();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, VersionedODataModelBuilder modelBuilder)
            {
            if ( env.IsDevelopment() )
                {
                app.UseDeveloperExceptionPage();
                }

            app.UseHttpsRedirection();

            app.UseRouting();

            var models = modelBuilder.GetEdmModels();
            app.UseMvc(routes => {
                routes.Select().Filter().OrderBy().Count();

                routes.MapVersionedODataRoutes("odata", "odata", models);
                });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.Select().Expand().OrderBy().Filter().MaxTop(100).Count();
            //    endpoints.MapODataRoute("odata", "odata", builder.GetEdmModel());
            //});
            }
        }
    }
