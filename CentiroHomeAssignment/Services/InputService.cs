using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CentiroHomeAssignment.Models;
using CsvHelper;
using CsvHelper.Configuration;

public class InputService
{
    public (OrderModel, List<ProductModel>) ReadOrderAndProductsFromFile(string inputFile, string fileType)
    {
        if (fileType == "csv"){
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

            return (order, products);
        
        }
        else throw new NotSupportedException("File type not supported");
    }
}
