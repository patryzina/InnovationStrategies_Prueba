using System;
using AutoMapper;
using DGT.Api.Models;
using DGT.Data.Domain.Entities;
using DGT.Data.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DGT.Api.Features.DGT
{
    [Route("api/conductores")]
    [ApiController]
    public class ConductoresController : Controller
    {
        private IDgtRepository _dgtRepository;

        public ConductoresController(IDgtRepository dgtRepository)
        {
            _dgtRepository = dgtRepository ?? throw new NullReferenceException();
        }

        [HttpPost]
        public IActionResult CreateConductor([FromBody]ConductorCreacionDto conductor)
        {
            if (conductor == null)
            {
                return BadRequest();
            }

            // Si ya existe el conductor con este DNI devolvemos error
            if (_dgtRepository.GetConductor(conductor.DNI) != null)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            // mapeamos entre la entidad de dominio y la que exponemos en la API
            var conductorEntity = Mapper.Map<Conductor>(conductor);

            _dgtRepository.AddConductor(conductorEntity);

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

        // Podemos implementar esta request con diferentes lógicas: por puntos, por numero de infracciones
        // Un conductor podría tener 3 infracciones leves que le supusiesen por ejemplo 3 puntos menos
        // mientras que otro podría tener una sola infracción pero grave que le hubiese supuesto 5 puntos...
        // Implemetaremos la opción de devolver aquellos que tengan mas puntos
        [HttpGet("{topN}")]
        public IActionResult GetTopNConductores(int topN)
        {
            var listaConductores = _dgtRepository.GetMejoresConductores(topN);

            // Habitualmente suelo crear entidades para exponerlas en la API en vez de exponer les entidades de dominio
            // mapeandoles, por brevedad devuelvo la entidad de dominio aunque no sea lo mas correcto

            return new JsonResult(listaConductores);
        }
    }
}