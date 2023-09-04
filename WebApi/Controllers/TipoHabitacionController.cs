using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.Services.Contracts;
using WebApi.Utility;
using Infrastructure.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoHabitacionController : ControllerBase
    {
        private readonly ITipoHabitacionService _tipoHabitacionService;

        public TipoHabitacionController(ITipoHabitacionService tipoHabitacionService)
        {
            _tipoHabitacionService = tipoHabitacionService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<TipoDTO>>();
            try
            {
               response.IsSuccess = true;
               response.Value = await _tipoHabitacionService.Lista(); 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }
    }
}
