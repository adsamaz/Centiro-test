
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CentiroHomeAssignment.Models
{
    public class ProductModel
    {
        [Key]
        public string ProductNumber { get; set; }
        // public string OrderLineNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        // public int Quantity { get; set; }
        public string ProductGroup { get; set; }

    }
}