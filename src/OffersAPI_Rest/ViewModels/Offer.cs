using System;

namespace OffersAPI_Rest.ViewModels
{
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Offer model.
    /// </summary>
    public class Offer
    {
        public Guid OfferId { get; set; }
        public string JobTitle { get; set; }
        public GeoLocation Location { get; set; }
        public Employer Employer { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public Salary Salary { get; set; }
    }
}
