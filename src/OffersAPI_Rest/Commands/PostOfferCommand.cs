using Datasource.Repositories;

namespace OffersAPI_Rest.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using OffersAPI_Rest.Constants;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class PostOfferCommand : IPostOfferCommand
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper<Datasource.OfferData, Offer> _offerToOfferMapper;
        private readonly IMapper<SaveOffer, Datasource.OfferData> _saveOfferToOfferMapper;

        public PostOfferCommand(
            IOfferRepository offerRepository,
            IMapper<Datasource.OfferData, Offer> offerToOfferMapper,
            IMapper<SaveOffer, Datasource.OfferData> saveOfferToOfferMapper)
        {
            this._offerRepository = offerRepository;
            this._offerToOfferMapper = offerToOfferMapper;
            this._saveOfferToOfferMapper = saveOfferToOfferMapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveOffer saveOffer, CancellationToken cancellationToken)
        {
            var offer = this._saveOfferToOfferMapper.Map(saveOffer);
            offer = await this._offerRepository.Update(offer, cancellationToken);
            var offerViewModel = this._offerToOfferMapper.Map(offer);

            return new CreatedAtRouteResult(
                OffersControllerRoute.GetOffer,
                new { offerId = offerViewModel.OfferId },
                offerViewModel);
        }
    }
}
