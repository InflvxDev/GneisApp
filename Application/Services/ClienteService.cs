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
    public class ClienteService : IClienteService
    {
        public readonly IGenericRepository<Cliente> _repository;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(ClienteDTO cliente)
        {
            try
            {
                var ClienteModelo = _mapper.Map<Cliente>(cliente);
                var ClienteEncontrado = await _repository.Obtener(cliente => cliente.Cedula == ClienteModelo.Cedula);   

                if (ClienteEncontrado == null)
                {
                    throw new TaskCanceledException("Cliente no Registrado");
                }

                ClienteEncontrado.Cedula = ClienteModelo.Cedula;
                ClienteEncontrado.Nombre = ClienteModelo.Nombre;
                ClienteEncontrado.Edad = ClienteModelo.Edad;
                ClienteEncontrado.Sexo = ClienteModelo.Sexo;
                ClienteEncontrado.Telefono = ClienteModelo.Telefono;

                bool respuesta = await _repository.Actualizar(ClienteEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al actualizar el cliente");
                }

                return respuesta;
            }
            catch
            {  
                throw;
            }
        }

        public async Task<bool> Eliminar(int idcliente)
        {
            try
            {
                var clienteEncontrado = await _repository.Obtener(cliente => cliente.Cedula == idcliente.ToString());

                if (clienteEncontrado == null)
                {
                    throw new TaskCanceledException("Cliente no Registrado");
                }

                bool respuesta = await _repository.Eliminar(clienteEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al eliminar el Cliente");
                }

                return respuesta;

            }
            catch
            { 
                throw;
            }
        }

        public async Task<ClienteDTO> Guardar(ClienteDTO cliente)
        {
            try
            {
                var clienteCreado = await _repository.Guardar(_mapper.Map<Cliente>(cliente));

                if (clienteCreado == null)
                {
                    throw new TaskCanceledException("Error al guardar el cliente");
                }

                var query = await _repository.Consultar(cliente => cliente.Cedula == clienteCreado.Cedula);
                
                return _mapper.Map<ClienteDTO>(clienteCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            try {
                var queryClientes = await _repository.Consultar();
                var listaClientes = queryClientes.ToList();
                return _mapper.Map<List<ClienteDTO>>(listaClientes);
            }catch{
                throw;
            }
        }
    }
}
