using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.Services.Contracts;
using WebApi.Utility;
using Infrastructure.Dtos;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<UsuarioDTO>>();
            try
            {
               response.IsSuccess = true;
               response.Value = await _usuarioService.Lista(); 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("IniciarSession")]
        public async Task<IActionResult> IniciarSession([FromBody] LoginDTO login)
        {
            var response = new Response<SesionDTO>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _usuarioService.ValidarCredenciales(login.Correo!, login.Clave!);    
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
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var response = new Response<UsuarioDTO>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _usuarioService.Guardar(usuario);    
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
        public async Task<IActionResult> Actualizar([FromBody] UsuarioDTO usuario)
        {
            var response = new Response<bool>();
            try
            {
                response.IsSuccess = true;
                response.Value = await _usuarioService.Actualizar(usuario);   
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
                response.Value = await _usuarioService.Eliminar(id);   
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
