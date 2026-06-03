namespace SimuladorResiduos.Results
{
    public class ResultadoDia
    {
        public int Dia { get; set; }

        public int CantLoteDia { get; set; }
        public int DispDia { get; set; }

        public int CantHdd3Dia { get; set; }
        public int CantHdd2Dia { get; set; }
        public int CantSsd2Dia { get; set; }
        public int CantSsdMDia { get; set; }
        public int CantCdDia { get; set; }

        public double PesoTotalDia { get; set; }

        public double PesoRecuperadoDia { get; set; }
        public double PesoDesperDia { get; set; }

        public double RecicEfectivoDia =>
            PesoTotalDia > 0
                ? PesoRecuperadoDia / PesoTotalDia * 100.0
                : 0;

        public Dictionary<int, double> OcupacionTrituradoras { get; set; } = new();

        public double UtilizacionPromedio =>
            OcupacionTrituradoras.Count > 0
                ? OcupacionTrituradoras.Values.Average()
                : 0;
    }
}