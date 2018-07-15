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
            //gets the database characters in List format 
            var ch = db.Charas.ToList();
            //creates a new model to alocate the characters
            var model = new ViewModelCreateActors();
            //gets the array length
            model.IdsAllCha = new int[db.Charas.Count()];
            //allocates the Characters List
            model.ListAllCha = ch;
            //retuens the model 
            return View(model);
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateActors Actor, HttpPostedFileBase photo, DateTime Date, int valueButton)
        {
            //adds the list to the param bc this param will be empty
            var p = db.Charas.ToList();
            Actor.ListAllCha = p;

            // verify the button value to redirect
            Actor.ListAllCha = db.Charas;
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");
            }
            //new actor creation
            Actors newActor = new Actors();
            //define the Actor ID
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

            //Name verification //thas not really necessary tbh
            if (Actor.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(Actor);

            }

            //define the photo name
            string photoName = "ActorPhoto" + newID;
            //will contain the photo Path
            string pathPhoto = "";

            //if the photo is null then 
            if (photo == null)
            {
                //returns the model state
                ModelState.AddModelError("", "Image not found");
                return View(Actor);
            }
            else
            {
                //if the img format is jpg
                if (photo.ContentType == "image/jpeg")
                {
                    //give the photo a name and a Path
                    photoName = photoName + ".jpg";
                    newActor.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Atores/"), photoName);

                }
                else
                {
                    //if not the model state will be returned
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(Actor);

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

            //name attribution
            newActor.Name = Actor.Name;
            
            //MiniBio attribution 
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
            //--Add the characters to the List
            //charactersAux was created to add values
            var CharacterAux = new List<Characters> { };
            foreach (var charac in Actor.IdsCha)
            {
                //ch1 is the character object
                Characters ch1 = db.Charas.Find(charac);
                //adds the ch1 to the Characters aux variable
                CharacterAux.Add(ch1);
            }
            //characterList will receive the characterAux Values
            newActor.CharacterList = CharacterAux;


            //ModelState
            if (ModelState.IsValid)
            {
                db.Actors.Add(newActor);
                db.SaveChanges();
                //saves the photo Path
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
            //actors will be the object to edit
            Actors actors = db.Actors.Find(id);
            //model is a new model ViewModelEditActors
            var model = new ViewModelEditActors();

            //IdValues allocates the actor ID
            model.IdValue = actors.ID;
            
            //Name allocates the actor Name
            model.Name = actors.Name;
            
            //Mini Bio allocates the actor MiniBio
            model.Minibio = actors.Minibio;

            //BD allocates the actor BD(Birth Date)
            model.BD = actors.BD;

            //Photograph allocates the actor Photograph
            model.Photograph = actors.Photograph;

            //IdsAllCha length definition 
            model.IdsAllCha = new int[db.Charas.Count()];

            //ListAllCha will contain every Characcter 
            model.ListAllCha = db.Charas.ToList();

            //------Characters that are already selected
            //var that will go through the Array
            int i = 0;

            //charactersAux is an aux List
            var charactersAux = new List<Characters> { };
            
            //IdsCha legth definition
            model.IdsCha = new int[actors.CharacterList.Count()];
            //each character will be added into the aux Variable
            foreach (var p in actors.CharacterList)
            {
                model.IdsCha[i] = p.ID;
                charactersAux.Add(p);
                i++;
            }
            // the Characters List will contain the aux values
            model.ListCha = charactersAux;


            if (actors == null)
            {
                return HttpNotFound();
            }
            //return the model
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
            Actors newActor = db.Actors.Find(actors.IdValue);

            //define List for Model errors
            actors.ListAllCha = db.Charas.ToList();
            actors.ListCha = newActor.CharacterList;

            //button verification
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");
            }

            //--photo confirmations
                  //define the photo name
            string photoName = "ActorPhoto" + newActor.ID;
            //will contain the photo Path
            string pathPhoto = "";

            //if the photo is null then 
            if (photo == null)
            {
                //returns the model state
                actors.Photograph = oldphoto;
            }
            else
            {
                //if the img format is jpg
                if (photo.ContentType == "image/jpeg")
                {
                    //give the photo a name and a Path
                    photoName = "ActorPhoto" + actors.IdValue + ".jpg";
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Atores/"), photoName);
                    System.IO.File.Delete(pathPhoto);
                    actors.Photograph = photoName;
                    photo.SaveAs(pathPhoto);

                }
                else
                {
                    //if not the model state will be returned
                    actors.BD = newActor.BD;
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(actors);

                }

            }

            //date confirmations
            if (date > DateTime.Now)
            {
                //if date is above todays date then the error will appear
                actors.BD = newActor.BD;
                ModelState.AddModelError("", "Invalid date");
                return View(actors);

            }
            else
            {
                //if not then the date is valid
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
            //add the selected character to the CharacterList
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
            //removes the fk connections
            Actors actors = db.Actors.Find(id);
            actors.CharacterList = new List<Characters> { };
            foreach (var ch in actors.CharacterList.ToList())
            {
                actors.CharacterList.Remove(ch);
            }

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
