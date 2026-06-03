namespace SimuladorResiduos.Simulation
{
    public class ConfiguracionSimulacion
    {
        public int CantDias { get; set; }
        public int HorasLabDia { get; set; }
        public int CapacidadMaq { get; set; }
        public double TiempoCamHoras { get; set; }
        public double TiempoCamMinutos { get; set; }
        public double DispLote { get; set; }
        public int CantTrituradoras { get; set; }
        public int Seed { get; set; }
    }
}