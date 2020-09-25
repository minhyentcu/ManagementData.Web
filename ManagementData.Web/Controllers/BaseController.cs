using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementData.Web.Controllers
{
    public class BaseController : Controller
    {
        public RedirectToRouteResult RedirectToAction<T>(string ActionName, object routeValues) where T : BaseController
        {
            string controllerName = typeof(T).Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            return RedirectToAction(ActionName, controllerName, routeValues);
        }
        public RedirectToRouteResult RedirectToAction<T>(string ActionName) where T : BaseController
        {
            return RedirectToAction<T>(ActionName, null);
        }
    }
}