using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels
{
    public class OrderItem : Entity
    {
        public string ProductId { get;private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int ProductQuantity { get; private set; }
        public string PictureUrl { get; private set; }

        public OrderItem()
        {
        }

        public OrderItem(string productId, string productName, string pictureUrl, decimal price , int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            ProductPrice = price;
            ProductQuantity = quantity;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price , int quantity)
        {
            ProductName = productName;
            ProductPrice = price;
            PictureUrl = pictureUrl;
            ProductQuantity = quantity;
        }
    }
}
