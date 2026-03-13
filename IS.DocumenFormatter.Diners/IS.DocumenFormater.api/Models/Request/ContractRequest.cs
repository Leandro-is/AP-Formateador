using System;
using System.Runtime.Serialization;

namespace IS.DocumenFormater.api.Models.Request
{
    public class ContractRequest
    {
        #region Hojafirmas
        [DataMember]
        public String FormatoCronograma { get; set; }

        [DataMember]
        public String AddHojaNombres { get; set; }

        [DataMember]
        public String AddHojaApellidoPaterno { get; set; }

        [DataMember]
        public String AddHojaApellidoMaterno { get; set; }

        [DataMember]
        public String AddHojaDocumentoIdentidad { get; set; }

        [DataMember]
        public String AddHojaNombres2 { get; set; }

        [DataMember]
        public String AddHojaApellidoPaterno2 { get; set; }

        [DataMember]
        public String AddHojaApellidoMaterno2 { get; set; }

        [DataMember]
        public String AddHojaDocumentoIdentidad2 { get; set; }

        [DataMember]
        public String AddHojaNombres3 { get; set; }

        [DataMember]
        public String AddHojaApellidoPaterno3 { get; set; }

        [DataMember]
        public String AddHojaApellidoMaterno3 { get; set; }

        [DataMember]
        public String AddHojaDocumentoIdentidad3 { get; set; }

        #endregion

        [DataMember]
        public String NroDocumentoCliente { get; set; }

        [DataMember]
        public String NombresCliente { get; set; }
        [DataMember]
        public String ApPaternoCliente { get; set; }
        [DataMember]
        public String ApMaternoCliente { get; set; }
        [DataMember]
        public virtual TipoDoc? TipoDocumentoCliente { get; set; }
        [DataMember]
        public String DireccionCliente { get; set; }
        [DataMember]
        public String NroDireccionCliente { get; set; }
        [DataMember]
        public virtual String DireccionInteriorCliente { get; set; }
        [DataMember]
        public virtual SexoD? SexoCliente { get; set; }
        [DataMember]
        public String FechaNacimientoCliente { get; set; }
        [DataMember]
        public String EmailCliente { get; set; }
        [DataMember]
        public String TelefonoCliente { get; set; }
        [DataMember]
        public String CelularCliente { get; set; }
        [DataMember]
        public virtual String ImpresionBiometricaCliente { get; set; }
        [DataMember]
        public String RazonSocialCliente { get; set; }
        [DataMember]
        public String RucCliente { get; set; }
        [DataMember]
        public String RepresentanteCliente { get; set; }
        [DataMember]
        public String DocRepresentanteCliente { get; set; }
        [DataMember]
        public String PoderesCliente { get; set; }

        [DataMember]
        public String MontoCredito { get; set; }
        [DataMember]
        public String LugarTransaccion { get; set; }
        [DataMember]
        public String FechaTransaccion { get; set; }
        [DataMember]
        public String MontoTotal { get; set; }
        [DataMember]
        public Moneda TipoMonedaD { get; set; }

        //Beneficiario 1
        [DataMember]
        public String NombresBeneficiario1 { get; set; }
        [DataMember]
        public String ApPaternoBeneficiario1 { get; set; }
        [DataMember]
        public String ApMaternoBeneficiario1 { get; set; }
        [DataMember]
        public virtual String TipoDocumentoBeneficiario1 { get; set; }
        [DataMember]
        public String NroDocumentoBeneficiario1 { get; set; }
        [DataMember]
        public String RelacionBeneficiario1 { get; set; }
        [DataMember]
        public String PorcentajeBeneficiario1 { get; set; }
        [DataMember]
        public String FechaNacimientoBeneficiario1 { get; set; }

        //Beneficiario 2
        [DataMember]
        public String NombresBeneficiario2 { get; set; }
        [DataMember]
        public String ApPaternoBeneficiario2 { get; set; }
        [DataMember]
        public String ApMaternoBeneficiario2 { get; set; }
        [DataMember]
        public virtual String TipoDocumentoBeneficiario2 { get; set; }
        [DataMember]
        public String NroDocumentoBeneficiario2 { get; set; }
        [DataMember]
        public String RelacionBeneficiario2 { get; set; }
        [DataMember]
        public String PorcentajeBeneficiario2 { get; set; }
        [DataMember]
        public String FechaNacimientoBeneficiario2 { get; set; }

        //Beneficiario 3
        [DataMember]
        public String NombresBeneficiario3 { get; set; }
        [DataMember]
        public String ApPaternoBeneficiario3 { get; set; }
        [DataMember]
        public String ApMaternoBeneficiario3 { get; set; }
        [DataMember]
        public virtual String TipoDocumentoBeneficiario3 { get; set; }
        [DataMember]
        public String NroDocumentoBeneficiario3 { get; set; }
        [DataMember]
        public String RelacionBeneficiario3 { get; set; }
        [DataMember]
        public String PorcentajeBeneficiario3 { get; set; }
        [DataMember]
        public virtual bool EstadoConsentimiento { get; set; }
        [DataMember]
        public String FechaNacimientoBeneficiario3 { get; set; }

        //Cliente 2
        [DataMember]
        public String NombresCliente2 { get; set; }
        [DataMember]
        public String ApPaternoCliente2 { get; set; }
        [DataMember]
        public String ApMaternoCliente2 { get; set; }
        [DataMember]
        public String NroDocumentoCliente2 { get; set; }
        [DataMember]
        public virtual String ImpresionBiometricaCliente2 { get; set; }
        [DataMember]
        public virtual TipoDoc? TipoDocumentoCliente2 { get; set; }

        //Datos institucion
        [DataMember]
        public String NombreInstitucion1 { get; set; }
        [DataMember]
        public TipoTarjeta? TipoTarjeta1 { get; set; }
        [DataMember]
        public String NumeroTarjeta1 { get; set; }
        [DataMember]
        public Valor? TipoValor1 { get; set; }
        [DataMember]
        public Moneda? TipoMoneda1 { get; set; }
        [DataMember]
        public String MontoCancelar1 { get; set; }

        [DataMember]
        public String NombreInstitucion2 { get; set; }
        [DataMember]
        public TipoTarjeta? TipoTarjeta2 { get; set; }
        [DataMember]
        public String NumeroTarjeta2 { get; set; }
        [DataMember]
        public Valor? TipoValor2 { get; set; }
        [DataMember]
        public Moneda? TipoMoneda2 { get; set; }
        [DataMember]
        public String MontoCancelar2 { get; set; }

        [DataMember]
        public String NombreInstitucion3 { get; set; }
        [DataMember]
        public TipoTarjeta? TipoTarjeta3 { get; set; }
        [DataMember]
        public String NumeroTarjeta3 { get; set; }
        [DataMember]
        public Valor? TipoValor3 { get; set; }
        [DataMember]
        public Moneda? TipoMoneda3 { get; set; }
        [DataMember]
        public String MontoCancelar3 { get; set; }

        [DataMember]
        public String NombreInstitucion4 { get; set; }
        [DataMember]
        public TipoTarjeta? TipoTarjeta4 { get; set; }
        [DataMember]
        public String NumeroTarjeta4 { get; set; }
        [DataMember]
        public Valor? TipoValor4 { get; set; }
        [DataMember]
        public Moneda? TipoMoneda4 { get; set; }
        [DataMember]
        public String MontoCancelar4 { get; set; }



        [DataMember]
        public virtual Moneda? TipoMoneda { get; set; }
        [DataMember]
        public virtual Credito? TipoCredito { get; set; }
        [DataMember]
        public String PlazoCredito { get; set; }

        //Conyuge
        [DataMember]
        public String NombresConyuge { get; set; }
        [DataMember]
        public String ApPaternoConyuge { get; set; }
        [DataMember]
        public String ApMaternoConyuge { get; set; }
        [DataMember]
        public virtual TipoDoc? TipoDocumentoConyuge { get; set; }
        [DataMember]
        public String NroDocumentoConyuge { get; set; }
        [DataMember]
        public virtual String ImpresionBiometricaConyuge { get; set; }
        [DataMember]
        public String FechaNacimientoConyuge { get; set; }
        [DataMember]
        public String NacionalidadConyuge { get; set; }
        [DataMember]
        public String NroDependientesConyuge { get; set; }
        [DataMember]
        public String EmailConyuge { get; set; }
        [DataMember]
        public String CelularConyuge { get; set; }
        [DataMember]
        public String CodigoConyuge { get; set; }
        [DataMember]
        public String TelefonoConyuge { get; set; }
        [DataMember]
        public String RUCConyuge { get; set; }
        [DataMember]
        public String CentroActualConyuge { get; set; }
        [DataMember]
        public String CargoActualConyuge { get; set; }
        [DataMember]
        public String GiroConyuge { get; set; }
        [DataMember]
        public String FechaIngresoConyuge { get; set; }
        [DataMember]
        public String UbigeoEmpresaConyuge { get; set; }
        [DataMember]
        public String ReferenciaEmpresaConyuge { get; set; }
        [DataMember]
        public String CodigoCiudadEmpresaConyuge { get; set; }
        [DataMember]
        public String TelefonoEmpresaConyuge { get; set; }
        [DataMember]
        public String FechaFinContratoConyuge { get; set; }
        [DataMember]
        public String MontoIngresoConyuge { get; set; }
        [DataMember]
        public String MontoOtroIngresoConyuge { get; set; }
        [DataMember]
        public EstadoC? EstadoCivilConyuge { get; set; }
        [DataMember]
        public bool FuncionesConyuge { get; set; }
        [DataMember]
        public ViviendaT? ViviendaConyuge { get; set; }
        [DataMember]
        public SexoD? SexoConyuge { get; set; }
        [DataMember]
        public GradoI? GradoConyuge { get; set; }
        [DataMember]
        public bool ContinuidadConyuge { get; set; }
        [DataMember]
        public SituacionL? SituacionLaboralConyuge { get; set; }


        //RepresentanteBanco
        [DataMember]
        public String NombreRepresentante { get; set; }
        [DataMember]
        public String NombreAgencia { get; set; }
        [DataMember]
        public String CodVendedor { get; set; }
        [DataMember]
        public String NroSolicitud { get; set; }

        [DataMember]
        public Canal? CanalVenta { get; set; }
        [DataMember]
        public Cuota? TipoCuota { get; set; }
        [DataMember]
        public bool PeriodoGracia { get; set; }
        [DataMember]
        public String PeriodoGraciaDet { get; set; }
        [DataMember]
        public String TasaCredito { get; set; }
        [DataMember]
        public String NroCuentaD { get; set; }
        [DataMember]
        public String NroCtaTransfer { get; set; }
        [DataMember]
        public String Entidad { get; set; }
        [DataMember]
        public Desembolso? TipoDesembolso { get; set; }
        [DataMember]
        public Garantia? TipoGarantia { get; set; }
        [DataMember]
        public String Nacionalidad { get; set; }
        [DataMember]
        public String NroDependientes { get; set; }
        [DataMember]
        public EstadoC? EstadoCivil { get; set; }
        [DataMember]
        public bool Funciones { get; set; }
        [DataMember]
        public String CodigoCiudad { get; set; }
        [DataMember]
        public ViviendaT? Vivienda { get; set; }
        [DataMember]
        public GradoI? GradoInstruccion { get; set; }
        [DataMember]
        public bool ContinuidadLaboral { get; set; }
        [DataMember]
        public SituacionL? SituacionLaboral { get; set; }
        [DataMember]
        public DireccionDetalleD? DireccionDetalleCliente { get; set; }
        [DataMember]
        public DireccionDetalleExteriorD? DireccionDetalleExteriorCliente { get; set; }
        [DataMember]
        public DireccionDetalleInteriorD? DireccionDetalleInteriorCliente { get; set; }
        [DataMember]
        public DireccionDetalleZonaD? DireccionDetalleZonaCliente { get; set; }
        [DataMember]
        public String DireccionExteriorCliente { get; set; }
        [DataMember]
        public String DireccionZonaCliente { get; set; }
        [DataMember]
        public String UbiegoCliente { get; set; }
        [DataMember]
        public String ReferenciaCliente { get; set; }
        [DataMember]
        public String CentroActualTitular { get; set; }
        [DataMember]
        public String CargoActualTitular { get; set; }
        [DataMember]
        public String GiroTitular { get; set; }
        [DataMember]
        public String FechaIngresoTitular { get; set; }
        [DataMember]
        public String DireccionEmpresaT { get; set; }
        [DataMember]
        public String DireccionExteriorEmpresaT { get; set; }
        [DataMember]
        public String DireccionInteriorEmpresaT { get; set; }
        [DataMember]
        public String DireccionZonaEmpresaT { get; set; }
        [DataMember]
        public String UbiegoEmpresaTitular { get; set; }
        [DataMember]
        public String ReferenciaEmpresaTitular { get; set; }
        [DataMember]
        public String CodigoCiudadEmpresaTitular { get; set; }
        [DataMember]
        public String TelefonoEmpresaTitular { get; set; }
        [DataMember]
        public String FechaFinContratoTitular { get; set; }
        [DataMember]
        public String MontoIngresoTitular { get; set; }
        [DataMember]
        public String MontoOtroIngresoTitular { get; set; }

        //Garante
        [DataMember]
        public String RazonSocialGarante { get; set; }
        [DataMember]
        public String RucGarante { get; set; }
        [DataMember]
        public String DireccionGarante { get; set; }
        [DataMember]
        public String NroDireccionGarante { get; set; }
        [DataMember]
        public String RepresentanteGarante { get; set; }
        [DataMember]
        public String DocRepresentanteGarante { get; set; }
        [DataMember]
        public String PoderesGarante { get; set; }
        [DataMember]
        public String TipoCuenta { get; set; }
        [DataMember]
        public String TipoCuenta2 { get; set; }
        [DataMember]
        public String NroCuentaD2 { get; set; }

        [DataMember]
        public String TipoCuenta3 { get; set; }
        [DataMember]
        public String NroCuentaD3 { get; set; }

        [DataMember]
        public String TipoCuenta4 { get; set; }
        [DataMember]
        public String NroCuentaD4 { get; set; }



        //Pagare
        [DataMember]
        public String NroPagare { get; set; }
        [DataMember]
        public String FechaVencimiento { get; set; }
        [DataMember]
        public String ImportePagare { get; set; }
        [DataMember]
        public String InteresEfectivo { get; set; }
        [DataMember]
        public String InteresMoratorio { get; set; }
        [DataMember]
        public String NombreEmitente { get; set; }
        [DataMember]
        public String NroDocumentoEmitente { get; set; }
        [DataMember]
        public String DireccionEmitente { get; set; }
        [DataMember]
        public String NroDireccionEmitente { get; set; }
        [DataMember]
        public String NombreAvalista { get; set; }

        [DataMember]
        public virtual String Distrito { get; set; }
        [DataMember]
        public virtual String Provincia { get; set; }
        [DataMember]
        public virtual String Departamento { get; set; }
        [DataMember]
        public SexoD Sexo { get; set; }
        [DataMember]
        public String PrimerNombre { get; set; }
        [DataMember]
        public String SegundoNombre { get; set; }
        [DataMember]
        public String Apellidos { get; set; }
        [DataMember]
        public String Ocupacion { get; set; }
        [DataMember]
        public String FechaNacimiento { get; set; }
        [DataMember]
        public String Correo { get; set; }
        [DataMember]
        public TipoSeguro PlanSeguro { get; set; }
        [DataMember]
        public String NombresFuncionario{ get; set; }
        [DataMember]
        public String EmailFuncionario { get; set; }

        //Firmante adicional

        [DataMember]
        public String ApPaternoFirmanteAdicional { get; set; }
        [DataMember]
        public String ApMaternoFirmanteAdicional { get; set; }
        [DataMember]
        public String NombresFirmanteAdicional { get; set; }
        [DataMember]
        public TipoDoc TipoDocumentoFirmanteAdicional { get; set; }
        [DataMember]
        public String NroDocumentoFirmanteAdicional { get; set; }



        [DataMember]
        public DireccionDetalleD? DireccionDetalleEmpresaT { get; set; }
        [DataMember]
        public DireccionDetalleExteriorD? DireccionDetalleExteriorEmpresaT { get; set; }
        [DataMember]
        public DireccionDetalleInteriorD? DireccionDetalleInteriorEmpresaT { get; set; }
        [DataMember]
        public DireccionDetalleZonaD? DireccionDetalleZonaEmpresaT { get; set; }

        [DataMember]
        public TipoContratoT? TipoContratoTitular { get; set; }
        [DataMember]
        public Moneda? TipoMonedaIngresoTitular { get; set; }
        [DataMember]
        public Moneda? MonedaOtroIngresoTitular { get; set; }
        [DataMember]
        public String DireccionEmpresaC { get; set; }
        [DataMember]
        public String DireccionExteriorEmpresaC { get; set; }
        [DataMember]
        public String DireccionInteriorEmpresaC { get; set; }
        [DataMember]
        public String DireccionZonaEmpresaC { get; set; }

        //Datos domiciliarios Empresa Conyuge
        [DataMember]
        public DireccionDetalleD? DireccionDetalleEmpresaC { get; set; }
        [DataMember]
        public DireccionDetalleExteriorD? DireccionDetalleExteriorEmpresaC { get; set; }
        [DataMember]
        public DireccionDetalleInteriorD? DireccionDetalleInteriorEmpresaC { get; set; }
        [DataMember]
        public DireccionDetalleZonaD? DireccionDetalleZonaEmpresaC { get; set; }
        [DataMember]
        public TipoContratoT? TipoContratoConyuge { get; set; }
        [DataMember]
        public Moneda? TipoMonedaIngresoConyuge { get; set; }
        [DataMember]
        public Moneda? MonedaOtroIngresoConyuge { get; set; }

        //Informacion patrimonial
        [DataMember]
        public String DireccionPatrimonio1 { get; set; }
        [DataMember]
        public String DireccionPatrimonio2 { get; set; }
        [DataMember]
        public String TotalPatrimonio1 { get; set; }
        [DataMember]
        public String TotalPatrimonio2 { get; set; }
        [DataMember]
        public TipoPatrimonioP? TipoPatrimonio { get; set; }
        [DataMember]
        public Moneda? TipoMonedaPatrimonio1 { get; set; }
        [DataMember]
        public Moneda? TipoMonedaPatrimonio2 { get; set; }
        [DataMember]
        public bool Hipoteca1 { get; set; }
        [DataMember]
        public bool Hipoteca2 { get; set; }

        //Prestamo personal
        [DataMember]
        public SubProducto? SeleccioneSubProducto { get; set; }
        [DataMember]
        public TipoPrestamo? TipoPrestamoPersonal { get; set; }
        [DataMember]
        public string FechaPagoPrestamoPersonal { get; set; }
        [DataMember]
        public UsoPrestamo? UsoPrestamoPersonal { get; set; }
        [DataMember]
        public string UsoPrestamoPersonalOtros { get; set; }

        //Prestamo estudios
        [DataMember]
        public Estudios? TipoPrestamoEstudios { get; set; }
        [DataMember]
        public TipoEstudio? TipoEstudioPrestamo { get; set; }
        [DataMember]
        public string TipoEstudioPrestamoOtros { get; set; }
        [DataMember]
        public string InstitutoPrestamo { get; set; }
        [DataMember]
        public string CarreraPrestamo { get; set; }
        [DataMember]
        public string ProgramaPrestamo { get; set; }

        //Credito por convenio
        [DataMember]
        public String FechaCredito { get; set; }
        [DataMember]
        public String LineaConvenio { get; set; }
        [DataMember]
        public String UsoCredito { get; set; }
        [DataMember]
        public String TransferenciaCCI { get; set; }

        //Préstamo por convenio
        //Cuotas al año
        [DataMember]
        public CuotasAnioD? CuotasAnio { get; set; }
        [DataMember]
        public ModalidadClienteT? ModalidadCliente { get; set; }
        [DataMember]
        public ModalidadCreditoT? ModalidadCredito { get; set; }
        [DataMember]
        public bool AfiliacionSeguro { get; set; }
        [DataMember]
        public bool EnvioEstadoCuenta { get; set; }
        [DataMember]
        public FormaEnvio? FormaEstadoCuenta { get; set; }
        [DataMember]
        public Correspondencia? CorrespondenciaEstadoCuenta { get; set; }
        [DataMember]
        public FormaEnvio? FormaHojaResumen { get; set; }
        [DataMember]
        public Correspondencia? CorrespondenciaHojaResumen { get; set; }
        [DataMember]
        public SeguroDes? SeguroDesgravamen { get; set; }

        [DataMember]
        public bool PrimerConsentimiento { get; set; }
        [DataMember]
        public bool SegundoConsentimiento { get; set; }

        //Clausula Adicional
        [DataMember]
        public String MontoDesembolso { get; set; }
        [DataMember]
        public String TasaDesembolso { get; set; }
        [DataMember]
        public String CuentaSueldo { get; set; }
        [DataMember]
        public String ApellidosNombresCliente { get; set; }

        [DataMember]
        public virtual SexoD GeneroCliente { get; set; }
        [DataMember]
        public virtual String EstaturaCliente { get; set; }
        [DataMember]
        public virtual String PesoCliente { get; set; }

        [DataMember]
        public virtual String MontoSolicitado { get; set; }

        [DataMember]
        public virtual String DepartamentoInterior { get; set; }

        [DataMember]
        public virtual String DireccionPiso { get; set; }

        [DataMember]
        public virtual String Urbanizacion { get; set; }

        [DataMember]
        public virtual String ConsumoCigarrillos { get; set; }

        [DataMember]
        public virtual String EstadoSalud { get; set; }



        //Salud

        [DataMember]
        public bool Cancer { get; set; }

        [DataMember]
        public bool CancerMama { get; set; }

        [DataMember]
        public String FechaDiagnosticoCancerMama { get; set; }
        [DataMember]
        public String EstadoCancerMama { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCancerMama { get; set; }
        [DataMember]
        public String MedicoEntranteCancerMama { get; set; }

        [DataMember]
        public bool CancerColon { get; set; }
        [DataMember]
        public String FechaDiagnosticoCancerColon { get; set; }
        [DataMember]
        public String EstadoCancerColon { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCancerColon { get; set; }
        [DataMember]
        public String MedicoEntranteCancerColon { get; set; }


        [DataMember]
        public bool CancerPulmon { get; set; }

        [DataMember]
        public String FechaDiagnosticoCancerPulmon { get; set; }
        [DataMember]
        public String EstadoCancerPulmon { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCancerPulmon { get; set; }
        [DataMember]
        public String MedicoEntranteCancerPulmon { get; set; }

        [DataMember]
        public String CancerOtro { get; set; }

        [DataMember]
        public String FechaDiagnosticoCancerOtro { get; set; }
        [DataMember]
        public String EstadoCancerOtro { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCancerOtro { get; set; }
        [DataMember]
        public String MedicoEntranteCancerOtro { get; set; }


        [DataMember]
        public bool CardioVascular { get; set; }
        [DataMember]
        public bool CardiopatiaCoronaria { get; set; }


        [DataMember]
        public String FechaDiagnosticoCardiopatiaCoronaria { get; set; }
        [DataMember]
        public String EstadoCardiopatiaCoronaria { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCardiopatiaCoronaria { get; set; }
        [DataMember]
        public String MedicoEntranteCardiopatiaCoronaria { get; set; }

        [DataMember]
        public bool InsuficienciaCardiaca { get; set; }


        [DataMember]
        public String FechaDiagnosticoInsuficienciaCardiaca { get; set; }
        [DataMember]
        public String EstadoInsuficienciaCardiaca { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteInsuficienciaCardiaca { get; set; }
        [DataMember]
        public String MedicoEntranteInsuficienciaCardiaca { get; set; }

        [DataMember]
        public String CardioOtro { get; set; }

        [DataMember]
        public String FechaDiagnosticoCardioOtro { get; set; }
        [DataMember]
        public String EstadoCardioOtro { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteCardioOtro { get; set; }
        [DataMember]
        public String MedicoEntranteCardioOtro { get; set; }

        [DataMember]
        public bool Renal { get; set; }
        [DataMember]
        public String FechaDiagnosticoRenal { get; set; }
        [DataMember]
        public String EstadoRenal { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteRenal { get; set; }
        [DataMember]
        public String MedicoEntranteRenal { get; set; }

        [DataMember]
        public bool Diabetes { get; set; }
        [DataMember]
        public String FechaDiagnosticoDiabetes { get; set; }
        [DataMember]
        public String EstadoDiabetes { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteDiabetes { get; set; }
        [DataMember]
        public String MedicoEntranteDiabetes { get; set; }

        [DataMember]
        public bool Neurologicas { get; set; }
        [DataMember]
        public String FechaDiagnosticoNeurologicas { get; set; }
        [DataMember]
        public String EstadoNeurologicas { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteNeurologicas { get; set; }
        [DataMember]
        public String MedicoEntranteNeurologicas { get; set; }

        [DataMember]
        public bool Psiquiatricas { get; set; }
        [DataMember]
        public String FechaDiagnosticoPsiquiatricas { get; set; }
        [DataMember]
        public String EstadoPsiquiatricas { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntrantePsiquiatricas { get; set; }
        [DataMember]
        public String MedicoEntrantePsiquiatricas { get; set; }


        [DataMember]
        public bool EnfermedadesRespiratorias { get; set; }
        [DataMember]
        public String FechaDiagnosticoEnfermedadesRespiratorias { get; set; }
        [DataMember]
        public String EstadoEnfermedadesRespiratorias { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteEnfermedadesRespiratorias { get; set; }
        [DataMember]
        public String MedicoEntranteEnfermedadesRespiratorias { get; set; }

        [DataMember]
        public bool SIDA { get; set; }
        [DataMember]
        public String FechaDiagnosticoSIDA { get; set; }
        [DataMember]
        public String EstadoSIDA { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteSIDA { get; set; }
        [DataMember]
        public String MedicoEntranteSIDA { get; set; }


        [DataMember]
        public bool OtrasEnfermedades { get; set; }
        [DataMember]
        public String FechaDiagnosticoOtrasEnfermedades { get; set; }
        [DataMember]
        public String EstadoOtrasEnfermedades { get; set; }
        [DataMember]
        public String IsntitucionMedicaEntranteOtrasEnfermedades { get; set; }
        [DataMember]
        public String MedicoEntranteOtrasEnfermedades { get; set; }


        public String DataReceived { get { try { return Newtonsoft.Json.JsonConvert.SerializeObject(this); } catch (Exception ex) { return ""; } } }

        public enum SeguroDes : int
        {
            ConSeguro = 1,
            ConPoliza = 2,
            SinSeguro = 3
        }
        public enum SexoD : int
        {
            Femenino = 1,
            Masculino = 2
        }

        public enum CuotasAnioD : int
        {
            Diez = 1,
            Doce = 2
        }
        public enum TipoDoc : int
        {
            DNI = 1,
            CE = 2,
            RUC = 3,
            CI = 4, 
            Otros = 5
        }
        public enum Moneda : int
        {
            Soles = 1,
            Dolares = 2,
            Euros = 3
        }
        public enum Credito : int
        {
            PrestamoNegocio = 1,
            Convenios = 2,
            VehicularGNV = 3,
            PrestamoPersonal = 4,
            Hipotecario = 5,
            HipotecarioEspecial = 6,
            MiVivienda = 7,
            TechoPropio = 8,
            Vehicular = 9,
            HipotecarioRetorno = 10,
            Microfinanzas = 11,
            Pymes = 12,
            Preferente = 13,
            NuevoMiVivienda = 14,
            PrestamoPersonalColaborador = 15,
            PrestamoPersonalEstudios = 16

        }

        public enum Canal : int
        {
            Agencia = 1,
            Externo = 2,
            FuerzaVenta = 3,
            Otro = 4
        }
        public enum Cuota : int
        {
            Simple = 1,
            DobleJul = 2,
            DobleEne = 3,
            Dic = 4,
            Ago = 5
        }
        public enum Desembolso : int
        {
            Desembolso = 1,
            Cheque = 2,
            OrdenPago = 3,
            Otro = 4,
            CuentaCCI = 5
        }
        public enum Garantia : int
        {
            Liquida = 1,
            Hipotecaria = 2,
            Aval = 3
        }

        public enum EstadoC : int
        {
            Soltero = 1,
            Conviviente = 2,
            Casado = 3,
            CasadoSepBienes = 4,
            Viudo = 5,
            Divorciado = 6
        }

        public enum ViviendaT : int
        {
            Propia = 1,
            PropiaFinanciada = 2,
            Alquilada = 3,
            Familiar = 4
        }
        public enum GradoI : int
        {
            Primaria = 1,
            Secundaria = 2,
            Tecnico = 3,
            Universitario = 4,
            Ninguno = 5
        }
        public enum SituacionL : int
        {
            Dependiente = 1,
            Profesional = 2,
            Accionista = 3,
            Rentista = 4,
            PersonaNatural = 5,
            Jubilado = 6
        }

        public enum DireccionDetalleD : int
        {
            Calle = 1,
            Avenida = 2,
            Jiron = 3,
            Pasaje = 4,
            Otro = 5
        }
        public enum DireccionDetalleExteriorD : int
        {
            Numero = 1,
            Bloque = 2,
            Manzana = 3,
            Otro = 4
        }
        public enum DireccionDetalleInteriorD : int
        {
            Lote = 1,
            Departamento = 2,
            Int = 3,
            Otro = 4
        }
        public enum DireccionDetalleZonaD : int
        {
            Seccion = 1,
            Etapa = 2,
            Urbanizacion = 3,
            AAHH = 4,
            Otro = 5
        }
        public enum TipoContratoT : int
        {
            Nombrado = 1,
            Cesante = 2,
            PlazoFijo = 3,
            Ninguno = 4,
            CAS = 5
        }
        public enum TipoPatrimonioP : int
        {
            Inmuebles = 1,
            Vehiculos = 2,
            Depositos = 3,
            Varios = 4
        }
        public enum ModalidadClienteT : int
        {
            Nuevo = 1,
            Reenganche = 2
        }
        public enum ModalidadCreditoT : int
        {
            Compra = 1,
            SinCompra = 2
        }
        public enum FormaEnvio : int
        {
            Fisica = 1,
            Electronica = 2
        }
        public enum Correspondencia : int
        {
            Domicilio = 1,
            Trabajo = 2
        }
        public enum TipoTarjeta : int
        {
            Tarjeta = 1,
            Prestamo = 2
        }
        public enum Valor : int
        {
            PortaValor = 1,
            Directo = 2,
            CCE = 3
        }
        public enum SubProducto : int
        {
            Tradicional = 1,
            Estudios = 2
        }
        public enum TipoPrestamo : int
        {
            Nuevo = 1,
            Reenganche = 2
        }
        public enum UsoPrestamo : int
        {
            Ocio = 1,
            CompraDeuda = 2,
            Otro = 3
        }
        public enum Estudios : int
        {
            Nacional = 1,
            Extranjera =2
        }
        public enum TipoEstudio : int
        {
            Especializacion = 1,
            Maestria = 2,
            MBA = 3,
            Doctorado = 4,
            Otros = 5
        }

        public enum TipoSeguro : int
        {
            Plan1 = 1,
            Plan2 = 2,
            Plan3 = 3,
            Plan4 = 4
        }
    }
}
