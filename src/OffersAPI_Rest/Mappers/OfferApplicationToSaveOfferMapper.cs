namespace OffersAPI_Rest.Mappers
{
    using System;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;

    public class OfferToSaveOfferApplicationMapper : IMapper<Datasource.OfferApplicationData, SaveOfferApplication>, IMapper<SaveOfferApplication, Datasource.OfferApplicationData>
    {
        public void Map(Datasource.OfferApplicationData source, SaveOfferApplication destination)
        {
            throw new NotImplementedException("Empty on purpose");
        }

        public void Map(SaveOfferApplication source, Datasource.OfferApplicationData destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.CandidateId = source.CandidateId;
            destination.OfferId = source.OfferId;
        }
    }
}