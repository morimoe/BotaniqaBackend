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
            // Загружаем продукты ОДИН РАЗ
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            // Валидация
            foreach (var item in request.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Товар с ID {item.ProductId} не найден.");
                if (product.Stock == 0)
                    throw new InvalidOperationException($"Товар «{product.ProductName}» закончился.");
                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException(
                        $"Товара «{product.ProductName}» осталось только {product.Stock} шт.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
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

                // Списываем Stock из уже загруженных объектов
                foreach (var item in request.Items)
                {
                    var product = products.First(p => p.Id == item.ProductId);
                    product.Stock -= item.Quantity;
                }
                // В CreateAsync, ПЕРЕД await _context.SaveChangesAsync();
                foreach (var item in request.Items)
                {
                    var product = products.First(p => p.Id == item.ProductId);
                    Console.WriteLine($"Product {product.Id} Stock BEFORE: {product.Stock + item.Quantity}, AFTER: {product.Stock}");
                    Console.WriteLine($"EF State: {_context.Entry(product).State}");
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<OrderData>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
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