using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.Services.Contracts;
using WebApi.Utility;
using Infrastructure.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<ClienteDTO>>();
            try
            {
               response.IsSuccess = true;
               response.Value = await _clienteService.Lista(); 
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
        public async Task<IActionResult> Guardar([FromBody] ClienteDTO cliente)
        {
            var response = new Response<ClienteDTO>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _clienteService.Guardar(cliente);    
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
        public async Task<IActionResult> Actualizar([FromBody] ClienteDTO cliente)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _clienteService.Actualizar(cliente);   
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
                response.Value = await _clienteService.Eliminar(id);   
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
