using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.CarritoCompras.Aplicacion;
using TiendaLibros.API.CarritoCompras.Persistencia;
using NLog.Web;
using TiendaLibros.API.CarritoCompras.InterfazRemota;
using TiendaLibros.API.CarritoCompras.ServicioRemoto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CartContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddMediatR(typeof(NewCart.Handler).Assembly);
builder.Services.AddMediatR(typeof(NewCartProduct.Handler).Assembly);

builder.Services.AddHttpClient("Authors", config => { // Conexión HTTP hacia el Microservicio de Autores.
    config.BaseAddress = new Uri(builder.Configuration.GetSection("Services")["MS_Autor"]);
});

builder.Services.AddHttpClient("Books", config => { // Conexión HTTP hacia el Microservicio de Libros.
    config.BaseAddress = new Uri(builder.Configuration.GetSection("Services")["MS_Libro"]);
});

builder.Services.AddScoped<IAuthorService, AuthorService>(); // Registramos nuestro servicio de Autores.
builder.Services.AddScoped<IBookService, BookService>(); // Registramos nuestro servicio de Libros.

// Utilizaremos el sistema de registro de eventos NLog, en lugar del por defecto de .Net
//builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
