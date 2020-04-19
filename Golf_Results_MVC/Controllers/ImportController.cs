using Golf_Results_MVC.DAL;
using Golf_Results_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Golf_Results_MVC.Controllers
{
    // This controllers allows us to upload bulk CSV's instead of manually entering all details. 
    public class ImportController : Controller
    {

        private GolfContext db = new GolfContext();


        // GET: Import
        [Authorize(Roles = "admin")]
        public ActionResult UploadGolfers()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing Golfers 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UploadGolfers(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var golfers = new List<Golfer>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            golfers.Add(new Golfer
                            {
                                //ID = int.Parse(rows[0].ToString()), // dont need
                                Firstname = rows[0].ToString(),
                                Surname = rows[1].ToString(),
                                //FullName = int.Parse(rows[3].ToString()) // dont need
                            });
                        }
                    }
                    
                    foreach (Golfer golfer in golfers.ToList())
                    {
                        // here we are checking if golfer in our upload list is already in the db
                        var foundFName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname);
                        var foundSName = db.Golfers.FirstOrDefault(i => i.Surname == golfer.Surname);
                        
                        if ((foundFName != null) && (foundSName != null))
                        {
                            golfers.Remove(golfer); //... and if golfer is already in db we just remove from the list before uploading
                        }
                        else
                        {
                            db.Golfers.Add(golfer);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index", "Golfer");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }

        // GET: Import
        [Authorize(Roles = "admin")]
        public ActionResult UploadComps()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing Golfers 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UploadComps(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var comps = new List<Competition>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            comps.Add(new Competition
                            {
                                Name = rows[0].ToString(),
                            });
                        }
                    }

                    foreach (Competition comp in comps.ToList())
                    {
                        var foundName = db.Competitions.FirstOrDefault(i => i.Name == comp.Name);

                        if (foundName != null)
                        {
                            comps.Remove(comp);
                        }
                        else
                        {
                            db.Competitions.Add(comp);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index", "Competition");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }

        // GET: Import
        [Authorize(Roles = "admin")]
        public ActionResult UploadCompResults()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing Golfers 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UploadCompResults(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var results = new List<Comp_Result>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');


                            results.Add(new Comp_Result
                            {
                                CompetitionID = int.Parse(rows[0].ToString()),
                                Season = int.Parse(rows[1].ToString()),
                                StartDate = DateTime.Parse(rows[2].ToString()),
                                EndDate = DateTime.Parse(rows[3].ToString()),
                                GolferID = int.Parse(rows[4].ToString()),
                                Position = rows[5].ToString(),
                                GolferScore = rows[6].ToString(),
                            }); 
                        }
                    }

                    foreach (Comp_Result comp in results.ToList())
                    {    
                        // here we check for record with CompID, Season, GolferID conditions all matching.
                        var foundMatch = db.Comp_Results.Where(x => x.CompetitionID == comp.CompetitionID && x.Season == comp.Season && x.GolferID == comp.GolferID).FirstOrDefault();

                        if (foundMatch != null)
                        {
                            results.Remove(comp);
                        }
                        else
                        {
                            db.Comp_Results.Add(comp);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index", "Competition");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }
    }
}
