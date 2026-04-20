namespace Botaniqa.BusinessLogic
{
    public class TokenService
    {
        public string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}