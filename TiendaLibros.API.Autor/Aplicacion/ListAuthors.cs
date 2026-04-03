using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Autor.DTOs;
using TiendaLibros.API.Autor.Modelo;
using TiendaLibros.API.Autor.Persistencia;

namespace TiendaLibros.API.Autor.Aplicacion
{
    public class ListAuthors
    {
        // Definimos qué devolveremos al usuario luego de la operación de lectura (lista de todos los autores).
        public class Query: IRequest<List<AuthorResponseDto>>
        {

        }

        // Implementamos el manejo de la petición de lectura del usuario.
        public class Handler : IRequestHandler<Query, List<AuthorResponseDto>>
        {
            public readonly AuthorContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorContext context, IMapper mapper) { // inyección de dependencias por constructor
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AuthorResponseDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                // Pedimos a la DB el listado de todos los autores existentes.
                List<Author> authors = await _context.Authors.ToListAsync();

                // Mapeamos cada entidad autor a dto:
                List<AuthorResponseDto> authorsDTO = _mapper.Map<List<Author>, List<AuthorResponseDto>>(authors);

                return authorsDTO; // directamente retornamos dicha lista de autores mapeada
            }
        }
    }
}
