using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CentiroHomeAssignment.Models;


namespace CentiroHomeAssignment.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<OrderModel> Orders { get; set; }
    }
}