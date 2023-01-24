using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentiroHomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentiroHomeAssignment.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products Returns all Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll()
        {
            var productList = await _productService.GetAllProducts();
            if (productList == null) return NotFound();
            
            return productList;
        }


        // GET: api/products/5 Returns a specific products using productNumber
        [HttpGet("{productNumber}")]
        public async Task<ActionResult<ProductModel>> GetByProductNumber(string productNumber)
        {
            var product = await _productService.GetByProductNumber(productNumber);
            if (product == null) return NotFound();
            
            return product;
        }

        // POST: api/products Adds new products entry, then makes a Get on that products and returns it
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {
            try{
                await _productService.CreateNewProduct(product);
            }
            catch(Exception e){
                throw e;
            }

            // Returns status "201 Created" 
            return CreatedAtAction(nameof(GetByProductNumber), new { ProductNumber = product.ProductNumber }, product);
        }
    }
}
