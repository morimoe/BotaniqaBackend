using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Botaniqa.Api
{
    public class AdminMod : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult(); // 401
                return;
            }

            if (!user.IsInRole("Admin"))
            {
                context.Result = new ForbidResult(); // 403
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}