using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaLibros.API.Autor.Aplicacion;
using TiendaLibros.API.Autor.DTOs;

namespace TiendaLibros.API.Autor.Controlador
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator) { // Inyectamos la interfaz del mediador (servicio de MediatR).
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateAuthor(NewAuthor.Command data)
        {
            try {
                return await _mediator.Send(data); // MediatR buscará al manejador de creación autores (debido al tipo de 'data') y retornará la respuesta al usuario.
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDto>>> RetrieveAllAuthors()
        {
            try {
                return await _mediator.Send(new ListAuthors.Query()); // MediatR buscará al manejador de listado de autores.
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<AuthorResponseDto>> FindAuthorByGUID(string guid)
        {
            try {
                return await _mediator.Send(new FindAuthor.Query { authorGUID = guid }); // MediatR buscará al manejador de búsqueda de autores.
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

    }
}
