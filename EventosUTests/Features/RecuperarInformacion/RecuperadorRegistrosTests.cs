using Xunit;
using Features.RecuperarInformacion;
using System;
using System.Collections.Generic;
using Framework.Excepciones;

namespace Tests.RecuperarInformacion
{
    public class RecuperadorRegistrosTests
    {
        [Fact(DisplayName="Dado un directorio vacio, cuando se solicita leer el archivo, entonces lanza excepción")]
        public void DadoDirectorioVacioCuandoSolicitaLeerEntoncesLanzaExcepcion()
        {
            //Arrange
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            var ControladorDoble = new ControladorDobleArchivoNoExistente(false,null);
            var SUT = GenerarInstanciaSUT(Directorio,NombreArchivo, ControladorDoble);

            //Act, Assert
            var ExcepcionLanzada = Assert.Throws<NotFoundedException>(() => SUT.RecuperarEventos());
            Assert.Equal("Archivo no existe", ExcepcionLanzada.Message);

        }

        [Fact(DisplayName = "Dado un archivo existente, cuando no tiene informacion separada por comas, entonces lanza excepción")]
        public void DadoArchivoExistenteCuandoInformacionNoEstaSeparadaPorComasEntoncesLanzaExcepcion()
        {
            //Arrange
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            List<string> Cadenas = new List<string>();
            Cadenas.Add("Evento1|22/09/2021 15:30");
            var ControladorDoble = new ControladorDobleArchivoNoExistente(true,()=>Cadenas);
            var SUT = GenerarInstanciaSUT(Directorio, NombreArchivo, ControladorDoble);

            //Act, Assert
            var ExcepcionLanzada = Assert.Throws<BadRequestException>(() => SUT.RecuperarEventos());
           

        }

        [Fact(DisplayName = "Dado un archivo existente, cuando el evento tiene mas de 50 caracteres, entonces lanza excepción")]
        public void DadoArchivoExistenteCuandoEventoTieneLongitudMayorALaPermitidaEntoncesLanzaExcepcion()
        {
            //Arrange
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            List<string> Cadenas = new List<string>();
            Cadenas.Add("Este es un Evento De Muy larga Longitud De Manera Que Excede el máximo permitido y hace que la prueba falle,22/09/2021 15:30");
            var ControladorDoble = new ControladorDobleArchivoNoExistente(true, () => Cadenas);
            var SUT = GenerarInstanciaSUT(Directorio, NombreArchivo, ControladorDoble);

            //Act, Assert
            var ExcepcionLanzada = Assert.Throws<BadRequestException>(() => SUT.RecuperarEventos());


        }

        [Fact(DisplayName = "Dado un archivo existente, cuando el evento no cumple el formato de las fechas, entonces lanza excepción")]
        public void DadoArchivoExistenteCuandoEventoNoCumpleFormatoDeFechasEntoncesLanzaExcepcion()
        {
            //Arrange
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            List<string> Cadenas = new List<string>();
            Cadenas.Add("Evento de prueba,22-09-2021 15:30 P.M");
            var ControladorDoble = new ControladorDobleArchivoNoExistente(true, () => Cadenas);
            var SUT = GenerarInstanciaSUT(Directorio, NombreArchivo, ControladorDoble);

            //Act, Assert
            var ExcepcionLanzada = Assert.Throws<BadRequestException>(() => SUT.RecuperarEventos());
        }

        [Fact(DisplayName = "Dado un archivo existente, cuando el evento cumple las reglas de negocio, entonces se extrae la información")]
        public void DadoArchivoExistenteCuandoEventoCumpleReglasDeNegocioEntoncesSeExtraeInformacionCorrecta()
        {
            //Arrange
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            List<string> Cadenas = new List<string>();
            var NombreEvento = "Evento de prueba";
            int Año = 2021, Mes = 09, Dia = 22, Hora = 15, Minutos = 30;
            var FechaEvento = new DateTime(Año, Mes, Dia, Hora, Minutos, 0);
            Cadenas.Add($"{NombreEvento},{Dia}/{Mes:00}/{Año} {Hora}:{Minutos}");
            var ControladorDoble = new ControladorDobleArchivoNoExistente(true, () => Cadenas);
            var SUT = GenerarInstanciaSUT(Directorio, NombreArchivo, ControladorDoble);

            //Act
            var Resultado = SUT.RecuperarEventos();

            Assert.Equal(NombreEvento, Resultado[0].Nombre);
            Assert.Equal(FechaEvento, Resultado[0].FechaOcurrencia);
        }

        private RecuperadorRegistros GenerarInstanciaSUT(string Directorio, string NombreArchivo, IControladorArchivo Controlador)
        {
            var SUT = new RecuperadorRegistros(Directorio,NombreArchivo, Controlador);
            return SUT;
        }

    }

    public class ControladorDobleArchivoNoExistente: IControladorArchivo
    {
        private readonly bool _ExisteArchivo;
        private readonly Func<List<string>> recuperadorCadenas;

        public ControladorDobleArchivoNoExistente(bool ExisteArchivo, Func<List<string>> RecuperadorCadenas)
        {
            _ExisteArchivo = ExisteArchivo;
            recuperadorCadenas = RecuperadorCadenas;
        }

        public List<string> ExtraerCadenas(string rutaCompletaArchivo)
        {
            if (recuperadorCadenas!=null)
            {
                return recuperadorCadenas();
            }

            return new List<string>();
        }

        public bool VerificarExistenciaArchivo(string RutaCompletaArchivo)
        {
            return _ExisteArchivo;
        }
    }
}