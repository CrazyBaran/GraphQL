using Datasource.Repositories;

namespace OffersAPI_Rest.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class GetOfferCommand : IGetOfferCommand
    {
        private readonly IOfferRepository offerRepository;
        private readonly IMapper<Datasource.OfferData, Offer> offerMapper;

        public GetOfferCommand(
            IOfferRepository offerRepository,
            IMapper<Datasource.OfferData, Offer> offerMapper)
        {
            this.offerRepository = offerRepository;
            this.offerMapper = offerMapper;
        }

        public async Task<IActionResult> ExecuteAsync(Guid offerId, CancellationToken cancellationToken)
        {
            var offer = await this.offerRepository.GetOffer(offerId, cancellationToken);
            if (offer == null)
            {
                return new NotFoundResult();
            }

            var offerViewModel = this.offerMapper.Map(offer);
            return new OkObjectResult(offerViewModel);
        }
    }
}
