namespace OffersAPI_GraphQL.Options
{
    using GraphQL.Server;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        public GraphQLOptions GraphQL { get; set; }

        public KestrelServerOptions Kestrel { get; set; }
    }
}
