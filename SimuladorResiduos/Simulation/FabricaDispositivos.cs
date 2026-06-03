using SimuladorResiduos.Models;

namespace SimuladorResiduos.Simulation
{
    public class FabricaDispositivos
    {
        private readonly Distribuciones distribuciones;

        public FabricaDispositivos(Distribuciones distribuciones)
        {
            this.distribuciones = distribuciones;
        }

        public Dispositivo CrearDispositivo(TipoDispositivo tipo, TipoCliente tipoCliente)
        {
            double pesoKg = GenerarPesoKg(tipo);
            double tiempoSensible = GenerarTiempoSensible(tipoCliente);
            double tiempoTriturado = GenerarTiempoTriturado(tipo) + tiempoSensible;
            double tiempoSeparacion = GenerarTiempoSeparacion(tipo);
            Dictionary<TipoMaterial, double> materialesKg = GenerarMateriales(tipo, pesoKg);

            return new Dispositivo
            {
                Tipo = tipo,
                TipoCliente = tipoCliente,
                PesoKg = pesoKg,
                TiempoTriturado = tiempoTriturado,
                TiempoSeparacion = tiempoSeparacion,
                TiempoSensible = tiempoSensible,
                MaterialesKg = materialesKg
            };
        }

        private double GenerarPesoKg(TipoDispositivo tipo)
        {
            double pesoGramos = tipo switch
            {
                TipoDispositivo.Hdd35 => distribuciones.Uniforme(400, 750),
                TipoDispositivo.Hdd25 => distribuciones.Uniforme(80, 150),
                TipoDispositivo.Ssd25 => distribuciones.Uniforme(50, 80),
                TipoDispositivo.SsdM2 => distribuciones.Uniforme(6, 10),
                TipoDispositivo.CdDvd => distribuciones.Uniforme(14, 18),
                _ => 0
            };

            return pesoGramos / 1000.0;
        }

        private double GenerarTiempoTriturado(TipoDispositivo tipo)
        {
            double tiempo = tipo switch
            {
                TipoDispositivo.Hdd35 => distribuciones.Normal(45, 5),
                TipoDispositivo.Hdd25 => distribuciones.Normal(30, 4),
                TipoDispositivo.Ssd25 => distribuciones.Normal(20, 3),
                TipoDispositivo.SsdM2 => distribuciones.Normal(10, 2),
                TipoDispositivo.CdDvd => distribuciones.Normal(5, 1),
                _ => 0
            };

            if (tiempo < 0)
            {
                tiempo = 0;
            }

            return tiempo;
        }

        private double GenerarTiempoSensible(TipoCliente tipoCliente)
        {
            if (tipoCliente != TipoCliente.Sensible)
            {
                return 0;
            }

            double tiempo = distribuciones.Normal(15, 5);

            if (tiempo < 0)
            {
                tiempo = 0;
            }

            return tiempo;
        }

        private double GenerarTiempoSeparacion(TipoDispositivo tipo)
        {
            double tiempo = tipo switch
            {
                TipoDispositivo.Hdd35 => distribuciones.Normal(600, 120),
                TipoDispositivo.Hdd25 => distribuciones.Normal(600, 120),
                TipoDispositivo.Ssd25 => distribuciones.Normal(360, 60),
                TipoDispositivo.SsdM2 => distribuciones.Normal(360, 60),
                TipoDispositivo.CdDvd => distribuciones.Normal(120, 30),
                _ => 0
            };

            if (tiempo < 0)
            {
                tiempo = 0;
            }

            return tiempo;
        }

        private Dictionary<TipoMaterial, double> GenerarMateriales(TipoDispositivo tipo, double pesoKg)
        {
            if (tipo == TipoDispositivo.Hdd35 || tipo == TipoDispositivo.Hdd25)
            {
                return GenerarMaterialesHdd(pesoKg);
            }

            if (tipo == TipoDispositivo.Ssd25 || tipo == TipoDispositivo.SsdM2)
            {
                return GenerarMaterialesSsd(pesoKg);
            }

            return GenerarMaterialesCdDvd(pesoKg);
        }

        private Dictionary<TipoMaterial, double> GenerarMaterialesHdd(double pesoKg)
        {
            Dictionary<TipoMaterial, double> materiales = CrearDiccionarioMateriales();

            materiales[TipoMaterial.Aluminio] = distribuciones.Uniforme(0.50, 0.70) * pesoKg;
            materiales[TipoMaterial.AceroHierro] = distribuciones.Uniforme(0.10, 0.20) * pesoKg;
            materiales[TipoMaterial.PlasticosPolimeros] = distribuciones.Uniforme(0.03, 0.08) * pesoKg;
            materiales[TipoMaterial.Cobre] = distribuciones.Uniforme(0.02, 0.05) * pesoKg;
            materiales[TipoMaterial.Silicio] = distribuciones.Uniforme(0.00, 0.01) * pesoKg;
            materiales[TipoMaterial.TierrasRaras] = distribuciones.Uniforme(0.01, 0.03) * pesoKg;

            double porcentajeOro = distribuciones.Uniforme(0.00, 0.001);
            double porcentajePlata = distribuciones.Uniforme(0.00, 0.001);
            double porcentajePaladio = distribuciones.Uniforme(0.00, 0.001);

            materiales[TipoMaterial.Oro] = porcentajeOro * pesoKg;
            materiales[TipoMaterial.Plata] = porcentajePlata * pesoKg;
            materiales[TipoMaterial.Paladio] = porcentajePaladio * pesoKg;

            AgregarDesperdicio(materiales, pesoKg);

            return materiales;
        }

        private Dictionary<TipoMaterial, double> GenerarMaterialesSsd(double pesoKg)
        {
            Dictionary<TipoMaterial, double> materiales = CrearDiccionarioMateriales();

            materiales[TipoMaterial.Aluminio] = distribuciones.Uniforme(0.05, 0.15) * pesoKg;
            materiales[TipoMaterial.AceroHierro] = distribuciones.Uniforme(0.03, 0.05) * pesoKg;
            materiales[TipoMaterial.PlasticosPolimeros] = distribuciones.Uniforme(0.20, 0.30) * pesoKg;
            materiales[TipoMaterial.Cobre] = distribuciones.Uniforme(0.10, 0.20) * pesoKg;
            materiales[TipoMaterial.Silicio] = distribuciones.Uniforme(0.15, 0.25) * pesoKg;
            materiales[TipoMaterial.TierrasRaras] = 0;

            double porcentajeOro = distribuciones.Uniforme(0.005, 0.01);
            double porcentajePlata = distribuciones.Uniforme(0.005, 0.01);
            double porcentajePaladio = distribuciones.Uniforme(0.005, 0.01);

            materiales[TipoMaterial.Oro] = porcentajeOro * pesoKg;
            materiales[TipoMaterial.Plata] = porcentajePlata * pesoKg;
            materiales[TipoMaterial.Paladio] = porcentajePaladio * pesoKg;

            AgregarDesperdicio(materiales, pesoKg);

            return materiales;
        }

        private Dictionary<TipoMaterial, double> GenerarMaterialesCdDvd(double pesoKg)
        {
            Dictionary<TipoMaterial, double> materiales = CrearDiccionarioMateriales();

            materiales[TipoMaterial.Aluminio] = distribuciones.Uniforme(0.01, 0.03) * pesoKg;
            materiales[TipoMaterial.PlasticosPolimeros] = distribuciones.Uniforme(0.95, 0.97) * pesoKg;

            AgregarDesperdicio(materiales, pesoKg);

            return materiales;
        }

        private Dictionary<TipoMaterial, double> CrearDiccionarioMateriales()
        {
            Dictionary<TipoMaterial, double> materiales = new();

            foreach (TipoMaterial tipoMaterial in Enum.GetValues<TipoMaterial>())
            {
                materiales[tipoMaterial] = 0;
            }

            return materiales;
        }

        private void AgregarDesperdicio(Dictionary<TipoMaterial, double> materiales, double pesoKg)
        {
            double pesoRecuperado = 0;

            foreach (var material in materiales)
            {
                if (material.Key != TipoMaterial.Desperdicio)
                {
                    pesoRecuperado += material.Value;
                }
            }

            double desperdicio = pesoKg - pesoRecuperado;

            if (desperdicio < 0)
            {
                desperdicio = 0;
            }

            materiales[TipoMaterial.Desperdicio] = desperdicio;
        }
    }
}