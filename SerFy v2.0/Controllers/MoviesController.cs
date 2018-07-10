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
            model.idsDirectores = new int[db.Directores.Count()];
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
            //new movie creation
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
            
            //Lists inicialization 
            var chr = db.Charas.ToList();
            movie.Listcharacters = chr;

            var drs = db.Directores.ToList();
            movie.ListDirectors = drs;

            var wrs = db.Writers.ToList();
            movie.ListWriters = wrs;

            //Name verification
            if (movie.Name == null) {
                ModelState.AddModelError("", "Name not found");

            }
            //
            
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
                   
                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");

                }

            }
            //see if the fks are selected

            if (movie.idsCharacters == null){
                movie.idsCharacters = new int[0];
                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];
                ModelState.AddModelError("","No Characters selected ");
                return View(movie);
            }

            if (movie.idsDirectores == null)
            {

                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];

                ModelState.AddModelError("", "No Directors selected ");
                return View(movie);
            }

            if (movie.idsWriters == null)
            {
                
                movie.idsWriters = new int[0];

                ModelState.AddModelError("", "No Writers selected ");
                return View(movie);
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

            var model = new viewModelEditFilme();

            var ch = db.Charas.ToList();
            model.idsAllCharacters = new int[db.Charas.Count()];
            model.ListAllcharacters = ch;

            var dr = db.Directores.ToList();
            model.idsAllCharacters = new int[db.Directores.Count()];
            model.ListAllDirectors = dr;

            var wr = db.Writers.ToList();
            model.idsAllWriters = new int[db.Writers.Count()];
            model.ListAllWriters = wr;


            //Movie{id} characters
            int i = 0;
            var charactersAux = new List<Characters> { };
            model.idsCharacters = new int[movie.CharactersList.Count()];
            foreach (var p in movie.CharactersList)
            {
                model.idsCharacters[i] = p.ID;
                charactersAux.Add(p);
                i++;
            }
            model.Listcharacters = charactersAux;

            //Movie{id} Directors
            i = 0;
            var directorsAux = new List<Director> { };
            model.idsDirectores = new int[movie.DirectorList.Count()];
            foreach (var d in movie.DirectorList)
            {
                model.idsDirectores[i] = d.ID;
                directorsAux.Add(d);
                i++;
            }
            model.ListDirectors = directorsAux;

            //Movie{id} Writters
            i = 0;
            var wrtersAux = new List<Writer> { };
            model.idsWriters = new int[movie.WriterList.Count()];
            foreach (var w in movie.WriterList)
            {
                model.idsWriters[i] = w.ID;
                wrtersAux.Add(w);
                i++;
            }
            model.ListWriters = wrtersAux;

            //Movie{id} Comments 
            i = 0;
            var CommentsAux = new List<Comment> { };
            model.idsComments = new int[movie.Comments.Count()];
            foreach (var cm in movie.Comments)
            {
                model.idsWriters[i] = cm.ID;
                CommentsAux.Add(cm);
                i++;
            }
            model.ListComments = CommentsAux;

            //Movie{id} Comments 
            i = 0;
            var RateAux = new List<Rate> { };
            model.idsComments = new int[movie.Comments.Count()];
            foreach (var rt in movie.Rates)
            {
                model.idsRates[i] = rt.ID;
                RateAux.Add(rt);
                i++;
            }
            model.ListRates = RateAux;



            model.IDvalue = movie.ID;
            model.ListDirectors = movie.DirectorList;
            model.ListWriters = movie.WriterList;
            model.Name = movie.Name;
            model.dataDePub = movie.dataDePub;
            model.Photograph = movie.Photograph;
            model.Trailer = movie.Trailer;
            model.sinopse = movie.sinopse;
            model.Rating = movie.Rating;





            return View(model);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(viewModelEditFilme movie, DateTime date, HttpPostedFileBase photo,String oldphoto)
        {

            var moveitoenter = db.Movies.Find(movie.IDvalue);
            //photo confirmations
            string photoName = "";
            string pathPhoto = "";
            //All ids are full here
            //movie.idsAllCharacters = new int[db.Charas.Count()];
            //movie.idsAllDirectores = new int[db.Directores.Count()];
            //movie.idsAllWriters = new int[db.Writers.Count()];
            //movie.idsAllComments = new int[db.Comments.Count()];
            //movie.idsAllRates = new int[db.Rates.Count()];


            if (photo != null)
            {
                photoName = "MoviePhoto" + movie.IDvalue + ".jpg";
                pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                System.IO.File.Delete(pathPhoto);
                movie.Photograph = photoName;
                photo.SaveAs(pathPhoto);
            }
            else {

                
                movie.Photograph = oldphoto;

            }
            //date confirmations
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
            //trailer changes 
            movie.Trailer = movie.Trailer.Replace("/watch?v=", "/embed/");

            //Lists

            //--Characters-remove
            foreach (var AllCharacters in movie.idsAllCharacters){
                Characters charac = db.Charas.Find(AllCharacters);
                if (moveitoenter.CharactersList.Contains(charac))
                {
                    moveitoenter.CharactersList.Remove(charac);
                }
            }

            //--Characters-add
            foreach (var ch in movie.idsCharacters.ToList())
            {
                Characters charac = db.Charas.Find(ch);
                if (!moveitoenter.CharactersList.Contains(charac))
                {
                    moveitoenter.CharactersList.Add(charac);
                }
            }

            //--Directors-Remove
            foreach (var AllDirectors in movie.idsAllDirectores.ToList())
            {
                Director dir = db.Directores.Find(AllDirectors);
                if (moveitoenter.DirectorList.Contains(dir))
                {
                    moveitoenter.DirectorList.Remove(dir);
                }
            }

            

            //--Directors-Add
            foreach (var dir in movie.idsDirectores.ToList())
            {
                Director direc = db.Directores.Find(dir);
                if (!moveitoenter.DirectorList.Contains(direc))
                {
                    moveitoenter.DirectorList.Add(direc);
                }
            }
            //--Writters-Remove
            foreach (var AllWriters in movie.idsAllWriters.ToList())
            {
                Writer writ = db.Writers.Find(AllWriters);
                if (moveitoenter.WriterList.Contains(writ))
                {
                    moveitoenter.WriterList.Remove(writ);
                }
            }


            //--Writters-Add
            foreach (var wrt in movie.idsWriters.ToList())
            {
                Writer writer = db.Writers.Find(wrt);
                if (!moveitoenter.WriterList.Contains(writer))
                {
                    moveitoenter.WriterList.Add(writer);
                }
            }

            //--Comments
            if (movie.idsComments != null)
            {
                var commentsAux = new List<Comment> { };
                foreach (var cm in movie.idsComments.ToList())
                {
                    Comment com = db.Comments.Find(cm);
                    commentsAux.Add(com);
                }
                moveitoenter.Comments = commentsAux;
            }
            //rate
            if (movie.idsRates != null)
            {
                var rateAux = new List<Rate> { };
                foreach (var rt in movie.idsRates.ToList())
                {
                    Rate rat = db.Rates.Find(rt);
                    rateAux.Add(rat);
                }
                moveitoenter.Rates = rateAux;
            }

            //MOVIE entries
            moveitoenter.ID = movie.IDvalue;
            moveitoenter.Name = movie.Name;
            moveitoenter.Photograph = movie.Photograph;
            moveitoenter.Trailer = movie.Trailer;
            moveitoenter.sinopse = movie.sinopse;
            moveitoenter.dataDePub = movie.dataDePub;
            moveitoenter.Rating = movie.Rating;

            if (moveitoenter.Comments == null)
            {
                moveitoenter.Comments = new List<Comment> { };
            }
            if (moveitoenter.Rates == null)
            {
                moveitoenter.Rates = new List<Rate> { };
            }

            
            

            if (ModelState.IsValid)
            {
                db.Entry(moveitoenter).State = EntityState.Modified;

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
            foreach (var ch in movie.CharactersList.ToList())
            {
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
                var elC = db.Comments.Find(cm.ID);
                db.Comments.Remove(elC);
                movie.Comments.Remove(cm);
            }
            movie.Rates = new List<Rate> { };
            foreach (var rt in movie.Rates.ToList())
            {
                var elR = db.Rates.Find(rt.ID);
                db.Rates.Remove(elR);
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
