namespace SimuladorResiduos.Models
{
    public class Lote
    {
        public int Dia { get; set; }
        public int NumeroLote { get; set; }
        public double MinutoLlegada { get; set; }
        public List<Dispositivo> Dispositivos { get; set; } = new();

        public int DispLote => Dispositivos.Count;
        public double PesoTotalLote => Dispositivos.Sum(d => d.PesoKg);

        public double TiempoTritLote { get; set; }
        public double TiempoSeparLote => Dispositivos.Sum(d => d.TiempoSeparacion);

        public int CantHdd3 => Dispositivos.Count(d => d.Tipo == TipoDispositivo.Hdd35);
        public int CantHdd2 => Dispositivos.Count(d => d.Tipo == TipoDispositivo.Hdd25);
        public int CantSsd2 => Dispositivos.Count(d => d.Tipo == TipoDispositivo.Ssd25);
        public int CantSsdM => Dispositivos.Count(d => d.Tipo == TipoDispositivo.SsdM2);
        public int CantCd => Dispositivos.Count(d => d.Tipo == TipoDispositivo.CdDvd);
    }
}