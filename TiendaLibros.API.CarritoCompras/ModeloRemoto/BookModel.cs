namespace TiendaLibros.API.CarritoCompras.ModeloRemoto
{
    public class BookModel
    {
        // Esquema de la respuesta del Microservicio de Libros al hacer un HTTP GET Libro.
        public string BookGUID { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? BookAuthorGUID { get; set; }
    }
}
