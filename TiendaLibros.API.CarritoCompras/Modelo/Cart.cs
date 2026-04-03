namespace TiendaLibros.API.CarritoCompras.Modelo
{
    public class Cart
    {
        public int CartID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<CartProduct> CartProductList { get; set; } = []; // Por defecto el carrito estaría vacío.

    }
}
