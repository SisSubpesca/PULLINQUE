using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;
using Datos.Entidades.Relocalizacion;

namespace Datos.Entidades
{

     [Serializable()]
    public class SolicitudConcesion
    {
        public Usuario usuario { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico region { get; set; }
        public ParametroGenerico provincia { get; set; }
        public List<ParametroGenerico> comuna { get; set; }
        public ParametroGenerico tipoBarrio { get; set; }
        public Barrio barrio { get; set; }
        public Barrio acm { get; set; }
        public Macrozona macrozona { get; set; }
        public ParametroGenerico tipoUnidadEspacial { get; set; }
        public bool reqAntecTerreno { get; set; }
        public bool reqRegularizacion { get; set; }
        public DateTime fechaIngresoTramite { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public DateTime fechaRecepcion { get; set; }
        //public ParametroGenerico tipoUnidEspacial { get; set; }
        public List<CoordenadaGeografica> coordenadaGeografica { get; set; }
        public string numPert { get; set; }
        public ParametroGenerico estadoActual { get; set; }
        public ParametroGenerico estadoAnterior { get; set; }
        public ParametroGenerico estadoPosterior { get; set; }
        public List<Persona> titularesSolConcesion { get; set; }
        public ParametroGenerico comunaFiltro { get; set; }
        public ParametroGenerico amerbFiltro { get; set; }
        public ParametroGenerico ecmpoFiltro { get; set; }
         
        public DateTime fechaRangoFiltro1 { get; set; }
        public DateTime fechaRangoFiltro2 { get; set; }
        public string DescripcionComuna { get { return comunas(); } }
        public string DescripcionTitulares { get { return titulares(); } }
        public string DescripcionTitularesComa { get { return titularesComa(); } }
        public string DescripcionEspeciesComa { get { return especiesComa(); } }
        public string DescripcionToponimios { get { return toponimios(); } }
        public UnidadEspacial unidadEspacial { get; set; }
        public ParametroGenerico sometimientoSEA { get; set; }
        public Persona titularFiltro { get; set; }
        public int pagina { get; set; }
        public bool comunaFronteriza { get; set; }
        public float superficieCalculada { get; set; }
        public float superficieCalculadaCultivo { get; set; }
        public ParametroGenerico tipoTramite { get; set; }
        public List<ParametroGenerico> tipoModificacionesTram { get; set; }
        public float superficieTramReq { get; set; }
        public float superficieTramFinal { get; set; }
        public TramiteModConcesion tramiteModConcesion { get; set; }
        public string DescripcionTipoModificacion { get { return tipoModificacionString(); } }
        public int aplicaDependencia { get; set; }
        public List<UnidadDependenciaMod> unidadDependenciaList { get; set; }
        public bool traspasoOk { get; set; }
        public DatosSolicitudUE datosSolicitudUE { get; set; }
        public int tieneRCA { get; set; }
        public ParametroGenerico especie { get; set; }
        public List<ParametroGenerico> especiesSolicitud { get; set; }
        public bool tieneAsignadaSolicitud { get; set; }
        public DocSolicitudCierre solicitudCierre { get; set; }
        public DetalleSector sectorRelocalizacion { get; set; }
        public bool recursoReposicion { get; set; }
        public bool tieneIT { get; set; }
        public bool tieneSSP { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public ParametroGenerico estadoCierreForzado { get; set; }
        public String titularesCad { get; set; }
        public String especiesCad { get; set; }
        public String gruposInformativosCad { get; set; }
        public String comunasCad { get; set; }
        public String toponimiosCad { get; set; }
        public String tipoSolicitudCad { get; set; }
        public String tipoModSolicitud { get; set; }
        public String resolucionSSFFAA { get; set; }
        public String itc { get; set; }
        public String resolucionSSP { get; set; }
        public ParametroGenerico diasCantidad { get; set; }
        public ParametroGenerico temaReq { get; set; }
        public string especieCadFiltro { get; set; }
        public string codigoCentro { get { return codigoCentroString(); } }
        public CuerpoDeAgua cuerpoAgua { get; set;}
        //temaDestinatarioDinámico id: idSubRequerimiento; nombre:nombreSubRequerimiento; clave:correoDestinatario
        public List<ParametroGenerico> temaDestinatarioDinamico { get; set; }
        public ParametroGenerico tipoModificacion { get; set; }


        public ParametroGenerico evaluaUOT { get; set; }
        //public ParametroGenerico sometimientoCPS_INFAS { get; set; }
        public ParametroGenerico verificaCertOperacion { get; set; }
        public ParametroGenerico tramitaUTS { get; set; }
        public ParametroGenerico sspaNivelCentral { get; set; }
        public ParametroGenerico evaluaUOT_Amb { get; set; }

        public ParametroGenerico devolucionJuridica { get; set; }  //check para la devolucion de juridica
        public ParametroGenerico devolucionMarina { get; set; }  //check para la devolucion de marina
        public ParametroGenerico pertinenciaSMA { get; set; }  //check para la pertinencia de consulta SMA
        public ParametroGenerico avanzaSMA { get; set; }  //check para indicar si el flujo avanza sin esperar la respuesta de la SMA
        public ParametroGenerico tipoEvaluacionAmbiental { get; set; }  //check para indicar que tipo de evaluacion ambiental se debe realizar
        public ParametroGenerico tipoCertificadoDistancia { get; set; }  //check para indicar comos se hara el certificado de distancia
        public ParametroGenerico nuevoITCMOUOT { get; set; }  //check para indicar si se requiere un nuevo ITC MO UOT
        public ParametroGenerico nuevoITCCPSUOT { get; set; }  //check para indicar si se requiere un nuevo ITC MO UOT

        public ParametroGenerico decisionZonal { get; set; }  //check para indicar sobre la decision zonal
        public ParametroGenerico decisionCentral { get; set; }  //check para indicar sobre la decision central
        public ParametroGenerico decisionNotificacionInsuficiencia { get; set; }  //check para indicar sobre la decision de zonal por la notificaciond e insuficiancia
        public ParametroGenerico decisionNotificacionInsuficienciaCentral { get; set; }  //check para indicar sobre la decision de Central por la notificaciond e insuficiancia
        public ParametroGenerico decisionUGP { get; set; }  //check para indicar sobre la decision de UGP sobre si sigue el tramite
        public ParametroGenerico decisionUGPMultiple { get; set; }  //check para indicar sobre la decision de UGP sobre a que estado debe continuar
        public ParametroGenerico requiereNuevoPT { get; set; } //check que indica si la solicitud requiere nuevo PT o no.

        public ParametroGenerico verificaAntecedentes { get; set; }  //check para indicar sobre la decision del sectorialista para experimental concesion
        public ParametroGenerico tipoRechazoSol { get; set; }  //check para indicar sobre la decision si el tipo de rechazo en exp concesion es por resolución o carta
        public ParametroGenerico requiereITUOT_Plano { get; set; }  //check para indicar decisión del usuario sobre isla planos que indica si la solicitud requiere planos 14 ter.
        public ParametroGenerico evaluaUOT_Cartografia { get; set; } //check para indicar decisión del usuario si requiere evaluación por parte de UOT para rama de Cartografía

        public ParametroGenerico supeditaAvanzaAprueba { get; set; } //check para indicar decisión del usuario si un IT UOT supedita avanza por el camino del Aprueba
        public ParametroGenerico suspendeAvanzaEstado { get; set; } //check para indicar decisión del usuario si un IT UOT supedita avanza por el camino del Aprueba

        public ParametroGenerico omiteSSFFAA { get; set; }  //check para indicar si la solicitud omite el ingreso de la resolución SSFFAA o no.
        public ParametroGenerico omiteInspTerreno { get; set; }  //check para indicar si omite el flujo de estados solicitando Inspección de Terreno o no.
        public ParametroGenerico omiteBanco { get; set; }  //check para indicar si omite el flujo de estados solicitando Banco Natural o no.
        public ParametroGenerico omiteDifRadial { get; set; }  //check para indicar si omite el flujo de estados solicitando Difusión Radial o no.
        public ParametroGenerico omiteEvAmbiental { get; set; }  //check para indicar si omite el flujo de estados solicitando Evaluación Ambiental o no.

        public bool mensajePendienteMostrado { get; set; } //indica si el mensaje de que la solicitud esta pendiente o suspendida ya se mostro



        public string docRequerido { get; set; }

        public List<EstadoIsla> islas { get; set; }


        public ParametroGenerico subTipoTramite { get; set; }

        public int idConcesion { get; set; }

        public string islasString() { 
        
            string islasString = "";
            int i = 0;

            if (islas != null && islas.Count > 0)
            {

                foreach (EstadoIsla estadoIsla in this.islas)
                {
                    string tipoEstadoAux = "";

                    if (estadoIsla.tipoIsla != null)
                    {
                        if (i == 0)
                        {
                            islasString = "("+ estadoIsla.tipoIsla.descripcion + "):" + estadoIsla.estadoActual.descripcion;
                            
                        }
                        else
                        {

                            if (estadoIsla.tipoIsla != null && !estadoIsla.tipoIsla.descripcion.Equals(tipoEstadoAux))
                            {
                                islasString = islasString + "<br>";
                                islasString = islasString + " (" + estadoIsla.tipoIsla.descripcion + "):" + estadoIsla.estadoActual.descripcion;
                                
                            }
                            else
                            {
                                islasString = islasString + "<br>";
                                islasString = islasString + "-" + estadoIsla.estadoActual.descripcion;
                                
                            }
                        }
                        tipoEstadoAux = estadoIsla.tipoIsla.descripcion;
                        i++;
                    }
                }
            }

            return islasString;

        }

        public string codigoCentroString() { 
        
            string codigoCentro = "";

            if (unidadEspacial != null && unidadEspacial.centrosDeCultivo != null && unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
            {
                codigoCentro = Convert.ToString(unidadEspacial.centrosDeCultivo.codigoCentro);
            }

            return codigoCentro;
        }

        public string toponimios()
        {

            string toponimiosLista = "";

            int i = 0;
            if (coordenadaGeografica != null)
            {
                foreach (CoordenadaGeografica aCoordenadaGeografica in coordenadaGeografica)
                {

                    if (aCoordenadaGeografica.listaPoligono != null)
                    { 
                        foreach(Poligono aPoligono in aCoordenadaGeografica.listaPoligono){

                            if (i == 0)
                            {
                                toponimiosLista = aPoligono.toponimio;
                            }
                            else
                            {
                                toponimiosLista = toponimiosLista + ", " + aPoligono.toponimio;
                            }
                            i++;
                        }
                    }
                }
            }

            return toponimiosLista;
        }

        public int idEstadoUnidadEspacial {

            get
            {
                if (estadoVigencia != null && estadoVigencia.id > 0)
                {
                    return estadoVigencia.id;
                }else{
                    return 0;
                }
            }
        }

        public string comunas() {
            
            string comunasLista = "";
            int i = 0;

            if (comuna != null)
            {
                foreach (ParametroGenerico paramGenAux in comuna)
                {
                    if (i == 0)
                    {
                        comunasLista = paramGenAux.descripcion;
                    }
                    else
                    {
                        comunasLista = comunasLista + " <br>" + paramGenAux.descripcion;
                    }
                    i++;
                }
            }
                
        
           return comunasLista;
        }

        public string titulares()
        {
            string titularesLista = "";

            int i = 0;

            if (titularesSolConcesion != null)
            {
                foreach (Persona titularAux in titularesSolConcesion)
                {
                    if (i == 0)
                    {
                        titularesLista = titularAux.rutPersona + "-" + Convert.ToString(titularAux.dvPersona) + ": " + titularAux.nombreSolicitante;
                    }
                    else
                    {
                        titularesLista = titularesLista + " <br>" + titularAux.rutPersona + "-" + Convert.ToString(titularAux.dvPersona) + ": " + titularAux.nombreSolicitante;
                    }
                    i++;
                }
            }

            return titularesLista;
        }


        public string titularesComa()
        {
            string titularesLista = "";
            int i = 0;


            if (titularesSolConcesion != null)
            {
                foreach (Persona titularAux in titularesSolConcesion)
                {
                    if (i == 0)
                    {
                        titularesLista = titularAux.rutPersona + "-" + Convert.ToString(titularAux.dvPersona) + ": " + titularAux.nombreSolicitante;
                    }
                    else
                    {
                        titularesLista = titularesLista + ", " + titularAux.rutPersona + "-" + Convert.ToString(titularAux.dvPersona) + ": " + titularAux.nombreSolicitante;
                    }
                    i++;
                }
            }

            return titularesLista;
        }


        public string especiesComa()
        {
            string especiesLista = "";
            int i = 0;


            if (especiesSolicitud != null)
            {
                foreach (ParametroGenerico especie in especiesSolicitud)
                {
                    if (i == 0)
                    {
                        especiesLista = especie.descripcion;
                    }
                    else
                    {
                        especiesLista = especiesLista + ", " + especie.descripcion;
                    }
                    i++;
                }
            }

            return especiesLista;
        }



        public string tipoModificacionString()
        {
            string tipoModificacionLista = "";
            int i = 0;

            if (tipoModificacionesTram != null)
            {
                foreach (ParametroGenerico tipoModificacion in tipoModificacionesTram)
                {
                    if (tipoModificacion != null && tipoModificacion.id == 90) {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_ESPECIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 91) {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_PT;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 92) {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_AMPLIA_SUPERFICIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 93)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_REDUCE_SUPERFICIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 94)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_REGULARIZACION;
                    }

                    if (i == 0)
                    {
                        tipoModificacionLista = tipoModificacion.descripcion;
                    }
                    else
                    {
                        tipoModificacionLista = tipoModificacionLista + ", " + tipoModificacion.descripcion;
                    }
                    i++;
                }
            }

            return tipoModificacionLista;
        }


        

        // MÉTODOS (Constructores)
        public SolicitudConcesion()
        { }


        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idSolConcesion { get; set; }
            public ParametroGenerico region { get; set; }
            public ParametroGenerico provincia { get; set; }
            public List<ParametroGenerico> comuna { get; set; }
            //public ParametroGenerico tipoBarrio { get; set; }
            public Barrio barrio { get; set; }
            public Barrio acm { get; set; }
            //public Macrozona macrozona { get; set; }
            public ParametroGenerico tipoUnidadEspacial { get; set; }
            public bool reqAntecTerreno { get; set; }
            public bool reqRegularizacion { get; set; }
            public DateTime fechaIngresoTramite { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public DateTime fechaRecepcion { get; set; }
            //public ParametroGenerico tipoUnidEspacial { get; set; }
            public List<CoordenadaGeografica> coordenadaGeografica { get; set; }
            public string numPert { get; set; }
            public ParametroGenerico estadoActual { get; set; }
            public ParametroGenerico estadoAnterior { get; set; }
            public ParametroGenerico estadoPosterior { get; set; }
            public List<Persona> titularesSolConcesion { get; set; }
            public ParametroGenerico comunaFiltro { get; set; }
            public ParametroGenerico amerbFiltro { get; set; }
            public ParametroGenerico ecmpoFiltro { get; set; }
            public DateTime fechaRangoFiltro1 { get; set; }
            public DateTime fechaRangoFiltro2 { get; set; }
            public UnidadEspacial unidadEspacial { get; set; }
            public Persona titularFiltro { get; set; }
            public int pagina { get; set; }
            public bool comunaFronteriza { get; set; }
            public float superficieCalculada { get; set; }
            public ParametroGenerico tipoTramite { get; set; }
            public List<ParametroGenerico> tipoModificacionesTram { get; set; }
            public ParametroGenerico sometimientoSEA { get; set; }
            public float superficieTramReq { get; set; }
            public float superficieTramFinal { get; set; }
            public TramiteModConcesion tramiteModConcesion { get; set; }
            public int aplicaDependencia { get; set; }
            public List<UnidadDependenciaMod> unidadDependenciaList { get; set; }
            public bool traspasoOk { get; set; }
            public float superficieCalculadaCultivo { get; set; }
            public DatosSolicitudUE datosSolicitudUE { get; set; }
            public int tieneRCA { get; set; }
            public ParametroGenerico especie { get; set; }
            public List<ParametroGenerico> especiesSolicitud { get; set; }
            public bool tieneAsignadaSolicitud { get; set; }
            public DocSolicitudCierre solicitudCierre { get; set; }
            public DetalleSector sectorRelocalizacion { get; set; }
            public bool recursoReposicion { get; set; }
            public bool tieneIT { get; set; }
            public bool tieneSSP { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public ParametroGenerico estadoCierreForzado { get; set; }
            public String titularesCad { get; set; }
            public String especiesCad { get; set; }
            public String gruposInformativosCad { get; set; }
            public String comunasCad { get; set; }
            public String toponimiosCad { get; set; }
            public String tipoSolicitudCad { get; set; }
            public String tipoModSolicitud { get; set; }
            public String resolucionSSFFAA { get; set; }
            public String resolucionSSP { get; set; }
            public String itc { get; set; }
            public ParametroGenerico diasCantidad { get; set; }
            public ParametroGenerico temaReq { get; set; }
            public string especieCadFiltro { get; set; }
            public CuerpoDeAgua cuerpoAgua { get; set; }


            public ParametroGenerico evaluaUOT { get; set; }
            //public ParametroGenerico sometimientoCPS_INFAS { get; set; }
            public ParametroGenerico verificaCertOperacion { get; set; }
            public ParametroGenerico tramitaUTS { get; set; }
            public ParametroGenerico sspaNivelCentral { get; set; }
            public ParametroGenerico evaluaUOT_Amb { get; set; }

            public ParametroGenerico devolucionJuridica { get; set; }  //check para la devolucion de juridica
            public ParametroGenerico devolucionMarina { get; set; }  //check para la devolucion de marina
            public ParametroGenerico pertinenciaSMA { get; set; }  //check para la pertinencia de consulta SMA
            public ParametroGenerico avanzaSMA { get; set; }  //check para indicar si el flujo avanza sin esperar la respuesta de la SMA
            public ParametroGenerico tipoEvaluacionAmbiental { get; set; }  //check para indicar que tipo de evaluacion ambiental se debe realizar
            public ParametroGenerico tipoCertificadoDistancia { get; set; }  //check para indicar comos se hara el certificado de distancia
            public ParametroGenerico nuevoITCMOUOT { get; set; }  //check para indicar si se requiere un nuevo ITC MO UOT
            public ParametroGenerico nuevoITCCPSUOT { get; set; }  //check para indicar si se requiere un nuevo ITC MO UOT

            public ParametroGenerico decisionZonal { get; set; }  //check para indicar sobre la decision zonal
            public ParametroGenerico decisionCentral { get; set; }  //check para indicar sobre la decision central
            public ParametroGenerico decisionNotificacionInsuficiencia { get; set; }  //check para indicar sobre la decision de zonal por la notificaciond e insuficiancia
            public ParametroGenerico decisionNotificacionInsuficienciaCentral { get; set; }  //check para indicar sobre la decision de Central por la notificaciond e insuficiancia
            public ParametroGenerico decisionUGP { get; set; }  //check para indicar sobre la decision de UGP sobre si sigue el tramite
            public ParametroGenerico decisionUGPMultiple { get; set; }  //check para indicar sobre la decision de UGP sobre a que estado debe continuar
            public ParametroGenerico tipoModificacion { get; set; }

            public ParametroGenerico verificaAntecedentes { get; set; }  //check para indicar sobre la decision del sectorialista para experimental concesion
            public ParametroGenerico tipoRechazoSol { get; set; }  //check para indicar sobre la decision si el tipo de rechazo en exp concesion es por resolución o carta
            public ParametroGenerico requiereITUOT_Plano { get; set; }  //check para indicar decisión del usuario sobre isla planos que indica si la solicitud requiere planos 14 ter.
            public ParametroGenerico evaluaUOT_Cartografia { get; set; } //check para indicar decisión del usuario si requiere evaluación por parte de UOT para rama de Cartografía

            public List<EstadoIsla> islas { get; set; }

            public string docRequerido { get; set; }

            public ParametroGenerico subTipoTramite { get; set; }

            public bool mensajePendienteMostrado { get; set; } //indica si el mensaje de que la solicitud esta pendiente o suspendida ya se mostro
            public ParametroGenerico requiereNuevoPT { get; set; }
        }

       
    }
}
