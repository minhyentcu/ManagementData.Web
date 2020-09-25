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
    public class UIElementController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Icon()
        {
            return View();
        }

        public ActionResult Button()
        {
            return View();
        }

        public ActionResult Slider()
        {
            return View();
        }

        public ActionResult Timeline()
        {
            return View();
        }

        public ActionResult Modal()
        {
            return View();
        }
    }
}