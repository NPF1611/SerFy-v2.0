using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerFy_v2._0.Models;

namespace SerFy_v2._0.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Photograph,Trailer,sinopse,Rating")] Movie movie, HttpPostedFileBase photo, DateTime date)
        {
            int newID;
            if (db.Movies.Count() == 0)
            {
                newID = 1;

            }
            else
            {
                newID = db.Movies.Max(a => a.ID) + 1;
            }

            string photoName = "MoviePhoto" + newID;
            string pathPhoto = "";

            if (photo == null)
            {
                ModelState.AddModelError("", "Image not found");
                return View(movie);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    movie.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                    //Image image = new Bitmap(photo, 300, 800 ); 

                }
                else
                {
                    ModelState.AddModelError("", "Invalid photo");
                }

            }

            if (date == null)
            {
                ModelState.AddModelError("", "no date defined");
                return View(movie);
            }
            else
            {
                if (date.Day > DateTime.Now.Day && date.Month >= DateTime.Now.Month && date.Year >= DateTime.Now.Year)
                {
                    ModelState.AddModelError("", "Invalid date");
                }
                else
                {
                    movie.dataDePub = date;
                }
            }

            movie.Rating = 0;

            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                photo.SaveAs(pathPhoto);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)


        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Photograph,Trailer,sinopse,Rating")] Movie movie, DateTime date, HttpPostedFileBase photo)
        {

            int NewID = movie.ID;
            string photoName = "";
            string pathPhoto = "";

            if (photo != null) {
                photoName = "MoviePhoto" + movie.ID + ".jpg";
                pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                System.IO.File.Delete(pathPhoto);
                movie.Photograph = photoName;
                photo.SaveAs(pathPhoto);
            }

            if (date != new DateTime(2222,02,01)) 
            {
                if (date.Day > DateTime.Now.Day && date.Month >= DateTime.Now.Month && date.Year >= DateTime.Now.Year)
                {
                    ModelState.AddModelError("", "Invalid date");
                }
                else
                {
                    movie.dataDePub = date;
                }
            }
        
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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
