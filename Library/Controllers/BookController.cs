﻿using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    
    public class BookController : Controller
    {

        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }



        // GET: BookController
        public ActionResult Index()
        {
            var booksAndCategory = _context.Books.Include(b => b.Category).ToList();
            return View(booksAndCategory);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var booksAndCategory = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            return View(booksAndCategory);
            //return View(_context.Books.Find(id));

        }

        [Authorize(Roles = "Admin")]
        // GET: BookController/Create
        public ActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();

        }

        
        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Book book)
        {
            book.IsAvailable = true;
            _context.Books.Add(book);
            _context.SaveChanges();                
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", data.CategoryId);
            return View(data);
        }


        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Book book)
        {
            var data = _context.Books.Where(x => x.Id == book.Id).FirstOrDefault();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", data.CategoryId);

            if (data != null)
            {
                data.Title = book.Title;
                data.Author = book.Author;
                data.CategoryId = book.CategoryId;
                data.Description = book.Description;
                data.IsAvailable = book.IsAvailable;               
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        // GET: BookController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Book bookToDelete = _context.Books.FirstOrDefault(x => x.Id == id);
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, Book book)
        {
            Book bookToDelete = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Roles = "Admin,User")]
        //public ActionResult Borrow(int bookId)
        //{
        //    var bookToBorrow = _context.Books.FirstOrDefault(b => b.Id == bookId);

        //    var model = new Tuple<Book, BookBorrowed>(bookToBorrow, new BookBorrowed { BookId = bookToBorrow.Id });
        //    return View(model);
        //}

        //// POST: Book/Borrow/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin,User")]
        //public ActionResult Borrow(BookBorrowed bookBorrowed)
        //{
        //    var book = _context.Books.Find(bookBorrowed.BookId);

        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (book.IsAvailable)
        //    {
        //        book.IsAvailable = false;
        //        _context.Books.Update(book);

        //        bookBorrowed.StartTime = DateTime.Now;
        //        bookBorrowed.EndTime = DateTime.Now.AddDays(30);
        //        bookBorrowed.LibraryUserId = userId;
        //        bookBorrowed.IsReturned = false;

        //        _context.BooksBorrowed.Add(bookBorrowed);
        //        _context.SaveChanges();

        //        return RedirectToAction("Index"); 
        //    }
        //    TempData["Message"] = "The book is not available for borrowing.";
        //    return RedirectToAction("Index");
        //}
    }
}
