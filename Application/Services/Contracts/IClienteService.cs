using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> Lista();

        Task<ClienteDTO> Guardar(ClienteDTO cliente);

        Task<bool> Actualizar(ClienteDTO cliente);

        Task<bool> Eliminar(int idcliente);
    }
}
