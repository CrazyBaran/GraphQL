using System.Collections.Generic;
using Datasource.Repositories;
using Microsoft.EntityFrameworkCore.Internal;

namespace OffersAPI_Rest.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class GetOffersCommand : IGetOffersCommand
    {
        private readonly IOfferRepository offerRepository;
        private readonly IMapper<Datasource.OfferData, Offer> offerMapper;

        public GetOffersCommand(
            IOfferRepository offerRepository,
            IMapper<Datasource.OfferData, Offer> offerMapper)
        {
            this.offerRepository = offerRepository;
            this.offerMapper = offerMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var offers = await this.offerRepository.GetOffers(cancellationToken);
            if (offers == null || !offers.Any())
            {
                return new OkObjectResult(new List<Offer>());
            }

            var offersViewModel = offers?.ConvertAll(o => offerMapper.Map(o));
            return new OkObjectResult(offersViewModel);
        }
    }
}
