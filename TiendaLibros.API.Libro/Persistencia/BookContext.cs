using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Libro.Modelo;

namespace TiendaLibros.API.Libro.Persistencia
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options) {

        }

    }
}
