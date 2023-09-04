using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Application.Services.Contracts
{
    public interface IHabitacionService
    {
        Task<List<HabitacionDTO>> Lista();

        Task<HabitacionDTO> Guardar(HabitacionDTO habitacion);

        Task<bool> Actualizar(HabitacionDTO habitacion);

        Task<bool> Eliminar(int idhabitacion);
    }
}
