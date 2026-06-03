using Microsoft.AspNetCore.Mvc;
using SimuladorResiduos.Dtos;
using SimuladorResiduos.Requests;
using SimuladorResiduos.Services;

namespace SimuladorResiduos.Controllers
{
    [ApiController]
    [Route("api/simulacion")]
    public class SimulacionController : ControllerBase
    {
        private readonly ServicioSimulacion servicioSimulacion;

        public SimulacionController(ServicioSimulacion servicioSimulacion)
        {
            this.servicioSimulacion = servicioSimulacion;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                mensaje = "Backend funcionando"
            });
        }

        [HttpPost("ejecutar")]
        public ActionResult<RespuestaSimulacionFront> Ejecutar([FromBody] SolicitudSimulacion solicitud)
        {
            RespuestaSimulacionFront resultado = servicioSimulacion.EjecutarSimulacion(solicitud);
            return Ok(resultado);
        }
    }
}