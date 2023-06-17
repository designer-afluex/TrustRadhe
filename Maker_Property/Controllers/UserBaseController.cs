﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrustRadhe.Controllers
{
    public class UserBaseController : Controller
    {
        //
        // GET: /Base/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            // If the browser session or authentication session has expired...
            if (session.IsNewSession)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }
            else
            {
                //var data=(List<Login>) Session["LoginUser"];
                //if (data[0].UserType == 1)
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                //      new { action = "Index", Controller = "Admin" }));
                //}
                //else if (data[0].UserType == 3)
                //{
                //    filterContext.Result = new RedirectResult("http://www.google.com", true);                 
                //}
                //else
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                //      new { action = "Index", Controller = "Home" }));
                //}
            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
