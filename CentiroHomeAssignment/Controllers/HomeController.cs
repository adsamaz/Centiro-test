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
         private readonly InputService _inputService;

        public HomeController(ILogger<HomeController> logger, OrderService orderService, InputService inputService)
        {
            _logger = logger;
            _orderService = orderService;
            _inputService = inputService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var orders = _inputService.ReadOrdersFromCsv();
            foreach(var order in orders){
                await _orderService.CreateNewOrder(order);
            }
            

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
