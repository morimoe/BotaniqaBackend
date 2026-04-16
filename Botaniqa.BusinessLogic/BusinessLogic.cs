using Botaniqa.BusinessLogic;
using Botaniqa.BusinessLogic.Interfaces;

namespace eUseControl.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}