using TiendaLibros.API.CarritoCompras.ModeloRemoto;

namespace TiendaLibros.API.CarritoCompras.InterfazRemota
{
    public interface IAuthorService
    {
        Task<(bool success, AuthorModel? author, string? errorMessage)> getAuthor(string authorGUID);
    }
}
