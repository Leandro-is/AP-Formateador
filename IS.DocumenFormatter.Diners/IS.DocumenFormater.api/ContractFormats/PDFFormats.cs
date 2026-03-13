using System;

namespace IS.DocumenFormater.api.ContractFormats
{
    public class PdfFormats : IPdfFormats
    {
        // AP - FORMATO UNICO JNE
        public FormatSettings AP_FORMATO_UNICO_JNE { get; set; }
        public FormatSettings AP_ANEXO1 { get; set; }
        public FormatSettings AP_ANEXO2 { get; set; }


        //PLD - DIGITAL
        public FormatSettings GARANTIA { get; set; }
        public FormatSettings PAGARE { get; set; }
        public FormatSettings SOLICITUDPLD { get; set; }
        public FormatSettings DESGRAVAMEN { get; set; }
        public FormatSettings HOJARESUMEN { get; set; }
        public FormatSettings MULTIPRODUCTO { get; set; }
        public FormatSettings CONTRATO_CUENTAS { get; set; }
        public FormatSettings CARTILLA_AHORRO { get; set; }
        public FormatSettings CLAUSULA_ADICIONAL { get; set; }
        public FormatSettings PROTECCION_PAGOS { get; set; }
        public FormatSettings SOLICITUD_DESGRAVAMEN_SALDO { get; set; }
        public FormatSettings SOLICITUD_DESGRAVAMEN_DEVOLUCION { get; set; }

        //PLD - BPM
        public FormatSettings SOLICITUD_CREDITO { get; set; }
        public FormatSettings CONTRATO_CREDITO { get; set; }
        public FormatSettings CARTILLA_CUENTA { get; set; }
        public FormatSettings SEGURO_DESG { get; set; }
        public FormatSettings SEGURO_OPTATIVO { get; set; }
        public FormatSettings HOJA_ACEPTACION { get; set; }
        public FormatSettings HOJA_OPTATIVO { get; set; }
        public FormatSettings HOJA_RESUMEN { get; set; }
        public FormatSettings CARTILLA_AHORRO_EFECTIVO { get; set; }
        public FormatSettings CONSENTIMIENTO { get; set; }
        public FormatSettings DESGRAVAMEN_SALDO { get; set; }
        public FormatSettings DESGRAVAMEN_DEVOLUCION { get; set; }

        //DCM
        public FormatSettings DCM_CLAUSULA_PROTECCION { get; set; }
        public FormatSettings DCM_CONTRATO_MULTIPRODUCTO { get; set; }
        public FormatSettings DCM_HOJA_RESUMEN { get; set; }
        public FormatSettings DCM_INFORMACION { get; set; }
        public FormatSettings DCM_DESGRAVAMEN { get; set; }
        public FormatSettings DCM_SOLICITUD_AFILIACION { get; set; }
        public FormatSettings DCM_TARIFARIO { get; set; }

        //DCMSE
        public FormatSettings DCMSE_CLAUSULA_PROTECCION { get; set; }
        public FormatSettings DCMSE_CONTRATO_MULTIPRODUCTO { get; set; }
        public FormatSettings DCMSE_HOJA_RESUMEN { get; set; }
        public FormatSettings DCMSE_INFORMACION { get; set; }
        public FormatSettings DCMSE_DESGRAVAMEN { get; set; }
        public FormatSettings DCMSE_SOLICITUD_AFILIACION { get; set; }
        public FormatSettings DCMSE_TARIFARIO { get; set; }



        //FREE
        public FormatSettings FREE_CLAUSULA_PROTECCION { get; set; }
        public FormatSettings FREE_CONTRATO_MULTIPRODUCTO { get; set; }
        public FormatSettings FREE_HOJA_RESUMEN { get; set; }
        public FormatSettings FREE_INFORMACION { get; set; }
        public FormatSettings FREE_DESGRAVAMEN { get; set; }
        public FormatSettings FREE_SOLICITUD_AFILIACION { get; set; }
        public FormatSettings FREE_TARIFARIO { get; set; }




        public string SignTextLinea1 { get; set; }
        public string SignTextLinea2 { get; set; }



    }
    public class FormatSettings : IFormatSettings
    {
        public String PathFileBase { get; set; }
        public int SignFromX { get; set; }
        public int SignFromY { get; set; }
        public int SignWidth { get; set; }
        public int SignHeight { get; set; }
        public int SignPage { get; set; }

        public int SignPage2 { get; set; }
        public int SignFromX2 { get; set; }
        public int SignFromY2 { get; set; }

        public int SignPage3 { get; set; }
        public int SignFromX3 { get; set; }
        public int SignFromY3 { get; set; }

        public int SignPage4 { get; set; }
        public int SignFromX4 { get; set; }
        public int SignFromY4 { get; set; }

        public int SignPage5 { get; set; }
        public int SignFromX5 { get; set; }
        public int SignFromY5 { get; set; }

        public int SignPage6 { get; set; }
        public int SignFromX6 { get; set; }
        public int SignFromY6 { get; set; }



        public int BarcodeFromX { get; set; }
        public int BarcodeFromY { get; set; }
        public int BarcodePage { get; set; }
        public int BarcodeWidth { get; set; }
        public int BarcodeHeight { get; set; }

        public int BarcodeFromX2 { get; set; }
        public int BarcodeFromY2 { get; set; }
        public int BarcodePage2 { get; set; }

        public int BarcodeFromX3 { get; set; }
        public int BarcodeFromY3 { get; set; }
        public int BarcodePage3 { get; set; }

        public int BarcodeFromX4 { get; set; }
        public int BarcodeFromY4 { get; set; }
        public int BarcodePage4 { get; set; }

        public int BarcodeFromX5 { get; set; }
        public int BarcodeFromY5 { get; set; }
        public int BarcodePage5 { get; set; }

        public int BarcodeFromX6 { get; set; }
        public int BarcodeFromY6 { get; set; }
        public int BarcodePage6 { get; set; }


        public int StampFromX { get; set; }
        public int StampFromY { get; set; }
        public int StampWidth { get; set; }
        public int StampHeight { get; set; }
        public int StampPage { get; set; }
        public int StampFontSize { get; set; }

        public int DataFontSize { get; set; }
        public int DataPage { get; set; }
        public int LugarFromX { get; set; }
        public int LugarFromY { get; set; }
        public int FechaDiaFromX { get; set; }
        public int FechaDiaFromY { get; set; }
        public int FechaMesFromX { get; set; }
        public int FechaMesFromY { get; set; }
        public int FechaAnioFromX { get; set; }
        public int FechaAnioFromY { get; set; }
        public int DOIFromX { get; set; }
        public int DOIFromY { get; set; }
    }
}
