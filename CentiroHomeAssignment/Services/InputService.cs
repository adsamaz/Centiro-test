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
    public IEnumerable<OrderModel> ReadOrdersFromCsv()
    {
        // var fileName = @"<path to our CSV file>";
        // var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        // {
        //     Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
        //     Delimiter = "," // The delimiter is a comma.
        // };

        // using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        // {
        //     using (var textReader = new StreamReader(fs, Encoding.UTF8))
        //     using (var csv = new CsvReader(textReader, configuration))
        //     {
        //         var data = csv.GetRecords<Person>();
                
        //         foreach (var person in data)
        //         {
        //             // Do something with values in each row
        //         }
        //     }
        // }

        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
            Delimiter = "|" // The delimiter is a pipe.
        };
        using (var reader = new StreamReader("App_Data/Order1.txt"))
        {
            var csv = new CsvReader(reader, configuration);
            // csv.Context.RegisterClassMap<OrderMap>();
            var orders = csv.GetRecords<OrderModel>().ToList();
            // Do something with the orders
            return orders;
        }
    }
}

public class OrderMap : ClassMap<OrderModel>
{
    public OrderMap()
    {
        Map(m => m.Id).Name("OrderNumber");
    }
}