namespace TiendaLibros.API.Autor.DTOs
{
    // Objeto que finalmente se le devolverá al usuario tras sus peticiones de lectura.
    public class AuthorResponseDto
    {
        public string AuthorGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
