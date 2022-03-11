using Features.DesplegarEventos;
using Features.ProcesarEventos;
using Features.RecuperarInformacion;

namespace Eventos
{
    class Program
    {
        static void Main(string[] args)
        {
            var Directorio = @"D:\Temporal";
            var NombreArchivo = "eventos.txt";
            var Controlador = new ControladorArchivo();
            IRecuperadorRegistros Recuperador = new RecuperadorRegistros(Directorio, NombreArchivo, Controlador);
            IDesplegadorEventos Desplegador = new DesplegadorEventos();
            var Procesador = new ProcesadorEventos();
            AppEventos App = new AppEventos(Recuperador, Desplegador, Procesador);
            App.Ejecutar();
        }
    }
}
