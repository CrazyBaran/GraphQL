using Datasource;

namespace OffersAPI_Rest.Mappers
{
    using System;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class OfferApplicationToOfferApplicationMapper : IMapper<OfferApplicationData, OfferApplicationData>
    {
        public void Map(OfferApplicationData source, OfferApplicationData destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.OfferId = source.OfferId;
            destination.CandidateId = source.CandidateId;
        }
    }
}
