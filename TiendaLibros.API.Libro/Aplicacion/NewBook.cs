using MediatR;
using TiendaLibros.API.Libro.Modelo;
using TiendaLibros.API.Libro.Persistencia;

namespace TiendaLibros.API.Libro.Aplicacion
{
    public class NewBook
    {
        public class Command : IRequest
        {
            public required string title { get; set; }
            public DateTime? publishedDate { get; set; }
            public string? bookAuthorGUID { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public readonly BookContext _context;

            public Handler(BookContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(Command req, CancellationToken cancellationToken)
            {
                var book = new Book
                {
                    Title = req.title,
                    PublishedDate = req.publishedDate,
                    BookAuthorGUID = req.bookAuthorGUID,
                    BookGUID = Convert.ToString(Guid.NewGuid())
                };

                _context.Books.Add(book);
                int db_res = await _context.SaveChangesAsync();

                if (db_res <= 0) throw new Exception("Error on inserting new Book into database.");

                return Unit.Value;
            }

        }
    }
}
