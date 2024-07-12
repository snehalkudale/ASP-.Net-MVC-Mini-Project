using System.ComponentModel.DataAnnotations;

namespace SoleMates.Models
{
    public class OrderDetail
    {
        [Key]
        public int orderdetail_id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

        public Order Orders { get; set; }
        public Product Products { get; set; }
    }
}
