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

        private RecuperadorRegistros GenerarInstanciaSUT(string Directorio, string NombreArchivo, IControladorArchivo Controlador)
        {
            var SUT = new RecuperadorRegistros(Directorio,NombreArchivo, Controlador);
            return SUT;
        }

        
       
        //La longitud del evento debe de ser de máximo 50 caracteres.
        //La fecha debe de cumplir el formato que se indica: dd/mm/yyyy HH:mm
        //Convertir los registros al modelo
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