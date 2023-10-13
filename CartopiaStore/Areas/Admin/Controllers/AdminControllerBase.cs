using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class AdminControllerBase : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string token = HttpContext.Request.Cookies["AuthToken"];
        string userId = HttpContext.Request.Cookies["UserId"];

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            
            context.Result = RedirectToAction("index", "Auth");
        }

        base.OnActionExecuting(context);
    }


}
