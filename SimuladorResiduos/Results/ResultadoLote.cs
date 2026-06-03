namespace SimuladorResiduos.Results
{
    public class ResultadoLote
    {
        public int Dia { get; set; }
        public int NumeroLote { get; set; }

        public int DispLote { get; set; }

        public int CantHdd3 { get; set; }
        public int CantHdd2 { get; set; }
        public int CantSsd2 { get; set; }
        public int CantSsdM { get; set; }
        public int CantCd { get; set; }

        public double PesoTotalLote { get; set; }

        public double TiempoTritLote { get; set; }
        public double TiempoSeparLote { get; set; }

        public double TiempoTotalLote => TiempoTritLote + TiempoSeparLote;

        public int MaqUso { get; set; }
        public int MaqLibre { get; set; }
        public int MaqLimite { get; set; }

        public double OcupMaq1 { get; set; }
        public double OcupMaq2 { get; set; }
        public double OcupMaq3 { get; set; }

        public int DispMaq1 { get; set; }
        public int DispMaq2 { get; set; }
        public int DispMaq3 { get; set; }

        public double TiempoTritMaq1 { get; set; }
        public double TiempoTritMaq2 { get; set; }
        public double TiempoTritMaq3 { get; set; }
    }
}