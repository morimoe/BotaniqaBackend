using Botaniqa.BL.OrderDTO;
using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;

namespace Botaniqa.BusinessLogic
{
    public class OrderService
    {
        private readonly UserContext _context;

        public OrderService(UserContext context)
        {
            _context = context;
        }

        public async Task<OrderData> CreateAsync(CreateOrderRequest request, int? userId)
        {
            var total = request.Items.Sum(i => i.Price * i.Quantity);
            var order = new OrderData
            {
                UserId = userId,
                City = request.City,
                Street = request.Street,
                House = request.House,
                Apartment = request.Apartment?.ToString(),
                Entrance = request.Entrance?.ToString(),
                Floor = request.Floor,
                Intercom = request.Intercom?.ToString(),
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Comment = request.Comment,
                PaymentMethod = request.PaymentMethod,
                TotalPrice = total,
                CreatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var items = request.Items.Select(i => new OrderItem
            {
                OrderId = order.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();
            _context.OrderItems.AddRange(items);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<OrderData>> GetAllAsync()
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}