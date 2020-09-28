using Management.Entity;
using ManagementData.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagementData.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private ManagementDataContext db = new ManagementDataContext();
        public async Task<ActionResult> Index()
        {
            ViewBag.InfoUser = await GetInfoUser();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        protected async Task<UserViewModel> GetInfoUser()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.FullName ?? user.Email,
                Avatar = user.Avatar ?? string.Empty
            };
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}