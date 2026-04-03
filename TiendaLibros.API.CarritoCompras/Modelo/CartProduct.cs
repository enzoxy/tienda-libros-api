using System.Text.Json.Serialization;

namespace TiendaLibros.API.CarritoCompras.Modelo
{
    public class CartProduct
    {
        public int CartProductID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        // clave foránea "interna" (db carrito)
        public required int CartID { get; set; }
        [JsonIgnore] public Cart Cart { get; set; }
        // clave foránea "externa" (db libros)
        public required string SelectedBookProductGUID { get; set; } 

    }
}
