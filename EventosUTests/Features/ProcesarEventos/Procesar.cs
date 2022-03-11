using Xunit;
using Features.ProcesarEventos;
using System;
using System.Collections.Generic;
using Features.RecuperarInformacion;

namespace Tests.ProcesarEventos
{
    internal class RecuperadorRegistrosParaPruebas : IRecuperadorRegistros
    {
        private readonly List<EventoRecuperado> _EventosPorDevolver;

        public RecuperadorRegistrosParaPruebas(List<EventoRecuperado> EventosPorDevolver)
        {
            _EventosPorDevolver = EventosPorDevolver ?? throw new ArgumentNullException(nameof(EventosPorDevolver));
        }
        public List<EventoRecuperado> RecuperarEventos()
        {
            return _EventosPorDevolver;
        }
    }
    public class Procesar
    {
        private DateTime _FechaActual;

        public Procesar()
        {
            _FechaActual = new DateTime(2022,03,11,12,0,0);
        }
        [Fact(DisplayName = "Cuando la fecha del evento es del pasado, entonces Evento Procesado tiene una marca que lo indica")]
        public void CuandoFechaEsDelPasadoEntoncesMarcarEventoProcesadoComoPasado()
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento Pasado", _FechaActual.AddDays(-10));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar,_FechaActual);
            Assert.True(Resultado[0].EventoPasado);
        }

        private ProcesadorEventos GenerarInstanciaSUT()
        {
            return new ProcesadorEventos();
        }

        [Fact(DisplayName = "Cuando la fecha del evento es del futuro, entonces Evento Procesado tiene una marca que lo indica")]
        public void CuandoFechaEsDelFuturoEntoncesMarcarEventoProcesadoComoPasado()
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento Futuro", _FechaActual.AddDays(10));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar, _FechaActual);
            Assert.False(Resultado[0].EventoPasado);
        }

        [Theory(DisplayName = "Cuando la diferencia del evento se encuentra en el rango de minutos, entonces Evento Procesado calcula la cantidad de minutos")]
        [InlineData(15)]
        [InlineData(-15)]
        public void CuandoDiferenciaDeFechasEsEnMinutos_EntoncesSeCalculaCantidadMinutos(int MinutosDeDiferencia)
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento con diferencia de minutos", _FechaActual.AddMinutes(MinutosDeDiferencia));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar, _FechaActual);
            Assert.Equal(Math.Abs(MinutosDeDiferencia),Resultado[0].Diferencia);
            Assert.Equal(RangoDiferencia.Minutos, Resultado[0].Rango);
        }

        [Theory(DisplayName = "Cuando la diferencia del evento se encuentra en el rango de horas, entonces Evento Procesado calcula la cantidad de horas")]
        [InlineData(60)]
        [InlineData(-60)]
        public void CuandoDiferenciaDeFechasEsEnHoras_EntoncesSeCalculaCantidadHoras(int MinutosDeDiferencia)
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento con diferencia de Horas", _FechaActual.AddMinutes(MinutosDeDiferencia));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar, _FechaActual);
            Assert.Equal(1, Resultado[0].Diferencia);
            Assert.Equal(RangoDiferencia.Horas, Resultado[0].Rango);
        }

        [Theory(DisplayName = "Cuando la diferencia del evento se encuentra en el rango de días, entonces Evento Procesado calcula la cantidad de días")]
        [InlineData(2)]
        [InlineData(-15)]
        public void CuandoDiferenciaDeFechasEsEnDias_EntoncesSeCalculaCantidadDias(int DiasDeDiferencia)
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento con diferencia de Dias", _FechaActual.AddDays(DiasDeDiferencia));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar, _FechaActual);
            Assert.Equal(Math.Abs(DiasDeDiferencia), Resultado[0].Diferencia);
            Assert.Equal(RangoDiferencia.Dias, Resultado[0].Rango);
        }

        [Theory(DisplayName = "Cuando la diferencia del evento se encuentra en el rango de meses, entonces Evento Procesado calcula la cantidad de meses")]
        [InlineData(45,1)]
        [InlineData(-95,3)]
        public void CuandoDiferenciaDeFechasEsEnMeses_EntoncesSeCalculaCantidadMeses(int DiasDeDiferencia,int MesEsperado)
        {
            var EventosPorProcesar = GeneraEventoPorProcesar("Evento con diferencia de Dias", _FechaActual.AddDays(DiasDeDiferencia));
            var Procesador = GenerarInstanciaSUT();
            var Resultado = Procesador.ProcesarEventos(EventosPorProcesar, _FechaActual);
            Assert.Equal(MesEsperado, Resultado[0].Diferencia);
            Assert.Equal(RangoDiferencia.Meses, Resultado[0].Rango);
        }

        private List<EventoRecuperado> GeneraEventoPorProcesar(string Evento, DateTime FechaEvento)
        {
            var Resultado = new List<EventoRecuperado>();
            var EventoPorDevolver = new EventoRecuperado(Evento, $"{FechaEvento.Day:00}/{FechaEvento.Month:00}/{FechaEvento.Year} {FechaEvento.Hour:00}:{FechaEvento.Minute:00}");
            Resultado.Add(EventoPorDevolver);
            return Resultado;

        }


    }
}