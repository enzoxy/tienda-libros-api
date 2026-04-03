namespace TiendaLibros.API.Libro.Modelo
{
    public class Book
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? BookAuthorGUID { get; set; } // Un libro podría ser anónimo, en dicho caso, este campo tendría NULL.

        public required string BookGUID { get; set; }
    }
}
