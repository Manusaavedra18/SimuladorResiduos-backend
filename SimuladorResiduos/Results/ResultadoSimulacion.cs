namespace SimuladorResiduos.Results
{
    public class ResultadoSimulacion
    {
        public int SeedUsada { get; set; }
        public int DispTotal { get; set; }
        public double PesoTotal { get; set; }

        public double PesoDesper { get; set; }

        public double PesoAl { get; set; }
        public double PesoFe { get; set; }
        public double PesoPlast { get; set; }
        public double PesoCu { get; set; }
        public double PesoSi { get; set; }
        public double PesoTierrasRaras { get; set; }

        public double PesoAu { get; set; }
        public double PesoAg { get; set; }
        public double PesoPd { get; set; }

        public double PesoMetalesPreciosos => PesoAu + PesoAg + PesoPd;

        public double PesoRecuperado =>
            PesoAl
            + PesoFe
            + PesoPlast
            + PesoCu
            + PesoSi
            + PesoTierrasRaras
            + PesoAu
            + PesoAg
            + PesoPd;

        public double RecicEfectivo =>
            PesoTotal > 0
            ? PesoRecuperado / PesoTotal * 100.0
            : 0;

        public int DiaMajCongestion { get; set; }
        public int DispDiaMajor { get; set; }

        public List<ResultadoLote> ResultadosLotes { get; set; } = new();
        public List<ResultadoDia> ResultadosDias { get; set; } = new();
    }
}