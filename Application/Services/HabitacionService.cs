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
    public class HabitacionService : IHabitacionService
    {
        public readonly IGenericRepository<Habitacion> _repository;
        private readonly IMapper _mapper;

        public HabitacionService(IGenericRepository<Habitacion> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(HabitacionDTO habitacion)
        {
            try
            {
                var HabitacionModelo = _mapper.Map<Habitacion>(habitacion);
                var HabitacionEncontrada = await _repository.Obtener(habitacion => habitacion.Id == HabitacionModelo.Id);   

                if (HabitacionEncontrada == null)
                {
                    throw new TaskCanceledException("Habitacion no Registrado");
                }

                HabitacionEncontrada.Descripcion = HabitacionModelo.Descripcion;
                HabitacionEncontrada.IdTipo = HabitacionModelo.IdTipo;
                HabitacionEncontrada.Precio = HabitacionModelo.Precio;
                HabitacionEncontrada.Disponibilidad = HabitacionModelo.Disponibilidad;
                HabitacionEncontrada.Usos = HabitacionModelo.Usos;

                bool respuesta = await _repository.Actualizar(HabitacionEncontrada);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al actualizar la Habitacion");
                }

                return respuesta;
            }
            catch
            {  
                throw;
            }
        }

        public async Task<bool> Eliminar(int idhabitacion)
        {
            try
            {
                var habitacionEncontrada = await _repository.Obtener(habitacion => habitacion.Id == idhabitacion);

                if (habitacionEncontrada == null)
                {
                    throw new TaskCanceledException("Habitacion no Registrado");
                }

                bool respuesta = await _repository.Eliminar(habitacionEncontrada);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al eliminar la Habitacion");
                }

                return respuesta;

            }
            catch
            { 
                throw;
            }
        }

        public async Task<HabitacionDTO> Guardar(HabitacionDTO habitacion)
        {
            try
            {
                var habitacionCreada = await _repository.Guardar(_mapper.Map<Habitacion>(habitacion));

                if (habitacionCreada == null)
                {
                    throw new TaskCanceledException("Error al guardar la Habitacion");
                }

                var query = await _repository.Consultar(habitacion => habitacion.Id == habitacionCreada.Id);
                habitacionCreada = query.Include(tipo => tipo.IdTipoNavigation).First();

                return _mapper.Map<HabitacionDTO>(habitacionCreada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<HabitacionDTO>> Lista()
        {
            try {
                var queryHabitaciones = await _repository.Consultar();
                var listaHabitaciones = queryHabitaciones.Include(tipo => tipo.IdTipoNavigation).ToList();
                return _mapper.Map<List<HabitacionDTO>>(listaHabitaciones);
            }catch{
                throw;
            }
        }
    }
}
