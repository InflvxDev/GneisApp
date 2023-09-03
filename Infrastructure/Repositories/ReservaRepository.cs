using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Repositories.Contracts;
using Infrastructure.DBcontext;
using Domain.Entities.Gneis;

namespace Infrastructure.Repositories
{
    public class ReservaRepository : GenericRepository<Reserva> , IReservaRepository
    {
        private readonly PostgresContext _context;

        public ReservaRepository(PostgresContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Reserva> Registrar(Reserva reserva)
        {
            Reserva reservaGenerada = new Reserva();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Habitacion habitacion = _context.Habitacions.Where(p => p.Id == reserva.Id).First();
                    habitacion.Usos++;

                    _context.Habitacions.Update(habitacion);
                    await _context.SaveChangesAsync();

                    await _context.Reservas.AddAsync(reserva);
                    await _context.SaveChangesAsync();

                    reservaGenerada = reserva;

                    transaction.Commit();
                }
                catch
                {
                    transaction.RollbackAsync();
                    throw;

                }

                return reservaGenerada;
            }
        }
    }
}
