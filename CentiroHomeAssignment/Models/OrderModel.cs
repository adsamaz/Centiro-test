
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentiroHomeAssignment.Models
{
    public class OrderModel
    {
        [Key]
		public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int CustomerNumber { get; set; }
        public List<OrderProductModel>OrderProducts { get; set; }
    }
}