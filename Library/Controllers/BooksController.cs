using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Newtonsoft.Json;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private LibraryDb _db;

        public BooksController()
        {
            _db = new LibraryDb();
        }

        public BooksController(ILibraryDb db)
        {
            _db = db as LibraryDb;
        }


        /// <summary>
        /// Shows all books in library in default order
        /// </summary>
        /// <returns>List of books List<Book>/returns>
        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }


        /// <summary>
        /// Shows all books in library ordered ascending by title
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexAsc()
        {
            return View(_db.Books.OrderBy(book => book.Title).ToList());
        }

        /// <summary>
        /// Shows all books in library ordered descending by title
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexDesc()
        {
            return View(_db.Books.OrderByDescending(book => book.Title).ToList());
        }

        /// <summary>
        /// Shows all books in library ordered ascending by author
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexAuthAsc()
        {
            return View(_db.Books.OrderBy(book => book.Author).ToList());
        }

        /// <summary>
        /// Shows all books in library ordered descending by author
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexAuthDesc()
        {
            return View(_db.Books.OrderByDescending(book => book.Author).ToList());
        }

        /// <summary>
        /// Allows to export all books from database in json format
        /// </summary>
        /// <returns>Json file</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Export()
        {
            if (true)
            {
                var books = _db.Books;
                var str = JsonConvert.SerializeObject(books);
                byte[] data = Encoding.ASCII.GetBytes(str);
                return File(data, "application/json", "export.json");
            }
        }


        /// <summary>
        /// Shows view allowing import data to database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Import()
        {
            return View();
        }

        /// <summary>
        /// Removes all books from database
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult CleanDb()
        {
            var books = _db.Books.ToList();

            foreach (var book in books)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
            }
            ViewBag.Message = "Baza danych została wyczyszczona!";
            return View();
        }

        /// <summary>
        /// Imports data to database from json file
        /// </summary>
        /// <param name="jsonPostedFile"></param>
        /// <returns>View</returns>
        [HttpPost]
        [Authorize]
        public  ActionResult Import(HttpPostedFileBase jsonPostedFile)
        {
            try
            {
                if (jsonPostedFile.FileName.EndsWith(".json"))
                {
                    StreamReader streamReader = new StreamReader(jsonPostedFile.InputStream);
                    string stReadToEnd = streamReader.ReadToEnd();
                    List<Book> books = JsonConvert.DeserializeObject<List<Book>>(stReadToEnd);

                    foreach (var book in books)
                    {
                        _db.Books.AddOrUpdate(book);
                    }
                    _db.SaveChanges();
                    ViewBag.Success = "Baza wirusów została zaktualizowana!";
                }
                else
                {
                    ViewBag.Error = "Niewłaściwy format pliku";
                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.ToString();
            }
            return View();
        }


        /// <summary>
        /// Shows details about book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Book</returns>
       public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        /// <summary>
        /// Shows form to add book
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Adds book to library
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Rate,Isbn")] Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        /// <summary>
        /// Shows form to edit single book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        /// <summary>
        /// Edit book in database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Rate,Isbn")] Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(book).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        /// <summary>
        /// Shows confirmation page deleting book from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        /// <summary>
        /// If confirmed deleting book from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _db.Books.Find(id);
            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Disposing db
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
