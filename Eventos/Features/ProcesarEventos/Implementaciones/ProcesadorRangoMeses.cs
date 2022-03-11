using Features.RecuperarInformacion;
using System;

namespace Features.ProcesarEventos
{

    public class ProcesadorRangoMeses : ProcesadorRangoBase
    {
        private const int CANTIDAD_DIAS_PARA_CONSIDERAR_RANGO_MES = 30;

        public ProcesadorRangoMeses(DateTime FechaActual) : base(FechaActual)
        {
        }

        protected override int CalcularDiferencia(TimeSpan DiferenciaEnTiempo)
        {
            var DiferenciaMeses = DiferenciaEnTiempo.Days / CANTIDAD_DIAS_PARA_CONSIDERAR_RANGO_MES;
            return DiferenciaMeses;
        }

        protected override RangoDiferencia EstablecerRango()
        {
            return RangoDiferencia.Meses;
        }

   
        protected override bool VerificarCondicionDelRango(EventoRecuperado EventoPorProcesar)
        {
            TimeSpan Diferencia = EventoPorProcesar.FechaOcurrencia - _FechaActual;
            return Math.Abs(Diferencia.Days) > CANTIDAD_DIAS_PARA_CONSIDERAR_RANGO_MES;
        }

        
    }
}