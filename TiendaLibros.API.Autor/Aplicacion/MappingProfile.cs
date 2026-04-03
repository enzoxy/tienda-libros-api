using AutoMapper;
using TiendaLibros.API.Autor.DTOs;
using TiendaLibros.API.Autor.Modelo;

namespace TiendaLibros.API.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorResponseDto>();
        }
    }
}
