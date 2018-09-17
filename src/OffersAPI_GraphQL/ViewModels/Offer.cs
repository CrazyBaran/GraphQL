using System;
using Datasource;

namespace OffersAPI_GraphQL.ViewModels
{
    public class Offer
    {
        public Guid OfferId { get; set; }
        public string JobTitle { get; set; }
        public GeoLocation Location { get; set; }
        public Employer Employer { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public Salary Salary { get; set; }
        public int ApplicationsNumber { get; set; }

        public Offer() {}

        public Offer(OfferData offerData)
        {
            OfferId = offerData.Id;
            JobTitle = offerData.JobTitle;
            Employer = new Employer() { Name = offerData.EmployerName, Address = new GeoLocation()
                { CountryName = offerData.EmployerAddressCountry, CityName = offerData.EmployerAddressCity } };
            CreationDateUtc = offerData.CreationDateUtc;
            ExpirationDateUtc = offerData.ExpirationDateUtc;
            Location = new GeoLocation() { CityName = offerData.LocationCity, CountryName = offerData.LocationCountry };
            Salary = new Salary() { Range = offerData.Salary };
            ApplicationsNumber = offerData.CandidateIds.Count;
        }
    }
}