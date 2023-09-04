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
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ReservaService : IReservaService
    {
        public readonly IReservaRepository _repositoryReserva;
        public readonly IGenericRepository<Reserva> _repository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository repositoryReserva, IGenericRepository<Reserva> repository, IMapper mapper)
        {
            _repositoryReserva = repositoryReserva;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CheckIn(ReservaDTO reserva)
        {
            try
            {
                var ReservaModelo = _mapper.Map<Reserva>(reserva);
                var ReservaEncontrada = await _repository.Obtener(reserva => reserva.Id == ReservaModelo.Id);   

                if (ReservaEncontrada == null)
                {
                    throw new TaskCanceledException("Reserva no Registrada");
                }

                bool respuesta = await _repositoryReserva.CheckIn(ReservaEncontrada);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al realizar el checkIn");
                }

                return respuesta;
            }
            catch
            {  
                throw;
            }
        }

        public async Task<bool> CheckOut(ReservaDTO reserva)
        {
            try
            {
                var ReservaModelo = _mapper.Map<Reserva>(reserva);
                var ReservaEncontrada = await _repository.Obtener(reserva => reserva.Id == ReservaModelo.Id);   

                if (ReservaEncontrada == null)
                {
                    throw new TaskCanceledException("Reserva no Registrada");
                }

                bool respuesta = await _repositoryReserva.CheckOut(ReservaEncontrada);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al realizar el checkOut");
                }

                return respuesta;
            }
            catch
            {  
                throw;
            }
        }

        public async Task<ReservaDTO> Registrar(ReservaDTO reserva)
        {
            try
            {
                var reservaCreada = await _repositoryReserva.Registrar(_mapper.Map<Reserva>(reserva));

                if (reservaCreada == null)
                {
                    throw new TaskCanceledException("Error al guardar la Reserva");
                }

                var query = await _repository.Consultar(reserva => reserva.Id == reservaCreada.Id);
                reservaCreada = query.Include(cliente => cliente.CedulaNavigation).First();
                reservaCreada = query.Include(habitacion => habitacion.IdHabitacionNavigation).First();

                return _mapper.Map<ReservaDTO>(reservaCreada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ReservaDTO>> Historial(string buscarPor, string idReserva, string fechaInicio, string fechaFin)
        {
            IQueryable<Reserva> query = await _repository.Consultar();

            var listResultados = new List<Reserva>();

            try
            {
                if(buscarPor == "fecha"){
                    DateTime fechaInicioDate = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime fechaFinDate = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    listResultados = await query.Where(reserva => reserva.FechaReserva >= fechaInicioDate && reserva.FechaReserva <= fechaFinDate)
                        .Include(cliente => cliente.CedulaNavigation)
                        .Include(habitacion => habitacion.IdHabitacionNavigation)
                        .ToListAsync();
                     
                }else{
                    listResultados = await query.Where(reserva => reserva.Id.ToString() == idReserva)
                        .Include(cliente => cliente.CedulaNavigation)
                        .Include(habitacion => habitacion.IdHabitacionNavigation)
                        .ToListAsync();
                }
                
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReservaDTO>>(listResultados);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<Reserva> query = await _repository.Consultar();

            var listResultados = new List<Reserva>();

            try
            {
                
                DateTime fechaInicioDate = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime fechaFinDate = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                listResultados = await query.Where(reserva => reserva.FechaReserva >= fechaInicioDate && reserva.FechaReserva <= fechaFinDate)
                    .Include(cliente => cliente.CedulaNavigation)
                    .Include(habitacion => habitacion.IdHabitacionNavigation)
                    .ToListAsync();

            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listResultados);
        }
    }
}
