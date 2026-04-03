using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Autor.Modelo;

namespace TiendaLibros.API.Autor.Persistencia
{
    public class AuthorContext : DbContext
    {
        // Definición de tablas de nuestra DB.
        public DbSet<Author> Authors { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }

        // Config. de operaciones que podremos hacer sobre la DB.
        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options) {

        }

    }
}
