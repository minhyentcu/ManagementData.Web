using Management.Entity;
using ManagementData.Web.Models;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
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
            ViewBag.InfoUser = await GetInfoUser();
            return View();
        }


        public async Task<ActionResult> Details(string userId)
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteData(int id, string admin)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_DeleteData  @id ", new SqlParameter { ParameterName = "id", Value = id });
            if (!string.IsNullOrEmpty(admin))
            {
                return RedirectToAction<AdminController>(nameof(Index));
            }
            return RedirectToAction<HomeController>(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAllData(IEnumerable<int> dataItems, string admin)
        {
            try
            {
                foreach (var item in dataItems)
                {
                    await db.Database.ExecuteSqlCommandAsync("EXEC Proc_DeleteData  @id ", new SqlParameter { ParameterName = "id", Value = item });
                }

                if (!string.IsNullOrEmpty(admin))
                {
                    return RedirectToAction<AdminController>(nameof(Index));
                }
                return RedirectToAction<HomeController>(nameof(Index));
            }
            catch (Exception)
            {

                if (!string.IsNullOrEmpty(admin))
                {
                    return RedirectToAction<AdminController>(nameof(Index));
                }
                return RedirectToAction<HomeController>(nameof(Index));
            }

        }



        [HttpPost]
        public async Task<ActionResult> UpdateData(string text, int id, string admin)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_UpdateData  @id , @text", new SqlParameter { ParameterName = "id", Value = id }, new SqlParameter { ParameterName = "text", Value = text });
            if (!string.IsNullOrEmpty(admin))
            {
                return RedirectToAction<AdminController>(nameof(Index));
            }
            return RedirectToAction<HomeController>(nameof(Index));
        }


        [HttpPost]
        public async Task<ActionResult> InsertData(string text, string userId, string admin)
        {
            try
            {
                var datas = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (var item in datas)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        await db.Database.ExecuteSqlCommandAsync("EXEC Proc_InsertData  @userId , @text", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "text", Value = item });
                    }
                }
                if (!string.IsNullOrEmpty(admin))
                {
                    return RedirectToAction<AdminController>(nameof(Index));
                }
                return RedirectToAction<HomeController>(nameof(Index));
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(admin))
                {
                    return RedirectToAction<AdminController>(nameof(Index));
                }
                return RedirectToAction<HomeController>(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetData(string userId, int? idLast = 0)
        {
            try
            {
                var page = 1;
                //idLast = Convert.ToInt32(Request["idLast"]);
                var start = Convert.ToInt32(Request["start"]);
                var orderSort = 1;
                if (Request["order[0][dir]"] == "asc")
                {
                    orderSort = 0;
                }
                var search = string.Empty;
                int length = Convert.ToInt32(Request["length"]) < 0 ? 50 : Convert.ToInt32(Request["length"]);
                page = (start / length) + 1;
                var datas = await db.Database
                  .SqlQuery<DataDto>($"SELECT* FROM DataInserts where UserId = '{userId}' ORDER BY TimeCreate desc  OFFSET({page} - 1) * {length} ROWS FETCH NEXT {length} ROWS ONLY").ToListAsync();

                //var count = start / 10;
                var dataCount = datas.Count();
                for (int i = 0; i < dataCount; i++)
                {
                    datas[i].Stt = i + 1 + (page - 1) * length;
                    datas[i].TimeString = datas[i].TimeCreate.ToString("yyyy-MM-dd hh:mm:ss.fff");
                }

                return Json(new { dataObject = datas, idlast = idLast, page = page, size = length, orderSort = orderSort }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public async Task<FileResult> Export(string userId, int page = 1, int size = 10, int orderSort = 0, string windowId = "")
        {
            try
            {
                //var datas = await db.Database
                //   .SqlQuery<DataInsert>("PROC_GetData_Paging  @page , @size, @userId, @orderId, @search",
                //   new SqlParameter { ParameterName = "userId", Value = userId },
                //   new SqlParameter { ParameterName = "page", Value = page },
                //   new SqlParameter { ParameterName = "size", Value = size },
                //   new SqlParameter { ParameterName = "orderId", Value = orderSort },
                //   new SqlParameter { ParameterName = "search", Value = string.Empty }).ToListAsync();
                using (var dbstransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var datas = await db.Database
                                    .SqlQuery<DataInsert>($"SELECT * FROM DataInserts WHERE UserId='{userId}'").ToListAsync();

                        string fileName = "DataExport" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        using (ExcelPackage p = new ExcelPackage())
                        {
                            //Here setting some document properties
                            p.Workbook.Properties.Author = "DataLarge";
                            p.Workbook.Properties.Title = "Danh sách data";

                            //Create a sheet
                            p.Workbook.Worksheets.Add("Danh sách");
                            ExcelWorksheet ws = p.Workbook.Worksheets.FirstOrDefault();
                            ws.Name = "Danh sách"; //Setting Sheet's name
                            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                            // Create header column
                            string[] arrColumnHeader = {    "STT",
                                                "Id",
                                                "Text",
                                                "CreateDate",
                                                };
                            var countColHeader = arrColumnHeader.Count();
                            int colIndex = 1;
                            int rowIndex = 1;

                            //Creating Headings
                            foreach (var item in arrColumnHeader)
                            {
                                var cell = ws.Cells[rowIndex, colIndex];

                                //Setting the background color of header cells to Gray
                                var fill = cell.Style.Fill;
                                fill.PatternType = ExcelFillStyle.Solid;
                                fill.BackgroundColor.SetColor(Color.LightBlue);

                                //Setting Top/left,right/bottom borders.
                                var border = cell.Style.Border;
                                border.Bottom.Style =
                                    border.Top.Style =
                                    border.Left.Style =
                                    border.Right.Style = ExcelBorderStyle.Thin;

                                //Setting Value in cell
                                cell.Value = item;

                                colIndex++;
                            }

                            // Adding Data into rows
                            int stt = 1;
                            foreach (var item in datas)
                            {
                                colIndex = 1;
                                rowIndex++;
                                //Setting Value in cell
                                ws.Cells[rowIndex, colIndex++].Value = stt++;
                                ws.Cells[rowIndex, colIndex++].Value = item.Id;
                                ws.Cells[rowIndex, colIndex++].Value = item.Text;
                                ws.Cells[rowIndex, colIndex++].Value = item.TimeCreate.ToString("yyyy-MM-dd hh:mm:ss.fff");
                            }

                            // Generate A File with name
                            Byte[] bin = p.GetAsByteArray();
                            await db.Database.ExecuteSqlCommandAsync($"DELETE FROM DataInserts WHERE UserId='{userId}'");
                            dbstransaction.Commit();
                            Response.Cookies.Add(new HttpCookie(windowId, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff")));
                            return File(bin, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                        }
                    }
                    catch (Exception)
                    {
                        dbstransaction.Rollback();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }


            //System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(mstream, System.Text.Encoding.UTF8);
            //streamWriter.WriteLine("abc");
            //streamWriter.WriteLine("abc");
            //byte[] buffer = new byte[mstream.Length];
            //mstream.Seek(0, System.IO.SeekOrigin.Begin);
            //mstream.Read(buffer, 0, (int)mstream.Length);



            //StringBuilder sb = new StringBuilder();
            //foreach (var item in datas)
            //{
            //    sb.AppendLine(item.ToString());
            //}
            //byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
            //return File(buffer, System.Net.Mime.MediaTypeNames.Application.Octet, "Data.txt");

            //using (System.IO.MemoryStream mstream = new System.IO.MemoryStream())
            //{
            //    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(mstream, System.Text.Encoding.UTF8))
            //    {
            //        foreach (var item in datas)
            //        {
            //            streamWriter.WriteLine(item.ToString());
            //        }
            //        byte[] buffer = new byte[mstream.Length];
            //        mstream.Seek(0, System.IO.SeekOrigin.Begin);
            //        mstream.Read(buffer, 0, (int)mstream.Length);
            //        return File(buffer, System.Net.Mime.MediaTypeNames.Application.Octet, "Data.txt");
            //    }
            //}


        }


        [HttpPost]
        public async Task<ActionResult> Import(string userId, string admin)
        {
            try
            {
                HttpPostedFileBase file = Request.Files[0];

                var path = Path.GetExtension(file.FileName);


                switch (path)
                {
                    case ".txt":
                        using (var stream = new StreamReader(file.InputStream, Encoding.UTF8))
                        {

                            while (stream.Peek() >= 0)
                            {
                                var line = stream.ReadLine();
                                if (!string.IsNullOrEmpty(line))
                                {
                                    await db.Database.ExecuteSqlCommandAsync("EXEC Proc_InsertDataPost  @userId , @text, @timeCreate",
                                       new SqlParameter { ParameterName = "userId", Value = userId },
                                       new SqlParameter { ParameterName = "text", Value = line },
                                       new SqlParameter { ParameterName = "timeCreate", Value = DateTime.Now });
                                }
                            }

                            if (!string.IsNullOrEmpty(admin))
                            {
                                return RedirectToAction<AdminController>(nameof(Index));
                            }
                            return RedirectToAction<HomeController>(nameof(Index));
                        }
                    case ".xls":
                    case ".xlsx":
                        // Excel
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        using (ExcelPackage excelPackage = new ExcelPackage(file.InputStream))
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                            int rows = worksheet.Dimension.Rows; // 20
                            int columns = worksheet.Dimension.Columns; // 7
                                                                       // loop through the worksheet rows and columns
                            for (int i = 1; i <= rows; i++)
                            {
                                try
                                {
                                    var text = worksheet.Cells[i, 1].Value.ToString();
                                    await db.Database.ExecuteSqlCommandAsync("EXEC Proc_InsertDataPost  @userId , @text, @timeCreate",
                                        new SqlParameter { ParameterName = "userId", Value = userId },
                                        new SqlParameter { ParameterName = "text", Value = text },
                                        new SqlParameter { ParameterName = "timeCreate", Value = DateTime.Now });
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (!string.IsNullOrEmpty(admin))
                            {
                                return RedirectToAction<AdminController>(nameof(Index));
                            }
                            return RedirectToAction<HomeController>(nameof(Index));
                        };
                    default:
                        if (!string.IsNullOrEmpty(admin))
                        {
                            return RedirectToAction<AdminController>(nameof(Index));
                        }
                        return RedirectToAction<HomeController>(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(admin))
                {
                    return RedirectToAction<AdminController>(nameof(Index));
                }
                return RedirectToAction<HomeController>(nameof(Index));
            }
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