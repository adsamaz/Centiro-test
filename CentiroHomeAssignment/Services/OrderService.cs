using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;


public interface IOrderService
{
    Task<List<OrderModel>> GetAllOrders();
    Task<OrderModel> GetByOrderNumber(int orderNumber);
    Task CreateNewOrder(OrderModel order);
}

public class OrderService : IOrderService
{
    private readonly OrderContext _orderContext;
    private readonly IProductService _productService;

    public OrderService(OrderContext orderContext, IProductService productService)
    {
        _orderContext = orderContext;
        _productService = productService;
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
        if (!await ValidateOrderProducts(order.OrderProducts)) throw new ArgumentException("OrderProducts did not validate. Do the products exist?");

        _orderContext.Orders.Add(order);
        await _orderContext.SaveChangesAsync();
    }

    private async Task<bool> ValidateOrderProducts(List<OrderProductModel> orderProducts){
        if (orderProducts.Count == 0) return false;

        foreach (var orderProduct in orderProducts){
            var product = await _productService.GetByProductNumber(orderProduct.ProductNumber);
            if (product == null) return false;
        }

        return true;

    }
}