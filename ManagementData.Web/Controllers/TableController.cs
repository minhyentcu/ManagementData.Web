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
    public class TableController : Controller
    {
        public ActionResult Simple()
        {
            return View();
        }

        public ActionResult Data()
        {
            return View();
        }
    }
}