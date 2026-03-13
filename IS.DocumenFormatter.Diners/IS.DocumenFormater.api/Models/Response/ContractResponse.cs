using System;

namespace IS.DocumenFormater.api.Models.Response
{
    public class ContractResponse : GenericResponse
    {
        internal static class ResponseCode
        {
            internal const string Successful = "0";

            internal const string RequestInvalid = "030400010001";
            internal const string ValidationError = "030400010002";
            internal const string ServerError = "030500010001";
            internal const string DatabaseError = "030500010002";

            internal const string ConvertMinuciaError = "030400010003";
            internal const string GenerateImageError = "030400010004";
            internal const string GenerateBarcodeError = "030400010005";
            internal const string SignDocumentError = "030400010006";
            internal const string StorageError = "030400010007";

            internal const String TokenNotFound = "040400020001";
            internal const String TokenExpired = "044000020002";
            internal const String Unauthorized = "030400020003";
        }

        internal static class ContractApiResponseCode
        {
            internal const string Success = "0";
        }

        public override String message
        {
            get
            {
                _message = base.message == "error" ? "" : base.message;
                if (String.IsNullOrEmpty(_message))
                {
                    _message = "error";
                    switch (code)
                    {
                        case ResponseCode.Successful: _message = "Operación exitosa."; break;

                        case ResponseCode.RequestInvalid: _message = "La petición es inválida."; break;
                        case ResponseCode.ServerError: _message = "Error interno."; break;
                        case ResponseCode.DatabaseError: _message = "Error en la base de datos."; break;
                        case ResponseCode.ValidationError: _message = "Error de validación de datos de entrada."; break;

                        case ResponseCode.ConvertMinuciaError: _message = "Error en la conversión de la minucia dactilar."; break;
                        case ResponseCode.GenerateImageError: _message = "Error en la generación de la imagen de la huella."; break;
                        case ResponseCode.GenerateBarcodeError: _message = "Error en la generación del código de barras."; break;
                        case ResponseCode.SignDocumentError: _message = "Error en la realización de la firma digital."; break;
                        case ResponseCode.StorageError: _message = "Error en el almacenamiento del documento."; break;

                        case ResponseCode.TokenExpired: _message = "Token expirado."; break;
                        case ResponseCode.TokenNotFound: _message = "Token no encontrado."; break;
                        case ResponseCode.Unauthorized: _message = "Sin autorización."; break;
                    }
                }
                return _message;
            }
        }

        public static ContractResponse CreateInstance(String code, Byte status, String message = "", Object data = null) => new ContractResponse() { code = code, _message = message, data = data };
    }

}
