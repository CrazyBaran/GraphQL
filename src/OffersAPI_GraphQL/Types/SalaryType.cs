using GraphQL.Types;
using OffersAPI_GraphQL.ViewModels;

namespace OffersAPI_GraphQL.Types
{
    internal class SalaryType : ObjectGraphType<Salary>
    {
        public SalaryType()
        {
            Field(x => x.Range).Description("Salary range.");
        }
    }
}