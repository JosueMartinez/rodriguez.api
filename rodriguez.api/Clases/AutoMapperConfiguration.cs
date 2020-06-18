using AutoMapper;
using Rodriguez.Data.DTOs;
using Rodriguez.Data.Models;

namespace rodriguez.api.Clases
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("MapperConfg") { }

        protected AutoMapperConfiguration(string profileName) : base(profileName)
        {
            CreateMap<TasaMoneda, TasaDto>()
                .ForMember(dest => dest.Simbolo, opt => opt.MapFrom(src => src.Moneda.Simbolo))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda.Descripcion))
                .ForMember(dest => dest.MonedaId, opt => opt.MapFrom(src => src.Moneda.Id));

            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.rol.Descripcion));
        }

    }
}