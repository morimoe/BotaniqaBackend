using Botaniqa.DataAccess.Context;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Botaniqa.Domain.Entities.User;


namespace Botaniqa.BusinessLogic.Core
 
{

    public class UserActions
    {


        public UserActions() { }
        internal bool UserLoginDataValidationExecution(UserLogin udata)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(

                 u  => u.UserName == udata.Credential && u.Password == udata.Password);
                return user != null;
            }
        }


        public string UserTokenGeneration(UserLogin udata)
        {

            var token = new TokenService();

            var userToken= token.GenerateToken();

            return userToken;
        }
    }
}