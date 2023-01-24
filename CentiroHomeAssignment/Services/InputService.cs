using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

public class InputService
{
    public Tuple<OrderModel, List<ProductModel>> ReadOrderAndProductsFromCsv(string inputFile)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
            Delimiter = "|", // The delimiter is a pipe.
        };
        var reader = new StreamReader(inputFile);
        var csv = new CsvReader(reader, configuration);
        var order = csv.GetRecords<OrderModel>().ToList()[0];

        reader = new StreamReader(inputFile);
        csv = new CsvReader(reader, configuration);
        var orderProducts = csv.GetRecords<OrderProductModel>().ToList();
        order.OrderProducts = orderProducts;

        reader = new StreamReader(inputFile);
        csv = new CsvReader(reader, configuration);
        var products = csv.GetRecords<ProductModel>().ToList();

        return Tuple.Create(order, products);
        
    }
}
