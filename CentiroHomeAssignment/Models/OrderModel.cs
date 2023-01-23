
namespace CentiroHomeAssignment.Models
{
    public class OrderModel
    {
		public int Id { get; set; }
        public string OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProductGroup { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int CustomerNumber { get; set; }
    }
}