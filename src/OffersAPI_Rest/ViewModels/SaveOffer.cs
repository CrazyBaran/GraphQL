using System;

namespace OffersAPI_Rest.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Offer model.
    /// </summary>
    public class SaveOffer
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public Employer Employer { get; set; }
        [Required]
        public GeoLocation Location { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public Salary Salary { get; set; }
    }
}
