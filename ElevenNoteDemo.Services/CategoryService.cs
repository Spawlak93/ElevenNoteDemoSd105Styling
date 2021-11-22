using ElevenNoteDemo.Data;
using ElevenNoteDemo.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNoteDemo.Services
{
    public class CategoryService
    {
        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx
                    .Categories
                    .Select(e => new CategoryListItem
                    {
                        Name = e.Name,
                        CategoryId = e.CategoryId
                    }).ToList();
            }
        }

        public CategoryDetail GetCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .SingleOrDefault(e => e.CategoryId == id);
                var category = new CategoryDetail()
                {
                    CategoryId = entity.CategoryId,
                    Name = entity.Name,
                    Notes = entity.Notes.Select(e => new Models.NoteListItem()
                    {
                        Title = e.Title,
                        CategoryName = e.Category.Name,
                        CategoryId = e.CategoryId,
                        CreatedUtc = e.CreatedUtc,
                        NoteId = e.NoteId
                    }).ToList()
                };

                return category;
            }
        }

        public bool UpdateCategory(int id, CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == id);
                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == id);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
