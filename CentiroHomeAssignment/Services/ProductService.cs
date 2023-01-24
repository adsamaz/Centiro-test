using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly OrderContext _orderContext;

    public ProductService(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public Task<List<ProductModel>> GetAllProducts()
    {
        if (_orderContext.Products == null) return null;
        return _orderContext.Products.ToListAsync();
    }

    public async Task<ProductModel> GetByProductNumber(string productNumber)
    {
        if (_orderContext.Products == null) return null;
        
        var product = await _orderContext.Products.FindAsync(productNumber);

        if (product == null) return null;

        return product;
    }


    public async Task CreateNewProduct(ProductModel product)
    {
        if (_orderContext.Products == null) throw new Exception("Products storage does not exist");

        _orderContext.Products.Add(product);
        await _orderContext.SaveChangesAsync();
    }
}