using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly LibraryContext _context;

        public CategoriesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            var data = _context.Categories.Where(x => x.Id == model.Id).FirstOrDefault();
            
            if (data != null)
            {
                data.Name = model.Name;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category categoryToDelete = _context.Categories.FirstOrDefault(x => x.Id == id);
            return View(categoryToDelete);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            Category categoryToDelete = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            var booksWithCategory = _context.Books.Where(b => b.CategoryId == id).ToList();

            if ( categoryToDelete.Name == "default") 
                return RedirectToAction(nameof(Index));


            if (booksWithCategory.Count > 0 )
            {

                var defaultCategory = _context.Categories.FirstOrDefault(c => c.Name.Equals("default"));                

                if (defaultCategory == null)
                {
                    categoryToDelete = new Category { Name = "default" };
                    _context.Categories.Add(categoryToDelete);
                }

                foreach (var book in booksWithCategory) 
                {
                    book.Category = defaultCategory;
                    _context.Update(book);
                }               

            }
                _context.Categories.Remove(categoryToDelete);
         
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
