using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.Cart;
using Botaniqa.Domain.Entities.Favorites;
using Microsoft.EntityFrameworkCore;

namespace Botaniqa.BusinessLogic
{
    public class CartActions
    {
        private readonly UserContext _context;

        public CartActions(UserContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCart(int userId)
        {
            return await _context.CartItems
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task AddToCart(int userId, int productId, int quantity)
        {
            var items = await _context.CartItems
                .Where(x => x.UserId == userId && x.ProductId == productId)
                .ToListAsync();

            if (items.Count == 0)
            {
                _context.CartItems.Add(new CartItem { UserId = userId, ProductId = productId, Quantity = quantity });
            }
            else
            {
                items[0].Quantity += quantity;
                if (items.Count > 1)
                    _context.CartItems.RemoveRange(items.Skip(1));
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int userId, int productId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCart(int userId)
        {
            var items = _context.CartItems.Where(x => x.UserId == userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        // ===== ИЗБРАННОЕ =====

        public async Task<List<FavoriteItem>> GetFavorites(int userId)
        {
            return await _context.FavoriteItems
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task ToggleFavorite(int userId, int productId)
        {
            var item = await _context.FavoriteItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (item == null)
                _context.FavoriteItems.Add(new FavoriteItem { UserId = userId, ProductId = productId });
            else
                _context.FavoriteItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}