using TiendaLibros.API.CarritoCompras.ModeloRemoto;

namespace TiendaLibros.API.CarritoCompras.InterfazRemota
{
    public interface IBookService
    {
        Task<(bool success, BookModel? book, string? errorMessage)> getBook(string bookGUID);
    }
}
