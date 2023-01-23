using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly OrderContext _orderContext;

    public OrderService(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public Task<List<OrderModel>> GetAllOrders()
    {
        if (_orderContext.Orders == null) return null;
        return _orderContext.Orders?.ToListAsync();
    }

    public async Task<OrderModel> GetByOrderNumber(string orderNumber)
    {
        if (_orderContext.Orders == null) return null;
        
        var order = await _orderContext.Orders.FindAsync(orderNumber);

        if (order == null) return null;

        return order;
    }

    public async Task CreateNewOrder(OrderModel order)
        {
            if (_orderContext.Orders == null) throw new Exception("Orders storage does not exist");
    
            _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync();
        }
}