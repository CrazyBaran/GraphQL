using GraphQL.Types;
using OffersAPI_GraphQL.ViewModels;

namespace OffersAPI_GraphQL.Types
{
    public class GeoLocationType : ObjectGraphType<GeoLocation>
    {
        public GeoLocationType()
        {
            Field(x => x.CountryName).Description("Country");
            Field(x => x.CityName).Description("City");
        }
    }
}