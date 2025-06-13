using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.OrderModule
{
    public class Order
    {

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatues Status { get; set; } = OrderStatues.Pending;
        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        public decimal GetTotal() // Will Calculate it at RunTime
        {
            return SubTotal * DeliveryMethod.Cost;
        }

        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
