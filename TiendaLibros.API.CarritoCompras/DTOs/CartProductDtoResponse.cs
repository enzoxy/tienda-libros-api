namespace TiendaLibros.API.CarritoCompras.DTOs
{
    public class CartProductDtoResponse
    {
        public int CartProductID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SelectedBookProductGUID { get; set; }

        // Datos del Libro (Microservicio de Libros)
        public string BookTitle { get; set; }
        public DateTime? BookPublishedDate { get; set; }
        public string? BookAuthorGUID { get; set; }

        //Datos del Autor (Microservicio de Autores)
        public string? BookAuthorFirstName { get; set; }
        public string? BookAuthorLastName { get; set; }

    }
}
