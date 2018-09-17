using System;

namespace OffersAPI_Rest.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Offer application model.
    /// </summary>
    public class SaveOfferApplication
    {
        [Required]
        public Guid OfferId { get; set; }
        [Required]
        public int CandidateId { get; set; }
    }
}
