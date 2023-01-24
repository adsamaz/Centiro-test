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
        return _orderContext.Orders.Include(order => order.OrderProducts).ToListAsync();
    }

    public async Task<OrderModel> GetByOrderNumber(int orderNumber)
    {
        if (_orderContext.Orders == null) return null;
        
        var order = await _orderContext.Orders.Include(order => order.OrderProducts).FirstOrDefaultAsync(order => order.OrderNumber == orderNumber);

        if (order == null) return null;

        return order;
    }

    public async Task CreateNewOrder(OrderModel order)
    {    
        _orderContext.Orders.Add(order);
        await _orderContext.SaveChangesAsync();
    }
}