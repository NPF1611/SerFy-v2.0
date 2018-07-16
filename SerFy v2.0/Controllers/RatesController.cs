
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerFy_v2._0.Models;

namespace SerFy_v2._0.Controllers
{
    public class RatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rates
        public ActionResult Index()
        {
            return View(db.Rates.ToList());
        }

        // GET: Rates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // GET: Rates/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieFK")]Rate rt, int value)
        {
            //create a rate
            var rate = new Rate();
            
            //create utilizador to get the user
            var utilizador = db.Utilizadores.Where(u => u.email == User.Identity.Name).FirstOrDefault();
            
            //rate userFK will be equals to thee utilizador.ID
            rate.UserFK = utilizador.ID;

            //confirm will get the old rate
            Rate confirm = db.Rates.Where(u => u.MovieFK == rt.MovieFK && u.UserFK == utilizador.ID).FirstOrDefault();

            //if confirm exists
            if (confirm != null)
            {
                //rate selected by the user
                rate.rate = value;
                //confirm id need to fit the rate ID
                rate.ID = confirm.ID;
                // Movie referent to the rate
                rate.MovieFK = rt.MovieFK;
                //delete the rate to create a new one after this
                DeleteConfirmed(confirm.ID);
            }
            else
            {
                //if not rate will be created for the firs time
                //id defenition
                if (db.Rates.Count() == 0)
                {
                    rate.ID = 1;
                }
                else
                {
                    rate.ID = db.Rates.Max(r => r.ID) + 1;
                }
                //value of the rate selected
                rate.rate = value;
                //Movie selected
                rate.MovieFK = rt.MovieFK;
            }
            if (ModelState.IsValid)
            {
                //add the new rate
                db.Rates.Add(rate);
                db.SaveChanges();
                //redirect to the movie view
                return RedirectToAction("Details", "Movies", new { id = rate.MovieFK });

            }

            return View(rate);
        }

        // GET: Rates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }
        // POST: Rates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,rate")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rate);
        }

        // GET: Rates/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
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
