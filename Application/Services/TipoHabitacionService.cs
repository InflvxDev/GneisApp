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

namespace Application.Services
{
    public class TipoHabitacionService : ITipoHabitacionService
    {
        public readonly IGenericRepository<TipoHabitacion> _repository;
        private readonly IMapper _mapper;

        public TipoHabitacionService(IGenericRepository<TipoHabitacion> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TipoDTO>> Lista()
        {
            try
            {
                var ListaTiposHabitaciones = await _repository.Consultar();
                return _mapper.Map<List<TipoDTO>>(ListaTiposHabitaciones.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
