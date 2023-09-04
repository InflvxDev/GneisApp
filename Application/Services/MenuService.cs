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
    public class MenuService : IMenuService
    {
        public readonly IGenericRepository<Usuario> _repositoryUsuario;
        public readonly IGenericRepository<MenuRol> _repositoryMenuRol;
        public readonly IGenericRepository<Menu> _repositoryMenu;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> repositoryUsuario, IGenericRepository<MenuRol> repositoryMenuRol, IGenericRepository<Menu> repositoryMenu, IMapper mapper)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryMenuRol = repositoryMenuRol;
            _repositoryMenu = repositoryMenu;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int id)
        {
            IQueryable<Usuario> queryUsuarios = await _repositoryUsuario.Consultar(u => u.Id == id);
            IQueryable<MenuRol> queryMenuRol = await _repositoryMenuRol.Consultar();
            IQueryable<Menu> queryMenu = await _repositoryMenu.Consultar();

            
            try
            {
                IQueryable<Menu> resultados = (
                    from u in queryUsuarios 
                    join mr in queryMenuRol on u.IdRol equals mr.IdRol 
                    join m in queryMenu on mr.IdMenu equals m.Id
                    select m).AsQueryable();

                var listamenus = resultados.ToList(); 
                return _mapper.Map<List<MenuDTO>>(listamenus);   
            }   
            catch
            {
                throw;
            }
        }
    }
}
