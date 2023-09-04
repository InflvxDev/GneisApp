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

        public async Task<bool> CheckIn(Reserva reserva)
        {
            using (var transaction = _context.Database.BeginTransaction()) {
                try{
                    Reserva reservaEncontrada = _context.Reservas.Where(p => p.Id == reserva.Id).First();

                    if(reservaEncontrada == null){
                        throw new TaskCanceledException("Reserva no Registrada");
                    }

                    reservaEncontrada.FechaEntrada = DateTime.Now;

                    _context.Reservas.Update(reservaEncontrada);
                    await _context.SaveChangesAsync();

                    Habitacion habitacion = _context.Habitacions.Where(p => p.Id == reserva.IdHabitacion).First();
                    habitacion.Disponibilidad = false;
                    habitacion.Usos++;

                    _context.Habitacions.Update(habitacion);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }catch{
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> CheckOut(Reserva reserva)
        {
            using (var transaction = _context.Database.BeginTransaction()) {
                try{
                    Reserva reservaEncontrada = _context.Reservas.Where(p => p.Id == reserva.Id).First();

                    if(reservaEncontrada == null){
                        throw new TaskCanceledException("Reserva no Registrada");
                    }

                    Habitacion habitacion = _context.Habitacions.Where(p => p.Id == reserva.IdHabitacion).First();
                    habitacion.Disponibilidad = true;

                    _context.Habitacions.Update(habitacion);
                    await _context.SaveChangesAsync();

                    reservaEncontrada.FechaSalida = DateTime.Now;
                    reservaEncontrada.DiaHospedaje = (reservaEncontrada.FechaSalida - reservaEncontrada.FechaEntrada)!.Value.Days;
                    reservaEncontrada.Total = reservaEncontrada.DiaHospedaje * habitacion.Precio;

                    _context.Reservas.Update(reservaEncontrada);
                    await _context.SaveChangesAsync();

                    

                    transaction.Commit();
                }catch{
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return true;
        }

        public async Task<Reserva> Registrar(Reserva reserva)
        {
            Reserva reservaGenerada = new Reserva();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Reservas.AddAsync(reserva);
                    await _context.SaveChangesAsync();

                    reservaGenerada = reserva;

                    transaction.Commit();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;

                }

                return reservaGenerada;
            }
        }
    }
}
