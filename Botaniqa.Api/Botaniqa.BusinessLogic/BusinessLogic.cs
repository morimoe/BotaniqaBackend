using Botaniqa.BusinessLogic.Core;
using Botaniqa.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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