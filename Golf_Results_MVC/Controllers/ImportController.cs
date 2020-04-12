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
    public class ImportController : Controller
    {

        private GolfContext db = new GolfContext();

        // GET: Import
        public ActionResult UploadGolfers()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing Golfers 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
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
                                //ID = int.Parse(rows[0].ToString()),
                                Firstname = rows[0].ToString(),
                                Surname = rows[1].ToString(),
                                //FullName = int.Parse(rows[3].ToString())
                            });
                        }
                    }
                    
                    foreach (Golfer golfer in golfers.ToList())
                    {
                        var foundFName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname);
                        var foundSName = db.Golfers.FirstOrDefault(i => i.Surname == golfer.Surname);
                        
                        if ((foundFName != null) && (foundSName != null))
                        {
                            golfers.Remove(golfer);
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
        public ActionResult UploadComps()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing Golfers 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
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
    }
}
