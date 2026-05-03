using Botaniqa.DataAccess;
using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Botaniqa.BusinessLogic.Core
{
    public class UserActions
    {
        public UserActions() { }

        internal bool UserLoginDataValidationExecution(UserLogin udata)
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseSqlServer(DbSession.ConnectionString)
                .Options;

            using (var db = new UserContext(options))
            {
                var user = db.Users.FirstOrDefault(
                    u => u.UserName == udata.Credential && u.Password == udata.Password);
                return user != null;
            }
        }
    }
}