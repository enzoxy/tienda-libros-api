namespace TiendaLibros.API.CarritoCompras.ModeloRemoto
{
    public class AuthorModel
    {
        // Esquema de la respuesta del Microservicio de Autores al hacer un HTTP GET Autor.
        public string AuthorGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
