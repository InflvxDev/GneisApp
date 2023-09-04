using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace Infrastructure.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);

        Task<TModel> Guardar(TModel modelo);

        Task<bool> Actualizar(TModel modelo);

        Task<bool> Eliminar(TModel modelo);

        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);
    }
}
