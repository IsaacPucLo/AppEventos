using Features.RecuperarInformacion;
using System;
using System.Collections.Generic;

namespace Features.ProcesarEventos
{
    public interface IProcesadorEventos
    {
        List<EventoProcesado> ProcesarEventos(List<EventoRecuperado> EventosPorProcesar, DateTime FechaActual);
    }
}