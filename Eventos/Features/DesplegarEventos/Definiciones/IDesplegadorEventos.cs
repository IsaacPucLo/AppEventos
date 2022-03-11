using Features.ProcesarEventos;
using System.Collections.Generic;

namespace Features.DesplegarEventos
{
    public interface IDesplegadorEventos
    {
        void DesplegarEventos(List<EventoProcesado> EventosPorDesplegar);
    }
}