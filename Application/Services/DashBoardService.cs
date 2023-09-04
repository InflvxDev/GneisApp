using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Application.Services.Contracts;
using Infrastructure.Repositories.Contracts;
using Infrastructure.Dtos;
using Domain.Entities.Gneis;
using System.Globalization;

namespace Application.Services
{
    public class DashBoardService : IDashBoardService
    {
        public readonly IReservaRepository _repositoryReserva;
        public readonly IGenericRepository<Habitacion> _repository;
        private readonly IMapper _mapper;

        public DashBoardService(IReservaRepository repositoryReserva, IGenericRepository<Habitacion> repository, IMapper mapper)
        {
            _repositoryReserva = repositoryReserva;
            _repository = repository;
            _mapper = mapper;
        }

        private IQueryable<Reserva> retornarReservas(IQueryable<Reserva> reservas, int diasRestar){
            DateTime? ultimaFecha = reservas.OrderByDescending(x => x.FechaReserva).Select(x => x.FechaReserva).First();

            ultimaFecha = ultimaFecha?.AddDays(diasRestar);

            return reservas.Where(x => x.FechaReserva >= ultimaFecha);
        }

        private async Task<int> TotalReservasUltimaSemana () {
            int total = 0;
            IQueryable<Reserva> reservasQuery = await _repositoryReserva.Consultar();

            if (reservasQuery.Count() > 0) {
                var tablaReservas = retornarReservas(reservasQuery, -7);
                total = tablaReservas.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana () {
            double resultado = 0;
            IQueryable<Reserva> reservasQuery = await _repositoryReserva.Consultar();

            if (reservasQuery.Count() > 0) {
                var tablaReservas = retornarReservas(reservasQuery, -7);
                resultado = tablaReservas.Select(x => x.Total).Sum(x => x!.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-CO"));
        }

        private async Task<int> TotalReservas (){
            IQueryable<Reserva> reservasQuery = await _repositoryReserva.Consultar();
            int total = reservasQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> ReservasUltimaSemana (){
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Reserva> reservasQuery = await _repositoryReserva.Consultar();

            if(reservasQuery.Count() > 0){
                var tablaReservas = retornarReservas(reservasQuery, -7);

                resultado = tablaReservas
                    .GroupBy(x => x.FechaReserva)
                    .OrderBy(x => x.Key)
                    .Select(x => new { fecha = x.Key.ToString("dd/MM/yyyy"), total = x.Count() })
                    .ToDictionary(keySelector: x => x.fecha, elementSelector: x => x.total);
                
            }

            return resultado;
        }
 

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();

            try {

                vmDashboard.TotalReservas = await TotalReservas();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalReservasUltimaSemana = await TotalReservasUltimaSemana();

                List<ReservaSemanaDTO> listareservas = new List<ReservaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await ReservasUltimaSemana())
                {
                    ReservaSemanaDTO reserva = new ReservaSemanaDTO();
                    reserva.Fecha = item.Key;
                    reserva.Total = item.Value.ToString();
                    listareservas.Add(reserva);
                }

                vmDashboard.ReservasUltimaSemana = listareservas;

            } catch{
                throw;
            }

            return vmDashboard;
        }
    }
}
