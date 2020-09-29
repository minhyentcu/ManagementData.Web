using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Management.Entity;
using ManagementData.Web.FilterAttribute;
using ManagementData.Web.Models;

namespace ManagementData.Web.Controllers.Api
{
    [RoutePrefix("api/data")]
    public class DataInsertController : ApiController
    {
        private ManagementDataContext db = new ManagementDataContext();



        // GET: api/DataInsert
        public IQueryable<DataInsert> GetDataInserts()
        {
            return db.DataInserts;
        }

        // GET: api/DataInsert/5
        //[ResponseType(typeof(DataInsert))]
        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<IHttpActionResult> GetDataInsert(int id)
        //{
        //    try
        //    {
        //        var authorization = Request.Headers.Authorization;
        //        if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
        //        {
        //            return Ok(new HttpResultModel { StatusCode = (int)HttpStatusCode.Unauthorized });
        //        }

        //        var user = await db.Users.FirstOrDefaultAsync(x => x.ApiToken == Request.Headers.Authorization.Parameter);
        //        if (user == null)
        //        {
        //            return Ok(new HttpResultModel { StatusCode = (int)HttpStatusCode.Unauthorized });
        //        }
        //        var data = await db.Database
        //                  .SqlQuery<DataInserDto>("GetDataById  @Id , @userId", new SqlParameter { ParameterName = "Id", Value = id },
        //                  new SqlParameter { ParameterName = "userId", Value = user.Id }).FirstOrDefaultAsync();

        //        if (data != null)
        //        {
        //            await db.Database.ExecuteSqlCommandAsync("EXEC Proc_DeleteData  @id ", new SqlParameter { ParameterName = "id", Value = data.Id });
        //        }
        //        return Ok(new HttpResultModel { StatusCode = (int)HttpStatusCode.OK, Data = data });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new HttpResultModel { StatusCode = (int)HttpStatusCode.OK, Data = ex.Message });
        //    }
        //}

        // GET: api/DataInsert/5

        [HttpGet]
        [Route("details")]
        public async Task<IHttpActionResult> GetLastDataInsert()
        {
            try
            {
                var authorization = Request.Headers.Authorization;
                if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
                {
                    return Ok(new { });
                }

                var user = await db.Users.FirstOrDefaultAsync(x => x.ApiToken == Request.Headers.Authorization.Parameter);
                if (user == null)
                {
                    return Ok(new { });
                }
                var data = await db.Database
                          .SqlQuery<DataInserDto>("Proc_GetLastData  @userId", new SqlParameter { ParameterName = "userId", Value = user.Id }).FirstOrDefaultAsync();
                if (data != null)
                {
                    await db.Database.ExecuteSqlCommandAsync("EXEC Proc_DeleteData  @id ", new SqlParameter { ParameterName = "id", Value = data.Id });
                }

                return Ok(new HttpResultModel { Text = data.Text, TimeCreate = data.TimeCreate });
            }
            catch (Exception ex)
            {
                return Ok(new { });
            }

        }

        // POST: api/DataInsert
        [ResponseType(typeof(DataInsert))]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostDataInsert([FromBody] DataInserDto dataInsert)
        {
            try
            {
                var authorization = Request.Headers.Authorization;
                if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
                {
                    return Ok(new { });
                }

                var user = await db.Users.FirstOrDefaultAsync(x => x.ApiToken == Request.Headers.Authorization.Parameter);
                if (user == null)
                {
                    return Ok(new { });
                }
                if (dataInsert.TimeCreate == null)
                {
                    dataInsert.TimeCreate = DateTime.Now;
                }
                var result = await db.Database
                    .ExecuteSqlCommandAsync("EXEC Proc_InsertDataPost  @userId , @text, @timeCreate",
                    new SqlParameter { ParameterName = "userId", Value = user.Id },
                    new SqlParameter { ParameterName = "text", Value = dataInsert.Text },
                    new SqlParameter { ParameterName = "timeCreate", Value = dataInsert.TimeCreate });

                return Ok(new { Text = "Created" });
            }
            catch (Exception ex)
            {
                return Ok(new { });
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