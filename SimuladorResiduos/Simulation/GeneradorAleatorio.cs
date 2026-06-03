namespace SimuladorResiduos.Simulation
{
    public class GeneradorAleatorio
    {
        private long x;

        private const long A = 1664525;
        private const long C = 1013904223;
        private const long M = 4294967296;

        public GeneradorAleatorio(int semilla)
        {
            if (semilla <= 0)
            {
                semilla = 1;
            }

            x = semilla;
        }

        public double SiguienteU()
        {
            x = (A * x + C) % M;

            return (double)x / M;
        }
    }
}