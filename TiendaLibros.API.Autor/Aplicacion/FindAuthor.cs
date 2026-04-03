using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Autor.DTOs;
using TiendaLibros.API.Autor.Modelo;
using TiendaLibros.API.Autor.Persistencia;

namespace TiendaLibros.API.Autor.Aplicacion
{
    public class FindAuthor
    {
        // Definimos cuál será el parámetro de búsqueda y qué responderemos (1 objeto autor).
        public class Query : IRequest<AuthorResponseDto>
        {
            public required string authorGUID { get; set; }
        }

        // Implementamos el manejo de la petición de lectura del usuario.
        public class Handler : IRequestHandler<Query, AuthorResponseDto>
        {
            public readonly AuthorContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorContext context, IMapper mapper) { // inyección de dependencias por constructor
                _context = context;
                _mapper = mapper;
            }

            public async Task<AuthorResponseDto> Handle(Query req, CancellationToken cancellationToken)
            {
                // De la tabla autores buscamos los registros que tengan en el campo GUID el valor buscado, y nos quedamos con la primera ocurrencia (debería ser único..).
                Author? author = await _context.Authors
                                    .Where(x => x.AuthorGUID == req.authorGUID)
                                    .FirstOrDefaultAsync();
                
                if (author == null) {
                    throw new HttpRequestException("Author with given GUID not found.", null, System.Net.HttpStatusCode.NotFound);
                }

                AuthorResponseDto authorDTO = _mapper.Map<Author, AuthorResponseDto>(author);

                return authorDTO; // retornamos el objeto mapeado del autor encontrado
            }
        }
    }
}
