namespace OffersAPI_Rest
{
    using System;
    using OffersAPI_Rest.Constants;
    using Boxed.AspNetCore;
    using CorrelationId;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) =>
            services
                .AddCorrelationIdFluent()
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
                .AddCustomSwagger()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddCustomApiVersioning()
                .AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddApiExplorer()
                    .AddAuthorization()
                    .AddDataAnnotations()
                    .AddJsonFormatters()
                    .AddCustomJsonOptions(this.hostingEnvironment)
                    .AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV") // Version format: 'v'major[.minor][-status]
                    .AddCustomMvcOptions(this.hostingEnvironment)
                .Services
                .AddProjectCommands()
                .AddProjectMappers()
                .AddProjectRepositories()
                .BuildServiceProvider();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        public void Configure(IApplicationBuilder application) =>
            application
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperErrorPages())
                .UseMvc()
                .UseSwagger()
                .UseCustomSwaggerUI();
    }
}