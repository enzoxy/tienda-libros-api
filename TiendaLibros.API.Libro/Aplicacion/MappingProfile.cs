using AutoMapper;
using TiendaLibros.API.Libro.DTOs;
using TiendaLibros.API.Libro.Modelo;

namespace TiendaLibros.API.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResponseDto>();
        }
    }
}
