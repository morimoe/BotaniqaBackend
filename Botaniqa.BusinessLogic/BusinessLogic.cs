//разобраться 
using Botaniqa.BusinessLogic.Interfaces;
namespace Botaniqa.BusinessLogic
{
    public class SessionBL :  ISession
    {

    }
        public class BL
        {
            public ISession GetSessionBL() { return new SessionBL(); }
        }
}