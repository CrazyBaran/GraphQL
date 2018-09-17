namespace OffersAPI_Rest
{
    using System.Linq;
    using OffersAPI_Rest.Constants;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class MvcCoreBuilderExtensions
    {

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcCoreBuilder AddCustomJsonOptions(
            this IMvcCoreBuilder builder,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddJsonOptions(
                options =>
                {
                    if (hostingEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    }

                    // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
                    // DateTime everywhere. Not doing so can cause problems with time-zones.
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

                    // Output enumeration values as strings in JSON.
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

        public static IMvcCoreBuilder AddCustomMvcOptions(
            this IMvcCoreBuilder builder,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var jsonInputFormatterMediaTypes = options
                        .InputFormatters
                        .OfType<JsonInputFormatter>()
                        .First()
                        .SupportedMediaTypes;

                    var jsonOutputFormatterMediaTypes = options
                        .OutputFormatters
                        .OfType<JsonOutputFormatter>()
                        .First()
                        .SupportedMediaTypes;

                    // Add RESTful JSON media type (application/vnd.restful+json) to the JSON input and output formatters.
                    // See http://restfuljson.org/
                    jsonInputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);
                    jsonOutputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });
    }
}
