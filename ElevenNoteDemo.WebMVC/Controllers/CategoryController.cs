using ElevenNoteDemo.Models.CategoryModels;
using ElevenNoteDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNoteDemo.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var service = new CategoryService();
            var model = service.GetCategories();
            return View(model);
        }

        //Get: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = new CategoryService();

            if (service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Category Created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Category could not be added");
            return View(model);
        }

        //Get: Category/Detail
        public ActionResult Details(int id)
        {
            var service = new CategoryService();
            var category = service.GetCategory(id);
            return View(category);
        }

        //Get: Category/Edit
        public ActionResult Edit(int id)
        {
            var service = new CategoryService();
            var detail = service.GetCategory(id);
            var category = new CategoryEdit() { Name = detail.Name };
            return View(category);
        }

        //Post: Category/Edit
        [HttpPost]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            var service = new CategoryService();
            if (!ModelState.IsValid)
                return View(model);

            if (service.UpdateCategory(id, model))
            {

                TempData["SaveResult"] = "Category Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Category could not be updated");
            return View(model);
        }

        //Get: Category/Delete
        public ActionResult Delete(int id)
        {
            var service = new CategoryService();
            var category = service.GetCategory(id);
            return View(category);
        }

        //Post: Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = new CategoryService();

            if (service.DeleteCategory(id))
            {
                TempData["SaveResult"] = "Category Deleted";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Category could not be updated");
            var model = service.GetCategory(id);
            return View(model);
        }
    }
}