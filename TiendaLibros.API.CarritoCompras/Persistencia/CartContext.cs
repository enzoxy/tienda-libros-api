using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.CarritoCompras.Modelo;

namespace TiendaLibros.API.CarritoCompras.Persistencia
{
    public class CartContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {

        }
    }
}
