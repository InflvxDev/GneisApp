using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Repositories.Contracts;
using Infrastructure.DBcontext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly PostgresContext _dBcontext;

        public GenericRepository(PostgresContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public async Task<bool> Actualizar(TModel modelo)
        {
            try
            {
                _dBcontext.Set<TModel>().Update(modelo);
                await _dBcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModel> queryModelo = filtro == null 
                    ? _dBcontext.Set<TModel>() 
                    : _dBcontext.Set<TModel>().Where(filtro);
                return queryModelo; 
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModel modelo)
        {
            try
            {
                _dBcontext.Set<TModel>().Remove(modelo);
                await _dBcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Guardar(TModel modelo)
        {
            try
            {
                _dBcontext.Set<TModel>().Add(modelo);
                await _dBcontext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro)
        {
            try { 
                TModel model = await _dBcontext.Set<TModel>().FirstOrDefaultAsync(filtro);
                return model;
            } 
            catch {
                throw;
            }
        }
    }
}
