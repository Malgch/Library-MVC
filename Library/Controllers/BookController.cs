using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private static IList<Book> books = new List<Book>
        {
            new Book() {Id = 1, Title = "Twilight", Author = "Stephany Mayer", Description = "Teenage romance"},
            new Book() {Id = 2, Title = "Lord of the rings", Author = "Tolkien", Description = "Book about rings"},
            new Book() {Id = 3, Title = "1984", Author = "George Orwell", Description = "Big brother is watching"}
        };

        // GET: BookController
        public ActionResult Index()
        {
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View(books.FirstOrDefault(x => x.Id == id));
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(new Book());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            book.Id = books.Count + 1;
            books.Add(book);
            return RedirectToAction("Index");
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            Book b = books.FirstOrDefault(x => x.Id == id);
            b.Author = book.Author;
            b.Description = book.Description;
            b.Title = book.Title;

            return RedirectToAction(nameof(Index));
         
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            Book b = books.FirstOrDefault(x => x.Id == id);
            books.Remove(b);

            return RedirectToAction(nameof(Index));
        }
    }
}
