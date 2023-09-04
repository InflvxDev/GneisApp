using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Application.Services.Contracts
{
    public interface IDashBoardService
    {
        Task<DashboardDTO> Resumen();
    }
}
