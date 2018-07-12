using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerFy_v2._0.Models;

namespace SerFy_v2._0.Controllers
{
    public class DirectorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Directors
        public ActionResult Index()
        {
            return View(db.Directores.ToList());
        }

        // GET: Directors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directores.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // GET: Directors/Create
        public ActionResult Create()
        {

            var newDirector = new ViewModelCreateDirector();
            newDirector.MovieAllFK = new int[db.Movies.Count()];
            newDirector.MovieAllList = db.Movies.ToList();

            return View(newDirector);
        }

        // POST: Directors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateDirector director, HttpPostedFileBase photo, DateTime date, int valueButton)
        {
            var newDir = new Director();

            director.MovieAllFK =new int[db.Movies.Count()];
            director.MovieAllList = db.Movies.ToList();
            
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Movies");
            }
            
            //define the Director ID
            int newID;
            if (db.Directores.Count() == 0)
            {
                newID = 1;
            }
            else
            {
                newID = db.Directores.Max(a => a.ID) + 1;
            }
            newDir.ID = newID;

           

            //Name verification
            if (director.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(director);

            }

            //define the photo path
            string photoName = "DirectorPhoto" + newID;
            string pathPhoto = "";


            if (photo == null)
            {
                ModelState.AddModelError("", "Image not found");
                return View(director);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    newDir.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Directores/"), photoName);

                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");

                }

            }
            //Date validation
            if (date < DateTime.Now)
            {
                newDir.Place_BD = date;
            }
            else
            {
                ModelState.AddModelError("", "Invalid Date");
                return View(director);
            }
            //name and bio attribution
            newDir.Name = director.Name;
            newDir.MiniBio = director.MiniBio;
            //Movies List  Validation/attribution
            if (director.MovieFK == null)
            {
                ModelState.AddModelError("", "No Movie selected");
                return View(director);
            }
            //add a movie
            newDir.MovieList = new List<Movie> { };

            foreach (var mov in director.MovieFK.ToList())
            {
                Movie movie = db.Movies.Find(mov);

                newDir.MovieList.Add(movie);
            }

            if (ModelState.IsValid)
            {
                db.Directores.Add(newDir);
                db.SaveChanges();
                photo.SaveAs(pathPhoto);
                return RedirectToAction("Index");
            }

            return View(director);
        }

        // GET: Directors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directores.Find(id);
             if (director == null)
            {
                return HttpNotFound();
            }

            var newDir = new ViewModelEditDirector();
            newDir.IDValue = director.ID;
            newDir.MiniBio = director.MiniBio;
            newDir.Name = director.Name;
            newDir.Photograph = director.Photograph;
            newDir.Place_BD = director.Place_BD;
            newDir.MovieAllFK = new int[db.Movies.Count()];
            newDir.MovieAllList = db.Movies.ToList();


            int i = 0;
            var dirAux = new List<Movie> { };
            newDir.MovieFK = new int[director.MovieList.Count()];
            foreach (var p in director.MovieList)
            {
                newDir.MovieFK[i] = p.ID;
                dirAux.Add(p);
                i++;
            }
            newDir.MovieList = dirAux;
            
            return View(newDir);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelEditDirector director, String oldphoto, HttpPostedFileBase photo, DateTime date, int valueButton)
        {
            //get the director
            Director dir = db.Directores.Find(director.IDValue);
            
            //button verification
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Movies");
            }
            //--photo confirmations
            //photo name variable
            string photoName = "";
            //photo path variable
            string pathPhoto = "";
            //photo validations and names


            if (photo != null)
            {
                photoName = "ActorPhoto" + director.IDValue + ".jpg";
                pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Directores/"), photoName);
                System.IO.File.Delete(pathPhoto);
                director.Photograph = photoName;
                photo.SaveAs(pathPhoto);
            }
            else
            {


                director.Photograph = oldphoto;

            }

            //date confirmations
            if (date > DateTime.Now)
            {
                ModelState.AddModelError("", "Invalid date");
            }
            else
            {
                director.Place_BD = date;
            }
            //List confirmation
            //get and define the Movies
            //------------------Put the ModelState
            foreach (var allmov in director.MovieAllFK)
            {
                Movie m = db.Movies.Find(allmov);
                if (dir.MovieList.Contains(m))
                {
                    dir.MovieList.Remove(m);
                }
            }
            //add data
            foreach (var mov in director.MovieFK.ToList())
            {
                Movie movie = db.Movies.Find(mov);
                if (!dir.MovieList.Contains(movie))
                {
                    dir.MovieList.Add(movie);
                }
            }
            //Director entries
            dir.Name = director.Name;
            dir.MiniBio = director.MiniBio;
            dir.ID = director.IDValue;
            dir.Photograph = director.Photograph;
            dir.Place_BD = director.Place_BD;


            if (ModelState.IsValid)
            {
                db.Entry(dir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(director);
        }
      
        

        // GET: Directors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directores.Find(id);
            
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = db.Directores.Find(id);
            director.MovieList = new List<Movie> { };
            foreach (var ch in director.MovieList.ToList())
            {
                director.MovieList.Remove(ch);
            }


            db.Directores.Remove(director);
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
