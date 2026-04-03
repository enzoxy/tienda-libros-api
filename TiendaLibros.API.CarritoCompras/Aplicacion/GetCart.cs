using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.CarritoCompras.DTOs;
using TiendaLibros.API.CarritoCompras.InterfazRemota;
using TiendaLibros.API.CarritoCompras.Modelo;
using TiendaLibros.API.CarritoCompras.Persistencia;

namespace TiendaLibros.API.CarritoCompras.Aplicacion
{
    public class GetCart
    {
        public class Query : IRequest<CartDtoResponse>
        {
            public required int cartID { get; set; }
        }

        public class Handler : IRequestHandler<Query, CartDtoResponse>
        {
            private readonly CartContext _context;
            private readonly IBookService _bookService;
            private readonly IAuthorService _authorService;

            public Handler(CartContext context, IBookService bookService, IAuthorService authorService)
            {
                _context = context;
                _bookService = bookService;
                _authorService = authorService;
            }

            public async Task<CartDtoResponse> Handle(Query req, CancellationToken cancellationToken)
            {
                var cart = await _context.Carts.FindAsync(req.cartID);

                if (cart == null) {
                    throw new HttpRequestException($"Cart with ID={req.cartID} not found.", null, System.Net.HttpStatusCode.NotFound);
                }

                List<CartProduct> cartProducts = await _context.CartProducts.Where(p => p.CartID == req.cartID).ToListAsync();
                List<CartProductDtoResponse> cartProductDtoList = [];

                foreach (var p in cartProducts)
                {
                    var cartProductDto = new CartProductDtoResponse { CartProductID = p.CartProductID, CreatedDate = p.CreatedDate, SelectedBookProductGUID = p.SelectedBookProductGUID };
                    // Solicitamos los datos del Libro a su Microservicio.
                    var res = await _bookService.getBook(p.SelectedBookProductGUID);
                    if (res.success)
                    {
                        cartProductDto.BookTitle = res.book.Title;
                        cartProductDto.BookPublishedDate = res.book.PublishedDate;
                        cartProductDto.BookAuthorGUID = res.book.BookAuthorGUID;

                        if(cartProductDto.BookAuthorGUID != null)
                        {
                            // Solicitamos los datos del Autor del libro (si lo tenía) a su Microservicio.
                            var res2 = await _authorService.getAuthor(cartProductDto.BookAuthorGUID);
                            if (res2.success)
                            {
                                cartProductDto.BookAuthorFirstName = res2.author.FirstName;
                                cartProductDto.BookAuthorLastName = res2.author.LastName;
                            }
                            else
                            { // El Microservicio no pudo encontrar al Autor.
                                cartProductDto.BookAuthorFirstName = $"[Error] {res2.errorMessage}";
                                cartProductDto.BookAuthorLastName = null;
                            }
                        }
                        else
                        {
                            cartProductDto.BookAuthorFirstName = "Unknown";
                            cartProductDto.BookAuthorLastName = "Unknown";
                        }
                    }
                    else
                    { // El Microservicio no pudo encontrar el Libro.
                        cartProductDto.BookTitle = $"[Error] {res.errorMessage}";
                        cartProductDto.BookPublishedDate = null;
                        cartProductDto.BookAuthorGUID = null;
                    }
                    cartProductDtoList.Add(cartProductDto);
                }

                var cartDTO = new CartDtoResponse { CartID = cart.CartID, CreatedDate = cart.CreatedDate, CartProductList = cartProductDtoList };

                return cartDTO;
            }
        }

    }
}
