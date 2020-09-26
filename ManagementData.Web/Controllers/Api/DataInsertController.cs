using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Management.Entity;
using ManagementData.Web.FilterAttribute;

namespace ManagementData.Web.Controllers.Api
{
    [RoutePrefix("api/data")]
    public class DataInsertController : ApiController
    {
        private ManagementDataContext db = new ManagementDataContext();


        //[HttpGet]
        //public  IQueryable<DataInsert> DataInserts()
        //{

        //}

        
        // GET: api/DataInsert
        public IQueryable<DataInsert> GetDataInserts()
        {
            return db.DataInserts;
        }

        // GET: api/DataInsert/5
        [ResponseType(typeof(DataInsert))]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetDataInsert(int? id)
        {
            DataInsert dataInsert = await db.DataInserts.FindAsync(id);
            if (dataInsert == null)
            {
                return NotFound();
            }
            return Ok(dataInsert);
        }

        // GET: api/DataInsert/5
        //[ResponseType(typeof(DataInsert))]
        [HttpGet]
        [Route("details")]
        //[FilterAttributeData]
        public async Task<IHttpActionResult> GetLastDataInsert()
        {
           
            DataInsert dataInsert = await db.DataInserts.FindAsync(20);
            if (dataInsert == null)
            {
                return NotFound();
            }
            return Ok(dataInsert);
        }

        // PUT: api/DataInsert/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDataInsert(int id, DataInsert dataInsert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dataInsert.Id)
            {
                return BadRequest();
            }

            db.Entry(dataInsert).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataInsertExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DataInsert
        [ResponseType(typeof(DataInsert))]
        public async Task<IHttpActionResult> PostDataInsert(DataInsert dataInsert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DataInserts.Add(dataInsert);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dataInsert.Id }, dataInsert);
        }

        // DELETE: api/DataInsert/5
        [ResponseType(typeof(DataInsert))]
        public async Task<IHttpActionResult> DeleteDataInsert(int id)
        {
            DataInsert dataInsert = await db.DataInserts.FindAsync(id);
            if (dataInsert == null)
            {
                return NotFound();
            }

            db.DataInserts.Remove(dataInsert);
            await db.SaveChangesAsync();

            return Ok(dataInsert);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DataInsertExists(int id)
        {
            return db.DataInserts.Count(e => e.Id == id) > 0;
        }
    }
}