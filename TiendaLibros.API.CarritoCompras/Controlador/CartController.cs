using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaLibros.API.CarritoCompras.Aplicacion;
using TiendaLibros.API.CarritoCompras.DTOs;
using TiendaLibros.API.CarritoCompras.Modelo;

namespace TiendaLibros.API.CarritoCompras.Controlador
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(NewCart.CommandC data)
        {
            try {
                return await _mediator.Send(data);
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
        }

        [HttpPost("{cartID}/product")]
        public async Task<ActionResult<CartProduct>> AddProduct([FromRoute] int cartID, [FromBody] NewCartProduct.CommandCP data)
        {
            try {
                data.cartID = cartID;
                return await _mediator.Send(data);
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDtoResponse>> RetrieveCart(int id)
        {
            try {
                return await _mediator.Send(new GetCart.Query { cartID = id });
            }
            catch (Exception e) {
                return new ErrorResponse(e);
            }

        }

    }
}
