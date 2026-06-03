using SimuladorResiduos.Models;
using SimuladorResiduos.Results;

namespace SimuladorResiduos.Simulation
{
    public class EstadoSimulacion
    {
        private readonly ConfiguracionSimulacion configuracion;

        public List<Trituradora> Trituradoras { get; private set; } = new();
        public List<ResultadoLote> ResultadosLotes { get; private set; } = new();
        public List<ResultadoDia> ResultadosDias { get; private set; } = new();

        public int DispTotal { get; private set; }
        public double PesoTotal { get; private set; }

        public double PesoAl { get; private set; }
        public double PesoFe { get; private set; }
        public double PesoPlast { get; private set; }
        public double PesoCu { get; private set; }
        public double PesoSi { get; private set; }
        public double PesoTierrasRaras { get; private set; }
        public double PesoAu { get; private set; }
        public double PesoAg { get; private set; }
        public double PesoPd { get; private set; }
        public double PesoDesper { get; private set; }

        private int cantLoteDia;
        private int dispDia;
        private double pesoTotalDia;
        private double pesoRecuperadoDia;
        private double pesoDesperDia;

        private int cantHdd3Dia;
        private int cantHdd2Dia;
        private int cantSsd2Dia;
        private int cantSsdMDia;
        private int cantCdDia;

        private double acumOcupMaq1Dia;
        private double acumOcupMaq2Dia;
        private double acumOcupMaq3Dia;

        public EstadoSimulacion(ConfiguracionSimulacion configuracion)
        {
            this.configuracion = configuracion;

            for (int i = 1; i <= configuracion.CantTrituradoras; i++)
            {
                Trituradoras.Add(new Trituradora
                {
                    Id = i,
                    DispMaq = 0,
                    TiempoTritMaq = 0,
                    OcupacionMaq = 0
                });
            }
        }

        public void IniciarDia()
        {
            cantLoteDia = 0;
            dispDia = 0;
            pesoTotalDia = 0;
            pesoRecuperadoDia = 0;
            pesoDesperDia = 0;

            cantHdd3Dia = 0;
            cantHdd2Dia = 0;
            cantSsd2Dia = 0;
            cantSsdMDia = 0;
            cantCdDia = 0;

            acumOcupMaq1Dia = 0;
            acumOcupMaq2Dia = 0;
            acumOcupMaq3Dia = 0;

            ReiniciarTrituradoras();
        }

        public void ReiniciarTrituradoras()
        {
            foreach (Trituradora trituradora in Trituradoras)
            {
                trituradora.DispMaq = 0;
                trituradora.TiempoTritMaq = 0;
                trituradora.OcupacionMaq = 0;
            }
        }

        public void ConfigurarTrituradora(int indice, int dispMaq, double tiempoTritMaq)
        {
            if (indice < 0 || indice >= Trituradoras.Count)
            {
                return;
            }

            Trituradora trituradora = Trituradoras[indice];

            trituradora.DispMaq = dispMaq;
            trituradora.TiempoTritMaq = tiempoTritMaq;

            if (configuracion.CapacidadMaq > 0)
            {
                trituradora.OcupacionMaq = (double)dispMaq / configuracion.CapacidadMaq * 100.0;
            }
            else
            {
                trituradora.OcupacionMaq = 0;
            }
        }

        public void RegistrarDispositivo(Dispositivo dispositivo)
        {
            DispTotal++;
            PesoTotal += dispositivo.PesoKg;

            dispDia++;
            pesoTotalDia += dispositivo.PesoKg;

            RegistrarTipoDispositivoDia(dispositivo.Tipo);
            RegistrarMateriales(dispositivo);
        }

        public void RegistrarLote(Lote lote)
        {
            cantLoteDia++;

            int maqUso = Trituradoras.Count(t => t.DispMaq > 0);
            int maqLibre = configuracion.CantTrituradoras - maqUso;
            int maqLimite = Trituradoras.Count(t => t.DispMaq >= configuracion.CapacidadMaq);

            double ocupMaq1 = Trituradoras.Count > 0 ? Trituradoras[0].OcupacionMaq : 0;
            double ocupMaq2 = Trituradoras.Count > 1 ? Trituradoras[1].OcupacionMaq : 0;
            double ocupMaq3 = Trituradoras.Count > 2 ? Trituradoras[2].OcupacionMaq : 0;

            acumOcupMaq1Dia += ocupMaq1;
            acumOcupMaq2Dia += ocupMaq2;
            acumOcupMaq3Dia += ocupMaq3;

            ResultadoLote resultadoLote = new ResultadoLote
            {
                Dia = lote.Dia,
                NumeroLote = lote.NumeroLote,
                DispLote = lote.DispLote,

                CantHdd3 = lote.CantHdd3,
                CantHdd2 = lote.CantHdd2,
                CantSsd2 = lote.CantSsd2,
                CantSsdM = lote.CantSsdM,
                CantCd = lote.CantCd,

                PesoTotalLote = lote.PesoTotalLote,
                TiempoTritLote = lote.TiempoTritLote,
                TiempoSeparLote = lote.TiempoSeparLote,

                MaqUso = maqUso,
                MaqLibre = maqLibre,
                MaqLimite = maqLimite,

                OcupMaq1 = ocupMaq1,
                OcupMaq2 = ocupMaq2,
                OcupMaq3 = ocupMaq3,

                DispMaq1 = Trituradoras.Count > 0 ? Trituradoras[0].DispMaq : 0,
                DispMaq2 = Trituradoras.Count > 1 ? Trituradoras[1].DispMaq : 0,
                DispMaq3 = Trituradoras.Count > 2 ? Trituradoras[2].DispMaq : 0,

                TiempoTritMaq1 = Trituradoras.Count > 0 ? Trituradoras[0].TiempoTritMaq : 0,
                TiempoTritMaq2 = Trituradoras.Count > 1 ? Trituradoras[1].TiempoTritMaq : 0,
                TiempoTritMaq3 = Trituradoras.Count > 2 ? Trituradoras[2].TiempoTritMaq : 0
            };

            ResultadosLotes.Add(resultadoLote);
        }

        public void CerrarDia(int dia)
        {
            ResultadoDia resultadoDia = new ResultadoDia
            {
                Dia = dia,
                CantLoteDia = cantLoteDia,
                DispDia = dispDia,

                CantHdd3Dia = cantHdd3Dia,
                CantHdd2Dia = cantHdd2Dia,
                CantSsd2Dia = cantSsd2Dia,
                CantSsdMDia = cantSsdMDia,
                CantCdDia = cantCdDia,

                PesoTotalDia = pesoTotalDia,
                PesoRecuperadoDia = pesoRecuperadoDia,
                PesoDesperDia = pesoDesperDia
            };

            if (cantLoteDia > 0)
            {
                resultadoDia.OcupacionTrituradoras[1] = acumOcupMaq1Dia / cantLoteDia;
                resultadoDia.OcupacionTrituradoras[2] = acumOcupMaq2Dia / cantLoteDia;
                resultadoDia.OcupacionTrituradoras[3] = acumOcupMaq3Dia / cantLoteDia;
            }
            else
            {
                resultadoDia.OcupacionTrituradoras[1] = 0;
                resultadoDia.OcupacionTrituradoras[2] = 0;
                resultadoDia.OcupacionTrituradoras[3] = 0;
            }

            ResultadosDias.Add(resultadoDia);
        }

        public ResultadoSimulacion ConstruirResultadoFinal()
        {
            ResultadoDia? diaMayorCongestion = ResultadosDias
                .OrderByDescending(d => d.DispDia)
                .FirstOrDefault();

            return new ResultadoSimulacion
            {
                DispTotal = DispTotal,
                PesoTotal = PesoTotal,

                PesoAl = PesoAl,
                PesoFe = PesoFe,
                PesoPlast = PesoPlast,
                PesoCu = PesoCu,
                PesoSi = PesoSi,
                PesoTierrasRaras = PesoTierrasRaras,
                PesoAu = PesoAu,
                PesoAg = PesoAg,
                PesoPd = PesoPd,
                PesoDesper = PesoDesper,

                DiaMajCongestion = diaMayorCongestion?.Dia ?? 0,
                DispDiaMajor = diaMayorCongestion?.DispDia ?? 0,

                ResultadosLotes = ResultadosLotes,
                ResultadosDias = ResultadosDias
            };
        }

        private void RegistrarTipoDispositivoDia(TipoDispositivo tipo)
        {
            if (tipo == TipoDispositivo.Hdd35)
            {
                cantHdd3Dia++;
            }
            else if (tipo == TipoDispositivo.Hdd25)
            {
                cantHdd2Dia++;
            }
            else if (tipo == TipoDispositivo.Ssd25)
            {
                cantSsd2Dia++;
            }
            else if (tipo == TipoDispositivo.SsdM2)
            {
                cantSsdMDia++;
            }
            else if (tipo == TipoDispositivo.CdDvd)
            {
                cantCdDia++;
            }
        }

        private void RegistrarMateriales(Dispositivo dispositivo)
        {
            double aluminio = dispositivo.MaterialesKg[TipoMaterial.Aluminio];
            double aceroHierro = dispositivo.MaterialesKg[TipoMaterial.AceroHierro];
            double plasticos = dispositivo.MaterialesKg[TipoMaterial.PlasticosPolimeros];
            double cobre = dispositivo.MaterialesKg[TipoMaterial.Cobre];
            double silicio = dispositivo.MaterialesKg[TipoMaterial.Silicio];
            double tierrasRaras = dispositivo.MaterialesKg[TipoMaterial.TierrasRaras];
            double oro = dispositivo.MaterialesKg[TipoMaterial.Oro];
            double plata = dispositivo.MaterialesKg[TipoMaterial.Plata];
            double paladio = dispositivo.MaterialesKg[TipoMaterial.Paladio];
            double desperdicio = dispositivo.MaterialesKg[TipoMaterial.Desperdicio];

            PesoAl += aluminio;
            PesoFe += aceroHierro;
            PesoPlast += plasticos;
            PesoCu += cobre;
            PesoSi += silicio;
            PesoTierrasRaras += tierrasRaras;
            PesoAu += oro;
            PesoAg += plata;
            PesoPd += paladio;
            PesoDesper += desperdicio;

            pesoRecuperadoDia += aluminio
                + aceroHierro
                + plasticos
                + cobre
                + silicio
                + tierrasRaras
                + oro
                + plata
                + paladio;

            pesoDesperDia += desperdicio;
        }
    }
}