using Xunit;
using Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.RecuperarInformacion;

namespace Eventos.Tests
{
    public class AppEventosTests
    {
        [Fact(DisplayName="Dado un archivo con eventos, cuando se ejecuta la aplicación, entonces se imprimen todos los eventos")]
        public void DadoArchivoConEventosCuandoAppSeEjecutaEntoncesSeImprimenTodosLosEventos()
        {
            //Arrange
            int CantidadEventos = 5;
            //Generar archivo con 5 elementos
            //IRecuperadorRegistros Recuperador = new RecuperadorRegistros();
            //IDesplegadorEventos Desplegador = new DesplegadorEventos();
            //var SUT = GenerarInstanciaSUT(Recuperador, Desplegador);
            ////Act
            //SUT.Ejecutar();
            //Assert

        }
        //Probar eventos pasados
        //Probar eventos futuros
        //Probar Rango Minutos
        //Probar Rango Horas
        //Probar Rango Meses
        //Probar que el evento no sea mayor a 50 caracteres

        private AppEventos GenerarInstanciaSUT(IRecuperadorRegistros Recuperador, IDesplegadorEventos Desplegador)
        {
            return new AppEventos(Recuperador, Desplegador);
        }
    }

    
}