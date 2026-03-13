using System;

namespace IS.DocumenFormater.api.Models.Response
{
    public class GenericResponse
    {
        internal static class ResponseGenericCode
        {
            internal const String Successful = "0";
            internal const String Error = "1111111111";
            internal const String Failed = "2222222222";

            internal const String TokenNotFound = "04040002001";
            internal const String TokenExpired = "04400002002";
            internal const String Unauthorized = "03040002003";
        }

        public virtual String code { get; set; }
        protected String _message;
        public virtual String message
        {
            get
            {
                if (String.IsNullOrEmpty(_message))
                {
                    _message = "error";
                    switch (code)
                    {
                        case ResponseGenericCode.Error: _message = "Operación fallida."; break;
                        case ResponseGenericCode.Failed: _message = "Operación fallida."; break;
                        case ResponseGenericCode.Successful: _message = "Operación exitosa."; break;

                        case ResponseGenericCode.TokenExpired: _message = "Token expirada."; break;
                        case ResponseGenericCode.TokenNotFound: _message = _message = "Token no encontrada."; break;
                        case ResponseGenericCode.Unauthorized: _message = "Sin autorización."; break;
                    }
                }
                return _message;
            }
        }
        public virtual Object data { get; set; }
        public static GenericResponse CreateInstance(String code, String message = "", Object data = null) => new GenericResponse() { code = code, _message = message, data = data };
    }
}
