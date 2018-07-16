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
    [Authorize(Roles = "Administrador")]
    public class CharactersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Characters
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Movies");
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Charas.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            return View(characters);
        }

        // GET: Characters/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Characters characters, HttpPostedFileBase photo)
        {

            //define the Character ID   
            int newID;
            if (db.Charas.Count() == 0)
            {
                newID = 1;

            }
            else
            {
                newID = db.Charas.Max(a => a.ID) + 1;
            }
            characters.ID = newID;
            ////validates the name
            //if (characters.Name == null) {
            //    ModelState.AddModelError("", "No name found");
            //    return View(characters);
            //}

            //define the photo path and validate it
            string photoName = "CharaPhoto" + newID;
            string pathPhoto = "";


            if (photo == null)
            {
                ModelState.AddModelError("", "Image not found");
                return View(characters);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    photoName = photoName + ".jpg";
                    characters.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Personagens/"), photoName);

                }
                else
                {

                    ModelState.AddModelError("", "Invalid photo");

                }

            }




            if (ModelState.IsValid)
            {
                db.Charas.Add(characters);
                db.SaveChanges();
               photo.SaveAs(pathPhoto);
                return RedirectToAction("Index", "Movies");
            }

            return View(characters);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Charas.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Photograph")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(characters);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Charas.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            return View(characters);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Characters characters = db.Charas.Find(id);

            characters.MoviesList = new List<Movie> { };
            foreach (var ch in characters.MoviesList.ToList())
            {
                characters.MoviesList.Remove(ch);
            }

            characters.actor = new Actors();

            characters.actor = null;
            db.Charas.Remove(characters);
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
