namespace SimuladorResiduos.Simulation
{
    public static class GeneradorSemilla
    {
        public static int Crear()
        {
            DateTime ahora = DateTime.Now;

            int semilla =
                ahora.Hour * 10000000 +
                ahora.Minute * 100000 +
                ahora.Second * 1000 +
                ahora.Millisecond;

            if (semilla <= 0)
            {
                semilla = 1;
            }

            return semilla;
        }
    }
}