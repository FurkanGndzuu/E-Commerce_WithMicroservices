using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels
{
    public class Order : Entity , IAggregateRoot
    {
        public string UserId { get;private set; }
        public decimal totalPrice { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Address Adress { get;private set; }

        public List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public OrderStatus Status { get; set; }



        public Order()
        {
        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            UserId = buyerId;
            Adress = address;
            totalPrice = GetTotalPrice;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl , int quantity)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price,quantity);

                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.ProductPrice);
    }
}

public enum OrderStatus
{
    Suspend,
    Failed,
    Completed
}
