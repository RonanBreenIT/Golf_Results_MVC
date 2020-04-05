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
    public class CompetitionController : ApiController
    {
        private GolfContext db = new GolfContext();

        public CompetitionController()
        {
            db.Configuration.ProxyCreationEnabled = false; // make sure to use as had Seralisation error before - see https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
        }

        // GET: api/Competition
        public IQueryable<Competition> GetCompetitions()
        {
            return db.Competitions;
        }

        // GET: api/Competition/5
        [ResponseType(typeof(Competition))]
        public IHttpActionResult GetCompetition(int id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return NotFound();
            }

            return Ok(competition);
        }

        // PUT: api/Competition/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetition(int id, Competition competition)
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

        // POST: api/Competition
        [ResponseType(typeof(Competition))]
        public IHttpActionResult PostCompetition(Competition competition)
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

            return CreatedAtRoute("DefaultApi", new { id = competition.ID }, competition);
        }

        // DELETE: api/Competition/5
        [ResponseType(typeof(Competition))]
        public IHttpActionResult DeleteCompetition(int id)
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