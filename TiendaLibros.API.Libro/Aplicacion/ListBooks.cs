using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Libro.DTOs;
using TiendaLibros.API.Libro.Modelo;
using TiendaLibros.API.Libro.Persistencia;

namespace TiendaLibros.API.Libro.Aplicacion
{
    public class ListBooks
    {
        public class Query : IRequest<List<BookResponseDto>> { }

        public class Handler : IRequestHandler<Query, List<BookResponseDto>>
        {
            public readonly BookContext _context;
            public readonly IMapper _mapper;

            public Handler(BookContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<BookResponseDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var books = await _context.Books.ToListAsync();
                var booksDTO = _mapper.Map<List<Book>, List<BookResponseDto>>(books);

                return booksDTO;
            }

        }

    }
}
