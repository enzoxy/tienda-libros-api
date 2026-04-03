using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Libro.Aplicacion;
using TiendaLibros.API.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddMediatR(typeof(NewBook.Handler).Assembly);
builder.Services.AddAutoMapper(typeof(ListBooks.Handler));
builder.Services.AddAutoMapper(typeof(FindBook.Handler));

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
