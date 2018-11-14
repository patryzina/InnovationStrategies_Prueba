using System;
using AutoMapper;
using DGT.Api.Models;
using DGT.Data.Domain.Entities;
using DGT.Data.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DGT.Api.Controllers
{
    [Route("api/vehiculos")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private IDgtRepository _dgtRepository;

        private static readonly int MaximoVehiculosPorConductor = 10;

        public VehiculosController(IDgtRepository dgtRepository)
        {
            _dgtRepository = dgtRepository ?? throw new NullReferenceException();
        }

        [HttpPost]
        public IActionResult CreateVehiculo([FromBody]VehiculoDto vehiculo)
        {
            if (vehiculo == null)
            {
                return BadRequest();
            }

            // Si no existe el conductor devolvemos un error
            if (_dgtRepository.GetConductor(vehiculo.DniConductor) == null)
            {
                return NotFound();
            }

            // Vehiculo con matrícula duplicada
            if (_dgtRepository.GetVehiculo(vehiculo.Matricula) != null)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            
            // Comprobación de condición "El conductor en sí no puede ser habitual de mas de 10 vehiculos"
            var vehiculosParaConductor = _dgtRepository.GetVehiculosParaConductor(vehiculo.DniConductor);
            if (vehiculosParaConductor.Count >= MaximoVehiculosPorConductor)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }  

            // mapeamos entre la entidad de dominio y la que exponemos en la API
            var vehiculoEntity = Mapper.Map<Vehiculo>(vehiculo);

            _dgtRepository.AddVehiculo(vehiculoEntity, vehiculo.DniConductor);

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
    }
}