using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.DTOs;
using ApiControlDeColegio.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiControlDeColegio.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly ILogger<ClasesController> logger;
        private readonly IMapper mapper;

        public ClasesController(DbContextApi dbContext, ILogger<ClasesController> logger, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clase>>> GetClases()
        {
            logger.LogDebug("Iniciando el proceso de consulta de clases");
            var clases = await this.dbContext.Clases.ToListAsync();
            if(clases == null || clases.Count == 0)
            {
                logger.LogWarning("No se encontraron registros");
                return NoContent();
            }
            else
            {
                logger.LogInformation("Registros cargados exitosamente");
                return Ok(clases);
            }
        }

        [HttpGet("{claseId}", Name ="GetClase")]
        public async Task<ActionResult<IEnumerable<Clase>>> GetClase(string claseId)
        {
            logger.LogDebug($"Iniciando proceso de consulta sobre la clase con id: {claseId}");
            var clase = await this.dbContext.Clases.Include(i => i.Instructor).Include(c => c.Carrera).Include(s => s.Salon).Include(s => s.Horario).FirstOrDefaultAsync(c => c.ClaseId == claseId);
            if(clase == null) {
                logger.LogWarning($"No existe la clase con el id: {claseId}");
                return new NoContentResult();
            }
            else
            {
                logger.LogInformation("Consulta a la base de datos exitosa para la clase id");
                return Ok(clase);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Clase>> PostDetalleNota([FromBody] Clase nuevaClase)
        {
            logger.LogDebug("Iniciando el proceso de nueva asignación");
            logger.LogDebug($"Realizando la consulta del carrera con el carné {nuevaClase.CarreraId}");
            Carrera carrera = await this.dbContext.Carreras.FirstOrDefaultAsync(a => a.CarreraId == nuevaClase.CarreraId);
            if(carrera == null) 
            {
                logger.LogInformation($"No existe la carrera {nuevaClase.CarreraId}");
                return BadRequest();
            }

            logger.LogDebug($"Realizando la consulta al horario con el id {nuevaClase.HorarioId}");
            Horario horario = await this.dbContext.Horarios.FirstOrDefaultAsync(c => c.HorarioId == nuevaClase.HorarioId);
            if(horario == null) 
            {   logger.LogInformation($"No existe el horario con el id {nuevaClase.HorarioId}");
                return BadRequest();
            }

            logger.LogDebug($"Realizando la consulta al instructor con el id {nuevaClase.InstructorId}");
            Instructor instructor = await this.dbContext.Instructores.FirstOrDefaultAsync(c => c.InstructorId == nuevaClase.InstructorId);
            if(instructor == null) 
            {   logger.LogInformation($"No existe el instructor con el id {nuevaClase.InstructorId}");
                return BadRequest();
            }

            logger.LogDebug($"Realizando la consulta al salon con el id {nuevaClase.SalonId}");
            Salon salon = await this.dbContext.Salones.FirstOrDefaultAsync(c => c.SalonId == nuevaClase.SalonId);
            if(salon == null) 
            {   logger.LogInformation($"No existe el salon con el id {nuevaClase.SalonId}");
                return BadRequest();
            }
            nuevaClase.ClaseId = Guid.NewGuid().ToString();
            await this.dbContext.Clases.AddAsync(nuevaClase);
            await this.dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetClases", new {claseId = nuevaClase.ClaseId}, 
                mapper.Map<ClaseAsignacionDTO>(nuevaClase));
        }

        [HttpPut("{claseId}")]
        public async Task<ActionResult> PutClase(string claseId, [FromBody] Clase ActualizarClase){
            logger.LogDebug($"Inicio del proceso de modificacion de una clase con el id {claseId}");
            Clase clase = await this.dbContext.Clases.FirstOrDefaultAsync(a => a.ClaseId == claseId);
            if(clase == null)
            {
                logger.LogInformation($"No existe una clase con el id {claseId}");
                return NotFound();
            }
            else
            {
                logger.LogDebug($"Realizando la consulta del carrera  {ActualizarClase.CarreraId}");
                Carrera carrera = await this.dbContext.Carreras.FirstOrDefaultAsync(a => a.CarreraId == ActualizarClase.CarreraId);
                if(carrera == null) 
                {
                    logger.LogInformation($"No existe la carrera {ActualizarClase.CarreraId}");
                    return BadRequest();
                }

                logger.LogDebug($"Realizando la consulta al horario con el id {ActualizarClase.HorarioId}");
                Horario horario = await this.dbContext.Horarios.FirstOrDefaultAsync(c => c.HorarioId == ActualizarClase.HorarioId);
                if(horario == null) 
                {   logger.LogInformation($"No existe el horario con el id {ActualizarClase.HorarioId}");
                    return BadRequest();
                }

                logger.LogDebug($"Realizando la consulta al instructor con el id {ActualizarClase.InstructorId}");
                Instructor instructor = await this.dbContext.Instructores.FirstOrDefaultAsync(c => c.InstructorId == ActualizarClase.InstructorId);
                if(instructor == null) 
                {   logger.LogInformation($"No existe el instructor con el id {ActualizarClase.InstructorId}");
                    return BadRequest();
                }

                logger.LogDebug($"Realizando la consulta al salon con el id {ActualizarClase.SalonId}");
                Salon salon = await this.dbContext.Salones.FirstOrDefaultAsync(c => c.SalonId == ActualizarClase.SalonId);
                if(salon == null) 
                {   logger.LogInformation($"No existe el salon con el id {ActualizarClase.SalonId}");
                    return BadRequest();
                }
                 
                clase.Ciclo = ActualizarClase.Ciclo;
                clase.CupoMaximo = ActualizarClase.CupoMaximo;
                clase.CupoMinimo = ActualizarClase.CupoMinimo;
                clase.Descripcion = ActualizarClase.Descripcion;
                clase.CarreraId = ActualizarClase.CarreraId;
                clase.HorarioId = ActualizarClase.HorarioId;
                clase.InstructorId = ActualizarClase.InstructorId;
                clase.SalonId = ActualizarClase.SalonId;
                this.dbContext.Entry(clase).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos de la clase fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{claseId}")]
        public async Task<ActionResult<Clase>> DeleteClase(String claseId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion de la clase");
            Clase clase = await this.dbContext.Clases.FirstOrDefaultAsync(a => a.CarreraId == claseId);
            if(clase == null){
                logger.LogInformation($"No existe la clase con el Id {claseId}");
                return NotFound();
            }
            else
            {
                this.dbContext.Clases.Remove(clase);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {claseId}");
                return mapper.Map<Clase>(clase);
            }
        }
    }
}