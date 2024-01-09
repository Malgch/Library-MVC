using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Controllers
{
    public class BookBorrowedsController : Controller
    {
        private readonly LibraryContext _context;

        public BookBorrowedsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookBorroweds
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.BooksBorrowed.Include(b => b.Book).Include(b => b.LibraryUser);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BookBorroweds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BooksBorrowed == null)
            {
                return NotFound();
            }

            var bookBorrowed = await _context.BooksBorrowed
                .Include(b => b.Book)
                .Include(b => b.LibraryUser)
                .FirstOrDefaultAsync(m => m.LibraryUserId == id);
            if (bookBorrowed == null)
            {
                return NotFound();
            }

            return View(bookBorrowed);
        }

        // GET: BookBorroweds/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
            ViewData["LibraryUserId"] = new SelectList(_context.LibraryUsers, "Id", "Id");
            return View();
        }

        // POST: BookBorroweds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,IsReturned,LibraryUserId,BookId")] BookBorrowed bookBorrowed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookBorrowed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookBorrowed.BookId);
            ViewData["LibraryUserId"] = new SelectList(_context.LibraryUsers, "Id", "Id", bookBorrowed.LibraryUserId);
            return View(bookBorrowed);
        }

        // GET: BookBorroweds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BooksBorrowed == null)
            {
                return NotFound();
            }

            var bookBorrowed = await _context.BooksBorrowed.FindAsync(id);
            if (bookBorrowed == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookBorrowed.BookId);
            ViewData["LibraryUserId"] = new SelectList(_context.LibraryUsers, "Id", "Id", bookBorrowed.LibraryUserId);
            return View(bookBorrowed);
        }

        // POST: BookBorroweds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,StartTime,EndTime,IsReturned,LibraryUserId,BookId")] BookBorrowed bookBorrowed)
        {
            if (id != bookBorrowed.LibraryUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookBorrowed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookBorrowedExists(bookBorrowed.LibraryUserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookBorrowed.BookId);
            ViewData["LibraryUserId"] = new SelectList(_context.LibraryUsers, "Id", "Id", bookBorrowed.LibraryUserId);
            return View(bookBorrowed);
        }

        // GET: BookBorroweds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BooksBorrowed == null)
            {
                return NotFound();
            }

            var bookBorrowed = await _context.BooksBorrowed
                .Include(b => b.Book)
                .Include(b => b.LibraryUser)
                .FirstOrDefaultAsync(m => m.LibraryUserId == id);
            if (bookBorrowed == null)
            {
                return NotFound();
            }

            return View(bookBorrowed);
        }

        // POST: BookBorroweds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BooksBorrowed == null)
            {
                return Problem("Entity set 'LibraryContext.BooksBorrowed'  is null.");
            }
            var bookBorrowed = await _context.BooksBorrowed.FindAsync(id);
            if (bookBorrowed != null)
            {
                _context.BooksBorrowed.Remove(bookBorrowed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookBorrowedExists(string id)
        {
          return (_context.BooksBorrowed?.Any(e => e.LibraryUserId == id)).GetValueOrDefault();
        }
    }
}
