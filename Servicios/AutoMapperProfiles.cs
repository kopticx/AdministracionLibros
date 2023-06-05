using AdministracionLibros.Models;
using AutoMapper;

namespace AdministracionLibros.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Libro, LibroDetallesViewModel>();
        }
    }
}
