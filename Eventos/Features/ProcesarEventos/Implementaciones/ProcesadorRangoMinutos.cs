using Features.RecuperarInformacion;
using System;

namespace Features.ProcesarEventos
{
    public class ProcesadorRangoMinutos : ProcesadorRangoBase
    {
        public ProcesadorRangoMinutos(DateTime FechaActual) : base(FechaActual)
        {
        }

        protected override int CalcularDiferencia(TimeSpan DiferenciaEnTiempo)
        {
            return DiferenciaEnTiempo.Minutes;
        }

        protected override RangoDiferencia EstablecerRango()
        {
            return RangoDiferencia.Minutos;
        }

        protected override bool VerificarCondicionDelRango(EventoRecuperado EventoPorProcesar)
        {
            TimeSpan Diferencia = EventoPorProcesar.FechaOcurrencia - _FechaActual;
            return Diferencia.Minutes != 0;
        }
    }
}