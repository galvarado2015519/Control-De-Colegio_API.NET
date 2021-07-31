using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.DTOs;
using System;
using AutoMapper;

namespace ApiControlDeColegio.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DetalleNotasController
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<DetalleNotasController> logger;

        public DetalleNotasController(DbContextApi dbContext, IMapper mapper, ILogger<DetalleNotasController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignacionAlumnoDTO>>> GetAsignaciones() {
            
            logger.LogDebug("Iniciando el proceso para obtener el listado de las asignaciones");
            var asignaciones = await this.dbContext.AsignacionAlumnos.Include(a => a.Alumno).Include(c => c.Clase).ToListAsync();
            if(asignaciones == null || asignaciones.Count == 0)
            {
                logger.LogWarning("No existen registros de asignaciones de alumnos");
                return NoContent();
            }
            else
            {
                List<AsignacionAlumnoDetalleDTO> asignacionAlumnoDTOs = mapper.Map<List<AsignacionAlumnoDetalleDTO>>(asignaciones);
                logger.LogInformation("Consulta exitosa sobre las asignaciones de los alumnos");
                return Ok(asignacionAlumnoDTOs);
            }
        }

        [HttpGet("{asignacionId}", Name = "GetAsignacion")]
        public async Task<ActionResult<AsignacionAlumnoDetalleDTO>> GetAsignacion(string asignacionId)
        {
            logger.LogDebug($"Iniciando el proceso de la consulta de la asignación con el id: {asignacionId}");
            var asignacion = await this.dbContext.AsignacionAlumnos.Include(c => c.Alumno).Include(c => c.Clase).FirstOrDefaultAsync(c => c.AsignacionId == asignacionId);
            if(asignacion == null)
            {
                logger.LogWarning($"La asignación con el id {asignacionId} no existe");
                return NoContent();
            }
            else
            {
                // List<AsignacionAlumnoDTO> asignacionAlumnoDTOs = mapper.Map<List<AsignacionAlumno>>
                var asignacionAlumnoDTO = mapper.Map<AsignacionAlumnoDetalleDTO>(asignacion);
                logger.LogInformation("Se ejecuto exitosamente la consulta");
                return Ok(asignacionAlumnoDTO);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AsignacionAlumnoDetalleDTO>> PostAsignacion([FromBody] AsignacionAlumnoDTO nuevaAsignacion)
        {
            logger.LogDebug("Iniciando el proceso de nueva asignación");
            logger.LogDebug($"Realizando la consulta del alumno con el carné {nuevaAsignacion.Carne}");
            Alumno alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == nuevaAsignacion.Carne);
            if(alumno == null) 
            {
                logger.LogInformation($"No existe el alumno con el carné {nuevaAsignacion.Carne}");
                return BadRequest();
            }
            logger.LogDebug($"Realizando la consulta de la clase con el id {nuevaAsignacion.ClaseId}");
            Clase clase = await this.dbContext.Clases.FirstOrDefaultAsync(c => c.ClaseId == nuevaAsignacion.ClaseId);
            if(clase == null) 
            {
                logger.LogInformation($"No existe la clase con el id {nuevaAsignacion.ClaseId}");
                return BadRequest();
            }
            nuevaAsignacion.AsignacionId = Guid.NewGuid().ToString();
            var asignacion = mapper.Map<AsignacionAlumno>(nuevaAsignacion);
            await this.dbContext.AsignacionAlumnos.AddAsync(asignacion);
            await this.dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetAsignacion", new {asignacionId = nuevaAsignacion.AsignacionId}, 
                mapper.Map<AsignacionAlumnoDetalleDTO>(asignacion));
        }

        [HttpPut("{asignacionId}")]
        public async Task<ActionResult> PutAsignacion(string asignacionId, [FromBody] AsignacionAlumno ActualizarAsignacion){
            logger.LogDebug($"Inicio del proceso de modificacion de una asignación con el id {asignacionId}");
            AsignacionAlumno asignacion = await this.dbContext.AsignacionAlumnos.FirstOrDefaultAsync(a => a.AsignacionId == asignacionId);
            if(asignacion == null)
            {
                logger.LogInformation($"No existe la asignacion con el id {asignacionId}");
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
                logger.LogDebug($"Realizando la consulta de la clase con el id {ActualizarAsignacion.ClaseId}");
                Clase clase = await this.dbContext.Clases.FirstOrDefaultAsync(c => c.ClaseId == ActualizarAsignacion.ClaseId);
                if(clase == null) 
                {
                    logger.LogInformation($"No existe la clase con el id {ActualizarAsignacion.ClaseId}");
                    return BadRequest();
                }
                asignacion.Carne = ActualizarAsignacion.Carne;
                asignacion.ClaseId = ActualizarAsignacion.ClaseId;
                asignacion.FechaAsignacion = ActualizarAsignacion.FechaAsignacion;
                this.dbContext.Entry(asignacion).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos de la asignación fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{asignacionId}")]
        public async Task<ActionResult<AsignacionAlumnoDTO>> DeleteAsignacion(String asignacionId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion de la asignación");
            AsignacionAlumno asignacion = await this.dbContext.AsignacionAlumnos.FirstOrDefaultAsync(a => a.AsignacionId == asignacionId);
            if(asignacion == null){
                logger.LogInformation($"No existe la asignación con el Id {asignacionId}");
                return NotFound();
            }
            else
            {
                this.dbContext.AsignacionAlumnos.Remove(asignacion);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {asignacionId}");
                return mapper.Map<AsignacionAlumnoDTO>(asignacion);
            }
        }
    }
}