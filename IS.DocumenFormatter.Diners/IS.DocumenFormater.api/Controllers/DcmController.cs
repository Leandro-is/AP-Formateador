using IS.DocumenFormater.api.ContractFormats;
using IS.DocumenFormater.api.Factories;
using IS.DocumenFormater.api.Logging.Errors;
using IS.DocumenFormater.api.Models.Request;
using IS.DocumenFormater.api.Models.Response;
using IS.DocumenFormater.Repository;
using IS.DocumenFormater.Repository.Domain;
using IS.DocumenFormater.Repository.Exchange;
using IS.DocumenFormater.Services.Exchange;
using IS.DocumenFormater.utilities.BarCode;
using IS.DocumenFormater.utilities.Biometric;
using IS.DocumenFormater.utilities.iPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static IS.DocumenFormater.api.Models.Request.BpmRequest;
using static IS.DocumenFormater.api.Models.Request.ContractRequest;

namespace IS.DocumenFormater.api.Controllers
{
    [Area("API")]
    [Route("api/diners")]
    [ApiController]
    //[ApiAuthorization]

    public class DCMController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPdfFormats _pdfFormats;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<DCMController> _logger;

        public DCMController(
            IConfiguration configuration,
            IPdfFormats pdfFormats,
            IHostingEnvironment hostingEnvironment,
            ILogger<DCMController> logger)
        {
            _configuration = configuration;
            _pdfFormats = pdfFormats;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        
        //rutas de los archivos

        // GET api/values

        //1
        [Route("[controller]/solicitudtest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestSolicitudCredito()
        {
            BpmRequest request = new BpmRequest()
            {
                CanalVenta = "Agencia",
                NombreAgencia = "NombreAgencia",
                CodVendedor = "C001",
                NroSolicitud = "NroSolicitud",
                FechaTransaccion = "28/01/2021",
                TipoPrestamoPersonal = "Nuevo", //Nuevo, Reenganche
                FechaPagoPrestamoPersonal = "20/12/2012",
                //TipoCredito = "Nuevo",
                TipoMoneda = "Soles",
                MontoCredito = "2300",
                PlazoCredito = "036",
                TipoCuota = "Simple",
                TipoGarantia = "Liquida",
                TasaCredito = "20%",
                PeriodoGracia = "true",
                PeriodoGraciaDet = "30 días",
                //TipoDesembolso = "Desembolso",
                UsoPrestamoPersonal = "Ocio",
                UsoPrestamoPersonalOtros = "Otrooo",

                //agregado recien
                //NroCuentaD = "123456789",
                //NroCtaTransfer = "12345678910",
                //Entidad = "banco de credito",

                
                //Datos personales cliente
                TipoDocumentoCliente = "DNI",
                NroDocumentoCliente = "61092461",
                NombresCliente = "GRETEL ESTEFANIA",
                ApPaternoCliente = "VIDAL",
                ApMaternoCliente = "CHIRA",
                
                FechaNacimientoCliente = "28/01/2021",
                SexoCliente = "Femenino",
                Nacionalidad = "Peruana",
                EstadoCivilCliente = "Soltero",
                FuncionesCliente = "true",
                NroDependientes = "02",


                EmailCliente = "ESTEFANIA@GMAIL.COM",
                CelularCliente = "123456789",
                CodigoCiudad = "LIMA 31",
                TelefonoCliente = "1234567",
                TipoViviendaCliente = "Propia",

                GradoInstruccion = "Primaria",
                ContinuidadLaboral = "true",
                SituacionLaboralCliente = "Dependiente",
                RucCliente = "12345678910",

                DireccionDetalleCliente = "Calle",
                DireccionCliente = "FRANCISCO BOLOGNESI",
                DireccionDetalleExteriorCliente = "Numero",
                DireccionExteriorCliente = "PISO 3",
                DireccionDetalleInteriorCliente = "Lote",
                DireccionInteriorCliente = "252",
                DireccionDetalleZonaCliente = "Seccion",
                DireccionZonaCliente = "MESA REDONDA",
                UbiegoCliente = "LIMA, LIMA, SMP",
                ReferenciaCliente = "ALT. PARADERO PILAS",

                //Empresa del titular
                CentroActualTitular = "INSOLUTIONS",
                CargoActualTitular = "ASISTENTE",
                GiroTitular = "TEXTIL",
                FechaIngresoTitular = "12/12/2020",
                DireccionDetalleEmpresaT = "Calle",
                DireccionEmpresaT = "FRANCISCO BOLOGNESI",
                DireccionDetalleInteriorEmpresaT = "Lote",
                DireccionInteriorEmpresaT = "252",
                DireccionDetalleExteriorEmpresaT = "Numero",
                DireccionExteriorEmpresaT = "PISO 3",
                DireccionDetalleZonaEmpresaT = "Seccion",
                DireccionZonaEmpresaT = "MESA REDONDA",
                UbiegoEmpresaTitular = "LIMA, LIMA, SMP",
                ReferenciaEmpresaTitular = "ALT. PARADERO PILAS",

                CodigoCiudadEmpresaTitular = "LIMA 31",
                TelefonoEmpresaTitular = "1234567",
                TipoContratoTitular = "Nombrado",
                FechaFinContratoTitular = "12/12/2020",
                TipoMonedaIngresoTitular = "Soles",
                MontoIngresoTitular = "444.00",
                MonedaOtroIngresoTitular = "Soles",
                MontoOtroIngresoTitular = "444.00",

                //Informacion personal Cónyuge
                TipoDocumentoConyuge = "DNI",
                NroDocumentoConyuge = "12345678",
                ApPaternoConyuge = "FLORES",
                ApMaternoConyuge = "MEDINA",
                NombresConyuge = "FIORELLA",
                FechaNacimientoConyuge = "06/01/1998",
                SexoConyuge = "Femenino",
                NacionalidadConyuge = "PERUANA",
                EstadoCivilConyuge = "Soltero",
                NroDependientesConyuge = "15",
                FuncionesConyuge = "true",
                EmailConyuge = "FIOFLORESM@GMAIL.COM",
                CelularConyuge = "123456789",
                CodigoConyuge = "LIMA 31",
                TelefonoConyuge = "1234567",
                ViviendaConyuge = "Propia",
                GradoConyuge = "Primaria",
                ContinuidadConyuge = "true",
                SituacionLaboralConyuge = "Dependiente",
                RUCConyuge = "1234567899",


                //Datos empresa cónyuge
                //CentroActualConyuge = "LOS OLIVOS",
                //CargoActualConyuge = "GERENTE",
                //GiroConyuge = "TECNOLOGIA",
                //FechaIngresoConyuge = "22/12/2020",
                //FechaFinContratoConyuge = "22/12/2020",
                DireccionDetalleEmpresaC = "Calle",
                DireccionEmpresaC = "FRANCISCO BOLOGNESI",
                DireccionDetalleInteriorEmpresaC = "Lote",
                DireccionInteriorEmpresaC = "252",
                DireccionDetalleExteriorEmpresaC = "Numero",
                DireccionExteriorEmpresaC = "PISO 3",
                DireccionDetalleZonaEmpresaC = "Seccion",
                DireccionZonaEmpresaC = "MESA REDONDA",
                UbigeoEmpresaConyuge = "LIMA, LIMA, SMP",

                //ReferenciaEmpresaConyuge = "ALT. PARADERO PILAS",
                //CodigoCiudadEmpresaConyuge = "LIMA 31",
                //TelefonoEmpresaConyuge = "1234567",
                //TipoContratoConyuge = "Ninguno",
                //TipoMonedaIngresoConyuge = "Soles",
                //MontoIngresoConyuge = "2500",
                //MonedaOtroIngresoConyuge = "Soles",
                //MontoOtroIngresoConyuge = "12222",


                //Informacion patrimonial
                //TipoPatrimonio = "Inmuebles",
                //DireccionPatrimonio1 = "AVENIDA CALLE JIRON 2000",
                //TipoMonedaPatrimonio1 = "Soles",
                //TotalPatrimonio1 = "12345",
                //Hipoteca1 = "true",
                //DireccionPatrimonio2 = "AVENIDA CALLE JIRON 2000",
                //TipoMonedaPatrimonio2 = "Soles",
                //TotalPatrimonio2 = "123456789",
                //Hipoteca2 = "false",

                ////Prestamo personal
                //SeleccioneSubProducto = "Estudios",
                
                

                ////Prestamo estudios
                //TipoPrestamoEstudios = "Extranjera",
                //TipoEstudioPrestamo = "Otros",
                //TipoEstudioPrestamoOtros = "Otrooos",
                //InstitutoPrestamo = "Instituto",
                //CarreraPrestamo = "Carrera",
                //ProgramaPrestamo = "Programa",

                ////Credito por convenio
                //CuotasAnio = "Diez",
                //ModalidadCliente = "Nuevo",
                //ModalidadCredito = "Compra",
                //AfiliacionSeguro = "true",
                //LineaConvenio = "LINEA CONVENIO",
                //UsoCredito = "USO CREDITO",
                ////FechaVencimiento = "12/12/2020",
                //FechaCredito = "13/12/2020",

                //TransferenciaCCI = "40000000",

                //Institucion
                NombreInstitucion1 = "NombreInstitucion1",
                TipoTarjeta1 = "Tarjeta",
                NumeroTarjeta1 = "1234567891011222",
                TipoValor1 = "Directo",
                TipoMoneda1 = "Soles",
                MontoCancelar1 = "10000",

                NombreInstitucion2 = "NombreInstitucion1",
                TipoTarjeta2 = "Tarjeta",
                NumeroTarjeta2 = "1234567891011222",
                TipoValor2 = "CCE",
                TipoMoneda2 = "Soles",
                MontoCancelar2 = "10000",

                NombreInstitucion3 = "NombreInstitucion1",
                TipoTarjeta3 = "Tarjeta",
                NumeroTarjeta3 = "1234567891011222",
                TipoValor3 = "PortaValor",
                TipoMoneda3 = "Soles",
                MontoCancelar3 = "10000",

                NombreInstitucion4 = "NombreInstitucion1",
                TipoTarjeta4 = "Tarjeta",
                NumeroTarjeta4 = "1234567891011222",
                TipoValor4 = "PortaValor",
                TipoMoneda4 = "Soles",
                MontoCancelar4 = "10000",

                NombreInstitucion5 = "NombreInstitucion1",
                TipoTarjeta5 = "Tarjeta",
                NumeroTarjeta5 = "1235567891011222",
                TipoValor5 = "PortaValor",
                TipoMoneda5 = "Soles",
                MontoCancelar5 = "10000",

                NombreInstitucion6 = "NombreInstitucion1",
                TipoTarjeta6 = "Tarjeta",
                NumeroTarjeta6 = "1236567891011222",
                TipoValor6 = "PortaValor",
                TipoMoneda6 = "Soles",
                MontoCancelar6 = "10000",

                NombreInstitucion7 = "NombreInstitucion1",
                TipoTarjeta7 = "Tarjeta",
                NumeroTarjeta7 = "1237567891011222",
                TipoValor7 = "PortaValor",
                TipoMoneda7 = "Soles",
                MontoCancelar7 = "10000",

                NombreInstitucion8 = "NombreInstitucion1",
                TipoTarjeta8 = "Tarjeta",
                NumeroTarjeta8 = "1238567891011222",
                TipoValor8 = "PortaValor",
                TipoMoneda8 = "Soles",
                MontoCancelar8 = "10000",

                NombreInstitucion9 = "NombreInstitucion1",
                TipoTarjeta9 = "Tarjeta",
                NumeroTarjeta9 = "1239567891011222",
                TipoValor9 = "PortaValor",
                TipoMoneda9 = "Soles",
                MontoCancelar9 = "10000",

                MonedaMontoTotal = "Dolares",
                MontoTotal = "10000",

                //Referencias
                //NombresReferencia1 = "NombresRefrencia1",
                //ParentescoReferencia1 = "ParentescoReferencia1",
                //TelefonoReferencia1 = "TelefonoReferencia1",

                //NombresReferencia2 = "NombresRefrencia2",
                //ParentescoReferencia2 = "ParentescoReferencia2",
                //TelefonoReferencia2 = "TelefonoReferencia2",

                // Estado

                //Afilicación al envío electrónico. 
                EnvioEstadoCuenta = "true",
                FormaEstadoCuenta = "Fisica",
                CorrespondenciaEstadoCuenta = "Domicilio",

                //Tratamiento de datos personales 
                PrimerConsentimiento = "true",
                SegundoConsentimiento = "true",

                LugarTransaccion = "Lima",

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "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",

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetFormatoUnicoJNEPDF(_pdfFormats.AP_FORMATO_UNICO_JNE, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Solicitudproteccion.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //2
        //[Route("[controller]/contratocreditotest")]
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult TestContratoCredito()
        //{
        //    BpmRequest request = new BpmRequest()
        //    {
        //        NroDocumentoCliente = "48761737",
        //        LugarTransaccion = "Lima",
        //        NombresCliente = "Juan",
        //        ApPaternoCliente = "Chavez",
        //        ApMaternoCliente = "Diaz",
        //        FechaTransaccion = "30/03/2021",
        //        DireccionCliente = "Jr. Las Ortigas N° 205 Int 121. Urb. Las Flores de Lima. Distrito Lince",
        //        TipoDocumentoCliente = "DNI",

        //        NroDocumentoConyuge = "44257897",
        //        NombresConyuge = "Liliana",
        //        ApPaternoConyuge = "Perez",
        //        ApMaternoConyuge = "Surco",
        //        TipoDocumentoConyuge = "DNI",

        //        NombreRepresentante = "Marco Merino Flores",

        //        ImpresionBiometricaCliente = "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",
        //        ImpresionBiometricaConyuge = "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"

        //    };
        //    //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

        //    String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
        //    String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
        //    String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

        //    String file = GetDCMContratoMultiproductoPDF(_pdfFormats.DCM_CONTRATO_MULTIPRODUCTO, request/*, FingerprintImage, BarCode*/);

        //    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
        //    {
        //        FileName = "ContratoMultiproducto.pdf",
        //        Inline = true
        //    };
        //    Response.Headers.Add("Content-Disposition", cd.ToString());
        //    Response.Headers.Add("X-Content-Type-Options", "nosniff");
        //    return File(Convert.FromBase64String(file), "application/pdf");
        //}

        ////3
        //[Route("[controller]/cartillatest")]
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult TestCartillaCuenta()
        //{
        //    BpmRequest request = new BpmRequest()
        //    {

        //        LugarTransaccion = "Lima",
        //        FechaTransaccion = "08/12/2020",

        //        NombresCliente = "Jesús",
        //        ApPaternoCliente = "Diaz",
        //        ApMaternoCliente = "Sanchez",

        //        NroDocumentoCliente = "48761737",

        //        NombresCliente2 = "Jesús",
        //        ApPaternoCliente2 = "Diaz",
        //        ApMaternoCliente2 = "Sanchez",

        //        NroDocumentoCliente2 = "48761737",

        //        NombresCliente3 = "Jesús",
        //        ApPaternoCliente3 = "Diaz",
        //        ApMaternoCliente3 = "Sanchez",

        //        NroDocumentoCliente3 = "48761737",

        //        NombreRepresentante = "Juan Gutierrez Muñoz",

        //        ImpresionBiometricaCliente = "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",
        //        ImpresionBiometricaCliente2 = "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",
        //        ImpresionBiometricaCliente3 = "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",
        //        ImpresionBiometricaRepresentante = "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"


        //    };
        //    //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

        //    String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
        //    String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
        //    String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

        //    //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
        //    //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

        //    String file = GetDCMResumenPDF(_pdfFormats.DCM_HOJA_RESUMEN, request/*, FingerprintImage, BarCode*/);

        //    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
        //    {
        //        FileName = "Cartilla.pdf",
        //        Inline = true
        //    };
        //    Response.Headers.Add("Content-Disposition", cd.ToString());
        //    Response.Headers.Add("X-Content-Type-Options", "nosniff");
        //    return File(Convert.FromBase64String(file), "application/pdf");
        //}

        //4 problemas
        [Route("[controller]/segurodestest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestSeguroDesgravamen()
        {
            BpmRequest request = new BpmRequest()
            {
                TipoMoneda = "Soles",
                FechaTransaccion = "30/03/2021",

                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                NombresCliente = "Juan",
                TipoDocumentoCliente = "DNI",
                NroDocumentoCliente = "48757978",
                MontoCredito = "10,000",

                NombresBeneficiario1 = "Luz Marina",
                ApPaternoBeneficiario1 = "Perez",
                ApMaternoBeneficiario1 = "Roldán",
                NroDocumentoBeneficiario1 = "12567689",
                PorcentajeBeneficiario1 = "50%",
                RelacionBeneficiario1 = "Prima",
                FechaNacimientoBeneficiario1 = "06/04/1992",

                NombresBeneficiario2 = "Luz Marina",
                ApPaternoBeneficiario2 = "Perez",
                ApMaternoBeneficiario2 = "Roldán",
                NroDocumentoBeneficiario2 = "12567689",
                PorcentajeBeneficiario2 = "50%",
                RelacionBeneficiario2 = "Prima",
                FechaNacimientoBeneficiario2 = "06/04/1992",

                NombresBeneficiario3 = "Luz Marina",
                ApPaternoBeneficiario3 = "Perez",
                ApMaternoBeneficiario3 = "Roldán",
                NroDocumentoBeneficiario3 = "12567689",
                PorcentajeBeneficiario3 = "50%",
                RelacionBeneficiario3 = "Prima",
                FechaNacimientoBeneficiario3 = "06/04/1992",

                NombresVendedor = "Lizbet",
                ApPaternoVendedor = "Aguirre",
                ApMaternoVendedor = "Flores",
                EmailVendedor = "lizbet.aguirre@pichincha.pe",

                ApPaternoFirmanteAdicional = "Flores",
                ApMaternoFirmanteAdicional = "Medina",
                NombresFirmanteAdicional = "Fiorella",
                TipoDocumentoFirmanteAdicional = "DNI",
                NroDocumentoFirmanteAdicional = "74544048",

                Entidad = "Entidad",
                NombreAgencia = "NombreAgencia",
                NombresFuncionario = "NombresFuncionario",
                EmailFuncionario = "EmailFuncionario"

                //ImpresionBiometricaCliente = "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",
                //ImpresionBiometricaConyuge = "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"

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;


            String FingerprintImage = "";
            String BarCode = "";

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetDCMInfoPDF(_pdfFormats.DCM_INFORMACION, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "SeguroDesg.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //5
        [Route("[controller]/segurotest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestSeguroOptativo()
        {
            BpmRequest request = new BpmRequest()
            {
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                PrimerNombreCliente = "Juan",
                SegundoNombreCliente = "David",
                TipoDocumentoCliente = "DNI",
                NroDocumentoCliente = "48757978",
                SexoCliente = "Femenino",
                OcupacionCliente = "Analista",
                FechaNacimientoCliente = "12/05/1992",
                DireccionCliente = "Jr. Las Ortigas N° 205 Int 121. Urb. Las Flores de Lima.",
                DistritoCliente = "Lince",
                ProvinciaCliente = "Lima",
                DepartamentoCliente = "Lima",
                TelefonoCliente = "4587943/987475897",
                EmailCliente = "juan.diaz@gmail.com",

                LugarTransaccion = "Lima",
                FechaTransaccion = "31/03/2021",

                NombresVendedor = "Lizbet",
                ApPaternoVendedor = "Aguirre",
                ApMaternoVendedor = "Flores",
                EmailVendedor = "lizbet.aguirre@pichincha.pe",

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "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"



            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetDCMDesgravamenPDF(_pdfFormats.DCM_DESGRAVAMEN, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "SeguroOpt.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //6
        [Route("[controller]/hojatest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestHojaAceptacion()
        {
            BpmRequest request = new BpmRequest()
            {

                NombresCliente = "Jesus",
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                NroDocumentoCliente = "47895787",
                FechaTransaccion = "28/01/2021 14:35:39",
                TelefonoCliente = "987456789",
                ImpresionBiometricaCliente = "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"

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetDCMSolicitudAfiliacionPDF(_pdfFormats.DCM_SOLICITUD_AFILIACION, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "HojaAcept.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //7
        [Route("[controller]/hojaopttest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestHojaOptativo()
        {
            BpmRequest request = new BpmRequest()
            {
                NombresCliente = "Jesus",
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                NroDocumentoCliente = "47895787",
                FechaTransaccion = "28/01/2021 14:35:39",
                TelefonoCliente = "987456789",
                ImpresionBiometricaCliente = "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"
            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetDCMTarifarioPDF(_pdfFormats.DCM_TARIFARIO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "HojaOpt.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }



        //POST

        //1
        [Route("formatounicojneGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> FormatoUnicoJNEGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string solicitudGenerated = GetFormatoUnicoJNEPDF(_pdfFormats.AP_FORMATO_UNICO_JNE, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            solicitudGenerated = AddPageFacial(solicitudGenerated, 
                                FingerprintImage, 
                                request.AddHojaNombres, 
                                request.AddHojaApellidoPaterno,
                                request.AddHojaApellidoMaterno, 
                                request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                solicitudGenerated = ExistingPageFacial(
                                    solicitudGenerated, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    55, 380);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                solicitudGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //2
        [Route("apanexo1jneGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> APANexo1JNEGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string contratoGenerated = GetAPAnexo1JNEPDF(_pdfFormats.AP_ANEXO1, request/*, FingerprintImage, ""*/);

                        //if (FingerprintImage != null)
                        //{
                        //    contratoGenerated = AddPageFacial(contratoGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                        //    if (!String.IsNullOrEmpty(FingerprintImage2))
                        //    {
                        //        contratoGenerated = ExistingPageFacial(contratoGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
                        //    }
                        //}

                        // No añadir pagina para la huella en la misma hoja

                        int cuadroX = 420;
                        int cuadroY = 606;

                        int x = cuadroX - 25;
                        int y = cuadroY - 50;

                        if (FingerprintImage != null)
                        {
                            contratoGenerated = ExistingPageFacial(
                                contratoGenerated, 
                                FingerprintImage, 
                                request.AddHojaNombres, 
                                request.AddHojaApellidoPaterno,
                                request.AddHojaApellidoMaterno, 
                                request.AddHojaDocumentoIdentidad, 
                                x, y);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                contratoGenerated = ExistingPageFacial(
                                    contratoGenerated, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    470, 556);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                contratoGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //3
        [Route("apanexo2jneGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> APAnexo2JNEGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {

                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string garantiaGenerated = GetAPAnexo2JNEPDF(_pdfFormats.AP_ANEXO2, request/*, FingerprintImage, ""*/);


                        // No añadir pagina para la huella en la misma hoja

                        //if (FingerprintImage != null)

                        //{
                        //    garantiaGenerated = AddPageFacial(garantiaGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                        //    if (!String.IsNullOrEmpty(FingerprintImage2))
                        //    {
                        //        garantiaGenerated = ExistingPageFacial(garantiaGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
                        //    }
                        //}

                        int cuadroX = 353;
                        int cuadroY = 361;

                        int x = cuadroX - 25;
                        int y = cuadroY - 50;

                        if (FingerprintImage != null)
                        {
                            garantiaGenerated = ExistingPageFacial(
                                garantiaGenerated,
                                FingerprintImage,
                                request.AddHojaNombres,
                                request.AddHojaApellidoPaterno,
                                request.AddHojaApellidoMaterno,
                                request.AddHojaDocumentoIdentidad,
                                x, y);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                garantiaGenerated = ExistingPageFacial(
                                    garantiaGenerated,
                                    FingerprintImage2,
                                    request.AddHojaNombres2,
                                    request.AddHojaApellidoPaterno2,
                                    request.AddHojaApellidoMaterno2,
                                    request.AddHojaDocumentoIdentidad2,
                                    470, 556);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                garantiaGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //4
        [Route("dcminfoGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DCMInfoGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string pagareGenerated = GetDCMInfoPDF(_pdfFormats.DCM_INFORMACION, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            pagareGenerated = AddPageFacial(pagareGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                pagareGenerated = ExistingPageFacial(
                                    pagareGenerated, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    55, 380, false);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                pagareGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //5
        [Route("dcmdesgravamenGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DCMDesgravamenGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string desgravamenGenerated = GetDCMDesgravamenPDF(_pdfFormats.DCM_DESGRAVAMEN, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageFacial(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageFacial(
                                    desgravamenGenerated, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    55, 380, false);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                desgravamenGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //6
        [Route("dcmsolicitudafiliacionGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DCMSolicitudAfiliacionGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string hojaAceptacion = GetDCMSolicitudAfiliacionPDF(_pdfFormats.DCM_SOLICITUD_AFILIACION, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            hojaAceptacion = AddPageFacial(hojaAceptacion, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                hojaAceptacion = ExistingPageFacial(
                                    hojaAceptacion, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    55, 380, false);
                            }
                        }

                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                hojaAceptacion,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //7
        [Route("dcmtarifarioGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DCMTarifarioGenerate([FromBody] BpmRequest request)
        {
            ContractResponse response = new ContractResponse();
            using (_logger.BeginScope("SIGN Request Opening"))
            {
                _logger.LogInformation("Initializing formating.... ");
                try
                {
                    String bioFingerprint = request.ImpresionBiometricaCliente;
                    String FingerprintImage = bioFingerprint;

                    String bioFingerprint2 = request.ImpresionBiometricaConyuge;
                    String FingerprintImage2 = bioFingerprint2;

                    //String bioFingerprint = request.ImpresionBiometricaCliente;//GetFingerprint(requestContract.NroDocumentoCliente);//request.ImpesionBiometrica;
                    //String FingerprintImage = "", BarCode = ""; //
                    //if (!String.IsNullOrEmpty(bioFingerprint))
                    //{
                    //    FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //    BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);
                    //}

                    //String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
                    //String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

                    await Task.Run(async () =>
                    {
                        _logger.LogInformation("Initializing create Entity Transactional .... ");
                        //TransaccionalDocumentFormater EntityTransactional = await SaveEntityTransaccionalDocumentFormater(request);
                        //_logger.LogCritical("Entity Transactional created with id {0}.", EntityTransactional.Id);

                        _logger.LogInformation("Initializing create format...");
                        /*
                         if(FingerprintImage != null && BarCode != null)
                         {
                             string garantiaGenerated = GetGarantiaPDF(_pdfFormats.GARANTIA, request, FingerprintImage, BarCode);
                         } else
                         {
                             string garantiaGenerated2 = GetGarantiaPDF(_pdfFormats.GARANTIA, request);
                         }*/

                        string hojaOptativo = GetDCMTarifarioPDF(_pdfFormats.DCM_TARIFARIO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            hojaOptativo = AddPageFacial(hojaOptativo, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                hojaOptativo = ExistingPageFacial(
                                    hojaOptativo, 
                                    FingerprintImage2, 
                                    request.AddHojaNombres2, 
                                    request.AddHojaApellidoPaterno2, 
                                    request.AddHojaApellidoMaterno2, 
                                    request.AddHojaDocumentoIdentidad2, 
                                    55, 380, false);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                hojaOptativo,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }


        //Métodos de generacion de documentos

        //1
        private String GetFormatoUnicoJNEPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            ////Canal venta

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ProcesoElectoral}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 222, 732, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 214, 658, 0.0f);

            if (request.Sexo == "M")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 219, 640, 0.0f);
            }
            else if (request.Sexo == "F")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 272, 640, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 272, 200, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCandidato}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 840-214, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCandidato}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 214, 840-229, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreCandidato}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 214, 840-250, 0.0f);



            if (request.Cargo == (int)CargoOpciones.presidentedelarepublica)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 840-438, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.primervicepresidente)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 840-453, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.segundovicepresidente)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 840 - 470, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.diputados)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 176, 840 - 438, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.representanteparlamentoandino)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 176, 840 - 453, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.gobernadorregional)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 176, 840 - 470, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.senadores)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 255, 840 - 438, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.vicegobernadorregional)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 340, 840 - 438, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.consejeroregional)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 340, 840 - 453, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.alcaldeprovincial)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 340, 840 - 470, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.regidorprovincial)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 482, 840 - 438, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.alcaldedistrital)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 482, 840 - 453, 0.0f);
            }
            else if (request.Cargo == (int)CargoOpciones.regidordistrital)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 482, 840 - 470, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 272, 200, 0.0f);
            }


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 272, 200, 0.0f);


            return pdfbase64;
        }

        //2
        private String GetAPAnexo1JNEPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombreCandidato}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, /*X*/ 123, /*Y*/ 567, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 175, 556, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DireccionDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 101, 545, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DistritoDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 101, 533, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ProvinciaDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 270, 533, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DepartamentoDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 102, 522, 0.0f);

            // Normaliza el valor (null -> -1 para "sin selección")
            int? cmValueNullable = request.ConsejoMunicipal;   
            int cmValue = cmValueNullable.GetValueOrDefault(-1);

            if (cmValue == 0)
            {
                // Marcar PROVINCIAL
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X",1, 9, iTextSharp.text.Element.ALIGN_LEFT, 385, 508, 0.0f);
            }
            else if (cmValue == 1)
            {
                // Marcar DISTRITAL
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X",1, 9, iTextSharp.text.Element.ALIGN_LEFT, 447, 508, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.Lista}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 101, 500, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.Organizacion}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 102, 487, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.Lugar}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 133, 590, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");

            if (DateTime.TryParse(request.Fecha, cultureinfo, System.Globalization.DateTimeStyles.None, out var fechaCompleta))
            {
                // Escribe solo el día (ajusta coordenadas si van MM/yyyy en otros lugares)
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaCompleta:dd}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 133, 578, 0.0f);
            }
            else
            {
                // Opción B: si SOLO tienes el día en request.FechaDia (ej. "22")
                if (int.TryParse((request.FechaDia ?? "").Trim(), out var dia) && dia >= 1 && dia <= 31)
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {dia:00}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 133, 578, 0.0f);
                }
                // Si necesitas también mes/año, añade campos request.FechaMes, request.FechaAnio y escríbelos en sus coordenadas.
            }


            //pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, $"{request.ImpresionBiometricaCliente}", 2, 422, 542, 80, 80);
 
            return pdfbase64;
        }

        //3
        private String GetAPAnexo2JNEPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombreCandidato}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, /*X+61*/ 167, /*Y*/ 555, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 544, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DireccionDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 100, 533, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DistritoDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 149, 521, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ProvinciaDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 297, 521, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DepartamentoDomicilio}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 124, 510, 0.0f);

            // Tipo Comunidad
            int? cmValueNullable = request.TipoComunidad;
            int cmValue = cmValueNullable.GetValueOrDefault(-1);

            if (cmValue == 0)
            {
                // Marcar Nativa
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 338, 496, 0.0f);
            }
            else if (cmValue == 1)
            {
                // Marcar Campesina
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 417, 496, 0.0f);
            }
            else if (cmValue == 2)
            {
                // Marcar Pueblo 496
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 173, 495, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombreComunidad}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 339, 487, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ProvinciaComunidad}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 100, 463, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.DepartamentoComunidad}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 317, 464, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.Lugar}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 133, 360, 0.0f);

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.Fecha, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 135, 348, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 174, 348, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 224, 348, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.AutoridadCargo}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 147, 170, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.AutoridadNombres}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 147, 158, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.AutoridadApellidos}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 147, 148, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.AutoridadNroDNI}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 148, 138, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.JuezCargo}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 381, 170, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.JuezNombres}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 380, 158, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.JuezApellidos}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 380, 148, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.JuezNroDNI}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 381, 138, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ImpresionBiometricaCliente}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 354, 298, 0.0f);

            return pdfbase64;   
        }

        //4 
        private String GetDCMInfoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));



            //Pagina 1

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 190, 608, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 1, 10, iTextSharp.text.Element.ALIGN_LEFT, 120, 597, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);

 /*           if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 437, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 729, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 638, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 638, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 132, 730, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 730, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 701, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 156, 701, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 234, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 638, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }

                if (request.TipoDocumentoCliente2 == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 151, 0.0f);
                }
                else if (request.TipoDocumentoCliente2 == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 151, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 151, 0.0f); }
            else if (request.TipoDocumentoFirmanteAdicional == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 151, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 105, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 235, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 365, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 500, 63, 0.0f);

            //Pagina 5
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 437, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 729, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 638, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 638, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario1} {request.ApPaternoBeneficiario1} {request.ApMaternoBeneficiario1}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario1}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario1}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario1}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario1}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 247, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario2} {request.ApPaternoBeneficiario2} {request.ApMaternoBeneficiario2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 233, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario3} {request.ApPaternoBeneficiario3} {request.ApMaternoBeneficiario3}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario3}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario3}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario3}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario3}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 219, 0.0f);


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 305, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 150, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente2}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 150, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 132, 730, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 730, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 701, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 156, 701, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 234, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 638, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }

                if (request.TipoDocumentoCliente2 == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 151, 0.0f);
                }
                else if (request.TipoDocumentoCliente2 == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f);
                }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 151, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 151, 0.0f); }
            else if (request.TipoDocumentoFirmanteAdicional == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 151, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 105, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 235, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 365, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 500, 63, 0.0f);

            //Pagina 6
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 437, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 729, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 638, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 638, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario1} {request.ApPaternoBeneficiario1} {request.ApMaternoBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 247, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario2} {request.ApPaternoBeneficiario2} {request.ApMaternoBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 233, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario3} {request.ApPaternoBeneficiario3} {request.ApMaternoBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 219, 0.0f);


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 305, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 150, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 150, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 132, 730, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 730, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 701, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 156, 701, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 234, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 638, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }

                if (request.TipoDocumentoCliente2 == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 151, 0.0f);
                }
                else if (request.TipoDocumentoCliente2 == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 638, 0.0f);
                }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 177, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 151, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 286, 151, 0.0f); }
            else if (request.TipoDocumentoFirmanteAdicional == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 342, 151, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 151, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 105, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 235, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 365, 63, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 500, 63, 0.0f);
*/

            //Hoja 6
            //if (request.TipoMoneda == "Soles") {
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 178, 623, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" S/{request.MontoCredito}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 505, 0.0f);
            //}
            //else if (request.TipoMoneda == "Dolares") {
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 248, 623, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" ${request.MontoCredito}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 505, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 248, 623, 0.0f);
            //}

            //System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            //DateTime fechaTransaccion = DateTime.MinValue;
            //bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 445, 623, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 490, 623, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 530, 623, 0.0f);
            //}


            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 565, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApMaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 305, 565, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 533, 0.0f);

            //if (request.TipoDocumentoCliente == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 304, 533, 0.0f); }
            //else if (request.TipoDocumentoCliente == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 533, 0.0f); }
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 533, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 533, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoBeneficiario1} {request.ApMaternoBeneficiario1} {request.NombresBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 30, 452, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 452, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.PorcentajeBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 280, 452, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.RelacionBeneficiario1}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 452, 0.0f);

            //DateTime fechaNacBeneficiario1 = DateTime.MinValue;
            //bool formatNacBeneficiario1 = DateTime.TryParse(request.FechaNacimientoBeneficiario1, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacBeneficiario1);
            //if (formatNacBeneficiario1)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacBeneficiario1.ToString("dd/MM/yyyy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 452, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoBeneficiario2} {request.ApMaternoBeneficiario2} {request.NombresBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 30, 439, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 439, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.PorcentajeBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 280, 439, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.RelacionBeneficiario2}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 439, 0.0f);

            //DateTime fechaNacBeneficiario2 = DateTime.MinValue;
            //bool formatNacBeneficiario2 = DateTime.TryParse(request.FechaNacimientoBeneficiario2, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacBeneficiario2);
            //if (formatNacBeneficiario2)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacBeneficiario2.ToString("dd/MM/yyyy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 439, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoBeneficiario3} {request.ApMaternoBeneficiario3} {request.NombresBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 30, 426, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 426, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.PorcentajeBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 280, 426, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.RelacionBeneficiario3}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 426, 0.0f);

            //DateTime fechaNacBeneficiario3 = DateTime.MinValue;
            //bool formatNacBeneficiario3 = DateTime.TryParse(request.FechaNacimientoBeneficiario3, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacBeneficiario3);
            //if (formatNacBeneficiario3)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacBeneficiario3.ToString("dd/MM/yyyy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 426, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 370, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApMaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 315, 370, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 340, 0.0f);

            //if (request.TipoDocumentoFirmanteAdicional == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 340, 0.0f); }
            //else if (request.TipoDocumentoFirmanteAdicional == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f); }
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 340, 0.0f);


            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresVendedor} {request.ApPaternoVendedor} {request.ApMaternoVendedor}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 75, 100, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.EmailVendedor}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 375, 100, 0.0f);

            return pdfbase64;
        }

        //5
        private String GetDCMDesgravamenPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //PAGINA 4

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 128, 722, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 170, 722, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 190, 721, 0.0f);
            }

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 450, 723, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 490, 723, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 530, 723, 0.0f);
            }

            //if (request.TipoCredito == "DINERS CLUB FREE") { 
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 38, 698, 0.0f);
            //}
            if (request.TipoCredito == "DCM SPECIAL EDITION") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 682, 0.0f);
            }
            else if (request.TipoCredito == "DINERS CLUB") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 235, 696, 0.0f);
            }
            else if (request.TipoCredito == "CARTE BLANCHE") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 235, 682, 0.0f);
            }
            else if (request.TipoCredito == "DINERS CLUB MILES") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 420, 696, 0.0f);
            }
            else if (request.TipoCredito == "DINERS HIRAOKA") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 696, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

           
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 40, 655, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 655, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 40, 625, 0.0f);

            if (request.TipoDocumentoCliente == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 628, 0.0f); }
            else if (request.TipoDocumentoCliente == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 357, 628, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 336, 628, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 420, 625, 0.0f);

            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 222, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 222, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 168, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 168, 0.0f);
            }

            //PAGINA 5

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresVendedor} {request.ApPaternoVendedor} {request.ApMaternoVendedor}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 72, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.EmailVendedor}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 72, 0.0f);

            //PAGINA 6

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 128, 721, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 170, 721, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 190, 721, 0.0f);
            }

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 450, 721, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 490, 721, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 530, 721, 0.0f);
            }

            //if (request.TipoCredito == "DINERS CLUB FREE") { 
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 696, 0.0f);
            //}
            if (request.TipoCredito == "DCM SPECIAL EDITION") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 680, 0.0f);
            }
            else if (request.TipoCredito == "DINERS CLUB") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 235, 694, 0.0f);
            }
            else if (request.TipoCredito == "CARTE BLANCHE") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 235, 680, 0.0f);
            }
            else if (request.TipoCredito == "DINERS CLUB MILES") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 420, 694, 0.0f);
            }
            else if (request.TipoCredito == "DINERS HIRAOKA") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 694, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 155, 0.0f);
            }

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

           
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 40, 653, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 653, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 40, 623, 0.0f);

            if (request.TipoDocumentoCliente == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 626, 0.0f); }
            else if (request.TipoDocumentoCliente == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 356, 626, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 336, 626, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 420, 623, 0.0f);

            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 220, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 220, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 166, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 166, 0.0f);
            }

            //PAGINA 7

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresVendedor} {request.ApPaternoVendedor} {request.ApMaternoVendedor}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 62, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.EmailVendedor}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 62, 0.0f);



            return pdfbase64;
        }

        //6
        private String GetDCMSolicitudAfiliacionPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            // String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            String pdfbase64 = request.FormatoCronograma;

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}
/*
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 710, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 58, 695, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente} {request.ApMaternoCliente}, {request.NombresCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 150, 108, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 90, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd/MM/yyyy")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 95, 72, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("T")} {fechaTransaccion.ToString("tt")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 54, 0.0f);
            }


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 95, 36, 0.0f);
*/
            return pdfbase64;
        }

        //7
        private String GetDCMTarifarioPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

 /*           pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 724, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 58, 709, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente} {request.ApMaternoCliente}, {request.NombresCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 150, 95, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 77, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{fechaTransaccion.ToString("dd/MM/yyyy")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 95, 58, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{fechaTransaccion.ToString("T")} {fechaTransaccion.ToString("tt")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 40, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 95, 21, 0.0f);
 */
            return pdfbase64;
        }



        #region AddHoja Firmas
        private string AddPageSign(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)
        {
            DateTime fechaReniec = DateTime.Now;

            int numberOfPages;

            String pdfbase64 = PdfWorker.AddPage(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkD.png")));

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno + " - " + "1" + " " + DocumentoIdentidad, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x - 10, y + 190);
            pdfbase64 = PdfWorker.DrawLineInPdf(pdfbase64, numberOfPages, x - 15, y + 185, x + 495, y + 185);

            pdfbase64 = PdfWorker.DrawRectangleInPdf(pdfbase64, numberOfPages, x - 15, y - 25, 150, 200);

            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FacialImage, numberOfPages, x + 25, y + 50, 70, 120);
            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, watermark, numberOfPages, x, y + 50, 120, 120);

            //pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, numberOfPages, x, y + 30, 120, 20);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, _pdfFormats.SignTextLinea1, numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, _pdfFormats.SignTextLinea2 + " " + fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "servicio de verificación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "biométrica de Reniec con fecha", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "N° Doc Identidad: " + Id, numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);

            pdfbase64 = PdfWorker.DrawRectangleInPdf(pdfbase64, numberOfPages, 40, 40, 520, 70);


            string Stamp;
            string Empresa;
            string Direccion;

            Stamp = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/logo_diners.png")));
            Empresa = "Diners Club Perú S.A.";
            Direccion = "Av. Canaval y Moreyra Nro. 535 – San Isidro";


            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, Stamp, numberOfPages, 50, 50, 147, 50);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"El presente documento se encuentra firmado digitalmente de acuerdo a ", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 85, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Ley N° 27269 – Ley de Firmas y Certificados Digitales vigente en Perú.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 75, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Firmado digitalmente por " + Empresa, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 65, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, Direccion, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 55, 0.0f);

            return pdfbase64;
        }


        private string ExistingPageSign(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)
        {
            DateTime fechaReniec = DateTime.Now;
            int numberOfPages;

            PdfWorker.GetMaxPageNumber(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkD.png")));

            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno + " - " + "1" + " " + DocumentoIdentidad, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x - 10, y + 190);
            pdfBase64 = PdfWorker.DrawLineInPdf(pdfBase64, numberOfPages, x - 15, y + 185, x + 495, y + 185);

            pdfBase64 = PdfWorker.DrawRectangleInPdf(pdfBase64, numberOfPages, x - 15, y - 25, 150, 200);

            pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, FacialImage, numberOfPages, x + 25, y + 50, 70, 120);
            pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, watermark, numberOfPages, x, y + 50, 120, 120);

            //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, BarCode, numberOfPages, x, y + 30, 120, 20);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, _pdfFormats.SignTextLinea1, numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, _pdfFormats.SignTextLinea2 + " " + fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente con", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biometría facial utilizando el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "servicio de verificación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica de Reniec con fecha", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

            return pdfBase64;
        }
        #endregion

        #region AddHoja Firmas Facial
        private string AddPageFacial(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)
        {
            DateTime fechaReniec = DateTime.Now;

            int numberOfPages;

            String pdfbase64 = PdfWorker.AddPage(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkD.png")));

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno + " - " + "1" + " " + DocumentoIdentidad, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x - 10, y + 190);
            pdfbase64 = PdfWorker.DrawLineInPdf(pdfbase64, numberOfPages, x - 15, y + 185, x + 495, y + 185);

            pdfbase64 = PdfWorker.DrawRectangleInPdf(pdfbase64, numberOfPages, x - 15, y - 25, 150, 200);

            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FacialImage, numberOfPages, x + 25, y + 50, 70, 120);
            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, watermark, numberOfPages, x, y + 50, 120, 120);

            //pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, numberOfPages, x, y + 30, 120, 20);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "Firmado electrónicamente con", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "biometría facial utilizando el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "servicio de verificación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "biométrica de Reniec con fecha", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "Firmado electrónicamente el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, fechaReniec.ToString("dd/MM/yyyy") + " con tecnología", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "Bit4ID S.A.C. y validación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "biométrica facial a través de", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "la tecnología de Facetec Inc.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "N° Doc Identidad: " + Id, numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);

            pdfbase64 = PdfWorker.DrawRectangleInPdf(pdfbase64, numberOfPages, 40, 40, 520, 70);


            string Stamp;
            string Empresa;
            string Direccion;

            Stamp = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/logo_diners.png")));
            Empresa = "Diners Club Perú S.A.";
            Direccion = "Av. Canaval y Moreyra Nro. 535 – San Isidro";


            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, Stamp, numberOfPages, 50, 50, 147, 50);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"El presente documento se encuentra firmado digitalmente de acuerdo a ", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 85, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Ley N° 27269 – Ley de Firmas y Certificados Digitales vigente en Perú.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 75, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Firmado digitalmente por " + Empresa, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 65, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, Direccion, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 55, 0.0f);

            return pdfbase64;
        }

        private string ExistingPageFacial(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)//, bool textoDerecha)
        {
            DateTime fechaReniec = DateTime.Now;
            int numberOfPages;

            PdfWorker.GetMaxPageNumber(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkD.png")));

            //if (textoDerecha)
            //{
                pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, FacialImage, numberOfPages, x + 30, y - 8, 45, 55);
                //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, watermark, numberOfPages, x, y, 120, 55);

                //// texto a la derecha de la huella
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x + 90, y + 40);

                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy") + " con tecnología", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x + 90, y + 30);

                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Bit4ID S.A.C. y validación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x + 90, y+18);

                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica facial a través de", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x + 90, y + 8);

                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "la tecnología de Facetec Inc.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x + 90, y - 2);
            //}
            //else
            //{ 
            
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno + " - " + "1" + " " + DocumentoIdentidad, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x - 10, y + 190);
                //pdfBase64 = PdfWorker.DrawLineInPdf(pdfBase64, numberOfPages, x - 15, y + 185, x + 495, y + 185);

                //pdfBase64 = PdfWorker.DrawRectangleInPdf(pdfBase64, numberOfPages, x - 15, y - 25, 150, 200);

                //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, FacialImage, numberOfPages, x + 25, y + 50, 70, 120);
                //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, FacialImage, numberOfPages, x + 30, y - 8, 45, 55);
                //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, watermark, numberOfPages, x , y, 120, 55);

                ////pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, BarCode, numberOfPages, x, y + 30, 120, 20);

                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente con", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biometría facial utilizando el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "servicio de verificación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica de Reniec con fecha", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

                ////pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 44 );
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy") + " con tecnología", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 56 );
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Bit4ID S.A.C. y validación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 68);
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica facial a través de", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 80);
                //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "la tecnología de Facetec Inc.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 92);
            //}

            return pdfBase64;
        }
        #endregion

        private async Task<TransaccionalDocumentFormater> SaveEntityTransaccionalDocumentFormater(BpmRequest request)
        {
            TransaccionalDocumentFormater contract = null;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(Constants.ConnectionStringName));
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                TransaccionalDocumentFormaterRepository transaccionalDocumentFormaterRepository = new TransaccionalDocumentFormaterRepository(applicationDbContext);
                TransaccionalDocumentFormaterService transaccionalDocumentFormaterService = new TransaccionalDocumentFormaterService(transaccionalDocumentFormaterRepository);

                TransaccionalDocumentFormater transaccionalDocumentFormater = new TransaccionalDocumentFormater();

                transaccionalDocumentFormater.DataRegistered = DateTime.Now;
                //transaccionalDocumentFormater.DataReceived = request.DataReceived;
                contract = await transaccionalDocumentFormaterService.InsertAsync(transaccionalDocumentFormater);
            }
            return contract;
        }
        private async Task UpdateEntityTransaccionalDocumentFormater(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(Constants.ConnectionStringName));
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                TransaccionalDocumentFormaterRepository transaccionalDocumentFormaterServiceRepository = new TransaccionalDocumentFormaterRepository(applicationDbContext);
                TransaccionalDocumentFormaterService transaccionalDocumentFormaterServiceService = new TransaccionalDocumentFormaterService(transaccionalDocumentFormaterServiceRepository);

                TransaccionalDocumentFormater tcontract = await transaccionalDocumentFormaterServiceService.GetByIdAsync(id);
                tcontract.CreateFormats = DateTime.Now;
                await transaccionalDocumentFormaterServiceService.UpdateAsync(tcontract, true, x => x.CreateFormats);
            }
        }
        private String GetFingerprint(String identifier)
        {
            bool fingerFinded = false;
            String base64WSG = "";
            List<String> wsqPath = _configuration.GetSection("WSQPath:paths").Get<List<string>>();

            //List<String> wsqPath = _configuration.GetValue<List<String>>("WSQPath");
            String basePath = "";
            foreach (var path in wsqPath)
            {
                basePath = path;

                DirectoryInfo baseDir = new DirectoryInfo(basePath);
                if (!baseDir.Exists) continue;
                var posibleFilesWSQ = baseDir.GetFiles($"{identifier}*.wsq").OrderBy(f => f.LastWriteTime);
                if (posibleFilesWSQ.Count() <= 0) continue;
                FileInfo filesWSQ = posibleFilesWSQ.FirstOrDefault();

                if (filesWSQ != null && System.IO.File.Exists(filesWSQ.FullName))
                {
                    byte[] wsq = System.IO.File.ReadAllBytes(filesWSQ.FullName);
                    base64WSG = Convert.ToBase64String(wsq);
                    fingerFinded = true;
                    break;
                }
            }
            //DirectoryInfo baseDir = new DirectoryInfo(basePath);
            //FileInfo[] filesWSQ = baseDir.GetFiles($"{identifier}*.wsq").OrderBy(f => f.LastWriteTime).ToArray();
            if (!fingerFinded)
            {
                throw new Exception("No se encontró huella");
            }
            return base64WSG;
        }
        private String ConvertToBase64Fingerprint(string base64, bool forHtml = false)
        {
            try
            {
                Monitor.Enter(base64);
                _logger.LogInformation("Initializing ConvertToBase64Fingerprint.... ");
                return $"{(forHtml ? "data:image/jpeg;base64," : "")}{WSQConvert.ConvertToJpg(base64)}";
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                _logger.LogCritical("ERROR: ConvertToBase64Fingerprint: {0}", base64);
                throw;
                //return "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
            }
            finally
            {
                Monitor.Exit(base64);
            }
        }
        private String ConvertToBarCodeMinuciaFingerprint(string text, string base64, bool forHtml = false)
        {
            try
            {
                Monitor.Enter(base64);
                _logger.LogInformation("Initializing ConvertToBarCodeMinuciaFingerprint.... ");
                return $"{(forHtml ? "data:image/jpeg;base64," : "")}{BarCode.CreateBarCode417($"{text}|{WSQConvert.ConvertToMinucia(base64)}")}";
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.ManageException<DCMController>(ex, _logger);
                _logger.LogCritical("ERROR: ConvertToBarCodeMinuciaFingerprint: {0}", base64);
                throw;
                //return "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
            }
            finally
            {
                Monitor.Exit(base64);
            }
        }
    }
}
