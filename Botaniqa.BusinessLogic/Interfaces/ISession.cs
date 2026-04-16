using Botaniqa.Domain.Entities.User;

namespace Botaniqa.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResult UserLogin(ULoginData data);
    }
}