using SimuladorResiduos.Models;

namespace SimuladorResiduos.Simulation
{
    public class Distribuciones
    {
        private readonly GeneradorAleatorio generador;

        public Distribuciones(GeneradorAleatorio generador)
        {
            this.generador = generador;
        }

        public double Uniforme(double a, double b)
        {
            double u = generador.SiguienteU();
            return a + (b - a) * u;
        }

        public double Exponencial(double alfa)
        {
            double u = generador.SiguienteU();
            return -(1.0 / alfa) * Math.Log(u);
        }

        public double Normal(double media, double desviacion)
        {
            double suma = 0;

            for (int i = 1; i <= 12; i++)
            {
                double u = generador.SiguienteU();
                suma = suma + u;
            }

            double x = desviacion * (suma - 6) + media;

            return x;
        }

        public int Poisson(double lambda)
        {
            if (lambda <= 0)
            {
                return 0;
            }

            int x = 0;
            double sumaLog = 0;
            double limite = -lambda;

            do
            {
                x++;

                double u = generador.SiguienteU();

                if (u <= 0)
                {
                    u = 0.0000001;
                }

                sumaLog += Math.Log(u);
            }
            while (sumaLog > limite);

            return x - 1;
        }

        public TipoDispositivo ObtenerTipoDispositivo()
        {
            double u = generador.SiguienteU();

            if (u <= 0.35) return TipoDispositivo.Hdd35;
            if (u <= 0.50) return TipoDispositivo.Hdd25;
            if (u <= 0.70) return TipoDispositivo.Ssd25;
            if (u <= 0.95) return TipoDispositivo.SsdM2;

            return TipoDispositivo.CdDvd;
        }

        public TipoCliente ObtenerTipoCliente()
        {
            double u = generador.SiguienteU();

            if (u <= 0.70)
            {
                return TipoCliente.Normal;
            }

            return TipoCliente.Sensible;
        }
    }
}