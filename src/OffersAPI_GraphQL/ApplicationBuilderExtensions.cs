namespace OffersAPI_GraphQL
{
    using Microsoft.AspNetCore.Builder;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                .UseDeveloperExceptionPage();
    
    }
}
