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
    public class ActorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actors
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {

            var ch = db.Charas.ToList();
            var model = new ViewModelCreateActors();
            model.IdsAllCha = new int[db.Charas.Count()];
            model.ListAllCha = ch;

            return View(model);
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateActors Actor, HttpPostedFileBase photo, DateTime Date, int valueButton)
        {
            var p = db.Charas.ToList();
            Actor.ListAllCha = p;


            Actor.ListAllCha = db.Charas;
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");
            }
            //new actor creation
            Actors newActor = new Actors();
            //define the movie ID
            int newID;
            if (db.Actors.Count() == 0)
            {
                newID = 1;
            }
            else
            {
                newID = db.Actors.Max(a => a.ID) + 1;
            }
            newActor.ID = newID;

            //List inicialization 
            var chr = db.Charas.ToList();
            Actor.ListCha = chr;

            //Name verification
            if (Actor.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(Actor);

            }

            //define the photo path
            string photoName = "ActorPhoto" + newID;
            string pathPhoto = "";


            if (photo == null)
            {
                ModelState.AddModelError("", "Image not found");
                return View(Actor);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    newActor.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Atores/"), photoName);

                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");

                }

            }
            //Date validation
            if (Date < DateTime.Now)
            {
                newActor.BD = Date;
            }
            else
            {
                ModelState.AddModelError("", "Invalid Date");
                return View(Actor);
            }
            //name and bio attribution
            newActor.Name = Actor.Name;
            newActor.Minibio = Actor.Minibio;
            //CharacerList Validation/attribution
            if (Actor.IdsCha == null)
            {
                ModelState.AddModelError("", "No characters selected");
                return View(Actor);
            }

            if (Actor.IdsCha.Length > 1)
            {
                ModelState.AddModelError("", "YOU CAN ONLY SELECT ONE CHARACTER");
                return View(Actor);
            }

            var CharacterAux = new List<Characters> { };
            foreach (var charac in Actor.IdsCha)
            {
                Characters ch1 = db.Charas.Find(charac);
                CharacterAux.Add(ch1);
            }
            newActor.CharacterList = CharacterAux;


            //ModelState
            if (ModelState.IsValid)
            {
                db.Actors.Add(newActor);
                db.SaveChanges();
                photo.SaveAs(pathPhoto);
                return RedirectToAction("Index");
            }

            return View(Actor);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            var model = new ViewModelEditActors();
            model.IdValue = actors.ID;
            model.Name = actors.Name;
            model.Minibio = actors.Minibio;
            model.BD = actors.BD;
            model.Photograph = actors.Photograph;
            model.IdsAllCha = new int[db.Charas.Count()];
            model.ListAllCha = db.Charas.ToList();

            int i = 0;
            var charactersAux = new List<Characters> { };
            model.IdsCha = new int[actors.CharacterList.Count()];
            foreach (var p in actors.CharacterList)
            {
                model.IdsCha[i] = p.ID;
                charactersAux.Add(p);
                i++;
            }
            model.ListCha = charactersAux;


            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelEditActors actors, HttpPostedFileBase photo, DateTime date, String oldphoto, int valueButton)
        {
            //get the actor
            var newActor = db.Actors.Find(actors.IdValue);
            //--photo confirmations
            //photo name variable

            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");
            }

            string photoName = "";
            //photo path variable
            string pathPhoto = "";

            //photo validations and names
            if (photo != null)
            {
                photoName = "MoviePhoto" + actors.IdValue + ".jpg";
                pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                System.IO.File.Delete(pathPhoto);
                actors.Photograph = photoName;
                photo.SaveAs(pathPhoto);
            }
            else
            {


                actors.Photograph = oldphoto;

            }

            //date confirmations
            if (date > DateTime.Now)
            {
                ModelState.AddModelError("", "Invalid date");
            }
            else
            {
                actors.BD = date;
            }
            //List confirmation

            //get and define the char
            //------------------Put the ModelState
            //remove data
            foreach (var AllCharacters in actors.IdsAllCha)
            {
                Characters charac = db.Charas.Find(AllCharacters);
                if (newActor.CharacterList.Contains(charac))
                {
                    newActor.CharacterList.Remove(charac);
                }
            }
            //add data
            foreach (var ch in actors.IdsCha.ToList())
            {
                Characters charac = db.Charas.Find(ch);
                if (!newActor.CharacterList.Contains(charac))
                {
                    newActor.CharacterList.Add(charac);
                }
            }

            //ACTORS entries
            newActor.BD = actors.BD;
            newActor.ID = actors.IdValue;
            newActor.Minibio = actors.Minibio;
            newActor.Name = actors.Name;
            newActor.Photograph = actors.Photograph;
           

            if (ModelState.IsValid)
            {
                db.Entry(newActor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actors);
        }

        // GET: Actors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actors actors = db.Actors.Find(id);
            actors.CharacterList = new List<Characters> { };
            db.Actors.Remove(actors);
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
