namespace OffersAPI_Rest.Commands
{
    using OffersAPI_Rest.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostOfferCommand : IAsyncCommand<SaveOffer>
    {
    }
}
