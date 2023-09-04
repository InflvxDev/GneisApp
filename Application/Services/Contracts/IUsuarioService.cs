using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Application.Services.Contracts
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();

        Task<SesionDTO> ValidarCredenciales(string correo, string clave);

        Task<UsuarioDTO> Guardar(UsuarioDTO usuario);

        Task<bool> Actualizar(UsuarioDTO usuario);

        Task<bool> Eliminar(int idusuario);
    }
}
