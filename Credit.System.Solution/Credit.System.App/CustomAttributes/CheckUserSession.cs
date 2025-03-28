using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.CustomAttributes
{
    public class CheckUserSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userLogged = context.HttpContext.Session.GetString("UserLogged");
           
            if (string.IsNullOrEmpty(userLogged))
            {
                context.Result = new RedirectToActionResult("Index", "User", null);
            }

            base.OnActionExecuting(context);
        }
    }


}
