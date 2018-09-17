using GraphQL.Types;
using OffersAPI_GraphQL.ViewModels;

namespace OffersAPI_GraphQL.Types
{
    public class EmployerType : ObjectGraphType<Employer>
    {
        public EmployerType()
        {
            Field(x => x.Name);
            Field<GeoLocationType>("address");
        }
    }
}