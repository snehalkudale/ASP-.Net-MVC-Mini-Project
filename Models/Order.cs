using System.ComponentModel.DataAnnotations;

namespace SoleMates.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public DateTime order_date { get; set; }
        public decimal total_amount { get; set; }

        public Customer Customers { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
