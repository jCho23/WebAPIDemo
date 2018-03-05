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
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class PostMessagesTablesController : ApiController
    {
        private LionAppDBEntities db = new LionAppDBEntities();

        // GET: api/PostMessagesTables
        public IQueryable<PostMessagesTable> GetPostMessagesTables()
        {
            return db.PostMessagesTables;
        }

        // GET: api/PostMessagesTables/5
        [ResponseType(typeof(PostMessagesTable))]
        public async Task<IHttpActionResult> GetPostMessagesTable(int id)
        {
            PostMessagesTable postMessagesTable = await db.PostMessagesTables.FindAsync(id);
            if (postMessagesTable == null)
            {
                return NotFound();
            }

            return Ok(postMessagesTable);
        }

        // PUT: api/PostMessagesTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPostMessagesTable(int id, PostMessagesTable postMessagesTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postMessagesTable.Id)
            {
                return BadRequest();
            }

            db.Entry(postMessagesTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostMessagesTableExists(id))
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

        // POST: api/PostMessagesTables
        [ResponseType(typeof(PostMessagesTable))]
        public async Task<IHttpActionResult> PostPostMessagesTable(PostMessagesTable postMessagesTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostMessagesTables.Add(postMessagesTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = postMessagesTable.Id }, postMessagesTable);
        }

        // DELETE: api/PostMessagesTables/5
        [ResponseType(typeof(PostMessagesTable))]
        public async Task<IHttpActionResult> DeletePostMessagesTable(int id)
        {
            PostMessagesTable postMessagesTable = await db.PostMessagesTables.FindAsync(id);
            if (postMessagesTable == null)
            {
                return NotFound();
            }

            db.PostMessagesTables.Remove(postMessagesTable);
            await db.SaveChangesAsync();

            return Ok(postMessagesTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostMessagesTableExists(int id)
        {
            return db.PostMessagesTables.Count(e => e.Id == id) > 0;
        }
    }
}