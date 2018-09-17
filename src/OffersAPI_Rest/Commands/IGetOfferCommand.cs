using System;

namespace OffersAPI_Rest.Commands
{
    using Boxed.AspNetCore;

    public interface IGetOfferCommand : IAsyncCommand<Guid>
    {
    }
}
