using System;
using System.Collections.Generic;

namespace Datasource
{
    public class OfferData
    {
        public  Guid Id { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public string JobTitle { get; set; }
        public string LocationCity { get; set; }
        public string LocationCountry { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddressCity { get; set; }
        public string EmployerAddressCountry { get; set; }
        public string Salary { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public ICollection<int> CandidateIds { get; set; }
    }
}
