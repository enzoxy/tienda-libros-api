using MediatR;
using TiendaLibros.API.CarritoCompras.Modelo;
using TiendaLibros.API.CarritoCompras.Persistencia;

namespace TiendaLibros.API.CarritoCompras.Aplicacion
{
    public class NewCart
    {
        public class CommandC : IRequest<Cart>
        {
            public DateTime createdDate { get; set; }
        }

        public class Handler : IRequestHandler<CommandC, Cart>
        {
            public readonly CartContext _context;

            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<Cart> Handle(CommandC req, CancellationToken cancellationToken)
            {
                var cart = new Cart { CreatedDate = req.createdDate };

                var cartCreated = _context.Carts.Add(cart);

                int db_res = await _context.SaveChangesAsync();
                if (db_res <= 0)
                    throw new Exception("Error on inserting new Cart into database.");

                return cartCreated.Entity;

            }

        }

    }
}
