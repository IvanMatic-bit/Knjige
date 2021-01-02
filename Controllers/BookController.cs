using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka.Controllers
{
    
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Book> Bbook { get; set; }



        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            Book = new Book();
            
            return View(Book);
        }

        
        public IActionResult Update(int id)
        {
            Book = new Book();
            Book = _db.Book.FirstOrDefault(u => u.Id == id);
            
            return View(Book);

        }

        #region API
        //Dohvati sve knjige iz baze
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }
        //Unos nove knjige u bazu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNew()
        {
            
            if (ModelState.IsValid)
            {
                _db.Book.Add(Book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Book);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Book = new Book();
            Book = _db.Book.FirstOrDefault(u => u.Id == id);
            if(Book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(Book);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete successful" });

        }
        //update 
        [HttpPost]
        public IActionResult UpdateDb()
        {
            if(ModelState.IsValid)
            {
                _db.Book.Update(Book);
                _db.SaveChanges();
            }
            
           
           return RedirectToAction("Index");
        }

        #endregion
    }
}

