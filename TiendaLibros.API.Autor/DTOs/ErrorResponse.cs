using Microsoft.AspNetCore.Mvc;

namespace TiendaLibros.API.Autor.DTOs
{
    public class ErrorResponse : ObjectResult
    {
        // Encapsulamos el error en un ObjectResult usando las convenciones del RFC-7807.
        public ErrorResponse(Exception error) : base(getDetails(error))
        {
        }

        private static ProblemDetails getDetails(Exception error)
        {
            var details = new ProblemDetails {
                Title = "Unexpected Error",
                Status = 500,
                Detail = error.Message
            };

            if (error is HttpRequestException e)
            {
                details.Title = "HTTP Request Error";
                details.Status = (int?)e.StatusCode;
            }

            return details;
        }
    }
}
