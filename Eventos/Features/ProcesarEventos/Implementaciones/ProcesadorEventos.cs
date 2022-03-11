using Features.RecuperarInformacion;
using System;
using System.Collections.Generic;

namespace Features.ProcesarEventos
{
    public class ProcesadorEventos : IProcesadorEventos
    {

        public ProcesadorEventos()
        {
        }

        public List<EventoProcesado> ProcesarEventos(List<EventoRecuperado> EventosPorProcesar, DateTime FechaActual)
        {
            var Resultado = new List<EventoProcesado>();
            foreach (var Evento in EventosPorProcesar)
            {
                var EventoProcesado = ProcesarEventoIndividual(Evento, FechaActual);
                Resultado.Add(EventoProcesado);
            }

            return Resultado;
        }

        private EventoProcesado ProcesarEventoIndividual(EventoRecuperado EventoPorProcesar, DateTime FechaActual)
        {

            var EventoPasado = EventoPorProcesar.FechaOcurrencia < FechaActual;
            int CantidadDiferencia = CalcularDiferencia(EventoPorProcesar.FechaOcurrencia, FechaActual, RangoDiferencia.Dias);
            var RangoDiferenciaEvento = RangoDiferencia.Dias;
            if (CantidadDiferencia <= 0)
            {
                CantidadDiferencia = CalcularDiferencia(EventoPorProcesar.FechaOcurrencia, FechaActual, RangoDiferencia.Horas);
                RangoDiferenciaEvento = RangoDiferencia.Horas;
                if (CantidadDiferencia <= 0)
                {
                    CantidadDiferencia = CalcularDiferencia(EventoPorProcesar.FechaOcurrencia, FechaActual, RangoDiferencia.Minutos);
                    RangoDiferenciaEvento = RangoDiferencia.Minutos;
                }
            }


            var EventoProcesado = new EventoProcesado(EventoPasado, CantidadDiferencia, RangoDiferenciaEvento,EventoPorProcesar.Nombre);
            return EventoProcesado;
        }

        private int CalcularDiferencia(DateTime FechaOcurrencia, DateTime FechaActual, RangoDiferencia RangoPorCalcular)
        {
            var Diferencia = FechaOcurrencia - FechaActual;
            switch (RangoPorCalcular)
            {
                case RangoDiferencia.Minutos:
                    return Math.Abs(Diferencia.Minutes);
                case RangoDiferencia.Horas:
                    return Math.Abs(Diferencia.Hours);
                case RangoDiferencia.Dias:
                    return Math.Abs(Diferencia.Days);
                default:
                    break;
            }

            return 0;
        }
    }
}