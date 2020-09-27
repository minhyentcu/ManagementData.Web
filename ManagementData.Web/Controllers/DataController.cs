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
    public class DataController : BaseController
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

        [HttpPost]
        public async Task<ActionResult> UpdateData(string text, int id)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_UpdateData  @id , @text", new SqlParameter { ParameterName = "id", Value = id }, new SqlParameter { ParameterName = "text", Value = text });
            return RedirectToAction<DataController>(nameof(Index));
        }


        [HttpPost]
        public async Task<ActionResult> InsertData(string text, string userId)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_InsertData  @userId , @text", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "text", Value = text });
            //db.Database
            //       .SqlQuery<DataInsert>("Proc_InsertData  @userId , @text", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "text", Value = text });
            return RedirectToAction<DataController>(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string userId, int? idLast = 0)
        {
            try
            {
                var page = 1;
                idLast = Convert.ToInt32(Request["idLast"]);
                var start = Convert.ToInt32(Request["start"]);
                page = start / 10 + 1;
                int length = Convert.ToInt32(Request["length"]) < 0 ? 50 : Convert.ToInt32(Request["length"]);
                var datas = await db.Database
                      .SqlQuery<DataInsert>("PROC_GetData_Paging  @page , @size, @userId", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "page", Value = page }, new SqlParameter { ParameterName = "size", Value = length }).ToListAsync();
                //var datas = await db.Database
                //      .SqlQuery<DataInsert>("Proc_Paging_demo  @top , @userId, @idlast", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "top", Value = length }, new SqlParameter { ParameterName = "idLast", Value = idLast }).ToListAsync();
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

}