using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Application.Services.Contracts
{
    public interface IReservaService
    {
        Task<List<ReservaDTO>> Historial(string buscarPor, string idReserva, string fechaInicio, string fechaFin);

        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);

        Task<ReservaDTO> Registrar(ReservaDTO reserva);

        Task<bool> CheckIn(ReservaDTO reserva);

        Task<bool> CheckOut(ReservaDTO reserva);

        
    }
}
