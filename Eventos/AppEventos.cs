using Features.RecuperarInformacion;
using System;

namespace Eventos
{
    public class AppEventos
    {
        private readonly IRecuperadorRegistros _Recuperador;
        private readonly IDesplegadorEventos _Desplegador;

      
        public AppEventos(IRecuperadorRegistros Recuperador, IDesplegadorEventos Desplegador)
        {
            this._Recuperador = Recuperador;
            this._Desplegador = Desplegador;
        }

        public void Ejecutar()
        {
            //Leer el archivo
            //Procesar las entradas
            //Imprimir los resultados
        }
    }

    public interface IDesplegadorEventos
    {
        void DesplegarEvento(Evento EventoPorDesplegar);
    }

    public class DesplegadorEventos : IDesplegadorEventos
    {
        public DesplegadorEventos()
        {
        }

        public void DesplegarEvento(Evento EventoPorDesplegar)
        {
            throw new NotImplementedException();
        }
    }

    public class Evento
    {
    }
}