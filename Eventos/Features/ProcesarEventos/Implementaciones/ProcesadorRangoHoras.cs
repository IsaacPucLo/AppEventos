using Features.RecuperarInformacion;
using System;

namespace Features.ProcesarEventos
{
    public class ProcesadorRangoHoras : ProcesadorRangoBase
    {
        public ProcesadorRangoHoras(DateTime FechaActual) : base(FechaActual)
        {
        }

        protected override int CalcularDiferencia(TimeSpan DiferenciaEnTiempo)
        {
            return DiferenciaEnTiempo.Hours;
        }

        protected override RangoDiferencia EstablecerRango()
        {
            return RangoDiferencia.Horas;
        }

        protected override bool VerificarCondicionDelRango(EventoRecuperado EventoPorProcesar)
        {
            TimeSpan Diferencia = EventoPorProcesar.FechaOcurrencia - _FechaActual;
            return Diferencia.Hours != 0;
        }
    }
}