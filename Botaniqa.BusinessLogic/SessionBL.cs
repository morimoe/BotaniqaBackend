using Botaniqa.BusinessLogic.Core;
using Botaniqa.BusinessLogic.Interfaces;
using Botaniqa.Domain.Entities.User;

namespace Botaniqa.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResult UserLogin(ULoginData data)
        {
            if (data.Credential == "admin" && data.Password == "1234")
            {
                return new ULoginResult { Status = true, StatusMsg = "OK" };
            }

            return new ULoginResult { Status = false, StatusMsg = "Invalid credentials" };
        }
    }
}