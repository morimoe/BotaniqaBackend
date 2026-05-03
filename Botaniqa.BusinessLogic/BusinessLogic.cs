
using Botaniqa.BusinessLogic.Interfaces;

namespace Botaniqa.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}