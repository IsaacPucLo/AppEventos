using System;

namespace Features.DesplegarEventos
{
    public class VisualizadorMensajeConsola: IVisualizarMensaje
    {
        public void VisualizarMensaje(string MensajePorvisualizar)
        {
            Console.WriteLine(MensajePorvisualizar);
        }
    }
}