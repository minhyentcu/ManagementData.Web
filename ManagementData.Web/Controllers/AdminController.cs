using Management.Entity;
using ManagementData.Service.Repository;
using ManagementData.Service.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagementData.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataInserRepository _dataInserRepository;
        private ManagementDataContext db = new ManagementDataContext();
        public AdminController(IUserRepository userRepository, IDataInserRepository dataInserRepository)
        {
            _userRepository = userRepository;
            _dataInserRepository = dataInserRepository;
        }
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            ViewBag.InfoUser = await GetInfoUser();
            var users = _userRepository.GetAll().Select(x => new UserViewModel
            {
                Id = x.Id,
                Avatar = x.Avatar,
                Email = x.Email,
                CreateDate = x.CreateDate.GetValueOrDefault(),
                FullName = x.FullName,
                Token = x.ApiToken
            });
            return View(users);
        }

        protected async Task<ManagementData.Web.Models.UserViewModel> GetInfoUser()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return new ManagementData.Web.Models.UserViewModel
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