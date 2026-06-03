namespace SimuladorResiduos.Models
{
    public class Dispositivo
    {
        public TipoDispositivo Tipo { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public double PesoKg { get; set; }
        public double TiempoTriturado { get; set; }
        public double TiempoSeparacion { get; set; }
        public double TiempoSensible { get; set; }
        public Dictionary<TipoMaterial, double> MaterialesKg { get; set; } = new();
    }
}
