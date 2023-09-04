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
    public class ReservaTest : BaseTest
    {
        public ReservaController Setup (){

            var nombreBD = Guid.NewGuid().ToString();

            var context = ConstruirContext(nombreBD);

            var repository = new GenericRepository<Reserva>(context);

            var repositoryReserva = new ReservaRepository(context);

            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            var mapper = mapconfig.CreateMapper();

            var service = new ReservaService(repositoryReserva, repository, mapper);
            
            var controller = new ReservaController(service);

            return controller;

        }

        [TestMethod]
        public void SaveReservaTest (){

            //Setup
            
            var controller = Setup();

            //Prueba

            var reserva = new ReservaDTO();

            reserva.Id = 1;
            reserva.FechaReserva = "10/10/2021";
            reserva.Cedula = "123456789";
            reserva.NombreCliente = "Juan";
            reserva.IdHabitacion = 1;
            reserva.DescripcionHabitacion = "Habitacion 1";
            reserva.CostoDia = "100000";
            reserva.Acompañantes = 1;


            var respuesta = controller.Guardar(reserva);
            
            //Verificacion

            Assert.IsNotNull(respuesta);

        }

        [TestMethod]
         public void CheckinReservaTest ()
         {     
            //Setup
            
            var controller = Setup();

            //Prueba

            var reserva = new ReservaDTO();

            reserva.Id = 1;
            reserva.FechaReserva = "10/10/2021";
            reserva.Cedula = "123456789";
            reserva.NombreCliente = "Juan";
            reserva.IdHabitacion = 1;
            reserva.DescripcionHabitacion = "Habitacion 1";
            reserva.CostoDia = "100000";
            reserva.Acompañantes = 1;
            reserva.FechaEntrada = "10/10/2021";

            var respuesta = controller.CheckIn(reserva);

            //Verificacion

            Assert.IsNotNull(respuesta);
        }

        [TestMethod]
         public void CheckoutReservaTest ()
         {     
            //Setup
            
            var controller = Setup();

            //Prueba

            var reserva = new ReservaDTO();

            reserva.Id = 1;
            reserva.FechaReserva = "10/10/2021";
            reserva.Cedula = "123456789";
            reserva.NombreCliente = "Juan";
            reserva.IdHabitacion = 1;
            reserva.DescripcionHabitacion = "Habitacion 1";
            reserva.CostoDia = "100000";
            reserva.Acompañantes = 1;
            reserva.FechaEntrada = "10/10/2021";
            reserva.FechaSalida = "12/10/2021";
            reserva.DiaHospedaje = 2;
            reserva.Total = "200000";

            var respuesta = controller.CheckOut(reserva);

            //Verificacion

            Assert.IsNotNull(respuesta);
        }  


        
    }
}
