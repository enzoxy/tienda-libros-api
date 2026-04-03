namespace TiendaLibros.API.CarritoCompras.DTOs
{
    public class CartDtoResponse
    {
        public int CartID { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CartProductDtoResponse> CartProductList { get; set; }
    }
}
