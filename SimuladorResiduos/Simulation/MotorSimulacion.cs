using SimuladorResiduos.Models;
using SimuladorResiduos.Results;

namespace SimuladorResiduos.Simulation
{
    public class MotorSimulacion
    {
        private readonly ConfiguracionSimulacion configuracion;
        private readonly Distribuciones distribuciones;
        private readonly FabricaDispositivos fabricaDispositivos;
        private readonly EstadoSimulacion estado;

        private int numeroLoteGlobal = 0;

        public MotorSimulacion(ConfiguracionSimulacion configuracion)
        {
            this.configuracion = configuracion;

            GeneradorAleatorio generador = new GeneradorAleatorio(configuracion.Seed);
            distribuciones = new Distribuciones(generador);
            fabricaDispositivos = new FabricaDispositivos(distribuciones);
            estado = new EstadoSimulacion(configuracion);
        }

        public ResultadoSimulacion Ejecutar()
        {
            for (int dia = 1; dia <= configuracion.CantDias; dia++)
            {
                EjecutarDia(dia);
            }

            return estado.ConstruirResultadoFinal();
        }

        private void EjecutarDia(int dia)
        {
            estado.IniciarDia();

            double horasLabDia = 0;

            while (horasLabDia < configuracion.HorasLabDia)
            {
                double tiempoCamion = GenerarTiempoCamionHoras();
                horasLabDia += tiempoCamion;

                int dispLote = GenerarCantidadDispositivosLote();

                numeroLoteGlobal++;

                Lote lote = new Lote
                {
                    Dia = dia,
                    NumeroLote = numeroLoteGlobal,
                    MinutoLlegada = horasLabDia * 60
                };

                int[] dispositivosPorMaquina = CalcularDispositivosPorMaquina(dispLote);

                estado.ReiniciarTrituradoras();

                double tiempoTritLote = 0;

                for (int i = 0; i < configuracion.CantTrituradoras; i++)
                {
                    int dispMaq = dispositivosPorMaquina[i];
                    double tiempoTritMaq = 0;

                    for (int j = 0; j < dispMaq; j++)
                    {
                        TipoDispositivo tipoDispositivo = distribuciones.ObtenerTipoDispositivo();
                        TipoCliente tipoCliente = distribuciones.ObtenerTipoCliente();

                        Dispositivo dispositivo = fabricaDispositivos.CrearDispositivo(tipoDispositivo, tipoCliente);

                        lote.Dispositivos.Add(dispositivo);

                        tiempoTritMaq += dispositivo.TiempoTriturado;

                        estado.RegistrarDispositivo(dispositivo);
                    }

                    estado.ConfigurarTrituradora(i, dispMaq, tiempoTritMaq);

                    if (tiempoTritMaq > tiempoTritLote)
                    {
                        tiempoTritLote = tiempoTritMaq;
                    }
                }

                lote.TiempoTritLote = tiempoTritLote;

                estado.RegistrarLote(lote);
            }

            estado.CerrarDia(dia);
        }

        private int[] CalcularDispositivosPorMaquina(int dispLote)
        {
            int[] dispMaq = new int[configuracion.CantTrituradoras];

            int capacidadMaq = configuracion.CapacidadMaq;
            int restantes = dispLote;

            for (int i = 0; i < configuracion.CantTrituradoras; i++)
            {
                if (restantes <= 0)
                {
                    dispMaq[i] = 0;
                }
                else if (restantes >= capacidadMaq)
                {
                    dispMaq[i] = capacidadMaq;
                    restantes -= capacidadMaq;
                }
                else
                {
                    dispMaq[i] = restantes;
                    restantes = 0;
                }
            }

            return dispMaq;
        }

        private int GenerarCantidadDispositivosLote()
        {
            int cantidad = distribuciones.Poisson(configuracion.DispLote);

            if (cantidad < 1)
            {
                cantidad = 1;
            }

            return cantidad;
        }

        private double GenerarTiempoCamionHoras()
        {
            double alfa = 1.0 / configuracion.TiempoCamHoras;
            double tiempo = distribuciones.Exponencial(alfa);

            if (tiempo < 0)
            {
                tiempo = 0;
            }

            return tiempo;
        }
    }
}