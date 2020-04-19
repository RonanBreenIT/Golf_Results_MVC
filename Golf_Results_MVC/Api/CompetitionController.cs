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
    [RoutePrefix("api/Comp")]
    public class CompetitionController : ApiController
    {
        private GolfContext db = new GolfContext();

        public CompetitionController()
        {
            db.Configuration.ProxyCreationEnabled = false; // make sure to use as had Seralisation error before - see https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "user, admin")] // Authorise user and admin to read 
        public IQueryable<Competition> GetCompetitions()
        {
            return db.Competitions;
        }

        [HttpGet]
        [Route("GetComp/{id}")]
        [Authorize(Roles = "user, admin")]
        [ResponseType(typeof(Competition))]
        public IHttpActionResult GetComp(int id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return NotFound();
            }

            return Ok(competition);
        }

        [HttpPut]
        [Route("PutComp/{id}")]
        [Authorize(Roles = "admin")] // Authorise admin only to create, update, delete
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComp(int id, Competition competition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competition.ID)
            {
                return BadRequest("No Competition exists with that ID");
            }

            db.Entry(competition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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
        [Route("PostComp", Name = "AddCompetition")]
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Competition))]
        public IHttpActionResult PostComp(Competition competition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundName = db.Competitions.FirstOrDefault(i => i.Name == competition.Name);

            if (foundName != null) 
            {
                return BadRequest("Competition with that Name already exists");
            }

            db.Competitions.Add(competition);
            db.SaveChanges();

            //return Ok();
            return CreatedAtRoute("AddCompetition", new { id = competition.ID }, competition);
        }

        [HttpDelete]
        [Route("DeleteComp/{id}")]
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Competition))]
        public IHttpActionResult DeleteComp(int id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return NotFound();
            }

            db.Competitions.Remove(competition);
            db.SaveChanges();

            return Ok(competition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetitionExists(int id)
        {
            return db.Competitions.Count(e => e.ID == id) > 0;
        }
    }
}