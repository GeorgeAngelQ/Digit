using AutoMapper;
using Digitalizacion.EN;

namespace Digitalizacion.UI.Models
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioSistemaModel, UsuarioSistema>();
            CreateMap<UsuarioSistema, UsuarioSistemaModel>();

            CreateMap<EquipoDigitalizacionModel, EquipoDigitalizacion>();
            CreateMap<EquipoDigitalizacion, EquipoDigitalizacionModel>();

            CreateMap<DepartamentoModel, Departamento>();
            CreateMap<Departamento, DepartamentoModel>();

            CreateMap<ResponsableModel, Responsable>();
            CreateMap<Responsable, ResponsableModel>();

            CreateMap<ProcesoModel, Proceso>();
            CreateMap<Proceso, ProcesoModel>();
        }
    }
}
