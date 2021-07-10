using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        public async Task<ActionResult<AlumnoDTO>> Post([FromBody] AlumnoCreateDTO value)
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
            logger.LogDebug($"Resultado de procedimiento almacenado ${Resultado}");
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
    }
}