using Xunit;
using Features.DesplegarEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.ProcesarEventos;

namespace Tests.DesplegarEventos
{
    public class Desplegar
    {
        [Fact(DisplayName = "Cuando evento es del pasado, entonces mensaje incluyé las palabras para el pasado")]
        public void CuandoEventoEsDelPasado_EntoncesIncluyePalabraParaPasado()
        {
            List<EventoProcesado> EventosPorDesplegar = GenerarEvento("Evento del pasado", 2, RangoDiferencia.Minutos, true);
            DesplegadorEventos SUT = GenerarInstanciaSUT();
            SUT.DesplegarEventos(EventosPorDesplegar);

        }

        private DesplegadorEventos GenerarInstanciaSUT()
        {
            return new DesplegadorEventos();
        }

        private List<EventoProcesado> GenerarEvento(string NombreEvento, int Diferencia, RangoDiferencia RangoDiferencia, bool EventoDelPasado)
        {
            var Resultado = new List<EventoProcesado>();
            var EventoProcesadoGenerado = new EventoProcesado(EventoDelPasado, Diferencia, RangoDiferencia, NombreEvento);
            return Resultado;
        }

        //Mensaje correcto del pasado
        //Mensaje correcto Futuro
        //Mensaje correcto para Minutos
        //Mensaje para horas
        //Mensaje para días
        //Mensaje para meses
    }
}