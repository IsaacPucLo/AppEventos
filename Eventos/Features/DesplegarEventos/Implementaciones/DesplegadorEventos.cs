using Features.ProcesarEventos;
using System;
using System.Collections.Generic;

namespace Features.DesplegarEventos
{
    public class DesplegadorEventos : IDesplegadorEventos
    {
        private const string PALABRA_TIEMPO_PASADO = "ocurrió";
        private readonly IVisualizadorMensaje _Visualizador;

        public DesplegadorEventos(IVisualizadorMensaje Visualizador)
        {
            _Visualizador = Visualizador ?? throw new ArgumentNullException(nameof(Visualizador));
        }

        public void DesplegarEventos(List<EventoProcesado> EventosPorDesplegar)
        {
            foreach (var EventoPorDesplegar in EventosPorDesplegar)
            {
                DesplegarEventoIndividual(EventoPorDesplegar);
            }
        }

        private void DesplegarEventoIndividual(EventoProcesado EventoPorDesplegar)
        {
            var MensajePorDesplegar = string.Empty;
            string PalabraRango = DeterminarPalabraRango(EventoPorDesplegar);
            if (EventoPorDesplegar.EventoPasado)
            {
                MensajePorDesplegar = $"{EventoPorDesplegar.Nombre} {PALABRA_TIEMPO_PASADO} hace {EventoPorDesplegar.Diferencia} {PalabraRango}";
            }
            else
            {
                //Completar para los eventos del futuro.
            }

            _Visualizador.VisualizarMensaje(MensajePorDesplegar);
        }

        private string DeterminarPalabraRango(EventoProcesado EventoPorDesplegar)
        {
            if (EventoPorDesplegar.Diferencia==1)
            {
                return RecuperarPalabraRangoSingular(EventoPorDesplegar.Rango);
            }
            else
            {
                return RecuperarPalabraRangoPlural(EventoPorDesplegar.Rango);
            }
        }

        private string RecuperarPalabraRangoPlural(RangoDiferencia Rango)
        {
            switch (Rango)
            {
                case RangoDiferencia.Minutos:
                    return "Minutos";
                    
                case RangoDiferencia.Horas:
                    return "Horas";
                case RangoDiferencia.Dias:
                    return "Días";
                    
                case RangoDiferencia.Meses:
                    return "Meses";
                
            }
            return "";
        }

        private string RecuperarPalabraRangoSingular(RangoDiferencia Rango)
        {
            switch (Rango)
            {
                case RangoDiferencia.Minutos:
                    return "Minuto";

                case RangoDiferencia.Horas:
                    return "Hora";
                case RangoDiferencia.Dias:
                    return "Dia";

                case RangoDiferencia.Meses:
                    return "Mes";

            }
            return "";
        }
    }
}