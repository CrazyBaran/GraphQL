using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Datasource.Repositories
{
    public interface IOfferRepository
    {
        Task<OfferData> AddOffer(OfferData offerData, CancellationToken cancellationToken);
        Task<int> GetApplicationsNumber(Guid id, CancellationToken cancellationToken);
        Task<OfferData> GetOffer(Guid id, CancellationToken cancellationToken);
        Task<List<OfferData>> GetOffers(CancellationToken cancellationToken);
        Task<OfferData> Update(OfferData offerData, CancellationToken cancellationToken);
        Task<OfferApplicationData> UpdateApplications(OfferApplicationData offer, CancellationToken cancellationToken);
    }
}