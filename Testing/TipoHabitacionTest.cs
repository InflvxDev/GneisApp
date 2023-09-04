
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
    public class TipoHabitacionTest : BaseTest
    {
        public TipoHabitacionController Setup (){

            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);

            var repository = new GenericRepository<TipoHabitacion>(context);

            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new TipoMappingTest()));

            var mapper = mapconfig.CreateMapper();

            var service = new TipoHabitacionService(repository, mapper);
            
            var controller = new TipoHabitacionController(service);

            return controller;

        }

        [TestMethod]
        public void GetTipoHabitacionesTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var respuesta = controller.Lista();

            //Verificacion

            Assert.IsNotNull(respuesta);

        }
    }

    public class TipoMappingTest : Profile
    {
        public TipoMappingTest()
        {
            CreateMap<TipoHabitacion, TipoDTO>();
        }
    }
}
