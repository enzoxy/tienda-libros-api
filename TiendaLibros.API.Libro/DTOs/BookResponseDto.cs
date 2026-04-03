namespace TiendaLibros.API.Libro.DTOs
{
    public class BookResponseDto
    {
        public string BookGUID { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? BookAuthorGUID { get; set; }
    }
}
