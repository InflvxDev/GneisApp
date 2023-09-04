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
    public class UsuarioService : IUsuarioService
    {
        public readonly IGenericRepository<Usuario> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(UsuarioDTO usuario)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(usuario);
                var usuarioEncontrado = await _repository.Obtener(usuario => usuario.Id == usuarioModelo.Id);   

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario no Registrado");
                }

                usuarioEncontrado.Nombre = usuarioModelo.Nombre;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _repository.Actualizar(usuarioEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al actualizar el usuario");
                }

                return respuesta;



            }
            catch
            {  
                throw;
            }
        }

        public async Task<bool> Eliminar(int IdUsuario)
        {
            try
            {
                var usuarioEncontrado = await _repository.Obtener(usuario => usuario.Id == IdUsuario);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario no Registrado");
                }

                bool respuesta = await _repository.Eliminar(usuarioEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("Error al eliminar el usuario");
                }

                return respuesta;

            }
            catch
            { 
                throw;
            }
        }

        public async Task<UsuarioDTO> Guardar(UsuarioDTO usuario)
        {
            try
            {
                var usuarioCreado = await _repository.Guardar(_mapper.Map<Usuario>(usuario));

                if (usuarioCreado == null)
                {
                    throw new TaskCanceledException("Error al guardar el usuario");
                }

                var query = await _repository.Consultar(usuario => usuario.Id == usuarioCreado.Id);
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try {
                var queryUsuarios = await _repository.Consultar();
                var listaUsuarios = queryUsuarios.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }catch{
                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
               var queryUsuarios = await _repository.Consultar(usuario => usuario.Correo == correo && usuario.Clave == clave); 
               if (queryUsuarios.FirstOrDefault() == null) {
                    throw new TaskCanceledException("Las credenciales no coinciden");
               }

                Usuario devolverUsuario = queryUsuarios.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SesionDTO>(devolverUsuario);
              

            }catch{
                throw;
            }
        }
    }
}
