using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CentiroHomeAssignment.Models;
using System.Threading.Tasks;
using CentiroHomeAssignment.ViewModels;
using System.Collections.Generic;
using System;

namespace CentiroHomeAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly InputService _inputService;

        public HomeController(ILogger<HomeController> logger, OrderService orderService, ProductService productService, InputService inputService)
        {
            _logger = logger;
            _orderService = orderService;
            _productService = productService;
            _inputService = inputService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            // Read csv files and insert the data to storage
            for(var i = 1; i <= 3; i++){
                var orderAndProducts = _inputService.ReadOrderAndProductsFromFile($"App_Data/Order{i}.txt", "csv");
                try{

                    foreach(var product in orderAndProducts.Item2){
                        await _productService.CreateNewProduct(product);
                    }
                    await _orderService.CreateNewOrder(orderAndProducts.Item1);
                }catch(Exception){
                   // Already exists 
                }
            }
            
            

            // Render the view this all orders
            var orderList = await _orderService.GetAllOrders();
            var viewModel = new OrderViewModel
            {
                Orders = orderList
            };
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
