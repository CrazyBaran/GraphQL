using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Datasource.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private List<OfferData> Offers { get; }

        public OfferRepository()
        {
            Offers = new List<OfferData>()
            {
                new OfferData()
                {
                    Id = new Guid("7f7bf389-2cfb-45f4-b91e-9d95441c1ecc"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-8),
                    EmployerName = "IT Company",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(10),
                    JobTitle = "Software Developer",
                    LocationCity = "Warsaw",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {1, 2, 3, 4}
                },
                new OfferData()
                {
                    Id = new Guid("136f16ce-cdd7-42d1-86f0-eb06be83a6ba"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-7),
                    EmployerName = "Sales Company",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(15),
                    JobTitle = "Account Manager",
                    LocationCity = "Cracov",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {1, 3, 4}
                },
                new OfferData()
                {
                    Id = new Guid("87873bd2-7508-4175-a9c5-1d9097899e3b"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-7),
                    EmployerName = "Software Company",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(20),
                    JobTitle = ".NET Software Engineer",
                    LocationCity = "Poznan",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {4}

                },
                new OfferData()
                {
                    Id = new Guid("44aa1554-4ba6-43d6-a078-f6151e93894e"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-5),
                    EmployerName = " Pretty Software Company",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(2),
                    JobTitle = "Frontend Developer",
                    LocationCity = "Opole",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {1, 2, 3, 4, 5, 7}
                },
                new OfferData()
                {
                    Id = new Guid("1aa3638d-e41e-48a1-a8b5-772cd3df157f"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-4),
                    EmployerName = "Krzak Ltd.",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(20),
                    JobTitle = "IT Support",
                    LocationCity = "Radom",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {7}
                },
                new OfferData()
                {
                    Id = new Guid("62a74f8a-b0d6-4a50-8b01-43e2fd6ec499"),
                    CreationDateUtc = DateTime.UtcNow.AddDays(-4),
                    EmployerName = "Software Company",
                    EmployerAddressCity = "Warsaw",
                    EmployerAddressCountry = "Poland",
                    ExpirationDateUtc = DateTime.UtcNow.AddDays(5),
                    JobTitle = "C# Junior Software Developer",
                    LocationCity = "Warsaw",
                    LocationCountry = "Poland",
                    Salary = "10k -12k PLN",
                    CandidateIds = new List<int>() {1, 2, 3, 4, 5, 7, 10, 15}
                }
            };

        }

        public Task<OfferData> AddOffer(OfferData offerData, CancellationToken cancellationToken)
        {
            offerData.Id = Guid.NewGuid();
            Offers.Add(offerData);
            return Task.FromResult(offerData);
        }

        public Task<int> GetApplicationsNumber(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(Offers.FirstOrDefault(x => x.Id == id).CandidateIds.Count);

        public Task<OfferData> GetOffer(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(Offers.FirstOrDefault(x => x.Id == id));

        public Task<List<OfferData>> GetOffers(CancellationToken cancellationToken) =>
            Task.FromResult(Offers);

        public Task<OfferData> Update(OfferData offerData, CancellationToken cancellationToken)
        {
            var existingOffer = Offers.FirstOrDefault(x => x.Id == offerData.Id);

            if (existingOffer == null)
            {
                return AddOffer(offerData, cancellationToken);
            }

            existingOffer.JobTitle = offerData.JobTitle;
            existingOffer.EmployerName = offerData.EmployerName;
            existingOffer.EmployerAddressCity = offerData.EmployerAddressCity;
            existingOffer.EmployerAddressCountry = offerData.EmployerAddressCountry;
            existingOffer.ExpirationDateUtc = offerData.ExpirationDateUtc;
            existingOffer.LocationCity = offerData.LocationCity;
            existingOffer.LocationCountry = offerData.LocationCountry;
            existingOffer.CandidateIds = offerData.CandidateIds;
            return Task.FromResult(offerData);
        }

        public Task<OfferApplicationData> UpdateApplications(OfferApplicationData offer, CancellationToken cancellationToken)
        {
            var existingOffer = Offers.FirstOrDefault(x => x.Id == offer.OfferId);
            existingOffer.CandidateIds.Add(offer.CandidateId);
            return Task.FromResult(offer);
        }
    }
}