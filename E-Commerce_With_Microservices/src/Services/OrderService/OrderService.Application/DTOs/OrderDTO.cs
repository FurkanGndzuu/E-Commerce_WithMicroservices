using OrderService.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs
{
    public class OrderDTO
    {
        public string UserId { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public Address Adress { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
