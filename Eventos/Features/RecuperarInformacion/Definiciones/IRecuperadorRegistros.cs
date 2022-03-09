using System.Collections.Generic;

namespace Features.RecuperarInformacion
{
    public interface IRecuperadorRegistros
    {
        List<EventoRecuperado> RecuperarEventos();
    }
}