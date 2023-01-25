using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentiroHomeAssignment.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : Controller
    {

        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders Returns all Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAll()
        {
            var orderList = await _orderService.GetAllOrders();
            if (orderList == null) return NotFound();
            
            return orderList;
        }


        // GET: api/orders/5 Returns a specific order using orderNumber
        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<OrderModel>> GetByOrderNumber(int orderNumber)
        {
            var order = await _orderService.GetByOrderNumber(orderNumber);
            if (order == null) return NotFound();
            
            return order;
        }

        // POST: api/orders Adds new order entry, then makes a Get on that order and returns it
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
        {
            try{
                await _orderService.CreateNewOrder(order);
            }
            catch(ArgumentException e){
                return BadRequest(e.Message);
            }

            // Returns status "201 Created" 
            return CreatedAtAction(nameof(GetByOrderNumber), new { OrderNumber = order.OrderNumber }, order);
        }
    }
}
