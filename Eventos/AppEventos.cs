using Features.DesplegarEventos;
using Features.ProcesarEventos;
using Features.RecuperarInformacion;
using System;

namespace Eventos
{
    public class AppEventos
    {
        private readonly IRecuperadorRegistros _Recuperador;
        private readonly IDesplegadorEventos _Desplegador;
        private readonly IProcesadorEventos _Procesador;

        public AppEventos(IRecuperadorRegistros Recuperador, IDesplegadorEventos Desplegador, IProcesadorEventos Procesador)
        {
            _Recuperador = Recuperador;
            _Desplegador = Desplegador;
            _Procesador = Procesador ?? throw new System.ArgumentNullException(nameof(Procesador));
        }

        public void Ejecutar()
        {
            //Leer el archivo
            var EventosLeidos = _Recuperador.RecuperarEventos();
            //Procesar las entradas
            var EventosProcesados = _Procesador.ProcesarEventos(EventosLeidos, DateTime.Now);
            //Imprimir los resultados
            _Desplegador.DesplegarEventos(EventosProcesados);
        }
    }
}