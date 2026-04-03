using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Libro.DTOs;
using TiendaLibros.API.Libro.Modelo;
using TiendaLibros.API.Libro.Persistencia;

namespace TiendaLibros.API.Libro.Aplicacion
{
    public class FindBook
    {
        public class Query : IRequest<BookResponseDto>
        {
            public required string bookGUID { get; set; }
        }

        public class Handler : IRequestHandler<Query, BookResponseDto>
        {
            public readonly BookContext _context;
            private readonly IMapper _mapper;

            public Handler(BookContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookResponseDto> Handle(Query req, CancellationToken cancellationToken)
            {
                var book = await _context.Books
                              .Where(x => x.BookGUID == req.bookGUID)
                              .FirstOrDefaultAsync();

                if (book == null) {
                    throw new HttpRequestException("Book with given GUID not found.", null, System.Net.HttpStatusCode.NotFound);
                }

                var bookDTO = _mapper.Map<Book, BookResponseDto>(book);

                return bookDTO;
            }
        }

    }
}
