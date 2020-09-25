using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ManagementData.Web.Models;
using Management.Entity;

namespace ManagementData.Web.Controllers
{
    [Authorize]
    public class ExampleController : Controller
    {
        public ActionResult Invoice()
        {
            return View();
        }

        public ActionResult InvoicePrint()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Lockscreen()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult BlankPage()
        {
            return View();
        }

        public ActionResult PacePage()
        {
            return View();
        }
    }
}