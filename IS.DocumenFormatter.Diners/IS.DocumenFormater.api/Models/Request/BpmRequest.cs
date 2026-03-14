using Org.BouncyCastle.Utilities;
using System;
using System.Runtime.Serialization;

namespace IS.DocumenFormater.api.Models.Request
{
    public class BpmRequest
    {
        [DataMember]
        public String ProcesoElectoral { get; set; }
        [DataMember]
        public String NombreCandidato { get; set; }
        [DataMember]
        public String ApPaternoCandidato { get; set; }
        [DataMember]
        public String ApMaternoCandidato { get; set; }
        [DataMember]
        public String TipoDocumento { get; set; }
        [DataMember]
        public String NroDocumentoCliente { get; set; }
        [DataMember]
        public String Sexo { get; set; }
        [DataMember]
        public String FechaNacimiento { get; set; }
        [DataMember]
        public String DistritoNacimiento { get; set; }
        [DataMember]
        public String ProvinciaNacimiento { get; set; }
        [DataMember]
        public String DepartamentoNacimiento { get; set; }
        [DataMember]
        public String PaisNacimiento { get; set; }
        [DataMember]
        public String DistritoDomicilio { get; set; }
        [DataMember]
        public String ProvinciaDomicilio { get; set; }
        [DataMember]
        public String DepartamentoDomicilio { get; set; }
        [DataMember]
        public String DireccionDomicilio { get; set; }
        [DataMember]
        public String Organizacion { get; set; }
        [DataMember]
        public int Cargo { get; set; }
        [DataMember]
        public String RegionCircunscripcion { get; set; }
        [DataMember]
        public String ProvinciaCircunscripcion { get; set; }
        [DataMember]
        public String DistritoCircunscripcion { get; set; }
        [DataMember]
        public virtual String ImpresionBiometricaCliente { get; set; }
        [DataMember]
        public String NacionalCircunscripcion { get; set; }
        [DataMember]
        public String InfoCompleCircunscripcion { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararOficio { get; set; }
        [DataMember]
        public String CentroDePrestacion { get; set; }
        [DataMember]
        public String OficioOcupacionProfesion { get; set; }
        [DataMember]
        public String RUCEmpresaOficio { get; set; }
        [DataMember]
        public String DireccionOficio { get; set; }
        [DataMember]
        public String DesdeAñoOficio { get; set; }
        [DataMember]
        public String HastaAñoOficio { get; set; }
        [DataMember]
        public String DistritoOficio { get; set; }
        [DataMember]
        public String ProvinciaOficio { get; set; }
        [DataMember]
        public String DepartamentoOficio { get; set; }
        [DataMember]
        public String PaisOficio { get; set; }
        [DataMember]
        public String InfoCompleOficio { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararEducacionBasica { get; set; }
        [DataMember]
        public Boolean EstudiosPrimarios { get; set; }
        [DataMember]
        public Boolean EstudiosPrimariosConcluidos { get; set; }
        [DataMember]
        public Boolean EstudiosSecundarios { get; set; }
        [DataMember]
        public Boolean EstudiosSecundariosConcluidos { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararNoUniversitarios { get; set; }
        [DataMember]
        public Boolean EstudiosTecnicos { get; set; }
        [DataMember]
        public String NombreCentroEstudiosTecnicos { get; set; }
        [DataMember]
        public String CarreraTituloEstudiosTecnicos { get; set; }
        [DataMember]
        public Boolean EstudiosTecnicosConcluidos { get; set; }
        [DataMember]
        public Boolean EstudiosNoUniversitarios { get; set; }
        [DataMember]
        public String NombreCentroNoUniversitarios { get; set; }
        [DataMember]
        public String CarreraTituloNoUniversitarios { get; set; }
        [DataMember]
        public Boolean NoUniversitariosConcluidos { get; set; }
        [DataMember]
        public String InfoCompleNoUniversitarios { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararUniversitarios { get; set; }
        [DataMember]
        public Boolean EstudiosUniversitarios { get; set; }
        [DataMember]
        public String NombreUniversidad { get; set; }
        [DataMember]
        public Boolean UniversidadConcluido { get; set; }
        [DataMember]
        public String NombreGradoTitulo { get; set; }
        [DataMember]
        public Boolean UniversidadEgresado { get; set; }
        [DataMember]
        public String UniversidadAñoObtencion { get; set; }
        [DataMember]
        public String InfoCompleUniversitarios { get; set; }
        [DataMember]
        public Boolean EstudiosPostgrado { get; set; }
        [DataMember]
        public String NombreCentroEstudiosPostgrado { get; set; }
        [DataMember]
        public String EspecializacionPostgrado { get; set; }
        [DataMember]
        public Boolean EspecializacionPostgradoConcluidos { get; set; }
        [DataMember]
        public Boolean EgresadoPostgrado { get; set; }
        [DataMember]
        public Boolean GradoObtenidoMaestro { get; set; }
        [DataMember]
        public Boolean GradoObtenidoDoctor { get; set; }
        [DataMember]
        public String PostgradoAñoObtencion { get; set; }
        [DataMember]
        public String InfoComplePostgrado { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararOtrosEstudiosPostgrado { get; set; }
        [DataMember]
        public Boolean OtrosEstudiosPostgrado { get; set; }
        [DataMember]
        public String NombreCentroOtrosEstudiosPostgrado { get; set; }
        [DataMember]
        public String EspecializacionOtrosPostgrado { get; set; }
        [DataMember]
        public Boolean EspecializacionOtrosPostgradoConcluidos { get; set; }
        [DataMember]
        public String GradoOtrosPostgrado { get; set; }
        [DataMember]
        public Boolean EgresadoOtrosPostgrado { get; set; }
        [DataMember]
        public String OtrosPostgradoAñoObtencion { get; set; }
        [DataMember]
        public String InfoCompleOtrosPostgrado { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararCargosPartidarios { get; set; }
        [DataMember]
        public String TrayectoriaOrganizacion1 { get; set; }
        [DataMember]
        public String TrayectoriaCargo1 { get; set; }
        [DataMember]
        public String DesdeAñoCargo1 { get; set; }
        [DataMember]
        public String HastaAñoCargo1 { get; set; }
        [DataMember]
        public String InfoCompleCargo1 { get; set; }
        [DataMember]
        public String TrayectoriaOrganizacion2 { get; set; }
        [DataMember]
        public String TrayectoriaCargo2 { get; set; }
        [DataMember]
        public String DesdeAñoCargo2 { get; set; }
        [DataMember]
        public String HastaAñoCargo2 { get; set; }
        [DataMember]
        public String InfoCompleCargo2 { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararCargosEleccionPopular { get; set; }
        [DataMember]
        public int CargoEleccionPopular1 { get; set; }
        [DataMember]
        public String TrayectoriaOrganizacionPopular1 { get; set; }
        [DataMember]
        public String DesdeAñoCargoPopular1 { get; set; }
        [DataMember]
        public String HastaAñoCargoPopular1 { get; set; }
        [DataMember]
        public String InfoCompleCargoPopular1 { get; set; }
        [DataMember]
        public int CargoEleccionPopular2 { get; set; }
        [DataMember]
        public String TrayectoriaOrganizacionPopular2 { get; set; }
        [DataMember]
        public String DesdeAñoCargoPopular2 { get; set; }
        [DataMember]
        public String HastaAñoCargoPopular2 { get; set; }
        [DataMember]
        public String InfoCompleCargoPopular2 { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararSentencias { get; set; }
        [DataMember]
        public String SentenciasNroExpediente1 { get; set; }
        [DataMember]
        public String SentenciasFechaFirme1 { get; set; }
        [DataMember]
        public String SentenciasOrgano1 { get; set; }
        [DataMember]
        public String SentenciasDelito1 { get; set; }
        [DataMember]
        public String SentenciasFallaPena1 { get; set; }
        [DataMember]
        public int SentenciasModalidad1 { get; set; }
        [DataMember]
        public String SentenciasModalidadOtro1 { get; set; }
        [DataMember]
        public int SentenciasCumplimiento1 { get; set; }
        [DataMember]
        public String SentenciasNroExpediente2 { get; set; }
        [DataMember]
        public String SentenciasFechaFirme2 { get; set; }
        [DataMember]
        public String SentenciasOrgano2 { get; set; }
        [DataMember]
        public String SentenciasDelito2 { get; set; }
        [DataMember]
        public String SentenciasFallaPena2 { get; set; }
        [DataMember]
        public int SentenciasModalidad2 { get; set; }
        [DataMember]
        public String SentenciasModalidadOtro2 { get; set; }
        [DataMember]
        public int SentenciasCumplimiento2 { get; set; }
        [DataMember]
        public String InfoCompleSentencias { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararRelacionDeSentencias { get; set; }
        [DataMember]
        public int RelacionDeSentenciasMaterialDeDemanda1 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasNroExpediente1 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasOrganoJudicial1 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasFallo1 { get; set; }
        [DataMember]
        public int RelacionDeSentenciasMaterialDeDemanda2 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasNroExpediente2 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasOrganoJudicial2 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasFallo2 { get; set; }
        [DataMember]
        public int RelacionDeSentenciasMaterialDeDemanda3 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasNroExpediente3 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasOrganoJudicial3 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasFallo3 { get; set; }
        [DataMember]
        public int RelacionDeSentenciasMaterialDeDemanda4 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasNroExpediente4 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasOrganoJudicial4 { get; set; }
        [DataMember]
        public String RelacionDeSentenciasFallo4 { get; set; }
        [DataMember]
        public String InfoCompleRelacionDeSentencias { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararRenuncias { get; set; }
        [DataMember]
        public String RenunciasOrgano1 { get; set; }
        [DataMember]
        public String RenunciasAñoOrgano1 { get; set; }
        [DataMember]
        public String RenunciasComentario1 { get; set; }
        [DataMember]
        public String RenunciasOrgano2 { get; set; }
        [DataMember]
        public String RenunciasAñoOrgano2 { get; set; }
        [DataMember]
        public String RenunciasComentario2 { get; set; }
        [DataMember]
        public String InfoCompleRenuncias { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararIngresos { get; set; }
        [DataMember]
        public String IngresosAñoDeclarado { get; set; }
        [DataMember]
        public String IngresosRemuneracionBrutaAnualSectorPublico { get; set; }
        [DataMember]
        public String IngresosRemuneracionBrutaAnualSectorPrivado { get; set; }
        [DataMember]
        public String IngresosRemuneracionBrutaAnualTotal { get; set; }
        [DataMember]
        public String IngresosRentaBrutaAnualSectorPublico { get; set; }
        [DataMember]
        public String IngresosRentaBrutaAnualSectorPrivado { get; set; }
        [DataMember]
        public String IngresosRentaBrutaAnualTotal { get; set; }
        [DataMember]
        public String IngresosOtrosAnualesSectorPublico { get; set; }
        [DataMember]
        public String IngresosOtrosAnualesSectorPrivado { get; set; }
        [DataMember]
        public String IngresosOtrosAnualesTotal { get; set; }
        [DataMember]
        public String IngresosTotal { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararBienesInmuebles { get; set; }
        [DataMember]
        public String BienInmuebleTipo1 { get; set; }
        [DataMember]
        public String BienInmuebleDireccion1 { get; set; }
        [DataMember]
        public Boolean BienInmuebleSunarp1 { get; set; }
        [DataMember]
        public String BienInmuebleSunarpPartida1 { get; set; }
        [DataMember]
        public String BienInmuebleValor1 { get; set; }
        [DataMember]
        public String BienInmuebleValorAutoavaluo1 { get; set; }
        [DataMember]
        public String BienInmuebleInfoComple1 { get; set; }
        [DataMember]
        public String BienInmuebleTipo2 { get; set; }
        [DataMember]
        public String BienInmuebleDireccion2 { get; set; }
        [DataMember]
        public Boolean BienInmuebleSunarp2 { get; set; }
        [DataMember]
        public String BienInmuebleSunarpPartida2 { get; set; }
        [DataMember]
        public String BienInmuebleValor2 { get; set; }
        [DataMember]
        public String BienInmuebleValorAutoavaluo2 { get; set; }
        [DataMember]
        public String BienInmuebleInfoComple2 { get; set; }
        [DataMember]
        public String BienInmuebleTipo3 { get; set; }
        [DataMember]
        public String BienInmuebleDireccion3 { get; set; }
        [DataMember]
        public Boolean BienInmuebleSunarp3 { get; set; }
        [DataMember]
        public String BienInmuebleSunarpPartida3 { get; set; }
        [DataMember]
        public String BienInmuebleValor3 { get; set; }
        [DataMember]
        public String BienInmuebleValorAutoavaluo3 { get; set; }
        [DataMember]
        public String BienInmuebleInfoComple3 { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararBienesMuebles { get; set; }
        [DataMember]
        public String BienMuebleVehiculo1 { get; set; }
        [DataMember]
        public String BienMueblePlaca1 { get; set; }
        [DataMember]
        public String BienMuebleValor1 { get; set; }
        [DataMember]
        public String BienMuebleInfoComple1 { get; set; }
        [DataMember]
        public String BienMuebleVehiculo2 { get; set; }
        [DataMember]
        public String BienMueblePlaca2 { get; set; }
        [DataMember]
        public String BienMuebleValor2 { get; set; }
        [DataMember]
        public String BienMuebleInfoComple2 { get; set; }
        [DataMember]
        public String BienMuebleVehiculo3 { get; set; }
        [DataMember]
        public String BienMueblePlaca3 { get; set; }
        [DataMember]
        public String BienMuebleValor3 { get; set; }
        [DataMember]
        public String BienMuebleInfoComple3 { get; set; }
        [DataMember]
        public String BienMuebleVehiculo4 { get; set; }
        [DataMember]
        public String BienMueblePlaca4 { get; set; }
        [DataMember]
        public String BienMuebleValor4 { get; set; }
        [DataMember]
        public String BienMuebleInfoComple4 { get; set; }
        [DataMember]
        public String BienMuebleVehiculo5 { get; set; }
        [DataMember]
        public String BienMueblePlaca5 { get; set; }
        [DataMember]
        public String BienMuebleValor5 { get; set; }
        [DataMember]
        public String BienMuebleInfoComple5 { get; set; }
        [DataMember]
        public String BienMuebleTotal { get; set; }
        [DataMember]
        public String TitularidadPersonaJuridica { get; set; }
        [DataMember]
        public String TitularidadAcciones { get; set; }
        [DataMember]
        public String TitularidadParticipaciones { get; set; }
        [DataMember]
        public String TitularidadNroAccionesParticipaciones { get; set; }
        [DataMember]
        public String TitularidadValorNominalTotalAcciones { get; set; }
        [DataMember]
        public Boolean InfoPorDeclararInfoAdicional { get; set; }
        [DataMember]
        public String InfoAdicional1 { get; set; }
        [DataMember]
        public String InfoAdicional2 { get; set; }
        [DataMember]
        public String InfoAdicional3 { get; set; }
        [DataMember]
        public String FechaFormularioCompletado { get; set; }

        //ANEXO 1

        [DataMember]
        public int ConsejoMunicipal { get; set; }
        [DataMember]
        public String Lista { get; set; }
        [DataMember]
        public String Lugar { get; set; }
        [DataMember]
        public String FechaDia { get; set; }

        //ANEXO 2

        [DataMember]
        public int TipoComunidad { get; set; }
        [DataMember]
        public String NombreComunidad { get; set; }
        [DataMember]
        public String ProvinciaComunidad { get; set; }
        [DataMember]
        public String DepartamentoComunidad { get; set; }
        [DataMember]
        public String Fecha { get; set; }
        [DataMember]
        public String AutoridadCargo { get; set; }
        [DataMember]
        public String AutoridadNombres { get; set; }
        [DataMember]
        public String AutoridadApellidos { get; set; }
        [DataMember]
        public String AutoridadNroDNI { get; set; }
        [DataMember]
        public String JuezCargo { get; set; }
        [DataMember]
        public String JuezNombres { get; set; }
        [DataMember]
        public String JuezApellidos { get; set; }
        [DataMember]
        public String JuezNroDNI { get; set; }


        [DataMember]
        public String RazonSocialCliente { get; set; }
        [DataMember]
        public String RucCliente { get; set; }
        [DataMember]
        public String NombresCliente { get; set; }
        [DataMember]
        public String ApPaternoCliente { get; set; }
        [DataMember]
        public String ApMaternoCliente { get; set; }
        [DataMember]
        public virtual String TipoDocumentoCliente { get; set; }
        [DataMember]
        public String DireccionCliente { get; set; }
        [DataMember]
        public String NroDireccionCliente { get; set; }
        [DataMember]
        public virtual String DireccionInteriorCliente { get; set; }
        [DataMember]
        public virtual String DistritoCliente { get; set; }
        [DataMember]
        public virtual String ProvinciaCliente { get; set; }
        [DataMember]
        public virtual String DepartamentoCliente { get; set; }
        [DataMember]
        public virtual String SexoCliente { get; set; }
        [DataMember]
        public String FechaNacimientoCliente { get; set; }
        [DataMember]
        public String OcupacionCliente { get; set; }
        [DataMember]
        public String EmailCliente { get; set; }
        [DataMember]
        public String TelefonoCliente { get; set; }
        [DataMember]
        public String CelularCliente { get; set; }
        [DataMember]
        public String PrimerNombreCliente { get; set; }
        [DataMember]
        public String SegundoNombreCliente { get; set; }
        [DataMember]
        public String MontoCredito { get; set; }
        [DataMember]
        public String LugarTransaccion { get; set; }
        [DataMember]
        public String FechaTransaccion { get; set; }
        [DataMember]
        public String MontoTotal { get; set; }
        [DataMember]
        public String TipoMonedaD { get; set; }

        //Firmante adicional

        [DataMember]
        public String ApPaternoFirmanteAdicional { get; set; }
        [DataMember]
        public String ApMaternoFirmanteAdicional { get; set; }
        [DataMember]
        public String NombresFirmanteAdicional { get; set; }
        [DataMember]
        public String TipoDocumentoFirmanteAdicional { get; set; }
        [DataMember]
        public String NroDocumentoFirmanteAdicional { get; set; }

        //Beneficiario 1
        [DataMember]
        public String NombresBeneficiario1 { get; set; }
        [DataMember]
        public String ApPaternoBeneficiario1 { get; set; }
        [DataMember]
        public String ApMaternoBeneficiario1 { get; set; }
        //[DataMember]
        //public virtual String TipoDocumentoBeneficiario1 { get; set; }
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
        //[DataMember]
        //public virtual String TipoDocumentoBeneficiario2 { get; set; }
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
        //[DataMember]
        //public virtual String TipoDocumentoBeneficiario3 { get; set; }
        [DataMember]
        public String NroDocumentoBeneficiario3 { get; set; }
        [DataMember]
        public String RelacionBeneficiario3 { get; set; }
        [DataMember]
        public String PorcentajeBeneficiario3 { get; set; }
        [DataMember]
        public virtual String EstadoConsentimiento { get; set; }
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
        public virtual String TipoDocumentoCliente2 { get; set; }

        [DataMember]
        public String NombresCliente3 { get; set; }
        [DataMember]
        public String ApPaternoCliente3 { get; set; }
        [DataMember]
        public String ApMaternoCliente3 { get; set; }
        [DataMember]
        public String NroDocumentoCliente3 { get; set; }
        [DataMember]
        public virtual String ImpresionBiometricaCliente3 { get; set; }
        //[DataMember]
        //public virtual String TipoDocumentoCliente3 { get; set; }

        //Datos institucion
        [DataMember]
        public String NombreInstitucion1 { get; set; }
        [DataMember]
        public String TipoTarjeta1 { get; set; }
        [DataMember]
        public String NumeroTarjeta1 { get; set; }
        [DataMember]
        public String TipoValor1 { get; set; }
        [DataMember]
        public String TipoMoneda1 { get; set; }
        [DataMember]
        public string MontoCancelar1 { get; set; }

        [DataMember]
        public String NombreInstitucion2 { get; set; }
        [DataMember]
        public String TipoTarjeta2 { get; set; }
        [DataMember]
        public String NumeroTarjeta2 { get; set; }
        [DataMember]
        public String TipoValor2 { get; set; }
        [DataMember]
        public String TipoMoneda2 { get; set; }
        [DataMember]
        public String MontoCancelar2 { get; set; }

        [DataMember]
        public String NombreInstitucion3 { get; set; }
        [DataMember]
        public String TipoTarjeta3 { get; set; }
        [DataMember]
        public String NumeroTarjeta3 { get; set; }
        [DataMember]
        public String TipoValor3 { get; set; }
        [DataMember]
        public String TipoMoneda3 { get; set; }
        [DataMember]
        public String MontoCancelar3 { get; set; }

        [DataMember]
        public String NombreInstitucion4 { get; set; }
        [DataMember]
        public String TipoTarjeta4 { get; set; }
        [DataMember]
        public String NumeroTarjeta4 { get; set; }
        [DataMember]
        public String TipoValor4 { get; set; }
        [DataMember]
        public String TipoMoneda4 { get; set; }
        [DataMember]
        public String MontoCancelar4 { get; set; }


        [DataMember]
        public String NombreInstitucion5 { get; set; }
        [DataMember]
        public String TipoTarjeta5 { get; set; }
        [DataMember]
        public String NumeroTarjeta5 { get; set; }
        [DataMember]
        public String TipoValor5 { get; set; }
        [DataMember]
        public String TipoMoneda5 { get; set; }
        [DataMember]
        public String MontoCancelar5 { get; set; }


        [DataMember]
        public String NombreInstitucion6 { get; set; }
        [DataMember]
        public String TipoTarjeta6 { get; set; }
        [DataMember]
        public String NumeroTarjeta6 { get; set; }
        [DataMember]
        public String TipoValor6 { get; set; }
        [DataMember]
        public String TipoMoneda6 { get; set; }
        [DataMember]
        public String MontoCancelar6 { get; set; }


        [DataMember]
        public String NombreInstitucion7 { get; set; }
        [DataMember]
        public String TipoTarjeta7 { get; set; }
        [DataMember]
        public String NumeroTarjeta7 { get; set; }
        [DataMember]
        public String TipoValor7 { get; set; }
        [DataMember]
        public String TipoMoneda7 { get; set; }
        [DataMember]
        public String MontoCancelar7 { get; set; }


        [DataMember]
        public String NombreInstitucion8 { get; set; }
        [DataMember]
        public String TipoTarjeta8 { get; set; }
        [DataMember]
        public String NumeroTarjeta8 { get; set; }
        [DataMember]
        public String TipoValor8 { get; set; }
        [DataMember]
        public String TipoMoneda8 { get; set; }
        [DataMember]
        public String MontoCancelar8 { get; set; }


        [DataMember]
        public String NombreInstitucion9 { get; set; }
        [DataMember]
        public String TipoTarjeta9 { get; set; }
        [DataMember]
        public String NumeroTarjeta9 { get; set; }
        [DataMember]
        public String TipoValor9 { get; set; }
        [DataMember]
        public String TipoMoneda9 { get; set; }
        [DataMember]
        public String MontoCancelar9 { get; set; }

        [DataMember]
        public String MonedaMontoTotal { get; set; }



        [DataMember]
        public virtual String TipoMoneda { get; set; }
        [DataMember]
        public virtual String TipoCredito { get; set; }
        [DataMember]
        public String PlazoCredito { get; set; }
        [DataMember]
        public String SeguroDesgravamen { get; set; }

        //Vendedor
        [DataMember]
        public String NombresVendedor { get; set; }
        [DataMember]
        public String ApPaternoVendedor { get; set; }
        [DataMember]
        public String ApMaternoVendedor { get; set; }
        [DataMember]
        public String EmailVendedor { get; set; }


        //Conyuge
        [DataMember]
        public String NombresConyuge { get; set; }
        [DataMember]
        public String ApPaternoConyuge { get; set; }
        [DataMember]
        public String ApMaternoConyuge { get; set; }
        [DataMember]
        public virtual String TipoDocumentoConyuge { get; set; }
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
        //[DataMember]
        //public String CentroActualConyuge { get; set; }
        //[DataMember]
        //public String CargoActualConyuge { get; set; }
        //[DataMember]
        //public String GiroConyuge { get; set; }
        //[DataMember]
        //public String FechaIngresoConyuge { get; set; }
        [DataMember]
        public String UbigeoEmpresaConyuge { get; set; }
        //[DataMember]
        //public String ReferenciaEmpresaConyuge { get; set; }
        //[DataMember]
        //public String CodigoCiudadEmpresaConyuge { get; set; }
        //[DataMember]
        //public String TelefonoEmpresaConyuge { get; set; }
        //[DataMember]
        //public String FechaFinContratoConyuge { get; set; }
        //[DataMember]
        //public String MontoIngresoConyuge { get; set; }
        //[DataMember]
        //public String MontoOtroIngresoConyuge { get; set; }
        //[DataMember]
        //public String DireccionConyuge { get; set; }
        //[DataMember]
        //public String OcupacionConyuge { get; set; }
        [DataMember]
        public String EstadoCivilConyuge { get; set; }
        [DataMember]
        public String FuncionesConyuge { get; set; }
        [DataMember]
        public String ViviendaConyuge { get; set; }
        [DataMember]
        public String SexoConyuge { get; set; }
        [DataMember]
        public String GradoConyuge { get; set; }
        [DataMember]
        public String ContinuidadConyuge { get; set; }
        [DataMember]
        public String SituacionLaboralConyuge { get; set; }


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
        public virtual String ImpresionBiometricaRepresentante { get; set; }

        [DataMember]
        public String CanalVenta { get; set; }
        [DataMember]
        public String TipoCuota { get; set; }
        [DataMember]
        public String PeriodoGracia { get; set; }
        [DataMember]
        public String PeriodoGraciaDet { get; set; }
        [DataMember]
        public String TasaCredito { get; set; }
        //[DataMember]
        //public String NroCuentaD { get; set; }
        //[DataMember]
        //public String NroCtaTransfer { get; set; }
        [DataMember]
        public String Entidad { get; set; }
        //[DataMember]
        //public String TipoDesembolso { get; set; }
        [DataMember]
        public String TipoGarantia { get; set; }
        [DataMember]
        public String Nacionalidad { get; set; }
        [DataMember]
        public String NroDependientes { get; set; }
        [DataMember]
        public String EstadoCivilCliente { get; set; }
        [DataMember]
        public String FuncionesCliente { get; set; }
        [DataMember]
        public String CodigoCiudad { get; set; }
        [DataMember]
        public String TipoViviendaCliente { get; set; }
        [DataMember]
        public String GradoInstruccion { get; set; }
        [DataMember]
        public String ContinuidadLaboral { get; set; }
        [DataMember]
        public String SituacionLaboralCliente { get; set; }


        [DataMember]
        public String DireccionDetalleCliente { get; set; }
        [DataMember]
        public String DireccionDetalleExteriorCliente { get; set; }
        [DataMember]
        public String DireccionDetalleInteriorCliente { get; set; }
        [DataMember]
        public String DireccionDetalleZonaCliente { get; set; }
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
        [DataMember]
        public String DireccionDetalleEmpresaT { get; set; }
        [DataMember]
        public String DireccionDetalleExteriorEmpresaT { get; set; }
        [DataMember]
        public String DireccionDetalleInteriorEmpresaT { get; set; }
        [DataMember]
        public String DireccionDetalleZonaEmpresaT { get; set; }

        [DataMember]
        public String TipoContratoTitular { get; set; }
        [DataMember]
        public String TipoMonedaIngresoTitular { get; set; }
        [DataMember]
        public String MonedaOtroIngresoTitular { get; set; }
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
        public String DireccionDetalleEmpresaC { get; set; }
        [DataMember]
        public String DireccionDetalleExteriorEmpresaC { get; set; }
        [DataMember]
        public String DireccionDetalleInteriorEmpresaC { get; set; }
        [DataMember]
        public String DireccionDetalleZonaEmpresaC { get; set; }
        //[DataMember]
        //public String TipoContratoConyuge { get; set; }
        //[DataMember]
        //public String TipoMonedaIngresoConyuge { get; set; }
        //[DataMember]
        //public String MonedaOtroIngresoConyuge { get; set; }

        //Informacion patrimonial
        //[DataMember]
        //public String DireccionPatrimonio1 { get; set; }
        //[DataMember]
        //public String DireccionPatrimonio2 { get; set; }
        //[DataMember]
        //public String TotalPatrimonio1 { get; set; }
        //[DataMember]
        //public String TotalPatrimonio2 { get; set; }
        //[DataMember]
        //public String TipoPatrimonio { get; set; }
        //[DataMember]
        //public String TipoMonedaPatrimonio1 { get; set; }
        //[DataMember]
        //public String TipoMonedaPatrimonio2 { get; set; }
        //[DataMember]
        //public String Hipoteca1 { get; set; }
        //[DataMember]
        //public String Hipoteca2 { get; set; }

        //Prestamo personal
        //[DataMember]
        //public String SeleccioneSubProducto { get; set; }
        [DataMember]
        public String TipoPrestamoPersonal { get; set; }
        [DataMember]
        public string FechaPagoPrestamoPersonal { get; set; }
        [DataMember]
        public string UsoPrestamoPersonal { get; set; }
        [DataMember]
        public string UsoPrestamoPersonalOtros { get; set; }

        ////Prestamo estudios
        //[DataMember]
        //public String TipoPrestamoEstudios { get; set; }
        //[DataMember]
        //public String TipoEstudioPrestamo { get; set; }
        //[DataMember]
        //public String TipoEstudioPrestamoOtros { get; set; }
        //[DataMember]
        //public string InstitutoPrestamo { get; set; }
        //[DataMember]
        //public string CarreraPrestamo { get; set; }
        //[DataMember]
        //public string ProgramaPrestamo { get; set; }

        ////Credito por convenio
        //[DataMember]
        //public String FechaCredito { get; set; }
        //[DataMember]
        //public String LineaConvenio { get; set; }
        //[DataMember]
        //public String UsoCredito { get; set; }

        //[DataMember]
        //public String TransferenciaCCI { get; set; }

        //Préstamo por convenio
        //Cuotas al año
        //[DataMember]
        //public String CuotasAnio { get; set; }
        //[DataMember]
        //public String ModalidadCliente { get; set; }
        //[DataMember]
        //public String ModalidadCredito { get; set; }
        //[DataMember]
        //public String AfiliacionSeguro { get; set; }
        [DataMember]
        public String EnvioEstadoCuenta { get; set; }
        [DataMember]
        public String FormaEstadoCuenta { get; set; }
        [DataMember]
        public String CorrespondenciaEstadoCuenta { get; set; }
        //[DataMember]
        //public String FormaHojaResumen { get; set; }
        //[DataMember]
        //public String CorrespondenciaHojaResumen { get; set; }
        

        [DataMember]
        public String PrimerConsentimiento { get; set; }
        [DataMember]
        public String SegundoConsentimiento { get; set; }

        public String DataReceived { get { try { return Newtonsoft.Json.JsonConvert.SerializeObject(this); } catch (Exception ex) { return ""; } } }

        //Referecias

        //[DataMember]
        //public String NombresReferencia1 { get; set; }
        //[DataMember]
        //public String ParentescoReferencia1 { get; set; }
        //[DataMember]
        //public String TelefonoReferencia1 { get; set; }

        //[DataMember]
        //public String NombresReferencia2 { get; set; }
        //[DataMember]
        //public String ParentescoReferencia2 { get; set; }
        //[DataMember]
        //public String TelefonoReferencia2 { get; set; }
        
        [DataMember]
        public String NombresFuncionario { get; set; }
        [DataMember]
        public String EmailFuncionario { get; set; }

        //Salud

        //[DataMember]
        //public String Cancer { get; set; }

        //[DataMember]
        //public String CancerMama { get; set; }

        //[DataMember]
        //public String FechaDiagnosticoCancerMama { get; set; }
        //[DataMember]
        //public String EstadoCancerMama { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCancerMama { get; set; }
        //[DataMember]
        //public String MedicoEntranteCancerMama { get; set; }

        //[DataMember]
        //public String CancerColon { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoCancerColon { get; set; }
        //[DataMember]
        //public String EstadoCancerColon { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCancerColon { get; set; }
        //[DataMember]
        //public String MedicoEntranteCancerColon { get; set; }


        //[DataMember]
        //public String CancerPulmon { get; set; }

        //[DataMember]
        //public String FechaDiagnosticoCancerPulmon { get; set; }
        //[DataMember]
        //public String EstadoCancerPulmon { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCancerPulmon { get; set; }
        //[DataMember]
        //public String MedicoEntranteCancerPulmon { get; set; }

        //[DataMember]
        //public String CancerOtro { get; set; }

        //[DataMember]
        //public String FechaDiagnosticoCancerOtro { get; set; }
        //[DataMember]
        //public String EstadoCancerOtro { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCancerOtro { get; set; }
        //[DataMember]
        //public String MedicoEntranteCancerOtro { get; set; }


        //[DataMember]
        //public String CardioVascular { get; set; }
        //[DataMember]
        //public String CardiopatiaCoronaria { get; set; }


        //[DataMember]
        //public String FechaDiagnosticoCardiopatiaCoronaria { get; set; }
        //[DataMember]
        //public String EstadoCardiopatiaCoronaria { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCardiopatiaCoronaria { get; set; }
        //[DataMember]
        //public String MedicoEntranteCardiopatiaCoronaria { get; set; }

        //[DataMember]
        //public String InsuficienciaCardiaca { get; set; }


        //[DataMember]
        //public String FechaDiagnosticoInsuficienciaCardiaca { get; set; }
        //[DataMember]
        //public String EstadoInsuficienciaCardiaca { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteInsuficienciaCardiaca { get; set; }
        //[DataMember]
        //public String MedicoEntranteInsuficienciaCardiaca { get; set; }

        //[DataMember]
        //public String CardioOtro { get; set; }

        //[DataMember]
        //public String FechaDiagnosticoCardioOtro { get; set; }
        //[DataMember]
        //public String EstadoCardioOtro { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteCardioOtro { get; set; }
        //[DataMember]
        //public String MedicoEntranteCardioOtro { get; set; }

        //[DataMember]
        //public String Renal { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoRenal { get; set; }
        //[DataMember]
        //public String EstadoRenal { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteRenal { get; set; }
        //[DataMember]
        //public String MedicoEntranteRenal { get; set; }

        //[DataMember]
        //public String Diabetes { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoDiabetes { get; set; }
        //[DataMember]
        //public String EstadoDiabetes { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteDiabetes { get; set; }
        //[DataMember]
        //public String MedicoEntranteDiabetes { get; set; }

        //[DataMember]
        //public String Neurologicas { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoNeurologicas { get; set; }
        //[DataMember]
        //public String EstadoNeurologicas { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteNeurologicas { get; set; }
        //[DataMember]
        //public String MedicoEntranteNeurologicas { get; set; }

        //[DataMember]
        //public String Psiquiatricas { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoPsiquiatricas { get; set; }
        //[DataMember]
        //public String EstadoPsiquiatricas { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntrantePsiquiatricas { get; set; }
        //[DataMember]
        //public String MedicoEntrantePsiquiatricas { get; set; }


        //[DataMember]
        //public String EnfermedadesRespiratorias { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoEnfermedadesRespiratorias { get; set; }
        //[DataMember]
        //public String EstadoEnfermedadesRespiratorias { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteEnfermedadesRespiratorias { get; set; }
        //[DataMember]
        //public String MedicoEntranteEnfermedadesRespiratorias { get; set; }

        //[DataMember]
        //public String SIDA { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoSIDA { get; set; }
        //[DataMember]
        //public String EstadoSIDA { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteSIDA { get; set; }
        //[DataMember]
        //public String MedicoEntranteSIDA { get; set; }


        //[DataMember]
        //public String OtrasEnfermedades { get; set; }
        //[DataMember]
        //public String FechaDiagnosticoOtrasEnfermedades { get; set; }
        //[DataMember]
        //public String EstadoOtrasEnfermedades { get; set; }
        //[DataMember]
        //public String IsntitucionMedicaEntranteOtrasEnfermedades { get; set; }
        //[DataMember]
        //public String MedicoEntranteOtrasEnfermedades { get; set; }

        public enum CargoOpciones : int
        {
            presidentedelarepublica = 1,
            primervicepresidente = 2,
            segundovicepresidente = 3,
            diputados = 4,
            representanteparlamentoandino = 5,
            gobernadorregional = 6,
            senadores = 7,
            vicegobernadorregional = 8,
            consejeroregional = 9,
            alcaldeprovincial = 10,
            regidorprovincial = 11,
            alcaldedistrital = 12,
            regidordistrital = 13
        }
        public enum CargoEleccionPopularOpciones : int
        {
            presidente = 1,
            vicepresidente = 2,
            congresista = 3,
            parlamentarioandino = 4,
            gobernadorregional = 5,
            vicegobernadorregional = 6,
            consejeroregional = 7,
            alcaldeprovincial = 8,
            alcaldedistrital = 9,
            alcaldecentropoblado = 10,
            regidorprovincial = 11,
            regidordistrital = 12,
            regidorcentropoblado = 13,
            diputado = 14,
            senador = 15
        }

        public enum SentenciasModalidadOpciones : int
        {
            efectiva = 1,
            reservadefallo = 2,
            suspendida = 3,
            otro = 4
        }

        public enum SentenciasCumplimientoOpciones : int
        {
            penacumplida = 1,
            encumplimiento = 2
        }

        public enum MaterialDemandaOpciones : int
        {
            laboral = 1,
            contractual = 2,
            familia = 3,
            violenciafamiliar = 4
        }

        public enum ConsejoMunicipalOpciones : int
        {
            provincial = 1,
            distrital = 2
        }

        public enum TipoComunidadOpciones : int
        {
            nativa = 1,
            campesina = 2,
            pueblooriginario = 3
        }
        //public enum TipoDoc : int
        //{
        //    DNI = 1,
        //    CE = 2,
        //    RUC = 3,
        //    CI = 4,
        //    Otros = 5
        //}

        //public enum Moneda : int
        //{
        //    Soles = 1,
        //    Dolares = 2,
        //    Euros = 3
        //}

        //public enum Credito : int
        //{
        //    PrestamoNegocio = 1,
        //    Convenios = 2,
        //    VehicularGNV = 3,
        //    PrestamoPersonal = 4,
        //    Hipotecario = 5,
        //    HipotecarioEspecial = 6,
        //    MiVivienda = 7,
        //    TechoPropio = 8,
        //    Vehicular = 9,
        //    HipotecarioRetorno = 10,
        //    Microfinanzas = 11,
        //    Pymes = 12,
        //    Preferente = 13,
        //    NuevoMiVivienda = 14,
        //    PrestamoPersonalColaborador = 15,
        //    PrestamoPersonalEstudios = 16,
        //    Pyme = 17,
        //    Nuevo = 18,
        //    Reenganche = 19

        //}

        //public enum Genero : int
        //{
        //    Femenino = 1,
        //    Masculino = 2
        //}

        //public enum TipoVivienda : int
        //{
        //    Propia = 1,
        //    PropiaFinanciada = 2,
        //    Alquilada = 3,
        //    Familiar = 4
        //}

        //public enum EstadoCivil : int
        //{
        //    Soltero = 1,
        //    Conviviente = 2,
        //    Casado = 3,
        //    CasadoSepBienes = 4,
        //    Viudo = 5,
        //    Divorciado = 6
        //}
        //public enum GradoEstudios : int
        //{
        //    Primaria = 1,
        //    Secundaria = 2,
        //    Tecnico = 3,
        //    Universitario = 4,
        //    Ninguno = 5
        //}

        //public enum SituacionLaboral : int
        //{
        //    Dependiente = 1,
        //    Profesional = 2,
        //    Accionista = 3,
        //    Rentista = 4,
        //    PersonaNatural = 5,
        //    Jubilado = 6
        //}

        //public enum TipoTarjeta : int
        //{
        //    Tarjeta = 1,
        //    Prestamo = 2
        //}
        //public enum TipoValor : int
        //{
        //    PortaValor = 1,
        //    Directo = 2,
        //    CCE = 3
        //}
        //public enum direcciondetalled : int
        //{
        //    calle = 1,
        //    avenida = 2,
        //    jiron = 3,
        //    pasaje = 4,
        //    otro = 5
        //}
        //public enum DireccionDetalleExteriorD : int
        //{
        //    Numero = 1,
        //    Bloque = 2,
        //    Manzana = 3,
        //    Otro = 4
        //}
        //public enum DireccionDetalleInteriorD : int
        //{
        //    Lote = 1,
        //    Departamento = 2,
        //    Int = 3,
        //    Otro = 4
        //}
        //public enum DireccionDetalleZonaD : int
        //{
        //    Seccion = 1,
        //    Etapa = 2,
        //    Urbanizacion = 3,
        //    AAHH = 4,
        //    Otro = 5
        //}

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

    }
}
