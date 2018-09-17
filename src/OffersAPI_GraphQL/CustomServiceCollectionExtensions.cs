namespace OffersAPI_GraphQL
{
    using CorrelationId;
    using GraphQL;
    using GraphQL.Server;
    using OffersAPI_GraphQL.Options;
    using Microsoft.AspNetCore.Hosting;
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

        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .Configure<ApplicationOptions>(configuration)
                .AddSingleton(x => x.GetRequiredService<IOptions<ApplicationOptions>>().Value)
                .Configure<GraphQLOptions>(configuration.GetSection(nameof(ApplicationOptions.GraphQL)));

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

    
        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services, IHostingEnvironment hostingEnvironment) =>
            services
                // Add a way for GraphQL.NET to resolve types.
                .AddSingleton<IDependencyResolver, GraphQLDependencyResolver>()
                .AddGraphQL(
                    options =>
                    {
                        // Set some limits for security, read from configuration.
                        options.ComplexityConfiguration = services
                            .BuildServiceProvider()
                            .GetRequiredService<IOptions<GraphQLOptions>>()
                            .Value
                            .ComplexityConfiguration;
                        // Show stack traces in exceptions. Don't turn this on in production.
                        options.ExposeExceptions = hostingEnvironment.IsDevelopment();
                    })
                // Adds all graph types in the current assembly with a singleton lifetime.
                .AddGraphTypes()
                // Adds ConnectionType<T>, EdgeType<T> and PageInfoType.
                .AddRelayGraphTypes()
                // Add GraphQL data loader to reduce the number of calls to our repository.
                .AddDataLoader()
                // Add WebSockets support for subscriptions.
                .AddWebSockets()
                .Services;

    }
}
