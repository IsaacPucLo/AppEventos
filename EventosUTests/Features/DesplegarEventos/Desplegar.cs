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

    public class VisualizadorMensajeSpy : IVisualizadorMensaje
    {
        private string MensajeDesplegado=string.Empty;

        public void VisualizarMensaje(string MensajePorvisualizar)
        {
            MensajeDesplegado = MensajePorvisualizar;
        }

        public void VerificarMensajeDesplegado(string MensajeEsperado)
        {
            Assert.Equal(MensajeEsperado, MensajeDesplegado);
        }
    }
    public class Desplegar
    {
        
        [Fact(DisplayName = "Cuando evento es del pasado, entonces mensaje incluyé las palabras para el pasado")]
        public void CuandoEventoEsDelPasado_EntoncesIncluyePalabraParaPasado()
        {
            List<EventoProcesado> EventosPorDesplegar = GenerarEvento("Evento del pasado", 2, RangoDiferencia.Minutos, true);
            var EventoPorDesplegar = EventosPorDesplegar[0];
            var Visualizador = new VisualizadorMensajeSpy();
            DesplegadorEventos SUT = GenerarInstanciaSUT(Visualizador);
            SUT.DesplegarEventos(EventosPorDesplegar);
            Visualizador.VerificarMensajeDesplegado($"{EventoPorDesplegar.Nombre} ocurrió hace {EventoPorDesplegar.Diferencia} Minutos");

        }

        private DesplegadorEventos GenerarInstanciaSUT(IVisualizadorMensaje Visualiador)
        {
            return new DesplegadorEventos(Visualiador);
        }

        private List<EventoProcesado> GenerarEvento(string NombreEvento, int Diferencia, RangoDiferencia RangoDiferencia, bool EventoDelPasado)
        {
            var Resultado = new List<EventoProcesado>();
            var EventoProcesadoGenerado = new EventoProcesado(EventoDelPasado, Diferencia, RangoDiferencia, NombreEvento);
            Resultado.Add(EventoProcesadoGenerado);
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