using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksBorrowedController : Controller
    {
        private readonly LibraryContext _context;

        public BooksBorrowedController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BooksBorrowedController
        public ActionResult Index()
        {
            var libraryContext = _context.BooksBorrowed.Include(b => b.Book).Include(b => b.LibraryUser);
            return View(libraryContext.ToList());
        }

        // GET: BooksBorrowedController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null || _context.BooksBorrowed == null)
            {
                return NotFound();
            }

            var bookBorrowed = _context.BooksBorrowed
                .Include(b => b.Book)
                .Include(b => b.LibraryUser);
                //.FirstOrDefault(m => m.LibraryUserId == id);
            if (bookBorrowed == null)
            {
                return NotFound();
            }
            return View(bookBorrowed);
        }

        // GET: BooksBorrowedController/Create
        public ActionResult Create()
        {
            var availableBooks = _context.Books.Where(b => b.IsAvailable).ToList();
            ViewData["BookId"] = new SelectList(availableBooks, "Id", "Author");
            return View();
        }

        // POST: BooksBorrowedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int bookId)
        {
            var bookToBorrow = _context.Books.Find(bookId);

            if (bookToBorrow != null && bookToBorrow.IsAvailable)
            {
                var bookBorrowed = new BookBorrowed
                {
                    StartTime = DateTime.Now.Date,
                    EndTime = DateTime.Now.Date.AddDays(30),
                    LibraryUserId = User.FindFirstValue(ClaimTypes.NameIdentifier), 
                    IsReturned = false,
                    BookId = bookId
                };

                bookToBorrow.IsAvailable = false;
                _context.Books.Update(bookToBorrow);
                _context.BooksBorrowed.Add(bookBorrowed);
                _context.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Book is not available.";
                return RedirectToAction("Index", "Book");
            }

            return RedirectToAction("Index", "Book");
        }

        // GET: BooksBorrowedController/Edit/5
        public ActionResult Prolong()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var borrowedBooks = _context.BooksBorrowed
                .Include(bb => bb.Book)
                .Where(bb => bb.LibraryUserId == userId && !bb.IsReturned);
                //.ToList();

            ViewData["BookTitle"] = new SelectList(borrowedBooks, "BookId", "Book.Title");

            return View(borrowedBooks.FirstOrDefault());
        }

        // POST: BooksBorrowedController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prolong(string libraryUserId, int? bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var borrowedBook = _context.BooksBorrowed
                                .Include(bb => bb.Book)
                                .FirstOrDefault(bb => bb.LibraryUserId == userId && bb.BookId == bookId);

            if (borrowedBook == null || borrowedBook.IsReturned)
            {
                ViewBag.ErrorMessage = "You cannot prolong returned book.";
                return RedirectToAction("Index", "Book");
            }

            borrowedBook.EndTime = borrowedBook.EndTime.AddDays(30);
            _context.BooksBorrowed.Update(borrowedBook);
            _context.SaveChanges();

            return RedirectToAction("Index", "BooksBorrowed");
        }

        // GET: BooksBorrowedController/Delete/5
        public ActionResult Delete(int id)
        {
            var bookBorrowed = _context.BooksBorrowed
                .Include(b => b.Book)
                .Include(b => b.LibraryUser);
                //.FirstOrDefault(m => m.LibraryUserId == id);
            if (bookBorrowed == null)
            {
                return NotFound();
            }

            return View(bookBorrowed);
        }

        // POST: BooksBorrowedController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
