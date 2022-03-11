using Features.RecuperarInformacion;
using System;

namespace Features.ProcesarEventos
{
    public class ProcesadorRangoDias : ProcesadorRangoBase
    {
        public ProcesadorRangoDias(DateTime FechaActual) : base(FechaActual)
        {
        }

        protected override int CalcularDiferencia(TimeSpan DiferenciaEnTiempo)
        {
            return DiferenciaEnTiempo.Days;
        }

        protected override RangoDiferencia EstablecerRango()
        {
            return RangoDiferencia.Dias;
        }

        protected override bool VerificarCondicionDelRango(EventoRecuperado EventoPorProcesar)
        {
            TimeSpan Diferencia = EventoPorProcesar.FechaOcurrencia - _FechaActual;
            return Diferencia.Days != 0;
        }
    }
}