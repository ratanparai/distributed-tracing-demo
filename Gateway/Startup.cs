using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.AccountInfo;
using Gateway.AddressInfo;
using Gateway.ResultService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddHttpClient<IAccountInfoClient, AccountInfoClient>(client =>
            {
                client.BaseAddress = new Uri("http://accountinfoservice");
            });

            services.AddHttpClient<IAddressServiceClient, AddressServiceClient>(client =>
            {
                client.BaseAddress = new Uri("http://addressservice");
            });

            services.AddHttpClient<IResultServiceClient, ResultServiceClient>(client =>
            {
                client.BaseAddress = new Uri("http://resultservice");
            });

            services.AddOpenTelemetryTracing(configure =>
            {
                configure.SetSampler(new AlwaysOnSampler());
                configure.AddAspNetCoreInstrumentation();
                configure.AddHttpClientInstrumentation();
                configure.AddJaegerExporter(config =>
                {
                    config.AgentHost = "jaeger";
                });

                configure.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService("Gateway"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
