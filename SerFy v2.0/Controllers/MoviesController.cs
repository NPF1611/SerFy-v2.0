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
            var ch = db.Charas.ToList();
            var model = new ViewModelCreateFilmePerso();
            model.idsCharacters = new int[db.Charas.Count()];
            model.Listcharacters = ch;

            var dr = db.Directores.ToList();
            model.idsCharacters = new int[db.Directores.Count()];
            model.ListDirectors = dr;

            var wr = db.Writers.ToList();
            model.idsWriters = new int[db.Writers.Count()];
            model.ListWriters = wr;

            return View(model);
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateFilmePerso movie, HttpPostedFileBase photo, DateTime date)
        {
            //criar um filme
            Movie newmovie = new Movie();
            //define the movie ID
            int newID;
            if (db.Movies.Count() == 0)
            {
                newID = 1;

            }
            else
            {
                newID = db.Movies.Max(a => a.ID) + 1;
            }

            newmovie.ID = newID;

            //define the photo path
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
                    newmovie.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                    //Image image = new Bitmap(photo, 300, 800 ); 

                }
                else
                {
                    ModelState.AddModelError("", "Invalid photo");
                }

            }
            //define the date
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
                    newmovie.dataDePub = date;
                }
            }
            //change the trailer
            newmovie.Trailer = movie.Trailer.Replace("/watch?v=", "/embed/");

            //define movie rating
            newmovie.Rating = 0;

            newmovie.CharactersList = new List<Characters> { };

            foreach (var ch in movie.idsCharacters.ToList())
            {
                Characters charac = db.Charas.Find(ch);

                newmovie.CharactersList.Add(charac);
            }

            newmovie.DirectorList = new List<Director> { };

            foreach (var ch in movie.idsDirectores.ToList())
            {
                Director dir = db.Directores.Find(ch);

                newmovie.DirectorList.Add(dir);
            }

            newmovie.WriterList = new List<Writer> { };

            foreach (var ch in movie.idsWriters.ToList())
            {
                Writer writer = db.Writers.Find(ch);


                newmovie.WriterList.Add(writer);
            }

            newmovie.Comments = new List<Comment> { };
            newmovie.Rates = new List<Rate> { };
            newmovie.sinopse = movie.sinopse;

            newmovie.Name = movie.Name;


            if (ModelState.IsValid)
            {
                db.Movies.Add(newmovie);
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
        public ActionResult Edit( Movie movie, DateTime date, HttpPostedFileBase photo)
        {

            int NewID = movie.ID;
            string photoName = "";
            string pathPhoto = "";

            if (photo != null)
            {
                photoName = "MoviePhoto" + movie.ID + ".jpg";
                pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                System.IO.File.Delete(pathPhoto);
                movie.Photograph = photoName;
                photo.SaveAs(pathPhoto);
            }

            if (date != new DateTime(2222, 02, 01))
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
            else
            {
                movie.dataDePub = movie.dataDePub;


            }

            movie.Trailer = movie.Trailer.Replace("/watch?v=", "/embed/");

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

            movie.CharactersList = new List<Characters> { };
            foreach (var ch in movie.CharactersList.ToList()) {
                movie.CharactersList.Remove(ch);
            }

            movie.DirectorList = new List<Director> { };
            foreach (var dr in movie.DirectorList.ToList())
            {
                movie.DirectorList.Remove(dr);
            }

            movie.WriterList = new List<Writer> { };
            foreach (var wr in movie.WriterList.ToList())
            {
                movie.WriterList.Remove(wr);
            }

            movie.Comments = new List<Comment> { };
            foreach (var cm in movie.Comments.ToList())
            {
                movie.Comments.Remove(cm);
            }
            movie.Rates = new List<Rate> { };
            foreach (var rt in movie.Rates.ToList())
            {
                movie.Rates.Remove(rt);
            }

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Movies/Delete/Characters
        public ActionResult ListadePersonagens()
        {

            return PartialView(db.Charas.ToList());

        }

        // GET: Movies/Atores
        public ActionResult ListDirectors()
        {

            return PartialView(db.Directores.ToList());

        }
        // GET: Movies/Atores
        public ActionResult ListWriters()
        {

            return PartialView(db.Writers.ToList());

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
