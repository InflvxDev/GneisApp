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

namespace Testing
{   
    [TestClass]
    public class RolTest : BaseTest
    {
        public RolController Setup (){

            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);

            var repository = new GenericRepository<Rol>(context);

            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new RolMappingTest()));

            var mapper = mapconfig.CreateMapper();

            var service = new RolService(repository, mapper);
            
            var controller = new RolController(service);

            return controller;

        }
        

        [TestMethod]
        public void GetRolsTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var respuesta = controller.Lista();

            //Verificacion

            Assert.IsNotNull(respuesta);
            
        }
    }

    public class RolMappingTest : Profile
    {
        public RolMappingTest()
        {
            CreateMap<Rol, RolDTO>();
        }
    }
}
