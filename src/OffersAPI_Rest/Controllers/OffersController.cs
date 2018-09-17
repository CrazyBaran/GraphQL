using System;
using System.Collections.Generic;

namespace OffersAPI_Rest.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using OffersAPI_Rest.Commands;
    using OffersAPI_Rest.Constants;
    using OffersAPI_Rest.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Swashbuckle.AspNetCore.Annotations;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OffersController : ControllerBase
    {
        /// <summary>
        /// Gets the offer with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="offerId">The offers unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the offer or a 404 Not Found if a offer with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{offerId}", Name = OffersControllerRoute.GetOffer)]
        [SwaggerResponse(StatusCodes.Status200OK, "The offer with the specified unique identifier.", typeof(ViewModels.Offer))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A offer with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetOfferCommand command,
            Guid offerId,
            CancellationToken cancellationToken) => command.ExecuteAsync(offerId);
        
        /// <summary>
        /// Gets all offers.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing list of offers or empty list.</returns>
        [HttpGet("", Name = OffersControllerRoute.GetOffers)]
        [SwaggerResponse(StatusCodes.Status200OK, "The offer with the specified unique identifier.", typeof(IEnumerable<ViewModels.Offer>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A offer with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetOffersCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync();


        /// <summary>
        /// Creates a new offer.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="offer">The offer to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created offer or a 400 Bad Request if the offer is
        /// invalid.</returns>
        [HttpPost("", Name = OffersControllerRoute.PostOffer)]
        [SwaggerResponse(StatusCodes.Status201Created, "The offer was created.", typeof(ViewModels.Offer))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The offer is invalid.", typeof(ModelStateDictionary))]
        public Task<IActionResult> Post(
            [FromServices] IPostOfferCommand command,
            [FromBody] SaveOffer offer,
            CancellationToken cancellationToken) => command.ExecuteAsync(offer);


        /// <summary>
        /// Creates a new application for offer.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="candidateId">Candidate id</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <param name="offerId">Offer id</param>
        /// <returns>A 201 Created response containing the newly created offer or a 400 Bad Request if the offer is
        /// invalid.</returns>
        [HttpPost("{offerId}/candidates/{candidateId}", Name = OffersControllerRoute.PostOfferApplication)]
        [SwaggerResponse(StatusCodes.Status201Created, "The application was created.", typeof(ViewModels.SaveOfferApplication))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The application is invalid.", typeof(ModelStateDictionary))]
        public Task<IActionResult> PostApplication(
            [FromServices] IPostOfferApplicationCommand command,
            Guid offerId,
            int candidateId,
            CancellationToken cancellationToken) => command.ExecuteAsync(new SaveOfferApplication() { CandidateId = candidateId, OfferId = offerId });

    }
}