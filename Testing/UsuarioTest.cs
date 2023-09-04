using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Application.Services;
using Domain.Entities.Gneis;
using Infrastructure.Repositories;
using WebApi.Controllers;
using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Utility;

namespace Testing
{   
    [TestClass]
    public class UsuarioTest : BaseTest
    {
        public UsuarioController Setup (){

            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);

            var repository = new GenericRepository<Usuario>(context);

            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            var mapper = mapconfig.CreateMapper();

            var service = new UsuarioService(repository, mapper);
            
            var controller = new UsuarioController(service);

            return controller;

        }

        [TestMethod]
        public void SaveUsuariosTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var usuario = new UsuarioDTO();

            usuario.Id = 1;
            usuario.Nombre = "Juan";
            usuario.Correo = "juan@correo.com";
            usuario.IdRol = 1;
            usuario.DescripcionRol = "Administrador";
            usuario.Clave = "123456";
            usuario.EsActivo = 1;

            var respuesta = controller.Guardar(usuario);
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
        public void GetUsuariosTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var respuesta = controller.Lista();

            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
        public void UpdateUsuariosTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var usuario = new UsuarioDTO();

            usuario.Id = 1;
            usuario.Nombre = "Juan";
            usuario.Correo = "juan@correo.com";
            usuario.IdRol = 1;
            usuario.DescripcionRol = "Administrador";
            usuario.Clave = "1234";
            usuario.EsActivo = 1;

            var respuesta = controller.Actualizar(usuario);

            //Verificacion

            Assert.IsNotNull(respuesta);

        } 

        [TestMethod]
        public void CredentialsUsuariosTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var usuario = new LoginDTO();

            usuario.Correo = "juan@correo.com";
            usuario.Clave = "1234";

            var respuesta = controller.IniciarSession(usuario);

            //Verificacion

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(usuario.Correo, "juan@correo.com");
            Assert.AreEqual(usuario.Clave, "1234");

        }

        [TestMethod]
        public void DeleteUsuariosTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var id = 1;

            var respuesta = controller.Eliminar(id);

            //Verificacion

            Assert.IsNotNull(respuesta);

        }

}    

    
}
