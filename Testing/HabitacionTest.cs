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
    public class HabitacionTest : BaseTest
    {
        public HabitacionController Setup (){

            var nombreBD = Guid.NewGuid().ToString();

            var context = ConstruirContext(nombreBD);

            var repository = new GenericRepository<Habitacion>(context);

            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            var mapper = mapconfig.CreateMapper();

            var service = new HabitacionService(repository, mapper);
            
            var controller = new HabitacionController(service);

            return controller;

        }

        [TestMethod]
        public void SaveHabitacionesTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var habitacion = new HabitacionDTO();

            habitacion.Id = 1;
            habitacion.Descripcion = "Habitacion 1";
            habitacion.IdTipo = 1;
            habitacion.DescripcionTipo = "Individual";
            habitacion.Precio = "100000";
            habitacion.Disponibilidad = true;

            var respuesta = controller.Guardar(habitacion);
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
        public void GetHabitacionesTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var respuesta = controller.Lista();
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
        public void UpdateHabitacionesTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var habitacion = new HabitacionDTO();

            habitacion.Id = 1;
            habitacion.Descripcion = "Habitacion 1";
            habitacion.IdTipo = 1;
            habitacion.DescripcionTipo = "Individual";
            habitacion.Precio = "100000";
            habitacion.Disponibilidad = true;

            var respuesta = controller.Guardar(habitacion);
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
        public void DeleteHabitacionesTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var respuesta = controller.Eliminar(1);
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }
    }
}
