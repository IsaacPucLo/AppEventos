using Framework.Excepciones;
using System;

namespace Features.RecuperarInformacion
{
    public class EventoRecuperado
    {
        private const int LONGITUD_MAXIMA_PERMITIDA_EVENTO = 80;
        public EventoRecuperado(string Evento, string FechaEvento)
        {
            ValidarLongitudEvento(Evento);
            ValidarFormatoFecha(FechaEvento);
            Nombre = Evento;
            FechaOcurrencia = Convert.ToDateTime(FechaEvento);
        }

        private void ValidarFormatoFecha(string FechaEvento)
        {
            try
            {
                DateTime.ParseExact(FechaEvento, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception Error)
            {
                throw new BadRequestException("El formato de la fecha es incorrecto");
            }
            
        }

        private void ValidarLongitudEvento(string Evento)
        {
            if (Evento.Length > LONGITUD_MAXIMA_PERMITIDA_EVENTO)
            {
                throw new BadRequestException($"El evento {Evento} excede la longitud máxima permitida que es: {LONGITUD_MAXIMA_PERMITIDA_EVENTO}");
            }
        }

        public string Nombre { get; set; }
        public DateTime FechaOcurrencia { get; set; }
        
    }
}