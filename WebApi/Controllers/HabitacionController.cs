using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.Services.Contracts;
using WebApi.Utility;
using Infrastructure.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionService _habitacionService;

        public HabitacionController(IHabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<HabitacionDTO>>();
            try
            {
               response.IsSuccess = true;
               response.Value = await _habitacionService.Lista(); 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] HabitacionDTO habitacion)
        {
            var response = new Response<HabitacionDTO>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _habitacionService.Guardar(habitacion);    
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] HabitacionDTO habitacion)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _habitacionService.Actualizar(habitacion);   
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _habitacionService.Eliminar(id);   
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
