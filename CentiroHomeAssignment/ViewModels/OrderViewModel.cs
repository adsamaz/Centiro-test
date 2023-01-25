using System.Collections.Generic;
using CentiroHomeAssignment.Models;


namespace CentiroHomeAssignment.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<OrderModel> Orders { get; set; }
    }
}