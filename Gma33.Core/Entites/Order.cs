using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }= DateTime.UtcNow;
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }

        public int CustomerId { get; set; }
        public Customer customer { get; set; }

        public ICollection<OrderProduct> orderProducts { get; set; } = new HashSet<OrderProduct>();


        public Payment Payment { get; set; } // Navprop to payment


    }
}
