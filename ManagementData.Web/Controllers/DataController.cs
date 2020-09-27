using Management.Entity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
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
        public async Task<ActionResult> DeleteData(int id)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_DeleteData  @id ", new SqlParameter { ParameterName = "id", Value = id });
            return RedirectToAction<DataController>(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateData(string text, int id, string admin)
        {
            var result = await db.Database.ExecuteSqlCommandAsync("EXEC Proc_UpdateData  @id , @text", new SqlParameter { ParameterName = "id", Value = id }, new SqlParameter { ParameterName = "text", Value = text });
            if (!string.IsNullOrEmpty(admin))
            {
                return RedirectToAction<AdminController>(nameof(Index));
            }
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

        [HttpGet]
        public async Task<FileResult> Export(string userId)
        {
            //System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(mstream, System.Text.Encoding.UTF8);
            //streamWriter.WriteLine("abc");
            //streamWriter.WriteLine("abc");
            //byte[] buffer = new byte[mstream.Length];
            //mstream.Seek(0, System.IO.SeekOrigin.Begin);
            //mstream.Read(buffer, 0, (int)mstream.Length);

            var datas = await db.Database
                     .SqlQuery<DataInsert>("PROC_GetData_Paging  @page , @size, @userId", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "page", Value = 1 }, new SqlParameter { ParameterName = "size", Value = 100000 }).ToListAsync();

            StringBuilder sb = new StringBuilder();
            foreach (var item in datas)
            {
                sb.AppendLine(item.ToString());
            }
            byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, System.Net.Mime.MediaTypeNames.Application.Octet, "Data.txt");

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

            //string fileName = "DataExport" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;
            //using (ExcelPackage p = new ExcelPackage())
            //{
            //    //Here setting some document properties
            //    p.Workbook.Properties.Author = "DataLarge";
            //    p.Workbook.Properties.Title = "Danh sách data";

            //    //Create a sheet
            //    p.Workbook.Worksheets.Add("Danh sách");
            //    ExcelWorksheet ws = p.Workbook.Worksheets.FirstOrDefault();
            //    ws.Name = "Danh sách"; //Setting Sheet's name
            //    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            //    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            //    // Create header column
            //    string[] arrColumnHeader = {    "STT",
            //                                    "Id",
            //                                    "Text",
            //                                    "CreateDate",
            //                                    };
            //    var countColHeader = arrColumnHeader.Count();

            //    //Merging cells and create a center heading for out table
            //    ws.Cells[1, 1].Value = "Danh sách";
            //    ws.Cells[1, 1, 1, countColHeader].Merge = true;
            //    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
            //    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //    int colIndex = 1;
            //    int rowIndex = 2;

            //    //Creating Headings
            //    foreach (var item in arrColumnHeader)
            //    {
            //        var cell = ws.Cells[rowIndex, colIndex];

            //        //Setting the background color of header cells to Gray
            //        var fill = cell.Style.Fill;
            //        fill.PatternType = ExcelFillStyle.Solid;
            //        fill.BackgroundColor.SetColor(Color.LightBlue);

            //        //Setting Top/left,right/bottom borders.
            //        var border = cell.Style.Border;
            //        border.Bottom.Style =
            //            border.Top.Style =
            //            border.Left.Style =
            //            border.Right.Style = ExcelBorderStyle.Thin;

            //        //Setting Value in cell
            //        cell.Value = item;

            //        colIndex++;
            //    }

            //    // Adding Data into rows
            //    int stt = 1;
            //    foreach (var item in datas)
            //    {
            //        colIndex = 1;
            //        rowIndex++;
            //        //Setting Value in cell
            //        ws.Cells[rowIndex, colIndex++].Value = stt++;
            //        ws.Cells[rowIndex, colIndex++].Value = item.Id;
            //        ws.Cells[rowIndex, colIndex++].Value = item.Text;
            //        ws.Cells[rowIndex, colIndex++].Value = item.TimeCreate.ToString("yyyy-mm-dd hh:mm:ss.fff") ;

            //        //Setting borders of cell
            //        //var border = cell.Style.Border;
            //        //border.Left.Style =
            //        //    border.Right.Style = ExcelBorderStyle.Thin;
            //    }

            //Generate A File with name
            //Byte[] bin = p.GetAsByteArray();
            //return File(bin, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            //}
        }


        [HttpPost]
        public async Task<ActionResult> Import(string userId)
        {
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (ExcelPackage excelPackage = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    int rows = worksheet.Dimension.Rows; // 20
                    int columns = worksheet.Dimension.Columns; // 7

                    // loop through the worksheet rows and columns
                    for (int i = 3; i <= rows; i++)
                    {
                        for (int j = 1; j <= columns; j++)
                        {

                            string content = worksheet.Cells[i, j].Value.ToString();
                            /* Do something ...*/
                        }
                    }
                };
            }
            catch (Exception ex)
            {

                return View();
            }

            return View();

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