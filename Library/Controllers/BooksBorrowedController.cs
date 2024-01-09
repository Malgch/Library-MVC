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
        public ActionResult Create(int? selectedBookId)
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
            // Retrieve book information based on bookId
            var bookToBorrow = _context.Books.Find(bookId);

            // Check if the book is available for borrowing
            if (bookToBorrow != null && bookToBorrow.IsAvailable)
            {
                // Create a new BookBorrowed instance
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

                TempData["Message"] = "Book borrowed successfully.";
            }
            else
            {
                TempData["Message"] = "The book is not available for borrowing.";
            }

            return RedirectToAction("Index", "Book");
        }

        // GET: BooksBorrowedController/Edit/5
        public ActionResult Edit(string libraryUserId, int bookId)
        {
            var borrowedBook = _context.BooksBorrowed.Find(libraryUserId, bookId);

            if (borrowedBook == null)
            {
                return NotFound();
            }

            // Pass the BookBorrowed object to the view for display
            return View(borrowedBook);
        }

        // POST: BooksBorrowedController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookBorrowed book)
        {
            var borrowedBook = _context.BooksBorrowed.Find(id);

            if (borrowedBook == null)
            {
                return NotFound();
            }

            borrowedBook.IsReturned = true;
            var associatedBook = _context.Books.Find(borrowedBook.BookId);

            if (associatedBook != null)
            {
                associatedBook.IsAvailable = true;
                _context.Books.Update(associatedBook);
            }
            _context.BooksBorrowed.Update(borrowedBook);
            _context.SaveChanges();

            return RedirectToAction("Index", "BooksBorrowed");
        }

        // GET: BooksBorrowedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
