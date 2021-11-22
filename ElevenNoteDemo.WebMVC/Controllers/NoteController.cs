using ElevenNoteDemo.Models;
using ElevenNoteDemo.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNoteDemo.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note Index
        public ActionResult Index()
        {
            var service = CreateNoteService();
            var model = service.GetNotes();
            PopulateCategories();
            return View(model);
        }
        //Get: Note/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //Get: Note/Create
        public ActionResult Create()
        {
            PopulateCategories();
            return View();
        }
        
        //Post: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if(!ModelState.IsValid)
            {
                PopulateCategories();
                return View(model);
            }

            var service = CreateNoteService();
            if(service.CreateNote(model))
            {
                ViewBag.SaveResult = "Your note was created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");
            PopulateCategories();
            return View(model);
        }
        //Get: Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                    CategoryId = detail.CategoryId,
                    IsStarred = detail.IsStarred
                };
            PopulateCategories();
            return View(model);
        }

        //Post: Note/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                PopulateCategories();
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            PopulateCategories();
            return View(model);
        }

        //Get: Note/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //Post: Note/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new NoteService(userId);
        }

        private void PopulateCategories()
        {
            ViewBag.CategoryIds = new SelectList(new CategoryService().GetCategories(), "CategoryId", "Name");
        }

    }
}