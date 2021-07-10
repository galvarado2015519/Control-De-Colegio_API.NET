using System.Collections.Generic;
using System.Threading.Tasks;
using ApiControlDeColegio.DbContexts;
using ApiControlDeColegio.Entities;
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
        public ClasesController(DbContextApi dbContext, ILogger<ClasesController> logger)
        {
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
    }
}