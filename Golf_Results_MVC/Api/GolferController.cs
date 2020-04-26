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
using Golf_Results_MVC.Providers;

namespace Golf_Results_MVC.Api
{
    [RoutePrefix("api/Golfer")]
    public class GolferController : ApiController
    {
         //*** Removing just whilst need unit tests - comment back in After***
        private GolfContext db = new GolfContext();

        public GolferController()
        {
            db.Configuration.ProxyCreationEnabled = false; // make sure to use as had Seralisation error before - see https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
        }

        //// adding for unit tests
        //// modify the type of the db field
        //private IGolfContext db = new GolfContext();

        //// add these constructors
        //public GolferController() { }

        //public GolferController(IGolfContext context)
        //{
        //    db = context;
        //}

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "user, admin")]
        public IQueryable<Golfer> GetGolfers()
        {
            return db.Golfers;
        }

        [HttpGet]
        [Route("GetGolfer/{id}")]
        [Authorize(Roles = "user, admin")]
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

        [HttpPut]
        [Route("PutGolfer/{id}")]
        [Authorize(Roles = "admin")]
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

            // commented line for unit tests and added the mark as modified, reverse before deploying
            db.Entry(golfer).State = EntityState.Modified;
            //db.MarkAsModified(golfer);
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

        [HttpPost]
        [Route("PostGolfer", Name = "AddGolfer")]
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Golfer))]
        public IHttpActionResult PostGolfer(Golfer golfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Note Can't just give Full Name as it is derived
            //var foundFName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname);
            //var foundSName = db.Golfers.FirstOrDefault(i => i.Surname == golfer.Surname);
            var foundName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname && i.Surname == golfer.Surname);

            if (foundName != null) 
            {
                return BadRequest("Golfer with that Name already exists");
            }

            db.Golfers.Add(golfer);
            db.SaveChanges();

            return CreatedAtRoute("AddGolfer", new { id = golfer.ID }, golfer);
        }

        [HttpDelete]
        [Route("DeleteGolfer/{id}")]
        [Authorize(Roles = "admin")]
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