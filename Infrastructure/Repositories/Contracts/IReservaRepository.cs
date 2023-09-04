using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities.Gneis;

namespace Infrastructure.Repositories.Contracts
{
    public interface IReservaRepository : IGenericRepository<Reserva>
    {
        Task<Reserva> Registrar(Reserva reserva);
        Task<bool> CheckIn(Reserva reserva);
        Task<bool> CheckOut(Reserva reserva);
    }
}
