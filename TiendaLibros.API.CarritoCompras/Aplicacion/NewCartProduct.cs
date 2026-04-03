using MediatR;
using TiendaLibros.API.CarritoCompras.Modelo;
using TiendaLibros.API.CarritoCompras.Persistencia;

namespace TiendaLibros.API.CarritoCompras.Aplicacion
{
    public class NewCartProduct
    {
        public class CommandCP : IRequest<CartProduct>
        {
            public required DateTime createdDate { get; set; }
            public int cartID { get; set; }
            public required string selectedBookProductGUID { get; set; }
        }

        public class Handler : IRequestHandler<CommandCP, CartProduct>
        {
            public readonly CartContext _context;

            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<CartProduct> Handle(CommandCP req, CancellationToken cancellationToken)
            {

                var cart = await _context.Carts.FindAsync(req.cartID);

                if (cart == null) {
                    throw new HttpRequestException($"The Cart where the Product is wanted to add doesn't exist (CartID={req.cartID}).", null, System.Net.HttpStatusCode.NotFound);
                }

                var cartProduct = new CartProduct
                {
                    CreatedDate = req.createdDate,
                    CartID = req.cartID,
                    SelectedBookProductGUID = req.selectedBookProductGUID
                };

                var cartProductCreated = _context.CartProducts.Add(cartProduct);

                int db_res = await _context.SaveChangesAsync();
                if (db_res <= 0)
                    throw new Exception("Error on inserting new CartProduct into database.");

                return cartProductCreated.Entity;

            }

        }

    }
}
