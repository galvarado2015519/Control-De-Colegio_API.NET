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
    public class ModulosController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ModulosController> logger;

        public ModulosController(DbContextApi dbContext, IMapper mapper, ILogger<ModulosController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<ModuloDTO>>> GetModulos() {
            
        //     logger.LogDebug("Iniciando el proceso para obtener el listado de los instructores");
        //     var modulo = await this.dbContext.Modulos.Include(a => a.Carrera).ToListAsync();
        //     if(modulo == null || modulo.Count == 0)
        //     {
        //         logger.LogWarning("No existen registros");
        //         return NoContent();
        //     }
        //     else
        //     {
        //         List<ModuloDTO> ModuloDTOs = mapper.Map<List<ModuloDTO>>(modulo);
        //         logger.LogInformation("Consulta exitosa sobre las modulo de los instructores");
        //         return Ok(ModuloDTOs);
        //     }
        // }

        // [HttpGet("{moduloId}", Name = "GetModulo")]
        // public async Task<ActionResult<ModuloDTO>> GetModulo(string moduloId)
        // {
        //     logger.LogDebug($"Iniciando el proceso de la consulta del modulo con el id: {moduloId}");
        //     var modulo = await this.dbContext.Modulos.Include(c => c.Carrera).FirstOrDefaultAsync(c => c.ModuloId == moduloId);
        //     if(modulo == null)
        //     {
        //         logger.LogWarning($"El modulo con el id {moduloId} no existe");
        //         return NoContent();
        //     }
        //     else
        //     {
        //         // List<ModuloDTO> ModuloDTOs = mapper.Map<List<modulo>>
        //         var moduloDTO = mapper.Map<ModuloDTO>(modulo);
        //         logger.LogInformation("Se ejecuto exitosamente la consulta");
        //         return Ok(moduloDTO);
        //     }
        // }

        // [HttpPost]
        // public async Task<ActionResult<ModuloDTO>> PostModulo([FromBody] ModuloDTO nuevoModulo)
        // {
        //     logger.LogDebug("Iniciando el proceso de nuevo modulo");
        //     logger.LogDebug($"Realizando la consulta del Carrera con el modulo {nuevoModulo.CarreraId}");
        //     Carrera Carrera = await this.dbContext.Carreras.FirstOrDefaultAsync(a => a.CarreraId == nuevoModulo.CarreraId);
        //     if(Carrera == null) 
        //     {
        //         logger.LogInformation($"No existe el Carrera con el carné {nuevoModulo.CarreraId}");
        //         return BadRequest();
        //     }
           
        //     nuevoModulo.CarreraId = Guid.NewGuid().ToString();
        //     var modulo = mapper.Map<Modulo>(nuevoModulo);
        //     await this.dbContext.Modulos.AddAsync(modulo);
        //     await this.dbContext.SaveChangesAsync();
        //     return new CreatedAtRouteResult("GetModulo", new {moduloId = nuevoModulo.ModuloId}, 
        //         mapper.Map<ModuloDTO>(modulo));
        // }

        // [HttpPut("{moduloId}")]
        // public async Task<ActionResult> PutModulo(string moduloId, [FromBody] Modulo ActualizarModulo){
        //     logger.LogDebug($"Inicio del proceso de modificacion del modulo con el id {moduloId}");
        //     Modulo modulo = await this.dbContext.Modulos.FirstOrDefaultAsync(a => a.ModuloId == moduloId);
        //     if(modulo == null)
        //     {
        //         logger.LogInformation($"No existe el modulo con el id {moduloId}");
        //         return NotFound();
        //     }
        //     else
        //     {
        //         logger.LogDebug($"Realizando la consulta del Carrera con id {ActualizarModulo.ModuloId}");
        //         Carrera Carrera = await this.dbContext.Carreras.FirstOrDefaultAsync(a => a.CarreraId == ActualizarModulo.CarreraId);
        //         if(Carrera == null) 
        //         {
        //             logger.LogInformation($"No existe la carrera con el id: {ActualizarModulo.CarreraId}");
        //             return BadRequest();
        //         }

        //         modulo.CarreraId = ActualizarModulo.CarreraId;
        //         modulo.NombreModulo = ActualizarModulo.NombreModulo;
        //         modulo.NumeroSeminarios = ActualizarModulo.NumeroSeminarios;
        //         this.dbContext.Entry(modulo).State = EntityState.Modified;
        //         await this.dbContext.SaveChangesAsync();
        //         logger.LogInformation("Los datos del modulo fueron actualizados exitosamente");
        //         return NoContent();
        //     }
        // }

        // [HttpDelete("{moduloId}")]
        // public async Task<ActionResult<ModuloDTO>> DeleteModulo(String moduloId) 
        // {
        //     logger.LogDebug("Iniciando el procesos de eliminacion del modulo");
        //     Modulo modulo = await this.dbContext.Modulos.FirstOrDefaultAsync(a => a.ModuloId == moduloId);
        //     if(modulo == null){
        //         logger.LogInformation($"No existe la modulo con el Id {moduloId}");
        //         return NotFound();
        //     }
        //     else
        //     {
        //         this.dbContext.Modulos.Remove(modulo);
        //         await this.dbContext.SaveChangesAsync();
        //         logger.LogInformation($"Se ha realizado la eliminación del registro con el id {moduloId}");
        //         return mapper.Map<ModuloDTO>(modulo);
        //     }
        // }
    }
}