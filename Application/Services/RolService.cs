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
    public class RolService : IRolService
    {
        public readonly IGenericRepository<Rol> _repository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var ListaRoles = await _repository.Consultar();
                return _mapper.Map<List<RolDTO>>(ListaRoles.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
