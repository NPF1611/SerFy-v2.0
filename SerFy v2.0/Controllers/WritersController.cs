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
    public class WritersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Writers
        public ActionResult Index()
        {
            return View(db.Writers.ToList());
        }

        // GET: Writers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        // GET: Writers/Create
        public ActionResult Create()
        {
            //create a new Model
            var newWriter = new ViewModelCreateWriter();
            //get the Fks ids
            newWriter.MovieAllFK = new int[db.Movies.Count()];
            //get the objects
            newWriter.MovieAllList = db.Movies.ToList();

            return View(newWriter);
        }

        // POST: Writers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateWriter writer, HttpPostedFileBase photo, DateTime date, int valueButton)
        {
            var newWriter = new Writer();

            writer.MovieAllFK = new int[db.Movies.Count()];
            writer.MovieAllList = db.Movies.ToList();

            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Movies");
            }

            //define the writer ID
            int newID;
            if (db.Writers.Count() == 0)
            {
                newID = 1;
            }
            else
            {
                newID = db.Writers.Max(a => a.ID) + 1;
            }
            newWriter.ID = newID;



            //Name verification
            if (writer.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(writer);

            }

            //define the photo path
            string photoName = "WriterPhoto" + newID;
            string pathPhoto = "";


            if (photo == null)
            {
                ModelState.AddModelError("", "Image not found");
                return View(writer);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    newWriter.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Writers/"), photoName);

                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");
                    return View(writer);

                }

            }
            //Date validation
            if (date < DateTime.Now)
            {
                newWriter.Place_DB = date;
            }
            else
            {
                ModelState.AddModelError("", "Invalid Date");
                return View(writer);
            }
            //name and bio attribution
            newWriter.Name = writer.Name;
            newWriter.MiniBio = writer.MiniBio;
            //Movies List  Validation/attribution
            if (writer.MovieFK == null)
            {
               //writer can be created without a Movie
                writer.MovieFK = new int[0];
            }
            //add a movie
            newWriter.MoviesList = new List<Movie> { };
            //each movie selected will be added into the MovieList
            foreach (var mov in writer.MovieFK.ToList())
            {
                Movie movie = db.Movies.Find(mov);

                newWriter.MoviesList.Add(movie);
            }


            if (ModelState.IsValid)
            {
                db.Writers.Add(newWriter);
                db.SaveChanges();
                //saves the photo
                photo.SaveAs(pathPhoto);
                return RedirectToAction("Index");
            }

            return View(writer);
        }

        // GET: Writers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            //create a new Model
            var newWr = new ViewModelEditWriter();
            //give the writer calues to the model
            newWr.IDValue = writer.ID;
            newWr.MiniBio = writer.MiniBio;
            newWr.Name = writer.Name;
            newWr.Photograph = writer.Photograph;
            newWr.Place_BD = writer.Place_DB;
            //give the length and all the Moves database  List
            newWr.MovieAllFK = new int[db.Movies.Count()];
            newWr.MovieAllList = db.Movies.ToList();

            //get the defined List values
            int i = 0;
            var WRAux = new List<Movie> { };
            newWr.MovieFK = new int[writer.MoviesList.Count()];
            foreach (var p in writer.MoviesList)
            {
                newWr.MovieFK[i] = p.ID;
                WRAux.Add(p);
                i++;
            }
            newWr.MovieList = WRAux;

            return View(newWr);
        }

        // POST: Writers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelEditWriter writer, String oldphoto, HttpPostedFileBase photo, DateTime date, int valueButton)
        {

            //get the Writer
            Writer wr = db.Writers.Find(writer.IDValue);
            // give the writer values to the model ro Modelstates
            writer.MovieAllList = db.Movies.ToList();
            writer.MovieList = wr.MoviesList;
            //button verification
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Movies");
            }
            //--photo confirmations
            //photo name variable
            string photoName = "WriterPhoto" + wr.ID;
            //photo path variable
            string pathPhoto = "";
          

            //photo validations and names
            if (photo == null)
            {
                writer.Photograph = wr.Photograph;
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    wr.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Writers/"), photoName);

                }
                else
                {
                    writer.Place_BD= wr.Place_DB;
                    ModelState.AddModelError("", "Invalid photo");
                    return View(writer);

                }

            }

            //date confirmations
            if (date > DateTime.Now)
            {
                writer.Place_BD = wr.Place_DB;
                ModelState.AddModelError("", "Invalid date");
                return View(writer);
            }
            else
            {
                writer.Place_BD = date;
            }
            //List confirmation
            if (writer.MovieFK == null)
            {
                int i = 0;
                writer.MovieFK = new int[wr.MoviesList.Count()];
                foreach (var mov in wr.MoviesList)
                {
                    writer.MovieFK[i] = mov.ID;
                    i++;
                }
                ModelState.AddModelError("", "No Movie Selected");
                return View(writer);
            }
                //get and define the Movies
                //------------------Put the ModelState
                foreach (var allmov in writer.MovieAllFK)
            {
                Movie m = db.Movies.Find(allmov);
                if (wr.MoviesList.Contains(m))
                {
                    wr.MoviesList.Remove(m);
                }
            }
            //add data
            foreach (var mov in writer.MovieFK.ToList())
            {
                Movie movie = db.Movies.Find(mov);
                if (!wr.MoviesList.Contains(movie))
                {
                    wr.MoviesList.Add(movie);
                }
            }
            //Director entries
            wr.Name = writer.Name;
            wr.MiniBio = writer.MiniBio;
            wr.ID = writer.IDValue;
            wr.Photograph = writer.Photograph;
            wr.Place_DB = writer.Place_BD;

            if (ModelState.IsValid)
            {
                db.Entry(wr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        // GET: Writers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        // POST: Writers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //find the movie
            Writer writer = db.Writers.Find(id);
            //for each value remove from the list
            writer.MoviesList = new List<Movie> { };
            foreach (var ch in writer.MoviesList.ToList())
            {
                writer.MoviesList.Remove(ch);
            }


            db.Writers.Remove(writer);
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
