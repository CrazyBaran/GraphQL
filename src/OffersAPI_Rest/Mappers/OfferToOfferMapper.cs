namespace OffersAPI_Rest.Mappers
{
    using System;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;

    public class OfferDataToOfferMapper : IMapper<Datasource.OfferData, Offer>
    {
        public void Map(Datasource.OfferData source, Offer destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.OfferId = source.Id;
            destination.Employer = new Employer()
            {
                Name = source.EmployerName,
                Address = new GeoLocation() { CityName = source.EmployerAddressCity, CountryName = source.EmployerAddressCountry }
            };
            destination.JobTitle = source.JobTitle;
            destination.Location = new GeoLocation() { CityName = source.LocationCity, CountryName = source.LocationCountry };
            destination.ExpirationDateUtc = source.ExpirationDateUtc;
            destination.Salary = new Salary() { Range = source.Salary };
        }
    }
}