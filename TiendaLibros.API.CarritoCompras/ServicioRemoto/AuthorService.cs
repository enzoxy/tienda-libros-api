using System.Text.Json;
using TiendaLibros.API.CarritoCompras.InterfazRemota;
using TiendaLibros.API.CarritoCompras.ModeloRemoto;

namespace TiendaLibros.API.CarritoCompras.ServicioRemoto
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IHttpClientFactory httpClient, ILogger<AuthorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool success, AuthorModel? author, string? errorMessage)> getAuthor(string authorGUID)
        {
            var AuthorHttpClient = _httpClient.CreateClient("Authors");
            var JsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            try
            {
                var ResponseBody = await AuthorHttpClient.GetFromJsonAsync<AuthorModel>($"api/author/{authorGUID}", JsonOptions);
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
