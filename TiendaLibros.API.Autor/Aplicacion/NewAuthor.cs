using MediatR;
using TiendaLibros.API.Autor.Modelo;
using TiendaLibros.API.Autor.Persistencia;

namespace TiendaLibros.API.Autor.Aplicacion
{
    public class NewAuthor
    {
        // Definimos qué nos deberá enviar el usuario para ejecutar la operación de escritura (datos del autor).
        public class Command : IRequest
        {
            public required string firstName { get; set; }
            public required string lastName { get; set; }
            public DateTime? birthDate { get; set; }
        }

        // Implementamos el manejo de la petición de escritura del usuario.
        public class Handler : IRequestHandler<Command>
        {
            public readonly AuthorContext _context;

            public Handler(AuthorContext context) { // inyección de dependencias por constructor
                _context = context;
            }

            public async Task<Unit> Handle(Command req, CancellationToken cancellationToken) {

                // Creamos la nueva entidad Author.
                Author author = new Author { 
                    FirstName = req.firstName,
                    LastName = req.lastName,
                    BirthDate = req.birthDate,
                    AuthorGUID = Convert.ToString(Guid.NewGuid())
                };

                // Agregamos el autor a la tabla de autores del contexto de la DB.
                _context.Authors.Add(author);
                int db_res = await _context.SaveChangesAsync();

                // Si hubo algún error de parámetros o restricciones, lanzamos una excepción.
                if (db_res <= 0) throw new Exception("Error on inserting new Author into database.");

                return Unit.Value; // esto es equivalente a no retornar ningún valor en especial (void)
            }
        }
    }
}
