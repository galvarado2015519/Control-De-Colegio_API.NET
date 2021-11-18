using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.DTOs;
using AutoMapper;

namespace ApiControlDeColegio.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DetalleActividadesController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<DetalleActividadesController> logger;

        public DetalleActividadesController(DbContextApi dbContext, IMapper mapper, ILogger<DetalleActividadesController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleActividadDTO>>> GetDetalleActividades() {
            
            logger.LogDebug("Iniciando el proceso para obtener el listado de los detalles de actividades");
            var detalleActividad = await this.dbContext.DetallesActividad.Include(a => a.Seminario).ToListAsync();
            if(detalleActividad == null || detalleActividad.Count == 0)
            {
                logger.LogWarning("No existen registros de detalleActividad de alumnos");
                return NoContent();
            }
            else
            {
                List<DetalleActividadDTO> detalleActividadDTOs = mapper.Map<List<DetalleActividadDTO>>(detalleActividad);
                logger.LogInformation("Consulta exitosa sobre las detalleActividad de los alumnos");
                return Ok(detalleActividadDTOs);
            }
        }

        [HttpGet("{detalleActividadId}", Name = "GetDetalleActividad")]
        public async Task<ActionResult<DetalleActividadDTO>> GetDetalleActividad(string detalleActividadId)
        {
            logger.LogDebug($"Iniciando el proceso de la consulta de la detalle de actividad con el id: {detalleActividadId}");
            var detalleActividad = await this.dbContext.DetallesActividad.Include(c => c.Seminario).FirstOrDefaultAsync(c => c.DetalleActividadId == detalleActividadId);
            if(detalleActividad == null)
            {
                logger.LogWarning($"El detalle de actividad con el id {detalleActividadId} no existe");
                return NoContent();
            }
            else
            {
                // List<DetalleActividadDTO> detalleActividadDTOs = mapper.Map<List<DetalleActividad>>
                var asignacionAlumnoDTO = mapper.Map<DetalleActividadDTO>(detalleActividad);
                logger.LogInformation("Se ejecuto exitosamente la consulta");
                return Ok(asignacionAlumnoDTO);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DetalleActividadDTO>> PostDetalleActividad([FromBody] DetalleActividadDTO nuevoDetalleActividad)
        {
            logger.LogDebug("Iniciando el proceso de nuevo detalle de actividad");
            logger.LogDebug($"Realizando la consulta del seminario con el detalle de actividad {nuevoDetalleActividad.SeminarioId}");
            Seminario seminario = await this.dbContext.Seminarios.FirstOrDefaultAsync(a => a.SeminarioId == nuevoDetalleActividad.SeminarioId);
            if(seminario == null) 
            {
                logger.LogInformation($"No existe el seminario con el carné {nuevoDetalleActividad.SeminarioId}");
                return BadRequest();
            }
           
            nuevoDetalleActividad.DetalleActividadId = Guid.NewGuid().ToString();
            var detalleActividad = mapper.Map<DetalleActividad>(nuevoDetalleActividad);
            await this.dbContext.DetallesActividad.AddAsync(detalleActividad);
            await this.dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetDetalleActividad", new {detalleActividadId = nuevoDetalleActividad.DetalleActividadId}, 
                mapper.Map<DetalleActividadDTO>(detalleActividad));
        }

        [HttpPut("{detalleActividadId}")]
        public async Task<ActionResult> PutDetalleActividad(string detalleActividadId, [FromBody] DetalleActividad ActualizarAsignacion){
            logger.LogDebug($"Inicio del proceso de modificacion del detalle de actividad con el id {detalleActividadId}");
            DetalleActividad detalleActividad = await this.dbContext.DetallesActividad.FirstOrDefaultAsync(a => a.DetalleActividadId == detalleActividadId);
            if(detalleActividad == null)
            {
                logger.LogInformation($"No existe el detalle de actividad con el id {detalleActividadId}");
                return NotFound();
            }
            else
            {
                logger.LogDebug($"Realizando la consulta del seminario con id {ActualizarAsignacion.SeminarioId}");
                Seminario seminario = await this.dbContext.Seminarios.FirstOrDefaultAsync(a => a.SeminarioId == ActualizarAsignacion.SeminarioId);
                if(seminario == null) 
                {
                    logger.LogInformation($"No existe el seminario con el carné {ActualizarAsignacion.SeminarioId}");
                    return BadRequest();
                }

                detalleActividad.SeminarioId = ActualizarAsignacion.SeminarioId;
                detalleActividad.NombreActividad = ActualizarAsignacion.NombreActividad;
                detalleActividad.Estado = ActualizarAsignacion.Estado;
                detalleActividad.NotaActividad = ActualizarAsignacion.NotaActividad;
                detalleActividad.FechaCreacion = ActualizarAsignacion.FechaCreacion;
                detalleActividad.FechaEntrega = ActualizarAsignacion.FechaEntrega;
                detalleActividad.FechaPostergacion = ActualizarAsignacion.FechaPostergacion;
                this.dbContext.Entry(detalleActividad).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos del detalle de actividad fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{detalleActividadId}")]
        public async Task<ActionResult<DetalleActividadDTO>> DeleteDetalleActividad(String detalleActividadId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion del detalle de actividad");
            DetalleActividad detalleActividad = await this.dbContext.DetallesActividad.FirstOrDefaultAsync(a => a.DetalleActividadId == detalleActividadId);
            if(detalleActividad == null){
                logger.LogInformation($"No existe la detalle de actividad con el Id {detalleActividadId}");
                return NotFound();
            }
            else
            {
                this.dbContext.DetallesActividad.Remove(detalleActividad);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {detalleActividadId}");
                return mapper.Map<DetalleActividadDTO>(detalleActividad);
            }
        }
    }
}