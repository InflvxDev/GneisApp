using AutoMapper;
using Infrastructure.Dtos;
using Domain.Entities.Gneis;
using System.Globalization;

namespace Infrastructure.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol

            CreateMap<Rol, RolDTO>().ReverseMap();

            #endregion Rol

            #region Menu

            CreateMap<Menu, MenuDTO>().ReverseMap();

            #endregion Menu

            #region Usuario

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.DescripcionRol,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation!.Nombre)
                 )
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 );
            
            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.DescripcionRol,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation!.Nombre)
                );
            
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 );

            #endregion Usuario
        
            #region Tipo

            CreateMap<TipoHabitacion, TipoDTO>().ReverseMap();

            #endregion Tipo
        
            #region Habitacion

            CreateMap<Habitacion, HabitacionDTO>()
                .ForMember(destino =>
                    destino.DescripcionTipo,
                    opt => opt.MapFrom(origen => origen.IdTipoNavigation!.Nombre)
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio!.Value, new CultureInfo("es-CO")))
                );

            CreateMap<HabitacionDTO, Habitacion>()
                .ForMember(destino =>
                    destino.IdTipoNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDouble(origen.Precio, new CultureInfo("es-CO")))
                );    

            #endregion Habitacion

            #region Reserva

            CreateMap<Reserva, ReservaDTO>()
                .ForMember(destino =>
                    destino.FechaReserva,
                    opt => opt.MapFrom(origen => origen.FechaReserva.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.CedulaNavigation!.Nombre)
                )
                .ForMember(destino =>
                    destino.DescripcionHabitacion,
                    opt => opt.MapFrom(origen => origen.IdHabitacionNavigation!.Descripcion)
                )
                .ForMember(destino =>
                    destino.CostoDia,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdHabitacionNavigation.Precio.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                    destino.FechaEntrada,
                    opt => opt.MapFrom(origen => origen.FechaEntrada!.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.FechaSalida,
                    opt => opt.MapFrom(origen => origen.FechaSalida!.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total!.Value, new CultureInfo("es-CO")))
                );
            
            CreateMap<ReservaDTO, Reserva>()
                .ForMember(destino =>
                    destino.FechaReserva,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaReserva!, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.CedulaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdHabitacionNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.FechaEntrada,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaEntrada!, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.FechaSalida,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaSalida!, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDouble(origen.Total, new CultureInfo("es-CO")))
                );

            #endregion Reserva

            #region Reporte

            CreateMap<Reserva, ReporteDTO>()
                .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.FechaReserva.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.TotalReserva,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total!.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                    destino.Habitacion,
                    opt => opt.MapFrom(origen => origen.IdHabitacionNavigation!.Descripcion)
                )
                .ForMember(destino =>
                    destino.CostoDia,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdHabitacionNavigation.Precio.Value, new CultureInfo("es-CO")))
                );

            #endregion Reporte
        }
    }
}
