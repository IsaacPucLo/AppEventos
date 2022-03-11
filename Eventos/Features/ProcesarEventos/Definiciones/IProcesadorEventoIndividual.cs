using Features.RecuperarInformacion;

namespace Features.ProcesarEventos
{
    public interface IProcesadorEventoIndividual {
        EventoProcesado Procesar(EventoRecuperado EventoPorProcesar);
        void AsignarSiguienteRango(IProcesadorEventoIndividual ProcesadorSiguiente);

    }
}