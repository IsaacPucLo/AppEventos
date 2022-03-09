using System.Collections.Generic;
using System.IO;

namespace Features.RecuperarInformacion
{
    public class ControladorArchivo : IControladorArchivo
    {
        public List<string> ExtraerCadenas(string RutaCompletaArchivo)
        {
            var Lineas = File.ReadAllLines(RutaCompletaArchivo);
            var Resultado = new List<string>(Lineas);
            return Resultado;
        }

        public bool VerificarExistenciaArchivo(string RutaCompletaArchivo)
        {
            return File.Exists(RutaCompletaArchivo);
        }
    }
}