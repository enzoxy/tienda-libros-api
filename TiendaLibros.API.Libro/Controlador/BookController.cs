using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaLibros.API.Libro.Aplicacion;
using TiendaLibros.API.Libro.DTOs;

namespace TiendaLibros.API.Libro.Controlador
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateBook(NewBook.Command data)
        {
            try {
                return await _mediator.Send(data);
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponseDto>>> RetrieveAllBooks()
        {
            try {
                return await _mediator.Send(new ListBooks.Query());
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<BookResponseDto>> FindBookByGUID(string guid)
        {
            try {
                return await _mediator.Send(new FindBook.Query { bookGUID = guid });
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }
    }
}
