using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CentiroHomeAssignment.Models;
using System.Threading.Tasks;
using CentiroHomeAssignment.ViewModels;
using System;

namespace CentiroHomeAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly InputService _inputService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IProductService productService, InputService inputService)
        {
            _logger = logger;
            _orderService = orderService;
            _productService = productService;
            _inputService = inputService;

            // Read csv files and insert the data to storage
            PopulateStorage();
        }

        public async Task<IActionResult> IndexAsync()
        {
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

        private async void PopulateStorage()
        {
            for (var i = 1; i <= 3; i++)
            {
                (var order, var products) = _inputService.ReadOrderAndProductsFromFile($"App_Data/Order{i}.txt", "csv");
                try
                {

                    foreach (var product in products)
                    {
                        await _productService.CreateNewProduct(product);
                    }
                    await _orderService.CreateNewOrder(order);
                }
                catch (Exception)
                {
                    // Already exists 
                }
            }
        }
    }
}
