using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.Services.Contracts;
using WebApi.Utility;
using Infrastructure.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ReservaDTO reserva)
        {
            var response = new Response<ReservaDTO>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _reservaService.Registrar(reserva);    
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("CheckIn")]
        public async Task<IActionResult> CheckIn([FromBody] ReservaDTO reserva)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _reservaService.CheckIn(reserva);   
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("CheckOut")]
        public async Task<IActionResult> CheckOut([FromBody] ReservaDTO reserva)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _reservaService.CheckOut(reserva);   
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? idReserva, string? fechaInicio, string? fechaFin)
        {
            var response = new Response<List<ReservaDTO>>();
            idReserva = idReserva ?? "";
            fechaInicio = fechaInicio ?? "";
            fechaFin = fechaFin ?? "";

            try
            {
               response.IsSuccess = true;
               response.Value = await _reservaService.Historial(buscarPor, idReserva, fechaInicio, fechaFin); 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string fechaInicio, string fechaFin)
        {
            var response = new Response<List<ReporteDTO>>();
            
            try
            {
               response.IsSuccess = true;
               response.Value = await _reservaService.Reporte(fechaInicio, fechaFin);
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
