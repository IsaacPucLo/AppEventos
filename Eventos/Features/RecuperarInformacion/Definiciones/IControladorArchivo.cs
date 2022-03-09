using System.Collections.Generic;

namespace Features.RecuperarInformacion
{
    public interface IControladorArchivo
    {
        bool VerificarExistenciaArchivo(string RutaCompletaArchivo);
        List<string> ExtraerCadenas(string rutaCompletaArchivo);
    }
}