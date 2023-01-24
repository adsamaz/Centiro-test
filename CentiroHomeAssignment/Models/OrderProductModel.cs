using System.ComponentModel.DataAnnotations;
using CentiroHomeAssignment.Models;
using CsvHelper.Configuration.Attributes;

public class OrderProductModel
{
    [Key]
    [Optional]
    public int Id { get; set; }
    public string ProductNumber { get; set; }

    public int Quantity { get; set; }
    public string OrderLineNumber { get; set; }
    
}
