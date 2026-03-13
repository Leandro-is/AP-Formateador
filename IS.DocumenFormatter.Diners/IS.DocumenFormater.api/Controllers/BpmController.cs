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

namespace IS.DocumenFormater.api.Controllers
{
    [Area("API")]
    [Route("api/pichincha/pld")]
    [ApiController]
    //[ApiAuthorization]

    public class BpmController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPdfFormats _pdfFormats;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<BpmController> _logger;

        public BpmController(
            IConfiguration configuration,
            IPdfFormats pdfFormats,
            IHostingEnvironment hostingEnvironment,
            ILogger<BpmController> logger)
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
                ImpresionBiometricaCliente2 = "/6D/qAB6TklTVF9DT00gOQpQSVhfV0lEVEggNTEyClBJWF9IRUlHSFQgNTEyClBJWF9ERVBUSCA4ClBQSSA1MDAKTE9TU1kgMQpDT0xPUlNQQUNFIEdSQVkKQ09NUFJFU1NJT04gV1NRCldTUV9CSVRSQVRFIDAuNzUwMDAw/6QAOgkHAAky0yXNAArg8xmaAQpB7/GaAQuOJ2TNAAvheaMzAAku/1YAAQr5M9MzAQvyhyGaAAomd9oz/6UBhQIALAPXUAIZ1gPXUAIZ1gPXUAIZ1gPXUAIZ1gPlwQIbkgPlGwIbfgPuEgIckQPtOgIceAPiuQIbNQPvKQIcswPvrgIcwwPX9gIZ6gPerwIauQPYnAIZ/gPcWQIacQPrTgIcPQP7VgIeKQPk0wIbdgP0EQIdSgP77AIeOwIa/wIgZgIZpwIeyAIblwIhHAIbLgIgnQIcNwIh3AIafgIfygIciwIiQQIbKQIgmAIb6gIhfwIbTQIgwwIbuAIhQwIbfgIg/gIdjQIjdgIbVwIgzwIcxgIihwPwvwIc5AP26wIdoQP9NAIeYgIbVAIgywIaAgIfNgIZzgIe9wIb5AIheAIarQIgAwIalQIf5gIaRgIfiAIbswIhPgIdHwIi8gIbXgIg1wIaeQIfxQIdVQIjMwIdDAIi2wIdZAIjRQP83AIeWAIlXAIs1AP9owIecAInKgIu/wP1xwIdfgP0vQIdXgIirgIpngIkGgIrUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP+iABEA/wIAAgACVNMEQkUCAAD/pgBuAAABAwEDBgcHCwgQEwkAAAC1AbKztgKxtxESr7C4wQMQE665ursECA8Uaa3ABQYHFRaqq6y8vb8JFxiSk5mevgoOGRoci4yNj5GUlZyhwsQLDA0dHh8ig4WHiYqOmKOnqKnDkJaXm52foKTG/6MAAwDffff09Pp9Px/H6b77777777/y3/6/6/n/AD/lvvvvvvv6fyt/t/D9v6v4/Ttvvvvvvv1+/wDb+39n+H29Pnvvvvvv6dPt/f8Ad9v/AG+6fTffffff02/X9/3f6f5/u1/Hffffff0x/f8A5fp/X+r7v9/p6b7777+nX/H9P7P1/b/7/j9PTfffff0+X/j7ft/d93+v8Px9N9999/T5/wDr/wA/u/7/AOv7/wD7233333336/H+P8P4fh+H4fP0333333+XP/b4fh933/8APz333333/l16/H3/AIff9/v+m++++++/z+l/+P8A5/v8F9N999999+3b6Y22339N9999999999/Tf09N999/7p014N9HXrDRTDbPWV0HFDve1RNV7JoYAOvsh86cDr312hHgns7dkLFj11WlPV2nrrHxRIj2YeivFfb2UzWtpNu/4Aofnijbn1dK/H0/n/8AvxVd+mlY2qd5fo5ad8Wor9K75FFtVd5tRq6699arZOP6sTXbaXPYYDWnK72oac53ibxOdz2SFa7VXeC2OfTp31SwE/pk/O6hYZPZVxN6/ViO8b/JFsaf0PCoJ37Dm0U79lqtoz6mNPYnrixetUYnsoV1hn1XUoh9TR2s/wAkED79lP6p5PJ3BnXmgjsCwooflPPqykBaGFDwuWjTE6nJ60sUhU5i6nG/vTpSzUNSeA7puxKmoyZfBNS7uoiVONaFr1WB21JxrSKcCXkW04y99FShImicTVyWqKCNZxmsx0GKpeK5xnm9jExSJgVcb6q9o8S92yuOrjko2wq1aDw1wbPmBrJGjwNKa567WJcl68DXF3rcuwbjPAvWpqhyJsQflGg2UhMC1k8C/AZQqV+B+UaEFiBwngTgRBOBhHgV65Y8argS0tPK3kkkcnIqHTrKCsIzBuqOlLGxiQzGyItcRlCqkwVZ0zKamwi1OlM8alFKkKXmZKMUgwCnIvLUpbDMyKpLvWo4ZF5dY7gvKzu9Rqrmt9HzrKopzjb15rneZPr86jQpUg8q+RQK7WL2M6qPRTnaMDLV7R69NrlVDiW1tTVPVMyi5u2BDhrjHGTBUaMYmDOIpAIWrV+UAxzW1GNLvk4prVHS8ucg10MR6yhHEg9XXaPmdMZ1wirGNj3T+yeV8eraNysWmQh1te+kqMMoEihVrzlVVD16NTIwYCSW0fKjEU4TVzlL1FFS1VUpkDg1GjptMnJV7MaCZ1ahRCuXvfIUmJa7kvXTPETilq2XSZ3FLyJvbGL50M1aqtbk1zqWGUsRX5YvT6Rtq/CcRayleFm8TMAKFLRScZKMWlopM5MApEa3xt4uQB2JUylpK7hNbeMHaScJnERRF6q0+uT7I/L9oVcKkijE8oWLQBRTjrreEBSKjN1O4QicpEIVCkUzwAopFSkzJfBWBUrsU44Aq8ERAZU6IODiqcqvXCOsKkZ7c4IscGZ1ppTVxUWmUNXKkU1lWnG5pelJFhA5x0WqohQYlTXi0VqVMSalp4EixZpmrMXngWk2WJTaL1PA7uiaqEFeeAoKRXRI54x6CLDV1PA4MQp3VnPUKWlGcliCf1ndLHl7sTKnqp5gCbJIbVm4qQpHKkU4u2KaKQLrnGw4SQHM9dkAFoci6ikk7mjLVOHSKUoZi6rAxUmnUyi1dFir4inGXvcYxRQzmt+v/GO66LTjH71RESgRlOvbq1E+6I4vGlUx6VdUSPwCmpOa7QwocZNcD7P4AqRk1SnNt0to9TkbSWupbGTVJ2g+6eNUIgHjLNFKNQk9oKFUh1OMLMdofmJiCEeWrO4tIeJShWw3KM45dFndGJyJJaIGTMRgFJKEZuKKSWljOFWJwJIjjTgroq0oykaYItRtjOh0ohRlXM6KctFpdR5MqKlUNudw5UOq4dIipxFO3nOEBx5BOnwd5KUUK4mo7SIhGHGIFKRGqPcEkVNG2UtTiIipJyiB0pCKcQUQoIcVScRRlgtCfWKKIkPdIixJnO4sf1zy3CewM5aeopVEZQIIWEKvGJ0iVVV4wQoeqjlK4FjgZjwoqswUzgWNgEPMvZl0JrFpxJHweI0OGVLrWsWIoIymxFJVNS0ykw7qHdLHEV+t7U3FJUyrHn2QSFqA8DXsyzJaGcl7kHKqU84or2gIcRxksNSDlM5UCgUqkk4wsIiCOop4ThU7nCvzCESme5iFET2iFD3HrnleFpJJOUl5evjfnz0052kyO17mGSuBOMWiBx2kzkkrR2lLCPElCr1EzwS/dTkrMhsIE7oPEnORQcUzvauy2dhpmWPkLQRhaxysqhrdrVzXS7eeOirpYo2m2pl0vFUa14Ghk7xmk12om3XOlJ0xXR0jTbIvjVfK/Ppi+LmmbWHf51Lq9tp4E2Qnzv2+ldJroZEl5t219/PzUmd+nTrWvmvl1+bkzOej01+fy9+NPhmW6VOfb6fDbXz67ZS0fPpsvj58/kao8Qw1Wyc5HCqKj9tdwjyvT80tIsaXZ1rgcSSY87mOmvxifEjtMYx0muvKPBaPXnKrHjC6hWxqVnNL1wj5yu2JlOmNaJVbVt1JyZBXcRfS+RSiuKSPnzrlFsIaxNdtsoChGKu3y665vGyk1hfHyfwOIOqvzpTz0116YyHMdeyc27YfScZVq+S0TmjqnkNVdixVneuIYvJUiiqVMpcmLSosK5xJh1BVwCZBpV064YUmcpqoqjFPbC9SS0IchBBRAeMURBz2licCf2nlryMfdJ7a221cu7xlp4Q5306XMIKnKbfT+eLrHADOuc10YgmzMhxfPtJE6imRMfH8ejqBUnjpPPzkHYq6OMi1VV3VYyrsroLottXKryNS7cnIhNOmynPXm0ZE5q6uhVQPJ6O9huVfDzKTV6ve9Ver8YpRNdta6Ptpg5RbR1jTzXn8+UpaF/izp2+nyxxiilQr4Oq56KeAGnzTnTpi+vwhyxojE58+p1xmK+3OF1j3kZxlqnWrv311v52njpJKrUx9ZUWiwpyfCRRSe08sMTiK4HEhCTba0Uk4tFLbHvveQFMtqh59p79b87mcw49bvFGFWdTzF5nXGxK5U+lvh53von4xU9tZjBjtRyk7TrswNWplEF72wsY2V+V5fSSzvORhuUnJYh4kXXZSo6Rmyz89emFr/wAvMCVj5Udfl9NnnG9pNJd/CjMDp1DhO2pMovj56xXlFS0yrExUTs4nnIcCkWhOMtfCkkVypkWogPHPAj2xAalwJyCqY3PbOBE7TxD1inlrCklCih4hRV7yICZRHAVUtfEndIjCnIALVI0+URwd1JdmdUqtVF1tVZxHCOI02qswwS1O/a95k5ha0mi/xes5F05Wp1UDlEVKWvy61RnVppLj02LucSINZalOUQtj1OYUMjXbFKKkRScYOBFFKtjKKsK5JEWrk/UyWJyDgRCYciRSCuz3FhCCx7imN8J9kBByk7gnl+nCoe52ALSZyxwBD8Rqepwp/WWERPOJt2pjljOAKoXLHKd1Ym1oTPFwlXwDIZ1bCvtTmA5DhYu4yTOSFjWsS05UpsgZBVyg6iEBMx0RSiItMynUnAUpGcqO0ZKT5FopFUklchUz1VMoi0UUKWx4lg7oZncQ4RHtlKRolq+sgp7j1nl9Sx9gnwJGSInJqHSMup4zu53JWKtOMida37dey+JMzDtG/h8fjV3nObDRytsbIzmOlunXXHPpz1YZXWAdLHnOimY2tMKCGGQQOimNHWJMgs8YnDBjlSdhX20veGU4Shdq2+dKuQoSF+eiqs4oYkUtEZSm1LRSpyL6UFgVJmdddrEUgmZxzgHADjEWbtLQgZsREEU+yRMh9YThKTiPEtD9E7jy3yeLsKRzF0chUIixezEZSwigLVyHYFFJSmbdWNaVX6PxGpHhkZ43qgRtcqoZ4eB0tr1znJ1RBTn1sQzFSjw3aTKThRJ0iZOUTjieKMIypkIhlIuZRN3iJi0MpwvEQjT8XSBCAqZyWkT4EfixFWIKs4IQiIdGRYoTliI8aTCcGp7YCdj7ICEEnKd0R+iWPJv/AP+mAHQBAAIBAQQCCggLCxcHCgcAALO1AQIDBLK2BQYHCBITFBUWabG3CQoLDRARF2oMDg8YGRpKS05RuEhJTE1PUFRVVmCwGxwdHh9FRkdSU1dYWVpbXF1eYWJjZK8hIiQoQl+5ICUnKjVAQUSuuiMsLzE2P63/owADAecRmxBXQPU0YNjuYUQrM6G5uxpzDgozMZkN6QYMIO5zMilEcQ8UpGILuzTGmh4PSMC6niJ/6rDj1/4nUwXcR3D0IQTlk/C2Lm96wHp1xRQeOZ0x06z8MXDwZil6Y/CFjdiZhCYes6HirGY6YrHHMejnGenU5YSxSPi0IGEjB4HaeAU0fmaOfyDQ3ParHeJZYY4ZsUiHJIFMPWuaXgFwYLvc4g6GHxFzcyVngEaxEnXc4xMxhFjHemaYzPAFgetoYMbI78dSK4InBOgOBfWFMLG9wRCJQbiEKT19YDgu8TC4gpuNcscnkzEXQ8C4ufyMOe+eRRiAU2dwxgw6rvDUQeChdmIblHCDHipG+MvAizo0QhvDQmcDvMBMYRjw/wDUyHTLY3oqmQjDxFfwpIRHcdHJAIOcHgHy6UosafB6EbKZgviDgtjEcO/GWYbJl8ShixMDxzjJCgmHeXAhHkGYkXyazHDHc3Sj8Zz/AIuntQo/EfiabLwGEWsVjdihiQsbhoYJGJ4uo2N5jQEscCyHtLJMnAymKDA4dyTMKITBubN2ZN+OoUK2PFwzDGz5BTYycWi7HewiXxHPEIRix4CRzR5YHRp4JEpo4thX8r+R5+7sWGODZ2LuLECzybBCMIeJAgXc8WmEDggMQjA3ZaYRDioUnS2DeWcsKXcwFopMG5pAbu4M5gNgp9TYGgjwJil6OSMHwMUYmGMQ3lOWCvAIBG4+LTboYFo8CEew4N2A7myVgI2ODGswj6yye1Ofwdh7S5+Y4ly7hDgwhGEx6y6nrIwCG8p0Xe0Nmw7yiEKTiQiMMU7mJ1IUm8Z0buh4EGZjRRuGNiGGB4lsGw4sZhRE8SxWIJF4FEX1lwMj5NkjjeaOjyaCL7Szj8Rz2XteBG5/8PsabJxULFO8Gzm2Xc2WK9adzErDFXegNdY9civgzr1dcHFchFgcMw65Ixo4PTGBay8sGCNiG9gRxHIb0xZBKDxSwkYsfEpZiEeWbo3yeAIYMoXeLFohje2GFEHiVlsespaOT/kc/wBbP/6WH2rH/s3bLH1sI2dxDR5NDtN4uGFgjuKKYetcupHflYEbu92FCb+pRQ/lKHxY4GZGExwVoobPBixoXyNGBvIQ06exH8hc9pTz/ROLQ6I+TRBI7kCyTJnktkWO9TXFO9ixoyU7koMjF4MwCUq7xiE6uIbyKB1oeOIjfMThnqUxLYfAY3CEYbkXNkp3qFIMOGcQFjS+JoF2jwYqxEVd6MQacnAhCEIcWiYjQnAiwjH2PrOw5/xR/wBnTNg4pGhTgwuweBM0QLBvQjBLO4EUbYXe2JisWdxRoTPkxKQOLbJ60bC2Nxl64s00eKvSxZxwIwstC+JGKzNHJrG18Cls00eLArL+MKLBwZmJZDisdX2D5F2x5gI2INZ4qWy2OBRBROCOKLGfluZlikxCY9eJhmadxGLgSx4thTGFwG8gzM6LMcCJmnCY3tmhyTHB6kCEaN5SAjHctCxiWzwTQseDBGBo701Sz4tJDV3DSGWJvaw6seQEEeDq50eSFD7XzASMhvLlkmOJEIRM8CFsqmDcXYKLwI2YMeKRbnAuEOtne2M0uCPDNlHExwCDCMzwxFhAo9pkscVLkB8QCFBiKbghM658SxMQA5ZTUi8CMBzgg72zYsbxYsA9ikI+SXBj7GP5HnsO0i+0svtWleBQ3yHFpVmA5LEhyWi7yLrYOTZGscCmZoZg5FEHMDfiAWMw3iKwQp35zcaVPE6aFNjxVgWyqcFpzHKu8wKNk4KVlzTyCwzMMciINHrFfxJTTHc3YL+N/K+YCOtMf8nD+MfxlD7QscCFEYFG9HUfWQLu8KbHkMWJHyYkGEeKkwrGJvQpoj5DBX8Q6PEWw0HAhY0fJsR8miH4z/Qh5HtdTzASBeLTcj7FwXOLQKsHgEAPWUYgKR4NmEeK3MGCHDK2bPJzY0NwDTCHFIRsQmeA/LKTLHgGMMTEVj4JnVgsNy5cFEY+BMpYCHtyhD8SmYnFVsAu8ItZSHLOZiiLyIwX8z7Q5/J2n/4aHsdHydFCZjwbEaDiO1d6NAWA3ugx4JCnExTDcxuUU72i2YtG5jZbJwIMQION4t2jkDCmI+DAUgw9eGzGh5EIxDg6Dd5FkXi2S7+Np9rz91gRz165y2fHPVIRs7wCGcTEOAM/99IjnkfJ+RoY3s/4/D5DFIviVj5ZonXkOM/IrNl5YcmmHxflOnURKIeBl69UALG4nSKdOmMYxjfjPybJTvOhYHIvTcqvynTrOpxIrkmcvyxH1MwmZnGX/D/jc0QxmdPw/D8Iu/P+JmdMdc5+Ud/ToMzOh0wb3OWkz8s7wrHSsfLpWDg5XGH5ewzCKQOTQ55Bc57ucEWFO56pF0XxGnoZIpHxxEfkKYXeU9chAo3dcP8AjP8A7/469flld/SPSzOnTgv/ABmfgQwdcpuzmdSEc058SjGZmxDHidUcU2Y7ugYMYQJ06bsu3Lnr4nSgMGOnT/HBSydfl8n8N4zAW69M/wCM+OW4McE6+LDFFjDxx0xCGmN6WYmX15rNf4yPBjEzOuHixYwXeEbIkeDQvsMbHneAAHBmClzHk/8AGejhYR45w02eLM2Ix4kSx5Ey2yGDe2czICjvTBZYTry/DGY+TnrYskdwQMTB0DDvIZVbZeOIpBgG5hHDEDDuwHyWsJB8WLAhmM6nl0yh0xwIQI4j1Dc5rKB//YY7zJDHyzky7wGs4jk68FBSf4HiDbo5yY5EAhxb5ZjnuPsaJmF3jmdDGYsxTuzS5cU8CkUFjw6wYZgeWK6pgcu5hYmbu/AuYxsblI5IsOTscrvyEKaOTHM6URmN7ZBhiPtzZh4LTWEmYG9IZmcuHiKDSkN+KQbEN7GkoD2Inyp3lIRwmeJfEMdT1lGMnsGlPWK+t59C8mGSiPEunSsWPAot+GCGeQT8JnrEzuaIlNYx4pGGA6QM43uMTDR1eAQixj0Ibi4mTqjuK6asycCCzAQzvWsuFwYNzC2MdVnR3qqCvXPBMEZhgj4Geq7cG46BG48mmyDybivIRrLY4BMNOT1rqHFKGPE7Dn9kZh9r1Pw+WOKLfrhd6MTNDDg0iNOHcMJiYZ8umdwlNZDDk3MEMdcZ6T8ODCDnJMLvLIZzjq7yhMuMgZ4IApl64dxbqiOeibyEIVhOCR6kw0vDrM0zMdxMU2c0Z8cQQHQ4saFhRvw2YGeTWWkhuKUITFPJaaDyQopN7TQQ5/REpKdwAxMljc0ZSEOARsTDDkQVFd+aBKeRTWZkjvBjQGSG9ikcNByVguMG5aUsZjuKFZnC5N5oMCneiuILE5DYIx4OIkIw4ESm7xcjCPJskUzxex8iiDF8mI+tjGBd5FFHP/fxFsZOQxx0fJ0OuMvALJTnDuGFmMyx8TMzRHr1gpuMUUx5ARWMeJOkHNY6dTjjKwJk3lZikcxPEbYpgdcby2fkQepnk2KzkTwLoYY2PA2sTe1m+S54tyJYYbizCOSPka5OQxoY+LYijGDwGJQsxuNAse15/JTMY6dOmMcjMSw2fAzOpWIrxcGSn2FlaXgmY0uXgtxejmncRwnVpHc4mYBWVdwKjMiu9aQhQdOBM0kFhudG4bhgwHDB3MJmimk3BGiFGXk4PYJDEzQuPBg4Ry9CBje4gTPTD1OCYCukc7mzWcwIp4NJMZoxTxDR4tgpfxFPPZazQTNG5i2YWdzHMXI8skwls8AgsCsHAzgoI45DgXEyPDDkzWVg7jEzCNsdOLmAxhyJjFKkc+LMOWkLY4ubNENxGDHJXTyMOUenJZghnPWZ3i0XzM7muixoxnHizpjEMQiRdxGgbHIhMZjMbmnKNgwcGZgr0fY0YH1h5gICB7CYhQUH/wB9WjV4MzGzHgWCw8WJGOaDgXCsWN2WMAAzHxSwXwcGmjQ4EwwytdeDYgUeQwzBicjLAicjVHcaFGDIcCs02N7hosTL4lsXbEdzjLTGHJKKEeJGxd4jonkQpORdiPP5bOpuNCPIs2IHJppsQ3YM5oH1uIMaOBiZAFQ4DoRI+JQAtD5H5GxdV3tmmYjvbNHrLmq71sFDvLmo8RhqG4w0Ux9jDEJg4OSnMwexYtOPZmxZ4AMQ/In4iHPgBOJWGIZ8iEaccGA0xhxLYsRhvcOGkIcTILFYbhwXVxngwMlAOOWGyYhvYUkQgeLQ3YcimmzwYkW4u5ocgQCO9jCkE3Ec0RIO/DRTCMdw7GAHArpGMOQ3zCPkQKPIhmmjksfMBHiiP48GfJGdSsvEcVgz8p0TgUxz8npxy2wdJ+BDe9WskDoUbjAbENxTmYprDvzM6JTwBsOXfhtimk3mgQ5NlIKY4EQGsYhuCBjFNYN+Z0mCMyQ3Hypxi7x6xzFhQPgbSZpdzWLohwZizYfITI08ksKby7D1nmAgZoezMX2FYnUhyV1OKMMRjDkQIJF4BBGxHeR0aXyLvFWxd4tgY1nO9iGoexjZdzdAp5MIRHPAp0QNzTDRhvGxq/jfWjGnybMfxl0PY3PyHmAjLq/mPJjb5GfIgdIWeBkZ+BRCncN1R4CU3fW2Up5AamdxZSxHi0Fld4QbDQ7gNFmOI7Vj7Eg7mmxcTkXYwOQxGDwEuTKcMXVhvKUgWZjgEbFPJ1I8gi+YPXj+U734n/IjHYNjtP8AdLFnQ+x8n0lFn6ze6OjEbpGPNmdpd0GCXO5/qaBTCAw+JvNBglFMGiP0EeDGjU1GIzNPNpYJYY07X+6MbtyI4Qp/c7SIwuiJkJhjqe5udg7GxcbERKI1ij4niMaRGOMwSGr95EmYIUWMwuQ/eOxRpSJnCJCZxThYQ97q9g64awlOxEu/YnpbAUWLtMKT7WZhTqOgMYiJRGz9rR2rowSOKKYUglD8G5tGKUWUTJYusLJ9aFzsaLYzRTMxPsdGkjsREgZSMRLpRqfQiJYjZsjCBZwkazZs2H3NizsLMI4YEMwcWbGdr8GMPQFNBSTJTBw6P6k2tFIipgoxGYMnaFPuRppdCmsggUwjBg4LtP6W5BsQg3bC4EKYU4jFpWJ9DSDY0NCOcWKQWDTTZNH87BDMCHYMxWbhEMuKw0WI/BhCMHVg0RsEUGGSNDQe9ovmYYlzNlwAwCxmkW2fgDSsI6Fw0CiEQbv1FZoCNjJYozAGzRTREaFue8IWYJqg0URjMTKwiI0WH5m7opm5mAwxQRQ0xqwPgN2EyaDRZUNGGaV0X6igilMErLrmAwzRZCn6W7HQiUzBGmgQcwSFikgfA1abEaMaNAMGCQaLvzkIRKbiQuaEbKYhcu+40dXRjDCXKbkdWFj6CELOg9rAG5SUwiUMfpOxIU2EiwsZoTtP3sewNQsRU0Y+9p2kaKBhGYoGBGGw0PnLNylxWaNFSJhCBYYxhR+gstFzZhNSMGYmYMGsRoofcLc2sGizhazgGCWLmg/YZSCQSwgjYhls6B8BuWI0MbNOrSxopiie87mmkopomSECMzGIe8uwu0GwS52I7EKfrA0IO1s2Y4pjZFpufOAwppHYdjEg6ZhojH9L2DZLsbuxgjCyRufSUO4IRZnwD3hdpo7XQIF1Y6ie4ihoNgIwp7WjQdp7g2Nm4FnaupdpPebUp0LPAgtKfWR1ItN2NNnRo/YaJYos0NMbtI6ET4JdYUNMKdG400li5B+hHRs009zTCNFNMP0g6MKGEbrRCz2lOxsfnbsE0bCbG5qMKY0w+ppGPbi7YsLGmhpIlHwSi7EjMdoGxKDYfSurTCjYzO1LMI0n6H0JRRRTFCPcNECNH1EaCBCjYQ0YNhbMw0sPndXa6lmixZSy6n0ELqwdgWW4dhFs/FIelhc1KbNiJ+h7gh4tFyCWbIR+l7TuLLZoGDgbFP1NlphD1MW7oWf7sTvYbD4mhouoHoLEWCfMeh0CliwohdujCDTD7TQLCEKI6BCNgmfc+gsCwhRTqsFY6v7GFnU7mFy4x/mU2brZpBbmh8x2jddpcbigUUsLL+k9LZrFzQ0KIwYOh9wU2I0XGmlp2MPsfSwzsImpGy/cbM6MLl3UI2Ian6D0pQQpsU0Me0/Y2FLOpSqx0P7Nku06Ctjm4sWiH8Hc7Gjm5ng83Be97T4MTiR0PMHvf/+jAAMB8wfQDvP9j/oav1n/AMnxf9Q3n+x/qU6G05thsXuObK94c4o/abzR+o/0ebq+RzbnQKf3n5nRoDm0tg0XsI82l2OwuvuPzOxdrY5uroBHm7BYwfrd7o7GnUPnfzFNHcvxf8jvfoPFdpo3O4P7Hc0bG5873sNWxZ/q8SxZdgbCLA+BHYbw2NiMfe7D1Fil0bFGw960tjcR1ItOr+1uXdCNLZs7T6zcbAu3bHpP+5vNrA0blMNGPzLcpbmrtNF7GETQo/8AC6MLA6ujZ0dBdr72gooI7A1Tubn6mNzuCNmGrT6F+tuWfW82h2ugXSi2Wn9ZGGq0UeLcs/SrRqbCweLo+5swodCL3FLuX3sKIxjZuFEF7HaWYfMGova3YFg2HcfMw2LowsUUNmnYWfgwGiKws3UsujCzY1PpbCxVojwLNml+owWWzZ9GeT87QEdCgo7Q0PF+4COqwpdhZ2MX9LZpjfNPaxh2Aurd+dtmxTRq3Xvdr7ywQAoKCxcj5PzmxhRYo1YbmP0EL5W6w7GzRsf3BEYUxs/jaQi/QQMMCOh2NwsWdClfews6JYhqvBuWP0K2VhQdzTcgRWLcNT/Y0aKI+hojRxPndDtdBVpg3IpZ2H6DRs3KLr3B/J7QhDYvoSwbCHxLkaDse41foewp7QIEKKNobV+kNQLhCYoxcIdrYPmbL2FFkopCzQWPE95dbK7GjYtnRX9pAH0GrsfuNCNjUNW7TZjoFH0uowLBtaAsx7n9B6Dc2LPaar+g7SZpi2actnR7CLCj9D6l2tg0aYtPYfubnqLrMGqw+Z73UKbLTA9JHLY9x2F25Cs5e9u/oae4osO1aYUw9C6n/c4kXaLqvofiUXNCGpoU6AfwbG3FnuWjaH53af5vaUfnfSxYRhGjvW5SD97Fswph2ENW5727TsLnoDsy/MdpSvaGq6GrYX9poroQ2tNNFyH2gBYs2dibSiPzPeQ2hq2I7W59B4l1hcu3M0x+D7VbNESg0KbP2G1oofU096/qKNSAXNCixo/wLHYUfwPyho3P7HpAopinNuWGxhzlz/Z3PYdzzbiLzeDsfMYkjzAk45vp5gZoc4I8wjUfMEnDn8HmCOJqc3o8wGgP0j/mbx5tRwOf8/1ecqaHmA+Z5gIinNsefWf9DzEuk5v55gMQc3sPi/ie40PMB0TnNHaek5tzToR0dXm1L5gIMc3psU3f7PqYXebuQObmx2OgXObuUUHuT/qbX73k7H9zwe40ObQEdWwbW797AurTTzcTYwj2vzmp3vgbG5zZSPeXftV0e83H7jRhY2BtbNMH7l1O0ubza/e6NEYdq6PuND0NLFp7Cn4HkRoh636Ci73GoUtighZo/o2W4RsaNNH9lhT/AEe09KtHoKfvdSGjoUUwp+BqavY3U3r9ZT4upoWA7Q+kPUUBzaWx3NL2rRzaCHY2NgP+7qdxTF7zm8n7DvNrqwj3v8l0Tcw+1727Y9jY+0i6FOhAudzD7CK2drxP2HEdD7A7HQ9rRsf4L/o0/qDxabMKWz/uD2FOxaI0FPNpbHY2NGNPuO17l3Nz6TQ1XYFzY/3bm12BRGj4n/V2B/Zs7XxPqIR7DaWOx1WFP0se0jAo2EKO9+w2mx1LNBH6w2Pg2O47w+ksdrwfuO8O48G7DQ+IbDV2BGn1HxXY7Cz6n+Z6DY6ENT3na97TsdpsSxT/AHO50fef825qB/U7WP7z0u1j6H+zye9p+4dhT6HuWzza3uSLsfrfU7mxGHNtYGrRzZTkwjsPsOwiUw0PQWY/acXm8NEe0pufwNp2MKOb4Gwj/UuHY7j9pc7Fi6Mdqx5u4Lo8259BD+b2hGil5tBT4FjwP5FilhRRz6l5wxwefI+Y01PmB2R5g+YeYXNPmBjR5jLGrGKrSt3vfqdrGKsYqscrd9C/0Id56Tm2nefQ9ybS72tPaf8Ak3m01NB8T4rRQ6EY7Abv9kjGEIjGydh+wCizTBpHYwdWj7jRY6NiJCmERo/YxaaYwGGxKLFMbJ8HsaaYgw2iQsxE2H6yEMAFNjYkEob4PsabkAsl273FH2tMAC7nQs3Gz/MaC5CNyz2Opo7T3MDUHaUjHQsanuKSz2I09roUJdo+opoLsGDT6kWz2r8z3JEaadSxQUx+4jolxjDV9Sv6yiY6RgQ2NhuIuhD9RmwBFrIl2NOgliFL7yiiwwpYmRHa3IbSn6xojREKdGJolOjo+91aYwWDZiWYNyFDsH9RqqqRpjHQpCDYs/FgwFoG5GzDVgUtn4uhSvpaaSMdr9hCiEVBhq6pZ0Io/UwhcaT0MKaIEKLH1i0RhmijsbNyBtKLH6V0JiiMfQXbGmdT3hDQIXRjCFyG1uJB+t3FwKJiixdofcR9BY2kHQjZoKdD4uxjYhdjo3IQs2feR1NGHYU3GEDYfSwLtlsQOJYpg/tKbhsNDYWWn9pYsx2lyMYkf6N2wUbCMe4+DDU8saOgUH53wACjZmx2MD9RRDtIrT6mmjYe47yzduU3PB+5jMxdo7D1PuaI7Ws6mjR3lmMfeOj2m1NDvfiB2F2z4ELlz5yjvbtk1ewuHwbPYXaSJoaJQ0WfoVXVsQYlFNy5tdD3rA70hYoaQIxpIxj7izD1ZpKLNMKYlGiUPvPSrsVwREmGzCF2Z952OikWOhGO0rH0G02tDAilENEhYxqQ+1groUNhjZ1LmMQ/OerOjCxBdhjRhRHYfnOxpu0UINZhYpHLpl+l0fQU3bIkFI05mB+JC6I0Q8DQuRj7i7CntQ9QRI3f4mjA2l0NCzGCf+XY+k783I50cv6xUosQsYCnV0AopufSBHQouXFoix9Kn1l24djCmmMG4WfrSm53FMHsWFh+LtImpS3Y2V7HPxIwu2TQ1WiAF1jTF+GbEPSbSGhGDMxc/UQNGBgs6MC7otEbPzGhYDtWIRoKLCOwo+pIwGPYlDoxsUQ+5gsbNyxSekgH7mNFiMNg7RpKPtcMe0GFNn0NPvP+agWYXdT6GBCHYBZgU0lwgR2P/h7UibAou2SmN2khY+LQU7G7qGqox/cWdoOhEoEp+s7CiNEadAKCh0aICR+52tGiESJZjR/FpEpG5Q0IaOgfaQs92djAuokaP4sxcgkDVsav70KSOoMwMdDENGj9yWdClC7GysGJD9rZirZhqMbN3/c2G1iESmP3vodWLB/aek2vYUx5tD3Ogc3J5x5zcjYc3wgH8gx4l8XPMPAntecS82h5w5c5vRo97z4jm6Judg82sstnU5t5TchRzZzVY9zCjmzpBaI2NGks/re00LhdbBY9J/AU2hRGz2n0npLJGnR0aObMIwKKbNJsP7EENg0URinNoRgtGqUw0PsaTRpESLoDRGJCP7nRopucE/UbhiUQouNOj9Rc0exppO8g2P8AcHY6MEhZu/qPSN8XF0xDQufQ+LYopabOjYifwdptLBR/NLtk2kbqWIi3WH0EbC2G7YKIxg9ro/qSZHtbmhdhRG59ZYjGJDVsRi2IQU0ftKO17WFiJR/N2NzsW6R+xphdjdIFBZsujtPrNpojCmzClbMXsfpNhT6XRaaVhdj8GNniwjo6H3sKDU7DufqPSXOxPQU/U97AjHvQGxCP63UsWLDtSOwpafgj2kNi0xiLHvPg940XLoRufaXfQxINB3EH9zoatFJTCghsHR/i3B0djtYfW6GosNCF2kKbhZsfadi6BtT+L6CPaURs9pT/ABNC7o6LCENh874GiUwdDsKaPrLujowog3abn9ymmNJQJWdGz95cseKaP9nU72mz/Bj4PYwf5mpsLNkSGpT+w7xsLZuXf4neOrTdhHtfinoYex+17H0o6sH73R2GqGxB+Y/1bvYw+0sWNCG1s+g5spT2nqf2Z0dDa2f3vi3Tkv1MdhG5TTZ2kP7nebE0P1lyzQMYR1YbT0vuPBsXCJYs6NP2MLnqTeR+L6Vp8CFn7mh9BdhoGg7T9pcjR3joR+t0YQbJ6CxsIwjDm2FOw7H9hq2fSxhHQ/W7Hg6DH+Z4MKfSw+Y7XwI+kjHV/e+JZpjY/oPY+ouU/rO42MbPg/zfUmpG5/dseo/k6mjDtXVos/yY3fUmjCi5/Rs6ELu0/YbGGx1OxsH7imyFnR7Wz/V8n72xBYbTsP7uiWI3KIXf7m05tB4N3zAgAsU+Yc6AWAgAAAFAd55gbSeYG2vFOfu85E5vj+9/M84Z+LovccDm+Nn97q/5nPUO05vBc5uxq0c3sPY3PMBWTsPa+YDyP5X+T3HA56J/9HmA97c59Duebe+17Dn6G1OfGeYCIvPve95vzzfHQ597z7iPc9zzdzm8naeYCBnN7Yegsc+k7Xm4HpObOd5o/wBTnOPcc3p/iXef62eb20F38h9rzfnxaPQfwaOb+6Mdh4Gjz2XzAQB5vx6nac247Xm/K2HsObo6HNqdj6Hmyuroc/h3HN4PMD4DzEDU8wPwPMCZTnLnOJOf8+YG+HmBLjzoXzAeh8wEdOfa+YD9PmAyD5gIKf3Nhzh3zAfA7HzAYE5wJ5gXyeYCCvN3PMBFzm3vPnfMDkjm9vODObs7HQjD7zzAQd5v5+R5tDHn+mpzd3vPMCbnzB8T/6E=",

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetSolicitudPDF(_pdfFormats.SOLICITUD_CREDITO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Solicitud.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //2
        [Route("[controller]/contratocreditotest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestContratoCredito()
        {
            BpmRequest request = new BpmRequest()
            {
                NroDocumentoCliente = "48761737",
                LugarTransaccion = "Lima",
                NombresCliente = "Juan",
                ApPaternoCliente = "Chavez",
                ApMaternoCliente = "Diaz",
                FechaTransaccion = "30/03/2021",
                DireccionCliente = "Jr. Las Ortigas N° 205 Int 121. Urb. Las Flores de Lima. Distrito Lince",
                TipoDocumentoCliente = "DNI",

                NroDocumentoConyuge = "44257897",
                NombresConyuge = "Liliana",
                ApPaternoConyuge = "Perez",
                ApMaternoConyuge = "Surco",
                TipoDocumentoConyuge = "DNI",

                NombreRepresentante = "Marco Merino Flores",

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaConyuge = "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"

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetContratoPDF(_pdfFormats.CONTRATO_CREDITO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "ContratoMultiproducto.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //3
        [Route("[controller]/cartillatest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestCartillaCuenta()
        {
            BpmRequest request = new BpmRequest()
            {

                LugarTransaccion = "Lima",
                FechaTransaccion = "08/12/2020",

                NombresCliente = "Jesús",
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",

                NroDocumentoCliente = "48761737",

                NombresCliente2 = "Jesús",
                ApPaternoCliente2 = "Diaz",
                ApMaternoCliente2 = "Sanchez",

                NroDocumentoCliente2 = "48761737",

                NombresCliente3 = "Jesús",
                ApPaternoCliente3 = "Diaz",
                ApMaternoCliente3 = "Sanchez",

                NroDocumentoCliente3 = "48761737",

                NombreRepresentante = "Juan Gutierrez Muñoz",

                ImpresionBiometricaCliente = "/6D/qAB6TklTVF9DT00gOQpQSVhfV0lEVEggNTEyClBJWF9IRUlHSFQgNTEyClBJWF9ERVBUSCA4ClBQSSA1MDAKTE9TU1kgMQpDT0xPUlNQQUNFIEdSQVkKQ09NUFJFU1NJT04gV1NRCldTUV9CSVRSQVRFIDAuNzUwMDAw/6QAOgkHAAky0yXNAArg8xmaAQpB7/GaAQuOJ2TNAAvheaMzAAku/1YAAQr5M9MzAQvyhyGaAAomd9oz/6UBhQIALAPXUAIZ1gPXUAIZ1gPXUAIZ1gPXUAIZ1gPlwQIbkgPlGwIbfgPuEgIckQPtOgIceAPiuQIbNQPvKQIcswPvrgIcwwPX9gIZ6gPerwIauQPYnAIZ/gPcWQIacQPrTgIcPQP7VgIeKQPk0wIbdgP0EQIdSgP77AIeOwIa/wIgZgIZpwIeyAIblwIhHAIbLgIgnQIcNwIh3AIafgIfygIciwIiQQIbKQIgmAIb6gIhfwIbTQIgwwIbuAIhQwIbfgIg/gIdjQIjdgIbVwIgzwIcxgIihwPwvwIc5AP26wIdoQP9NAIeYgIbVAIgywIaAgIfNgIZzgIe9wIb5AIheAIarQIgAwIalQIf5gIaRgIfiAIbswIhPgIdHwIi8gIbXgIg1wIaeQIfxQIdVQIjMwIdDAIi2wIdZAIjRQP83AIeWAIlXAIs1AP9owIecAInKgIu/wP1xwIdfgP0vQIdXgIirgIpngIkGgIrUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP+iABEA/wIAAgACVNMEQkUCAAD/pgBuAAABAwEDBgcHCwgQEwkAAAC1AbKztgKxtxESr7C4wQMQE665ursECA8Uaa3ABQYHFRaqq6y8vb8JFxiSk5mevgoOGRoci4yNj5GUlZyhwsQLDA0dHh8ig4WHiYqOmKOnqKnDkJaXm52foKTG/6MAAwDffff09Pp9Px/H6b77777777/y3/6/6/n/AD/lvvvvvvv6fyt/t/D9v6v4/Ttvvvvvvv1+/wDb+39n+H29Pnvvvvvv6dPt/f8Ad9v/AG+6fTffffff02/X9/3f6f5/u1/Hffffff0x/f8A5fp/X+r7v9/p6b7777+nX/H9P7P1/b/7/j9PTfffff0+X/j7ft/d93+v8Px9N9999/T5/wDr/wA/u/7/AOv7/wD7233333336/H+P8P4fh+H4fP0333333+XP/b4fh933/8APz333333/l16/H3/AIff9/v+m++++++/z+l/+P8A5/v8F9N999999+3b6Y22339N9999999999/Tf09N999/7p014N9HXrDRTDbPWV0HFDve1RNV7JoYAOvsh86cDr312hHgns7dkLFj11WlPV2nrrHxRIj2YeivFfb2UzWtpNu/4Aofnijbn1dK/H0/n/8AvxVd+mlY2qd5fo5ad8Wor9K75FFtVd5tRq6699arZOP6sTXbaXPYYDWnK72oac53ibxOdz2SFa7VXeC2OfTp31SwE/pk/O6hYZPZVxN6/ViO8b/JFsaf0PCoJ37Dm0U79lqtoz6mNPYnrixetUYnsoV1hn1XUoh9TR2s/wAkED79lP6p5PJ3BnXmgjsCwooflPPqykBaGFDwuWjTE6nJ60sUhU5i6nG/vTpSzUNSeA7puxKmoyZfBNS7uoiVONaFr1WB21JxrSKcCXkW04y99FShImicTVyWqKCNZxmsx0GKpeK5xnm9jExSJgVcb6q9o8S92yuOrjko2wq1aDw1wbPmBrJGjwNKa567WJcl68DXF3rcuwbjPAvWpqhyJsQflGg2UhMC1k8C/AZQqV+B+UaEFiBwngTgRBOBhHgV65Y8argS0tPK3kkkcnIqHTrKCsIzBuqOlLGxiQzGyItcRlCqkwVZ0zKamwi1OlM8alFKkKXmZKMUgwCnIvLUpbDMyKpLvWo4ZF5dY7gvKzu9Rqrmt9HzrKopzjb15rneZPr86jQpUg8q+RQK7WL2M6qPRTnaMDLV7R69NrlVDiW1tTVPVMyi5u2BDhrjHGTBUaMYmDOIpAIWrV+UAxzW1GNLvk4prVHS8ucg10MR6yhHEg9XXaPmdMZ1wirGNj3T+yeV8eraNysWmQh1te+kqMMoEihVrzlVVD16NTIwYCSW0fKjEU4TVzlL1FFS1VUpkDg1GjptMnJV7MaCZ1ahRCuXvfIUmJa7kvXTPETilq2XSZ3FLyJvbGL50M1aqtbk1zqWGUsRX5YvT6Rtq/CcRayleFm8TMAKFLRScZKMWlopM5MApEa3xt4uQB2JUylpK7hNbeMHaScJnERRF6q0+uT7I/L9oVcKkijE8oWLQBRTjrreEBSKjN1O4QicpEIVCkUzwAopFSkzJfBWBUrsU44Aq8ERAZU6IODiqcqvXCOsKkZ7c4IscGZ1ppTVxUWmUNXKkU1lWnG5pelJFhA5x0WqohQYlTXi0VqVMSalp4EixZpmrMXngWk2WJTaL1PA7uiaqEFeeAoKRXRI54x6CLDV1PA4MQp3VnPUKWlGcliCf1ndLHl7sTKnqp5gCbJIbVm4qQpHKkU4u2KaKQLrnGw4SQHM9dkAFoci6ikk7mjLVOHSKUoZi6rAxUmnUyi1dFir4inGXvcYxRQzmt+v/GO66LTjH71RESgRlOvbq1E+6I4vGlUx6VdUSPwCmpOa7QwocZNcD7P4AqRk1SnNt0to9TkbSWupbGTVJ2g+6eNUIgHjLNFKNQk9oKFUh1OMLMdofmJiCEeWrO4tIeJShWw3KM45dFndGJyJJaIGTMRgFJKEZuKKSWljOFWJwJIjjTgroq0oykaYItRtjOh0ohRlXM6KctFpdR5MqKlUNudw5UOq4dIipxFO3nOEBx5BOnwd5KUUK4mo7SIhGHGIFKRGqPcEkVNG2UtTiIipJyiB0pCKcQUQoIcVScRRlgtCfWKKIkPdIixJnO4sf1zy3CewM5aeopVEZQIIWEKvGJ0iVVV4wQoeqjlK4FjgZjwoqswUzgWNgEPMvZl0JrFpxJHweI0OGVLrWsWIoIymxFJVNS0ykw7qHdLHEV+t7U3FJUyrHn2QSFqA8DXsyzJaGcl7kHKqU84or2gIcRxksNSDlM5UCgUqkk4wsIiCOop4ThU7nCvzCESme5iFET2iFD3HrnleFpJJOUl5evjfnz0052kyO17mGSuBOMWiBx2kzkkrR2lLCPElCr1EzwS/dTkrMhsIE7oPEnORQcUzvauy2dhpmWPkLQRhaxysqhrdrVzXS7eeOirpYo2m2pl0vFUa14Ghk7xmk12om3XOlJ0xXR0jTbIvjVfK/Ppi+LmmbWHf51Lq9tp4E2Qnzv2+ldJroZEl5t219/PzUmd+nTrWvmvl1+bkzOej01+fy9+NPhmW6VOfb6fDbXz67ZS0fPpsvj58/kao8Qw1Wyc5HCqKj9tdwjyvT80tIsaXZ1rgcSSY87mOmvxifEjtMYx0muvKPBaPXnKrHjC6hWxqVnNL1wj5yu2JlOmNaJVbVt1JyZBXcRfS+RSiuKSPnzrlFsIaxNdtsoChGKu3y665vGyk1hfHyfwOIOqvzpTz0116YyHMdeyc27YfScZVq+S0TmjqnkNVdixVneuIYvJUiiqVMpcmLSosK5xJh1BVwCZBpV064YUmcpqoqjFPbC9SS0IchBBRAeMURBz2licCf2nlryMfdJ7a221cu7xlp4Q5306XMIKnKbfT+eLrHADOuc10YgmzMhxfPtJE6imRMfH8ejqBUnjpPPzkHYq6OMi1VV3VYyrsroLottXKryNS7cnIhNOmynPXm0ZE5q6uhVQPJ6O9huVfDzKTV6ve9Ver8YpRNdta6Ptpg5RbR1jTzXn8+UpaF/izp2+nyxxiilQr4Oq56KeAGnzTnTpi+vwhyxojE58+p1xmK+3OF1j3kZxlqnWrv311v52njpJKrUx9ZUWiwpyfCRRSe08sMTiK4HEhCTba0Uk4tFLbHvveQFMtqh59p79b87mcw49bvFGFWdTzF5nXGxK5U+lvh53von4xU9tZjBjtRyk7TrswNWplEF72wsY2V+V5fSSzvORhuUnJYh4kXXZSo6Rmyz89emFr/wAvMCVj5Udfl9NnnG9pNJd/CjMDp1DhO2pMovj56xXlFS0yrExUTs4nnIcCkWhOMtfCkkVypkWogPHPAj2xAalwJyCqY3PbOBE7TxD1inlrCklCih4hRV7yICZRHAVUtfEndIjCnIALVI0+URwd1JdmdUqtVF1tVZxHCOI02qswwS1O/a95k5ha0mi/xes5F05Wp1UDlEVKWvy61RnVppLj02LucSINZalOUQtj1OYUMjXbFKKkRScYOBFFKtjKKsK5JEWrk/UyWJyDgRCYciRSCuz3FhCCx7imN8J9kBByk7gnl+nCoe52ALSZyxwBD8Rqepwp/WWERPOJt2pjljOAKoXLHKd1Ym1oTPFwlXwDIZ1bCvtTmA5DhYu4yTOSFjWsS05UpsgZBVyg6iEBMx0RSiItMynUnAUpGcqO0ZKT5FopFUklchUz1VMoi0UUKWx4lg7oZncQ4RHtlKRolq+sgp7j1nl9Sx9gnwJGSInJqHSMup4zu53JWKtOMida37dey+JMzDtG/h8fjV3nObDRytsbIzmOlunXXHPpz1YZXWAdLHnOimY2tMKCGGQQOimNHWJMgs8YnDBjlSdhX20veGU4Shdq2+dKuQoSF+eiqs4oYkUtEZSm1LRSpyL6UFgVJmdddrEUgmZxzgHADjEWbtLQgZsREEU+yRMh9YThKTiPEtD9E7jy3yeLsKRzF0chUIixezEZSwigLVyHYFFJSmbdWNaVX6PxGpHhkZ43qgRtcqoZ4eB0tr1znJ1RBTn1sQzFSjw3aTKThRJ0iZOUTjieKMIypkIhlIuZRN3iJi0MpwvEQjT8XSBCAqZyWkT4EfixFWIKs4IQiIdGRYoTliI8aTCcGp7YCdj7ICEEnKd0R+iWPJv/AP+mAHQBAAIBAQQCCggLCxcHCgcAALO1AQIDBLK2BQYHCBITFBUWabG3CQoLDRARF2oMDg8YGRpKS05RuEhJTE1PUFRVVmCwGxwdHh9FRkdSU1dYWVpbXF1eYWJjZK8hIiQoQl+5ICUnKjVAQUSuuiMsLzE2P63/owADAecRmxBXQPU0YNjuYUQrM6G5uxpzDgozMZkN6QYMIO5zMilEcQ8UpGILuzTGmh4PSMC6niJ/6rDj1/4nUwXcR3D0IQTlk/C2Lm96wHp1xRQeOZ0x06z8MXDwZil6Y/CFjdiZhCYes6HirGY6YrHHMejnGenU5YSxSPi0IGEjB4HaeAU0fmaOfyDQ3ParHeJZYY4ZsUiHJIFMPWuaXgFwYLvc4g6GHxFzcyVngEaxEnXc4xMxhFjHemaYzPAFgetoYMbI78dSK4InBOgOBfWFMLG9wRCJQbiEKT19YDgu8TC4gpuNcscnkzEXQ8C4ufyMOe+eRRiAU2dwxgw6rvDUQeChdmIblHCDHipG+MvAizo0QhvDQmcDvMBMYRjw/wDUyHTLY3oqmQjDxFfwpIRHcdHJAIOcHgHy6UosafB6EbKZgviDgtjEcO/GWYbJl8ShixMDxzjJCgmHeXAhHkGYkXyazHDHc3Sj8Zz/AIuntQo/EfiabLwGEWsVjdihiQsbhoYJGJ4uo2N5jQEscCyHtLJMnAymKDA4dyTMKITBubN2ZN+OoUK2PFwzDGz5BTYycWi7HewiXxHPEIRix4CRzR5YHRp4JEpo4thX8r+R5+7sWGODZ2LuLECzybBCMIeJAgXc8WmEDggMQjA3ZaYRDioUnS2DeWcsKXcwFopMG5pAbu4M5gNgp9TYGgjwJil6OSMHwMUYmGMQ3lOWCvAIBG4+LTboYFo8CEew4N2A7myVgI2ODGswj6yye1Ofwdh7S5+Y4ly7hDgwhGEx6y6nrIwCG8p0Xe0Nmw7yiEKTiQiMMU7mJ1IUm8Z0buh4EGZjRRuGNiGGB4lsGw4sZhRE8SxWIJF4FEX1lwMj5NkjjeaOjyaCL7Szj8Rz2XteBG5/8PsabJxULFO8Gzm2Xc2WK9adzErDFXegNdY9civgzr1dcHFchFgcMw65Ixo4PTGBay8sGCNiG9gRxHIb0xZBKDxSwkYsfEpZiEeWbo3yeAIYMoXeLFohje2GFEHiVlsespaOT/kc/wBbP/6WH2rH/s3bLH1sI2dxDR5NDtN4uGFgjuKKYetcupHflYEbu92FCb+pRQ/lKHxY4GZGExwVoobPBixoXyNGBvIQ06exH8hc9pTz/ROLQ6I+TRBI7kCyTJnktkWO9TXFO9ixoyU7koMjF4MwCUq7xiE6uIbyKB1oeOIjfMThnqUxLYfAY3CEYbkXNkp3qFIMOGcQFjS+JoF2jwYqxEVd6MQacnAhCEIcWiYjQnAiwjH2PrOw5/xR/wBnTNg4pGhTgwuweBM0QLBvQjBLO4EUbYXe2JisWdxRoTPkxKQOLbJ60bC2Nxl64s00eKvSxZxwIwstC+JGKzNHJrG18Cls00eLArL+MKLBwZmJZDisdX2D5F2x5gI2INZ4qWy2OBRBROCOKLGfluZlikxCY9eJhmadxGLgSx4thTGFwG8gzM6LMcCJmnCY3tmhyTHB6kCEaN5SAjHctCxiWzwTQseDBGBo701Sz4tJDV3DSGWJvaw6seQEEeDq50eSFD7XzASMhvLlkmOJEIRM8CFsqmDcXYKLwI2YMeKRbnAuEOtne2M0uCPDNlHExwCDCMzwxFhAo9pkscVLkB8QCFBiKbghM658SxMQA5ZTUi8CMBzgg72zYsbxYsA9ikI+SXBj7GP5HnsO0i+0svtWleBQ3yHFpVmA5LEhyWi7yLrYOTZGscCmZoZg5FEHMDfiAWMw3iKwQp35zcaVPE6aFNjxVgWyqcFpzHKu8wKNk4KVlzTyCwzMMciINHrFfxJTTHc3YL+N/K+YCOtMf8nD+MfxlD7QscCFEYFG9HUfWQLu8KbHkMWJHyYkGEeKkwrGJvQpoj5DBX8Q6PEWw0HAhY0fJsR8miH4z/Qh5HtdTzASBeLTcj7FwXOLQKsHgEAPWUYgKR4NmEeK3MGCHDK2bPJzY0NwDTCHFIRsQmeA/LKTLHgGMMTEVj4JnVgsNy5cFEY+BMpYCHtyhD8SmYnFVsAu8ItZSHLOZiiLyIwX8z7Q5/J2n/4aHsdHydFCZjwbEaDiO1d6NAWA3ugx4JCnExTDcxuUU72i2YtG5jZbJwIMQION4t2jkDCmI+DAUgw9eGzGh5EIxDg6Dd5FkXi2S7+Np9rz91gRz165y2fHPVIRs7wCGcTEOAM/99IjnkfJ+RoY3s/4/D5DFIviVj5ZonXkOM/IrNl5YcmmHxflOnURKIeBl69UALG4nSKdOmMYxjfjPybJTvOhYHIvTcqvynTrOpxIrkmcvyxH1MwmZnGX/D/jc0QxmdPw/D8Iu/P+JmdMdc5+Ud/ToMzOh0wb3OWkz8s7wrHSsfLpWDg5XGH5ewzCKQOTQ55Bc57ucEWFO56pF0XxGnoZIpHxxEfkKYXeU9chAo3dcP8AjP8A7/469flld/SPSzOnTgv/ABmfgQwdcpuzmdSEc058SjGZmxDHidUcU2Y7ugYMYQJ06bsu3Lnr4nSgMGOnT/HBSydfl8n8N4zAW69M/wCM+OW4McE6+LDFFjDxx0xCGmN6WYmX15rNf4yPBjEzOuHixYwXeEbIkeDQvsMbHneAAHBmClzHk/8AGejhYR45w02eLM2Ix4kSx5Ey2yGDe2czICjvTBZYTry/DGY+TnrYskdwQMTB0DDvIZVbZeOIpBgG5hHDEDDuwHyWsJB8WLAhmM6nl0yh0xwIQI4j1Dc5rKB//YY7zJDHyzky7wGs4jk68FBSf4HiDbo5yY5EAhxb5ZjnuPsaJmF3jmdDGYsxTuzS5cU8CkUFjw6wYZgeWK6pgcu5hYmbu/AuYxsblI5IsOTscrvyEKaOTHM6URmN7ZBhiPtzZh4LTWEmYG9IZmcuHiKDSkN+KQbEN7GkoD2Inyp3lIRwmeJfEMdT1lGMnsGlPWK+t59C8mGSiPEunSsWPAot+GCGeQT8JnrEzuaIlNYx4pGGA6QM43uMTDR1eAQixj0Ibi4mTqjuK6asycCCzAQzvWsuFwYNzC2MdVnR3qqCvXPBMEZhgj4Geq7cG46BG48mmyDybivIRrLY4BMNOT1rqHFKGPE7Dn9kZh9r1Pw+WOKLfrhd6MTNDDg0iNOHcMJiYZ8umdwlNZDDk3MEMdcZ6T8ODCDnJMLvLIZzjq7yhMuMgZ4IApl64dxbqiOeibyEIVhOCR6kw0vDrM0zMdxMU2c0Z8cQQHQ4saFhRvw2YGeTWWkhuKUITFPJaaDyQopN7TQQ5/REpKdwAxMljc0ZSEOARsTDDkQVFd+aBKeRTWZkjvBjQGSG9ikcNByVguMG5aUsZjuKFZnC5N5oMCneiuILE5DYIx4OIkIw4ESm7xcjCPJskUzxex8iiDF8mI+tjGBd5FFHP/fxFsZOQxx0fJ0OuMvALJTnDuGFmMyx8TMzRHr1gpuMUUx5ARWMeJOkHNY6dTjjKwJk3lZikcxPEbYpgdcby2fkQepnk2KzkTwLoYY2PA2sTe1m+S54tyJYYbizCOSPka5OQxoY+LYijGDwGJQsxuNAse15/JTMY6dOmMcjMSw2fAzOpWIrxcGSn2FlaXgmY0uXgtxejmncRwnVpHc4mYBWVdwKjMiu9aQhQdOBM0kFhudG4bhgwHDB3MJmimk3BGiFGXk4PYJDEzQuPBg4Ry9CBje4gTPTD1OCYCukc7mzWcwIp4NJMZoxTxDR4tgpfxFPPZazQTNG5i2YWdzHMXI8skwls8AgsCsHAzgoI45DgXEyPDDkzWVg7jEzCNsdOLmAxhyJjFKkc+LMOWkLY4ubNENxGDHJXTyMOUenJZghnPWZ3i0XzM7muixoxnHizpjEMQiRdxGgbHIhMZjMbmnKNgwcGZgr0fY0YH1h5gICB7CYhQUH/wB9WjV4MzGzHgWCw8WJGOaDgXCsWN2WMAAzHxSwXwcGmjQ4EwwytdeDYgUeQwzBicjLAicjVHcaFGDIcCs02N7hosTL4lsXbEdzjLTGHJKKEeJGxd4jonkQpORdiPP5bOpuNCPIs2IHJppsQ3YM5oH1uIMaOBiZAFQ4DoRI+JQAtD5H5GxdV3tmmYjvbNHrLmq71sFDvLmo8RhqG4w0Ux9jDEJg4OSnMwexYtOPZmxZ4AMQ/In4iHPgBOJWGIZ8iEaccGA0xhxLYsRhvcOGkIcTILFYbhwXVxngwMlAOOWGyYhvYUkQgeLQ3YcimmzwYkW4u5ocgQCO9jCkE3Ec0RIO/DRTCMdw7GAHArpGMOQ3zCPkQKPIhmmjksfMBHiiP48GfJGdSsvEcVgz8p0TgUxz8npxy2wdJ+BDe9WskDoUbjAbENxTmYprDvzM6JTwBsOXfhtimk3mgQ5NlIKY4EQGsYhuCBjFNYN+Z0mCMyQ3Hypxi7x6xzFhQPgbSZpdzWLohwZizYfITI08ksKby7D1nmAgZoezMX2FYnUhyV1OKMMRjDkQIJF4BBGxHeR0aXyLvFWxd4tgY1nO9iGoexjZdzdAp5MIRHPAp0QNzTDRhvGxq/jfWjGnybMfxl0PY3PyHmAjLq/mPJjb5GfIgdIWeBkZ+BRCncN1R4CU3fW2Up5AamdxZSxHi0Fld4QbDQ7gNFmOI7Vj7Eg7mmxcTkXYwOQxGDwEuTKcMXVhvKUgWZjgEbFPJ1I8gi+YPXj+U734n/IjHYNjtP8AdLFnQ+x8n0lFn6ze6OjEbpGPNmdpd0GCXO5/qaBTCAw+JvNBglFMGiP0EeDGjU1GIzNPNpYJYY07X+6MbtyI4Qp/c7SIwuiJkJhjqe5udg7GxcbERKI1ij4niMaRGOMwSGr95EmYIUWMwuQ/eOxRpSJnCJCZxThYQ97q9g64awlOxEu/YnpbAUWLtMKT7WZhTqOgMYiJRGz9rR2rowSOKKYUglD8G5tGKUWUTJYusLJ9aFzsaLYzRTMxPsdGkjsREgZSMRLpRqfQiJYjZsjCBZwkazZs2H3NizsLMI4YEMwcWbGdr8GMPQFNBSTJTBw6P6k2tFIipgoxGYMnaFPuRppdCmsggUwjBg4LtP6W5BsQg3bC4EKYU4jFpWJ9DSDY0NCOcWKQWDTTZNH87BDMCHYMxWbhEMuKw0WI/BhCMHVg0RsEUGGSNDQe9ovmYYlzNlwAwCxmkW2fgDSsI6Fw0CiEQbv1FZoCNjJYozAGzRTREaFue8IWYJqg0URjMTKwiI0WH5m7opm5mAwxQRQ0xqwPgN2EyaDRZUNGGaV0X6igilMErLrmAwzRZCn6W7HQiUzBGmgQcwSFikgfA1abEaMaNAMGCQaLvzkIRKbiQuaEbKYhcu+40dXRjDCXKbkdWFj6CELOg9rAG5SUwiUMfpOxIU2EiwsZoTtP3sewNQsRU0Y+9p2kaKBhGYoGBGGw0PnLNylxWaNFSJhCBYYxhR+gstFzZhNSMGYmYMGsRoofcLc2sGizhazgGCWLmg/YZSCQSwgjYhls6B8BuWI0MbNOrSxopiie87mmkopomSECMzGIe8uwu0GwS52I7EKfrA0IO1s2Y4pjZFpufOAwppHYdjEg6ZhojH9L2DZLsbuxgjCyRufSUO4IRZnwD3hdpo7XQIF1Y6ie4ihoNgIwp7WjQdp7g2Nm4FnaupdpPebUp0LPAgtKfWR1ItN2NNnRo/YaJYos0NMbtI6ET4JdYUNMKdG400li5B+hHRs009zTCNFNMP0g6MKGEbrRCz2lOxsfnbsE0bCbG5qMKY0w+ppGPbi7YsLGmhpIlHwSi7EjMdoGxKDYfSurTCjYzO1LMI0n6H0JRRRTFCPcNECNH1EaCBCjYQ0YNhbMw0sPndXa6lmixZSy6n0ELqwdgWW4dhFs/FIelhc1KbNiJ+h7gh4tFyCWbIR+l7TuLLZoGDgbFP1NlphD1MW7oWf7sTvYbD4mhouoHoLEWCfMeh0CliwohdujCDTD7TQLCEKI6BCNgmfc+gsCwhRTqsFY6v7GFnU7mFy4x/mU2brZpBbmh8x2jddpcbigUUsLL+k9LZrFzQ0KIwYOh9wU2I0XGmlp2MPsfSwzsImpGy/cbM6MLl3UI2Ian6D0pQQpsU0Me0/Y2FLOpSqx0P7Nku06Ctjm4sWiH8Hc7Gjm5ng83Be97T4MTiR0PMHvf/+jAAMB8wfQDvP9j/oav1n/AMnxf9Q3n+x/qU6G05thsXuObK94c4o/abzR+o/0ebq+RzbnQKf3n5nRoDm0tg0XsI82l2OwuvuPzOxdrY5uroBHm7BYwfrd7o7GnUPnfzFNHcvxf8jvfoPFdpo3O4P7Hc0bG5873sNWxZ/q8SxZdgbCLA+BHYbw2NiMfe7D1Fil0bFGw960tjcR1ItOr+1uXdCNLZs7T6zcbAu3bHpP+5vNrA0blMNGPzLcpbmrtNF7GETQo/8AC6MLA6ujZ0dBdr72gooI7A1Tubn6mNzuCNmGrT6F+tuWfW82h2ugXSi2Wn9ZGGq0UeLcs/SrRqbCweLo+5swodCL3FLuX3sKIxjZuFEF7HaWYfMGova3YFg2HcfMw2LowsUUNmnYWfgwGiKws3UsujCzY1PpbCxVojwLNml+owWWzZ9GeT87QEdCgo7Q0PF+4COqwpdhZ2MX9LZpjfNPaxh2Aurd+dtmxTRq3Xvdr7ywQAoKCxcj5PzmxhRYo1YbmP0EL5W6w7GzRsf3BEYUxs/jaQi/QQMMCOh2NwsWdClfews6JYhqvBuWP0K2VhQdzTcgRWLcNT/Y0aKI+hojRxPndDtdBVpg3IpZ2H6DRs3KLr3B/J7QhDYvoSwbCHxLkaDse41foewp7QIEKKNobV+kNQLhCYoxcIdrYPmbL2FFkopCzQWPE95dbK7GjYtnRX9pAH0GrsfuNCNjUNW7TZjoFH0uowLBtaAsx7n9B6Dc2LPaar+g7SZpi2actnR7CLCj9D6l2tg0aYtPYfubnqLrMGqw+Z73UKbLTA9JHLY9x2F25Cs5e9u/oae4osO1aYUw9C6n/c4kXaLqvofiUXNCGpoU6AfwbG3FnuWjaH53af5vaUfnfSxYRhGjvW5SD97Fswph2ENW5727TsLnoDsy/MdpSvaGq6GrYX9poroQ2tNNFyH2gBYs2dibSiPzPeQ2hq2I7W59B4l1hcu3M0x+D7VbNESg0KbP2G1oofU096/qKNSAXNCixo/wLHYUfwPyho3P7HpAopinNuWGxhzlz/Z3PYdzzbiLzeDsfMYkjzAk45vp5gZoc4I8wjUfMEnDn8HmCOJqc3o8wGgP0j/mbx5tRwOf8/1ecqaHmA+Z5gIinNsefWf9DzEuk5v55gMQc3sPi/ie40PMB0TnNHaek5tzToR0dXm1L5gIMc3psU3f7PqYXebuQObmx2OgXObuUUHuT/qbX73k7H9zwe40ObQEdWwbW797AurTTzcTYwj2vzmp3vgbG5zZSPeXftV0e83H7jRhY2BtbNMH7l1O0ubza/e6NEYdq6PuND0NLFp7Cn4HkRoh636Ci73GoUtighZo/o2W4RsaNNH9lhT/AEe09KtHoKfvdSGjoUUwp+BqavY3U3r9ZT4upoWA7Q+kPUUBzaWx3NL2rRzaCHY2NgP+7qdxTF7zm8n7DvNrqwj3v8l0Tcw+1727Y9jY+0i6FOhAudzD7CK2drxP2HEdD7A7HQ9rRsf4L/o0/qDxabMKWz/uD2FOxaI0FPNpbHY2NGNPuO17l3Nz6TQ1XYFzY/3bm12BRGj4n/V2B/Zs7XxPqIR7DaWOx1WFP0se0jAo2EKO9+w2mx1LNBH6w2Pg2O47w+ksdrwfuO8O48G7DQ+IbDV2BGn1HxXY7Cz6n+Z6DY6ENT3na97TsdpsSxT/AHO50fef825qB/U7WP7z0u1j6H+zye9p+4dhT6HuWzza3uSLsfrfU7mxGHNtYGrRzZTkwjsPsOwiUw0PQWY/acXm8NEe0pufwNp2MKOb4Gwj/UuHY7j9pc7Fi6Mdqx5u4Lo8259BD+b2hGil5tBT4FjwP5FilhRRz6l5wxwefI+Y01PmB2R5g+YeYXNPmBjR5jLGrGKrSt3vfqdrGKsYqscrd9C/0Id56Tm2nefQ9ybS72tPaf8Ak3m01NB8T4rRQ6EY7Abv9kjGEIjGydh+wCizTBpHYwdWj7jRY6NiJCmERo/YxaaYwGGxKLFMbJ8HsaaYgw2iQsxE2H6yEMAFNjYkEob4PsabkAsl273FH2tMAC7nQs3Gz/MaC5CNyz2Opo7T3MDUHaUjHQsanuKSz2I09roUJdo+opoLsGDT6kWz2r8z3JEaadSxQUx+4jolxjDV9Sv6yiY6RgQ2NhuIuhD9RmwBFrIl2NOgliFL7yiiwwpYmRHa3IbSn6xojREKdGJolOjo+91aYwWDZiWYNyFDsH9RqqqRpjHQpCDYs/FgwFoG5GzDVgUtn4uhSvpaaSMdr9hCiEVBhq6pZ0Io/UwhcaT0MKaIEKLH1i0RhmijsbNyBtKLH6V0JiiMfQXbGmdT3hDQIXRjCFyG1uJB+t3FwKJiixdofcR9BY2kHQjZoKdD4uxjYhdjo3IQs2feR1NGHYU3GEDYfSwLtlsQOJYpg/tKbhsNDYWWn9pYsx2lyMYkf6N2wUbCMe4+DDU8saOgUH53wACjZmx2MD9RRDtIrT6mmjYe47yzduU3PB+5jMxdo7D1PuaI7Ws6mjR3lmMfeOj2m1NDvfiB2F2z4ELlz5yjvbtk1ewuHwbPYXaSJoaJQ0WfoVXVsQYlFNy5tdD3rA70hYoaQIxpIxj7izD1ZpKLNMKYlGiUPvPSrsVwREmGzCF2Z952OikWOhGO0rH0G02tDAilENEhYxqQ+1groUNhjZ1LmMQ/OerOjCxBdhjRhRHYfnOxpu0UINZhYpHLpl+l0fQU3bIkFI05mB+JC6I0Q8DQuRj7i7CntQ9QRI3f4mjA2l0NCzGCf+XY+k783I50cv6xUosQsYCnV0AopufSBHQouXFoix9Kn1l24djCmmMG4WfrSm53FMHsWFh+LtImpS3Y2V7HPxIwu2TQ1WiAF1jTF+GbEPSbSGhGDMxc/UQNGBgs6MC7otEbPzGhYDtWIRoKLCOwo+pIwGPYlDoxsUQ+5gsbNyxSekgH7mNFiMNg7RpKPtcMe0GFNn0NPvP+agWYXdT6GBCHYBZgU0lwgR2P/h7UibAou2SmN2khY+LQU7G7qGqox/cWdoOhEoEp+s7CiNEadAKCh0aICR+52tGiESJZjR/FpEpG5Q0IaOgfaQs92djAuokaP4sxcgkDVsav70KSOoMwMdDENGj9yWdClC7GysGJD9rZirZhqMbN3/c2G1iESmP3vodWLB/aek2vYUx5tD3Ogc3J5x5zcjYc3wgH8gx4l8XPMPAntecS82h5w5c5vRo97z4jm6Judg82sstnU5t5TchRzZzVY9zCjmzpBaI2NGks/re00LhdbBY9J/AU2hRGz2n0npLJGnR0aObMIwKKbNJsP7EENg0URinNoRgtGqUw0PsaTRpESLoDRGJCP7nRopucE/UbhiUQouNOj9Rc0exppO8g2P8AcHY6MEhZu/qPSN8XF0xDQufQ+LYopabOjYifwdptLBR/NLtk2kbqWIi3WH0EbC2G7YKIxg9ro/qSZHtbmhdhRG59ZYjGJDVsRi2IQU0ftKO17WFiJR/N2NzsW6R+xphdjdIFBZsujtPrNpojCmzClbMXsfpNhT6XRaaVhdj8GNniwjo6H3sKDU7DufqPSXOxPQU/U97AjHvQGxCP63UsWLDtSOwpafgj2kNi0xiLHvPg940XLoRufaXfQxINB3EH9zoatFJTCghsHR/i3B0djtYfW6GosNCF2kKbhZsfadi6BtT+L6CPaURs9pT/ABNC7o6LCENh874GiUwdDsKaPrLujowog3abn9ymmNJQJWdGz95cseKaP9nU72mz/Bj4PYwf5mpsLNkSGpT+w7xsLZuXf4neOrTdhHtfinoYex+17H0o6sH73R2GqGxB+Y/1bvYw+0sWNCG1s+g5spT2nqf2Z0dDa2f3vi3Tkv1MdhG5TTZ2kP7nebE0P1lyzQMYR1YbT0vuPBsXCJYs6NP2MLnqTeR+L6Vp8CFn7mh9BdhoGg7T9pcjR3joR+t0YQbJ6CxsIwjDm2FOw7H9hq2fSxhHQ/W7Hg6DH+Z4MKfSw+Y7XwI+kjHV/e+JZpjY/oPY+ouU/rO42MbPg/zfUmpG5/dseo/k6mjDtXVos/yY3fUmjCi5/Rs6ELu0/YbGGx1OxsH7imyFnR7Wz/V8n72xBYbTsP7uiWI3KIXf7m05tB4N3zAgAsU+Yc6AWAgAAAFAd55gbSeYG2vFOfu85E5vj+9/M84Z+LovccDm+Nn97q/5nPUO05vBc5uxq0c3sPY3PMBWTsPa+YDyP5X+T3HA56J/9HmA97c59Duebe+17Dn6G1OfGeYCIvPve95vzzfHQ597z7iPc9zzdzm8naeYCBnN7Yegsc+k7Xm4HpObOd5o/wBTnOPcc3p/iXef62eb20F38h9rzfnxaPQfwaOb+6Mdh4Gjz2XzAQB5vx6nac247Xm/K2HsObo6HNqdj6Hmyuroc/h3HN4PMD4DzEDU8wPwPMCZTnLnOJOf8+YG+HmBLjzoXzAeh8wEdOfa+YD9PmAyD5gIKf3Nhzh3zAfA7HzAYE5wJ5gXyeYCCvN3PMBFzm3vPnfMDkjm9vODObs7HQjD7zzAQd5v5+R5tDHn+mpzd3vPMCbnzB8T/6E=",
                ImpresionBiometricaCliente2 = "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",
                ImpresionBiometricaCliente3 = "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",
                ImpresionBiometricaRepresentante = "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"


            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetCartillaPDF(_pdfFormats.CARTILLA_CUENTA, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Cartilla.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

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
                //ImpresionBiometricaConyuge = "/6D/qAB6TklTVF9DT00gOQpQSVhfV0lEVEggNTEyClBJWF9IRUlHSFQgNTEyClBJWF9ERVBUSCA4ClBQSSA1MDAKTE9TU1kgMQpDT0xPUlNQQUNFIEdSQVkKQ09NUFJFU1NJT04gV1NRCldTUV9CSVRSQVRFIDAuNzUwMDAw/6QAOgkHAAky0yXNAArg8xmaAQpB7/GaAQuOJ2TNAAvheaMzAAku/1YAAQr5M9MzAQvyhyGaAAomd9oz/6UBhQIALAPXUAIZ1gPXUAIZ1gPXUAIZ1gPXUAIZ1gPlwQIbkgPlGwIbfgPuEgIckQPtOgIceAPiuQIbNQPvKQIcswPvrgIcwwPX9gIZ6gPerwIauQPYnAIZ/gPcWQIacQPrTgIcPQP7VgIeKQPk0wIbdgP0EQIdSgP77AIeOwIa/wIgZgIZpwIeyAIblwIhHAIbLgIgnQIcNwIh3AIafgIfygIciwIiQQIbKQIgmAIb6gIhfwIbTQIgwwIbuAIhQwIbfgIg/gIdjQIjdgIbVwIgzwIcxgIihwPwvwIc5AP26wIdoQP9NAIeYgIbVAIgywIaAgIfNgIZzgIe9wIb5AIheAIarQIgAwIalQIf5gIaRgIfiAIbswIhPgIdHwIi8gIbXgIg1wIaeQIfxQIdVQIjMwIdDAIi2wIdZAIjRQP83AIeWAIlXAIs1AP9owIecAInKgIu/wP1xwIdfgP0vQIdXgIirgIpngIkGgIrUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP+iABEA/wIAAgACVNMEQkUCAAD/pgBuAAABAwEDBgcHCwgQEwkAAAC1AbKztgKxtxESr7C4wQMQE665ursECA8Uaa3ABQYHFRaqq6y8vb8JFxiSk5mevgoOGRoci4yNj5GUlZyhwsQLDA0dHh8ig4WHiYqOmKOnqKnDkJaXm52foKTG/6MAAwDffff09Pp9Px/H6b77777777/y3/6/6/n/AD/lvvvvvvv6fyt/t/D9v6v4/Ttvvvvvvv1+/wDb+39n+H29Pnvvvvvv6dPt/f8Ad9v/AG+6fTffffff02/X9/3f6f5/u1/Hffffff0x/f8A5fp/X+r7v9/p6b7777+nX/H9P7P1/b/7/j9PTfffff0+X/j7ft/d93+v8Px9N9999/T5/wDr/wA/u/7/AOv7/wD7233333336/H+P8P4fh+H4fP0333333+XP/b4fh933/8APz333333/l16/H3/AIff9/v+m++++++/z+l/+P8A5/v8F9N999999+3b6Y22339N9999999999/Tf09N999/7p014N9HXrDRTDbPWV0HFDve1RNV7JoYAOvsh86cDr312hHgns7dkLFj11WlPV2nrrHxRIj2YeivFfb2UzWtpNu/4Aofnijbn1dK/H0/n/8AvxVd+mlY2qd5fo5ad8Wor9K75FFtVd5tRq6699arZOP6sTXbaXPYYDWnK72oac53ibxOdz2SFa7VXeC2OfTp31SwE/pk/O6hYZPZVxN6/ViO8b/JFsaf0PCoJ37Dm0U79lqtoz6mNPYnrixetUYnsoV1hn1XUoh9TR2s/wAkED79lP6p5PJ3BnXmgjsCwooflPPqykBaGFDwuWjTE6nJ60sUhU5i6nG/vTpSzUNSeA7puxKmoyZfBNS7uoiVONaFr1WB21JxrSKcCXkW04y99FShImicTVyWqKCNZxmsx0GKpeK5xnm9jExSJgVcb6q9o8S92yuOrjko2wq1aDw1wbPmBrJGjwNKa567WJcl68DXF3rcuwbjPAvWpqhyJsQflGg2UhMC1k8C/AZQqV+B+UaEFiBwngTgRBOBhHgV65Y8argS0tPK3kkkcnIqHTrKCsIzBuqOlLGxiQzGyItcRlCqkwVZ0zKamwi1OlM8alFKkKXmZKMUgwCnIvLUpbDMyKpLvWo4ZF5dY7gvKzu9Rqrmt9HzrKopzjb15rneZPr86jQpUg8q+RQK7WL2M6qPRTnaMDLV7R69NrlVDiW1tTVPVMyi5u2BDhrjHGTBUaMYmDOIpAIWrV+UAxzW1GNLvk4prVHS8ucg10MR6yhHEg9XXaPmdMZ1wirGNj3T+yeV8eraNysWmQh1te+kqMMoEihVrzlVVD16NTIwYCSW0fKjEU4TVzlL1FFS1VUpkDg1GjptMnJV7MaCZ1ahRCuXvfIUmJa7kvXTPETilq2XSZ3FLyJvbGL50M1aqtbk1zqWGUsRX5YvT6Rtq/CcRayleFm8TMAKFLRScZKMWlopM5MApEa3xt4uQB2JUylpK7hNbeMHaScJnERRF6q0+uT7I/L9oVcKkijE8oWLQBRTjrreEBSKjN1O4QicpEIVCkUzwAopFSkzJfBWBUrsU44Aq8ERAZU6IODiqcqvXCOsKkZ7c4IscGZ1ppTVxUWmUNXKkU1lWnG5pelJFhA5x0WqohQYlTXi0VqVMSalp4EixZpmrMXngWk2WJTaL1PA7uiaqEFeeAoKRXRI54x6CLDV1PA4MQp3VnPUKWlGcliCf1ndLHl7sTKnqp5gCbJIbVm4qQpHKkU4u2KaKQLrnGw4SQHM9dkAFoci6ikk7mjLVOHSKUoZi6rAxUmnUyi1dFir4inGXvcYxRQzmt+v/GO66LTjH71RESgRlOvbq1E+6I4vGlUx6VdUSPwCmpOa7QwocZNcD7P4AqRk1SnNt0to9TkbSWupbGTVJ2g+6eNUIgHjLNFKNQk9oKFUh1OMLMdofmJiCEeWrO4tIeJShWw3KM45dFndGJyJJaIGTMRgFJKEZuKKSWljOFWJwJIjjTgroq0oykaYItRtjOh0ohRlXM6KctFpdR5MqKlUNudw5UOq4dIipxFO3nOEBx5BOnwd5KUUK4mo7SIhGHGIFKRGqPcEkVNG2UtTiIipJyiB0pCKcQUQoIcVScRRlgtCfWKKIkPdIixJnO4sf1zy3CewM5aeopVEZQIIWEKvGJ0iVVV4wQoeqjlK4FjgZjwoqswUzgWNgEPMvZl0JrFpxJHweI0OGVLrWsWIoIymxFJVNS0ykw7qHdLHEV+t7U3FJUyrHn2QSFqA8DXsyzJaGcl7kHKqU84or2gIcRxksNSDlM5UCgUqkk4wsIiCOop4ThU7nCvzCESme5iFET2iFD3HrnleFpJJOUl5evjfnz0052kyO17mGSuBOMWiBx2kzkkrR2lLCPElCr1EzwS/dTkrMhsIE7oPEnORQcUzvauy2dhpmWPkLQRhaxysqhrdrVzXS7eeOirpYo2m2pl0vFUa14Ghk7xmk12om3XOlJ0xXR0jTbIvjVfK/Ppi+LmmbWHf51Lq9tp4E2Qnzv2+ldJroZEl5t219/PzUmd+nTrWvmvl1+bkzOej01+fy9+NPhmW6VOfb6fDbXz67ZS0fPpsvj58/kao8Qw1Wyc5HCqKj9tdwjyvT80tIsaXZ1rgcSSY87mOmvxifEjtMYx0muvKPBaPXnKrHjC6hWxqVnNL1wj5yu2JlOmNaJVbVt1JyZBXcRfS+RSiuKSPnzrlFsIaxNdtsoChGKu3y665vGyk1hfHyfwOIOqvzpTz0116YyHMdeyc27YfScZVq+S0TmjqnkNVdixVneuIYvJUiiqVMpcmLSosK5xJh1BVwCZBpV064YUmcpqoqjFPbC9SS0IchBBRAeMURBz2licCf2nlryMfdJ7a221cu7xlp4Q5306XMIKnKbfT+eLrHADOuc10YgmzMhxfPtJE6imRMfH8ejqBUnjpPPzkHYq6OMi1VV3VYyrsroLottXKryNS7cnIhNOmynPXm0ZE5q6uhVQPJ6O9huVfDzKTV6ve9Ver8YpRNdta6Ptpg5RbR1jTzXn8+UpaF/izp2+nyxxiilQr4Oq56KeAGnzTnTpi+vwhyxojE58+p1xmK+3OF1j3kZxlqnWrv311v52njpJKrUx9ZUWiwpyfCRRSe08sMTiK4HEhCTba0Uk4tFLbHvveQFMtqh59p79b87mcw49bvFGFWdTzF5nXGxK5U+lvh53von4xU9tZjBjtRyk7TrswNWplEF72wsY2V+V5fSSzvORhuUnJYh4kXXZSo6Rmyz89emFr/wAvMCVj5Udfl9NnnG9pNJd/CjMDp1DhO2pMovj56xXlFS0yrExUTs4nnIcCkWhOMtfCkkVypkWogPHPAj2xAalwJyCqY3PbOBE7TxD1inlrCklCih4hRV7yICZRHAVUtfEndIjCnIALVI0+URwd1JdmdUqtVF1tVZxHCOI02qswwS1O/a95k5ha0mi/xes5F05Wp1UDlEVKWvy61RnVppLj02LucSINZalOUQtj1OYUMjXbFKKkRScYOBFFKtjKKsK5JEWrk/UyWJyDgRCYciRSCuz3FhCCx7imN8J9kBByk7gnl+nCoe52ALSZyxwBD8Rqepwp/WWERPOJt2pjljOAKoXLHKd1Ym1oTPFwlXwDIZ1bCvtTmA5DhYu4yTOSFjWsS05UpsgZBVyg6iEBMx0RSiItMynUnAUpGcqO0ZKT5FopFUklchUz1VMoi0UUKWx4lg7oZncQ4RHtlKRolq+sgp7j1nl9Sx9gnwJGSInJqHSMup4zu53JWKtOMida37dey+JMzDtG/h8fjV3nObDRytsbIzmOlunXXHPpz1YZXWAdLHnOimY2tMKCGGQQOimNHWJMgs8YnDBjlSdhX20veGU4Shdq2+dKuQoSF+eiqs4oYkUtEZSm1LRSpyL6UFgVJmdddrEUgmZxzgHADjEWbtLQgZsREEU+yRMh9YThKTiPEtD9E7jy3yeLsKRzF0chUIixezEZSwigLVyHYFFJSmbdWNaVX6PxGpHhkZ43qgRtcqoZ4eB0tr1znJ1RBTn1sQzFSjw3aTKThRJ0iZOUTjieKMIypkIhlIuZRN3iJi0MpwvEQjT8XSBCAqZyWkT4EfixFWIKs4IQiIdGRYoTliI8aTCcGp7YCdj7ICEEnKd0R+iWPJv/AP+mAHQBAAIBAQQCCggLCxcHCgcAALO1AQIDBLK2BQYHCBITFBUWabG3CQoLDRARF2oMDg8YGRpKS05RuEhJTE1PUFRVVmCwGxwdHh9FRkdSU1dYWVpbXF1eYWJjZK8hIiQoQl+5ICUnKjVAQUSuuiMsLzE2P63/owADAecRmxBXQPU0YNjuYUQrM6G5uxpzDgozMZkN6QYMIO5zMilEcQ8UpGILuzTGmh4PSMC6niJ/6rDj1/4nUwXcR3D0IQTlk/C2Lm96wHp1xRQeOZ0x06z8MXDwZil6Y/CFjdiZhCYes6HirGY6YrHHMejnGenU5YSxSPi0IGEjB4HaeAU0fmaOfyDQ3ParHeJZYY4ZsUiHJIFMPWuaXgFwYLvc4g6GHxFzcyVngEaxEnXc4xMxhFjHemaYzPAFgetoYMbI78dSK4InBOgOBfWFMLG9wRCJQbiEKT19YDgu8TC4gpuNcscnkzEXQ8C4ufyMOe+eRRiAU2dwxgw6rvDUQeChdmIblHCDHipG+MvAizo0QhvDQmcDvMBMYRjw/wDUyHTLY3oqmQjDxFfwpIRHcdHJAIOcHgHy6UosafB6EbKZgviDgtjEcO/GWYbJl8ShixMDxzjJCgmHeXAhHkGYkXyazHDHc3Sj8Zz/AIuntQo/EfiabLwGEWsVjdihiQsbhoYJGJ4uo2N5jQEscCyHtLJMnAymKDA4dyTMKITBubN2ZN+OoUK2PFwzDGz5BTYycWi7HewiXxHPEIRix4CRzR5YHRp4JEpo4thX8r+R5+7sWGODZ2LuLECzybBCMIeJAgXc8WmEDggMQjA3ZaYRDioUnS2DeWcsKXcwFopMG5pAbu4M5gNgp9TYGgjwJil6OSMHwMUYmGMQ3lOWCvAIBG4+LTboYFo8CEew4N2A7myVgI2ODGswj6yye1Ofwdh7S5+Y4ly7hDgwhGEx6y6nrIwCG8p0Xe0Nmw7yiEKTiQiMMU7mJ1IUm8Z0buh4EGZjRRuGNiGGB4lsGw4sZhRE8SxWIJF4FEX1lwMj5NkjjeaOjyaCL7Szj8Rz2XteBG5/8PsabJxULFO8Gzm2Xc2WK9adzErDFXegNdY9civgzr1dcHFchFgcMw65Ixo4PTGBay8sGCNiG9gRxHIb0xZBKDxSwkYsfEpZiEeWbo3yeAIYMoXeLFohje2GFEHiVlsespaOT/kc/wBbP/6WH2rH/s3bLH1sI2dxDR5NDtN4uGFgjuKKYetcupHflYEbu92FCb+pRQ/lKHxY4GZGExwVoobPBixoXyNGBvIQ06exH8hc9pTz/ROLQ6I+TRBI7kCyTJnktkWO9TXFO9ixoyU7koMjF4MwCUq7xiE6uIbyKB1oeOIjfMThnqUxLYfAY3CEYbkXNkp3qFIMOGcQFjS+JoF2jwYqxEVd6MQacnAhCEIcWiYjQnAiwjH2PrOw5/xR/wBnTNg4pGhTgwuweBM0QLBvQjBLO4EUbYXe2JisWdxRoTPkxKQOLbJ60bC2Nxl64s00eKvSxZxwIwstC+JGKzNHJrG18Cls00eLArL+MKLBwZmJZDisdX2D5F2x5gI2INZ4qWy2OBRBROCOKLGfluZlikxCY9eJhmadxGLgSx4thTGFwG8gzM6LMcCJmnCY3tmhyTHB6kCEaN5SAjHctCxiWzwTQseDBGBo701Sz4tJDV3DSGWJvaw6seQEEeDq50eSFD7XzASMhvLlkmOJEIRM8CFsqmDcXYKLwI2YMeKRbnAuEOtne2M0uCPDNlHExwCDCMzwxFhAo9pkscVLkB8QCFBiKbghM658SxMQA5ZTUi8CMBzgg72zYsbxYsA9ikI+SXBj7GP5HnsO0i+0svtWleBQ3yHFpVmA5LEhyWi7yLrYOTZGscCmZoZg5FEHMDfiAWMw3iKwQp35zcaVPE6aFNjxVgWyqcFpzHKu8wKNk4KVlzTyCwzMMciINHrFfxJTTHc3YL+N/K+YCOtMf8nD+MfxlD7QscCFEYFG9HUfWQLu8KbHkMWJHyYkGEeKkwrGJvQpoj5DBX8Q6PEWw0HAhY0fJsR8miH4z/Qh5HtdTzASBeLTcj7FwXOLQKsHgEAPWUYgKR4NmEeK3MGCHDK2bPJzY0NwDTCHFIRsQmeA/LKTLHgGMMTEVj4JnVgsNy5cFEY+BMpYCHtyhD8SmYnFVsAu8ItZSHLOZiiLyIwX8z7Q5/J2n/4aHsdHydFCZjwbEaDiO1d6NAWA3ugx4JCnExTDcxuUU72i2YtG5jZbJwIMQION4t2jkDCmI+DAUgw9eGzGh5EIxDg6Dd5FkXi2S7+Np9rz91gRz165y2fHPVIRs7wCGcTEOAM/99IjnkfJ+RoY3s/4/D5DFIviVj5ZonXkOM/IrNl5YcmmHxflOnURKIeBl69UALG4nSKdOmMYxjfjPybJTvOhYHIvTcqvynTrOpxIrkmcvyxH1MwmZnGX/D/jc0QxmdPw/D8Iu/P+JmdMdc5+Ud/ToMzOh0wb3OWkz8s7wrHSsfLpWDg5XGH5ewzCKQOTQ55Bc57ucEWFO56pF0XxGnoZIpHxxEfkKYXeU9chAo3dcP8AjP8A7/469flld/SPSzOnTgv/ABmfgQwdcpuzmdSEc058SjGZmxDHidUcU2Y7ugYMYQJ06bsu3Lnr4nSgMGOnT/HBSydfl8n8N4zAW69M/wCM+OW4McE6+LDFFjDxx0xCGmN6WYmX15rNf4yPBjEzOuHixYwXeEbIkeDQvsMbHneAAHBmClzHk/8AGejhYR45w02eLM2Ix4kSx5Ey2yGDe2czICjvTBZYTry/DGY+TnrYskdwQMTB0DDvIZVbZeOIpBgG5hHDEDDuwHyWsJB8WLAhmM6nl0yh0xwIQI4j1Dc5rKB//YY7zJDHyzky7wGs4jk68FBSf4HiDbo5yY5EAhxb5ZjnuPsaJmF3jmdDGYsxTuzS5cU8CkUFjw6wYZgeWK6pgcu5hYmbu/AuYxsblI5IsOTscrvyEKaOTHM6URmN7ZBhiPtzZh4LTWEmYG9IZmcuHiKDSkN+KQbEN7GkoD2Inyp3lIRwmeJfEMdT1lGMnsGlPWK+t59C8mGSiPEunSsWPAot+GCGeQT8JnrEzuaIlNYx4pGGA6QM43uMTDR1eAQixj0Ibi4mTqjuK6asycCCzAQzvWsuFwYNzC2MdVnR3qqCvXPBMEZhgj4Geq7cG46BG48mmyDybivIRrLY4BMNOT1rqHFKGPE7Dn9kZh9r1Pw+WOKLfrhd6MTNDDg0iNOHcMJiYZ8umdwlNZDDk3MEMdcZ6T8ODCDnJMLvLIZzjq7yhMuMgZ4IApl64dxbqiOeibyEIVhOCR6kw0vDrM0zMdxMU2c0Z8cQQHQ4saFhRvw2YGeTWWkhuKUITFPJaaDyQopN7TQQ5/REpKdwAxMljc0ZSEOARsTDDkQVFd+aBKeRTWZkjvBjQGSG9ikcNByVguMG5aUsZjuKFZnC5N5oMCneiuILE5DYIx4OIkIw4ESm7xcjCPJskUzxex8iiDF8mI+tjGBd5FFHP/fxFsZOQxx0fJ0OuMvALJTnDuGFmMyx8TMzRHr1gpuMUUx5ARWMeJOkHNY6dTjjKwJk3lZikcxPEbYpgdcby2fkQepnk2KzkTwLoYY2PA2sTe1m+S54tyJYYbizCOSPka5OQxoY+LYijGDwGJQsxuNAse15/JTMY6dOmMcjMSw2fAzOpWIrxcGSn2FlaXgmY0uXgtxejmncRwnVpHc4mYBWVdwKjMiu9aQhQdOBM0kFhudG4bhgwHDB3MJmimk3BGiFGXk4PYJDEzQuPBg4Ry9CBje4gTPTD1OCYCukc7mzWcwIp4NJMZoxTxDR4tgpfxFPPZazQTNG5i2YWdzHMXI8skwls8AgsCsHAzgoI45DgXEyPDDkzWVg7jEzCNsdOLmAxhyJjFKkc+LMOWkLY4ubNENxGDHJXTyMOUenJZghnPWZ3i0XzM7muixoxnHizpjEMQiRdxGgbHIhMZjMbmnKNgwcGZgr0fY0YH1h5gICB7CYhQUH/wB9WjV4MzGzHgWCw8WJGOaDgXCsWN2WMAAzHxSwXwcGmjQ4EwwytdeDYgUeQwzBicjLAicjVHcaFGDIcCs02N7hosTL4lsXbEdzjLTGHJKKEeJGxd4jonkQpORdiPP5bOpuNCPIs2IHJppsQ3YM5oH1uIMaOBiZAFQ4DoRI+JQAtD5H5GxdV3tmmYjvbNHrLmq71sFDvLmo8RhqG4w0Ux9jDEJg4OSnMwexYtOPZmxZ4AMQ/In4iHPgBOJWGIZ8iEaccGA0xhxLYsRhvcOGkIcTILFYbhwXVxngwMlAOOWGyYhvYUkQgeLQ3YcimmzwYkW4u5ocgQCO9jCkE3Ec0RIO/DRTCMdw7GAHArpGMOQ3zCPkQKPIhmmjksfMBHiiP48GfJGdSsvEcVgz8p0TgUxz8npxy2wdJ+BDe9WskDoUbjAbENxTmYprDvzM6JTwBsOXfhtimk3mgQ5NlIKY4EQGsYhuCBjFNYN+Z0mCMyQ3Hypxi7x6xzFhQPgbSZpdzWLohwZizYfITI08ksKby7D1nmAgZoezMX2FYnUhyV1OKMMRjDkQIJF4BBGxHeR0aXyLvFWxd4tgY1nO9iGoexjZdzdAp5MIRHPAp0QNzTDRhvGxq/jfWjGnybMfxl0PY3PyHmAjLq/mPJjb5GfIgdIWeBkZ+BRCncN1R4CU3fW2Up5AamdxZSxHi0Fld4QbDQ7gNFmOI7Vj7Eg7mmxcTkXYwOQxGDwEuTKcMXVhvKUgWZjgEbFPJ1I8gi+YPXj+U734n/IjHYNjtP8AdLFnQ+x8n0lFn6ze6OjEbpGPNmdpd0GCXO5/qaBTCAw+JvNBglFMGiP0EeDGjU1GIzNPNpYJYY07X+6MbtyI4Qp/c7SIwuiJkJhjqe5udg7GxcbERKI1ij4niMaRGOMwSGr95EmYIUWMwuQ/eOxRpSJnCJCZxThYQ97q9g64awlOxEu/YnpbAUWLtMKT7WZhTqOgMYiJRGz9rR2rowSOKKYUglD8G5tGKUWUTJYusLJ9aFzsaLYzRTMxPsdGkjsREgZSMRLpRqfQiJYjZsjCBZwkazZs2H3NizsLMI4YEMwcWbGdr8GMPQFNBSTJTBw6P6k2tFIipgoxGYMnaFPuRppdCmsggUwjBg4LtP6W5BsQg3bC4EKYU4jFpWJ9DSDY0NCOcWKQWDTTZNH87BDMCHYMxWbhEMuKw0WI/BhCMHVg0RsEUGGSNDQe9ovmYYlzNlwAwCxmkW2fgDSsI6Fw0CiEQbv1FZoCNjJYozAGzRTREaFue8IWYJqg0URjMTKwiI0WH5m7opm5mAwxQRQ0xqwPgN2EyaDRZUNGGaV0X6igilMErLrmAwzRZCn6W7HQiUzBGmgQcwSFikgfA1abEaMaNAMGCQaLvzkIRKbiQuaEbKYhcu+40dXRjDCXKbkdWFj6CELOg9rAG5SUwiUMfpOxIU2EiwsZoTtP3sewNQsRU0Y+9p2kaKBhGYoGBGGw0PnLNylxWaNFSJhCBYYxhR+gstFzZhNSMGYmYMGsRoofcLc2sGizhazgGCWLmg/YZSCQSwgjYhls6B8BuWI0MbNOrSxopiie87mmkopomSECMzGIe8uwu0GwS52I7EKfrA0IO1s2Y4pjZFpufOAwppHYdjEg6ZhojH9L2DZLsbuxgjCyRufSUO4IRZnwD3hdpo7XQIF1Y6ie4ihoNgIwp7WjQdp7g2Nm4FnaupdpPebUp0LPAgtKfWR1ItN2NNnRo/YaJYos0NMbtI6ET4JdYUNMKdG400li5B+hHRs009zTCNFNMP0g6MKGEbrRCz2lOxsfnbsE0bCbG5qMKY0w+ppGPbi7YsLGmhpIlHwSi7EjMdoGxKDYfSurTCjYzO1LMI0n6H0JRRRTFCPcNECNH1EaCBCjYQ0YNhbMw0sPndXa6lmixZSy6n0ELqwdgWW4dhFs/FIelhc1KbNiJ+h7gh4tFyCWbIR+l7TuLLZoGDgbFP1NlphD1MW7oWf7sTvYbD4mhouoHoLEWCfMeh0CliwohdujCDTD7TQLCEKI6BCNgmfc+gsCwhRTqsFY6v7GFnU7mFy4x/mU2brZpBbmh8x2jddpcbigUUsLL+k9LZrFzQ0KIwYOh9wU2I0XGmlp2MPsfSwzsImpGy/cbM6MLl3UI2Ian6D0pQQpsU0Me0/Y2FLOpSqx0P7Nku06Ctjm4sWiH8Hc7Gjm5ng83Be97T4MTiR0PMHvf/+jAAMB8wfQDvP9j/oav1n/AMnxf9Q3n+x/qU6G05thsXuObK94c4o/abzR+o/0ebq+RzbnQKf3n5nRoDm0tg0XsI82l2OwuvuPzOxdrY5uroBHm7BYwfrd7o7GnUPnfzFNHcvxf8jvfoPFdpo3O4P7Hc0bG5873sNWxZ/q8SxZdgbCLA+BHYbw2NiMfe7D1Fil0bFGw960tjcR1ItOr+1uXdCNLZs7T6zcbAu3bHpP+5vNrA0blMNGPzLcpbmrtNF7GETQo/8AC6MLA6ujZ0dBdr72gooI7A1Tubn6mNzuCNmGrT6F+tuWfW82h2ugXSi2Wn9ZGGq0UeLcs/SrRqbCweLo+5swodCL3FLuX3sKIxjZuFEF7HaWYfMGova3YFg2HcfMw2LowsUUNmnYWfgwGiKws3UsujCzY1PpbCxVojwLNml+owWWzZ9GeT87QEdCgo7Q0PF+4COqwpdhZ2MX9LZpjfNPaxh2Aurd+dtmxTRq3Xvdr7ywQAoKCxcj5PzmxhRYo1YbmP0EL5W6w7GzRsf3BEYUxs/jaQi/QQMMCOh2NwsWdClfews6JYhqvBuWP0K2VhQdzTcgRWLcNT/Y0aKI+hojRxPndDtdBVpg3IpZ2H6DRs3KLr3B/J7QhDYvoSwbCHxLkaDse41foewp7QIEKKNobV+kNQLhCYoxcIdrYPmbL2FFkopCzQWPE95dbK7GjYtnRX9pAH0GrsfuNCNjUNW7TZjoFH0uowLBtaAsx7n9B6Dc2LPaar+g7SZpi2actnR7CLCj9D6l2tg0aYtPYfubnqLrMGqw+Z73UKbLTA9JHLY9x2F25Cs5e9u/oae4osO1aYUw9C6n/c4kXaLqvofiUXNCGpoU6AfwbG3FnuWjaH53af5vaUfnfSxYRhGjvW5SD97Fswph2ENW5727TsLnoDsy/MdpSvaGq6GrYX9poroQ2tNNFyH2gBYs2dibSiPzPeQ2hq2I7W59B4l1hcu3M0x+D7VbNESg0KbP2G1oofU096/qKNSAXNCixo/wLHYUfwPyho3P7HpAopinNuWGxhzlz/Z3PYdzzbiLzeDsfMYkjzAk45vp5gZoc4I8wjUfMEnDn8HmCOJqc3o8wGgP0j/mbx5tRwOf8/1ecqaHmA+Z5gIinNsefWf9DzEuk5v55gMQc3sPi/ie40PMB0TnNHaek5tzToR0dXm1L5gIMc3psU3f7PqYXebuQObmx2OgXObuUUHuT/qbX73k7H9zwe40ObQEdWwbW797AurTTzcTYwj2vzmp3vgbG5zZSPeXftV0e83H7jRhY2BtbNMH7l1O0ubza/e6NEYdq6PuND0NLFp7Cn4HkRoh636Ci73GoUtighZo/o2W4RsaNNH9lhT/AEe09KtHoKfvdSGjoUUwp+BqavY3U3r9ZT4upoWA7Q+kPUUBzaWx3NL2rRzaCHY2NgP+7qdxTF7zm8n7DvNrqwj3v8l0Tcw+1727Y9jY+0i6FOhAudzD7CK2drxP2HEdD7A7HQ9rRsf4L/o0/qDxabMKWz/uD2FOxaI0FPNpbHY2NGNPuO17l3Nz6TQ1XYFzY/3bm12BRGj4n/V2B/Zs7XxPqIR7DaWOx1WFP0se0jAo2EKO9+w2mx1LNBH6w2Pg2O47w+ksdrwfuO8O48G7DQ+IbDV2BGn1HxXY7Cz6n+Z6DY6ENT3na97TsdpsSxT/AHO50fef825qB/U7WP7z0u1j6H+zye9p+4dhT6HuWzza3uSLsfrfU7mxGHNtYGrRzZTkwjsPsOwiUw0PQWY/acXm8NEe0pufwNp2MKOb4Gwj/UuHY7j9pc7Fi6Mdqx5u4Lo8259BD+b2hGil5tBT4FjwP5FilhRRz6l5wxwefI+Y01PmB2R5g+YeYXNPmBjR5jLGrGKrSt3vfqdrGKsYqscrd9C/0Id56Tm2nefQ9ybS72tPaf8Ak3m01NB8T4rRQ6EY7Abv9kjGEIjGydh+wCizTBpHYwdWj7jRY6NiJCmERo/YxaaYwGGxKLFMbJ8HsaaYgw2iQsxE2H6yEMAFNjYkEob4PsabkAsl273FH2tMAC7nQs3Gz/MaC5CNyz2Opo7T3MDUHaUjHQsanuKSz2I09roUJdo+opoLsGDT6kWz2r8z3JEaadSxQUx+4jolxjDV9Sv6yiY6RgQ2NhuIuhD9RmwBFrIl2NOgliFL7yiiwwpYmRHa3IbSn6xojREKdGJolOjo+91aYwWDZiWYNyFDsH9RqqqRpjHQpCDYs/FgwFoG5GzDVgUtn4uhSvpaaSMdr9hCiEVBhq6pZ0Io/UwhcaT0MKaIEKLH1i0RhmijsbNyBtKLH6V0JiiMfQXbGmdT3hDQIXRjCFyG1uJB+t3FwKJiixdofcR9BY2kHQjZoKdD4uxjYhdjo3IQs2feR1NGHYU3GEDYfSwLtlsQOJYpg/tKbhsNDYWWn9pYsx2lyMYkf6N2wUbCMe4+DDU8saOgUH53wACjZmx2MD9RRDtIrT6mmjYe47yzduU3PB+5jMxdo7D1PuaI7Ws6mjR3lmMfeOj2m1NDvfiB2F2z4ELlz5yjvbtk1ewuHwbPYXaSJoaJQ0WfoVXVsQYlFNy5tdD3rA70hYoaQIxpIxj7izD1ZpKLNMKYlGiUPvPSrsVwREmGzCF2Z952OikWOhGO0rH0G02tDAilENEhYxqQ+1groUNhjZ1LmMQ/OerOjCxBdhjRhRHYfnOxpu0UINZhYpHLpl+l0fQU3bIkFI05mB+JC6I0Q8DQuRj7i7CntQ9QRI3f4mjA2l0NCzGCf+XY+k783I50cv6xUosQsYCnV0AopufSBHQouXFoix9Kn1l24djCmmMG4WfrSm53FMHsWFh+LtImpS3Y2V7HPxIwu2TQ1WiAF1jTF+GbEPSbSGhGDMxc/UQNGBgs6MC7otEbPzGhYDtWIRoKLCOwo+pIwGPYlDoxsUQ+5gsbNyxSekgH7mNFiMNg7RpKPtcMe0GFNn0NPvP+agWYXdT6GBCHYBZgU0lwgR2P/h7UibAou2SmN2khY+LQU7G7qGqox/cWdoOhEoEp+s7CiNEadAKCh0aICR+52tGiESJZjR/FpEpG5Q0IaOgfaQs92djAuokaP4sxcgkDVsav70KSOoMwMdDENGj9yWdClC7GysGJD9rZirZhqMbN3/c2G1iESmP3vodWLB/aek2vYUx5tD3Ogc3J5x5zcjYc3wgH8gx4l8XPMPAntecS82h5w5c5vRo97z4jm6Judg82sstnU5t5TchRzZzVY9zCjmzpBaI2NGks/re00LhdbBY9J/AU2hRGz2n0npLJGnR0aObMIwKKbNJsP7EENg0URinNoRgtGqUw0PsaTRpESLoDRGJCP7nRopucE/UbhiUQouNOj9Rc0exppO8g2P8AcHY6MEhZu/qPSN8XF0xDQufQ+LYopabOjYifwdptLBR/NLtk2kbqWIi3WH0EbC2G7YKIxg9ro/qSZHtbmhdhRG59ZYjGJDVsRi2IQU0ftKO17WFiJR/N2NzsW6R+xphdjdIFBZsujtPrNpojCmzClbMXsfpNhT6XRaaVhdj8GNniwjo6H3sKDU7DufqPSXOxPQU/U97AjHvQGxCP63UsWLDtSOwpafgj2kNi0xiLHvPg940XLoRufaXfQxINB3EH9zoatFJTCghsHR/i3B0djtYfW6GosNCF2kKbhZsfadi6BtT+L6CPaURs9pT/ABNC7o6LCENh874GiUwdDsKaPrLujowog3abn9ymmNJQJWdGz95cseKaP9nU72mz/Bj4PYwf5mpsLNkSGpT+w7xsLZuXf4neOrTdhHtfinoYex+17H0o6sH73R2GqGxB+Y/1bvYw+0sWNCG1s+g5spT2nqf2Z0dDa2f3vi3Tkv1MdhG5TTZ2kP7nebE0P1lyzQMYR1YbT0vuPBsXCJYs6NP2MLnqTeR+L6Vp8CFn7mh9BdhoGg7T9pcjR3joR+t0YQbJ6CxsIwjDm2FOw7H9hq2fSxhHQ/W7Hg6DH+Z4MKfSw+Y7XwI+kjHV/e+JZpjY/oPY+ouU/rO42MbPg/zfUmpG5/dseo/k6mjDtXVos/yY3fUmjCi5/Rs6ELu0/YbGGx1OxsH7imyFnR7Wz/V8n72xBYbTsP7uiWI3KIXf7m05tB4N3zAgAsU+Yc6AWAgAAAFAd55gbSeYG2vFOfu85E5vj+9/M84Z+LovccDm+Nn97q/5nPUO05vBc5uxq0c3sPY3PMBWTsPa+YDyP5X+T3HA56J/9HmA97c59Duebe+17Dn6G1OfGeYCIvPve95vzzfHQ597z7iPc9zzdzm8naeYCBnN7Yegsc+k7Xm4HpObOd5o/wBTnOPcc3p/iXef62eb20F38h9rzfnxaPQfwaOb+6Mdh4Gjz2XzAQB5vx6nac247Xm/K2HsObo6HNqdj6Hmyuroc/h3HN4PMD4DzEDU8wPwPMCZTnLnOJOf8+YG+HmBLjzoXzAeh8wEdOfa+YD9PmAyD5gIKf3Nhzh3zAfA7HzAYE5wJ5gXyeYCCvN3PMBFzm3vPnfMDkjm9vODObs7HQjD7zzAQd5v5+R5tDHn+mpzd3vPMCbnzB8T/6E="

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;


            String FingerprintImage = "";
            String BarCode = "";

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetSeguroDesgravamenPDF(_pdfFormats.SEGURO_DESG, request/*, FingerprintImage, BarCode*/);

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

            String file = GetSeguroOptativoPDF(_pdfFormats.SEGURO_OPTATIVO, request/*, FingerprintImage, BarCode*/);

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

            String file = GetHojaAceptacionPDF(_pdfFormats.HOJA_ACEPTACION, request/*, FingerprintImage, BarCode*/);

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
                ImpresionBiometricaCliente = "/6D/qAB6TklTVF9DT00gOQpQSVhfV0lEVEggNTEyClBJWF9IRUlHSFQgNTEyClBJWF9ERVBUSCA4ClBQSSA1MDAKTE9TU1kgMQpDT0xPUlNQQUNFIEdSQVkKQ09NUFJFU1NJT04gV1NRCldTUV9CSVRSQVRFIDAuNzUwMDAw/6QAOgkHAAky0yXNAArg8xmaAQpB7/GaAQuOJ2TNAAvheaMzAAku/1YAAQr5M9MzAQvyhyGaAAomd9oz/6UBhQIALAPXUAIZ1gPXUAIZ1gPXUAIZ1gPXUAIZ1gPlwQIbkgPlGwIbfgPuEgIckQPtOgIceAPiuQIbNQPvKQIcswPvrgIcwwPX9gIZ6gPerwIauQPYnAIZ/gPcWQIacQPrTgIcPQP7VgIeKQPk0wIbdgP0EQIdSgP77AIeOwIa/wIgZgIZpwIeyAIblwIhHAIbLgIgnQIcNwIh3AIafgIfygIciwIiQQIbKQIgmAIb6gIhfwIbTQIgwwIbuAIhQwIbfgIg/gIdjQIjdgIbVwIgzwIcxgIihwPwvwIc5AP26wIdoQP9NAIeYgIbVAIgywIaAgIfNgIZzgIe9wIb5AIheAIarQIgAwIalQIf5gIaRgIfiAIbswIhPgIdHwIi8gIbXgIg1wIaeQIfxQIdVQIjMwIdDAIi2wIdZAIjRQP83AIeWAIlXAIs1AP9owIecAInKgIu/wP1xwIdfgP0vQIdXgIirgIpngIkGgIrUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP+iABEA/wIAAgACVNMEQkUCAAD/pgBuAAABAwEDBgcHCwgQEwkAAAC1AbKztgKxtxESr7C4wQMQE665ursECA8Uaa3ABQYHFRaqq6y8vb8JFxiSk5mevgoOGRoci4yNj5GUlZyhwsQLDA0dHh8ig4WHiYqOmKOnqKnDkJaXm52foKTG/6MAAwDffff09Pp9Px/H6b77777777/y3/6/6/n/AD/lvvvvvvv6fyt/t/D9v6v4/Ttvvvvvvv1+/wDb+39n+H29Pnvvvvvv6dPt/f8Ad9v/AG+6fTffffff02/X9/3f6f5/u1/Hffffff0x/f8A5fp/X+r7v9/p6b7777+nX/H9P7P1/b/7/j9PTfffff0+X/j7ft/d93+v8Px9N9999/T5/wDr/wA/u/7/AOv7/wD7233333336/H+P8P4fh+H4fP0333333+XP/b4fh933/8APz333333/l16/H3/AIff9/v+m++++++/z+l/+P8A5/v8F9N999999+3b6Y22339N9999999999/Tf09N999/7p014N9HXrDRTDbPWV0HFDve1RNV7JoYAOvsh86cDr312hHgns7dkLFj11WlPV2nrrHxRIj2YeivFfb2UzWtpNu/4Aofnijbn1dK/H0/n/8AvxVd+mlY2qd5fo5ad8Wor9K75FFtVd5tRq6699arZOP6sTXbaXPYYDWnK72oac53ibxOdz2SFa7VXeC2OfTp31SwE/pk/O6hYZPZVxN6/ViO8b/JFsaf0PCoJ37Dm0U79lqtoz6mNPYnrixetUYnsoV1hn1XUoh9TR2s/wAkED79lP6p5PJ3BnXmgjsCwooflPPqykBaGFDwuWjTE6nJ60sUhU5i6nG/vTpSzUNSeA7puxKmoyZfBNS7uoiVONaFr1WB21JxrSKcCXkW04y99FShImicTVyWqKCNZxmsx0GKpeK5xnm9jExSJgVcb6q9o8S92yuOrjko2wq1aDw1wbPmBrJGjwNKa567WJcl68DXF3rcuwbjPAvWpqhyJsQflGg2UhMC1k8C/AZQqV+B+UaEFiBwngTgRBOBhHgV65Y8argS0tPK3kkkcnIqHTrKCsIzBuqOlLGxiQzGyItcRlCqkwVZ0zKamwi1OlM8alFKkKXmZKMUgwCnIvLUpbDMyKpLvWo4ZF5dY7gvKzu9Rqrmt9HzrKopzjb15rneZPr86jQpUg8q+RQK7WL2M6qPRTnaMDLV7R69NrlVDiW1tTVPVMyi5u2BDhrjHGTBUaMYmDOIpAIWrV+UAxzW1GNLvk4prVHS8ucg10MR6yhHEg9XXaPmdMZ1wirGNj3T+yeV8eraNysWmQh1te+kqMMoEihVrzlVVD16NTIwYCSW0fKjEU4TVzlL1FFS1VUpkDg1GjptMnJV7MaCZ1ahRCuXvfIUmJa7kvXTPETilq2XSZ3FLyJvbGL50M1aqtbk1zqWGUsRX5YvT6Rtq/CcRayleFm8TMAKFLRScZKMWlopM5MApEa3xt4uQB2JUylpK7hNbeMHaScJnERRF6q0+uT7I/L9oVcKkijE8oWLQBRTjrreEBSKjN1O4QicpEIVCkUzwAopFSkzJfBWBUrsU44Aq8ERAZU6IODiqcqvXCOsKkZ7c4IscGZ1ppTVxUWmUNXKkU1lWnG5pelJFhA5x0WqohQYlTXi0VqVMSalp4EixZpmrMXngWk2WJTaL1PA7uiaqEFeeAoKRXRI54x6CLDV1PA4MQp3VnPUKWlGcliCf1ndLHl7sTKnqp5gCbJIbVm4qQpHKkU4u2KaKQLrnGw4SQHM9dkAFoci6ikk7mjLVOHSKUoZi6rAxUmnUyi1dFir4inGXvcYxRQzmt+v/GO66LTjH71RESgRlOvbq1E+6I4vGlUx6VdUSPwCmpOa7QwocZNcD7P4AqRk1SnNt0to9TkbSWupbGTVJ2g+6eNUIgHjLNFKNQk9oKFUh1OMLMdofmJiCEeWrO4tIeJShWw3KM45dFndGJyJJaIGTMRgFJKEZuKKSWljOFWJwJIjjTgroq0oykaYItRtjOh0ohRlXM6KctFpdR5MqKlUNudw5UOq4dIipxFO3nOEBx5BOnwd5KUUK4mo7SIhGHGIFKRGqPcEkVNG2UtTiIipJyiB0pCKcQUQoIcVScRRlgtCfWKKIkPdIixJnO4sf1zy3CewM5aeopVEZQIIWEKvGJ0iVVV4wQoeqjlK4FjgZjwoqswUzgWNgEPMvZl0JrFpxJHweI0OGVLrWsWIoIymxFJVNS0ykw7qHdLHEV+t7U3FJUyrHn2QSFqA8DXsyzJaGcl7kHKqU84or2gIcRxksNSDlM5UCgUqkk4wsIiCOop4ThU7nCvzCESme5iFET2iFD3HrnleFpJJOUl5evjfnz0052kyO17mGSuBOMWiBx2kzkkrR2lLCPElCr1EzwS/dTkrMhsIE7oPEnORQcUzvauy2dhpmWPkLQRhaxysqhrdrVzXS7eeOirpYo2m2pl0vFUa14Ghk7xmk12om3XOlJ0xXR0jTbIvjVfK/Ppi+LmmbWHf51Lq9tp4E2Qnzv2+ldJroZEl5t219/PzUmd+nTrWvmvl1+bkzOej01+fy9+NPhmW6VOfb6fDbXz67ZS0fPpsvj58/kao8Qw1Wyc5HCqKj9tdwjyvT80tIsaXZ1rgcSSY87mOmvxifEjtMYx0muvKPBaPXnKrHjC6hWxqVnNL1wj5yu2JlOmNaJVbVt1JyZBXcRfS+RSiuKSPnzrlFsIaxNdtsoChGKu3y665vGyk1hfHyfwOIOqvzpTz0116YyHMdeyc27YfScZVq+S0TmjqnkNVdixVneuIYvJUiiqVMpcmLSosK5xJh1BVwCZBpV064YUmcpqoqjFPbC9SS0IchBBRAeMURBz2licCf2nlryMfdJ7a221cu7xlp4Q5306XMIKnKbfT+eLrHADOuc10YgmzMhxfPtJE6imRMfH8ejqBUnjpPPzkHYq6OMi1VV3VYyrsroLottXKryNS7cnIhNOmynPXm0ZE5q6uhVQPJ6O9huVfDzKTV6ve9Ver8YpRNdta6Ptpg5RbR1jTzXn8+UpaF/izp2+nyxxiilQr4Oq56KeAGnzTnTpi+vwhyxojE58+p1xmK+3OF1j3kZxlqnWrv311v52njpJKrUx9ZUWiwpyfCRRSe08sMTiK4HEhCTba0Uk4tFLbHvveQFMtqh59p79b87mcw49bvFGFWdTzF5nXGxK5U+lvh53von4xU9tZjBjtRyk7TrswNWplEF72wsY2V+V5fSSzvORhuUnJYh4kXXZSo6Rmyz89emFr/wAvMCVj5Udfl9NnnG9pNJd/CjMDp1DhO2pMovj56xXlFS0yrExUTs4nnIcCkWhOMtfCkkVypkWogPHPAj2xAalwJyCqY3PbOBE7TxD1inlrCklCih4hRV7yICZRHAVUtfEndIjCnIALVI0+URwd1JdmdUqtVF1tVZxHCOI02qswwS1O/a95k5ha0mi/xes5F05Wp1UDlEVKWvy61RnVppLj02LucSINZalOUQtj1OYUMjXbFKKkRScYOBFFKtjKKsK5JEWrk/UyWJyDgRCYciRSCuz3FhCCx7imN8J9kBByk7gnl+nCoe52ALSZyxwBD8Rqepwp/WWERPOJt2pjljOAKoXLHKd1Ym1oTPFwlXwDIZ1bCvtTmA5DhYu4yTOSFjWsS05UpsgZBVyg6iEBMx0RSiItMynUnAUpGcqO0ZKT5FopFUklchUz1VMoi0UUKWx4lg7oZncQ4RHtlKRolq+sgp7j1nl9Sx9gnwJGSInJqHSMup4zu53JWKtOMida37dey+JMzDtG/h8fjV3nObDRytsbIzmOlunXXHPpz1YZXWAdLHnOimY2tMKCGGQQOimNHWJMgs8YnDBjlSdhX20veGU4Shdq2+dKuQoSF+eiqs4oYkUtEZSm1LRSpyL6UFgVJmdddrEUgmZxzgHADjEWbtLQgZsREEU+yRMh9YThKTiPEtD9E7jy3yeLsKRzF0chUIixezEZSwigLVyHYFFJSmbdWNaVX6PxGpHhkZ43qgRtcqoZ4eB0tr1znJ1RBTn1sQzFSjw3aTKThRJ0iZOUTjieKMIypkIhlIuZRN3iJi0MpwvEQjT8XSBCAqZyWkT4EfixFWIKs4IQiIdGRYoTliI8aTCcGp7YCdj7ICEEnKd0R+iWPJv/AP+mAHQBAAIBAQQCCggLCxcHCgcAALO1AQIDBLK2BQYHCBITFBUWabG3CQoLDRARF2oMDg8YGRpKS05RuEhJTE1PUFRVVmCwGxwdHh9FRkdSU1dYWVpbXF1eYWJjZK8hIiQoQl+5ICUnKjVAQUSuuiMsLzE2P63/owADAecRmxBXQPU0YNjuYUQrM6G5uxpzDgozMZkN6QYMIO5zMilEcQ8UpGILuzTGmh4PSMC6niJ/6rDj1/4nUwXcR3D0IQTlk/C2Lm96wHp1xRQeOZ0x06z8MXDwZil6Y/CFjdiZhCYes6HirGY6YrHHMejnGenU5YSxSPi0IGEjB4HaeAU0fmaOfyDQ3ParHeJZYY4ZsUiHJIFMPWuaXgFwYLvc4g6GHxFzcyVngEaxEnXc4xMxhFjHemaYzPAFgetoYMbI78dSK4InBOgOBfWFMLG9wRCJQbiEKT19YDgu8TC4gpuNcscnkzEXQ8C4ufyMOe+eRRiAU2dwxgw6rvDUQeChdmIblHCDHipG+MvAizo0QhvDQmcDvMBMYRjw/wDUyHTLY3oqmQjDxFfwpIRHcdHJAIOcHgHy6UosafB6EbKZgviDgtjEcO/GWYbJl8ShixMDxzjJCgmHeXAhHkGYkXyazHDHc3Sj8Zz/AIuntQo/EfiabLwGEWsVjdihiQsbhoYJGJ4uo2N5jQEscCyHtLJMnAymKDA4dyTMKITBubN2ZN+OoUK2PFwzDGz5BTYycWi7HewiXxHPEIRix4CRzR5YHRp4JEpo4thX8r+R5+7sWGODZ2LuLECzybBCMIeJAgXc8WmEDggMQjA3ZaYRDioUnS2DeWcsKXcwFopMG5pAbu4M5gNgp9TYGgjwJil6OSMHwMUYmGMQ3lOWCvAIBG4+LTboYFo8CEew4N2A7myVgI2ODGswj6yye1Ofwdh7S5+Y4ly7hDgwhGEx6y6nrIwCG8p0Xe0Nmw7yiEKTiQiMMU7mJ1IUm8Z0buh4EGZjRRuGNiGGB4lsGw4sZhRE8SxWIJF4FEX1lwMj5NkjjeaOjyaCL7Szj8Rz2XteBG5/8PsabJxULFO8Gzm2Xc2WK9adzErDFXegNdY9civgzr1dcHFchFgcMw65Ixo4PTGBay8sGCNiG9gRxHIb0xZBKDxSwkYsfEpZiEeWbo3yeAIYMoXeLFohje2GFEHiVlsespaOT/kc/wBbP/6WH2rH/s3bLH1sI2dxDR5NDtN4uGFgjuKKYetcupHflYEbu92FCb+pRQ/lKHxY4GZGExwVoobPBixoXyNGBvIQ06exH8hc9pTz/ROLQ6I+TRBI7kCyTJnktkWO9TXFO9ixoyU7koMjF4MwCUq7xiE6uIbyKB1oeOIjfMThnqUxLYfAY3CEYbkXNkp3qFIMOGcQFjS+JoF2jwYqxEVd6MQacnAhCEIcWiYjQnAiwjH2PrOw5/xR/wBnTNg4pGhTgwuweBM0QLBvQjBLO4EUbYXe2JisWdxRoTPkxKQOLbJ60bC2Nxl64s00eKvSxZxwIwstC+JGKzNHJrG18Cls00eLArL+MKLBwZmJZDisdX2D5F2x5gI2INZ4qWy2OBRBROCOKLGfluZlikxCY9eJhmadxGLgSx4thTGFwG8gzM6LMcCJmnCY3tmhyTHB6kCEaN5SAjHctCxiWzwTQseDBGBo701Sz4tJDV3DSGWJvaw6seQEEeDq50eSFD7XzASMhvLlkmOJEIRM8CFsqmDcXYKLwI2YMeKRbnAuEOtne2M0uCPDNlHExwCDCMzwxFhAo9pkscVLkB8QCFBiKbghM658SxMQA5ZTUi8CMBzgg72zYsbxYsA9ikI+SXBj7GP5HnsO0i+0svtWleBQ3yHFpVmA5LEhyWi7yLrYOTZGscCmZoZg5FEHMDfiAWMw3iKwQp35zcaVPE6aFNjxVgWyqcFpzHKu8wKNk4KVlzTyCwzMMciINHrFfxJTTHc3YL+N/K+YCOtMf8nD+MfxlD7QscCFEYFG9HUfWQLu8KbHkMWJHyYkGEeKkwrGJvQpoj5DBX8Q6PEWw0HAhY0fJsR8miH4z/Qh5HtdTzASBeLTcj7FwXOLQKsHgEAPWUYgKR4NmEeK3MGCHDK2bPJzY0NwDTCHFIRsQmeA/LKTLHgGMMTEVj4JnVgsNy5cFEY+BMpYCHtyhD8SmYnFVsAu8ItZSHLOZiiLyIwX8z7Q5/J2n/4aHsdHydFCZjwbEaDiO1d6NAWA3ugx4JCnExTDcxuUU72i2YtG5jZbJwIMQION4t2jkDCmI+DAUgw9eGzGh5EIxDg6Dd5FkXi2S7+Np9rz91gRz165y2fHPVIRs7wCGcTEOAM/99IjnkfJ+RoY3s/4/D5DFIviVj5ZonXkOM/IrNl5YcmmHxflOnURKIeBl69UALG4nSKdOmMYxjfjPybJTvOhYHIvTcqvynTrOpxIrkmcvyxH1MwmZnGX/D/jc0QxmdPw/D8Iu/P+JmdMdc5+Ud/ToMzOh0wb3OWkz8s7wrHSsfLpWDg5XGH5ewzCKQOTQ55Bc57ucEWFO56pF0XxGnoZIpHxxEfkKYXeU9chAo3dcP8AjP8A7/469flld/SPSzOnTgv/ABmfgQwdcpuzmdSEc058SjGZmxDHidUcU2Y7ugYMYQJ06bsu3Lnr4nSgMGOnT/HBSydfl8n8N4zAW69M/wCM+OW4McE6+LDFFjDxx0xCGmN6WYmX15rNf4yPBjEzOuHixYwXeEbIkeDQvsMbHneAAHBmClzHk/8AGejhYR45w02eLM2Ix4kSx5Ey2yGDe2czICjvTBZYTry/DGY+TnrYskdwQMTB0DDvIZVbZeOIpBgG5hHDEDDuwHyWsJB8WLAhmM6nl0yh0xwIQI4j1Dc5rKB//YY7zJDHyzky7wGs4jk68FBSf4HiDbo5yY5EAhxb5ZjnuPsaJmF3jmdDGYsxTuzS5cU8CkUFjw6wYZgeWK6pgcu5hYmbu/AuYxsblI5IsOTscrvyEKaOTHM6URmN7ZBhiPtzZh4LTWEmYG9IZmcuHiKDSkN+KQbEN7GkoD2Inyp3lIRwmeJfEMdT1lGMnsGlPWK+t59C8mGSiPEunSsWPAot+GCGeQT8JnrEzuaIlNYx4pGGA6QM43uMTDR1eAQixj0Ibi4mTqjuK6asycCCzAQzvWsuFwYNzC2MdVnR3qqCvXPBMEZhgj4Geq7cG46BG48mmyDybivIRrLY4BMNOT1rqHFKGPE7Dn9kZh9r1Pw+WOKLfrhd6MTNDDg0iNOHcMJiYZ8umdwlNZDDk3MEMdcZ6T8ODCDnJMLvLIZzjq7yhMuMgZ4IApl64dxbqiOeibyEIVhOCR6kw0vDrM0zMdxMU2c0Z8cQQHQ4saFhRvw2YGeTWWkhuKUITFPJaaDyQopN7TQQ5/REpKdwAxMljc0ZSEOARsTDDkQVFd+aBKeRTWZkjvBjQGSG9ikcNByVguMG5aUsZjuKFZnC5N5oMCneiuILE5DYIx4OIkIw4ESm7xcjCPJskUzxex8iiDF8mI+tjGBd5FFHP/fxFsZOQxx0fJ0OuMvALJTnDuGFmMyx8TMzRHr1gpuMUUx5ARWMeJOkHNY6dTjjKwJk3lZikcxPEbYpgdcby2fkQepnk2KzkTwLoYY2PA2sTe1m+S54tyJYYbizCOSPka5OQxoY+LYijGDwGJQsxuNAse15/JTMY6dOmMcjMSw2fAzOpWIrxcGSn2FlaXgmY0uXgtxejmncRwnVpHc4mYBWVdwKjMiu9aQhQdOBM0kFhudG4bhgwHDB3MJmimk3BGiFGXk4PYJDEzQuPBg4Ry9CBje4gTPTD1OCYCukc7mzWcwIp4NJMZoxTxDR4tgpfxFPPZazQTNG5i2YWdzHMXI8skwls8AgsCsHAzgoI45DgXEyPDDkzWVg7jEzCNsdOLmAxhyJjFKkc+LMOWkLY4ubNENxGDHJXTyMOUenJZghnPWZ3i0XzM7muixoxnHizpjEMQiRdxGgbHIhMZjMbmnKNgwcGZgr0fY0YH1h5gICB7CYhQUH/wB9WjV4MzGzHgWCw8WJGOaDgXCsWN2WMAAzHxSwXwcGmjQ4EwwytdeDYgUeQwzBicjLAicjVHcaFGDIcCs02N7hosTL4lsXbEdzjLTGHJKKEeJGxd4jonkQpORdiPP5bOpuNCPIs2IHJppsQ3YM5oH1uIMaOBiZAFQ4DoRI+JQAtD5H5GxdV3tmmYjvbNHrLmq71sFDvLmo8RhqG4w0Ux9jDEJg4OSnMwexYtOPZmxZ4AMQ/In4iHPgBOJWGIZ8iEaccGA0xhxLYsRhvcOGkIcTILFYbhwXVxngwMlAOOWGyYhvYUkQgeLQ3YcimmzwYkW4u5ocgQCO9jCkE3Ec0RIO/DRTCMdw7GAHArpGMOQ3zCPkQKPIhmmjksfMBHiiP48GfJGdSsvEcVgz8p0TgUxz8npxy2wdJ+BDe9WskDoUbjAbENxTmYprDvzM6JTwBsOXfhtimk3mgQ5NlIKY4EQGsYhuCBjFNYN+Z0mCMyQ3Hypxi7x6xzFhQPgbSZpdzWLohwZizYfITI08ksKby7D1nmAgZoezMX2FYnUhyV1OKMMRjDkQIJF4BBGxHeR0aXyLvFWxd4tgY1nO9iGoexjZdzdAp5MIRHPAp0QNzTDRhvGxq/jfWjGnybMfxl0PY3PyHmAjLq/mPJjb5GfIgdIWeBkZ+BRCncN1R4CU3fW2Up5AamdxZSxHi0Fld4QbDQ7gNFmOI7Vj7Eg7mmxcTkXYwOQxGDwEuTKcMXVhvKUgWZjgEbFPJ1I8gi+YPXj+U734n/IjHYNjtP8AdLFnQ+x8n0lFn6ze6OjEbpGPNmdpd0GCXO5/qaBTCAw+JvNBglFMGiP0EeDGjU1GIzNPNpYJYY07X+6MbtyI4Qp/c7SIwuiJkJhjqe5udg7GxcbERKI1ij4niMaRGOMwSGr95EmYIUWMwuQ/eOxRpSJnCJCZxThYQ97q9g64awlOxEu/YnpbAUWLtMKT7WZhTqOgMYiJRGz9rR2rowSOKKYUglD8G5tGKUWUTJYusLJ9aFzsaLYzRTMxPsdGkjsREgZSMRLpRqfQiJYjZsjCBZwkazZs2H3NizsLMI4YEMwcWbGdr8GMPQFNBSTJTBw6P6k2tFIipgoxGYMnaFPuRppdCmsggUwjBg4LtP6W5BsQg3bC4EKYU4jFpWJ9DSDY0NCOcWKQWDTTZNH87BDMCHYMxWbhEMuKw0WI/BhCMHVg0RsEUGGSNDQe9ovmYYlzNlwAwCxmkW2fgDSsI6Fw0CiEQbv1FZoCNjJYozAGzRTREaFue8IWYJqg0URjMTKwiI0WH5m7opm5mAwxQRQ0xqwPgN2EyaDRZUNGGaV0X6igilMErLrmAwzRZCn6W7HQiUzBGmgQcwSFikgfA1abEaMaNAMGCQaLvzkIRKbiQuaEbKYhcu+40dXRjDCXKbkdWFj6CELOg9rAG5SUwiUMfpOxIU2EiwsZoTtP3sewNQsRU0Y+9p2kaKBhGYoGBGGw0PnLNylxWaNFSJhCBYYxhR+gstFzZhNSMGYmYMGsRoofcLc2sGizhazgGCWLmg/YZSCQSwgjYhls6B8BuWI0MbNOrSxopiie87mmkopomSECMzGIe8uwu0GwS52I7EKfrA0IO1s2Y4pjZFpufOAwppHYdjEg6ZhojH9L2DZLsbuxgjCyRufSUO4IRZnwD3hdpo7XQIF1Y6ie4ihoNgIwp7WjQdp7g2Nm4FnaupdpPebUp0LPAgtKfWR1ItN2NNnRo/YaJYos0NMbtI6ET4JdYUNMKdG400li5B+hHRs009zTCNFNMP0g6MKGEbrRCz2lOxsfnbsE0bCbG5qMKY0w+ppGPbi7YsLGmhpIlHwSi7EjMdoGxKDYfSurTCjYzO1LMI0n6H0JRRRTFCPcNECNH1EaCBCjYQ0YNhbMw0sPndXa6lmixZSy6n0ELqwdgWW4dhFs/FIelhc1KbNiJ+h7gh4tFyCWbIR+l7TuLLZoGDgbFP1NlphD1MW7oWf7sTvYbD4mhouoHoLEWCfMeh0CliwohdujCDTD7TQLCEKI6BCNgmfc+gsCwhRTqsFY6v7GFnU7mFy4x/mU2brZpBbmh8x2jddpcbigUUsLL+k9LZrFzQ0KIwYOh9wU2I0XGmlp2MPsfSwzsImpGy/cbM6MLl3UI2Ian6D0pQQpsU0Me0/Y2FLOpSqx0P7Nku06Ctjm4sWiH8Hc7Gjm5ng83Be97T4MTiR0PMHvf/+jAAMB8wfQDvP9j/oav1n/AMnxf9Q3n+x/qU6G05thsXuObK94c4o/abzR+o/0ebq+RzbnQKf3n5nRoDm0tg0XsI82l2OwuvuPzOxdrY5uroBHm7BYwfrd7o7GnUPnfzFNHcvxf8jvfoPFdpo3O4P7Hc0bG5873sNWxZ/q8SxZdgbCLA+BHYbw2NiMfe7D1Fil0bFGw960tjcR1ItOr+1uXdCNLZs7T6zcbAu3bHpP+5vNrA0blMNGPzLcpbmrtNF7GETQo/8AC6MLA6ujZ0dBdr72gooI7A1Tubn6mNzuCNmGrT6F+tuWfW82h2ugXSi2Wn9ZGGq0UeLcs/SrRqbCweLo+5swodCL3FLuX3sKIxjZuFEF7HaWYfMGova3YFg2HcfMw2LowsUUNmnYWfgwGiKws3UsujCzY1PpbCxVojwLNml+owWWzZ9GeT87QEdCgo7Q0PF+4COqwpdhZ2MX9LZpjfNPaxh2Aurd+dtmxTRq3Xvdr7ywQAoKCxcj5PzmxhRYo1YbmP0EL5W6w7GzRsf3BEYUxs/jaQi/QQMMCOh2NwsWdClfews6JYhqvBuWP0K2VhQdzTcgRWLcNT/Y0aKI+hojRxPndDtdBVpg3IpZ2H6DRs3KLr3B/J7QhDYvoSwbCHxLkaDse41foewp7QIEKKNobV+kNQLhCYoxcIdrYPmbL2FFkopCzQWPE95dbK7GjYtnRX9pAH0GrsfuNCNjUNW7TZjoFH0uowLBtaAsx7n9B6Dc2LPaar+g7SZpi2actnR7CLCj9D6l2tg0aYtPYfubnqLrMGqw+Z73UKbLTA9JHLY9x2F25Cs5e9u/oae4osO1aYUw9C6n/c4kXaLqvofiUXNCGpoU6AfwbG3FnuWjaH53af5vaUfnfSxYRhGjvW5SD97Fswph2ENW5727TsLnoDsy/MdpSvaGq6GrYX9poroQ2tNNFyH2gBYs2dibSiPzPeQ2hq2I7W59B4l1hcu3M0x+D7VbNESg0KbP2G1oofU096/qKNSAXNCixo/wLHYUfwPyho3P7HpAopinNuWGxhzlz/Z3PYdzzbiLzeDsfMYkjzAk45vp5gZoc4I8wjUfMEnDn8HmCOJqc3o8wGgP0j/mbx5tRwOf8/1ecqaHmA+Z5gIinNsefWf9DzEuk5v55gMQc3sPi/ie40PMB0TnNHaek5tzToR0dXm1L5gIMc3psU3f7PqYXebuQObmx2OgXObuUUHuT/qbX73k7H9zwe40ObQEdWwbW797AurTTzcTYwj2vzmp3vgbG5zZSPeXftV0e83H7jRhY2BtbNMH7l1O0ubza/e6NEYdq6PuND0NLFp7Cn4HkRoh636Ci73GoUtighZo/o2W4RsaNNH9lhT/AEe09KtHoKfvdSGjoUUwp+BqavY3U3r9ZT4upoWA7Q+kPUUBzaWx3NL2rRzaCHY2NgP+7qdxTF7zm8n7DvNrqwj3v8l0Tcw+1727Y9jY+0i6FOhAudzD7CK2drxP2HEdD7A7HQ9rRsf4L/o0/qDxabMKWz/uD2FOxaI0FPNpbHY2NGNPuO17l3Nz6TQ1XYFzY/3bm12BRGj4n/V2B/Zs7XxPqIR7DaWOx1WFP0se0jAo2EKO9+w2mx1LNBH6w2Pg2O47w+ksdrwfuO8O48G7DQ+IbDV2BGn1HxXY7Cz6n+Z6DY6ENT3na97TsdpsSxT/AHO50fef825qB/U7WP7z0u1j6H+zye9p+4dhT6HuWzza3uSLsfrfU7mxGHNtYGrRzZTkwjsPsOwiUw0PQWY/acXm8NEe0pufwNp2MKOb4Gwj/UuHY7j9pc7Fi6Mdqx5u4Lo8259BD+b2hGil5tBT4FjwP5FilhRRz6l5wxwefI+Y01PmB2R5g+YeYXNPmBjR5jLGrGKrSt3vfqdrGKsYqscrd9C/0Id56Tm2nefQ9ybS72tPaf8Ak3m01NB8T4rRQ6EY7Abv9kjGEIjGydh+wCizTBpHYwdWj7jRY6NiJCmERo/YxaaYwGGxKLFMbJ8HsaaYgw2iQsxE2H6yEMAFNjYkEob4PsabkAsl273FH2tMAC7nQs3Gz/MaC5CNyz2Opo7T3MDUHaUjHQsanuKSz2I09roUJdo+opoLsGDT6kWz2r8z3JEaadSxQUx+4jolxjDV9Sv6yiY6RgQ2NhuIuhD9RmwBFrIl2NOgliFL7yiiwwpYmRHa3IbSn6xojREKdGJolOjo+91aYwWDZiWYNyFDsH9RqqqRpjHQpCDYs/FgwFoG5GzDVgUtn4uhSvpaaSMdr9hCiEVBhq6pZ0Io/UwhcaT0MKaIEKLH1i0RhmijsbNyBtKLH6V0JiiMfQXbGmdT3hDQIXRjCFyG1uJB+t3FwKJiixdofcR9BY2kHQjZoKdD4uxjYhdjo3IQs2feR1NGHYU3GEDYfSwLtlsQOJYpg/tKbhsNDYWWn9pYsx2lyMYkf6N2wUbCMe4+DDU8saOgUH53wACjZmx2MD9RRDtIrT6mmjYe47yzduU3PB+5jMxdo7D1PuaI7Ws6mjR3lmMfeOj2m1NDvfiB2F2z4ELlz5yjvbtk1ewuHwbPYXaSJoaJQ0WfoVXVsQYlFNy5tdD3rA70hYoaQIxpIxj7izD1ZpKLNMKYlGiUPvPSrsVwREmGzCF2Z952OikWOhGO0rH0G02tDAilENEhYxqQ+1groUNhjZ1LmMQ/OerOjCxBdhjRhRHYfnOxpu0UINZhYpHLpl+l0fQU3bIkFI05mB+JC6I0Q8DQuRj7i7CntQ9QRI3f4mjA2l0NCzGCf+XY+k783I50cv6xUosQsYCnV0AopufSBHQouXFoix9Kn1l24djCmmMG4WfrSm53FMHsWFh+LtImpS3Y2V7HPxIwu2TQ1WiAF1jTF+GbEPSbSGhGDMxc/UQNGBgs6MC7otEbPzGhYDtWIRoKLCOwo+pIwGPYlDoxsUQ+5gsbNyxSekgH7mNFiMNg7RpKPtcMe0GFNn0NPvP+agWYXdT6GBCHYBZgU0lwgR2P/h7UibAou2SmN2khY+LQU7G7qGqox/cWdoOhEoEp+s7CiNEadAKCh0aICR+52tGiESJZjR/FpEpG5Q0IaOgfaQs92djAuokaP4sxcgkDVsav70KSOoMwMdDENGj9yWdClC7GysGJD9rZirZhqMbN3/c2G1iESmP3vodWLB/aek2vYUx5tD3Ogc3J5x5zcjYc3wgH8gx4l8XPMPAntecS82h5w5c5vRo97z4jm6Judg82sstnU5t5TchRzZzVY9zCjmzpBaI2NGks/re00LhdbBY9J/AU2hRGz2n0npLJGnR0aObMIwKKbNJsP7EENg0URinNoRgtGqUw0PsaTRpESLoDRGJCP7nRopucE/UbhiUQouNOj9Rc0exppO8g2P8AcHY6MEhZu/qPSN8XF0xDQufQ+LYopabOjYifwdptLBR/NLtk2kbqWIi3WH0EbC2G7YKIxg9ro/qSZHtbmhdhRG59ZYjGJDVsRi2IQU0ftKO17WFiJR/N2NzsW6R+xphdjdIFBZsujtPrNpojCmzClbMXsfpNhT6XRaaVhdj8GNniwjo6H3sKDU7DufqPSXOxPQU/U97AjHvQGxCP63UsWLDtSOwpafgj2kNi0xiLHvPg940XLoRufaXfQxINB3EH9zoatFJTCghsHR/i3B0djtYfW6GosNCF2kKbhZsfadi6BtT+L6CPaURs9pT/ABNC7o6LCENh874GiUwdDsKaPrLujowog3abn9ymmNJQJWdGz95cseKaP9nU72mz/Bj4PYwf5mpsLNkSGpT+w7xsLZuXf4neOrTdhHtfinoYex+17H0o6sH73R2GqGxB+Y/1bvYw+0sWNCG1s+g5spT2nqf2Z0dDa2f3vi3Tkv1MdhG5TTZ2kP7nebE0P1lyzQMYR1YbT0vuPBsXCJYs6NP2MLnqTeR+L6Vp8CFn7mh9BdhoGg7T9pcjR3joR+t0YQbJ6CxsIwjDm2FOw7H9hq2fSxhHQ/W7Hg6DH+Z4MKfSw+Y7XwI+kjHV/e+JZpjY/oPY+ouU/rO42MbPg/zfUmpG5/dseo/k6mjDtXVos/yY3fUmjCi5/Rs6ELu0/YbGGx1OxsH7imyFnR7Wz/V8n72xBYbTsP7uiWI3KIXf7m05tB4N3zAgAsU+Yc6AWAgAAAFAd55gbSeYG2vFOfu85E5vj+9/M84Z+LovccDm+Nn97q/5nPUO05vBc5uxq0c3sPY3PMBWTsPa+YDyP5X+T3HA56J/9HmA97c59Duebe+17Dn6G1OfGeYCIvPve95vzzfHQ597z7iPc9zzdzm8naeYCBnN7Yegsc+k7Xm4HpObOd5o/wBTnOPcc3p/iXef62eb20F38h9rzfnxaPQfwaOb+6Mdh4Gjz2XzAQB5vx6nac247Xm/K2HsObo6HNqdj6Hmyuroc/h3HN4PMD4DzEDU8wPwPMCZTnLnOJOf8+YG+HmBLjzoXzAeh8wEdOfa+YD9PmAyD5gIKf3Nhzh3zAfA7HzAYE5wJ5gXyeYCCvN3PMBFzm3vPnfMDkjm9vODObs7HQjD7zzAQd5v5+R5tDHn+mpzd3vPMCbnzB8T/6E="
            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            String file = GetHojaOptativoPDF(_pdfFormats.HOJA_OPTATIVO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "HojaOpt.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //8
        [Route("[controller]/hojaresumentest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestHojaResumen()
        {
            BpmRequest request = new BpmRequest()
            {

                MontoCredito = "20,0000000",
                TipoMoneda = "Soles",
                MontoTotal = "40,0000000",
                TipoMonedaD = "Dolares",
                TasaCredito = "12",
                PlazoCredito = "60",
                SeguroDesgravamen = "ConSeguroSaldo", // ConSeguroSaldo | ConSeguroDevolucion | ConPoliza | SinSeguro

                //pagina 2
                TipoGarantia = "Liquida",
                FechaTransaccion = "30/03/2021",

                NombresCliente = "Juan",
                ApPaternoCliente = "Perez",
                ApMaternoCliente = "Diaz",
                NroDocumentoCliente = "47589617",

                NombresConyuge = "Maria",
                ApPaternoConyuge = "Ugaz",
                ApMaternoConyuge = "Sanchez",
                NroDocumentoConyuge = "41528579",
                ImpresionBiometricaCliente = "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"

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetHojaResumenPDF(_pdfFormats.HOJA_RESUMEN, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "HojaResumen.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //9
        [Route("[controller]/cartillaAhorrotest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestCartillaAhorroCuenta()
        {
            BpmRequest request = new BpmRequest()
            {

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "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",
                ImpresionBiometricaCliente3 = "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",
                ImpresionBiometricaRepresentante = "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"


            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetCartillaAhorroPDF(_pdfFormats.CARTILLA_AHORRO_EFECTIVO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Cartilla.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //10
        [Route("[controller]/consentimientotest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestConsentiemiento()
        {
            BpmRequest request = new BpmRequest()
            {

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "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",
                ImpresionBiometricaCliente3 = "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",
                ImpresionBiometricaRepresentante = "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"


            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetConsentimientoPDF(_pdfFormats.CONSENTIMIENTO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Cartilla.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //11 problemas
        [Route("[controller]/desgravamensaldotest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestDesgravamenSaldo()
        {
            BpmRequest request = new BpmRequest()
            {
                RazonSocialCliente = "RazonSocial",
                RucCliente = "1234567891",
                DireccionCliente = "Direccion",
                DistritoCliente = "Distrito",
                ProvinciaCliente = "Provincia",
                DepartamentoCliente = "Departamento",
                TelefonoCliente = "123456789",

                TipoCredito = "Hipotecario",

                TipoMoneda = "Soles",
                FechaTransaccion = "30/03/2021",
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                NombresCliente = "Juan",
                TipoDocumentoCliente = "DNI",
                NroDocumentoCliente = "48757978",

                PrimerConsentimiento = "true",
                SegundoConsentimiento = "true",

                ApPaternoCliente2 = "Diaz2",
                ApMaternoCliente2 = "Sanchez2",
                NombresCliente2 = "Juan2",
                TipoDocumentoCliente2 = "DNI",
                NroDocumentoCliente2 = "487579782",

                ApPaternoFirmanteAdicional = "Flores",
                ApMaternoFirmanteAdicional = "Medina",
                NombresFirmanteAdicional = "Fiorella",
                TipoDocumentoFirmanteAdicional = "DNI",
                NroDocumentoFirmanteAdicional = "74544048",

                Entidad = "Entidad",
                NombreAgencia = "NombreAgencia",
                NombresFuncionario = "NombresFuncionario",
                EmailFuncionario = "EmailFuncionario",

                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "/6D/qAB6TklTVF9DT00gOQpQSVhfV0lEVEggNTEyClBJWF9IRUlHSFQgNTEyClBJWF9ERVBUSCA4ClBQSSA1MDAKTE9TU1kgMQpDT0xPUlNQQUNFIEdSQVkKQ09NUFJFU1NJT04gV1NRCldTUV9CSVRSQVRFIDAuNzUwMDAw/6QAOgkHAAky0yXNAArg8xmaAQpB7/GaAQuOJ2TNAAvheaMzAAku/1YAAQr5M9MzAQvyhyGaAAomd9oz/6UBhQIALAPXUAIZ1gPXUAIZ1gPXUAIZ1gPXUAIZ1gPlwQIbkgPlGwIbfgPuEgIckQPtOgIceAPiuQIbNQPvKQIcswPvrgIcwwPX9gIZ6gPerwIauQPYnAIZ/gPcWQIacQPrTgIcPQP7VgIeKQPk0wIbdgP0EQIdSgP77AIeOwIa/wIgZgIZpwIeyAIblwIhHAIbLgIgnQIcNwIh3AIafgIfygIciwIiQQIbKQIgmAIb6gIhfwIbTQIgwwIbuAIhQwIbfgIg/gIdjQIjdgIbVwIgzwIcxgIihwPwvwIc5AP26wIdoQP9NAIeYgIbVAIgywIaAgIfNgIZzgIe9wIb5AIheAIarQIgAwIalQIf5gIaRgIfiAIbswIhPgIdHwIi8gIbXgIg1wIaeQIfxQIdVQIjMwIdDAIi2wIdZAIjRQP83AIeWAIlXAIs1AP9owIecAInKgIu/wP1xwIdfgP0vQIdXgIirgIpngIkGgIrUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP+iABEA/wIAAgACVNMEQkUCAAD/pgBuAAABAwEDBgcHCwgQEwkAAAC1AbKztgKxtxESr7C4wQMQE665ursECA8Uaa3ABQYHFRaqq6y8vb8JFxiSk5mevgoOGRoci4yNj5GUlZyhwsQLDA0dHh8ig4WHiYqOmKOnqKnDkJaXm52foKTG/6MAAwDffff09Pp9Px/H6b77777777/y3/6/6/n/AD/lvvvvvvv6fyt/t/D9v6v4/Ttvvvvvvv1+/wDb+39n+H29Pnvvvvvv6dPt/f8Ad9v/AG+6fTffffff02/X9/3f6f5/u1/Hffffff0x/f8A5fp/X+r7v9/p6b7777+nX/H9P7P1/b/7/j9PTfffff0+X/j7ft/d93+v8Px9N9999/T5/wDr/wA/u/7/AOv7/wD7233333336/H+P8P4fh+H4fP0333333+XP/b4fh933/8APz333333/l16/H3/AIff9/v+m++++++/z+l/+P8A5/v8F9N999999+3b6Y22339N9999999999/Tf09N999/7p014N9HXrDRTDbPWV0HFDve1RNV7JoYAOvsh86cDr312hHgns7dkLFj11WlPV2nrrHxRIj2YeivFfb2UzWtpNu/4Aofnijbn1dK/H0/n/8AvxVd+mlY2qd5fo5ad8Wor9K75FFtVd5tRq6699arZOP6sTXbaXPYYDWnK72oac53ibxOdz2SFa7VXeC2OfTp31SwE/pk/O6hYZPZVxN6/ViO8b/JFsaf0PCoJ37Dm0U79lqtoz6mNPYnrixetUYnsoV1hn1XUoh9TR2s/wAkED79lP6p5PJ3BnXmgjsCwooflPPqykBaGFDwuWjTE6nJ60sUhU5i6nG/vTpSzUNSeA7puxKmoyZfBNS7uoiVONaFr1WB21JxrSKcCXkW04y99FShImicTVyWqKCNZxmsx0GKpeK5xnm9jExSJgVcb6q9o8S92yuOrjko2wq1aDw1wbPmBrJGjwNKa567WJcl68DXF3rcuwbjPAvWpqhyJsQflGg2UhMC1k8C/AZQqV+B+UaEFiBwngTgRBOBhHgV65Y8argS0tPK3kkkcnIqHTrKCsIzBuqOlLGxiQzGyItcRlCqkwVZ0zKamwi1OlM8alFKkKXmZKMUgwCnIvLUpbDMyKpLvWo4ZF5dY7gvKzu9Rqrmt9HzrKopzjb15rneZPr86jQpUg8q+RQK7WL2M6qPRTnaMDLV7R69NrlVDiW1tTVPVMyi5u2BDhrjHGTBUaMYmDOIpAIWrV+UAxzW1GNLvk4prVHS8ucg10MR6yhHEg9XXaPmdMZ1wirGNj3T+yeV8eraNysWmQh1te+kqMMoEihVrzlVVD16NTIwYCSW0fKjEU4TVzlL1FFS1VUpkDg1GjptMnJV7MaCZ1ahRCuXvfIUmJa7kvXTPETilq2XSZ3FLyJvbGL50M1aqtbk1zqWGUsRX5YvT6Rtq/CcRayleFm8TMAKFLRScZKMWlopM5MApEa3xt4uQB2JUylpK7hNbeMHaScJnERRF6q0+uT7I/L9oVcKkijE8oWLQBRTjrreEBSKjN1O4QicpEIVCkUzwAopFSkzJfBWBUrsU44Aq8ERAZU6IODiqcqvXCOsKkZ7c4IscGZ1ppTVxUWmUNXKkU1lWnG5pelJFhA5x0WqohQYlTXi0VqVMSalp4EixZpmrMXngWk2WJTaL1PA7uiaqEFeeAoKRXRI54x6CLDV1PA4MQp3VnPUKWlGcliCf1ndLHl7sTKnqp5gCbJIbVm4qQpHKkU4u2KaKQLrnGw4SQHM9dkAFoci6ikk7mjLVOHSKUoZi6rAxUmnUyi1dFir4inGXvcYxRQzmt+v/GO66LTjH71RESgRlOvbq1E+6I4vGlUx6VdUSPwCmpOa7QwocZNcD7P4AqRk1SnNt0to9TkbSWupbGTVJ2g+6eNUIgHjLNFKNQk9oKFUh1OMLMdofmJiCEeWrO4tIeJShWw3KM45dFndGJyJJaIGTMRgFJKEZuKKSWljOFWJwJIjjTgroq0oykaYItRtjOh0ohRlXM6KctFpdR5MqKlUNudw5UOq4dIipxFO3nOEBx5BOnwd5KUUK4mo7SIhGHGIFKRGqPcEkVNG2UtTiIipJyiB0pCKcQUQoIcVScRRlgtCfWKKIkPdIixJnO4sf1zy3CewM5aeopVEZQIIWEKvGJ0iVVV4wQoeqjlK4FjgZjwoqswUzgWNgEPMvZl0JrFpxJHweI0OGVLrWsWIoIymxFJVNS0ykw7qHdLHEV+t7U3FJUyrHn2QSFqA8DXsyzJaGcl7kHKqU84or2gIcRxksNSDlM5UCgUqkk4wsIiCOop4ThU7nCvzCESme5iFET2iFD3HrnleFpJJOUl5evjfnz0052kyO17mGSuBOMWiBx2kzkkrR2lLCPElCr1EzwS/dTkrMhsIE7oPEnORQcUzvauy2dhpmWPkLQRhaxysqhrdrVzXS7eeOirpYo2m2pl0vFUa14Ghk7xmk12om3XOlJ0xXR0jTbIvjVfK/Ppi+LmmbWHf51Lq9tp4E2Qnzv2+ldJroZEl5t219/PzUmd+nTrWvmvl1+bkzOej01+fy9+NPhmW6VOfb6fDbXz67ZS0fPpsvj58/kao8Qw1Wyc5HCqKj9tdwjyvT80tIsaXZ1rgcSSY87mOmvxifEjtMYx0muvKPBaPXnKrHjC6hWxqVnNL1wj5yu2JlOmNaJVbVt1JyZBXcRfS+RSiuKSPnzrlFsIaxNdtsoChGKu3y665vGyk1hfHyfwOIOqvzpTz0116YyHMdeyc27YfScZVq+S0TmjqnkNVdixVneuIYvJUiiqVMpcmLSosK5xJh1BVwCZBpV064YUmcpqoqjFPbC9SS0IchBBRAeMURBz2licCf2nlryMfdJ7a221cu7xlp4Q5306XMIKnKbfT+eLrHADOuc10YgmzMhxfPtJE6imRMfH8ejqBUnjpPPzkHYq6OMi1VV3VYyrsroLottXKryNS7cnIhNOmynPXm0ZE5q6uhVQPJ6O9huVfDzKTV6ve9Ver8YpRNdta6Ptpg5RbR1jTzXn8+UpaF/izp2+nyxxiilQr4Oq56KeAGnzTnTpi+vwhyxojE58+p1xmK+3OF1j3kZxlqnWrv311v52njpJKrUx9ZUWiwpyfCRRSe08sMTiK4HEhCTba0Uk4tFLbHvveQFMtqh59p79b87mcw49bvFGFWdTzF5nXGxK5U+lvh53von4xU9tZjBjtRyk7TrswNWplEF72wsY2V+V5fSSzvORhuUnJYh4kXXZSo6Rmyz89emFr/wAvMCVj5Udfl9NnnG9pNJd/CjMDp1DhO2pMovj56xXlFS0yrExUTs4nnIcCkWhOMtfCkkVypkWogPHPAj2xAalwJyCqY3PbOBE7TxD1inlrCklCih4hRV7yICZRHAVUtfEndIjCnIALVI0+URwd1JdmdUqtVF1tVZxHCOI02qswwS1O/a95k5ha0mi/xes5F05Wp1UDlEVKWvy61RnVppLj02LucSINZalOUQtj1OYUMjXbFKKkRScYOBFFKtjKKsK5JEWrk/UyWJyDgRCYciRSCuz3FhCCx7imN8J9kBByk7gnl+nCoe52ALSZyxwBD8Rqepwp/WWERPOJt2pjljOAKoXLHKd1Ym1oTPFwlXwDIZ1bCvtTmA5DhYu4yTOSFjWsS05UpsgZBVyg6iEBMx0RSiItMynUnAUpGcqO0ZKT5FopFUklchUz1VMoi0UUKWx4lg7oZncQ4RHtlKRolq+sgp7j1nl9Sx9gnwJGSInJqHSMup4zu53JWKtOMida37dey+JMzDtG/h8fjV3nObDRytsbIzmOlunXXHPpz1YZXWAdLHnOimY2tMKCGGQQOimNHWJMgs8YnDBjlSdhX20veGU4Shdq2+dKuQoSF+eiqs4oYkUtEZSm1LRSpyL6UFgVJmdddrEUgmZxzgHADjEWbtLQgZsREEU+yRMh9YThKTiPEtD9E7jy3yeLsKRzF0chUIixezEZSwigLVyHYFFJSmbdWNaVX6PxGpHhkZ43qgRtcqoZ4eB0tr1znJ1RBTn1sQzFSjw3aTKThRJ0iZOUTjieKMIypkIhlIuZRN3iJi0MpwvEQjT8XSBCAqZyWkT4EfixFWIKs4IQiIdGRYoTliI8aTCcGp7YCdj7ICEEnKd0R+iWPJv/AP+mAHQBAAIBAQQCCggLCxcHCgcAALO1AQIDBLK2BQYHCBITFBUWabG3CQoLDRARF2oMDg8YGRpKS05RuEhJTE1PUFRVVmCwGxwdHh9FRkdSU1dYWVpbXF1eYWJjZK8hIiQoQl+5ICUnKjVAQUSuuiMsLzE2P63/owADAecRmxBXQPU0YNjuYUQrM6G5uxpzDgozMZkN6QYMIO5zMilEcQ8UpGILuzTGmh4PSMC6niJ/6rDj1/4nUwXcR3D0IQTlk/C2Lm96wHp1xRQeOZ0x06z8MXDwZil6Y/CFjdiZhCYes6HirGY6YrHHMejnGenU5YSxSPi0IGEjB4HaeAU0fmaOfyDQ3ParHeJZYY4ZsUiHJIFMPWuaXgFwYLvc4g6GHxFzcyVngEaxEnXc4xMxhFjHemaYzPAFgetoYMbI78dSK4InBOgOBfWFMLG9wRCJQbiEKT19YDgu8TC4gpuNcscnkzEXQ8C4ufyMOe+eRRiAU2dwxgw6rvDUQeChdmIblHCDHipG+MvAizo0QhvDQmcDvMBMYRjw/wDUyHTLY3oqmQjDxFfwpIRHcdHJAIOcHgHy6UosafB6EbKZgviDgtjEcO/GWYbJl8ShixMDxzjJCgmHeXAhHkGYkXyazHDHc3Sj8Zz/AIuntQo/EfiabLwGEWsVjdihiQsbhoYJGJ4uo2N5jQEscCyHtLJMnAymKDA4dyTMKITBubN2ZN+OoUK2PFwzDGz5BTYycWi7HewiXxHPEIRix4CRzR5YHRp4JEpo4thX8r+R5+7sWGODZ2LuLECzybBCMIeJAgXc8WmEDggMQjA3ZaYRDioUnS2DeWcsKXcwFopMG5pAbu4M5gNgp9TYGgjwJil6OSMHwMUYmGMQ3lOWCvAIBG4+LTboYFo8CEew4N2A7myVgI2ODGswj6yye1Ofwdh7S5+Y4ly7hDgwhGEx6y6nrIwCG8p0Xe0Nmw7yiEKTiQiMMU7mJ1IUm8Z0buh4EGZjRRuGNiGGB4lsGw4sZhRE8SxWIJF4FEX1lwMj5NkjjeaOjyaCL7Szj8Rz2XteBG5/8PsabJxULFO8Gzm2Xc2WK9adzErDFXegNdY9civgzr1dcHFchFgcMw65Ixo4PTGBay8sGCNiG9gRxHIb0xZBKDxSwkYsfEpZiEeWbo3yeAIYMoXeLFohje2GFEHiVlsespaOT/kc/wBbP/6WH2rH/s3bLH1sI2dxDR5NDtN4uGFgjuKKYetcupHflYEbu92FCb+pRQ/lKHxY4GZGExwVoobPBixoXyNGBvIQ06exH8hc9pTz/ROLQ6I+TRBI7kCyTJnktkWO9TXFO9ixoyU7koMjF4MwCUq7xiE6uIbyKB1oeOIjfMThnqUxLYfAY3CEYbkXNkp3qFIMOGcQFjS+JoF2jwYqxEVd6MQacnAhCEIcWiYjQnAiwjH2PrOw5/xR/wBnTNg4pGhTgwuweBM0QLBvQjBLO4EUbYXe2JisWdxRoTPkxKQOLbJ60bC2Nxl64s00eKvSxZxwIwstC+JGKzNHJrG18Cls00eLArL+MKLBwZmJZDisdX2D5F2x5gI2INZ4qWy2OBRBROCOKLGfluZlikxCY9eJhmadxGLgSx4thTGFwG8gzM6LMcCJmnCY3tmhyTHB6kCEaN5SAjHctCxiWzwTQseDBGBo701Sz4tJDV3DSGWJvaw6seQEEeDq50eSFD7XzASMhvLlkmOJEIRM8CFsqmDcXYKLwI2YMeKRbnAuEOtne2M0uCPDNlHExwCDCMzwxFhAo9pkscVLkB8QCFBiKbghM658SxMQA5ZTUi8CMBzgg72zYsbxYsA9ikI+SXBj7GP5HnsO0i+0svtWleBQ3yHFpVmA5LEhyWi7yLrYOTZGscCmZoZg5FEHMDfiAWMw3iKwQp35zcaVPE6aFNjxVgWyqcFpzHKu8wKNk4KVlzTyCwzMMciINHrFfxJTTHc3YL+N/K+YCOtMf8nD+MfxlD7QscCFEYFG9HUfWQLu8KbHkMWJHyYkGEeKkwrGJvQpoj5DBX8Q6PEWw0HAhY0fJsR8miH4z/Qh5HtdTzASBeLTcj7FwXOLQKsHgEAPWUYgKR4NmEeK3MGCHDK2bPJzY0NwDTCHFIRsQmeA/LKTLHgGMMTEVj4JnVgsNy5cFEY+BMpYCHtyhD8SmYnFVsAu8ItZSHLOZiiLyIwX8z7Q5/J2n/4aHsdHydFCZjwbEaDiO1d6NAWA3ugx4JCnExTDcxuUU72i2YtG5jZbJwIMQION4t2jkDCmI+DAUgw9eGzGh5EIxDg6Dd5FkXi2S7+Np9rz91gRz165y2fHPVIRs7wCGcTEOAM/99IjnkfJ+RoY3s/4/D5DFIviVj5ZonXkOM/IrNl5YcmmHxflOnURKIeBl69UALG4nSKdOmMYxjfjPybJTvOhYHIvTcqvynTrOpxIrkmcvyxH1MwmZnGX/D/jc0QxmdPw/D8Iu/P+JmdMdc5+Ud/ToMzOh0wb3OWkz8s7wrHSsfLpWDg5XGH5ewzCKQOTQ55Bc57ucEWFO56pF0XxGnoZIpHxxEfkKYXeU9chAo3dcP8AjP8A7/469flld/SPSzOnTgv/ABmfgQwdcpuzmdSEc058SjGZmxDHidUcU2Y7ugYMYQJ06bsu3Lnr4nSgMGOnT/HBSydfl8n8N4zAW69M/wCM+OW4McE6+LDFFjDxx0xCGmN6WYmX15rNf4yPBjEzOuHixYwXeEbIkeDQvsMbHneAAHBmClzHk/8AGejhYR45w02eLM2Ix4kSx5Ey2yGDe2czICjvTBZYTry/DGY+TnrYskdwQMTB0DDvIZVbZeOIpBgG5hHDEDDuwHyWsJB8WLAhmM6nl0yh0xwIQI4j1Dc5rKB//YY7zJDHyzky7wGs4jk68FBSf4HiDbo5yY5EAhxb5ZjnuPsaJmF3jmdDGYsxTuzS5cU8CkUFjw6wYZgeWK6pgcu5hYmbu/AuYxsblI5IsOTscrvyEKaOTHM6URmN7ZBhiPtzZh4LTWEmYG9IZmcuHiKDSkN+KQbEN7GkoD2Inyp3lIRwmeJfEMdT1lGMnsGlPWK+t59C8mGSiPEunSsWPAot+GCGeQT8JnrEzuaIlNYx4pGGA6QM43uMTDR1eAQixj0Ibi4mTqjuK6asycCCzAQzvWsuFwYNzC2MdVnR3qqCvXPBMEZhgj4Geq7cG46BG48mmyDybivIRrLY4BMNOT1rqHFKGPE7Dn9kZh9r1Pw+WOKLfrhd6MTNDDg0iNOHcMJiYZ8umdwlNZDDk3MEMdcZ6T8ODCDnJMLvLIZzjq7yhMuMgZ4IApl64dxbqiOeibyEIVhOCR6kw0vDrM0zMdxMU2c0Z8cQQHQ4saFhRvw2YGeTWWkhuKUITFPJaaDyQopN7TQQ5/REpKdwAxMljc0ZSEOARsTDDkQVFd+aBKeRTWZkjvBjQGSG9ikcNByVguMG5aUsZjuKFZnC5N5oMCneiuILE5DYIx4OIkIw4ESm7xcjCPJskUzxex8iiDF8mI+tjGBd5FFHP/fxFsZOQxx0fJ0OuMvALJTnDuGFmMyx8TMzRHr1gpuMUUx5ARWMeJOkHNY6dTjjKwJk3lZikcxPEbYpgdcby2fkQepnk2KzkTwLoYY2PA2sTe1m+S54tyJYYbizCOSPka5OQxoY+LYijGDwGJQsxuNAse15/JTMY6dOmMcjMSw2fAzOpWIrxcGSn2FlaXgmY0uXgtxejmncRwnVpHc4mYBWVdwKjMiu9aQhQdOBM0kFhudG4bhgwHDB3MJmimk3BGiFGXk4PYJDEzQuPBg4Ry9CBje4gTPTD1OCYCukc7mzWcwIp4NJMZoxTxDR4tgpfxFPPZazQTNG5i2YWdzHMXI8skwls8AgsCsHAzgoI45DgXEyPDDkzWVg7jEzCNsdOLmAxhyJjFKkc+LMOWkLY4ubNENxGDHJXTyMOUenJZghnPWZ3i0XzM7muixoxnHizpjEMQiRdxGgbHIhMZjMbmnKNgwcGZgr0fY0YH1h5gICB7CYhQUH/wB9WjV4MzGzHgWCw8WJGOaDgXCsWN2WMAAzHxSwXwcGmjQ4EwwytdeDYgUeQwzBicjLAicjVHcaFGDIcCs02N7hosTL4lsXbEdzjLTGHJKKEeJGxd4jonkQpORdiPP5bOpuNCPIs2IHJppsQ3YM5oH1uIMaOBiZAFQ4DoRI+JQAtD5H5GxdV3tmmYjvbNHrLmq71sFDvLmo8RhqG4w0Ux9jDEJg4OSnMwexYtOPZmxZ4AMQ/In4iHPgBOJWGIZ8iEaccGA0xhxLYsRhvcOGkIcTILFYbhwXVxngwMlAOOWGyYhvYUkQgeLQ3YcimmzwYkW4u5ocgQCO9jCkE3Ec0RIO/DRTCMdw7GAHArpGMOQ3zCPkQKPIhmmjksfMBHiiP48GfJGdSsvEcVgz8p0TgUxz8npxy2wdJ+BDe9WskDoUbjAbENxTmYprDvzM6JTwBsOXfhtimk3mgQ5NlIKY4EQGsYhuCBjFNYN+Z0mCMyQ3Hypxi7x6xzFhQPgbSZpdzWLohwZizYfITI08ksKby7D1nmAgZoezMX2FYnUhyV1OKMMRjDkQIJF4BBGxHeR0aXyLvFWxd4tgY1nO9iGoexjZdzdAp5MIRHPAp0QNzTDRhvGxq/jfWjGnybMfxl0PY3PyHmAjLq/mPJjb5GfIgdIWeBkZ+BRCncN1R4CU3fW2Up5AamdxZSxHi0Fld4QbDQ7gNFmOI7Vj7Eg7mmxcTkXYwOQxGDwEuTKcMXVhvKUgWZjgEbFPJ1I8gi+YPXj+U734n/IjHYNjtP8AdLFnQ+x8n0lFn6ze6OjEbpGPNmdpd0GCXO5/qaBTCAw+JvNBglFMGiP0EeDGjU1GIzNPNpYJYY07X+6MbtyI4Qp/c7SIwuiJkJhjqe5udg7GxcbERKI1ij4niMaRGOMwSGr95EmYIUWMwuQ/eOxRpSJnCJCZxThYQ97q9g64awlOxEu/YnpbAUWLtMKT7WZhTqOgMYiJRGz9rR2rowSOKKYUglD8G5tGKUWUTJYusLJ9aFzsaLYzRTMxPsdGkjsREgZSMRLpRqfQiJYjZsjCBZwkazZs2H3NizsLMI4YEMwcWbGdr8GMPQFNBSTJTBw6P6k2tFIipgoxGYMnaFPuRppdCmsggUwjBg4LtP6W5BsQg3bC4EKYU4jFpWJ9DSDY0NCOcWKQWDTTZNH87BDMCHYMxWbhEMuKw0WI/BhCMHVg0RsEUGGSNDQe9ovmYYlzNlwAwCxmkW2fgDSsI6Fw0CiEQbv1FZoCNjJYozAGzRTREaFue8IWYJqg0URjMTKwiI0WH5m7opm5mAwxQRQ0xqwPgN2EyaDRZUNGGaV0X6igilMErLrmAwzRZCn6W7HQiUzBGmgQcwSFikgfA1abEaMaNAMGCQaLvzkIRKbiQuaEbKYhcu+40dXRjDCXKbkdWFj6CELOg9rAG5SUwiUMfpOxIU2EiwsZoTtP3sewNQsRU0Y+9p2kaKBhGYoGBGGw0PnLNylxWaNFSJhCBYYxhR+gstFzZhNSMGYmYMGsRoofcLc2sGizhazgGCWLmg/YZSCQSwgjYhls6B8BuWI0MbNOrSxopiie87mmkopomSECMzGIe8uwu0GwS52I7EKfrA0IO1s2Y4pjZFpufOAwppHYdjEg6ZhojH9L2DZLsbuxgjCyRufSUO4IRZnwD3hdpo7XQIF1Y6ie4ihoNgIwp7WjQdp7g2Nm4FnaupdpPebUp0LPAgtKfWR1ItN2NNnRo/YaJYos0NMbtI6ET4JdYUNMKdG400li5B+hHRs009zTCNFNMP0g6MKGEbrRCz2lOxsfnbsE0bCbG5qMKY0w+ppGPbi7YsLGmhpIlHwSi7EjMdoGxKDYfSurTCjYzO1LMI0n6H0JRRRTFCPcNECNH1EaCBCjYQ0YNhbMw0sPndXa6lmixZSy6n0ELqwdgWW4dhFs/FIelhc1KbNiJ+h7gh4tFyCWbIR+l7TuLLZoGDgbFP1NlphD1MW7oWf7sTvYbD4mhouoHoLEWCfMeh0CliwohdujCDTD7TQLCEKI6BCNgmfc+gsCwhRTqsFY6v7GFnU7mFy4x/mU2brZpBbmh8x2jddpcbigUUsLL+k9LZrFzQ0KIwYOh9wU2I0XGmlp2MPsfSwzsImpGy/cbM6MLl3UI2Ian6D0pQQpsU0Me0/Y2FLOpSqx0P7Nku06Ctjm4sWiH8Hc7Gjm5ng83Be97T4MTiR0PMHvf/+jAAMB8wfQDvP9j/oav1n/AMnxf9Q3n+x/qU6G05thsXuObK94c4o/abzR+o/0ebq+RzbnQKf3n5nRoDm0tg0XsI82l2OwuvuPzOxdrY5uroBHm7BYwfrd7o7GnUPnfzFNHcvxf8jvfoPFdpo3O4P7Hc0bG5873sNWxZ/q8SxZdgbCLA+BHYbw2NiMfe7D1Fil0bFGw960tjcR1ItOr+1uXdCNLZs7T6zcbAu3bHpP+5vNrA0blMNGPzLcpbmrtNF7GETQo/8AC6MLA6ujZ0dBdr72gooI7A1Tubn6mNzuCNmGrT6F+tuWfW82h2ugXSi2Wn9ZGGq0UeLcs/SrRqbCweLo+5swodCL3FLuX3sKIxjZuFEF7HaWYfMGova3YFg2HcfMw2LowsUUNmnYWfgwGiKws3UsujCzY1PpbCxVojwLNml+owWWzZ9GeT87QEdCgo7Q0PF+4COqwpdhZ2MX9LZpjfNPaxh2Aurd+dtmxTRq3Xvdr7ywQAoKCxcj5PzmxhRYo1YbmP0EL5W6w7GzRsf3BEYUxs/jaQi/QQMMCOh2NwsWdClfews6JYhqvBuWP0K2VhQdzTcgRWLcNT/Y0aKI+hojRxPndDtdBVpg3IpZ2H6DRs3KLr3B/J7QhDYvoSwbCHxLkaDse41foewp7QIEKKNobV+kNQLhCYoxcIdrYPmbL2FFkopCzQWPE95dbK7GjYtnRX9pAH0GrsfuNCNjUNW7TZjoFH0uowLBtaAsx7n9B6Dc2LPaar+g7SZpi2actnR7CLCj9D6l2tg0aYtPYfubnqLrMGqw+Z73UKbLTA9JHLY9x2F25Cs5e9u/oae4osO1aYUw9C6n/c4kXaLqvofiUXNCGpoU6AfwbG3FnuWjaH53af5vaUfnfSxYRhGjvW5SD97Fswph2ENW5727TsLnoDsy/MdpSvaGq6GrYX9poroQ2tNNFyH2gBYs2dibSiPzPeQ2hq2I7W59B4l1hcu3M0x+D7VbNESg0KbP2G1oofU096/qKNSAXNCixo/wLHYUfwPyho3P7HpAopinNuWGxhzlz/Z3PYdzzbiLzeDsfMYkjzAk45vp5gZoc4I8wjUfMEnDn8HmCOJqc3o8wGgP0j/mbx5tRwOf8/1ecqaHmA+Z5gIinNsefWf9DzEuk5v55gMQc3sPi/ie40PMB0TnNHaek5tzToR0dXm1L5gIMc3psU3f7PqYXebuQObmx2OgXObuUUHuT/qbX73k7H9zwe40ObQEdWwbW797AurTTzcTYwj2vzmp3vgbG5zZSPeXftV0e83H7jRhY2BtbNMH7l1O0ubza/e6NEYdq6PuND0NLFp7Cn4HkRoh636Ci73GoUtighZo/o2W4RsaNNH9lhT/AEe09KtHoKfvdSGjoUUwp+BqavY3U3r9ZT4upoWA7Q+kPUUBzaWx3NL2rRzaCHY2NgP+7qdxTF7zm8n7DvNrqwj3v8l0Tcw+1727Y9jY+0i6FOhAudzD7CK2drxP2HEdD7A7HQ9rRsf4L/o0/qDxabMKWz/uD2FOxaI0FPNpbHY2NGNPuO17l3Nz6TQ1XYFzY/3bm12BRGj4n/V2B/Zs7XxPqIR7DaWOx1WFP0se0jAo2EKO9+w2mx1LNBH6w2Pg2O47w+ksdrwfuO8O48G7DQ+IbDV2BGn1HxXY7Cz6n+Z6DY6ENT3na97TsdpsSxT/AHO50fef825qB/U7WP7z0u1j6H+zye9p+4dhT6HuWzza3uSLsfrfU7mxGHNtYGrRzZTkwjsPsOwiUw0PQWY/acXm8NEe0pufwNp2MKOb4Gwj/UuHY7j9pc7Fi6Mdqx5u4Lo8259BD+b2hGil5tBT4FjwP5FilhRRz6l5wxwefI+Y01PmB2R5g+YeYXNPmBjR5jLGrGKrSt3vfqdrGKsYqscrd9C/0Id56Tm2nefQ9ybS72tPaf8Ak3m01NB8T4rRQ6EY7Abv9kjGEIjGydh+wCizTBpHYwdWj7jRY6NiJCmERo/YxaaYwGGxKLFMbJ8HsaaYgw2iQsxE2H6yEMAFNjYkEob4PsabkAsl273FH2tMAC7nQs3Gz/MaC5CNyz2Opo7T3MDUHaUjHQsanuKSz2I09roUJdo+opoLsGDT6kWz2r8z3JEaadSxQUx+4jolxjDV9Sv6yiY6RgQ2NhuIuhD9RmwBFrIl2NOgliFL7yiiwwpYmRHa3IbSn6xojREKdGJolOjo+91aYwWDZiWYNyFDsH9RqqqRpjHQpCDYs/FgwFoG5GzDVgUtn4uhSvpaaSMdr9hCiEVBhq6pZ0Io/UwhcaT0MKaIEKLH1i0RhmijsbNyBtKLH6V0JiiMfQXbGmdT3hDQIXRjCFyG1uJB+t3FwKJiixdofcR9BY2kHQjZoKdD4uxjYhdjo3IQs2feR1NGHYU3GEDYfSwLtlsQOJYpg/tKbhsNDYWWn9pYsx2lyMYkf6N2wUbCMe4+DDU8saOgUH53wACjZmx2MD9RRDtIrT6mmjYe47yzduU3PB+5jMxdo7D1PuaI7Ws6mjR3lmMfeOj2m1NDvfiB2F2z4ELlz5yjvbtk1ewuHwbPYXaSJoaJQ0WfoVXVsQYlFNy5tdD3rA70hYoaQIxpIxj7izD1ZpKLNMKYlGiUPvPSrsVwREmGzCF2Z952OikWOhGO0rH0G02tDAilENEhYxqQ+1groUNhjZ1LmMQ/OerOjCxBdhjRhRHYfnOxpu0UINZhYpHLpl+l0fQU3bIkFI05mB+JC6I0Q8DQuRj7i7CntQ9QRI3f4mjA2l0NCzGCf+XY+k783I50cv6xUosQsYCnV0AopufSBHQouXFoix9Kn1l24djCmmMG4WfrSm53FMHsWFh+LtImpS3Y2V7HPxIwu2TQ1WiAF1jTF+GbEPSbSGhGDMxc/UQNGBgs6MC7otEbPzGhYDtWIRoKLCOwo+pIwGPYlDoxsUQ+5gsbNyxSekgH7mNFiMNg7RpKPtcMe0GFNn0NPvP+agWYXdT6GBCHYBZgU0lwgR2P/h7UibAou2SmN2khY+LQU7G7qGqox/cWdoOhEoEp+s7CiNEadAKCh0aICR+52tGiESJZjR/FpEpG5Q0IaOgfaQs92djAuokaP4sxcgkDVsav70KSOoMwMdDENGj9yWdClC7GysGJD9rZirZhqMbN3/c2G1iESmP3vodWLB/aek2vYUx5tD3Ogc3J5x5zcjYc3wgH8gx4l8XPMPAntecS82h5w5c5vRo97z4jm6Judg82sstnU5t5TchRzZzVY9zCjmzpBaI2NGks/re00LhdbBY9J/AU2hRGz2n0npLJGnR0aObMIwKKbNJsP7EENg0URinNoRgtGqUw0PsaTRpESLoDRGJCP7nRopucE/UbhiUQouNOj9Rc0exppO8g2P8AcHY6MEhZu/qPSN8XF0xDQufQ+LYopabOjYifwdptLBR/NLtk2kbqWIi3WH0EbC2G7YKIxg9ro/qSZHtbmhdhRG59ZYjGJDVsRi2IQU0ftKO17WFiJR/N2NzsW6R+xphdjdIFBZsujtPrNpojCmzClbMXsfpNhT6XRaaVhdj8GNniwjo6H3sKDU7DufqPSXOxPQU/U97AjHvQGxCP63UsWLDtSOwpafgj2kNi0xiLHvPg940XLoRufaXfQxINB3EH9zoatFJTCghsHR/i3B0djtYfW6GosNCF2kKbhZsfadi6BtT+L6CPaURs9pT/ABNC7o6LCENh874GiUwdDsKaPrLujowog3abn9ymmNJQJWdGz95cseKaP9nU72mz/Bj4PYwf5mpsLNkSGpT+w7xsLZuXf4neOrTdhHtfinoYex+17H0o6sH73R2GqGxB+Y/1bvYw+0sWNCG1s+g5spT2nqf2Z0dDa2f3vi3Tkv1MdhG5TTZ2kP7nebE0P1lyzQMYR1YbT0vuPBsXCJYs6NP2MLnqTeR+L6Vp8CFn7mh9BdhoGg7T9pcjR3joR+t0YQbJ6CxsIwjDm2FOw7H9hq2fSxhHQ/W7Hg6DH+Z4MKfSw+Y7XwI+kjHV/e+JZpjY/oPY+ouU/rO42MbPg/zfUmpG5/dseo/k6mjDtXVos/yY3fUmjCi5/Rs6ELu0/YbGGx1OxsH7imyFnR7Wz/V8n72xBYbTsP7uiWI3KIXf7m05tB4N3zAgAsU+Yc6AWAgAAAFAd55gbSeYG2vFOfu85E5vj+9/M84Z+LovccDm+Nn97q/5nPUO05vBc5uxq0c3sPY3PMBWTsPa+YDyP5X+T3HA56J/9HmA97c59Duebe+17Dn6G1OfGeYCIvPve95vzzfHQ597z7iPc9zzdzm8naeYCBnN7Yegsc+k7Xm4HpObOd5o/wBTnOPcc3p/iXef62eb20F38h9rzfnxaPQfwaOb+6Mdh4Gjz2XzAQB5vx6nac247Xm/K2HsObo6HNqdj6Hmyuroc/h3HN4PMD4DzEDU8wPwPMCZTnLnOJOf8+YG+HmBLjzoXzAeh8wEdOfa+YD9PmAyD5gIKf3Nhzh3zAfA7HzAYE5wJ5gXyeYCCvN3PMBFzm3vPnfMDkjm9vODObs7HQjD7zzAQd5v5+R5tDHn+mpzd3vPMCbnzB8T/6E=",

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;
            String FingerprintImage = ConvertToBase64Fingerprint(bioFingerprint);
            String BarCode = ConvertToBarCodeMinuciaFingerprint(request.NroDocumentoCliente, bioFingerprint);


            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetDesgravamenSaldoPDF(_pdfFormats.DESGRAVAMEN_SALDO, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "SeguroDesg.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //12 
        [Route("[controller]/desgravamendevoluciontest")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestDesgravamenDevolucion()
        {
            BpmRequest request = new BpmRequest()
            {

                RazonSocialCliente = "RazonSocial",
                RucCliente = "1234567891",
                TipoCredito = "Hipotecario",
                TipoMoneda = "Soles",
                FechaTransaccion = "30/03/2021",
                ApPaternoCliente = "Diaz",
                ApMaternoCliente = "Sanchez",
                NombresCliente = "Juan",
                TipoDocumentoCliente = "DNI",
                NroDocumentoCliente = "48757978",
                PrimerConsentimiento = "true",
                SegundoConsentimiento = "true",

                ApPaternoCliente2 = "Diaz2",
                ApMaternoCliente2 = "Sanchez2",
                NombresCliente2 = "Juan2",
                TipoDocumentoCliente2 = "DNI",
                NroDocumentoCliente2 = "487579782",
                ApPaternoFirmanteAdicional = "Flores",
                ApMaternoFirmanteAdicional = "Medina",
                NombresFirmanteAdicional = "Fiorella",
                TipoDocumentoFirmanteAdicional = "DNI",
                NroDocumentoFirmanteAdicional = "74544048",

                Entidad = "Entidad",
                NombreAgencia = "NombreAgencia",
                NombresFuncionario = "NombresFuncionario",
                EmailFuncionario = "EmailFuncionario",

                //Nuevos
                FechaNacimientoCliente = "20/01/1998",
                PlazoCredito = "PlazoCredito",
                DireccionCliente = "Direccion",
                NroDireccionCliente = "NroDireccionCliente",

                DistritoCliente = "Distrito",
                ProvinciaCliente = "Provincia",
                DepartamentoCliente = "Departamento",
                TelefonoCliente = "123456789",
                CelularCliente = "123456789",
                EmailCliente = "email@prueba.com",

                //Cancer = "true",
                //CancerMama = "true",
                //FechaDiagnosticoCancerMama = "FechaDiagnostico",
                //EstadoCancerMama = "Estado ",
                //IsntitucionMedicaEntranteCancerMama = "IsntitucionMedicaEntrante",
                //MedicoEntranteCancerMama = "MedicoEntrante",

                //CancerColon = "true",
                //FechaDiagnosticoCancerColon = "FechaDiagnosticoCancerColon",
                //EstadoCancerColon = "EstadoCancerColon",
                //IsntitucionMedicaEntranteCancerColon = "IsntitucionMedicaEntranteCancerColon",
                //MedicoEntranteCancerColon = "MedicoEntranteCancerColon",

                //CancerPulmon = "true",
                //FechaDiagnosticoCancerPulmon = "FechaDiagnosticoCancerPulmon",
                //EstadoCancerPulmon = "EstadoCancerPulmon",
                //IsntitucionMedicaEntranteCancerPulmon = "IsntitucionMedicaEntranteCancerPulmon",
                //MedicoEntranteCancerPulmon = "MedicoEntranteCancerPulmon",

                //CancerOtro = "CancerOtro",
                //FechaDiagnosticoCancerOtro = "FechaDiagnosticoCancerOtro",
                //EstadoCancerOtro = "EstadoCancerOtro",
                //IsntitucionMedicaEntranteCancerOtro = "IsntitucionMedicaEntranteCancerOtro",
                //MedicoEntranteCancerOtro = "MedicoEntranteCancerOtro",

                //CardioVascular = "true",
                //CardiopatiaCoronaria = "true",
                //FechaDiagnosticoCardiopatiaCoronaria = "FechaDiagnosticoCardiopatiaCoronaria",
                //EstadoCardiopatiaCoronaria = "EstadoCardiopatiaCoronaria",
                //IsntitucionMedicaEntranteCardiopatiaCoronaria = "IsntitucionMedicaEntranteCardiopatiaCoronaria",
                //MedicoEntranteCardiopatiaCoronaria = "MedicoEntranteCardiopatiaCoronaria",

                //InsuficienciaCardiaca = "true",
                //FechaDiagnosticoInsuficienciaCardiaca = "FechaDiagnosticoInsuficienciaCardiaca",
                //EstadoInsuficienciaCardiaca = "EstadoInsuficienciaCardiaca",
                //IsntitucionMedicaEntranteInsuficienciaCardiaca = "IsntitucionMedicaEntranteInsuficienciaCardiaca",
                //MedicoEntranteInsuficienciaCardiaca = "MedicoEntranteInsuficienciaCardiaca",

                //CardioOtro = "CardioOtro",
                //FechaDiagnosticoCardioOtro = "FechaDiagnosticoCardioOtro",
                //EstadoCardioOtro = "EstadoCardioOtro",
                //IsntitucionMedicaEntranteCardioOtro = "IsntitucionMedicaEntranteCardioOtro",
                //MedicoEntranteCardioOtro = "MedicoEntranteCardioOtro",

                //Renal = "true",
                //FechaDiagnosticoRenal = "FechaDiagnosticoRenal ",
                //EstadoRenal = "EstadoCardioRenal",
                //IsntitucionMedicaEntranteRenal = "IsntitucionMedicaEntranteRenal",
                //MedicoEntranteRenal = "MedicoEntranteRenal",

                //Diabetes = "true",
                //FechaDiagnosticoDiabetes = "FechaDiagnosticoDiabetes",
                //EstadoDiabetes = "EstadoDiabetes",
                //IsntitucionMedicaEntranteDiabetes = "IsntitucionMedicaEntranteDiabetes",
                //MedicoEntranteDiabetes = "MedicoEntranteDiabetes",

                //Neurologicas = "true",
                //FechaDiagnosticoNeurologicas = "FechaDiagnosticoNeurologicas",
                //EstadoNeurologicas = "EstadoNeurologicas",
                //IsntitucionMedicaEntranteNeurologicas = "IsntitucionMedicaEntranteNeurologicas",
                //MedicoEntranteNeurologicas = "MedicoEntranteNeurologicas",

                //Psiquiatricas = "true",
                //FechaDiagnosticoPsiquiatricas = "FechaDiagnosticoPsiquiatricas",
                //EstadoPsiquiatricas = "EstadoPsiquiatricas",
                //IsntitucionMedicaEntrantePsiquiatricas = "IsntitucionMedicaEntrantePsiquiatricas",
                //MedicoEntrantePsiquiatricas = "MedicoEntrantePsiquiatricas",

                //EnfermedadesRespiratorias = "true",
                //FechaDiagnosticoEnfermedadesRespiratorias = "FechaDiagnosticoEnfermedadesRespiratorias",
                //EstadoEnfermedadesRespiratorias = "EstadoEnfermedadesRespiratorias",
                //IsntitucionMedicaEntranteEnfermedadesRespiratorias = "IsntitucionMedicaEntranteEnfermedadesRespiratorias",
                //MedicoEntranteEnfermedadesRespiratorias = "MedicoEntranteEnfermedadesRespiratorias",

                //SIDA = "true",
                //FechaDiagnosticoSIDA = "FechaDiagnosticoSIDA",
                //EstadoSIDA = "EstadoSIDA",
                //IsntitucionMedicaEntranteSIDA = "IsntitucionMedicaEntranteSIDA",
                //MedicoEntranteSIDA = "MedicoEntranteSIDA",

                //OtrasEnfermedades = "true",
                //FechaDiagnosticoOtrasEnfermedades = "FechaDiagnosticoOtrasEnfermedades",
                //EstadoOtrasEnfermedades = "EstadoOtrasEnfermedades",
                //IsntitucionMedicaEntranteOtrasEnfermedades = "IsntitucionMedicaEntranteOtrasEnfermedades",
                //MedicoEntranteOtrasEnfermedades = "MedicoEntranteOtrasEnfermedades",


                ImpresionBiometricaCliente = "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",
                ImpresionBiometricaCliente2 = "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",

            };
            //string file = GetContractPDF(_pdfFormats.CONTRATO, request.ImpesionBiometrica;)//"";// GetContractPDF(_pdfFormats.CONTRATO, request);

            String bioFingerprint = request.ImpresionBiometricaCliente;//request.ImpesionBiometrica;


            String FingerprintImage = "";
            String BarCode = "";

            //String FingerprintImageHTML = $"data:image/jpeg;base64,{FingerprintImage}";
            //String BarCodeHTML = $"data:image/jpeg;base64,{BarCode}";

            String file = GetDesgravamenDevolucionPDF(_pdfFormats.DESGRAVAMEN_DEVOLUCION, request/*, FingerprintImage, BarCode*/);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "SeguroDesg.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(Convert.FromBase64String(file), "application/pdf");
        }

        //POST

        //1
        [Route("[controller]/solicitudGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> SolicitudCreditoGenerate([FromBody] BpmRequest request)
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

                        string solicitudGenerated = GetSolicitudPDF(_pdfFormats.SOLICITUD_CREDITO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            solicitudGenerated = AddPageSign(solicitudGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                solicitudGenerated = ExistingPageSign(solicitudGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //2
        [Route("[controller]/contratoGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> ContratoCreditoGenerate([FromBody] BpmRequest request)
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

                        string contratoGenerated = GetContratoPDF(_pdfFormats.CONTRATO_CREDITO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            contratoGenerated = AddPageSign(contratoGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                contratoGenerated = ExistingPageSign(contratoGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //3
        [Route("[controller]/cartillaGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> CartillaCuentaGenerate([FromBody] BpmRequest request)
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

                        string garantiaGenerated = GetCartillaPDF(_pdfFormats.CARTILLA_CUENTA, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            garantiaGenerated = AddPageSign(garantiaGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                garantiaGenerated = ExistingPageSign(garantiaGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //4
        [Route("[controller]/segurodesGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> SeguroDesgravamenGenerate([FromBody] BpmRequest request)
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

                        string pagareGenerated = GetSeguroDesgravamenPDF(_pdfFormats.SEGURO_DESG, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            pagareGenerated = AddPageSign(pagareGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                pagareGenerated = ExistingPageSign(pagareGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //5
        [Route("[controller]/seguroGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> SeguroOptativoGenerate([FromBody] BpmRequest request)
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

                        string desgravamenGenerated = GetSeguroOptativoPDF(_pdfFormats.SEGURO_OPTATIVO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageSign(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageSign(desgravamenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //6
        [Route("[controller]/hojaGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> HojaAceptacionGenerate([FromBody] BpmRequest request)
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

                        string hojaAceptacion = GetHojaAceptacionPDF(_pdfFormats.HOJA_ACEPTACION, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            hojaAceptacion = AddPageSign(hojaAceptacion, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                hojaAceptacion = ExistingPageSign(hojaAceptacion, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //7
        [Route("[controller]/hojaoptGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> HojaOptativoGenerate([FromBody] BpmRequest request)
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

                        string hojaOptativo = GetHojaOptativoPDF(_pdfFormats.HOJA_OPTATIVO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            hojaOptativo = AddPageSign(hojaOptativo, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                hojaOptativo = ExistingPageSign(hojaOptativo, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //8
        [Route("[controller]/resumenGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> HojaResumenGenerate([FromBody] BpmRequest request)
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

                        string resumenGenerated = GetHojaResumenPDF(_pdfFormats.HOJA_RESUMEN, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            resumenGenerated = AddPageSign(resumenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                resumenGenerated = ExistingPageSign(resumenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
                            }
                        }


                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                resumenGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //8-F
        [Route("[controller]/resumenGeneratefacial")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> HojaResumenGenerateFacial([FromBody] BpmRequest request)
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

                        string resumenGenerated = GetHojaResumenPDF(_pdfFormats.HOJA_RESUMEN, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            resumenGenerated = AddPageFacial(resumenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                resumenGenerated = ExistingPageFacial(resumenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
                            }
                        }


                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                              {
                                resumenGenerated,

                              }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //9
        [Route("[controller]/AhorroGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> CartillaAhorroGenerate([FromBody] BpmRequest request)
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

                        string garantiaGenerated = GetCartillaAhorroPDF(_pdfFormats.CARTILLA_AHORRO_EFECTIVO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            garantiaGenerated = AddPageSign(garantiaGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                garantiaGenerated = ExistingPageSign(garantiaGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //10
        [Route("[controller]/ConsentimientoGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> ConsentimientoGenerate([FromBody] BpmRequest request)
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

                        string consentimientoGenerated = GetConsentimientoPDF(_pdfFormats.CONSENTIMIENTO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            consentimientoGenerated = AddPageSign(consentimientoGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                consentimientoGenerated = ExistingPageSign(consentimientoGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
                            }
                        }
                        //await UpdateEntityTransaccionalDocumentFormater(EntityTransactional.Id);
                        _logger.LogCritical("Finalizing create format...");

                        response.data = new
                        {
                            documents = new List<string>()
                    {
                                consentimientoGenerated,

                    }
                        };
                        response.code = ContractResponse.ResponseCode.Successful;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //11
        [Route("[controller]/DesgravamenSaldoGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DesgravamenSaldoGenerate([FromBody] BpmRequest request)
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

                        string desgravamenGenerated = GetDesgravamenSaldoPDF(_pdfFormats.DESGRAVAMEN_SALDO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageSign(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageSign(desgravamenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //11-F
        [Route("[controller]/DesgravamenSaldoGeneratefacial")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DesgravamenSaldoGenerateFacial([FromBody] BpmRequest request)
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

                        string desgravamenGenerated = GetDesgravamenSaldoPDF(_pdfFormats.DESGRAVAMEN_SALDO, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageFacial(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageFacial(desgravamenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //12
        [Route("[controller]/DesgravamenDevolucionGenerate")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DesgravamenDevolucionGenerate([FromBody] BpmRequest request)
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

                        string desgravamenGenerated = GetDesgravamenDevolucionPDF(_pdfFormats.DESGRAVAMEN_DEVOLUCION, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageSign(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageSign(desgravamenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //12-F
        [Route("[controller]/DesgravamenDevolucionGeneratefacial")]
        [HttpPost]
        public async Task<ActionResult<ContractResponse>> DesgravamenDevolucionGenerateFacial([FromBody] BpmRequest request)
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

                        string desgravamenGenerated = GetDesgravamenDevolucionPDF(_pdfFormats.DESGRAVAMEN_DEVOLUCION, request/*, FingerprintImage, ""*/);

                        if (FingerprintImage != null)
                        {
                            desgravamenGenerated = AddPageFacial(desgravamenGenerated, FingerprintImage, request.AddHojaNombres, request.AddHojaApellidoPaterno, request.AddHojaApellidoMaterno, request.AddHojaDocumentoIdentidad, 55, 620);

                            if (!String.IsNullOrEmpty(FingerprintImage2))
                            {
                                desgravamenGenerated = ExistingPageFacial(desgravamenGenerated, FingerprintImage2, request.AddHojaNombres2, request.AddHojaApellidoPaterno2, request.AddHojaApellidoMaterno2, request.AddHojaDocumentoIdentidad2, 55, 380);
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
                    ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
                    _logger.LogError("Error {0}", ex.Message);

                    response.code = ContractResponse.ResponseCode.ServerError;
                    return Ok(response);
                }
            }
            return Ok(response);
        }

        //Métodos de generacion de documentos

        //1
        private String GetSolicitudPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            ////Canal venta
            if (request.CanalVenta == "Agencia")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 40, 750, 0.0f);
            }
            else if (request.CanalVenta == "Externo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 80, 750, 0.0f);

            }
            else if (request.CanalVenta == "FuerzaVenta")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 127, 750, 0.0f);
            }
            else if (request.CanalVenta == "Otro")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 177, 750, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 750, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 207, 750, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CodVendedor}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 345, 750, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroSolicitud}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 424, 750, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 505, 750, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 520, 750, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 535, 750, 0.0f);

            }


            

            //Tipo de Credito
            if (request.TipoPrestamoPersonal == "Nuevo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 23, 701, 0.0f);
                
            }
            else if (request.TipoPrestamoPersonal == "Reenganche")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 23, 691, 0.0f);

            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            DateTime fechaPagoPrestamoPersonal = DateTime.MinValue;
            bool formatPagoPrestamoPersonal = DateTime.TryParse(request.FechaPagoPrestamoPersonal, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaPagoPrestamoPersonal);
            if (formatPagoPrestamoPersonal)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{fechaPagoPrestamoPersonal.ToString("dd/MM/yyyy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 695, 0.0f);
            }

            //Moneda
            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 655, 0.0f);
                
            }
            else if (request.TipoMoneda == "Dolares")
            {

                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 655, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCredito}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 50, 655, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PlazoCredito}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 144, 654, 5.0f);

            //Selecciona cuota
            if (request.TipoCuota == "Simple") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 266, 697, 0.0f); 
            }
            else if (request.TipoCuota == "DobleJul") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 244, 682, 0.0f);
            }
            else if (request.TipoCuota == "Dic") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 266, 682, 0.0f);
            }
            else if (request.TipoCuota == "DobleEne") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 244, 667, 0.0f);
            }
            else if (request.TipoCuota == "Ago") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 266, 667, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TasaCredito}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 650, 0.0f);


            //Tipo de Garantia
            if (request.TipoGarantia == "Liquida")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 283, 693, 0.0f);
            }
            else if (request.TipoGarantia == "Hipotecaria")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 283, 678, 0.0f);
            }
            else if (request.TipoGarantia == "Aval")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 283, 662, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }


            if (request.PeriodoGracia == "false") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 364, 693, 0.0f);
            }
            else if (request.PeriodoGracia == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 364, 673, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PeriodoGraciaDet}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 364, 653, 0.0f);
            }

            if (request.UsoPrestamoPersonal == "Ocio") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 432, 693, 0.0f);
            }
            else if (request.UsoPrestamoPersonal == "CompraDeuda") {

                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 432, 677, 0.0f);
            }
            else if (request.UsoPrestamoPersonal == "Otro")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 432, 662, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UsoPrestamoPersonalOtros}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 460, 663, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }


            //TipoDocumento

            if (request.TipoDocumentoCliente == "DNI") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 603, 0.0f);
                
            }
            else if (request.TipoDocumentoCliente == "CE") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 42, 603, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 60, 603, 6.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 158, 604, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 298, 604, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 440, 604, 0.0f);

            DateTime fechaNacimientoCliente = DateTime.MinValue;
            bool formatNacimientoCliente = DateTime.TryParse(request.FechaNacimientoCliente, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacimientoCliente);
            if (formatNacimientoCliente)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoCliente.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 573, 3.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoCliente.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 573, 3.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoCliente.ToString("yy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 77, 573, 3.0f);
            }
            //Sexo
            if (request.SexoCliente == "Femenino") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 110, 573, 0.0f);
            }
            else if (request.SexoCliente == "Masculino") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 125, 573, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Nacionalidad}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 145, 573, 0.0f);

            //Estado Civil
            if (request.EstadoCivilCliente == "Soltero") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 573, 0.0f);
            }
            else if (request.EstadoCivilCliente == "Conviviente") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 257, 573, 0.0f);
            }
            else if (request.EstadoCivilCliente == "Casado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 573, 0.0f);
            }
            else if (request.EstadoCivilCliente == "CasadoSepBienes") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 573, 0.0f);
            }
            else if (request.EstadoCivilCliente == "Viudo") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 333, 573, 0.0f);
            }
            else if (request.EstadoCivilCliente == "Divorciado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 573, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDependientes}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 384, 570, 9.0f);

            //Cumple funciones
            if (request.FuncionesCliente == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 533, 572, 0.0f); 
            }
            else if (request.FuncionesCliente == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 560, 572, 0.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 541, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CelularCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 210, 541, 5.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CodigoCiudad}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 308, 541, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 360, 541, 0.0f);

            //Tipo de vivienda
            if (request.TipoViviendaCliente == "Propia") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 440, 540, 0.0f); 
            }
            else if (request.TipoViviendaCliente == "PropiaFinanciada") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 485, 540, 0.0f);
            }
            else if (request.TipoViviendaCliente == "Alquilada") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 525, 540, 0.0f);
            }
            else if (request.TipoViviendaCliente == "Familiar") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 560, 540, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            //Grado de Instruccion
            if (request.GradoInstruccion == "Primaria") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 25, 508, 0.0f);
            }
            else if (request.GradoInstruccion == "Secundaria") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 48, 508, 0.0f);
            }
            else if (request.GradoInstruccion == "Tecnico") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 68, 508, 0.0f);
            }
            else if (request.GradoInstruccion == "Universitario") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 508, 0.0f);
            }
            else if (request.GradoInstruccion == "Ninguno") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 120, 508, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 120, 403, 0.0f);
            }

            //Continuidad Laboral

            if (request.ContinuidadLaboral == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 156, 507, 0.0f);
            }
            else if (request.ContinuidadLaboral == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 182, 507, 0.0f);
            }

            //Situacion Laboral
            if (request.SituacionLaboralCliente == "Dependiente") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 245, 515, 0.0f);
            }
            else if (request.SituacionLaboralCliente == "Profesional") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 245, 506, 0.0f);
            }
            else if (request.SituacionLaboralCliente == "Accionista") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 515, 0.0f);
            }
            else if (request.SituacionLaboralCliente == "Rentista") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 506, 0.0f);
            }
            else if (request.SituacionLaboralCliente == "PersonaNatural") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 415, 515, 0.0f);
            }
            else if (request.SituacionLaboralCliente == "Jubilado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 415, 506, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 420, 398, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RucCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 469, 507, 4.7f);

            //Datos domiciliarios titular
            if (request.DireccionDetalleCliente == "Calle") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 23, 469, 0.0f);
            }
            else if (request.DireccionDetalleCliente == "Avenida") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 35, 469, 0.0f);
            }
            else if (request.DireccionDetalleCliente == "Jiron") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 48, 469, 0.0f);
            }
            else if (request.DireccionDetalleCliente == "Pasaje") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 61, 469, 0.0f);
            }
            else if (request.DireccionDetalleCliente == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 75, 469, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 347, 0.0f);
            }

            if (request.DireccionDetalleExteriorCliente == "Numero") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 152, 469, 0.0f);
            }
            else if (request.DireccionDetalleExteriorCliente == "Bloque") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 167, 469, 0.0f);
            }
            else if (request.DireccionDetalleExteriorCliente == "Manzana") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 186, 469, 0.0f);
            }
            else if (request.DireccionDetalleExteriorCliente == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 200, 469, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 347, 0.0f);
            }

            if (request.DireccionDetalleInteriorCliente == "Lote") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 222, 469, 0.0f);
            }
            else if (request.DireccionDetalleInteriorCliente == "Departamento") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 238, 469, 0.0f);
            }
            else if (request.DireccionDetalleInteriorCliente == "Int") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 257, 469, 0.0f);
            }
            else if (request.DireccionDetalleInteriorCliente == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 469, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 347, 0.0f);
            }

            if (request.DireccionDetalleZonaCliente == "Seccion") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 295, 469, 0.0f);
            }
            else if (request.DireccionDetalleZonaCliente == "Etapa") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 312, 469, 0.0f);
            }
            else if (request.DireccionDetalleZonaCliente == "Urbanizacion") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 469, 0.0f);
            }
            else if (request.DireccionDetalleZonaCliente == "AAHH") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 345, 469, 0.0f);
            }
            else if (request.DireccionDetalleZonaCliente == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 366, 469, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 347, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 454, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionExteriorCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 150, 454, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionInteriorCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 220, 454, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionZonaCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 292, 454, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UbiegoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 399, 454, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ReferenciaCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 58, 437, 0.0f);

            //Datos de la empresa Titular
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CentroActualTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 391, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CargoActualTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 222, 391, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.GiroTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 374, 391, 0.0f);

            DateTime fechaIngresoTitular = DateTime.MinValue;
            bool formatIngresoTitular = DateTime.TryParse(request.FechaIngresoTitular, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaIngresoTitular);
            if (formatIngresoTitular)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaIngresoTitular.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 484, 389, 4.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaIngresoTitular.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 389, 4.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaIngresoTitular.ToString("yy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 540, 389, 4.0f);
            }
            

            //Datos domiciliarios Empresa
            if (request.DireccionDetalleEmpresaT == "Calle") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 23, 370, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaT == "Avenida") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 35, 370, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaT == "Jiron") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 48, 370, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaT == "Pasaje") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 61, 370, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaT == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 75, 370, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            if (request.DireccionDetalleExteriorEmpresaT == "Numero") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 152, 370, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaT == "Bloque") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 167, 370, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaT == "Manzana") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 186, 370, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaT == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 200, 370, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            if (request.DireccionDetalleInteriorEmpresaT == "Lote") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 222, 370, 0.0f); 
            }
            else if (request.DireccionDetalleInteriorEmpresaT == "Departamento") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 238, 370, 0.0f);
            }
            else if (request.DireccionDetalleInteriorEmpresaT == "Int") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 257, 370, 0.0f);
            }
            else if (request.DireccionDetalleInteriorEmpresaT == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 271, 370, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            if (request.DireccionDetalleZonaEmpresaT == "Seccion") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 295, 370, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaT == "Etapa") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 312, 370, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaT == "Urbanizacion") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 370, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaT == "AAHH") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 345, 370, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaT == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 366, 370, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionEmpresaT}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 355, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionExteriorEmpresaT}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 150, 355, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionInteriorEmpresaT}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 220, 355, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionZonaEmpresaT}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 292, 355, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UbiegoEmpresaTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 399, 355, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ReferenciaEmpresaTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 55, 339, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CodigoCiudadEmpresaTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 310, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoEmpresaTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 73, 310, 0.0f);

            DateTime fechaFinContratoTitular = DateTime.MinValue;
            bool formatFinContratoTitular = DateTime.TryParse(request.FechaFinContratoTitular, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaFinContratoTitular);
            if (formatFinContratoTitular)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaFinContratoTitular.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 319, 308, 3.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaFinContratoTitular.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 348, 308, 3.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaFinContratoTitular.ToString("yy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 375, 308, 3.0f);
            }


            if (request.TipoContratoTitular == "Nombrado") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 187, 317, 0.0f);
            }
            else if (request.TipoContratoTitular == "Cesante") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 187, 307, 0.0f);
            }
            else if (request.TipoContratoTitular == "PlazoFijo") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 230, 317, 0.0f);
            }
            else if (request.TipoContratoTitular == "Ninguno") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 230, 307, 0.0f);
            }
            else if (request.TipoContratoTitular == "CAS") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 255, 317, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            if (request.TipoMonedaIngresoTitular == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 414, 311, 0.0f);
            }
            else if (request.MonedaOtroIngresoTitular == "Dolares") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 426, 311, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            if (request.MonedaOtroIngresoTitular == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 311, 0.0f);
            }
            else if (request.MonedaOtroIngresoTitular == "Dolares") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 311, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 512, 157, 0.0f);
            }

            
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoIngresoTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 438, 310, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoOtroIngresoTitular}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 523, 310, 0.0f);

            //Información personal Conyuge

            if (request.TipoDocumentoConyuge == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 27, 260, 0.0f);
            }
            else if (request.TipoDocumentoConyuge == "CE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 42, 260, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 717, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 60, 261, 6.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 158, 263, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 298, 263, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 440, 263, 0.0f);

            DateTime fechaNacimientoConyuge = DateTime.MinValue;
            bool formatNacimientoConyuge = DateTime.TryParse(request.FechaNacimientoConyuge, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacimientoConyuge);
            if (formatNacimientoConyuge)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoConyuge.ToString("dd")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 21, 232, 4.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoConyuge.ToString("MM")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 48, 232, 4.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimientoConyuge.ToString("yy")}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 74, 232, 4.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NacionalidadConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 145, 232, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDependientesConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 384, 232, 9.0f);


            //Sexo
            if (request.SexoConyuge == "Femenino") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 110, 232, 0.0f);
            }
            else if (request.SexoConyuge == "Masculino") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 125, 232, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            //Estado Civil
            if (request.EstadoCivilConyuge == "Soltero") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 232, 0.0f);
            }
            else if (request.EstadoCivilConyuge == "Conviviente") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 257, 232, 0.0f);
            }
            else if (request.EstadoCivilConyuge == "Casado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 232, 0.0f);
            }
            else if (request.EstadoCivilConyuge == "CasadoSepBienes") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 232, 0.0f);
            }
            else if (request.EstadoCivilConyuge == "Viudo") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 333, 232, 0.0f);
            }
            else if (request.EstadoCivilConyuge == "Divorciado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 352, 232, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            //Cumple funciones
            if (request.FuncionesConyuge == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 533, 232, 0.0f);
            }
            else if (request.FuncionesConyuge == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 560, 232, 0.0f);
            }


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 22, 200, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CelularConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 210, 201, 5.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 308, 200, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CodigoConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 360, 200, 0.0f);


            //Tipo de vivienda
            if (request.ViviendaConyuge == "Propia") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 438, 197, 0.0f);
            }
            else if (request.ViviendaConyuge == "PropiaFinanciada") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 485, 197, 0.0f);
            }
            else if (request.ViviendaConyuge == "Alquilada") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 525, 197, 0.0f);
            }
            else if (request.ViviendaConyuge == "Familiar") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 560, 197, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            //Grado de Instruccion
            if (request.GradoConyuge == "Primaria") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 25, 167, 0.0f);
            }
            else if (request.GradoConyuge == "Secundaria") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 48, 167, 0.0f);
            }

            else if (request.GradoConyuge == "Tecnico") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 68, 167, 0.0f);
            }
            else if (request.GradoConyuge == "Universitario") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 90, 167, 0.0f);
            }
            else if (request.GradoConyuge == "Ninguno") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 120, 167, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            //Continuidad Laboral

            if (request.ContinuidadConyuge == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 156, 167, 0.0f);
            }
            else if (request.ContinuidadConyuge == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 182, 167, 0.0f);
            }


            //Situacion Laboral
            if (request.SituacionLaboralConyuge == "Dependiente") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 245, 173, 0.0f);
            }
            else if (request.SituacionLaboralConyuge == "Profesional") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 245, 163, 0.0f);
            }
            else if (request.SituacionLaboralConyuge == "Accionista") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 173, 0.0f);
            }
            else if (request.SituacionLaboralConyuge == "Rentista") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 330, 163, 0.0f);
            }
            else if (request.SituacionLaboralConyuge == "PersonaNatural") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 415, 173, 0.0f);
            }
            else if (request.SituacionLaboralConyuge == "Jubilado") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 415, 163, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RUCConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 469, 166, 4.7f);


            //Datos domiciliarios Empresa Conyuge
            if (request.DireccionDetalleEmpresaC == "Calle") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 25, 147, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaC == "Avenida") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 37, 147, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaC == "Jiron") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 50, 147, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaC == "Pasaje") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 63, 147, 0.0f);
            }
            else if (request.DireccionDetalleEmpresaC == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 77, 147, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            if (request.DireccionDetalleExteriorEmpresaC == "Numero") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 151, 147, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaC == "Bloque") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 166, 147, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaC == "Manzana") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 185, 147, 0.0f);
            }
            else if (request.DireccionDetalleExteriorEmpresaC == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 199, 147, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            if (request.DireccionDetalleInteriorEmpresaC == "Lote") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 223, 147, 0.0f);
            }
            else if (request.DireccionDetalleInteriorEmpresaC == "Departamento") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 239, 147, 0.0f);
            }
            else if (request.DireccionDetalleInteriorEmpresaC == "Int") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 258, 147, 0.0f);
            }
            else if (request.DireccionDetalleInteriorEmpresaC == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 272, 147, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            if (request.DireccionDetalleZonaEmpresaC == "Seccion") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 296, 147, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaC == "Etapa") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 313, 147, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaC == "Urbanizacion") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 331, 147, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaC == "AAHH") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 346, 147, 0.0f);
            }
            else if (request.DireccionDetalleZonaEmpresaC == "Otro") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 8, iTextSharp.text.Element.ALIGN_LEFT, 367, 147, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 547, 540, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionEmpresaC}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 21, 134, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionExteriorEmpresaC}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 149, 134, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionInteriorEmpresaC}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 219, 134, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionZonaEmpresaC}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 292, 134, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UbigeoEmpresaConyuge}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 399, 134, 0.0f);

            ////Detalle prestamo por convenio
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion1}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 65, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta1}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 65, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar1}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 65, 0.0f);

            if (request.TipoTarjeta1 == "Tarjeta") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 65, 0.0f); 
            }
            else if (request.TipoTarjeta1 == "Prestamo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 65, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 716, 0.0f);
            }

            if (request.TipoValor1 == "PortaValor") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 65, 0.0f); 
            }
            else if (request.TipoValor1 == "Directo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 65, 0.0f); 
            }
            else if (request.TipoValor1 == "CCE") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 65, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 65, 0.0f);
            }

            if (request.TipoMoneda1 == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 65, 0.0f); 
            }
            else if (request.TipoMoneda1 == "Dolares") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 65, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 716, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion2}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 50, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta2}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 50, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar2}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 50, 0.0f);

            if (request.TipoTarjeta2 == "Tarjeta") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 50, 0.0f); 
            }
            else if (request.TipoTarjeta2 == "Prestamo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 50, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 50, 0.0f);
            }

            if (request.TipoValor2 == "PortaValor") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 50, 0.0f); 
            }
            else if (request.TipoValor2 == "Directo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 50, 0.0f); 
            }
            else if (request.TipoValor2 == "CCE") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 50, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 50, 0.0f);
            }

            if (request.TipoMoneda2 == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 50, 0.0f); 
            }
            else if (request.TipoMoneda2 == "Dolares") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 50, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 50, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion3}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 36, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta3}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 36, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar3}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 36, 0.0f);

            if (request.TipoTarjeta3 == "Tarjeta") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 36, 0.0f); 
            }
            else if (request.TipoTarjeta3 == "Prestamo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 36, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 36, 0.0f);
            }

            if (request.TipoValor3 == "PortaValor") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 36, 0.0f); 
            }
            else if (request.TipoValor3 == "Directo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 36, 0.0f); 
            }
            else if (request.TipoValor3 == "CCE") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 36, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 36, 0.0f);
            }

            if (request.TipoMoneda3 == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 36, 0.0f); 
            }
            else if (request.TipoMoneda3 == "Dolares") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 36, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 36, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion4}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 802, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta4}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 802, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar4}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 802, 0.0f);

            if (request.TipoTarjeta4 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 802, 0.0f); }
            else if (request.TipoTarjeta4 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 802, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 802, 0.0f);
            }

            if (request.TipoValor4 == "PortaValor") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 802, 0.0f); 
            }
            else if (request.TipoValor4 == "Directo") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 802, 0.0f); 
            }
            else if (request.TipoValor4 == "CCE") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 802, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 802, 0.0f);
            }

            if (request.TipoMoneda4 == "Soles") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 802, 0.0f); 
            }
            else if (request.TipoMoneda4 == "Dolares") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 802, 0.0f); 
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 802, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion5}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 787, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta5}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 787, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar5}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 787, 0.0f);

            if (request.TipoTarjeta5 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 787, 0.0f); }
            else if (request.TipoTarjeta5 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 787, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 787, 0.0f);
            }

            if (request.TipoValor5 == "PortaValor")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 787, 0.0f);
            }
            else if (request.TipoValor5 == "Directo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 787, 0.0f);
            }
            else if (request.TipoValor5 == "CCE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 787, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 787, 0.0f);
            }

            if (request.TipoMoneda5 == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 787, 0.0f);
            }
            else if (request.TipoMoneda5 == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 787, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 787, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion6}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 773, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta6}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 773, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar6}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 773, 0.0f);

            if (request.TipoTarjeta6 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 773, 0.0f); }
            else if (request.TipoTarjeta6 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 773, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 773, 0.0f);
            }

            if (request.TipoValor6 == "PortaValor")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 773, 0.0f);
            }
            else if (request.TipoValor6 == "Directo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 773, 0.0f);
            }
            else if (request.TipoValor6 == "CCE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 773, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 773, 0.0f);
            }

            if (request.TipoMoneda6 == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 773, 0.0f);
            }
            else if (request.TipoMoneda6 == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 773, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 773, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion7}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 758, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta7}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 758, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar7}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 758, 0.0f);

            if (request.TipoTarjeta7 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 758, 0.0f); }
            else if (request.TipoTarjeta7 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 758, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 758, 0.0f);
            }

            if (request.TipoValor7 == "PortaValor")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 758, 0.0f);
            }
            else if (request.TipoValor7 == "Directo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 758, 0.0f);
            }
            else if (request.TipoValor7 == "CCE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 758, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 758, 0.0f);
            }

            if (request.TipoMoneda7 == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 758, 0.0f);
            }
            else if (request.TipoMoneda7 == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 758, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 758, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion8}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 743, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta8}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 743, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar8}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 743, 0.0f);

            if (request.TipoTarjeta8 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 743, 0.0f); }
            else if (request.TipoTarjeta8 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 743, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 743, 0.0f);
            }

            if (request.TipoValor8 == "PortaValor")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 743, 0.0f);
            }
            else if (request.TipoValor8 == "Directo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 743, 0.0f);
            }
            else if (request.TipoValor8 == "CCE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 743, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 743, 0.0f);
            }

            if (request.TipoMoneda8 == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 743, 0.0f);
            }
            else if (request.TipoMoneda8 == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 743, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 743, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreInstitucion9}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 29, 728, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NumeroTarjeta9}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 728, 6.2f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoCancelar9}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 728, 0.0f);

            if (request.TipoTarjeta9 == "Tarjeta") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 728, 0.0f); }
            else if (request.TipoTarjeta9 == "Prestamo") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 728, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 802, 0.0f);
            }

            if (request.TipoValor9 == "PortaValor")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 728, 0.0f);
            }
            else if (request.TipoValor9 == "Directo")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 728, 0.0f);
            }
            else if (request.TipoValor9 == "CCE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 728, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 446, 728, 0.0f);
            }

            if (request.TipoMoneda9 == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 728, 0.0f);
            }
            else if (request.TipoMoneda9 == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 728, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 728, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoTotal}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 500, 714, 0.0f);

            if (request.MonedaMontoTotal == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 470, 714, 0.0f);
            }
            else if (request.MonedaMontoTotal == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 714, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 714, 0.0f);
            }


            ////Referencias
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresReferencia1}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 75, 235, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ParentescoReferencia1}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 269, 235, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoReferencia1}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 449, 235, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresReferencia2}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 75, 218, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ParentescoReferencia2}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 269, 218, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoReferencia2}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 449, 218, 0.0f);

            ////Afiliacion electronica

            if (request.EnvioEstadoCuenta == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 161, 563, 0.0f); 
            }
            else if (request.EnvioEstadoCuenta == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 178, 563, 0.0f);
            }


            if (request.FormaEstadoCuenta == "Fisica") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 544, 0.0f);
            }
            else if (request.FormaEstadoCuenta == "Electronica")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 243, 544, 0.0f);
                //CorreoEstadoDeCuenta
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 362, 546, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 175, 166, 0.0f);
            }

            if (request.CorrespondenciaEstadoCuenta == "Domicilio") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 237, 528, 0.0f);
                
            }
            else if (request.CorrespondenciaEstadoCuenta == "Trabajo") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 277, 528, 0.0f);
            }
            else { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 274, 67, 0.0f); 
            }

            ////Tratamiento de datos personales

            if (request.PrimerConsentimiento == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 8, iTextSharp.text.Element.ALIGN_LEFT, 227, 298, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 8, iTextSharp.text.Element.ALIGN_LEFT, 341, 298, 0.0f);
            }

            if (request.SegundoConsentimiento == "true") { 
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 8, iTextSharp.text.Element.ALIGN_LEFT, 227, 268, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false") {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 8, iTextSharp.text.Element.ALIGN_LEFT, 341, 268, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 8, iTextSharp.text.Element.ALIGN_LEFT, 198, 48, 0.0f);


            return pdfbase64;
        }

        //2
        private String GetContratoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.LugarTransaccion}", 11, 9, iTextSharp.text.Element.ALIGN_RIGHT, 382, 358, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 403, 358, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MMMM", new System.Globalization.CultureInfo("es-PE"))}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 475, 358, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 555, 358, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 170, 335, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 268, 317, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente} {request.NroDireccionCliente}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 90, 299, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresConyuge} {request.ApPaternoConyuge} {request.ApMaternoConyuge}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 173, 280, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoConyuge}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 268, 261, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreRepresentante}", 11, 10, iTextSharp.text.Element.ALIGN_LEFT, 177, 242, 0.0f);

            if (request.TipoDocumentoCliente != null)
            {
                if (request.TipoDocumentoCliente == "RUC") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 76, 314, 0.0f); }
                else if (request.TipoDocumentoCliente == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 136, 314, 0.0f); }
                else if (request.TipoDocumentoCliente == "CI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 180, 314, 0.0f); }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 228, 314, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 742, 0.0f);
                }
            }

                if (request.TipoDocumentoConyuge == "RUC") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 76, 258, 0.0f); }
                else if (request.TipoDocumentoConyuge == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 136, 258, 0.0f); }
                else if (request.TipoDocumentoConyuge == "CI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 180, 258, 0.0f); }
                else if (request.TipoDocumentoConyuge == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 228, 258, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 742, 0.0f);
                }

            return pdfbase64;
        }

        //3
        private String GetCartillaPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.LugarTransaccion}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 50, 418, 0.0f);


            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 418, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 360, 418, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yyyy")}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 410, 418, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 147, 298, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 197, 283, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresCliente2} {request.ApPaternoCliente2} {request.ApMaternoCliente2}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 147, 202, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente2}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 197, 187, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresCliente3} {request.ApPaternoCliente3} {request.ApMaternoCliente3}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 405, 298, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente3}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 455, 283, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombreRepresentante}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 187, 0.0f);

            return pdfbase64;
        }

        //4 
        private String GetSeguroDesgravamenPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //Pagina 4

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 437, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 729, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 729, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 306, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 638, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 388, 638, 15.5f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario1} {request.ApPaternoBeneficiario1} {request.ApMaternoBeneficiario1}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario1}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario1}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario1}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 247, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario1}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 247, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario2} {request.ApPaternoBeneficiario2} {request.ApMaternoBeneficiario2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 233, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 233, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresBeneficiario3} {request.ApPaternoBeneficiario3} {request.ApMaternoBeneficiario3}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoBeneficiario3}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PorcentajeBeneficiario3}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.RelacionBeneficiario3}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 370, 219, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaNacimientoBeneficiario3}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 465, 219, 0.0f);


            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 305, 176, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 150, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente2}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 150, 15.5f);

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
        private String GetSeguroOptativoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            //pagina 10
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 459, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 459, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PrimerNombreCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 445, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.SegundoNombreCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 445, 0.0f);

            if (request.TipoDocumentoCliente == "DNI") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 132, 431, 0.0f); }
            else if (request.TipoDocumentoCliente == "CE") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 166, 431, 0.0f); }
            else if (request.TipoDocumentoCliente == "Otros") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 431, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 510, 431, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 350, 431, 0.0f);

            if (request.SexoCliente == "Femenino") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 510, 431, 0.0f); }
            else if (request.SexoCliente == "Masculino") { pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 534, 431, 0.0f); }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 510, 431, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.OcupacionCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 417, 0.0f);

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaNacCliente = DateTime.MinValue;
            bool formatNacCliente = DateTime.TryParse(request.FechaNacimientoCliente, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacCliente);
            if (formatNacCliente)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacCliente.ToString("dd")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 452, 417, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacCliente.ToString("MM")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 490, 417, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacCliente.ToString("yyyy")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 530, 417, 0.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 92, 404, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DistritoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 404, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ProvinciaCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 92, 390, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 270, 390, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 390, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 137, 370, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.LugarTransaccion}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 340, 312, 0.0f);
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 387, 312, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MMMM", new System.Globalization.CultureInfo("es-PE"))}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 425, 312, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 525, 312, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PrimerNombreCliente} {request.SegundoNombreCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 83, 182, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente} {request.ApMaternoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 83, 167, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 80, 152, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresVendedor} {request.ApPaternoVendedor} {request.ApMaternoVendedor}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 478, 182, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.EmailVendedor}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 460, 167, 0.0f);


            return pdfbase64;
        }

        //6
        private String GetHojaAceptacionPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

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

            return pdfbase64;
        }

        //7
        private String GetHojaOptativoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 240, 724, 0.0f);
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

            return pdfbase64;
        }

        //8
        private String GetHojaResumenPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}
            //Cliente


            //Cliente
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.MontoCredito}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 509, 12.0f);

                if (request.TipoMoneda == "Soles")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 204, 509, 0.0f);
                }
                else if (request.TipoMoneda == "Dolares")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 252, 509, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 740, 0.0f);
                }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.MontoTotal}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 378, 479, 12.0f);

                if (request.TipoMonedaD == "Soles")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 298, 479, 0.0f);
                }
                else if (request.TipoMonedaD == "Dolares")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 357, 479, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 750, 0.0f);
                }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.TasaCredito}", 1, 9, iTextSharp.text.Element.ALIGN_RIGHT, 465, 702, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.PlazoCredito}", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 180, 445, 11.8f);

            switch (request.SeguroDesgravamen)
            {
                case "ConSeguroSaldo":
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 230, 0.0f);
                    break;

                case "ConSeguroDevolucion":
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 145, 0.0f);
                    break;

                case "ConPoliza":
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 49, 58, 0.0f);
                    break;

                case "SinSeguro":
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 1, 9, iTextSharp.text.Element.ALIGN_LEFT, 50, 39, 0.0f);
                    break;
            }

            if (request.TipoGarantia != null)
            {
                if (request.TipoGarantia == "Liquida")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 545, 0.0f);
                }
                else if (request.TipoGarantia == "Mobiliaria")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 545, 0.0f);
                }
                else if (request.TipoGarantia == "Bonos")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 539, 545, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
                }
            }

            //Pagina 2
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);
            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 2, 9, iTextSharp.text.Element.ALIGN_RIGHT, 375, 389, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MMMM", new System.Globalization.CultureInfo("es-PE"))}", 2, 9, iTextSharp.text.Element.ALIGN_CENTER, 445, 389, 0.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 525, 389, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 93, 284, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoCliente} {request.ApMaternoCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 93, 273, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoCliente}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 68, 261, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NombresConyuge}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 425, 284, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.ApPaternoConyuge} {request.ApMaternoConyuge}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 425, 273, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {request.NroDocumentoConyuge}", 2, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 261, 0.0f);

            return pdfbase64;
        }

        //9
        private String GetCartillaAhorroPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);

            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage2, formatSettings.SignFromX2, formatSettings.SignFromY2, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage2, formatSettings.BarcodeFromX2, formatSettings.BarcodeFromY2, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            return pdfbase64;
        }

        //10
        private String GetConsentimientoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //if (!String.IsNullOrEmpty(FingerprintImage) && !String.IsNullOrEmpty(BarCode))
            //{
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, FingerprintImage, formatSettings.SignPage, formatSettings.SignFromX, formatSettings.SignFromY, formatSettings.SignWidth, formatSettings.SignHeight);
            //    pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, BarCode, formatSettings.BarcodePage, formatSettings.BarcodeFromX, formatSettings.BarcodeFromY, formatSettings.BarcodeWidth, formatSettings.BarcodeHeight);
            //}

            return pdfbase64;
        }

        //11
        private String GetDesgravamenSaldoPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            //Pagina 3

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 442, 734, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 734, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 734, 7.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 658, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 658, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 632, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 392, 632, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 734, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 201, 734, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "Hipotecario")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 705, 0.0f);
            }
            else if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 168, 705, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 301, 704, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 453, 704, 0.0f);
            }
            else if (request.TipoCredito == "Pyme")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 687, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 687, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 302, 686, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 453, 685, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 632, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 632, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 3, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }

            // Página 4
            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 198, 420, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 363, 420, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 198, 358, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 363, 358, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 225, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 225, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 200, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 394, 200, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 202, 0.0f);
            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {

                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 202, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 90, 104, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 240, 104, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 353, 104, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 4, 9, iTextSharp.text.Element.ALIGN_CENTER, 482, 104, 0.0f);

            //Pagina 5

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 442, 727, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 727, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 727, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 652, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 652, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 627, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 394, 627, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 728, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 201, 728, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "Hipotecario")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 700, 0.0f);
            }
            else if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 168, 700, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 302, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 453, 701, 0.0f);
            }
            else if (request.TipoCredito == "Pyme")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 682, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 168, 682, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 681, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 453, 681, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 626, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 626, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }

            // Página 6
            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 197, 403, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 363, 402, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 197, 342, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 363, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 208, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 208, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 182, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 394, 182, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 184, 0.0f);
            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 184, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 90, 66, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 240, 66, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 353, 66, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 6, 9, iTextSharp.text.Element.ALIGN_CENTER, 482, 66, 0.0f);

            //Pagina 7

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 447, 740, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 740, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 740, 7.0f);
            }
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 663, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 663, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 637, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 392, 637, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 740, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 201, 740, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "Hipotecario")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 711, 0.0f);
            }
            else if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 168, 711, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 711, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 457, 711, 0.0f);
            }
            else if (request.TipoCredito == "Pyme")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 691, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 168, 692, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 691, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 458, 692, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 637, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 637, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }

            // Página 8
            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 199, 418, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 364, 417, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 200, 356, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 362, 356, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 222, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 222, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 46, 197, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 394, 197, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 199, 0.0f);
            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {

                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 199, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 8, 9, iTextSharp.text.Element.ALIGN_CENTER, 90, 97, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 8, 9, iTextSharp.text.Element.ALIGN_CENTER, 240, 97, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 8, 9, iTextSharp.text.Element.ALIGN_CENTER, 353, 97, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 8, 9, iTextSharp.text.Element.ALIGN_CENTER, 482, 97, 0.0f);


            return pdfbase64;
        }

        //12
        private String GetDesgravamenDevolucionPDF(FormatSettings formatSettings, BpmRequest request/*, String FingerprintImage, String BarCode*/)
        {
            String pdfbase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, formatSettings.PathFileBase)));

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-PE");
            DateTime fechaTransaccion = DateTime.MinValue;
            bool formatTransaccion = DateTime.TryParse(request.FechaTransaccion, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaTransaccion);


            //Pagina 4

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 442, 743, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 743, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 743, 7.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 30, 660, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 292, 660, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 30, 634, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 408, 634, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 122, 743, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 193, 743, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 115, 719, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalEstudios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 719, 0.0f);
            }
            else if (request.TipoCredito == "Vehicular")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 321, 719, 0.0f);
            }
            else if (request.TipoCredito == "Pyme")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 408, 719, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 484, 719, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 115, 696, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 696, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 321, 696, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 408, 696, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

            if (request.TipoDocumentoCliente == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 213, 634, 0.0f);
            }
            else if (request.TipoDocumentoCliente == "CE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 270, 634, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            }

            // Página 5
            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 422, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 424, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 364, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 366, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 233, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 233, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 36, 208, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 387, 208, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 282, 209, 0.0f);
            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 338, 209, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 80, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 230, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 343, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 5, 9, iTextSharp.text.Element.ALIGN_CENTER, 472, 110, 0.0f);


            //Pagina 6

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 442, 748, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 748, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 748, 7.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 45, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 664, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 45, 638, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 638, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 748, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 748, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "Hipotecario")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 724, 0.0f);
            }
            else if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 109, 724, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalEstudios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 724, 0.0f);
            }
            else if (request.TipoCredito == "Vehicular")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 323, 724, 0.0f);
            }
            else if (request.TipoCredito == "Pymes")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 409, 724, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 491, 724, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 109, 701, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 701, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 323, 701, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 409, 701, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 4, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

            if (request.TipoDocumentoCliente != null)
            {
                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 639, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 345, 639, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 6, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }
            }

            // Página 7

            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 416, 0.0f);

            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 416, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 358, 0.0f);

            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 362, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 238, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 308, 238, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 44, 213, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 390, 213, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 214, 0.0f);

            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {

                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 7, 9, iTextSharp.text.Element.ALIGN_LEFT, 345, 214, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 5, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 7, 9, iTextSharp.text.Element.ALIGN_CENTER, 80, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 7, 9, iTextSharp.text.Element.ALIGN_CENTER, 230, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 7, 9, iTextSharp.text.Element.ALIGN_CENTER, 343, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 7, 9, iTextSharp.text.Element.ALIGN_CENTER, 472, 110, 0.0f);


            //Pagina 8

            if (formatTransaccion)
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 442, 760, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 483, 760, 7.0f);
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 527, 760, 7.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 45, 676, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 676, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 45, 650, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 650, 15.5f);

            if (request.TipoMoneda == "Soles")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 130, 760, 0.0f);
            }
            else if (request.TipoMoneda == "Dolares")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 202, 760, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            }

            //Tipo de Credito
            if (request.TipoCredito == "Hipotecario")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 736, 0.0f);
            }
            else if (request.TipoCredito == "NuevoMiVivienda")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 109, 736, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalEstudios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 736, 0.0f);
            }
            else if (request.TipoCredito == "Vehicular")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 323, 736, 0.0f);
            }
            else if (request.TipoCredito == "Pymes")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 409, 736, 0.0f);
            }
            else if (request.TipoCredito == "TechoPropio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 491, 736, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoNegocio")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 713, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonal")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 109, 713, 0.0f);
            }
            else if (request.TipoCredito == "PrestamoPersonalColaborador")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 215, 713, 0.0f);
            }
            else if (request.TipoCredito == "Convenios")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 323, 713, 0.0f);
            }
            else if (request.TipoCredito == "VehicularGNV")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 409, 713, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            }

            if (request.TipoDocumentoCliente != null)
            {
                if (request.TipoDocumentoCliente == "DNI")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 289, 650, 0.0f);
                }
                else if (request.TipoDocumentoCliente == "CE")
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 345, 650, 0.0f);
                }
                else
                {
                    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 8, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
                }
            }

            // Página 9

            if (request.PrimerConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 416, 0.0f);
            }
            else if (request.PrimerConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 416, 0.0f);
            }

            if (request.SegundoConsentimiento == "true")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 188, 360, 0.0f);
            }
            else if (request.SegundoConsentimiento == "false")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 362, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 39, 232, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 302, 232, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 39, 207, 15.5f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 384, 207, 15.5f);

            if (request.TipoDocumentoFirmanteAdicional == "DNI")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 283, 208, 0.0f);
            }
            else if (request.TipoDocumentoFirmanteAdicional == "CE")
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 338, 208, 0.0f);
            }
            else
            {
                pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 9, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            }

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 9, 9, iTextSharp.text.Element.ALIGN_CENTER, 80, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 9, 9, iTextSharp.text.Element.ALIGN_CENTER, 230, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 9, 9, iTextSharp.text.Element.ALIGN_CENTER, 343, 110, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 9, 9, iTextSharp.text.Element.ALIGN_CENTER, 472, 110, 0.0f);



            //Pagina 10

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 433, 734, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 478, 734, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 524, 734, 7.0f);
            //}
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 650, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 303, 650, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 37, 624, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 386, 624, 15.7f);

            //if (request.TipoMoneda == "Soles")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 123, 734, 0.0f);
            //}
            //else if (request.TipoMoneda == "Dolares")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 194, 734, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            //}

            ////Tipo de Credito
            //if (request.TipoCredito == "Hipotecario")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 710, 0.0f);
            //}
            //else if (request.TipoCredito == "NuevoMiVivienda")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 113, 710, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalEstudios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 225, 709, 0.0f);
            //}
            //else if (request.TipoCredito == "Vehicular")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 338, 709, 0.0f);
            //}
            //else if (request.TipoCredito == "Pyme")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 709, 0.0f);
            //}
            //else if (request.TipoCredito == "TechoPropio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 488, 710, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoNegocio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 26, 686, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 113, 686, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalColaborador")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 225, 686, 0.0f);
            //}
            //else if (request.TipoCredito == "Convenios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 338, 686, 0.0f);
            //}
            //else if (request.TipoCredito == "VehicularGNV")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 421, 686, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            //}

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 282, 624, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 339, 624, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //// Página 11
            //if (request.PrimerConsentimiento == "true")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 189, 404, 0.0f);
            //}
            //else if (request.PrimerConsentimiento == "false")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 404, 0.0f);
            //}

            //if (request.SegundoConsentimiento == "true")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 189, 345, 0.0f);
            //}
            //else if (request.SegundoConsentimiento == "false")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 354, 347, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoFirmanteAdicional}", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 214, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoFirmanteAdicional}", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 214, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFirmanteAdicional}", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 189, 15.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoFirmanteAdicional}", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 393, 189, 15.5f);

            //if (request.TipoDocumentoFirmanteAdicional == "DNI")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 290, 190, 0.0f);
            //}
            //else if (request.TipoDocumentoFirmanteAdicional == "CE")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 346, 190, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 11, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 340, 0.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.Entidad}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 90, 74, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombreAgencia}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 240, 74, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 353, 74, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 11, 9, iTextSharp.text.Element.ALIGN_CENTER, 482, 74, 0.0f);


            ////Pagina 12

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 485, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 520, 730, 7.0f);
            //}
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 315, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 612, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 612, 11.5f);

            //if (request.TipoMoneda == "Soles")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 142, 729, 0.0f);

            //}
            //else if (request.TipoMoneda == "Dolares")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 729, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            //}

            ////Tipo de Credito
            //if (request.TipoCredito == "Hipotecario")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "NuevoMiVivienda")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 128, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalEstudios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Vehicular")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 319, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Pyme")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 401, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "TechoPropio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 478, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoNegocio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 127, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalColaborador")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "Convenios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 318, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "VehicularGNV")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 680, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            //}

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 612, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 320, 612, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //if (request.GeneroCliente != null)
            //{
            //    if (request.GeneroCliente == "Femenino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 582, 0.0f); 
            //    }
            //    else if (request.GeneroCliente == "Masculino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 78, 582, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //DateTime fechaNacimiento = DateTime.MinValue;
            //bool formatNacimiento = DateTime.TryParse(request.FechaNacimientoCliente, cultureinfo, System.Globalization.DateTimeStyles.None, out fechaNacimiento);

            //if (formatNacimiento)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("dd")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 108, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("MM")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 157, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("yyyy")}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 582, 10.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoSolicitado}", 12, 9, iTextSharp.text.Element.ALIGN_CENTER, 330, 582, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PlazoCredito}", 12, 9, iTextSharp.text.Element.ALIGN_CENTER, 470, 582, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDireccionCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 398, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoInterior}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 460, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionPisoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 490, 552, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UrbanizacionCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DistritoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ProvinciaCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 435, 523, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CelularCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 494, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstaturaCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 235, 421, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PesoCliente}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 355, 421, 0.0f);


            //if (request.ConsumoCigarrillos == "Ninguno")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 406, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Menosde5")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Entre5y25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 414, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Masde25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 488, 405, 0.0f);
            //}

            //if (request.EstadoSalud == "Normal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 375, 0.0f);
            //}
            //else if (request.EstadoSalud == "Anormal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 31, 327, 0.0f);
            //}


            //if (request.Cancer)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 255, 0.0f);

            //    if (request.CancerMama)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerMama}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerMama}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerMama}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerMama}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 274, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 274, 0.0f);
            //    }

            //    if (request.CancerColon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerColon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerColon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerColon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerColon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 260, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 260, 0.0f);
            //    }

            //    if (request.CancerPulmon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerPulmon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerPulmon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerPulmon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerPulmon}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 247, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 247, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CancerOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 234, 0.0f);

            //}

            //if (request.CardioVascular)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 208, 0.0f);
            //    if (request.CardiopatiaCoronaria)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardiopatiaCoronaria}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardiopatiaCoronaria}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardiopatiaCoronaria}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardiopatiaCoronaria}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 221, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 221, 0.0f);
            //    }

            //    if (request.InsuficienciaCardiaca)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoInsuficienciaCardiaca}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoInsuficienciaCardiaca}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteInsuficienciaCardiaca}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteInsuficienciaCardiaca}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 208, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 208, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CardioOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardioOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardioOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardioOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardioOtro}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 195, 0.0f);
            //}

            //if (request.Renal)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoRenal}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoRenal}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteRenal}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteRenal}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 182, 0.0f);
            //}

            //if (request.Diabetes)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoDiabetes}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoDiabetes}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteDiabetes}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteDiabetes}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 169, 0.0f);
            //}

            //if (request.Neurologicas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoNeurologicas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoNeurologicas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteNeurologicas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteNeurologicas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 156, 0.0f);
            //}

            //if (request.Psiquiatricas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoPsiquiatricas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoPsiquiatricas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntrantePsiquiatricas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntrantePsiquiatricas}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 143, 0.0f);
            //}

            //if (request.EnfermedadesRespiratorias)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoEnfermedadesRespiratorias}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoEnfermedadesRespiratorias}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteEnfermedadesRespiratorias}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteEnfermedadesRespiratorias}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 130, 0.0f);
            //}

            //if (request.SIDA)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoSIDA}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoSIDA}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteSIDA}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteSIDA}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 110, 0.0f);
            //}

            //if (request.OtrasEnfermedades)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoOtrasEnfermedades}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoOtrasEnfermedades}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteOtrasEnfermedades}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteOtrasEnfermedades}", 12, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 97, 0.0f);
            //}

            ////Pagina 13

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 485, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 520, 730, 7.0f);
            //}
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 315, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 612, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 612, 11.5f);

            //if (request.TipoMoneda == "Soles")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 142, 729, 0.0f);

            //}
            //else if (request.TipoMoneda == "Dolares")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 729, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            //}

            ////Tipo de Credito
            //if (request.TipoCredito == "Hipotecario")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "NuevoMiVivienda")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 128, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalEstudios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Vehicular")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 319, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Pyme")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 401, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "TechoPropio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 478, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoNegocio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 127, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalColaborador")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "Convenios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 318, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "VehicularGNV")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 680, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            //}

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 612, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 320, 612, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //if (request.GeneroCliente != null)
            //{
            //    if (request.GeneroCliente == "Femenino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 582, 0.0f);
            //    }
            //    else if (request.GeneroCliente == "Masculino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 78, 582, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //if (formatNacimiento)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("dd")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 108, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("MM")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 157, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("yyyy")}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 582, 10.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoSolicitado}", 13, 9, iTextSharp.text.Element.ALIGN_CENTER, 330, 582, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PlazoCredito}", 13, 9, iTextSharp.text.Element.ALIGN_CENTER, 470, 582, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDireccionCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 398, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoInterior}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 460, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionPisoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 490, 552, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UrbanizacionCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DistritoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ProvinciaCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 435, 523, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CelularCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 494, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstaturaCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 235, 421, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PesoCliente}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 355, 421, 0.0f);


            //if (request.ConsumoCigarrillos == "Ninguno")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 406, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Menosde5")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Entre5y25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 414, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Masde25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 488, 405, 0.0f);
            //}

            //if (request.EstadoSalud == "Normal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 375, 0.0f);
            //}
            //else if (request.EstadoSalud == "Anormal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 31, 327, 0.0f);
            //}


            //if (request.Cancer)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 255, 0.0f);

            //    if (request.CancerMama)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerMama}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerMama}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerMama}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerMama}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 274, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 274, 0.0f);
            //    }

            //    if (request.CancerColon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerColon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerColon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerColon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerColon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 260, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 260, 0.0f);
            //    }

            //    if (request.CancerPulmon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerPulmon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerPulmon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerPulmon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerPulmon}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 247, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 247, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CancerOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 234, 0.0f);

            //}

            //if (request.CardioVascular)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 208, 0.0f);
            //    if (request.CardiopatiaCoronaria)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardiopatiaCoronaria}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardiopatiaCoronaria}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardiopatiaCoronaria}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardiopatiaCoronaria}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 221, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 221, 0.0f);
            //    }

            //    if (request.InsuficienciaCardiaca)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoInsuficienciaCardiaca}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoInsuficienciaCardiaca}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteInsuficienciaCardiaca}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteInsuficienciaCardiaca}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 208, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 208, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CardioOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardioOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardioOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardioOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardioOtro}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 195, 0.0f);
            //}

            //if (request.Renal)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoRenal}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoRenal}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteRenal}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteRenal}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 182, 0.0f);
            //}

            //if (request.Diabetes)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoDiabetes}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoDiabetes}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteDiabetes}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteDiabetes}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 169, 0.0f);
            //}

            //if (request.Neurologicas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoNeurologicas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoNeurologicas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteNeurologicas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteNeurologicas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 156, 0.0f);
            //}

            //if (request.Psiquiatricas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoPsiquiatricas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoPsiquiatricas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntrantePsiquiatricas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntrantePsiquiatricas}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 143, 0.0f);
            //}

            //if (request.EnfermedadesRespiratorias)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoEnfermedadesRespiratorias}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoEnfermedadesRespiratorias}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteEnfermedadesRespiratorias}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteEnfermedadesRespiratorias}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 130, 0.0f);
            //}

            //if (request.SIDA)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoSIDA}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoSIDA}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteSIDA}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteSIDA}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 110, 0.0f);
            //}

            //if (request.OtrasEnfermedades)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoOtrasEnfermedades}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoOtrasEnfermedades}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteOtrasEnfermedades}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteOtrasEnfermedades}", 13, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 97, 0.0f);
            //}
            ////Pagina 14

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 450, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("MM")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 485, 730, 7.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("yy")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 520, 730, 7.0f);
            //}
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApPaternoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ApMaternoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 315, 642, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 35, 612, 11.5f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 365, 612, 11.5f);

            //if (request.TipoMoneda == "Soles")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 142, 729, 0.0f);

            //}
            //else if (request.TipoMoneda == "Dolares")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 203, 729, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 184, 730, 0.0f);
            //}

            ////Tipo de Credito
            //if (request.TipoCredito == "Hipotecario")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "NuevoMiVivienda")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 128, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalEstudios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Vehicular")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 319, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "Pyme")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 401, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "TechoPropio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 478, 702, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoNegocio")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 127, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "PrestamoPersonalColaborador")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 212, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "Convenios")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 318, 680, 0.0f);
            //}
            //else if (request.TipoCredito == "VehicularGNV")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 400, 680, 0.0f);
            //}
            //else
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 330, 701, 0.0f);
            //}

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 612, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 320, 612, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //if (request.GeneroCliente != null)
            //{
            //    if (request.GeneroCliente == "Femenino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 582, 0.0f);
            //    }
            //    else if (request.GeneroCliente == "Masculino")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 78, 582, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 10, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //if (formatNacimiento)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("dd")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 108, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("MM")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 157, 582, 9.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaNacimiento.ToString("yyyy")}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 582, 10.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MontoSolicitado}", 14, 9, iTextSharp.text.Element.ALIGN_CENTER, 330, 582, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PlazoCredito}", 14, 9, iTextSharp.text.Element.ALIGN_CENTER, 470, 582, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDireccionCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 398, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoInterior}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 460, 552, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DireccionPisoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 490, 552, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.UrbanizacionCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DistritoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.ProvinciaCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 523, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.DepartamentoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 435, 523, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.TelefonoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 34, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CelularCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 167, 494, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 310, 494, 0.0f);

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstaturaCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 235, 421, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.PesoCliente}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 355, 421, 0.0f);


            //if (request.ConsumoCigarrillos == "Ninguno")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 271, 406, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Menosde5")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Entre5y25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 414, 405, 0.0f);
            //}
            //else if (request.ConsumoCigarrillos == "Masde25")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 488, 405, 0.0f);
            //}

            //if (request.EstadoSalud == "Normal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 33, 375, 0.0f);
            //}
            //else if (request.EstadoSalud == "Anormal")
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 31, 327, 0.0f);
            //}


            //if (request.Cancer)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 255, 0.0f);

            //    if (request.CancerMama)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerMama}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerMama}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerMama}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 274, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerMama}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 274, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 274, 0.0f);
            //    }

            //    if (request.CancerColon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerColon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerColon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerColon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 260, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerColon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 260, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 260, 0.0f);
            //    }

            //    if (request.CancerPulmon)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerPulmon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerPulmon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerPulmon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 247, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerPulmon}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 247, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 247, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CancerOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCancerOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCancerOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCancerOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 234, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCancerOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 234, 0.0f);

            //}

            //if (request.CardioVascular)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 208, 0.0f);
            //    if (request.CardiopatiaCoronaria)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardiopatiaCoronaria}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardiopatiaCoronaria}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardiopatiaCoronaria}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 221, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardiopatiaCoronaria}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 221, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 221, 0.0f);
            //    }

            //    if (request.InsuficienciaCardiaca)
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 245, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoInsuficienciaCardiaca}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoInsuficienciaCardiaca}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteInsuficienciaCardiaca}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 208, 0.0f);
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteInsuficienciaCardiaca}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 208, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 275, 208, 0.0f);
            //    }

            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.CardioOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 205, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoCardioOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoCardioOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteCardioOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 195, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteCardioOtro}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 195, 0.0f);
            //}

            //if (request.Renal)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoRenal}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoRenal}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteRenal}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 182, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteRenal}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 182, 0.0f);
            //}

            //if (request.Diabetes)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoDiabetes}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoDiabetes}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteDiabetes}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 169, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteDiabetes}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 169, 0.0f);
            //}

            //if (request.Neurologicas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoNeurologicas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoNeurologicas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteNeurologicas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 156, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteNeurologicas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 156, 0.0f);
            //}

            //if (request.Psiquiatricas)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoPsiquiatricas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoPsiquiatricas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntrantePsiquiatricas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 143, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntrantePsiquiatricas}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 143, 0.0f);
            //}

            //if (request.EnfermedadesRespiratorias)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoEnfermedadesRespiratorias}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoEnfermedadesRespiratorias}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteEnfermedadesRespiratorias}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 130, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteEnfermedadesRespiratorias}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 130, 0.0f);
            //}

            //if (request.SIDA)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoSIDA}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoSIDA}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteSIDA}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 110, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteSIDA}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 110, 0.0f);
            //}

            //if (request.OtrasEnfermedades)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 38, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.FechaDiagnosticoOtrasEnfermedades}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 285, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EstadoOtrasEnfermedades}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 343, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.IsntitucionMedicaEntranteOtrasEnfermedades}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 395, 97, 0.0f);
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.MedicoEntranteOtrasEnfermedades}", 14, 9, iTextSharp.text.Element.ALIGN_LEFT, 475, 97, 0.0f);
            //}

            ////Página 15
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 373, 385, 0.0f);

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 428, 350, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 508, 350, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 638, 0.0f);
            //    }
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 373, 332, 11.0f);

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd/MM/yyyy")}", 15, 9, iTextSharp.text.Element.ALIGN_LEFT, 425, 245, 7.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 15, 9, iTextSharp.text.Element.ALIGN_CENTER, 105, 122, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 15, 9, iTextSharp.text.Element.ALIGN_CENTER, 482, 122, 0.0f);


            ////Página 16
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 368, 385, 0.0f);

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 423, 350, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 503, 350, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 326, 633, 0.0f);
            //    }
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 368, 332, 11.0f);

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd/MM/yyyy")}", 16, 9, iTextSharp.text.Element.ALIGN_LEFT, 420, 245, 7.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 16, 9, iTextSharp.text.Element.ALIGN_CENTER, 100, 122, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 16, 9, iTextSharp.text.Element.ALIGN_CENTER, 477, 122, 0.0f);

            ////Página 17
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresCliente} {request.ApPaternoCliente} {request.ApMaternoCliente}", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 368, 380, 0.0f);

            //if (request.TipoDocumentoCliente != null)
            //{
            //    if (request.TipoDocumentoCliente == "DNI")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 423, 346, 0.0f);
            //    }
            //    else if (request.TipoDocumentoCliente == "CE")
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "X", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 503, 346, 0.0f);
            //    }
            //    else
            //    {
            //        pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, "", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 321, 634, 0.0f);
            //    }
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NroDocumentoCliente}", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 369, 328, 11.0f);

            //if (formatTransaccion)
            //{
            //    pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $" {fechaTransaccion.ToString("dd/MM/yyyy")}", 17, 9, iTextSharp.text.Element.ALIGN_LEFT, 425, 242, 7.0f);
            //}

            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.NombresFuncionario}", 17, 9, iTextSharp.text.Element.ALIGN_CENTER, 100, 120, 0.0f);
            //pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"{request.EmailFuncionario}", 17, 9, iTextSharp.text.Element.ALIGN_CENTER, 477, 120, 0.0f);


            return pdfbase64;
        }


        #region AddHoja Firmas
        private string AddPageSign(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)
        {
            DateTime fechaReniec = DateTime.Now;

            int numberOfPages;

            String pdfbase64 = PdfWorker.AddPage(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkP.png")));

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

            Stamp = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/logo_pichincha.png")));
            Empresa = "BANCO PICHINCHA";
            Direccion = "Av. Ricardo Palma Nro. 278 Res. Miraflores (Ovalo Central de Miraflores)";


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

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkP.png")));

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

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkP.png")));

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

            Stamp = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/logo_pichincha.png")));
            Empresa = "BANCO PICHINCHA";
            Direccion = "Av. Ricardo Palma Nro. 278 Res. Miraflores (Ovalo Central de Miraflores)";


            pdfbase64 = PdfWorker.WriteImageInPdf(pdfbase64, Stamp, numberOfPages, 50, 50, 147, 50);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"El presente documento se encuentra firmado digitalmente de acuerdo a ", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 85, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Ley N° 27269 – Ley de Firmas y Certificados Digitales vigente en Perú.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 75, 0.0f);

            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, $"Firmado digitalmente por " + Empresa, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 65, 0.0f);
            pdfbase64 = PdfWorker.WriteTextInPdf(pdfbase64, Direccion, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, 215, 55, 0.0f);

            return pdfbase64;
        }

        private string ExistingPageFacial(string pdfBase64, string FacialImage, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string DocumentoIdentidad, int x, int y)
        {
            DateTime fechaReniec = DateTime.Now;
            int numberOfPages;

            PdfWorker.GetMaxPageNumber(pdfBase64, out numberOfPages);

            String watermark = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "images/watermarkP.png")));

            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno + " - " + "1" + " " + DocumentoIdentidad, numberOfPages, 10, iTextSharp.text.Element.ALIGN_LEFT, x - 10, y + 190);
            pdfBase64 = PdfWorker.DrawLineInPdf(pdfBase64, numberOfPages, x - 15, y + 185, x + 495, y + 185);

            pdfBase64 = PdfWorker.DrawRectangleInPdf(pdfBase64, numberOfPages, x - 15, y - 25, 150, 200);

            pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, FacialImage, numberOfPages, x + 25, y + 50, 70, 120);
            pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, watermark, numberOfPages, x, y + 50, 120, 120);

            //pdfBase64 = PdfWorker.WriteImageInPdf(pdfBase64, BarCode, numberOfPages, x, y + 30, 120, 20);

            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente con", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biometría facial utilizando el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "servicio de verificación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica de Reniec con fecha", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            //pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy"), numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Firmado electrónicamente el", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 20);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, fechaReniec.ToString("dd/MM/yyyy") + " con tecnología", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y + 10);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "Bit4ID S.A.C. y validación", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "biométrica facial a través de", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 10);
            pdfBase64 = PdfWorker.WriteTextInPdf(pdfBase64, "la tecnología de Facetec Inc.", numberOfPages, 10, iTextSharp.text.Element.ALIGN_CENTER, x + 60, y - 20);

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
                ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
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
                ExceptionManager.Instance.ManageException<BpmController>(ex, _logger);
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
