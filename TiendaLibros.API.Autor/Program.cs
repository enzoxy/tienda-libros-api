using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Autor.Aplicacion;
using TiendaLibros.API.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AuthorContext>(options => { // Agregamos la conexión a la DB.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddMediatR(typeof(NewAuthor.Handler).Assembly); // Agregamos el servicio de MediatR que maneja la creación de autores.

builder.Services.AddAutoMapper(typeof(ListAuthors.Handler)); // Agregamos el servicio de AutoMapper para las respuestas a consultas de lectura.
builder.Services.AddAutoMapper(typeof(FindAuthor.Handler));

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
