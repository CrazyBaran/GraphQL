using System.Collections.Generic;

namespace OffersAPI_Rest.Mappers
{
    using System;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;

    public class OfferDataToSaveOfferMapper : IMapper<SaveOffer, Datasource.OfferData>
    {
        public void Map(SaveOffer source, Datasource.OfferData destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (destination.CreationDateUtc == DateTime.MinValue)
            {
                destination.CreationDateUtc = DateTime.UtcNow;
                destination.CandidateIds = new List<int>();
            }
            
            destination.JobTitle = source.JobTitle;
            destination.EmployerName = source.Employer.Name;
            destination.EmployerAddressCity = source.Employer.Address.CityName;
            destination.EmployerAddressCountry = source.Employer.Address.CountryName;
            destination.LocationCity = source.Location.CityName;
            destination.LocationCountry = source.Location.CountryName;
            destination.ExpirationDateUtc = source.ExpirationDateUtc;
            destination.Salary = source.Salary.Range;
        }
    }
}
