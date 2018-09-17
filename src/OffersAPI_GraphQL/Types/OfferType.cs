using GraphQL.Types;
using OffersAPI_GraphQL.ViewModels;

namespace OffersAPI_GraphQL.Types
{
    public class OfferType : ObjectGraphType<Offer>
    {
        public OfferType()
        {
            Name = "Offer";
            Description = "Job offer";

            Field(x => x.OfferId, type: typeof(IdGraphType))
                .Description("The unique identifier of the offer.");
            Field(x => x.JobTitle)
                .Description("Job title.");

            Field<EmployerType>("employer");
            Field<GeoLocationType>("location");
            Field<SalaryType>("salary");
            Field(x => x.ExpirationDateUtc)
                .Description("Expiration date.");
            Field(x => x.CreationDateUtc)
                .Description("Creation date.");
            Field(x => x.ApplicationsNumber, nullable: true)
                .Description("Number of users who applied for the offer.");
        }
    }
}
