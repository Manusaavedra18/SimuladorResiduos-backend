using System.Text.Json.Serialization;

namespace SimuladorResiduos.Dtos
{
    public class RespuestaSimulacionFront
    {
        [JsonPropertyName("dailyReports")]
        public List<DailyReportDayDto> DailyReports { get; set; } = new();

        [JsonPropertyName("batchReports")]
        public List<BatchReportLotDto> BatchReports { get; set; } = new();

        [JsonPropertyName("globalResults")]
        public GlobalResultsDto GlobalResults { get; set; } = new();

        [JsonPropertyName("recoveredMaterials")]
        public List<RecoveredMaterialDto> RecoveredMaterials { get; set; } = new();
    }

    public class DailyReportDayDto
    {
        [JsonPropertyName("num_dia")]
        public int NumDia { get; set; }

        [JsonPropertyName("disp_dia")]
        public int DispDia { get; set; }

        [JsonPropertyName("cant_HDD3_dia")]
        public int CantHDD3Dia { get; set; }

        [JsonPropertyName("cant_HDD2_dia")]
        public int CantHDD2Dia { get; set; }

        [JsonPropertyName("cant_SSD2_dia")]
        public int CantSSD2Dia { get; set; }

        [JsonPropertyName("cant_SSDM_dia")]
        public int CantSSDMDia { get; set; }

        [JsonPropertyName("cant_CD_dia")]
        public int CantCDDia { get; set; }

        [JsonPropertyName("peso_total_dia")]
        public double PesoTotalDia { get; set; }

        [JsonPropertyName("ocup_maq_1_dia")]
        public double OcupMaq1Dia { get; set; }

        [JsonPropertyName("ocup_maq_2_dia")]
        public double OcupMaq2Dia { get; set; }

        [JsonPropertyName("ocup_maq_3_dia")]
        public double OcupMaq3Dia { get; set; }

        [JsonPropertyName("cant_lote_dia")]
        public int CantLoteDia { get; set; }

        [JsonPropertyName("recic_efectivo_dia")]
        public double RecicEfectivoDia { get; set; }
    }

    public class BatchReportLotDto
    {
        [JsonPropertyName("num_lote")]
        public int NumLote { get; set; }

        [JsonPropertyName("disp_lote")]
        public int DispLote { get; set; }

        [JsonPropertyName("cant_HDD3")]
        public int CantHDD3 { get; set; }

        [JsonPropertyName("cant_HDD2")]
        public int CantHDD2 { get; set; }

        [JsonPropertyName("cant_SSD2")]
        public int CantSSD2 { get; set; }

        [JsonPropertyName("cant_SSDM")]
        public int CantSSDM { get; set; }

        [JsonPropertyName("cant_CD")]
        public int CantCD { get; set; }

        [JsonPropertyName("peso_total_lote")]
        public double PesoTotalLote { get; set; }

        [JsonPropertyName("tiempo_trit_lote")]
        public string TiempoTritLote { get; set; } = "";

        [JsonPropertyName("tiempo_separ_lote")]
        public string TiempoSeparLote { get; set; } = "";

        [JsonPropertyName("tiempo_proces_lote")]
        public string TiempoProcesLote { get; set; } = "";

        [JsonPropertyName("maq_limite")]
        public int MaqLimite { get; set; }

        [JsonPropertyName("maq_uso")]
        public int MaqUso { get; set; }

        [JsonPropertyName("maq_libre")]
        public int MaqLibre { get; set; }

        [JsonPropertyName("num_dia")]
        public int NumDia { get; set; }
    }

    public class GlobalResultsDto
    {
        [JsonPropertyName("disp_total")]
        public int DispTotal { get; set; }

        [JsonPropertyName("peso_desper")]
        public double PesoDesper { get; set; }

        [JsonPropertyName("recic_efectivo")]
        public double RecicEfectivo { get; set; }

        [JsonPropertyName("dia_maj_cong")]
        public string DiaMajCong { get; set; } = "";

        [JsonPropertyName("disp_dia_maj")]
        public int DispDiaMaj { get; set; }
    }

    public class RecoveredMaterialDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = "kg";
    }
}