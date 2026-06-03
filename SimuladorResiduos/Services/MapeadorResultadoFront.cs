using SimuladorResiduos.Dtos;
using SimuladorResiduos.Results;
using System.Globalization;

namespace SimuladorResiduos.Services
{
    public static class MapeadorResultadoFront
    {
        public static RespuestaSimulacionFront Mapear(ResultadoSimulacion resultado)
        {
            return new RespuestaSimulacionFront
            {
                DailyReports = resultado.ResultadosDias
                    .Select(MapearDia)
                    .ToList(),

                BatchReports = resultado.ResultadosLotes
                    .Select(MapearLote)
                    .ToList(),

                GlobalResults = MapearGlobal(resultado),

                RecoveredMaterials = MapearMateriales(resultado)
            };
        }

        private static DailyReportDayDto MapearDia(ResultadoDia dia)
        {
            return new DailyReportDayDto
            {
                NumDia = dia.Dia,
                DispDia = dia.DispDia,

                CantHDD3Dia = dia.CantHdd3Dia,
                CantHDD2Dia = dia.CantHdd2Dia,
                CantSSD2Dia = dia.CantSsd2Dia,
                CantSSDMDia = dia.CantSsdMDia,
                CantCDDia = dia.CantCdDia,

                PesoTotalDia = Redondear(dia.PesoTotalDia),

                OcupMaq1Dia = ObtenerOcupacion(dia, 1),
                OcupMaq2Dia = ObtenerOcupacion(dia, 2),
                OcupMaq3Dia = ObtenerOcupacion(dia, 3),

                CantLoteDia = dia.CantLoteDia,
                RecicEfectivoDia = Redondear(dia.RecicEfectivoDia)
            };
        }

        private static BatchReportLotDto MapearLote(ResultadoLote lote)
        {
            return new BatchReportLotDto
            {
                NumLote = lote.NumeroLote,
                DispLote = lote.DispLote,

                CantHDD3 = lote.CantHdd3,
                CantHDD2 = lote.CantHdd2,
                CantSSD2 = lote.CantSsd2,
                CantSSDM = lote.CantSsdM,
                CantCD = lote.CantCd,

                PesoTotalLote = Redondear(lote.PesoTotalLote),

                TiempoTritLote = FormatearTiempo(lote.TiempoTritLote),
                TiempoSeparLote = FormatearTiempo(lote.TiempoSeparLote),
                TiempoProcesLote = FormatearTiempo(lote.TiempoTotalLote),

                MaqLimite = lote.MaqLimite,
                MaqUso = lote.MaqUso,
                MaqLibre = lote.MaqLibre,
                NumDia = lote.Dia
            };
        }

        private static GlobalResultsDto MapearGlobal(ResultadoSimulacion resultado)
        {
            return new GlobalResultsDto
            {
                DispTotal = resultado.DispTotal,
                PesoDesper = Redondear(resultado.PesoDesper),
                RecicEfectivo = Redondear(resultado.RecicEfectivo),
                DiaMajCong = ObtenerFechaCortaDesdeDia(resultado.DiaMajCongestion),
                DispDiaMaj = resultado.DispDiaMajor
            };
        }

        private static List<RecoveredMaterialDto> MapearMateriales(ResultadoSimulacion resultado)
        {
            return new List<RecoveredMaterialDto>
            {
                CrearMaterial("Aluminio", resultado.PesoAl),
                CrearMaterial("Hierro", resultado.PesoFe),
                CrearMaterial("Plastico", resultado.PesoPlast),
                CrearMaterial("Cobre", resultado.PesoCu),
                CrearMaterial("Silicio", resultado.PesoSi),
                CrearMaterial("Tierras raras", resultado.PesoTierrasRaras),
                CrearMaterial("Oro", resultado.PesoAu),
                CrearMaterial("Plata", resultado.PesoAg),
                CrearMaterial("Paladio", resultado.PesoPd),
                CrearMaterial("Desperdicio", resultado.PesoDesper)
            };
        }

        private static RecoveredMaterialDto CrearMaterial(string nombre, double valor)
        {
            return new RecoveredMaterialDto
            {
                Name = nombre,
                Value = Redondear(valor),
                Unit = "kg"
            };
        }

        private static double ObtenerOcupacion(ResultadoDia dia, int maquina)
        {
            if (dia.OcupacionTrituradoras.ContainsKey(maquina))
            {
                return Redondear(dia.OcupacionTrituradoras[maquina]);
            }

            return 0;
        }

        private static string FormatearTiempo(double segundos)
        {
            TimeSpan tiempo = TimeSpan.FromSeconds(segundos);

            if (tiempo.TotalHours >= 1)
            {
                return $"{(int)tiempo.TotalHours}h {tiempo.Minutes}m {tiempo.Seconds}s";
            }

            if (tiempo.TotalMinutes >= 1)
            {
                return $"{tiempo.Minutes}m {tiempo.Seconds}s";
            }

            return $"{tiempo.Seconds}s";
        }

        private static double Redondear(double valor)
        {
            return Math.Round(valor, 2);
        }

        private static string ObtenerFechaCortaDesdeDia(int dia)
        {
            if (dia <= 0)
            {
                return "";
            }

            DateTime fecha = DateTime.Today.AddDays(dia - 1);

            CultureInfo cultura = new CultureInfo("es-AR");

            string texto = fecha.ToString("dddd dd MMM", cultura);

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }
    }
}