using Features.RecuperarInformacion;
using System;
using System.Collections.Generic;

namespace Features.ProcesarEventos
{
    public class ProcesadorEventos : IProcesadorEventos
    {
        
        private IProcesadorEventoIndividual ProcesadorIndividual;
        
        private IProcesadorEventoIndividual CreaCadenaDeProcesadoresDeRangos(DateTime FechaActual)
        {
            var ProcesadorMeses = new ProcesadorRangoMeses(FechaActual);
            var ProcesadorDias = new ProcesadorRangoDias(FechaActual);
            var ProcesadorHoras = new ProcesadorRangoHoras(FechaActual);
            var ProcesadorMinutos = new ProcesadorRangoMinutos(FechaActual);

            ProcesadorMeses.AsignarSiguienteRango(ProcesadorDias);
            ProcesadorDias.AsignarSiguienteRango(ProcesadorHoras);
            ProcesadorHoras.AsignarSiguienteRango(ProcesadorMinutos);

            return ProcesadorMeses;
            
        }

        public List<EventoProcesado> ProcesarEventos(List<EventoRecuperado> EventosPorProcesar, DateTime FechaActual)
        {
            ProcesadorIndividual = CreaCadenaDeProcesadoresDeRangos(FechaActual);
            var Resultado = new List<EventoProcesado>();
            foreach (var Evento in EventosPorProcesar)
            {
                var EventoProcesado = ProcesadorIndividual.Procesar(Evento);
                Resultado.Add(EventoProcesado);
            }

            return Resultado;
        }

    }
}