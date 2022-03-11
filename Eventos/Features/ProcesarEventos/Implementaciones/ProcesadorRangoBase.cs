using Features.RecuperarInformacion;
using System;

namespace Features.ProcesarEventos
{
    public abstract class ProcesadorRangoBase : IProcesadorEventoIndividual
    {
        protected readonly DateTime _FechaActual;
        private IProcesadorEventoIndividual _ProcesadorSiguiente = null;

        protected ProcesadorRangoBase(DateTime FechaActual)
        {
            _FechaActual = FechaActual;
        }
        public void AsignarSiguienteRango(IProcesadorEventoIndividual ProcesadorSiguiente)
        {
            _ProcesadorSiguiente = ProcesadorSiguiente;
        }

        public EventoProcesado Procesar(EventoRecuperado EventoPorProcesar)
        {
            if (VerificarCondicionDelRango(EventoPorProcesar))
            {
                return ProcesarEventoDeAcuerdoAlRango(EventoPorProcesar);
            }
            else
            {
                return EnviarEventoAlSiguienteRango(EventoPorProcesar);
            }
        }

        protected EventoProcesado ProcesarEventoDeAcuerdoAlRango(EventoRecuperado EventoPorProcesar)
        {
            TimeSpan DiferenciaEnTiempo = EventoPorProcesar.FechaOcurrencia - _FechaActual;
            int Diferencia = Math.Abs(CalcularDiferencia(DiferenciaEnTiempo));
            var EventoPasado = EventoPorProcesar.FechaOcurrencia < _FechaActual;
            var EventoProcesado = new EventoProcesado(EventoPasado, Diferencia, EstablecerRango(), EventoPorProcesar.Nombre);
            return EventoProcesado;
        }

        protected abstract RangoDiferencia EstablecerRango();
        protected abstract int CalcularDiferencia(TimeSpan DiferenciaEnTiempo);

        private EventoProcesado EnviarEventoAlSiguienteRango(EventoRecuperado EventoPorProcesar)
        {
            if (_ProcesadorSiguiente!=null)
            {
                return _ProcesadorSiguiente.Procesar(EventoPorProcesar);
            }

            return null;
        }

        protected abstract bool VerificarCondicionDelRango(EventoRecuperado EventoPorProcesar);
    }
}