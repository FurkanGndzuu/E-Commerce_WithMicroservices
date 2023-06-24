using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels
{
    public class Address : ValueObject
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public Address(string country, string city, string district, string street, string houseNumber)
        {
            Country = country;
            City = city;
            District = district;
            Street = street;
            HouseNumber = houseNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;yield return City; yield return District; yield return Street; yield return HouseNumber;
        }
    }
}
