using System.Collections.Generic;

namespace Features.ProcesarEventos
{
    public enum RangoDiferencia
    {
        Minutos,
        Horas,
        Dias,
        Meses
    }

    public class EventoProcesado
    {
        public EventoProcesado(bool EventoDelPasado, int CantidadDiferencia, RangoDiferencia RangoDiferencia, string NombreEvento)
        {
            EventoPasado = EventoDelPasado;
            Diferencia = CantidadDiferencia;
            Rango = RangoDiferencia;
            Nombre = NombreEvento;
        }

        public int Diferencia { get; }

        public RangoDiferencia Rango { get; }

        public bool EventoPasado { get;}
        public string Nombre { get; }
    }
}