using SimuladorResiduos.Dtos;
using SimuladorResiduos.Requests;
using SimuladorResiduos.Results;
using SimuladorResiduos.Simulation;

namespace SimuladorResiduos.Services
{
    public class ServicioSimulacion
    {
        public RespuestaSimulacionFront EjecutarSimulacion(SolicitudSimulacion solicitud)
        {
            int seedUsada = solicitud.Seed ?? GeneradorSemilla.Crear();

            ConfiguracionSimulacion configuracion = new ConfiguracionSimulacion
            {
                CantDias = solicitud.CantDias,
                HorasLabDia = solicitud.HorasLabDia,
                CapacidadMaq = solicitud.CapacidadMaq,
                TiempoCamHoras = solicitud.TiempoCam,
                TiempoCamMinutos = solicitud.TiempoCam * 60.0,
                DispLote = solicitud.DispLote,
                CantTrituradoras = solicitud.CantTrituradoras,
                Seed = seedUsada
            };

            MotorSimulacion motor = new MotorSimulacion(configuracion);
            ResultadoSimulacion resultado = motor.Ejecutar();

            resultado.SeedUsada = seedUsada;

            return MapeadorResultadoFront.Mapear(resultado);
        }
    }
}