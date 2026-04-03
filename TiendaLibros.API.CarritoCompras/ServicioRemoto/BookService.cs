using System.Text.Json;
using TiendaLibros.API.CarritoCompras.InterfazRemota;
using TiendaLibros.API.CarritoCompras.ModeloRemoto;

namespace TiendaLibros.API.CarritoCompras.ServicioRemoto
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<BookService> _logger;

        public BookService(IHttpClientFactory httpClient, ILogger<BookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool success, BookModel? book, string? errorMessage)> getBook(string bookGUID)
        {
            var BookHttpClient = _httpClient.CreateClient("Books");
            var JsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            try
            {
                var ResponseBody = await BookHttpClient.GetFromJsonAsync<BookModel>($"api/book/{bookGUID}", JsonOptions);
                return (true, ResponseBody, null);
            }
            catch (Exception e)
            {
                if (e is HttpRequestException)
                    _logger?.LogWarning(e.Message); // Los errores propios de HTTP, como 404, los loggeamos como 'warning'.
                else
                    _logger?.LogError(e.Message);

                return (false, null, e.Message);
            }
        }

    }
}
