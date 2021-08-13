using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.DTOs;
using ApiControlDeColegio.Entities;
using AutoMapper;

namespace ApiControlDeColegio.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DetalleNotasController : ControllerBase
    {
        private readonly ILogger<DetalleNotasController> logger;
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;

        public DetalleNotasController(DbContextApi dbContext, IMapper mapper, ILogger<DetalleNotasController> logger)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleNotaDTO>>> GetDetalleNotas() {
            
            logger.LogDebug("Iniciando el proceso para obtener el listado del Detalle de notas");
            var detalleNotas = await this.dbContext.DetalleNotas.Include(a => a.DetalleActividad).Include(c => c.Alumno).ToListAsync();
            if(detalleNotas == null || detalleNotas.Count == 0)
            {
                logger.LogWarning("No existen registros del detalle de notas de alumnos");
                return NoContent();
            }
            else
            {
                List<DetalleNotaDTO> detalleNotasDTOs = mapper.Map<List<DetalleNotaDTO>>(detalleNotas);
                logger.LogInformation("Consulta exitosa sobre el detalle de notas de los alumnos");
                return Ok(detalleNotasDTOs);
            }
        }

        [HttpGet("{detalleNotaId}", Name = "GetAsignacion")]
        public async Task<ActionResult<DetalleNotaDTO>> GetDetalleNota(string detalleNotaId)
        {
            logger.LogDebug($"Iniciando el proceso de la consulta de la asignación con el id: {detalleNotaId}");
            var detalleNotas = await this.dbContext.DetalleNotas.Include(c => c.DetalleActividad).Include(c => c.Alumno).FirstOrDefaultAsync(c => c.DetalleNotaId == detalleNotaId);
            if(detalleNotas == null)
            {
                logger.LogWarning($"La asignación con el id {detalleNotaId} no existe");
                return NoContent();
            }
            else
            {
                // List<DetalleNotaDTO> detalleNotasDTOs = mapper.Map<List<DetalleNota>>
                var asignacionAlumnoDTO = mapper.Map<DetalleNotaDTO>(detalleNotas);
                logger.LogInformation("Se ejecuto exitosamente la consulta");
                return Ok(asignacionAlumnoDTO);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DetalleNotaDTO>> PostDetalleNota([FromBody] DetalleNotaDTO nuevoDetalleNota)
        {
            logger.LogDebug("Iniciando el proceso de nueva asignación");
            logger.LogDebug($"Realizando la consulta del alumno con el carné {nuevoDetalleNota.Carne}");
            Alumno alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == nuevoDetalleNota.Carne);
            if(alumno == null) 
            {
                logger.LogInformation($"No existe el alumno con el carné {nuevoDetalleNota.Carne}");
                return BadRequest();
            }
            logger.LogDebug($"Realizando la consulta de la clase con el id {nuevoDetalleNota.DetalleActividadId}");
            DetalleActividad detalleActividad = await this.dbContext.DetallesActividad.FirstOrDefaultAsync(c => c.DetalleActividadId == nuevoDetalleNota.DetalleActividadId);
            if(detalleActividad == null) 
            {
                logger.LogInformation($"No existe la clase con el id {nuevoDetalleNota.DetalleActividadId}");
                return BadRequest();
            }
            nuevoDetalleNota.DetalleNotaId = Guid.NewGuid().ToString();
            var detalleNotas = mapper.Map<DetalleNota>(nuevoDetalleNota);
            await this.dbContext.DetalleNotas.AddAsync(detalleNotas);
            await this.dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetAsignacion", new {detalleNotaId = nuevoDetalleNota.DetalleNotaId}, 
                mapper.Map<DetalleNotaDTO>(detalleNotas));
        }

        [HttpPut("{detalleNotaId}")]
        public async Task<ActionResult> PutDetalleNota(string detalleNotaId, [FromBody] DetalleNota ActualizarAsignacion){
            logger.LogDebug($"Inicio del proceso de modificacion de una asignación con el id {detalleNotaId}");
            DetalleNota detalleNotas = await this.dbContext.DetalleNotas.FirstOrDefaultAsync(a => a.DetalleNotaId == detalleNotaId);
            if(detalleNotas == null)
            {
                logger.LogInformation($"No existe un detalle de notas en la asignación con el id {detalleNotaId}");
                return NotFound();
            }
            else
            {
                logger.LogDebug($"Realizando la consulta del alumno con el carné {ActualizarAsignacion.Carne}");
                Alumno alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == ActualizarAsignacion.Carne);
                if(alumno == null) 
                {
                    logger.LogInformation($"No existe el alumno con el carné {ActualizarAsignacion.Carne}");
                    return BadRequest();
                }
                logger.LogDebug($"Realizando la consulta de la clase con el id {ActualizarAsignacion.DetalleActividadId}");
                DetalleActividad detalleActividad = await this.dbContext.DetallesActividad.FirstOrDefaultAsync(c => c.DetalleActividadId == ActualizarAsignacion.DetalleActividadId);
                if(detalleActividad == null) 
                {
                    logger.LogInformation($"No existe la clase con el id {ActualizarAsignacion.DetalleActividadId}");
                    return BadRequest();
                }
                detalleNotas.Carne = ActualizarAsignacion.Carne;
                detalleNotas.DetalleActividadId = ActualizarAsignacion.DetalleActividadId;
                detalleNotas.ValorNota = ActualizarAsignacion.ValorNota;
                this.dbContext.Entry(detalleNotas).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos de la asignación fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{detalleNotaId}")]
        public async Task<ActionResult<DetalleNotaDTO>> DeleteDetalleNota(String detalleNotaId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion de la asignación");
            DetalleNota detalleNotas = await this.dbContext.DetalleNotas.FirstOrDefaultAsync(a => a.DetalleNotaId == detalleNotaId);
            if(detalleNotas == null){
                logger.LogInformation($"No existe la asignación con el Id {detalleNotaId}");
                return NotFound();
            }
            else
            {
                this.dbContext.DetalleNotas.Remove(detalleNotas);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {detalleNotaId}");
                return mapper.Map<DetalleNotaDTO>(detalleNotas);
            }
        }
    }
}