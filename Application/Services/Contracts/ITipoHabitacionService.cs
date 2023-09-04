using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Application.Services.Contracts
{
    public interface ITipoHabitacionService
    {
        Task<List<TipoDTO>> Lista();
    }
}
