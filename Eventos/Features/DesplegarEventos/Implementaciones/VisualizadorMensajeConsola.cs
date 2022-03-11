using System;

namespace Features.DesplegarEventos
{
    public class VisualizadorMensajeConsola: IVisualizadorMensaje
    {
        public void VisualizarMensaje(string MensajePorvisualizar)
        {
            Console.WriteLine(MensajePorvisualizar);
        }
    }
}