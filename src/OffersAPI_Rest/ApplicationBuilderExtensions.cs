namespace OffersAPI_Rest
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                .UseDeveloperExceptionPage();


        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application) =>
            application.UseSwaggerUI(
                options =>
                {
                    // Set the Swagger UI browser document title.
                    options.DocumentTitle = typeof(Startup)
                        .Assembly
                        .GetCustomAttribute<AssemblyProductAttribute>()
                        .Product;
                    // Set the Swagger UI to render at '/'.
                    options.RoutePrefix = string.Empty;
                    // Show the request duration in Swagger UI.
                    options.DisplayRequestDuration();

                    var provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider
                        .ApiVersionDescriptions
                        .OrderByDescending(x => x.ApiVersion))
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}");
                    }
                });
    }
}
