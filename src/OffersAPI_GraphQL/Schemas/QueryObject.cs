using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Datasource.Repositories;
using OffersAPI_GraphQL.ViewModels;

namespace OffersAPI_GraphQL.Schemas
{
    using System;
    using GraphQL.Types;
    using OffersAPI_GraphQL.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>

    public class QueryObject : ObjectGraphType
    {
        public QueryObject(IOfferRepository offerRepository)
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<OfferType>(
                name: "offer",
                description: "Get an offer by it's unique identifier.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id",
                        Description = "The unique identifier of the offer.",
                    }),
                resolve: context => GetOffer(offerRepository,
                    context.GetArgument("id", Guid.Empty),
                    context.CancellationToken));

            this.FieldAsync<ListGraphType<OfferType>>(
                "offers",
                "Get all offers.",
                resolve: context => GetOffers(offerRepository, context.CancellationToken));
        }

        private async Task<object> GetOffers(IOfferRepository offerRepository, CancellationToken cancellationToken)
        {
            var offersData = await offerRepository.GetOffers(cancellationToken);
            return offersData.Select(offerData => new Offer(offerData));
        }

        private async Task<object> GetOffer(IOfferRepository offerRepository, Guid id, CancellationToken cancellationToken)
        {
            var offerData = await offerRepository.GetOffer(id, cancellationToken);
            return new Offer(offerData);
        }
    }
}
