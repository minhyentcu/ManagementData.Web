using Management.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagementData.Web.Controllers
{
    [Authorize]
    public class DataController : Controller
    {

        private ManagementDataContext db = new ManagementDataContext();
        // GET: Data
        public async Task<ActionResult> Index()
        {
            return View();
        }


        public async Task<ActionResult> Details(string userId)
        {

            return View();
        }
        public async Task<JsonResult> GetData(string userId, int? idLast = 0)
        {
            try
            {
                idLast = Convert.ToInt32(Request["idLast"]);
                var start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]) < 0 ? 50 : Convert.ToInt32(Request["length"]);
                var datas = await db.Database
                      .SqlQuery<DataInsert>("Proc_Paging_demo  @top , @userId, @idlast", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "top", Value = length }, new SqlParameter { ParameterName = "idLast", Value = idLast }).ToListAsync();
                idLast = datas.LastOrDefault()?.Id;
                return Json(new { dataObject = datas, idlast = idLast }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }

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

    public class MyList<T> : List<T>
    {
        public int? idLast { get; set; }
    }
}