namespace SimuladorResiduos.Requests
{
    public class SolicitudSimulacion
    {
        public int CantDias { get; set; } = 1;
        public int HorasLabDia { get; set; } = 16;
        public int CapacidadMaq { get; set; } = 500;
        public double TiempoCam { get; set; } = 1;
        public double DispLote { get; set; } = 3000;
        public int CantTrituradoras { get; set; } = 3;
        public int? Seed { get; set; }
    }
}