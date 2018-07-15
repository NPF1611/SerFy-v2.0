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
            //create a new Director
            var newDir = new Director();
            
            //give the director the ingo for modelStates
            director.MovieAllFK =new int[db.Movies.Count()];
            director.MovieAllList = db.Movies.ToList();
            
            //button to redirect 
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

           

            //Name verification //not necessary 
            if (director.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(director);

            }

            //define the photo Name
            string photoName = "DirectorPhoto" + newID;
            //will have the photo path
            string pathPhoto = "";


            if (photo == null)
            {
                //if fot is null then
                ModelState.AddModelError("", "Image not found");
                return View(director);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    //if photo is jpg than save it and « add it to the model 
                    photoName = photoName + ".jpg";
                    newDir.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Directores/"), photoName);

                }
                else
                {
                    //if the photo is invalid
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(director);

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

            //name attribution
            newDir.Name = director.Name; 

            //bio attribution
            newDir.MiniBio = director.MiniBio;

            //Movies List  Validation/attribution
            if (director.MovieFK == null)
            {
                //director can be created without a Movie
                director.MovieFK = new int[0];
            }

            //----add movies
            //add an empty List to the model
            newDir.MovieList = new List<Movie> { };

            //each movie selected goes into the Movie Lisst
            foreach (var mov in director.MovieFK.ToList())
            {
                Movie movie = db.Movies.Find(mov);

                newDir.MovieList.Add(movie);
            }

            if (ModelState.IsValid)
            {
                db.Directores.Add(newDir);
                db.SaveChanges();
                //saves the photo
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
            //finds the director
            Director director = db.Directores.Find(id);
             if (director == null)
            {
                return HttpNotFound();
            }
             //creates a new Model
            var newDir = new ViewModelEditDirector();
            //values definition----
            newDir.IDValue = director.ID;
            newDir.MiniBio = director.MiniBio;
            newDir.Name = director.Name;
            newDir.Photograph = director.Photograph;
            newDir.Place_BD = director.Place_BD;
            //MovieFk will be the array with all the Movies ids 
            newDir.MovieAllFK = new int[db.Movies.Count()];
            //MovirAllList List will have the database
            newDir.MovieAllList = db.Movies.ToList();

            //i will be an auxiliar int
            int i = 0;
            //dirAux is an auviliar List<Movie>
            var dirAux = new List<Movie> { };
            //MovieFK
            newDir.MovieFK = new int[director.MovieList.Count()];
            //each director will have the selected movies
            foreach (var p in director.MovieList)
            {
                newDir.MovieFK[i] = p.ID;
                dirAux.Add(p);
                i++;
            }
            //Movie Lis is equals to the auxiliar var
            newDir.MovieList = dirAux;
            
            //retuins the model
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
            director.MovieAllList = db.Movies.ToList();
            director.MovieList = dir.MovieList;


            
            //button verification
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Movies");
            }
            //--photo confirmations
            //photo name variable
            string photoName = "DirectorPhoto" + dir.ID;
            //photo path variable
            string pathPhoto = "";
            //photo validations and names
            
            /*This will verify the photo this one needs to be jpg and not null*/
            if (photo == null)
            {
                
                director.Photograph = oldphoto;

            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = "ActorPhoto" + director.IDValue + ".jpg";
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Directores/"), photoName);
                    System.IO.File.Delete(pathPhoto);
                    director.Photograph = photoName;
                    photo.SaveAs(pathPhoto);

                }
                else
                {
                    director.Place_BD = dir.Place_BD;
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(director);
                }

            }
            

            //date confirmations
            if (date > DateTime.Now)
            {
                //if date is invalid
                director.Place_BD = dir.Place_BD;
                ModelState.AddModelError("", "Invalid date");
                return View(director);
            }
            else
            {
                //if date is valid
                director.Place_BD = date;
            }

            //List confirmation
            if (director.MovieFK == null)
            {
                int i = 0;
                director.MovieFK = new int[dir.MovieList.Count()];
                foreach (var mov in dir.MovieList) {
                    director.MovieFK[i] = mov.ID;
                    i++;
                }
                ModelState.AddModelError("","No Movie Selected");
                return View(director);

            }

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
