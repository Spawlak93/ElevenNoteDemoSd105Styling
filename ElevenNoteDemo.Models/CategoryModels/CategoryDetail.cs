using System.Collections.Generic;

namespace ElevenNoteDemo.Models.CategoryModels
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public List<NoteListItem> Notes { get; set; } 
    }
}
