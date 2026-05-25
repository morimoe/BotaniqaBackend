using AutoMapper;
using Botaniqa.BL.ProductDTO;
using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Botaniqa.BusinessLogic
{
    public class ProductService
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public ProductService(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<Product>>(products);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : _mapper.Map<Product>(product);
        }

        public async Task<Product> CreateAsync(CreateProductRequest request)
        {
            var productData = _mapper.Map<ProductData>(request);
            _context.Products.Add(productData);
            await _context.SaveChangesAsync();
            return _mapper.Map<Product>(productData);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            if (!string.IsNullOrEmpty(request.ProductName)) product.ProductName = request.ProductName;
            if (!string.IsNullOrEmpty(request.Description)) product.Description = request.Description;
            if (request.Price.HasValue && request.Price.Value > 0)
                product.Price = request.Price.Value;
            if (request.Stock.HasValue && request.Stock.Value >= 0)
                product.Stock = request.Stock.Value;
            if (!string.IsNullOrEmpty(request.Image)) product.Image = request.Image;
            if (!string.IsNullOrEmpty(request.Category)) product.Category = request.Category;


            await _context.SaveChangesAsync();
            return _mapper.Map<Product>(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}