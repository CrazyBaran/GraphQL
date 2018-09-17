using Datasource;
using Datasource.Repositories;

namespace OffersAPI_Rest.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using OffersAPI_Rest.Constants;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class PostOfferApplicationCommand : IPostOfferApplicationCommand
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper<Datasource.OfferApplicationData, OfferApplicationData> offerToOfferMapper;
        private readonly IMapper<SaveOfferApplication, Datasource.OfferApplicationData> saveOfferToOfferMapper;

        public PostOfferApplicationCommand(
            IOfferRepository offerRepository,
            IMapper<Datasource.OfferApplicationData, OfferApplicationData> offerToOfferMapper,
            IMapper<SaveOfferApplication, Datasource.OfferApplicationData> saveOfferToOfferMapper)
        {
            this._offerRepository = offerRepository;
            this.offerToOfferMapper = offerToOfferMapper;
            this.saveOfferToOfferMapper = saveOfferToOfferMapper;
        }
        
        public async Task<IActionResult> ExecuteAsync(SaveOfferApplication parameter, CancellationToken cancellationToken = new CancellationToken())
        {
            var offer = this.saveOfferToOfferMapper.Map(parameter);
            offer = await this._offerRepository.UpdateApplications(offer, cancellationToken);
            var offerViewModel = this.offerToOfferMapper.Map(offer);

            return new CreatedAtRouteResult(
                OffersControllerRoute.GetOffer,
                new { offerId = offerViewModel.OfferId },
                offerViewModel);
        }
    }
}
