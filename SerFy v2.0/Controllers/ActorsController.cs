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
        public ActionResult Create(ViewModelCreateActors Actor, Actors actors, HttpPostedFileBase photo,DateTime Date)
        {
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
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);

                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");

                }

            }



            //ModelState
            if (ModelState.IsValid)
            {
                db.Actors.Add(actors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actors);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Photograph,BD,Minibio")] Actors actors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actors).State = EntityState.Modified;
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
