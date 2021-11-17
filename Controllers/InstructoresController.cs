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
    public class InstructoresController : ControllerBase
    {
        private readonly DbContextApi dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<InstructoresController> logger;

        public InstructoresController(DbContextApi dbContext, IMapper mapper, ILogger<InstructoresController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructores() {
            
        //     logger.LogDebug("Iniciando el proceso para obtener el listado de los instructores");
        //     var instructor = await this.dbContext.Instructores.ToListAsync();
        //     if(instructor == null || instructor.Count == 0)
        //     {
        //         logger.LogWarning("No existen registros de instructor de alumnos");
        //         return NoContent();
        //     }
        //     else
        //     {
        //         List<InstructorDTO> instructorDTOs = mapper.Map<List<InstructorDTO>>(instructor);
        //         logger.LogInformation("Consulta exitosa sobre el instructor de los alumnos");
        //         return Ok(instructorDTOs);
        //     }
        // }

        // [HttpGet("{instructorId}", Name = "GetInstructor")]
        // public async Task<ActionResult<InstructorDTO>> GetInstructor(string instructorId)
        // {
        //     logger.LogDebug($"Iniciando el proceso de la consulta de la Instructor con el id: {instructorId}");
        //     var instructor = await this.dbContext.Instructores.FirstOrDefaultAsync(c => c.InstructorId == instructorId);
        //     if(instructor == null)
        //     {
        //         logger.LogWarning($"El Instructor con el id {instructorId} no existe");
        //         return NoContent();
        //     }
        //     else
        //     {
        //         // List<InstructorDTO> instructorDTOs = mapper.Map<List<Instructor>>
        //         var instructorDTO = mapper.Map<InstructorDTO>(instructor);
        //         logger.LogInformation("Se ejecuto exitosamente la consulta");
        //         return Ok(instructorDTO);
        //     }
        // }

        // [HttpPost]
        // public async Task<ActionResult<InstructorDTO>> PostInstructor([FromBody] InstructorDTO nuevoInstructor)
        // {
         
        //     nuevoInstructor.InstructorId = Guid.NewGuid().ToString();
        //     var instructor = mapper.Map<Instructor>(nuevoInstructor);
        //     await this.dbContext.Instructores.AddAsync(instructor);
        //     await this.dbContext.SaveChangesAsync();
        //     return new CreatedAtRouteResult("GetInstructor", new {instructorId = nuevoInstructor.InstructorId}, 
        //         mapper.Map<InstructorDTO>(instructor));
        // }

        // [HttpPut("{instructorId}")]
        // public async Task<ActionResult> PutInstructor(string instructorId, [FromBody] Instructor ActualizarInstructor){
        //     logger.LogDebug($"Inicio del proceso de modificacion del Instructor con el id {instructorId}");
        //     Instructor instructor = await this.dbContext.Instructores.FirstOrDefaultAsync(a => a.InstructorId == instructorId);
        //     if(instructor == null)
        //     {
        //         logger.LogInformation($"No existe el Instructor con el id {instructorId}");
        //         return NotFound();
        //     }
        //     else
        //     {

        //         instructor.Apellidos = ActualizarInstructor.Apellidos;
        //         instructor.Comentario = ActualizarInstructor.Comentario;
        //         instructor.Direccion = ActualizarInstructor.Direccion;
        //         instructor.Estatus = ActualizarInstructor.Estatus;
        //         instructor.Foto = ActualizarInstructor.Foto;
        //         instructor.Nombres = ActualizarInstructor.Nombres;
        //         instructor.Telefono = ActualizarInstructor.Telefono;
        //         this.dbContext.Entry(instructor).State = EntityState.Modified;
        //         await this.dbContext.SaveChangesAsync();
        //         logger.LogInformation("Los datos del Instructor fueron actualizados exitosamente");
        //         return NoContent();
        //     }
        // }

        // [HttpDelete("{instructorId}")]
        // public async Task<ActionResult<InstructorDTO>> DeleteInstructor(String instructorId) 
        // {
        //     logger.LogDebug("Iniciando el procesos de eliminacion del Instructor");
        //     Instructor instructor = await this.dbContext.Instructores.FirstOrDefaultAsync(a => a.InstructorId == instructorId);
        //     if(instructor == null){
        //         logger.LogInformation($"No existe el Instructor con el Id {instructorId}");
        //         return NotFound();
        //     }
        //     else
        //     {
        //         this.dbContext.Instructores.Remove(instructor);
        //         await this.dbContext.SaveChangesAsync();
        //         logger.LogInformation($"Se ha realizado la eliminación del registro con el id {instructorId}");
        //         return mapper.Map<InstructorDTO>(instructor);
        //     }
        // }
    }
}