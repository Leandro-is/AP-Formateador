using Microsoft.Extensions.Logging;
using System;

namespace IS.DocumenFormater.api.Logging.Errors
{
    public sealed class ExceptionManager
    {
        private static readonly Lazy<ExceptionManager> lazy = new Lazy<ExceptionManager>(() => new ExceptionManager());
        private static readonly ILogger _logger;
        public static ExceptionManager Instance { get { return lazy.Value; } }

        public void ManageException<T>(Exception ex, ILogger<T> _logger)
        {
            GetExcepcionInformation(ex, _logger);
        }

        private void GetExcepcionInformation(Exception ex, ILogger _logger)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                FormatMessage Message = new FormatMessage();
                Message.Title = "Se ha producido un error en la aplicación";
                Message.Detail = "Se ha producido un error en un componente de la aplicación.";
                Message.Actions = "Revise el log de errores para un mayor detalle del mismo, y para que pueda iniciar acciones para su pronta corrección.";
                this.AddExtraInformation(Message, ex);
                this.RegistrarArchivo(Message, _logger);
            }
        }

        private void AddExtraInformation(FormatMessage message, Exception excepcion)
        {
            int contExcepcionInterna = 0;
            Exception excepcionInterna;
            string tituloExcepcionInterna;

            message.AddExtraData("Exception.Type", excepcion.GetType().Name.ToString());
            message.AddExtraData("Exception.Message", excepcion.Message);
            message.AddExtraData("Exception.Source", excepcion.Source);

            excepcionInterna = excepcion.InnerException;
            while (excepcionInterna != null)
            {
                contExcepcionInterna++;

                tituloExcepcionInterna = string.Format("Exception.InnerException{0}", contExcepcionInterna);

                message.AddExtraData(tituloExcepcionInterna + ".Type", excepcion.InnerException.GetType().Name);
                message.AddExtraData(tituloExcepcionInterna + ".Message", excepcion.InnerException.Message.ToString());
                message.AddExtraData(tituloExcepcionInterna + ".Source", excepcion.InnerException.Source != null ? excepcion.InnerException.Source.ToString() : "");

                excepcionInterna = excepcionInterna.InnerException;
            }

            message.AddExtraData("Exception.StackTrace", excepcion.StackTrace == null ? excepcion.ToString() : excepcion.StackTrace.ToString());
        }

        private void RegistrarArchivo(FormatMessage mensaje, ILogger _logger)
        {
            _logger.LogError(mensaje.ToString());
        }
    }
}
