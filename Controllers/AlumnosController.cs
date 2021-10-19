using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.DTOs;
using Microsoft.Data.SqlClient;
using System;
using AutoMapper;

namespace ApiControlDeColegio.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<AlumnosController> logger;
        public AlumnosController(DbContextApi dbContext, ILogger<AlumnosController> logger,
                                IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            List<Alumno> alumnos = null;
            this.logger.LogDebug("Iniciando el proceso de consulta de la información de alumnos");
            alumnos = await this.dbContext.Alumnos.ToListAsync();
            if(alumnos == null || alumnos.Count == 0)
            {
                this.logger.LogWarning("No se encontraron registros");
                return new NoContentResult();
            }
            this.logger.LogInformation("Registros cargados");
            return Ok(alumnos);
        }

        [HttpGet("{carne}", Name="GetAlumno")]
        public async Task<ActionResult<Alumno>> GetAlumno(string carne)
        {
            Alumno alumno = null;
            this.logger.LogInformation("Trayendo data");
            alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == carne);
            if(alumno == null)
            {
                this.logger.LogWarning($"No existe el alumno con el carné {carne}");
                return NotFound();
            }
            else
            {
                logger.LogInformation("Proceso de la consulta ejecutada exitosamente");
                return Ok(alumno);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlumnoDTO>> PostAlumno([FromBody] AlumnoCreateDTO value)
        {
            logger.LogDebug("Iniciando el proceso para la creación de un nuevo alumno");
            logger.LogDebug("Iniciando el proceso de la llamada del sp_registrar_alumno ");
            AlumnoDTO alumnoDTO = null;
            var ApellidosParameter = new SqlParameter("@Apellidos", value.Apellidos);
            var NombresParameter = new SqlParameter("@Nombres", value.Nombres);
            var EmailParameter = new SqlParameter("@Email", value.Email);
            var Resultado = await this.dbContext.Alumnos
                                                .FromSqlRaw("sp_registrar_alumno @Apellidos, @Nombres, @Email", 
                                                    ApellidosParameter, NombresParameter, EmailParameter)
                                                .ToListAsync();
            if(Resultado.Count == 0)
            {
                return NoContent();
            }
            foreach (Object registro in Resultado)
            {
                alumnoDTO = mapper.Map<AlumnoDTO>(registro);
            }
            return new CreatedAtRouteResult("GetAlumno", new { carne = alumnoDTO.Carne}, alumnoDTO);
        }

        [HttpPut("{alumnoId}")]
        public async Task<ActionResult> PutAlumno(string alumnoId, [FromBody] Alumno ActualizarAlumno){
            logger.LogDebug($"Inicio del proceso de modificacion del alumno con el id {alumnoId}");
            Alumno alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == alumnoId);
            if(alumno == null)
            {
                logger.LogInformation($"No existe el alumno con el id {alumnoId}");
                return NotFound();
            }
            else
            {

                alumno.NoExpediente = ActualizarAlumno.NoExpediente;
                alumno.Apellidos = ActualizarAlumno.Apellidos;
                alumno.Nombres = ActualizarAlumno.Nombres;
                alumno.Email = ActualizarAlumno.Email;
                this.dbContext.Entry(alumno).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos del alumno fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{alumnoId}")]
        public async Task<ActionResult<AlumnoDTO>> DeleteAlumno(String alumnoId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion del alumno");
            Alumno alumno = await this.dbContext.Alumnos.FirstOrDefaultAsync(a => a.Carne == alumnoId);
            if(alumno == null){
                logger.LogInformation($"No existe la alumno con el Id {alumnoId}");
                return NotFound();
            }
            else
            {
                this.dbContext.Alumnos.Remove(alumno);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {alumnoId}");
                return mapper.Map<AlumnoDTO>(alumno);
            }
        }
    }
}