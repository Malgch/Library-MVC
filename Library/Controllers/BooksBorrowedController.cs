using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
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


        // GET: BooksBorrowedController/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            var availableBooks = _context.Books.Where(b => b.IsAvailable).ToList();

            var defaultBookBorrowed = new BookBorrowed
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(30)
            };

            ViewData["BookId"] = new SelectList(availableBooks, "Id", "Author");
            return View(defaultBookBorrowed);
        }

        // POST: BooksBorrowedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create(int bookId)
        {
            var bookToBorrow = _context.Books.Where(b => b.Id == bookId).FirstOrDefault();

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
        [Authorize(Roles = "User, Admin")]
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
        [Authorize(Roles = "User, Admin")]
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

        // GET: BooksBorrowedController/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Return()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var borrowedBooks = _context.BooksBorrowed
                .Include(bb => bb.Book)
                .Where(bb => bb.LibraryUserId == userId && !bb.IsReturned);

            ViewData["BookTitle"] = new SelectList(borrowedBooks, "BookId", "Book.Title");

            return View(borrowedBooks.FirstOrDefault());
        }

        // POST: BooksBorrowedController/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Return(string libraryUserId, int? bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var borrowedBook = _context.BooksBorrowed
                                .Include(bb => bb.Book)
                                .FirstOrDefault(bb => bb.LibraryUserId == userId && bb.BookId == bookId);

            var book = _context.Books.Find(bookId);

            if (borrowedBook == null || borrowedBook.IsReturned)
            {
                ViewBag.ErrorMessage = "Book is alredy returned or it does not exist.";
                return RedirectToAction("Index", "Book");
            }

            borrowedBook.IsReturned = true;
            borrowedBook.EndTime = DateTime.Now.Date;
            _context.BooksBorrowed.Update(borrowedBook);
            _context.SaveChanges();
            book.IsAvailable = true;
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "BooksBorrowed");
        }


        // GET: BooksBorrowedController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var borrowedBook = _context.BooksBorrowed
                                .Include(bb => bb.Book)
                                .FirstOrDefault(bb => bb.LibraryUserId == userId && bb.BookId == bookId);

            return View(borrowedBook);
        }

        // POST: BooksBorrowedController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string libraryUserId, int bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var borrowedBook = _context.BooksBorrowed
                                .Include(bb => bb.Book)
                                .FirstOrDefault(bb => bb.LibraryUserId == userId && bb.BookId == bookId);

            var book = _context.Books.Find(bookId);
            book.IsAvailable = true;

            if (borrowedBook == null)
            {
                return NotFound();
            }

            _context.BooksBorrowed.Remove(borrowedBook);
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
