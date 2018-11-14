using System;
using System.Linq;
using AutoMapper;
using DGT.Api.Models;
using DGT.Data.Domain.Entities;
using DGT.Data.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DGT.Api.Controllers
{
    [Route("api/infracciones")]
    [ApiController]
    public class InfraccionesController : Controller
    {
        private IDgtRepository _dgtRepository;

        private const int MaximoTipoInfraccionesHabituales = 5;

        public InfraccionesController(IDgtRepository dgtRepository)
        {
            _dgtRepository = dgtRepository ?? throw new NullReferenceException();
        }

        [HttpPost]
        public IActionResult CreateInfraccion([FromBody]InfraccionCreacionDto infraccion)
        {
            if (infraccion == null)
            {
                return BadRequest();
            }

            var vehiculo = _dgtRepository.GetVehiculo(infraccion.MatriculaVehiculo);

            // Comprabamos si existe el vehiculo
            if (vehiculo == null)
            {
                return NotFound();
            }
            
            var tipoInfraccion = _dgtRepository.GetTipoInfraccion(infraccion.IdTipoInfraccion);
            if (tipoInfraccion == null)
            {
                return NotFound();
            }

            // Lógica para actualizar puntos del conductor habitual. Actualizamos los puntos del primer conductor habitual
            //`puesto que podemos tener varios
            var conductorHabitual = vehiculo.ConductoresHabituales.FirstOrDefault();

            if (conductorHabitual != null)
            {
                conductorHabitual.Conductor.Puntos -= tipoInfraccion.CostePuntos;
                _dgtRepository.UpdateConductor(conductorHabitual.Conductor);
            }

            var infraccionEntity = new Infraccion()
                                    {
                                         ConductorId = conductorHabitual.Conductor.Id,
                                         IdTipoInfraccion = infraccion.IdTipoInfraccion,
                                         FechaInfraccion = infraccion.FechaInfraccion
                                    };

            _dgtRepository.AddInfraccion(infraccionEntity);

            if (!_dgtRepository.Save())
            {
                // Podríamos añadir un Global exception handler para centralizar el manejo de excepcion
                // y aqui hacer algo parecido a...
                // throw new Exception("Error al guardar en la BD.");

                return StatusCode(500, "Error al procesar la request.");
            }

            // Deberiamos crear un metodo GET y devolver un CreateAtRoute... o en su defecto un Created(uri,...)
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpGet("{dniConductor}")]
        public IActionResult GetInfraccionesConductor(string dniConductor)
        {
            var infracciones = _dgtRepository.GetInfraccionesConductor(dniConductor);

            // Habitualmente suelo crear entidades para exponerlas en la API en vez de exponer les entidades de dominio
            // mapeandoles, por brevedad devuelvo la entidad de dominio aunque no sea lo mas correcto

            return new JsonResult(infracciones);
        }

        [Route("tipoInfraccion")]
        [HttpPost()]
        public IActionResult CreateTipoInfraccion([FromBody]TipoInfraccionDto tipoInfraccion)
        {
            if (tipoInfraccion == null)
            {
                return BadRequest();
            }

            // mapeamos entre la entidad de dominio y la que exponemos en la API
            var tipoInfraccionEntity = Mapper.Map<TipoInfraccion>(tipoInfraccion);

            _dgtRepository.AddTipoInfraccion(tipoInfraccionEntity);

            if (!_dgtRepository.Save())
            {
                // Podríamos añadir un Global exception handler para centralizar el manejo de excepcion
                // y aqui hacer algo cparecido a...
                // throw new Exception("Error al guardar en la BD.");

                return StatusCode(500, "Error al procesar la request.");
            }

            // Deberiamos crear un metodo GET y devolver un CreateAtRoute... o en su defecto un Created(uri,...)
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        // Esta llamada devuelve el "numeroTiposInfracion" numero de tipos de infracciones mas comunes
        [Route("TipoInfraccion")]
        [HttpGet()]
        public IActionResult GetTiposInfraccionesComunes()
        {
            var tiposInfracciones = _dgtRepository.GetTiposInfraccionesComunes(MaximoTipoInfraccionesHabituales);

            // Habitualmente suelo crear entidades para exponerlas en la API en vez de exponer les entidades de dominio
            // mapeandoles, por brevedad devuelvo la entidad de dominio aunque no sea lo mas correcto

            return new JsonResult(tiposInfracciones);
        }
    }
}