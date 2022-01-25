//cSpell:disable

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Peliculas.Infrastructure.repositories;
using API_Peliculas.Domain.Entities;
using API_Peliculas.Domain.dtos;
using API_Peliculas.Domain.dtos.response;
using API_Peliculas.Domain.dtos.request;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using API_Peliculas.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

/*Universidad Tecnologica Metropolitana
Alumno: Carlos M Mezquita Alvarado
Asignatura: Aplicaciones Web para I4.0
Profesora: Martinez Dominguez Ruth
5°B
Parcial: 1
*/

namespace Peliculas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly services _service;
        private readonly IValidator<PeliculaRequest> _createValidator;
        private readonly repository _repository;
        public PeliculasController(repository repository, 
        IHttpContextAccessor httpContext, 
        IMapper mapper, 
        services service, 
        IValidator<PeliculaRequest> createValidator)
        {
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._service = service;
            this._createValidator = createValidator;
        }


         //Retorna todos los pois
        //Ejemplo para Thunder client: https://localhost:5001/api/Poi/Todos
        [HttpGet]
        [Route("Todos")]
        public async  Task<IActionResult> TodosLosDatos()
        {
            var peliculas = await _repository.TodasPeliculas();
            //var Respuesta = Garbages.Select(g => CreateDtoFromObject(g));
            var Respuestapeliculas = _mapper.Map<IEnumerable<Pelicula>,IEnumerable<PeliculasCreate>>(peliculas);
            return Ok(Respuestapeliculas);
        } 

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.BuscarID(id);
            //entity.Status = false;
            var rows = _repository.ActualizarPelicula(id, entity);
            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> PorID(int id)
        {
            var pelicula = await _repository.BuscarID(id);

            if(pelicula == null)
                return NotFound("Lo sentimos, el poi no fue encontrado.");

            var respuesta = _mapper.Map<Pelicula, PeliculasCreate>(pelicula);

            return Ok(respuesta);
        }

        [HttpPost]
        
        public async Task<IActionResult> create(PeliculaRequest pelicula)
        {
            var Val = await _createValidator.ValidateAsync(pelicula);
            

            //var Val = _service.ValidatedPOI(entity);

            if(!Val.IsValid)
                return UnprocessableEntity (Val.Errors.Select(d => $"{d.PropertyName} => Error: {d.ErrorMessage}"));

            var entity = _mapper.Map<PeliculaRequest, Pelicula>(pelicula);

            var id = await _repository.CrearPelicula(entity);
            
            if(id <= 0)
                return Conflict("Fallo el registro, intente de nuevo");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/Eventos/{id}";
            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update (int id,[FromBody]Pelicula pelicula)
        {
            if(id <= 0)
                return NotFound("No se encontro el regsitro de la denuncia.");
            
            pelicula.Id = id;

            var Validated = _service.ValidarPelicula(pelicula);

            if(!Validated)
                UnprocessableEntity("No es posible actualizar la informacion.");
            
            var updated = await _repository.ActualizarPelicula(id, pelicula);

            if(!updated)
                Conflict("Ocurrio un fallo al intentar actualizar la denuncia.");
            
            return NoContent();
        }

        #region"Request"
        private Pelicula CreateObjectFromDto(PeliculaRequest dto)
        {
            var pelicula = new Pelicula {
                Id = 0,
                Titulo = string.Empty,
                Director = string.Empty,
                Genero = string.Empty,
                FechaPublicacion = string.Empty
            
            };
            return pelicula;
        }
        #endregion
    }
}