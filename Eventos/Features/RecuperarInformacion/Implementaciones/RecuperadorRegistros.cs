using Framework.Excepciones;
using System;
using System.Collections.Generic;
using System.IO;

namespace Features.RecuperarInformacion
{
    public class RecuperadorRegistros : IRecuperadorRegistros
    {
        private string _RutaCompletaArchivo;
        private const string MENSAJE_ERROR_ARCHIVO_NO_EXISTENTE = "Archivo no existe";
        private readonly IControladorArchivo _ControladorArchivoFisico;

        public RecuperadorRegistros(string Directorio, string NombreArchivo, IControladorArchivo ControladorArchivoFisico)
        {
            _ControladorArchivoFisico = ControladorArchivoFisico ?? throw new ArgumentNullException(nameof(ControladorArchivoFisico));
            _RutaCompletaArchivo = Path.Combine(Directorio, NombreArchivo);
        }

        public List<EventoRecuperado> RecuperarEventos()
        {
            VerificarExistenciaArchivo();
            List<string> CadenasLeidas = _ControladorArchivoFisico.ExtraerCadenas(_RutaCompletaArchivo);
            ValidarFormatoCadenas(CadenasLeidas);
            var EventosExtraidos = ExtraerEventosDeCadenas(CadenasLeidas);
            return EventosExtraidos;
        }

        private List<EventoRecuperado> ExtraerEventosDeCadenas(List<string> CadenasLeidas)
        {
            var Resultado = new List<EventoRecuperado>();
            foreach (var Evento in CadenasLeidas)
            {
                var EventoDividido = Evento.Split(',');
                EventoRecuperado NuevoEventoExtraido = GenerarEvento(EventoDividido[0], EventoDividido[1]);
                Resultado.Add(NuevoEventoExtraido);
            }

            return Resultado;
        }

        private EventoRecuperado GenerarEvento(string Evento, string FechaEvento)
        {
            var EventoGenerado = new EventoRecuperado(Evento,FechaEvento);
            return EventoGenerado;
            
        }

        private void ValidarFormatoCadenas(List<string> CadenasLeidas)
        {
            foreach (var Evento in CadenasLeidas)
            {
                var EventoDividido = Evento.Split(',');
                if (EventoDividido.Length!=2)
                {
                    throw new BadRequestException("Formato Incorrecto");
                }
            }
        }

        private void VerificarExistenciaArchivo()
        {
            if (!_ControladorArchivoFisico.VerificarExistenciaArchivo(_RutaCompletaArchivo))
            {
                throw new NotFoundedException(MENSAJE_ERROR_ARCHIVO_NO_EXISTENTE);
            }
        }
    }
}