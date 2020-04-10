using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Golf_Results_MVC.DAL;
using Golf_Results_MVC.Models;

namespace Golf_Results_MVC.Api
{
    public class GolferController : ApiController
    {
        private GolfContext db = new GolfContext();

        public GolferController()
        {
            db.Configuration.ProxyCreationEnabled = false; // make sure to use as had Seralisation error before - see https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
        }

        public IQueryable<Golfer> GetGolfers()
        {
            return db.Golfers;
        }

        // GET: api/Golfer/5
        [ResponseType(typeof(Golfer))]
        public IHttpActionResult GetGolfer(int id)
        {
            Golfer golfer = db.Golfers.Find(id);
            if (golfer == null)
            {
                return NotFound();
            }

            return Ok(golfer);
        }

        // PUT: api/Golfer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGolfer(int id, Golfer golfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != golfer.ID)
            {
                return BadRequest("Golfer with ID doesn't exist");
            }

            db.Entry(golfer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GolferExists(id))
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

        // POST: api/Golfer
        [ResponseType(typeof(Golfer))]
        public IHttpActionResult PostGolfer(Golfer golfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Note Can't just give Full Name as it is derived
            var foundFName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname);
            var foundSName = db.Golfers.FirstOrDefault(i => i.Surname == golfer.Surname);

            if ((foundFName != null) && (foundSName != null))
            {
                return BadRequest("Golfer with that Name already exists");
            }

            db.Golfers.Add(golfer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = golfer.ID }, golfer);
        }

        // DELETE: api/Golfer/5
        [ResponseType(typeof(Golfer))]
        public IHttpActionResult DeleteGolfer(int id)
        {
            Golfer golfer = db.Golfers.Find(id);
            if (golfer == null)
            {
                return NotFound();
            }

            db.Golfers.Remove(golfer);
            db.SaveChanges();

            return Ok(golfer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GolferExists(int id)
        {
            return db.Golfers.Count(e => e.ID == id) > 0;
        }
    }
}