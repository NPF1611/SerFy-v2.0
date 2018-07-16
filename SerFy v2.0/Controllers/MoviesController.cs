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
//**********************************************************MOVIE CONTROLLER
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
            //variable that will have the avarage
            var avg = 0;
            //Nrates is the number of rates for each movie
            var Nrates = movie.Rates.Count();
            //if the rate is null than the view goes with zero
            if (Nrates != 0)
            {
                // each value need to be added to the avg
                foreach (var rt in movie.Rates.ToList())
                {
                    avg = rt.rate + avg;
                }
                //division
                avg = avg / Nrates;
            }
            //add the average to the Movie
            movie.Rating = avg;

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            //variable that will contain the Characters that are into the database
            var ch = db.Charas.ToList();
            //new Model to be possible represent the fks
            var model = new ViewModelCreateFilmePerso();
            //get the length necessary to put all the ids from the characters database
            model.idsCharacters = new int[db.Charas.Count()];
            //define characters list
            model.Listcharacters = ch;

            //variable that will contain the Directors that are into the database
            var dr = db.Directores.ToList();
            //get the length necessary to put all the ids from the Directors database
            model.idsDirectores = new int[db.Directores.Count()];
            //define Directors list
            model.ListDirectors = dr;

            //variable that will contain the Writers that are into the database
            var wr = db.Writers.ToList();
            //get the length necessary to put all the ids from the Writers database
            model.idsWriters = new int[db.Writers.Count()];
            //define Writers list
            model.ListWriters = wr;

            return View(model);
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateFilmePerso movie, HttpPostedFileBase photo, DateTime date, int valueButton)
            {

            //-----valueButton  "exception"------

            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");


            }
            if (valueButton == 2)
            {

                return RedirectToAction("Create", "Directors");

            }
            if (valueButton == 3)
            {

                return RedirectToAction("Create", "Writers");

            }
            //new movie creation
            Movie newmovie = new Movie();
            //define the movie ID
            int newID;
            //if we have 0 ids then the id will be one 
            if (db.Movies.Count() == 0)
            {
                newID = 1;

            }
            else
            {
                //the id will be the higher ID value plus One
                newID = db.Movies.Max(a => a.ID) + 1;
            }

            //the id will be equals to the variable
            newmovie.ID = newID;

            //-----Lists inicialization ------ thats because the Lists are null and that will generate an error in the end----
            //characters List inicialization
            var chr = db.Charas.ToList();
            movie.Listcharacters = chr;

            //Directors list inicialization
            var drs = db.Directores.ToList();
            movie.ListDirectors = drs;

            //Writers list inicialization
            var wrs = db.Writers.ToList();
            movie.ListWriters = wrs;

            //This will verify the name, this statement is not necessary but in the begin an error occurred  
            if (movie.Name == null)
            {
                ModelState.AddModelError("", "Name not found");
                return View(movie);
            }
            //

            //define the photo Name 
            string photoName = "MoviePhoto" + newID;

            //will define the photo path 
            string pathPhoto = "";

            if (photo == null)
            {
                movie.idsCharacters = new int[0];
                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];
                //photo cannot be null if that happens then this error will appear 
                ModelState.AddModelError("", "Image not found");
                return View(movie);
            }
            else
            {
                if (photo.ContentType == "image/jpeg")
                {
                    //the content needs to be jpeg if not the site will reject it 
                    photoName = photoName + ".jpg";
                    newmovie.Photograph = photoName;
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);

                }
                else
                {
                    movie.idsCharacters = new int[0];
                    movie.idsDirectores = new int[0];
                    movie.idsWriters = new int[0];
                    //rejection of the photo
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(movie);
                }

            }
            //--see if the following fks are selected

            //Characters
            if (movie.idsCharacters == null)
            {
                movie.idsCharacters = new int[0];
                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];
                ModelState.AddModelError("", "No Characters selected ");
                return View(movie);
            }

            //Directors
            if (movie.idsDirectores == null)
            {
                movie.idsCharacters = new int[0];
                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];

                ModelState.AddModelError("", "No Directors selected ");
                return View(movie);
            }
            //Writers
            if (movie.idsWriters == null)
            {
                movie.idsCharacters = new int[0];
                movie.idsDirectores = new int[0];
                movie.idsWriters = new int[0];

                ModelState.AddModelError("", "No Writers selected ");
                return View(movie);
            }

            //-----Date validation------
            if (date == null)
            {
                //if date equals Null then the an error will appear
                ModelState.AddModelError("", "no date defined");
                return View(movie);
            }
            else
            {
                if (date > DateTime.Now)
                {
                    //if date is not below today's date an error will appear
                    ModelState.AddModelError("", "Invalid date");
                }
                else
                {
                    //if not the movie receives the date value
                    newmovie.dataDePub = date;
                }
            }

            //----change the trailer
            //string that will contain the Trailer
            string verNumbTrailer = movie.Trailer;
            //if Trailer characters are less then 32 then the trailer is not from youtube 
            if (verNumbTrailer.Length > 32)
            {
                //var that will contain the trailer withou the youtube trailer reference
                var verTrailer = movie.Trailer.Substring(0, 32);
                //if the varTrailer is equals to https://www.youtube.com/watch?v= the Trailer can be added
                if (verTrailer.Equals("https://www.youtube.com/watch?v="))
                {
                    //trailers will be only from youtube so we replace the watch value to facilitate the user
                    newmovie.Trailer = movie.Trailer.Replace("/watch?v=", "/embed/");
                }
                else
                {
                    //if not th trailler its not valid
                    ModelState.AddModelError("", "Invalid Trailer");
                    return View(movie);
                }
            }
            else
            {
                //if not the trailer do not have even the length to be an youtube link 
                ModelState.AddModelError("", "Invalid Trailer thats not even from Youtube");
                return View(movie);

            }
            //define movie rating this one will start at zero for default
            newmovie.Rating = 0;

            //---Lists Values atribution
            //Characters
            newmovie.CharactersList = new List<Characters> { };
            //this foreach will find the characters based on FKS ids
            foreach (var ch in movie.idsCharacters.ToList())
            {
                Characters charac = db.Charas.Find(ch);
                //add the Character to the Character List
                newmovie.CharactersList.Add(charac);
            }


            //Directors 
            newmovie.DirectorList = new List<Director> { };
            //this foreach will find the Directors based on FKS ids
            foreach (var ch in movie.idsDirectores.ToList())
            {
                Director dir = db.Directores.Find(ch);
                //add the Director to the Directors List
                newmovie.DirectorList.Add(dir);
            }


            //Writers
            newmovie.WriterList = new List<Writer> { };
            //this foreach will find the Writers based on FKS ids
            foreach (var ch in movie.idsWriters.ToList())
            {
                Writer writer = db.Writers.Find(ch);

                //add the Writer to the Writer List
                newmovie.WriterList.Add(writer);
            }

            //List (Comments,Rates) will be empty for defauult
            newmovie.Comments = new List<Comment> { };
            newmovie.Rates = new List<Rate> { };

            //Synopse will not be verified
            newmovie.sinopse = movie.sinopse;
            //name attribution
            newmovie.Name = movie.Name;


            if (ModelState.IsValid)
            {
                db.Movies.Add(newmovie);
                db.SaveChanges();
                //Cover path
                photo.SaveAs(pathPhoto);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            //check if the is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finds the id value and brings the Movie to variable movie
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            //creates a new moveEdit 
            var model = new viewModelEditFilme();

            //gets all the characters into the var ch
            var ch = db.Charas.ToList();
            // gets the space necessary to allocate ids
            model.idsAllCharacters = new int[db.Charas.Count()];
            //all the characters go into the model
            model.ListAllcharacters = ch;

            //gets all the Directors into the var dr
            var dr = db.Directores.ToList();
            // gets the space necessary to allocate ids
            model.idsAllCharacters = new int[db.Directores.Count()];
            //all the Directos go into the model
            model.ListAllDirectors = dr;

            //gets all the Writers into the var dr
            var wr = db.Writers.ToList();
            // gets the space necessary to allocate ids
            model.idsAllWriters = new int[db.Writers.Count()];
            //all the writers go into the model
            model.ListAllWriters = wr;


            //Movie{id} characters
            /*Here the idea is to get the selected values*/
            //integer used to increment the id 
            int i = 0;
            //list aux to add the characters selected 
            var charactersAux = new List<Characters> { };
            model.idsCharacters = new int[movie.CharactersList.Count()];
            foreach (var p in movie.CharactersList)
            {
                //gets the id and add it to the ids array
                model.idsCharacters[i] = p.ID;
                //get the character and add it to the characterAux List
                charactersAux.Add(p);
                i++;
            }
            //characteraux values go into the listCharacters
            model.Listcharacters = charactersAux;

            //Movie{id} Directors
            /*Here the idea is to get the selected values*/
            //integer used to increment the id 
            i = 0;
            //list aux to add the Directors selected 
            var directorsAux = new List<Director> { };
            model.idsDirectores = new int[movie.DirectorList.Count()];
            foreach (var d in movie.DirectorList)
            {
                //gets the id and add it to the ids array
                model.idsDirectores[i] = d.ID;
                //get the directors and add it to the directorsAux List
                directorsAux.Add(d);
                i++;
            }
            //directorsAux values go into the ListDirectors
            model.ListDirectors = directorsAux;

            //Movie{id} Writters
            /*Here the idea is to get the selected values*/
            //integer used to increment the id 
            i = 0;
            var wrtersAux = new List<Writer> { };
            model.idsWriters = new int[movie.WriterList.Count()];
            foreach (var w in movie.WriterList)
            {
                //gets the id and add it to the ids array
                model.idsWriters[i] = w.ID;
                //get the writers and add it to the wrtersAux List
                wrtersAux.Add(w);
                i++;
            }
            //wrtersAux values go into the ListWriters
            model.ListWriters = wrtersAux;


            //Movie{id} Comments 
            i = 0;
            var CommentsAux = new List<Comment> { };
            model.idsComments = new int[movie.Comments.Count()];
            foreach (var cm in movie.Comments)
            {
                //gets the id and add it to the ids array
                model.idsComments[i] = cm.ID;
                //get the Comments and add them into the CommentsAux List
                CommentsAux.Add(cm);
                i++;
            }
            //CommentsAux values go into the ListComments
            model.ListComments = CommentsAux;

            //Movie{id} Rates 
            i = 0;
            var RateAux = new List<Rate> { };
            model.idsRates = new int[movie.Rates.Count()];
            foreach (var rt in movie.Rates)
            {
                //gets the id and add it to the ids array
                model.idsRates[i] = rt.ID;
                //get the Rates and add them into the RateAux List
                RateAux.Add(rt);
                i++;
            }
            //RateAux values go into the ListRates
            model.ListRates = RateAux;


            //give the idValue to the model
            model.IDvalue = movie.ID;
            //give the ListDirectors to the model
            model.ListDirectors = movie.DirectorList;
            //give the ListWriters to the model
            model.ListWriters = movie.WriterList;
            //give the Name to the model
            model.Name = movie.Name;
            //give the dataDePub to the model
            model.dataDePub = movie.dataDePub;
            //give the Photograph to the model
            model.Photograph = movie.Photograph;
            //give the Trailer to the model
            model.Trailer = movie.Trailer.Replace("/embed/", "/watch?v=");
            //give the sinopse to the model
            model.sinopse = movie.sinopse;
            //give the Rating to the model
            model.Rating = movie.Rating;

            //return the model to the view
            return View(model);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(viewModelEditFilme movie, DateTime date, HttpPostedFileBase photo, String oldphoto, int valueButton)
        {

            //-----valueButton  "exception"------
            if (valueButton == 1)
            {
                return RedirectToAction("Create", "Characters");


            }
            if (valueButton == 2)
            {

                return RedirectToAction("Create", "Directors");

            }
            if (valueButton == 3)
            {

                return RedirectToAction("Create", "Writers");

            }

            //get the movie
            var moveitoenter = db.Movies.Find(movie.IDvalue);

            //movies Lists default values
            movie.ListAllcharacters = db.Charas.ToList();
            movie.ListAllComments = db.Comments.ToList();
            movie.ListAllDirectors = db.Directores.ToList();
            movie.ListAllWriters = db.Writers.ToList();
            movie.ListAllRates = db.Rates.ToList();

            movie.Listcharacters = moveitoenter.CharactersList;
            movie.ListComments = moveitoenter.Comments;
            movie.ListDirectors = moveitoenter.DirectorList;
            movie.ListWriters = moveitoenter.WriterList;
            movie.ListRates = moveitoenter.Rates;

            //photo Name
            string photoName = "";
            //will have the photo path
            string pathPhoto = "";

            if (photo != null)
            {   //if the photo is an jpg 
                if (photo.ContentType == "image/jpeg")
                {
                    //delete and add the image
                    photoName = "MoviePhoto" + movie.IDvalue + ".jpg";
                    pathPhoto = Path.Combine(Server.MapPath("~/Multimedia/Filme/"), photoName);
                    System.IO.File.Delete(pathPhoto);
                    movie.Photograph = photoName;
                    photo.SaveAs(pathPhoto);

                }
                else
                {
                    //if the type is not jpg then the photo will be equals too the old photo
                    movie.Photograph = oldphoto;
                    //photo rejection   
                    ModelState.AddModelError("", "Invalid photo type");
                    return View(movie);
                }
            }
            else
            {
                //no file seleced then the photo remains the same
                movie.Photograph = oldphoto;

            }


            //--date confirmations
            if (date > DateTime.Now)
            {
                //if date is not below today's date an error will appear
                ModelState.AddModelError("", "Invalid date");
            }
            else
            {
                //if not date selected will be the movie date
                movie.dataDePub = date;
            }

            //----change the trailer
            //string that will contain the Trailer
            string verNumbTrailer = movie.Trailer;
            //if Trailer characters are less then 32 then the trailer is not from youtube 
            if (verNumbTrailer.Length > 32)
            {
                //var that will contain the trailer withou the youtube trailer reference
                var verTrailer = movie.Trailer.Substring(0, 32);
                //if the varTrailer is equals to https://www.youtube.com/watch?v= the Trailer can be added
                if (verTrailer.Equals("https://www.youtube.com/watch?v="))
                {
                    //trailers will be only from youtube so we replace the watch value to facilitate the user
                    moveitoenter.Trailer = movie.Trailer.Replace("/watch?v=", "/embed/");
                }
                else
                {
                    //if not th trailler its not valid
                    ModelState.AddModelError("", "Invalid Trailer");
                    return View(movie);
                }
            }
            else
            {
                //if not the trailer do not have even the length to be an youtube link 
                ModelState.AddModelError("", "Invalid Trailer thats not even from Youtube");
                return View(movie);

            }

            //----------------Lists
            //--Characters-remove
            //the List will be empty
            foreach (var AllCharacters in movie.idsAllCharacters)
            {
                Characters charac = db.Charas.Find(AllCharacters);
                if (moveitoenter.CharactersList.Contains(charac))
                {
                    moveitoenter.CharactersList.Remove(charac);
                }
            }
            if (movie.idsCharacters == null)
            {
                ModelState.AddModelError("", "No Characters selected");
                return View(movie);
            }
            //--Characters-add
            //The list will be with the selected values
            foreach (var ch in movie.idsCharacters.ToList())
            {
                Characters charac = db.Charas.Find(ch);
                if (!moveitoenter.CharactersList.Contains(charac))
                {
                    moveitoenter.CharactersList.Add(charac);
                }
            }

            //--Directors-Remove
            //the List will be empty
            foreach (var AllDirectors in movie.idsAllDirectores.ToList())
            {
                Director dir = db.Directores.Find(AllDirectors);
                if (moveitoenter.DirectorList.Contains(dir))
                {
                    moveitoenter.DirectorList.Remove(dir);
                }
            }

            //--Directors-Add
            //The list will be with the selected values
            foreach (var dir in movie.idsDirectores.ToList())
            {
                Director direc = db.Directores.Find(dir);
                if (!moveitoenter.DirectorList.Contains(direc))
                {
                    moveitoenter.DirectorList.Add(direc);
                }
            }
            //--Writters-Remove
            //the List will be empty
            foreach (var AllWriters in movie.idsAllWriters.ToList())
            {
                Writer writ = db.Writers.Find(AllWriters);
                if (moveitoenter.WriterList.Contains(writ))
                {
                    moveitoenter.WriterList.Remove(writ);
                }
            }


            //--Writters-Add
            //The list will be with the selected values
            foreach (var wrt in movie.idsWriters.ToList())
            {
                Writer writer = db.Writers.Find(wrt);
                if (!moveitoenter.WriterList.Contains(writer))
                {
                    moveitoenter.WriterList.Add(writer);
                }
            }

            //--Comments
            //old comments will be added here
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
            //old rates will be added here
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

            //give all the information that the user has changed and was not added before
            moveitoenter.ID = movie.IDvalue;
            moveitoenter.Name = movie.Name;
            // moveitoenter.Photograph = movie.Photograph;
            //moveitoenter.Trailer = movie.Trailer;
            moveitoenter.sinopse = movie.sinopse;
            //moveitoenter.dataDePub = movie.dataDePub;

            moveitoenter.Rating = movie.Rating;

            //add a Comment List if the value is null to avoid errors 
            if (moveitoenter.Comments == null)
            {
                moveitoenter.Comments = new List<Comment> { };
            }
            //add a Rates List if the value is null to avoid errors 

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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //find the movie based on the id
            Movie movie = db.Movies.Find(id);

            //--cut the fks connections
            //a empty list is added
            movie.CharactersList = new List<Characters> { };
            //if some of the characters still into the list they are removed here
            foreach (var ch in movie.CharactersList.ToList())
            {
                movie.CharactersList.Remove(ch);
            }
            
            //a empty list is added
            movie.DirectorList = new List<Director> { };
            //if some of the Directors still into the list they are removed here
            foreach (var dr in movie.DirectorList.ToList())
            {
                movie.DirectorList.Remove(dr);
            }

            //a empty list is added
            movie.WriterList = new List<Writer> { };
            //if some of the Writers still into the list they are removed here
            foreach (var wr in movie.WriterList.ToList())
            {
                movie.WriterList.Remove(wr);
            }

            //a empty list is added
            movie.Comments = new List<Comment> { };
            //if some of the Comments still into the list they are removed here
            foreach (var cm in movie.Comments.ToList())
            {
                var elC = db.Comments.Find(cm.ID);
                db.Comments.Remove(elC);
                movie.Comments.Remove(cm);
            }
            
            //a empty list is added
            movie.Rates = new List<Rate> { };
            //if some of the Rates still into the list they are removed here
            foreach (var rt in movie.Rates.ToList())
            {
                var elR = db.Rates.Find(rt.ID);
                db.Rates.Remove(elR);
                movie.Rates.Remove(rt);
            }

            //remove the Movie from the Database
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
