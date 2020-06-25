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

            CreateMap<Bono, BonoDetailDto>()
                .ForMember(dest => dest.Destinatario, opt => opt.MapFrom(src => $"{src.NombreDestino} {src.ApellidoDestino}"))
                .ForMember(dest => dest.Remitente, opt => opt.MapFrom(src => src.Cliente.NombreCompleto))
                .ForMember(dest => dest.EstadoBono, opt => opt.MapFrom(src => src.EstadoBono.Descripcion))
                .ForMember(dest => dest.SimboloMonedaOriginal, opt => opt.MapFrom(src => src.Tasa.Moneda.Simbolo))
                .ForMember(dest => dest.Tasa, opt => opt.MapFrom(src => src.Tasa.Valor))
                .ForMember(dest => dest.CedulaRemitente, opt => opt.MapFrom(src => src.Cliente.Cedula))
                .ForMember(dest => dest.telefonoRemitente, opt => opt.MapFrom(src => src.Cliente.Celular))
                .ForMember(dest => dest.TelefonoDestino, opt => opt.MapFrom(src => src.TelefonoDestino));
        }

    }
}