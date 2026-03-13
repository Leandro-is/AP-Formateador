namespace IS.DocumenFormater.api.ContractFormats
{
    public interface IPdfFormats
    {
        // AP - FORMATO UNICO JNE
        FormatSettings AP_FORMATO_UNICO_JNE { get; set; }
        FormatSettings AP_ANEXO1 { get; set; }
        FormatSettings AP_ANEXO2 { get; set; }


        //PLD - DIGITAL
        FormatSettings GARANTIA { get; set; }
        FormatSettings PAGARE { get; set; }
        FormatSettings SOLICITUDPLD { get; set; }
        FormatSettings DESGRAVAMEN { get; set; }
        FormatSettings HOJARESUMEN { get; set; }
        FormatSettings MULTIPRODUCTO { get; set; }
        FormatSettings CONTRATO_CUENTAS { get; set; }
        FormatSettings CARTILLA_AHORRO { get; set; }
        FormatSettings CLAUSULA_ADICIONAL { get; set; }
        FormatSettings PROTECCION_PAGOS { get; set; }
        FormatSettings SOLICITUD_DESGRAVAMEN_SALDO { get; set; }
        FormatSettings SOLICITUD_DESGRAVAMEN_DEVOLUCION { get; set; }

        //PLD - BPM
        FormatSettings SOLICITUD_CREDITO { get; set; }
        FormatSettings CONTRATO_CREDITO { get; set; }
        FormatSettings CARTILLA_CUENTA { get; set; }
        FormatSettings SEGURO_DESG { get; set; }
        FormatSettings SEGURO_OPTATIVO { get; set; }
        FormatSettings HOJA_ACEPTACION { get; set; }
        FormatSettings HOJA_OPTATIVO { get; set; }
        FormatSettings HOJA_RESUMEN { get; set; }
        FormatSettings CARTILLA_AHORRO_EFECTIVO { get; set; }
        FormatSettings CONSENTIMIENTO { get; set; }
        FormatSettings DESGRAVAMEN_SALDO { get; set; }
        FormatSettings DESGRAVAMEN_DEVOLUCION { get; set; }

        //DCM
        FormatSettings DCM_CLAUSULA_PROTECCION { get; set; }
        FormatSettings DCM_CONTRATO_MULTIPRODUCTO { get; set; }
        FormatSettings DCM_HOJA_RESUMEN { get; set; }
        FormatSettings DCM_INFORMACION { get; set; }
        FormatSettings DCM_DESGRAVAMEN { get; set; }
        FormatSettings DCM_SOLICITUD_AFILIACION { get; set; }
        FormatSettings DCM_TARIFARIO { get; set; }


        //DCMSE
        FormatSettings DCMSE_CLAUSULA_PROTECCION { get; set; }
        FormatSettings DCMSE_CONTRATO_MULTIPRODUCTO { get; set; }
        FormatSettings DCMSE_HOJA_RESUMEN { get; set; }
        FormatSettings DCMSE_INFORMACION { get; set; }
        FormatSettings DCMSE_DESGRAVAMEN { get; set; }
        FormatSettings DCMSE_SOLICITUD_AFILIACION { get; set; }
        FormatSettings DCMSE_TARIFARIO { get; set; }


        //FREE
        FormatSettings FREE_CLAUSULA_PROTECCION { get; set; }
        FormatSettings FREE_CONTRATO_MULTIPRODUCTO { get; set; }
        FormatSettings FREE_HOJA_RESUMEN { get; set; }
        FormatSettings FREE_INFORMACION { get; set; }
        FormatSettings FREE_DESGRAVAMEN { get; set; }
        FormatSettings FREE_SOLICITUD_AFILIACION { get; set; }
        FormatSettings FREE_TARIFARIO { get; set; }


        string SignTextLinea1 { get; set; }
        string SignTextLinea2 { get; set; }

    }
    public interface IFormatSettings
    {
        string PathFileBase { get; set; }
        int SignFromX { get; set; }
        int SignFromY { get; set; }
        int SignWidth { get; set; }
        int SignHeight { get; set; }
        int SignPage { get; set; }
        int BarcodeFromX { get; set; }
        int BarcodeFromY { get; set; }
        int BarcodeWidth { get; set; }
        int BarcodeHeight { get; set; }
        int BarcodePage { get; set; }
        int StampFromX { get; set; }
        int StampFromY { get; set; }
        int StampWidth { get; set; }
        int StampHeight { get; set; }
        int StampPage { get; set; }
        int StampFontSize { get; set; }

        int DataFontSize { get; set; }
        int DataPage { get; set; }
        int LugarFromX { get; set; }
        int LugarFromY { get; set; }
        int FechaDiaFromX { get; set; }
        int FechaDiaFromY { get; set; }
        int FechaMesFromX { get; set; }
        int FechaMesFromY { get; set; }
        int FechaAnioFromX { get; set; }
        int FechaAnioFromY { get; set; }
        int DOIFromX { get; set; }
        int DOIFromY { get; set; }
    }

}
