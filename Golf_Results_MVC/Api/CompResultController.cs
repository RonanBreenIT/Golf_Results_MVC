﻿using System;
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
    [RoutePrefix("api/CompResult")]
    public class CompResultController : ApiController
    {
        private GolfContext db = new GolfContext();

        public CompResultController()
        {
            db.Configuration.ProxyCreationEnabled = false; // make sure to use as had Seralisation error before - see https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
        }

        [HttpGet]
        [Route("all")]
        public IQueryable<Comp_Result> GetComp_Results()
        {
            return db.Comp_Results;
        }

        [HttpGet]
        [Route("GetCompResultID/{id}")]
        [ResponseType(typeof(Comp_Result))]
        public IHttpActionResult GetCompResultID(int id)
        {
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            if (comp_Result == null)
            {
                return NotFound();
            }

            return Ok(comp_Result);
        }

        [HttpGet]
        [Route("GetCompID/{compID}")]
        [ResponseType(typeof(Comp_Result))]
        public IHttpActionResult GetCompID(int compID)
        {
            var comp_Result = db.Comp_Results.Where(i => i.CompetitionID == compID);
            if (comp_Result == null)
            {
                return NotFound();
            }

            return Ok(comp_Result);
        }

        [HttpGet]
        [Route("GetCompResultPerSeason/{compID}/{season}")]
        [ResponseType(typeof(Comp_Result))]
        public IHttpActionResult GetCompResultPerSeason(int compID, int season)
        {
            var comp_Result = from i in db.Comp_Results
                              where i.CompetitionID == compID
                              where i.Season == season
                              select i;

            if (comp_Result == null)
            {
                return NotFound();
            }

            return Ok(comp_Result);
        }

        [HttpPut]
        [Route("PutComp_Result/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComp_Result(int id, Comp_Result comp_Result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comp_Result.CompResultID)
            {
                return BadRequest("Comp Result with that ID doesn't Exist");
            }

            db.Entry(comp_Result).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Comp_ResultExists(id))
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
        [Route("PostComp_Result")]
        [ResponseType(typeof(Comp_Result))]
        public IHttpActionResult PostComp_Result(Comp_Result comp_Result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundComp = from i in db.Comp_Results
                              where i.CompetitionID == comp_Result.CompetitionID
                              where i.Season == comp_Result.Season
                              select i;

            if (foundComp != null)
            {
                return BadRequest("Competition Results for that Comp ID and Year already exists");
            }

            db.Comp_Results.Add(comp_Result);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comp_Result.CompResultID }, comp_Result);
        }

        [HttpDelete]
        [Route("DeleteComp_Result/{id}")]
        [ResponseType(typeof(Comp_Result))]
        public IHttpActionResult DeleteComp_Result(int id)
        {
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            if (comp_Result == null)
            {
                return NotFound();
            }

            db.Comp_Results.Remove(comp_Result);
            db.SaveChanges();

            return Ok(comp_Result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Comp_ResultExists(int id)
        {
            return db.Comp_Results.Count(e => e.CompResultID == id) > 0;
        }
    }
}