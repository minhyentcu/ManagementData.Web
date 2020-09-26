using ManagementData.Service.Repository;
using ManagementData.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagementData.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataInserRepository _dataInserRepository;
        public AdminController(IUserRepository userRepository, IDataInserRepository dataInserRepository)
        {
            _userRepository = userRepository;
            _dataInserRepository = dataInserRepository;
        }
        // GET: Admin
        public async Task<ActionResult> Index()
        {
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
    }
}