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
    public class SeminariosController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<SeminariosController> logger;

        public SeminariosController(DbContextApi dbContext, IMapper mapper, ILogger<SeminariosController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeminarioDTO>>> GetSeminarios() {
            
            logger.LogDebug("Iniciando el proceso para obtener el listado de los detalles de actividades");
            var seminario = await this.dbContext.Seminarios.Include(a => a.Modulo).ToListAsync();
            if(seminario == null || seminario.Count == 0)
            {
                logger.LogWarning("No existen registros de seminario de alumnos");
                return NoContent();
            }
            else
            {
                List<SeminarioDTO> SeminarioDTOs = mapper.Map<List<SeminarioDTO>>(seminario);
                logger.LogInformation("Consulta exitosa sobre las seminario de los alumnos");
                return Ok(SeminarioDTOs);
            }
        }

        [HttpGet("{seminarioId}", Name = "GetSeminario")]
        public async Task<ActionResult<SeminarioDTO>> GetSeminario(string seminarioId)
        {
            logger.LogDebug($"Iniciando el proceso de la consulta del seminario con el id: {seminarioId}");
            var seminario = await this.dbContext.Seminarios.Include(c => c.Modulo).FirstOrDefaultAsync(c => c.SeminarioId == seminarioId);
            if(seminario == null)
            {
                logger.LogWarning($"El seminario con el id {seminarioId} no existe");
                return NoContent();
            }
            else
            {
                // List<SeminarioDTO> SeminarioDTOs = mapper.Map<List<Seminario>>
                var seminarioDTO = mapper.Map<SeminarioDTO>(seminario);
                logger.LogInformation("Se ejecuto exitosamente la consulta");
                return Ok(seminarioDTO);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SeminarioDTO>> PostSeminario([FromBody] SeminarioDTO nuevoSeminario)
        {
            logger.LogDebug("Iniciando el proceso de nuevo seminario");
            logger.LogDebug($"Realizando la consulta del modulo con el seminario {nuevoSeminario.ModuloId}");
            Modulo modulo = await this.dbContext.Modulos.FirstOrDefaultAsync(a => a.ModuloId == nuevoSeminario.ModuloId);
            if(modulo == null) 
            {
                logger.LogInformation($"No existe el modulo con el id {nuevoSeminario.ModuloId}");
                return BadRequest();
            }
           
            nuevoSeminario.SeminarioId = Guid.NewGuid().ToString();
            var seminarioMap = mapper.Map<Seminario>(nuevoSeminario);
            await this.dbContext.Seminarios.AddAsync(seminarioMap);
            await this.dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetSeminario", new {seminarioId = nuevoSeminario.SeminarioId}, 
                mapper.Map<SeminarioDTO>(seminarioMap));
        }

        [HttpPut("{seminarioId}")]
        public async Task<ActionResult> PutSeminario(string seminarioId, [FromBody] Seminario ActualizarModulo){
            logger.LogDebug($"Inicio del proceso de modificacion del seminario con el id {seminarioId}");
            Seminario seminario = await this.dbContext.Seminarios.FirstOrDefaultAsync(a => a.SeminarioId == seminarioId);
            if(seminario == null)
            {
                logger.LogInformation($"No existe el seminario con el id {seminarioId}");
                return NotFound();
            }
            else
            {
                logger.LogDebug($"Realizando la consulta del seminario con id {ActualizarModulo.SeminarioId}");
                Modulo modulo = await this.dbContext.Modulos.FirstOrDefaultAsync(a => a.ModuloId == ActualizarModulo.ModuloId);
                if(modulo == null) 
                {
                    logger.LogInformation($"No existe el modulo con el id {ActualizarModulo.ModuloId}");
                    return BadRequest();
                }

                seminario.ModuloId = ActualizarModulo.ModuloId;
                seminario.NombreSeminario = ActualizarModulo.NombreSeminario;
                seminario.FechaInicio = ActualizarModulo.FechaInicio;
                this.dbContext.Entry(seminario).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation("Los datos del seminario fueron actualizados exitosamente");
                return NoContent();
            }
        }

        [HttpDelete("{seminarioId}")]
        public async Task<ActionResult<SeminarioDTO>> DeleteSeminario(String seminarioId) 
        {
            logger.LogDebug("Iniciando el procesos de eliminacion del seminario");
            Seminario seminario = await this.dbContext.Seminarios.FirstOrDefaultAsync(a => a.SeminarioId == seminarioId);
            if(seminario == null){
                logger.LogInformation($"No existe la seminario con el Id {seminarioId}");
                return NotFound();
            }
            else
            {
                this.dbContext.Seminarios.Remove(seminario);
                await this.dbContext.SaveChangesAsync();
                logger.LogInformation($"Se ha realizado la eliminación del registro con el id {seminarioId}");
                return mapper.Map<SeminarioDTO>(seminario);
            }
        }
    }
}