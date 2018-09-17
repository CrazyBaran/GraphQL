
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace OffersAPI_Rest
{
    using OffersAPI_Rest.Options;
    using CorrelationId;
    using Boxed.AspNetCore.Swagger;
    using Boxed.AspNetCore.Swagger.OperationFilters;
    using Boxed.AspNetCore.Swagger.SchemaFilters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdFluent(this IServiceCollection services)
        {
            services.AddCorrelationId();
            return services;
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .Configure<ApplicationOptions>(configuration)
                .AddSingleton(x => x.GetRequiredService<IOptions<ApplicationOptions>>().Value);


        /// <summary>
        /// Add custom routing settings which determines how URL's are generated.
        /// </summary>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
            services.AddRouting(
                options =>
                {
                    // All generated URL's should be lower-case.
                    options.LowercaseUrls = true;
                });


        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllEnumsAsStrings();
                    options.DescribeAllParametersInCamelCase();
                    options.DescribeStringEnumsInCamelCase();
                    options.EnableAnnotations();

                    options.IncludeXmlCommentsIfExists(assembly);

                    options.OperationFilter<ForbiddenResponseOperationFilter>();
                    options.OperationFilter<UnauthorizedResponseOperationFilter>();

                    options.SchemaFilter<JsonPatchDocumentSchemaFilter>();
                    options.SchemaFilter<ModelStateDictionarySchemaFilter>();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new Info()
                        {
                            Title = assemblyProduct,
                            Description = apiVersionDescription.IsDeprecated ?
                                $"{assemblyDescription} This API version has been deprecated." :
                                assemblyDescription,
                            Version = apiVersionDescription.ApiVersion.ToString(),
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }

                });

    }
}
