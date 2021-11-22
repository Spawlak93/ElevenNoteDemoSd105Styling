namespace ElevenNoteDemo.Models
{
    public class NoteEdit
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public bool IsStarred { get; set; }
    }
}
