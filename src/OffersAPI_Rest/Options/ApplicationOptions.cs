namespace OffersAPI_Rest.Options
{
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {

        public KestrelServerOptions Kestrel { get; set; }
    }
}
