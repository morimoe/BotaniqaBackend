namespace Botaniqa.Domain.Entities.Favorites
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}