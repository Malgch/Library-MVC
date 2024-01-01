using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Controllers
{
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
            return View();
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
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            Category categoryToDelete = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            ViewBag.Message = "Record deleted succesfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
