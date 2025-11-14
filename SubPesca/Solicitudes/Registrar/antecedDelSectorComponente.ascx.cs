using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.antecedentesSector;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Validaciones.cl.subpesca.rb.solicitud;
using System.Data;
using Datos.Utilidades;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SubPesca.Solicitudes.Registrar
{
    public partial class antecedDelSectorComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        RegionDA regionDA = new LogicaNegocio.cl.subpesca.rb.common.RegionDA();
        ProvinciaDA provinciaDA = new LogicaNegocio.cl.subpesca.rb.common.ProvinciaDA();
        ComunaDA comunaDA = new ComunaDA();
        TipoDA tipoDa = new TipoDA();
        BarrioDA barrioDA = new BarrioDA();
        TipoConcesionDA tipoConcesionDA = new TipoConcesionDA();
        DatumDA datumDA = new DatumDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        PoligonoDA poligonoDA = new PoligonoDA();
        VerticeDA verticeDA = new VerticeDA();
        ObsPestaniaInformeDA obsPestaniaInformeDA = new ObsPestaniaInformeDA();
        CartaDA cartaDA = new CartaDA();
        EspecieProyTecnicoDA especieProyTecnicoDA = new EspecieProyTecnicoDA();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        RelocalizacionService relocalizacionService = new RelocalizacionService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        PermisosService permisosService = new PermisosService();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        AntecedDelSectorValidacion antecedDelSectorValidacion = new AntecedDelSectorValidacion();

        AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

        Funciones funciones = new Funciones();


        protected void setearModulo()
        {
            if (funciones.retornaModulo().Equals("Registrar"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_CONCESION };
                ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_CONCESION };
                ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_CONCESION };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_CONCESION };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_CONCESION };
                ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_CONCESION };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_CONCESION };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_CONCESION };
                ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGAL_CONCESION };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_CONCESION };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_CONCESION };
                ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_CONCESION };
                ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_CONCESION };

            }
            else if (funciones.retornaModulo().Equals("Concesion"))
            {

                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_CONCESION_CREADA };

            }
            else if (funciones.retornaModulo().Equals("Relocalizacion"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_FUSIONA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_SECTOR_CERO };
                }


            }
            else if (funciones.retornaModulo().Equals("RelocalizacionRESA"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_RELOCALIZACION_RESA;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_RELOCALIZACION_RESA;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_RELOCALIZACION_RESA;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_RELOCALIZACION_RESA;
                ViewState["solicitudSession"] = paginas.solicitudRelocalizacionSessionRESA;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_RELOCALIZACION_SECTOR_CERO_RESA };
                }


            }
            else if (funciones.retornaModulo().Equals("Acopio"))
            {
                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                    ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_ACOPIO_CREADO };
                }
                else
                {

                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                    ViewState["solicitudSession"] = paginas.solicitudAcopioSession;

                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_ACOPIO };
                }

            }
            else if (funciones.retornaModulo().Equals("Faenamiento"))
            {
                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                    ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_FAENAMIENTO_CREADO };
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                    ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;

                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_FAENAMIENTO };

                }

            }
            else if (funciones.retornaModulo().Equals("Amerb"))
            {
                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudAmerbSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_AMERB_CREADO };
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudAmerbSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_AMERB };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_AMERB };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_AMERB };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_AMERB };
                }


            }
            else if (funciones.retornaModulo().Equals("ExperimentalesAmerb"))
            {

                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_EXPERIMENTALES_AMERB_CREADO };
                }
                else {

                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_EXPERIMENTALES_AMERB };
                
                }
            }

            else if (funciones.retornaModulo().Equals("ExperimentalesConcesion"))
            {

                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {

                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_EXPERIMENTALES_CONCESION_CREADO };
                }
                else 
                { 

                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_EXPERIMENTALES_CONCESION };

                }


            }

            else if (funciones.retornaModulo().Equals("ECMPO"))
            {
                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                    ViewState["solicitudSession"] = paginas.solicitudECMPOSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_ECMPO_CREADO };

                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                    ViewState["solicitudSession"] = paginas.solicitudECMPOSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_ECMPO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_ECMPO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_ECMPO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_ECMPO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_ECMPO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_ECMPO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_ECMPO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_ECMPO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_ECMPO };

                }

            }

            else if (funciones.retornaModulo().Equals("Colector"))
            {
                if (funciones.retornaTipoModulo().Equals("Unidades"))
                {
                    ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                    ViewState["solicitudSession"] = paginas.solicitudColectorSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_COLECTOR_CREADO };
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                    ViewState["solicitudSession"] = paginas.solicitudColectorSession;


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = new int[] { rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = new int[] { rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = new int[] { rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = new int[] { rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.AS_OBSERVACIONES_COLECTOR };
                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionAmerb"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_AMERB;
                ViewState["solicitudSession"] = paginas.solicitudModificacionAmerbSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacionUbicacionGeografica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionAgrupacionConcesiones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionOtraDefinicion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_AMERB_AMPLIA_SUPERFICIE;

                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_AMERB_ESPECIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_AMERB_ESPECIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_AMERB_ESPECIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_AMERB_ESPECIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_AMERB_ESPECIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_AMERB_ESPECIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_AMERB_ESPECIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_AMERB_ESPECIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_AMERB_ESPECIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_AMERB_ESPECIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_AMERB_ESPECIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_AMERB_ESPECIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_AMERB_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_AMERB_PT;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_AMERB_PT;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_AMERB_PT;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_AMERB_PT;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_AMERB_PT;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_AMERB_PT;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_AMERB_PT;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_AMERB_PT;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_AMERB_PT;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_AMERB_PT;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_AMERB_PT;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_AMERB_PT;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_AMERB_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_AMERB_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_AMERB_REGULARIZACION;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_AMERB_REGULARIZACION;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_AMERB_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_AMERB_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_AMERB_REGULARIZACION;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_AMERB_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_AMERB_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_AMERB_REGULARIZACION;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_AMERB_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_AMERB_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_AMERB_REGULARIZACION;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_AMERB_REGULARIZACION;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_AMERB_REGULARIZACION;
                            contador++;
                        }
                    }


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = tiposModificacionUbicacionGeografica;
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = tiposModificacionAgrupacionConcesiones;
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = tiposModificacionOtraDefinicion;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = tiposModificacionReferenciaGeograficaOriginal;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = tiposModificacionArchivosAdjuntosOriginal;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = tiposModificacionPoligonoOriginal;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = tiposModificacionReferenciaGeograficaEntrega;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = tiposModificacionArchivosAdjuntosEntrega;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = tiposModificacionPoligonoEntrega;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = tiposModificacionReferenciaGeograficaRegularizacion;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = tiposModificacionArchivosAdjuntosRegularizacion;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = tiposModificacionPoligonoRegularizacion;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservacion;

                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroAcopio"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_ACOPIO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroAcopioSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacionUbicacionGeografica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionAgrupacionConcesiones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionOtraDefinicion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;

                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_ACOPIO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_ACOPIO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            contador++;
                        }
                    }


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = tiposModificacionUbicacionGeografica;
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = tiposModificacionAgrupacionConcesiones;
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = tiposModificacionOtraDefinicion;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = tiposModificacionReferenciaGeograficaOriginal;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = tiposModificacionArchivosAdjuntosOriginal;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = tiposModificacionPoligonoOriginal;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = tiposModificacionReferenciaGeograficaEntrega;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = tiposModificacionArchivosAdjuntosEntrega;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = tiposModificacionPoligonoEntrega;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = tiposModificacionReferenciaGeograficaRegularizacion;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = tiposModificacionArchivosAdjuntosRegularizacion;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = tiposModificacionPoligonoRegularizacion;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservacion;

                }
            }
            else if (funciones.retornaModulo().Equals("ModificacionCentroFaenamiento"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_CENTRO_FAENAMIENTO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionCentroFaenamientoSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacionUbicacionGeografica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionAgrupacionConcesiones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionOtraDefinicion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;

                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            contador++;
                        }
                    }


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = tiposModificacionUbicacionGeografica;
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = tiposModificacionAgrupacionConcesiones;
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = tiposModificacionOtraDefinicion;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = tiposModificacionReferenciaGeograficaOriginal;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = tiposModificacionArchivosAdjuntosOriginal;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = tiposModificacionPoligonoOriginal;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = tiposModificacionReferenciaGeograficaEntrega;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = tiposModificacionArchivosAdjuntosEntrega;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = tiposModificacionPoligonoEntrega;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = tiposModificacionReferenciaGeograficaRegularizacion;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = tiposModificacionArchivosAdjuntosRegularizacion;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = tiposModificacionPoligonoRegularizacion;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservacion;

                }


            }
            else if (funciones.retornaModulo().Equals("ModificacionECMPO"))
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_MODIFICACION_ECMPO;
                ViewState["solicitudSession"] = paginas.solicitudModificacionECMPOSession;

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacionUbicacionGeografica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionAgrupacionConcesiones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionOtraDefinicion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_ECMPO_AMPLIA_SUPERFICIE;

                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_ECMPO_ESPECIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_ECMPO_ESPECIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_ECMPO_ESPECIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_ECMPO_ESPECIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_ECMPO_ESPECIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_ECMPO_ESPECIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_ECMPO_ESPECIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_ECMPO_ESPECIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_ECMPO_ESPECIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_ECMPO_ESPECIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_ECMPO_ESPECIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_ECMPO_ESPECIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_ECMPO_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_ECMPO_PT;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_ECMPO_PT;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_ECMPO_PT;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_ECMPO_PT;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_ECMPO_PT;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_ECMPO_PT;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_ECMPO_PT;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_ECMPO_PT;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_ECMPO_PT;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_ECMPO_PT;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_ECMPO_PT;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_ECMPO_PT;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_ECMPO_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_ECMPO_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_ECMPO_REGULARIZACION;
                            contador++;
                        }
                    }


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = tiposModificacionUbicacionGeografica;
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = tiposModificacionAgrupacionConcesiones;
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = tiposModificacionOtraDefinicion;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = tiposModificacionReferenciaGeograficaOriginal;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = tiposModificacionArchivosAdjuntosOriginal;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = tiposModificacionPoligonoOriginal;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = tiposModificacionReferenciaGeograficaEntrega;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = tiposModificacionArchivosAdjuntosEntrega;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = tiposModificacionPoligonoEntrega;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = tiposModificacionReferenciaGeograficaRegularizacion;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = tiposModificacionArchivosAdjuntosRegularizacion;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = tiposModificacionPoligonoRegularizacion;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservacion;

                }


            }
            else
            {
                ViewState["URL_VER"] = paginas.URL_VER_SOLMOD;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLMOD;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLMOD;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLMOD;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLMOD;
                ViewState["solicitudSession"] = paginas.solicitudModificacionSession;



                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

                if (solicitudConcesion != null)
                {

                    int[] tiposModificacionUbicacionGeografica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionAgrupacionConcesiones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionOtraDefinicion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoOriginal = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoEntrega = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionReferenciaGeograficaRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionArchivosAdjuntosRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionPoligonoRegularizacion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservacion = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CONCESION_AMPLIA_SUPERFICIE;

                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CONCESION_ESPECIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CONCESION_ESPECIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CONCESION_ESPECIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CONCESION_ESPECIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CONCESION_ESPECIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CONCESION_ESPECIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CONCESION_ESPECIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CONCESION_ESPECIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CONCESION_ESPECIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CONCESION_ESPECIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CONCESION_ESPECIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CONCESION_ESPECIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CONCESION_ESPECIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CONCESION_PT;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CONCESION_PT;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CONCESION_PT;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CONCESION_PT;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CONCESION_PT;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CONCESION_PT;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CONCESION_PT;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CONCESION_PT;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CONCESION_PT;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CONCESION_PT;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CONCESION_PT;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CONCESION_PT;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CONCESION_PT;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CONCESION_REDUCE_SUPERFICIE;
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacionUbicacionGeografica[contador] = rbSeccionUnidadEspacial.AS_UBICACION_GEOGRAFICA_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionAgrupacionConcesiones[contador] = rbSeccionUnidadEspacial.AS_AGRUPACION_CONCESIONES_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionOtraDefinicion[contador] = rbSeccionUnidadEspacial.AS_OTRA_DEFINICION_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaOriginal[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ORIGINAL_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosOriginal[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ORIGINAL_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionPoligonoOriginal[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ORIGINAL_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaEntrega[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_ENTREGA_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosEntrega[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_ENTREGA_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionPoligonoEntrega[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_ENTREGA_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionReferenciaGeograficaRegularizacion[contador] = rbSeccionUnidadEspacial.AS_REFERENCIA_GEOGRAFICA_REGULARIZACION_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionArchivosAdjuntosRegularizacion[contador] = rbSeccionUnidadEspacial.AS_ARCHIVOS_ADJUNTOS_REGULARIZACION_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionPoligonoRegularizacion[contador] = rbSeccionUnidadEspacial.AS_POLIGONO_REGULARIZACION_MOD_CONCESION_REGULARIZACION;
                            tiposModificacionObservacion[contador] = rbSeccionUnidadEspacial.AS_OBSERVACIONES_MOD_CONCESION_REGULARIZACION;
                            contador++;
                        }
                    }


                    ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"] = tiposModificacionUbicacionGeografica;
                    ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"] = tiposModificacionAgrupacionConcesiones;
                    ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"] = tiposModificacionOtraDefinicion;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"] = tiposModificacionReferenciaGeograficaOriginal;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"] = tiposModificacionArchivosAdjuntosOriginal;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"] = tiposModificacionPoligonoOriginal;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"] = tiposModificacionReferenciaGeograficaEntrega;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"] = tiposModificacionArchivosAdjuntosEntrega;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"] = tiposModificacionPoligonoEntrega;
                    ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"] = tiposModificacionReferenciaGeograficaRegularizacion;
                    ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"] = tiposModificacionArchivosAdjuntosRegularizacion;
                    ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"] = tiposModificacionPoligonoRegularizacion;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservacion;

                }

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {



            String script = @"<script type='text/javascript'>nuevaFuncion();</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAutocompletar", script.ToString(), false);

            script = @"<script type='text/javascript'>nuevaFuncion2();</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAutocompletar2", script.ToString(), false);

            if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
            {
                if ((Request.Params["__EVENTTARGET"] == this.NombreCartaAntecedentesEspaciales.ClientID) && (Request.Params["__EVENTARGUMENT"] == "onchange"))
                {
                    this.NombreCartaAntecedentesEspaciales_TextChanged(null, null);

                }
                else if ((Request.Params["__EVENTTARGET"] == this.NombreCartaRegul.ClientID) && (Request.Params["__EVENTARGUMENT"] == "onchange"))
                {
                    this.NombreCartaRegul_TextChanged(null, null);

                }
            }

            // PAGE LOAD
            if (!Page.IsPostBack)
            {
                string Lang = "es-CL";//set your culture here
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);


                setearModulo();

                SolicitudConcesion solicitudConcesion = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudConcesion == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }

                //REGLAS POR TIPO DE MODIFICACION VS QUE SE PUEDE MODIFICAR (PASO 1)
                bool tieneAsignadaSolicitudAux = solicitudConcesion.tieneAsignadaSolicitud;
                Funciones.AplicarReglaTipoModificacion(solicitudConcesion, Funciones.ANTECEDENTES_DEL_SECTOR);


                
                // Inicializamos el formulario
                Initialize_Form();




                // ACCESO A Ubicación Geográfica
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_UBICACION_GEOGRAFICA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    Region.Enabled = true;
                    Provincia.Enabled = true;
                    Comuna.Enabled = true;
                    PanelGuardarUbicacionGeografica.Visible = true;
                    TipoCuerpoAgua.Enabled = true;
                    CuerpoDeAgua.Enabled = true;
                }
                else
                {
                    Region.Enabled = false;
                    Provincia.Enabled = false;
                    Comuna.Enabled = false;
                    PanelGuardarUbicacionGeografica.Visible = false;
                    TipoCuerpoAgua.Enabled = false;
                    CuerpoDeAgua.Enabled = false;
                }



                // ACCESO A Agrupación de Concesiones
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_AGRUPACION_CONCESIONES"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {

                    BarrioACS.Enabled = true;
                    BarrioACM.Enabled = true;
                    PanelGuardarDatosBarrio.Visible = true;

                }
                else
                {
                    BarrioACS.Enabled = false;
                    BarrioACM.Enabled = false;
                    PanelGuardarDatosBarrio.Visible = false;
                }




                // ACCESO A Otra Definición Geográfica
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_OTRA_DEFINICION"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {

                    CheckBoxListNecesita.Enabled = true;
                    PanelGuardarDatosPlanos.Visible = true;

                }
                else
                {
                    CheckBoxListNecesita.Enabled = false;
                    PanelGuardarDatosPlanos.Visible = false;
                }


                // ACCESO A Referencias Geográficas (Originales)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ORIGINAL"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    NombreCartaAntecedentesEspaciales.ReadOnly = false;
                    NumeroCartaAntEspaciales.ReadOnly = false;
                    EdicionCartaAntEspaciales.ReadOnly = false;
                    AnioCartaAntEspaciales.ReadOnly = false;
                    DatumCartaAntEspaciales.ReadOnly = false;
                    HusoCartaAntEspaciales.ReadOnly = false;
                    SeUsaParaBancoAntEspaciales.Enabled = true;
                    SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = true;
                    //AreaTotalSolicitadaAntecedentesEspeciales.ReadOnly = false;
                    //AreaTotalCalculadaAntecedentesEspeciales.ReadOnly = false;
                    PanelGuardarReferenciasGeograficasAntecedentesEspeciales.Visible = true;
                }
                else
                {
                    NombreCartaAntecedentesEspaciales.ReadOnly = true;
                    NumeroCartaAntEspaciales.ReadOnly = true;
                    EdicionCartaAntEspaciales.ReadOnly = true;
                    AnioCartaAntEspaciales.ReadOnly = true;
                    DatumCartaAntEspaciales.ReadOnly = true;
                    HusoCartaAntEspaciales.ReadOnly = true;
                    SeUsaParaBancoAntEspaciales.Enabled = false;
                    SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    //AreaTotalSolicitadaAntecedentesEspeciales.ReadOnly = true;
                    //AreaTotalCalculadaAntecedentesEspeciales.ReadOnly = true;
                    PanelGuardarReferenciasGeograficasAntecedentesEspeciales.Visible = false;
                }

                // ACCESO A Archivos Adjuntos (Original)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    PanelArchivoAdjuntoOriginal.Visible = true;
                }
                else
                {
                    PanelArchivoAdjuntoOriginal.Visible = false;
                }


                // ACCESO A Polígono (Original)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    TipoConcesionAntecedentesEspaciales.Enabled = true;
                    TipoUsoAntecedentesEspeciales.Enabled = true;
                    ToponimioAntecedentesEspeciales.ReadOnly = false;
                    AreaSolicitadaAntecedentesEspeciales.ReadOnly = false;
                    AreaCalculadaAntecedentesEspeciales.ReadOnly = false;
                    VerticeAntecedentesEspeciales.Enabled = true;
                    LatitudHoraAntecedentesEspeciales.ReadOnly = false;
                    LatitudMinutoAntecedentesEspeciales.ReadOnly = false;
                    LatitudSegundoAntecedentesEspeciales.ReadOnly = false;
                    //LatitudAntecedentesEspeciales.ReadOnly = false;
                    LongitudHoraAntecedentesEspeciales.ReadOnly = false;
                    LongitudMinutoAntecedentesEspeciales.ReadOnly = false;
                    LongitudSegundoAntecedentesEspeciales.ReadOnly = false;
                    //LongitudAntecedentesEspeciales.ReadOnly = false;
                    UTMEAntecedentesEspeciales.ReadOnly = false;
                    UtmNAntecedentesEspeciales.ReadOnly = false;

                    PanelBotonesVerticeAntEspaciales.Visible = true;
                    PanelBotonesPoligonoOriginal.Visible = true;

                }
                else
                {
                    TipoConcesionAntecedentesEspaciales.Enabled = false;
                    TipoUsoAntecedentesEspeciales.Enabled = false;
                    ToponimioAntecedentesEspeciales.ReadOnly = true;
                    AreaSolicitadaAntecedentesEspeciales.ReadOnly = true;
                    AreaCalculadaAntecedentesEspeciales.ReadOnly = true;
                    VerticeAntecedentesEspeciales.Enabled = false;
                    LatitudHoraAntecedentesEspeciales.ReadOnly = true;
                    LatitudMinutoAntecedentesEspeciales.ReadOnly = true;
                    LatitudSegundoAntecedentesEspeciales.ReadOnly = true;
                    //LatitudAntecedentesEspeciales.ReadOnly = true;
                    LongitudHoraAntecedentesEspeciales.ReadOnly = true;
                    LongitudMinutoAntecedentesEspeciales.ReadOnly = true;
                    LongitudSegundoAntecedentesEspeciales.ReadOnly = true;
                    //LongitudAntecedentesEspeciales.ReadOnly = true;
                    UTMEAntecedentesEspeciales.ReadOnly = true;
                    UtmNAntecedentesEspeciales.ReadOnly = true;

                    PanelBotonesVerticeAntEspaciales.Visible = false;
                    PanelBotonesPoligonoOriginal.Visible = false;
                }




                // ACCESO A Referencias Geográficas (Entrega)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_ENTREGA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    DATUMAntTerreno.Enabled = true;
                    HusoHorarioAntecedentesTerreno.Enabled = true;
                    SeUsaParaBancoAntTerreno.Enabled = true;
                    //AreaTotalSolicitadaAntecedentesTerreno.ReadOnly = false;
                    //AreaTotalCalculadaAntecedentesTerreno.ReadOnly = false;
                    PanelGuardarReferenciasGeograficasAntTerreno0.Visible = true;
                }
                else
                {
                    DATUMAntTerreno.Enabled = false;
                    HusoHorarioAntecedentesTerreno.Enabled = false;
                    SeUsaParaBancoAntTerreno.Enabled = false;
                    //AreaTotalSolicitadaAntecedentesTerreno.ReadOnly = true;
                    //AreaTotalCalculadaAntecedentesTerreno.ReadOnly = true;
                    PanelGuardarReferenciasGeograficasAntTerreno0.Visible = false;
                }

                // ACCESO A Archivos Adjuntos (Entrega)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    PanelArchivoAdjuntoMaterial.Visible = true;
                }
                else
                {
                    PanelArchivoAdjuntoMaterial.Visible = false;
                }


                // ACCESO A Polígono (Entrega)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    TipoConcesionAntecedentesTerreno.Enabled = true;
                    TipoUsoAntecedentesTerreno.Enabled = true;
                    ToponimioAntecedentesTerreno.ReadOnly = false;
                    AreaCalculadaAntecedentesTerreno.ReadOnly = false;
                    AreaSolicitadaAntecedentesTerreno.ReadOnly = false;
                    VerticeAntecedentesTerreno.Enabled = true;
                    LatitudHoraAntecedentesTerreno.ReadOnly = false;
                    LatitudMinutoAntecedentesTerreno.ReadOnly = false;
                    LatitudSegundoAntecedentesTerreno.ReadOnly = false;
                    //LatitudDecimalAntecedentesTerreno.ReadOnly = false;
                    LongitudHoraAntecedentesTerreno.ReadOnly = false;
                    LongitudMinutoAntecedentesTerreno.ReadOnly = false;
                    LongitudSegundoAntecedentesTerreno.ReadOnly = false;
                    //LongitudDecimalAntecedentesTerreno.ReadOnly = false;
                    UTMEAntecedentesTerreno.ReadOnly = false;
                    UTMNAntecedentesTerreno.ReadOnly = false;

                    PanelBotonesVerticeAntTerreno.Visible = true;
                    PanelBotonesPoligonoEntrega.Visible = true;

                }
                else
                {
                    TipoConcesionAntecedentesTerreno.Enabled = false;
                    TipoUsoAntecedentesTerreno.Enabled = false;
                    ToponimioAntecedentesTerreno.ReadOnly = true;
                    AreaCalculadaAntecedentesTerreno.ReadOnly = true;
                    AreaSolicitadaAntecedentesTerreno.ReadOnly = true;
                    VerticeAntecedentesTerreno.Enabled = false;
                    LatitudHoraAntecedentesTerreno.ReadOnly = true;
                    LatitudMinutoAntecedentesTerreno.ReadOnly = true;
                    LatitudSegundoAntecedentesTerreno.ReadOnly = true;
                    //LatitudDecimalAntecedentesTerreno.ReadOnly = true;
                    LongitudHoraAntecedentesTerreno.ReadOnly = true;
                    LongitudMinutoAntecedentesTerreno.ReadOnly = true;
                    LongitudSegundoAntecedentesTerreno.ReadOnly = true;
                    //LongitudDecimalAntecedentesTerreno.ReadOnly = true;
                    UTMEAntecedentesTerreno.ReadOnly = true;
                    UTMNAntecedentesTerreno.ReadOnly = true;

                    PanelBotonesVerticeAntTerreno.Visible = false;
                    PanelBotonesPoligonoEntrega.Visible = false;
                }





                // ACCESO A Referencias Geográficas (Regularizacion)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_REFERENCIA_GEOGRAFICA_REGULARIZACION"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    NombreCartaRegul.ReadOnly = false;
                    NumeroCartaRegul.ReadOnly = false;
                    EdicionCartaRegul.ReadOnly = false;
                    AnioCartaRegul.ReadOnly = false;
                    DatumRegul.ReadOnly = false;
                    HusoHorarioRegul.ReadOnly = false;
                    SeUsaParaBancoRegularizacion.Enabled = true;
                    //AreaTotalRegularizacion.ReadOnly = false;
                    PanelGuardarReferenciasGeograficasRegul.Visible = true;
                }
                else
                {
                    NombreCartaRegul.ReadOnly = true;
                    NumeroCartaRegul.ReadOnly = true;
                    EdicionCartaRegul.ReadOnly = true;
                    AnioCartaRegul.ReadOnly = true;
                    DatumRegul.ReadOnly = true;
                    HusoHorarioRegul.ReadOnly = true;
                    SeUsaParaBancoRegularizacion.Enabled = false;
                    //AreaTotalRegularizacion.ReadOnly = true;
                    PanelGuardarReferenciasGeograficasRegul.Visible = false;
                }

                // ACCESO A Archivos Adjuntos (Regularizacion)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    PanelArchivoAdjuntoRegularizacion.Visible = true;
                }
                else
                {
                    PanelArchivoAdjuntoRegularizacion.Visible = false;
                }


                // ACCESO A Polígono (Regularizacion)
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    TipoConsecionRegularizacion.Enabled = true;
                    TipoUsoRegularizacion.Enabled = true;
                    ToponimioRegularizacion.ReadOnly = false;
                    AreaRegularizacion.ReadOnly = false;
                    VerticeRegularizacion.Enabled = true;
                    LatitudHoraRegularizacion.ReadOnly = false;
                    LatitudMinutoRegularizacion.ReadOnly = false;
                    LatitudSegundoRegularizacion.ReadOnly = false;
                    //LatitudDecimalRegularizacion.ReadOnly = false;
                    LongitudHoraRegularizacion.ReadOnly = false;
                    LongitudMinutoRegularizacion.ReadOnly = false;
                    LongitudSegundoRegularizacion.ReadOnly = false;
                    //LongitudDecimalRegularizacion.ReadOnly = false;
                    UTMERegularizacion.ReadOnly = false;
                    UTMNRegularizacion.ReadOnly = false;
                    PanelBotonesVerticesRegul.Visible = true;
                    PanelBotonesPoligonoRegularizacion.Visible = true;


                }
                else
                {
                    TipoConsecionRegularizacion.Enabled = false;
                    TipoUsoRegularizacion.Enabled = false;
                    ToponimioRegularizacion.ReadOnly = true;
                    AreaRegularizacion.ReadOnly = true;
                    VerticeRegularizacion.Enabled = false;
                    LatitudHoraRegularizacion.ReadOnly = true;
                    LatitudMinutoRegularizacion.ReadOnly = true;
                    LatitudSegundoRegularizacion.ReadOnly = true;
                    //LatitudDecimalRegularizacion.ReadOnly = true;
                    LongitudHoraRegularizacion.ReadOnly = true;
                    LongitudMinutoRegularizacion.ReadOnly = true;
                    LongitudSegundoRegularizacion.ReadOnly = true;
                    //LongitudDecimalRegularizacion.ReadOnly = true;
                    UTMERegularizacion.ReadOnly = true;
                    UTMNRegularizacion.ReadOnly = true;
                    PanelBotonesVerticesRegul.Visible = false;
                    PanelBotonesPoligonoRegularizacion.Visible = false;
                }





                //ACCESO A Observaciones
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_OBSERVACIONES"], this.usuario_logeado, solicitudConcesion, rbAccion.EDITAR))
                {
                    observaciones.ReadOnly = false;
                    PanelGuardarObservaciones.Visible = true;
                }
                else
                {
                    observaciones.ReadOnly = true;
                    PanelGuardarObservaciones.Visible = false;
                }



                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA)
                {

                    CheckBoxListNecesita.Items[0].Enabled = true;
                    CheckBoxListNecesita.Items[1].Enabled = true;
                }


                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                {
                    //SECTOR
                    DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudConcesion.idSolConcesion);
                    ViewState["aDetalleSector"] = aDetalleSector;


                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, aDetalleSector.tipoRelocalizacion.id);


                    //Sección Agrupacion de concesiones
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, aDetalleSector.tipoRelocalizacion.id))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, aDetalleSector.tipoRelocalizacion.id);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, aDetalleSector.tipoRelocalizacion.id);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, aDetalleSector.tipoRelocalizacion.id);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, aDetalleSector.tipoRelocalizacion.id);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, aDetalleSector.tipoRelocalizacion.id);



                    //PARTICULARIDADES
                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                    {
                        SeUSaParaBancoOriginales.Visible = false;
                        SeUSaParaBancoMaterial.Visible = false;
                        SeUSaParaBancoRegula.Visible = false;
                    }

                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA)
                    {
                        SeUSaParaBancoOriginales.Visible = true;
                        SeUSaParaBancoMaterial.Visible = false;
                        SeUSaParaBancoRegula.Visible = true;
                    }
                }


                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                {
                    //SECTOR
                    DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudConcesion.idSolConcesion);
                    ViewState["aDetalleSector"] = aDetalleSector;


                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, aDetalleSector.tipoRelocalizacion.id);


                    //Sección Agrupacion de concesiones
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, aDetalleSector.tipoRelocalizacion.id))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Entrega Material
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, aDetalleSector.tipoRelocalizacion.id);
                    //Sección Regularización
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, aDetalleSector.tipoRelocalizacion.id);

                    //PARTICULARIDADES
                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                    {
                        SeUSaParaBancoOriginales.Visible = false;
                        SeUSaParaBancoMaterial.Visible = false;
                        SeUSaParaBancoRegula.Visible = false;
                    }

                    if (aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_CREA_RESA)
                    {
                        SeUSaParaBancoOriginales.Visible = true;
                        SeUSaParaBancoMaterial.Visible = false;
                        SeUSaParaBancoRegula.Visible = true;
                    }
                }



                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO))
                        {
                            //Sección Agrupacion de concesiones
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);



                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;


                }


                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO))
                        {
                            //Sección Agrupacion de concesiones
                            PanelSeccionBarrio.Visible = true;
                        }

                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);


                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        //Sección Agrupacion de conceiones
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        //Sección Agrupacion de conceiones
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        //Sección Agrupacion de conceiones
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);

                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        //Sección Agrupacion de conceiones
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = false;
                    SeUSaParaBancoRegula.Visible = false;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
                {

                    //Sección Ubicación Geográfica
                    PanelUbicacionGeografica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.UBICACION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);

                    DivCuerpoAgua.Visible = false; //LOS COLECTORES NO MUESTAN TIPO CUERPO DE AGUA Y CUERPO AGUA


                    //Sección Agrupacion de concesiones

                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.BARRIO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }


                    //Sección Otra Definición Geografica
                    PanelAdministracionPlanos.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OTRA_DEFINICION_GEOGRAFICA, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                    //Sección Capitania de puerto
                    PanelCapitaniaPuerto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                    //Sección Coordenadas Originales
                    PanelAntecedentesEspeciales.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);

                    //Sección Entrega Material

                    //PanelAntecedentesTerreno.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                    //PanelAntecedentesTerrenoVisibilidad.Visible = PanelAntecedentesTerreno.Visible;
                    //CheckBoxListNecesita.Items[0].Enabled = PanelAntecedentesTerreno.Visible;
                    CheckBoxListNecesita.Items[0].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);

                    //Sección Regularización

                    //PanelRegularizacion.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                    //PanelRegularizacionVisibilidad.Visible = PanelRegularizacion.Visible;
                    //CheckBoxListNecesita.Items[1].Enabled = PanelRegularizacion.Visible;
                    CheckBoxListNecesita.Items[1].Enabled = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);


                    //PARTICULARIDADES
                    SeUSaParaBancoOriginales.Visible = false;
                    SeUSaParaBancoMaterial.Visible = true;
                    SeUSaParaBancoRegula.Visible = true;

                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    /* Ubicación Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UBICACION_GEOGRAFICA))
                    {
                        PanelUbicacionGeografica.Visible = true;
                    }

                    /* Agrupación de Concesiones */
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BARRIO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    /* Otra Definición Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OTRA_DEFINICION_GEOGRAFICA))
                    {
                        PanelAdministracionPlanos.Visible = true;
                    }


                    /* Capitanía de Puerto */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO))
                    {
                        PanelCapitaniaPuerto.Visible = true;
                    }
                    else
                    {
                        PanelCapitaniaPuerto.Visible = false;
                    }

                    /* Pestaña Antecedentes de Terreno */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.ANTECEDENTES_TERRENO))
                    {

                        CheckBoxListNecesita.Items[0].Enabled = true;

                        if (solicitudConcesion.reqAntecTerreno)
                        {
                            CheckBoxListNecesita.Items[0].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[0].Enabled = false;
                    }

                    /* Pestaña Regularización */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.REGULARIZACION))
                    {
                        CheckBoxListNecesita.Items[1].Enabled = true;

                        if (solicitudConcesion.reqRegularizacion)
                        {
                            CheckBoxListNecesita.Items[1].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[1].Enabled = false;
                    }

                    if (!CheckBoxListNecesita.Items[0].Enabled && !CheckBoxListNecesita.Items[1].Enabled)
                    {
                        GuardarDatosPlanos.Visible = false;
                    }

                    /* Observaciones */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_DEL_SECTOR))
                    {
                        PanelObservacionesGenerales.Visible = true;
                    }

                    /* Opción se Usa para Banco en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaBancoAntEspaciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaBancoAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaBancoRegularizacion.Enabled = false;
                    }

                    /* Opción se Usa para Visualizador de Mapas en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaVisualizadorDeMapasAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaVisualizadorDeMapasRegularizacion.Enabled = false;
                    }
                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    /* Ubicación Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UBICACION_GEOGRAFICA))
                    {
                        PanelUbicacionGeografica.Visible = true;
                    }

                    /* Agrupación de Concesiones */
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BARRIO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    /* Otra Definición Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OTRA_DEFINICION_GEOGRAFICA))
                    {
                        PanelAdministracionPlanos.Visible = true;
                    }


                    /* Capitanía de Puerto */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO))
                    {
                        PanelCapitaniaPuerto.Visible = true;
                    }
                    else
                    {
                        PanelCapitaniaPuerto.Visible = false;
                    }

                    /* Pestaña Antecedentes de Terreno */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.ANTECEDENTES_TERRENO))
                    {

                        CheckBoxListNecesita.Items[0].Enabled = true;

                        if (solicitudConcesion.reqAntecTerreno)
                        {
                            CheckBoxListNecesita.Items[0].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[0].Enabled = false;
                    }

                    /* Pestaña Regularización */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.REGULARIZACION))
                    {
                        CheckBoxListNecesita.Items[1].Enabled = true;

                        if (solicitudConcesion.reqRegularizacion)
                        {
                            CheckBoxListNecesita.Items[1].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[1].Enabled = false;
                    }

                    /* Observaciones */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_DEL_SECTOR))
                    {
                        PanelObservacionesGenerales.Visible = true;
                    }

                    /* Opción se Usa para Banco en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaBancoAntEspaciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaBancoAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaBancoRegularizacion.Enabled = false;
                    }

                    /* Opción se Usa para Visualizador de Mapas en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaVisualizadorDeMapasAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaVisualizadorDeMapasRegularizacion.Enabled = false;
                    }
                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    /* Ubicación Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UBICACION_GEOGRAFICA))
                    {
                        PanelUbicacionGeografica.Visible = true;
                    }

                    /* Agrupación de Concesiones */
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BARRIO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    /* Otra Definición Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OTRA_DEFINICION_GEOGRAFICA))
                    {
                        PanelAdministracionPlanos.Visible = true;
                    }


                    /* Capitanía de Puerto */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO))
                    {
                        PanelCapitaniaPuerto.Visible = true;
                    }
                    else
                    {
                        PanelCapitaniaPuerto.Visible = false;
                    }

                    /* Pestaña Antecedentes de Terreno */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.ANTECEDENTES_TERRENO))
                    {

                        CheckBoxListNecesita.Items[0].Enabled = true;

                        if (solicitudConcesion.reqAntecTerreno)
                        {
                            CheckBoxListNecesita.Items[0].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[0].Enabled = false;
                    }

                    /* Pestaña Regularización */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.REGULARIZACION))
                    {
                        CheckBoxListNecesita.Items[1].Enabled = true;

                        if (solicitudConcesion.reqRegularizacion)
                        {
                            CheckBoxListNecesita.Items[1].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[1].Enabled = false;
                    }

                    /* Observaciones */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_DEL_SECTOR))
                    {
                        PanelObservacionesGenerales.Visible = true;
                    }

                    /* Opción se Usa para Banco en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaBancoAntEspaciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaBancoAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaBancoRegularizacion.Enabled = false;
                    }

                    /* Opción se Usa para Visualizador de Mapas en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaVisualizadorDeMapasAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaVisualizadorDeMapasRegularizacion.Enabled = false;
                    }
                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    /* Ubicación Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UBICACION_GEOGRAFICA))
                    {
                        PanelUbicacionGeografica.Visible = true;
                    }

                    /* Agrupación de Concesiones */
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BARRIO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    /* Otra Definición Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OTRA_DEFINICION_GEOGRAFICA))
                    {
                        PanelAdministracionPlanos.Visible = true;
                    }


                    /* Capitanía de Puerto */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO))
                    {
                        PanelCapitaniaPuerto.Visible = true;
                    }
                    else
                    {
                        PanelCapitaniaPuerto.Visible = false;
                    }

                    /* Pestaña Antecedentes de Terreno */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.ANTECEDENTES_TERRENO))
                    {

                        CheckBoxListNecesita.Items[0].Enabled = true;

                        if (solicitudConcesion.reqAntecTerreno)
                        {
                            CheckBoxListNecesita.Items[0].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[0].Enabled = false;
                    }

                    /* Pestaña Regularización */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.REGULARIZACION))
                    {
                        CheckBoxListNecesita.Items[1].Enabled = true;

                        if (solicitudConcesion.reqRegularizacion)
                        {
                            CheckBoxListNecesita.Items[1].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[1].Enabled = false;
                    }

                    /* Observaciones */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_DEL_SECTOR))
                    {
                        PanelObservacionesGenerales.Visible = true;
                    }

                    /* Opción se Usa para Banco en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaBancoAntEspaciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaBancoAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaBancoRegularizacion.Enabled = false;
                    }

                    /* Opción se Usa para Visualizador de Mapas en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaVisualizadorDeMapasAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaVisualizadorDeMapasRegularizacion.Enabled = false;
                    }
                }

                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                {

                    List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.ANTECEDENTES_DEL_SECTOR_MODIFICACION, 0);

                    /* Ubicación Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.UBICACION_GEOGRAFICA))
                    {
                        PanelUbicacionGeografica.Visible = true;
                    }

                    /* Agrupación de Concesiones */
                    if (antecedentesSectorService.tieneEspecieSolicitudConcesion(solicitudConcesion, rbTipo.SALMONIDOS, rbTipo.MITILIDOS))
                    {
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.BARRIO))
                        {
                            PanelSeccionBarrio.Visible = true;
                        }
                    }

                    /* Otra Definición Geográfica */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OTRA_DEFINICION_GEOGRAFICA))
                    {
                        PanelAdministracionPlanos.Visible = true;
                    }


                    /* Capitanía de Puerto */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.ANTECEDENTES_CAPITANIA_PUERTO))
                    {
                        PanelCapitaniaPuerto.Visible = true;
                    }
                    else
                    {
                        PanelCapitaniaPuerto.Visible = false;
                    }

                    /* Pestaña Antecedentes de Terreno */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.ANTECEDENTES_TERRENO))
                    {

                        CheckBoxListNecesita.Items[0].Enabled = true;

                        if (solicitudConcesion.reqAntecTerreno)
                        {
                            CheckBoxListNecesita.Items[0].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[0].Enabled = false;
                    }

                    /* Pestaña Regularización */
                    if (solicitudModificacionService.debeDesplegarPestania(solicitudConcesion.tipoModificacionesTram, despliegueMenuSeccionList, rbTipo.REGULARIZACION))
                    {
                        CheckBoxListNecesita.Items[1].Enabled = true;

                        if (solicitudConcesion.reqRegularizacion)
                        {
                            CheckBoxListNecesita.Items[1].Selected = true;
                        }

                    }
                    else
                    {
                        CheckBoxListNecesita.Items[1].Enabled = false;
                    }

                    /* Observaciones */
                    if (solicitudModificacionService.despligueSeccionesMenu(solicitudConcesion, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_ANTECEDENTES_DEL_SECTOR))
                    {
                        PanelObservacionesGenerales.Visible = true;
                    }

                    /* Opción se Usa para Banco en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaBancoAntEspaciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaBancoAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Banco", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaBancoRegularizacion.Enabled = false;
                    }

                    /* Opción se Usa para Visualizador de Mapas en las pestañas */
                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_ESPACIALES))
                    {
                        SeUsaParaVisualizadorDeMapasAntEspeciales.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.ANTECEDENTES_TERRENO))
                    {
                        SeUsaParaVisualizadorDeMapasAntTerreno.Enabled = false;
                    }

                    if (!solicitudModificacionService.debeDesplegarCampo(solicitudConcesion.tipoModificacionesTram, "Se usa para Visualizador de Mapas", rbTipo.REGULARIZACION))
                    {
                        SeUsaParaVisualizadorDeMapasRegularizacion.Enabled = false;
                    }
                }


                //REGLAS POR TIPO DE MODIFICACION VS QUE SE PUEDE MODIFICAR (PASO 2)
                solicitudConcesion.tieneAsignadaSolicitud = tieneAsignadaSolicitudAux;

            }
            else
            {
                eliminaMensajesError();
            }

        }



        protected void Initialize_Form()
        {
            Initialize_Comboboxs();
            Initialize_formulario();
        }



        /*
         * Cada vez que la página se carga, este código borra cualquier mensaje 
         * que pueda haber quedado de una devolución (postback) anterior.
         */
        protected void eliminaMensajesError()
        {
            Content_msgGrillaGral_1.Visible = false;
            msgGrillaGral_1.Text = "";
            //Ico_msgGrillaGral_1.Visible = false;

            Content_msgGrillaGral_2.Visible = false;
            msgGrillaGral_2.Text = "";
            //IcoGral_2.Visible = false;

            Content_msgGrillaGral_3.Visible = false;
            msgGrillaGral_3.Text = "";
            //IcoGrillaGral_3.Visible = false;

            Content_msgGrillaGral_4.Visible = false;
            msgGrillaGral_4.Text = "";
            //IcoGrillaGral_3.Visible = false;
        }



        #region DESPLIEGUE Y CARGA

        /**
        * Método que carga las distintas secciones del formulario de antecedentes del sector. 
        */
        private void Initialize_formulario()
        {
            SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);

                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneSolicitudConcesion(Convert.ToInt32(IdSolicitud.Value), ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (solicitudConcesion != null)
                {
                    despliegaUbicacionGeografica(solicitudConcesion);
                    despliegaBarrio(solicitudConcesion);
                    despliegaAdministracionPlanos(solicitudConcesion);
                    despliegaCapitaniaPuerto();
                    despliegaPestanias(solicitudConcesion);
                    despliegaObservaciones(solicitudConcesion);

                }
                else
                {
                    Response.Redirect(ViewState["URL_ERROR"].ToString());
                }
            }
            else
            {
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }
        }



        /**
         * Método que despliega en el formulario la sección Ubicación Geográfica.
         */
        private void despliegaUbicacionGeografica(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {
                if (solicitudConcesion.region != null && solicitudConcesion.region.id > 0)
                {

                    Region.SelectedValue = Convert.ToString(solicitudConcesion.region.id);

                    Provincia.Items.Clear();
                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    DataTable data = parametroGenericoDA.ListarProvinciaRegDataTable(0, Convert.ToInt32(Region.SelectedValue));

                    if (data != null)
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            Provincia.Items.Add(new ListItem(Convert.ToString(row["Provincia"]), Convert.ToString(row["IdProvincia"])));
                        }
                    }

                    Provincia.DataBind();
                    Provincia.SelectedValue = Convert.ToString(solicitudConcesion.provincia.id);

                    Comuna.Items.Clear();
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    data = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincia.SelectedValue));
                    if (data != null)
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            Comuna.Items.Add(new ListItem(Convert.ToString(row["Comuna"]), Convert.ToString(row["IdComuna"])));
                        }
                    }

                    Comuna.DataBind();

                    foreach (ListItem listItem in Comuna.Items)
                    {
                        foreach (ParametroGenerico comuna in solicitudConcesion.comuna)
                        {
                            if (comuna != null && listItem.Value.Equals(Convert.ToString(comuna.id)))
                            {
                                listItem.Selected = true;
                            }
                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }
                    }

                    /* Cuerpo de Agua */

                    if (solicitudConcesion.cuerpoAgua != null && solicitudConcesion.cuerpoAgua.tipoCuerpoAgua != null && solicitudConcesion.cuerpoAgua.tipoCuerpoAgua.id > 0)
                    {
                        TipoCuerpoAgua.SelectedValue = Convert.ToString(solicitudConcesion.cuerpoAgua.tipoCuerpoAgua.id);
                    }

                    CuerpoDeAgua.Items.Clear();
                    CuerpoDeAgua.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));


                    CuerpoDeAgua cuerpoAguaFiltro = new Datos.Entidades.CuerpoDeAgua();
                    cuerpoAguaFiltro.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(CuerpoDeAgua.SelectedValue));

                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Datos.Entidades.Region region = new Datos.Entidades.Region();
                        region.id_region = Convert.ToInt32(Region.SelectedValue);
                        cuerpoAguaFiltro.region = region;
                    }

                    if (Comuna.Items != null)
                    {
                        foreach (ListItem item in Comuna.Items)
                        {
                            if (item.Selected && Convert.ToInt32(item.Value) > 0)
                            {

                                if (cuerpoAguaFiltro.comunaCad == null || cuerpoAguaFiltro.comunaCad.Trim().Equals(""))
                                {
                                    cuerpoAguaFiltro.comunaCad = item.Value;
                                }
                                else
                                {
                                    cuerpoAguaFiltro.comunaCad = cuerpoAguaFiltro.comunaCad + "," + item.Value;
                                }
                            }
                        }
                    }



                    List<CuerpoDeAgua> cuerpoDeAguaList = mantenedorDA.ListarCuerpoAgua_Formulario(cuerpoAguaFiltro);

                    if (cuerpoDeAguaList != null)
                    {
                        foreach (CuerpoDeAgua cuerpo in cuerpoDeAguaList)
                        {
                            CuerpoDeAgua.Items.Add(new ListItem(Convert.ToString(cuerpo.nombreCuerpoAgua), Convert.ToString(cuerpo.idCuerpoDeAgua)));
                        }
                    }

                    CuerpoDeAgua.DataBind();

                    if (solicitudConcesion.cuerpoAgua != null && solicitudConcesion.cuerpoAgua.idCuerpoDeAgua > 0)
                    {
                        CuerpoDeAgua.SelectedValue = Convert.ToString(solicitudConcesion.cuerpoAgua.idCuerpoDeAgua);
                    }

                    /*
                    foreach (ParametroGenerico comuna in solicitudConcesion.comuna)
                    {
                        Comuna.SelectedValue = Convert.ToString(comuna.id);
                        Comuna.SelectedItem.Attributes.Add("style", "background-color:#316AC5; color: white;");
                    }
                    */
                }
            }
        }


        /**
        * Método que despliega en el formulario la sección Barrio.
        */
        private void despliegaBarrio(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {

                /* Campo Barrio */

                BarrioACS.Items.Clear();

                BarrioACS.SelectedValue = null;

                BarrioACS.Items.Add(new ListItem(Convert.ToString("-- Seleccione --"), Convert.ToString("-1")));

                DataTable barrio = (DataTable)barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACS, 0, Convert.ToInt32(Region.SelectedValue), 0);
                if (barrio != null)
                {
                    foreach (DataRow row in barrio.Rows)
                    {
                        BarrioACS.Items.Add(new ListItem(Convert.ToString(row["Barrio"]), Convert.ToString(row["IdBarrio"])));
                    }

                }
                BarrioACS.DataBind();

                if (solicitudConcesion.barrio != null && solicitudConcesion.barrio.id_barrio > 0)
                {
                    try
                    {
                        BarrioACS.SelectedValue = Convert.ToString(solicitudConcesion.barrio.id_barrio);
                    }
                    catch
                    { }
                }
                else
                {
                    BarrioACS.SelectedValue = "-1";
                }


                /* Campo Barrio ACM */

                BarrioACM.Items.Clear();

                BarrioACM.SelectedValue = null;

                BarrioACM.Items.Add(new ListItem(Convert.ToString("-- Seleccione --"), Convert.ToString("-1")));

                barrio = (DataTable)barrioDA.obtenerBarrio(rbTipo.TIPO_BARRIO_ACM, 0, Convert.ToInt32(Region.SelectedValue), 0);
                if (barrio != null)
                {
                    foreach (DataRow row in barrio.Rows)
                    {
                        BarrioACM.Items.Add(new ListItem(Convert.ToString(row["Barrio"]), Convert.ToString(row["IdBarrio"])));
                    }

                }
                BarrioACM.DataBind();

                if (solicitudConcesion.acm != null && solicitudConcesion.acm.id_barrio > 0)
                {

                    try
                    {
                        BarrioACM.SelectedValue = Convert.ToString(solicitudConcesion.acm.id_barrio);
                    }
                    catch
                    { }
                }
                else
                {
                    BarrioACM.SelectedValue = "-1";
                }

                PanelSeccionBarrio.Visible = true;
                UpdatePanelDatosBarrio.Update();
                UpdatePanelBarrioACM.Update();


            }
        }


        /**
        *  Método que despliega la sección de Administración de Planos
        *  del formulario de Ant. del Sector.
        */
        private void despliegaAdministracionPlanos(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {
                if (solicitudConcesion.reqAntecTerreno)
                {
                    CheckBoxListNecesita.Items[0].Selected = true;
                }

                if (solicitudConcesion.reqRegularizacion)
                {
                    CheckBoxListNecesita.Items[1].Selected = true;
                }
            }
        }


        /**
        * Método que despliega la sección del formulario llamada Capitanía de Puerto. 
        */
        private void despliegaCapitaniaPuerto()
        {
            PanelCapitaniaPuerto.Visible = true;
            UpdatePanelCapitaniaPuerto.Update();
        }


        /**
        * Método que despliega en el formulario las pestañas y su contenido.
        */
        private void despliegaPestanias(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {
                Carga_CamposPestania("AntecedentesEspacial");

                if (solicitudConcesion.reqAntecTerreno)
                {
                    /* Se obtienen los datos de la Pestaña Antecedentes de Terreno */
                    PanelAntecedentesTerrenoVisibilidad.Visible = true;
                    Carga_CamposPestania("AntecedentesTerreno");
                }
                else
                {
                    PanelAntecedentesTerrenoVisibilidad.Visible = false;
                    PanelAntecedentesTerreno.Visible = false;
                }

                if (solicitudConcesion.reqRegularizacion)
                {
                    /* Se obtienen los datos de la Pestaña Regularización */
                    PanelRegularizacionVisibilidad.Visible = true;
                    Carga_CamposPestania("Regularizacion");
                }
                else
                {
                    PanelRegularizacionVisibilidad.Visible = false;
                    PanelRegularizacion.Visible = false;
                }

                lnk_AntecedentesEspaciales.CssClass = "tab1_selected";
                UpdatePanelAntecedentesEspeciales.Update();

                lnk_AntecedentesTerreno.CssClass = "tab2";
                UpdatePanelAntecedentesTerreno.Update();

                lnk_Regularizacion.CssClass = "tab3";
                UpdatePanelRegularizacion.Update();

                PanelAntecedentesEspeciales.Visible = true;
                updPestanaAntecedentesEspaciales.Update();
            }
        }


        /**
        * Método que despliega en el formulario las observaciones de Ant. del Sector. 
        */
        private void despliegaObservaciones(SolicitudConcesion solicitudConcesion)
        {
            if (solicitudConcesion != null)
            {
                ObsPestaniaInforme obsPestaniaInforme = obsPestaniaInformeDA.ObtieneObsPestaniaInforme(solicitudConcesion.idSolConcesion, rbTipo.ANTEC_SECTOR);

                if (obsPestaniaInforme != null)
                {
                    observaciones.Text = Convert.ToString(obsPestaniaInforme.observaciones);
                }
            }
        }


        /**
        * Método que carga los campos de cada una pestañas. 
        */
        private void Carga_CamposPestania(string pestania)
        {
            List<CoordenadaGeografica> listaCoordenadas = coordenadaGeograficaDA.ListarCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), 0);

            switch (pestania)
            {
                case "AntecedentesEspacial":

                    if (listaCoordenadas != null && listaCoordenadas.Count > 0)
                    {
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_ESPACIALES)
                            {
                                IdCoordenadaGeoAntEspaciales.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);


                                IdCartaAntecedentesEspaciales.Value = Convert.ToString(coordenadaGeografica.carta.idCarta);
                                NombreCartaAntecedentesEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.descripcionCarta);
                                NumeroCartaAntEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.numeroCarta);
                                EdicionCartaAntEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.numeroEdicion);
                                AnioCartaAntEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.anioEdicion);

                                if (coordenadaGeografica.aplicaBanco)
                                {
                                    SeUsaParaBancoAntEspaciales.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaBancoAntEspaciales.Checked = false;
                                }

                                if (coordenadaGeografica.aplicaVisualizadorDeMapas)
                                {
                                    SeUsaParaVisualizadorDeMapasAntEspeciales.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaVisualizadorDeMapasAntEspeciales.Checked = false;
                                }

                                if (coordenadaGeografica.carta != null && coordenadaGeografica.carta.datum != null)
                                {
                                    IdDatumCartaAntEspaciales.Value = Convert.ToString(coordenadaGeografica.carta.datum.id);
                                    DatumCartaAntEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.datum.descripcion);
                                }
                                else
                                {
                                    IdDatumCartaAntEspaciales.Value = Convert.ToString("0");
                                }

                                if (coordenadaGeografica.carta != null && coordenadaGeografica.carta.huso != null)
                                {
                                    HusoCartaAntEspaciales.Text = Convert.ToString(coordenadaGeografica.carta.huso.descripcion);
                                    IdHusoCartaAntEspaciales.Value = Convert.ToString(coordenadaGeografica.carta.huso.id);
                                }
                                else
                                {
                                    IdHusoCartaAntEspaciales.Value = Convert.ToString("0");
                                }

                                AreaTotalCalculadaAntecedentesEspeciales.Text = Convert.ToString(coordenadaGeografica.areaTotalCalculada);
                                AreaTotalSolicitadaAntecedentesEspeciales.Text = Convert.ToString(coordenadaGeografica.areaTotalSolicitada);

                                if (!IdCoordenadaGeoAntEspaciales.Value.Equals("") && Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value) > 0)
                                {
                                    List<Poligono> listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);

                                    GridPoligonosAntecedentesEspaciales.DataSource = listaPoligonos;
                                    GridPoligonosAntecedentesEspaciales.DataBind();
                                    GridPoligonosAntecedentesEspaciales.Visible = true;

                                    List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                                    GridArchivoAdjuntoAntEspacial.DataSource = listaArchivoBinario;
                                    GridArchivoAdjuntoAntEspacial.DataBind();
                                    GridArchivoAdjuntoAntEspacial.Visible = true;

                                }
                                break;
                            }
                            else
                            {
                                AreaTotalCalculadaAntecedentesEspeciales.Text = Convert.ToString("0");
                                AreaTotalSolicitadaAntecedentesEspeciales.Text = Convert.ToString("0");
                                IdCoordenadaGeoAntEspaciales.Value = Convert.ToString("0");
                            }
                        }
                    }
                    else
                    {
                        AreaTotalCalculadaAntecedentesEspeciales.Text = Convert.ToString("0");
                        AreaTotalSolicitadaAntecedentesEspeciales.Text = Convert.ToString("0");
                        IdCoordenadaGeoAntEspaciales.Value = Convert.ToString("0");
                    }

                    break;

                case "AntecedentesTerreno":

                    if (listaCoordenadas != null && listaCoordenadas.Count > 0)
                    {
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO)
                            {
                                IdCoordenadaGeoAntTerreno.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);

                                DATUMAntTerreno.SelectedValue = Convert.ToString(coordenadaGeografica.datum.id);

                                if (coordenadaGeografica.tipoHuso != null)
                                {
                                    HusoHorarioAntecedentesTerreno.SelectedValue = Convert.ToString(coordenadaGeografica.tipoHuso.id);
                                }
                                
                                if (coordenadaGeografica.aplicaBanco)
                                {
                                    SeUsaParaBancoAntTerreno.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaBancoAntTerreno.Checked = false;
                                }

                                if (coordenadaGeografica.aplicaVisualizadorDeMapas)
                                {
                                    SeUsaParaVisualizadorDeMapasAntTerreno.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaVisualizadorDeMapasAntTerreno.Checked = false;
                                }

                                AreaTotalCalculadaAntecedentesTerreno.Text = Convert.ToString(coordenadaGeografica.areaTotalCalculada);
                                AreaTotalSolicitadaAntecedentesTerreno.Text = Convert.ToString(coordenadaGeografica.areaTotalSolicitada);

                                if (!IdCoordenadaGeoAntTerreno.Value.Equals("") && Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value) > 0)
                                {
                                    List<Poligono> listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);

                                    GridPoligonoAntecedentesTerreno.DataSource = listaPoligonos;
                                    GridPoligonoAntecedentesTerreno.DataBind();
                                    GridPoligonoAntecedentesTerreno.Visible = true;

                                    List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                                    GridArchivoAdjuntoAntTerreno.DataSource = listaArchivoBinario;
                                    GridArchivoAdjuntoAntTerreno.DataBind();
                                    GridArchivoAdjuntoAntTerreno.Visible = true;

                                }
                                break;
                            }
                            else
                            {
                                AreaTotalCalculadaAntecedentesTerreno.Text = Convert.ToString("0");
                                AreaTotalSolicitadaAntecedentesTerreno.Text = Convert.ToString("0");
                                IdCoordenadaGeoAntTerreno.Value = Convert.ToString("0");
                            }
                        }
                    }
                    else
                    {
                        AreaTotalCalculadaAntecedentesTerreno.Text = Convert.ToString("0");
                        AreaTotalSolicitadaAntecedentesTerreno.Text = Convert.ToString("0");
                        IdCoordenadaGeoAntTerreno.Value = Convert.ToString("0");
                    }

                    break;

                case "Regularizacion":

                    if (listaCoordenadas != null && listaCoordenadas.Count > 0)
                    {
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.REGULARIZACION)
                            {
                                IdReferenciaGeograficaRegularizacion.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);


                                IdCartaRegul.Value = Convert.ToString(coordenadaGeografica.carta.idCarta);
                                NombreCartaRegul.Text = Convert.ToString(coordenadaGeografica.carta.descripcionCarta);
                                NumeroCartaRegul.Text = Convert.ToString(coordenadaGeografica.carta.numeroCarta);
                                EdicionCartaRegul.Text = Convert.ToString(coordenadaGeografica.carta.numeroEdicion);
                                AnioCartaRegul.Text = Convert.ToString(coordenadaGeografica.carta.anioEdicion);

                                if (coordenadaGeografica.aplicaBanco)
                                {
                                    SeUsaParaBancoRegularizacion.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaBancoRegularizacion.Checked = false;
                                }

                                if (coordenadaGeografica.aplicaVisualizadorDeMapas)
                                {
                                    SeUsaParaVisualizadorDeMapasRegularizacion.Checked = true;
                                }
                                else
                                {
                                    SeUsaParaVisualizadorDeMapasRegularizacion.Checked = false;
                                }

                                if (coordenadaGeografica.carta != null && coordenadaGeografica.carta.datum != null)
                                {
                                    IdDatumRegul.Value = Convert.ToString(coordenadaGeografica.carta.datum.id);
                                    DatumRegul.Text = Convert.ToString(coordenadaGeografica.carta.datum.descripcion);
                                }
                                else
                                {
                                    IdDatumRegul.Value = Convert.ToString("0");
                                }

                                if (coordenadaGeografica.carta != null && coordenadaGeografica.carta.huso != null)
                                {
                                    HusoHorarioRegul.Text = Convert.ToString(coordenadaGeografica.carta.huso.descripcion);
                                    IdHusoHorarioRegul.Value = Convert.ToString(coordenadaGeografica.carta.huso.id);
                                }
                                else
                                {
                                    IdHusoHorarioRegul.Value = Convert.ToString("0");
                                }

                                AreaTotalRegularizacion.Text = Convert.ToString(coordenadaGeografica.areaTotalRegularizacion);

                                if (!IdReferenciaGeograficaRegularizacion.Value.Equals("") && Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value) > 0)
                                {
                                    List<Poligono> listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);

                                    GridPoligonoRegul.DataSource = listaPoligonos;
                                    GridPoligonoRegul.DataBind();
                                    GridPoligonoRegul.Visible = true;

                                    List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                                    GridArchivoAdjuntoRegularizacion.DataSource = listaArchivoBinario;
                                    GridArchivoAdjuntoRegularizacion.DataBind();
                                    GridArchivoAdjuntoRegularizacion.Visible = true;
                                }
                                break;
                            }
                            else
                            {
                                AreaTotalRegularizacion.Text = Convert.ToString("0");
                                IdReferenciaGeograficaRegularizacion.Value = Convert.ToString("0");
                            }
                        }
                    }
                    else
                    {
                        AreaTotalRegularizacion.Text = Convert.ToString("0");
                        IdReferenciaGeograficaRegularizacion.Value = Convert.ToString("0");
                    }
                    break;
            }
        }


        #endregion



        #region CARGA COMBOBOX

        /**
         * Método que inicializa los combobox del formulario. 
         */
        protected void Initialize_Comboboxs()
        {
            Carga_Combobox("Region");
            Region.SelectedValue = "-1";

            Carga_Combobox("Provincia");
            Provincia.SelectedValue = "-1";

            Carga_Combobox("Comuna");
            Comuna.SelectedValue = "-1";

            Carga_Combobox("Provincia");
            Provincia.SelectedValue = "-1";

            Carga_Combobox("TipoCuerpoAgua");
            TipoCuerpoAgua.SelectedValue = "-1";

            Carga_Combobox("CuerpoDeAgua");
            CuerpoDeAgua.SelectedValue = "-1";

            Carga_Combobox("TipoUsoAntecedentesEspeciales");
            TipoUsoAntecedentesEspeciales.SelectedValue = "-1";

            Carga_Combobox("VerticeAntecedentesEspeciales");
            VerticeAntecedentesEspeciales.SelectedValue = "-1";

            Carga_Combobox("TipoConcesionAntecedentesEspaciales");
            TipoConcesionAntecedentesEspaciales.SelectedValue = "-1";

            Carga_Combobox("HusoHorarioAntecedentesTerreno");
            HusoHorarioAntecedentesTerreno.SelectedValue = "-1";

            Carga_Combobox("TipoUsoAntecedentesTerreno");
            TipoUsoAntecedentesTerreno.SelectedValue = "-1";

            Carga_Combobox("VerticeAntecedentesTerreno");
            VerticeAntecedentesTerreno.SelectedValue = "-1";

            Carga_Combobox("TipoConcesionAntecedentesTerreno");
            TipoConcesionAntecedentesTerreno.SelectedValue = "-1";

            Carga_Combobox("DATUMAntTerreno");
            DATUMAntTerreno.SelectedValue = "-1";

            Carga_Combobox("TipoUsoRegularizacion");
            TipoUsoRegularizacion.SelectedValue = "-1";

            Carga_Combobox("VerticeRegularizacion");
            VerticeRegularizacion.SelectedValue = "-1";

            Carga_Combobox("TipoConsecionRegularizacion");
            TipoConsecionRegularizacion.SelectedValue = "-1";

            Carga_Combobox("TipoArchivoAntEspaciales");
            TipoArchivoAntEspaciales.SelectedValue = "-1";

            Carga_Combobox("TipoArchivoAntTerreno");
            TipoArchivoAntTerreno.SelectedValue = "-1";

            Carga_Combobox("TipoArchivoRegularizacion");
            TipoArchivoRegularizacion.SelectedValue = "-1";
        }

        /**
         * Método que carga los combobox del formulario completo. 
         */
        protected void Carga_Combobox(string combobox)
        {
            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            switch (combobox)
            {
                case "Region":
                    // Cargamos el combobox: Regiones
                    Region.Items.Clear();
                    Region.DataSource = regionDA.ListarRegion(0);
                    Region.DataTextField = "Region";
                    Region.DataValueField = "IdRegion";
                    Region.DataBind();
                    Region.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Provincia":
                    // Cargamos el combobox: Provincia
                    Provincia.Items.Clear();
                    if (Convert.ToInt32(Region.SelectedValue) > 0)
                    {
                        Provincia.DataSource = parametroGenericoDA.ListarProvinciaReg(0, Convert.ToInt32(Region.SelectedValue));
                        Provincia.DataTextField = "descripcion";
                        Provincia.DataValueField = "id";
                        Provincia.DataBind();
                    };
                    Provincia.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "Comuna":
                    // Cargamos el combobox: Comuna
                    Comuna.Items.Clear();
                    if (Convert.ToInt32(Provincia.SelectedValue) > 0)
                    {
                        Comuna.DataSource = parametroGenericoDA.ListarComunaDataTable(0, Convert.ToInt32(Provincia.SelectedValue));
                        Comuna.DataTextField = "Comuna";
                        Comuna.DataValueField = "IdComuna";
                        Comuna.DataBind();
                    };
                    Comuna.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                /* Cuerpo de Agua */
                case "TipoCuerpoAgua":
                    // Cargamos el combobox: TipoCuerpoAgua
                    TipoCuerpoAgua.Items.Clear();
                    TipoCuerpoAgua.DataSource = tipoDa.ListarTipo("TIPO_CUERPO_AGUA");
                    TipoCuerpoAgua.DataTextField = "descripcion";
                    TipoCuerpoAgua.DataValueField = "id";
                    TipoCuerpoAgua.DataBind();
                    TipoCuerpoAgua.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "CuerpoDeAgua":
                    // Cargamos el combobox: Tipo Barrio
                    CuerpoDeAgua.Items.Clear();
                    if (Convert.ToInt32(TipoCuerpoAgua.SelectedValue) > 0)
                    {
                        CuerpoDeAgua cuerpoAguaFiltro = new Datos.Entidades.CuerpoDeAgua();
                        cuerpoAguaFiltro.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(TipoCuerpoAgua.SelectedValue));



                        if (Convert.ToInt32(Region.SelectedValue) > 0)
                        {
                            Datos.Entidades.Region region = new Datos.Entidades.Region();
                            region.id_region = Convert.ToInt32(Region.SelectedValue);
                            cuerpoAguaFiltro.region = region;
                        }


                        if (Comuna.Items != null)
                        {
                            foreach (ListItem item in Comuna.Items)
                            {
                                if (item.Selected && Convert.ToInt32(item.Value) > 0)
                                {

                                    if (cuerpoAguaFiltro.comunaCad == null || cuerpoAguaFiltro.comunaCad.Trim().Equals(""))
                                    {
                                        cuerpoAguaFiltro.comunaCad = item.Value;
                                    }
                                    else
                                    {
                                        cuerpoAguaFiltro.comunaCad = cuerpoAguaFiltro.comunaCad + "," + item.Value;
                                    }
                                }
                            }
                        }


                        CuerpoDeAgua.DataSource = mantenedorDA.ListarCuerpoAgua_Formulario(cuerpoAguaFiltro);
                        CuerpoDeAgua.DataTextField = "nombreCuerpoAgua";
                        CuerpoDeAgua.DataValueField = "idCuerpoDeAgua";
                        CuerpoDeAgua.DataBind();
                    }
                    CuerpoDeAgua.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoUsoAntecedentesEspeciales":
                    TipoUsoAntecedentesEspeciales.Items.Clear();
                    TipoUsoAntecedentesEspeciales.DataSource = tipoDa.ListarTipo("TIPO_USO");
                    TipoUsoAntecedentesEspeciales.DataTextField = "descripcion";
                    TipoUsoAntecedentesEspeciales.DataValueField = "id";
                    TipoUsoAntecedentesEspeciales.DataBind();
                    TipoUsoAntecedentesEspeciales.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "VerticeAntecedentesEspeciales":
                    VerticeAntecedentesEspeciales.Items.Clear();
                    VerticeAntecedentesEspeciales.DataSource = tipoDa.ListarTipo("TIPO_VERTICE");
                    VerticeAntecedentesEspeciales.DataTextField = "descripcion";
                    VerticeAntecedentesEspeciales.DataValueField = "id";
                    VerticeAntecedentesEspeciales.DataBind();
                    VerticeAntecedentesEspeciales.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoConcesionAntecedentesEspaciales":
                    TipoConcesionAntecedentesEspaciales.Items.Clear();
                    TipoConcesionAntecedentesEspaciales.DataSource = tipoConcesionDA.obtenerTipoConcesion(0);
                    TipoConcesionAntecedentesEspaciales.DataTextField = "TipoConcesion";
                    TipoConcesionAntecedentesEspaciales.DataValueField = "IdTipoConcesion";
                    TipoConcesionAntecedentesEspaciales.DataBind();
                    TipoConcesionAntecedentesEspaciales.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "HusoHorarioAntecedentesTerreno":
                    HusoHorarioAntecedentesTerreno.Items.Clear();
                    HusoHorarioAntecedentesTerreno.DataSource = coordenadaGeograficaDA.ListaHuso(0);
                    HusoHorarioAntecedentesTerreno.DataTextField = "descripcion";
                    HusoHorarioAntecedentesTerreno.DataValueField = "id";
                    HusoHorarioAntecedentesTerreno.DataBind();
                    HusoHorarioAntecedentesTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoUsoAntecedentesTerreno":
                    TipoUsoAntecedentesTerreno.Items.Clear();
                    TipoUsoAntecedentesTerreno.DataSource = tipoDa.ListarTipo("TIPO_USO");
                    TipoUsoAntecedentesTerreno.DataTextField = "descripcion";
                    TipoUsoAntecedentesTerreno.DataValueField = "id";
                    TipoUsoAntecedentesTerreno.DataBind();
                    TipoUsoAntecedentesTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "VerticeAntecedentesTerreno":
                    VerticeAntecedentesTerreno.Items.Clear();
                    VerticeAntecedentesTerreno.DataSource = tipoDa.ListarTipo("TIPO_VERTICE");
                    VerticeAntecedentesTerreno.DataTextField = "descripcion";
                    VerticeAntecedentesTerreno.DataValueField = "id";
                    VerticeAntecedentesTerreno.DataBind();
                    VerticeAntecedentesTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoConcesionAntecedentesTerreno":
                    TipoConcesionAntecedentesTerreno.Items.Clear();
                    TipoConcesionAntecedentesTerreno.DataSource = tipoConcesionDA.obtenerTipoConcesion(0);
                    TipoConcesionAntecedentesTerreno.DataTextField = "TipoConcesion";
                    TipoConcesionAntecedentesTerreno.DataValueField = "IdTipoConcesion";
                    TipoConcesionAntecedentesTerreno.DataBind();
                    TipoConcesionAntecedentesTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "DATUMAntTerreno":
                    DATUMAntTerreno.Items.Clear();
                    DATUMAntTerreno.DataSource = datumDA.ListaDatum(0);
                    DATUMAntTerreno.DataTextField = "descripcion";
                    DATUMAntTerreno.DataValueField = "id";
                    DATUMAntTerreno.DataBind();
                    DATUMAntTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoUsoRegularizacion":
                    TipoUsoRegularizacion.Items.Clear();
                    TipoUsoRegularizacion.DataSource = tipoDa.ListarTipo("TIPO_USO");
                    TipoUsoRegularizacion.DataTextField = "descripcion";
                    TipoUsoRegularizacion.DataValueField = "id";
                    TipoUsoRegularizacion.DataBind();
                    TipoUsoRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "VerticeRegularizacion":
                    VerticeRegularizacion.Items.Clear();
                    VerticeRegularizacion.DataSource = tipoDa.ListarTipo("TIPO_VERTICE");
                    VerticeRegularizacion.DataTextField = "descripcion";
                    VerticeRegularizacion.DataValueField = "id";
                    VerticeRegularizacion.DataBind();
                    VerticeRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoConsecionRegularizacion":
                    TipoConsecionRegularizacion.Items.Clear();
                    TipoConsecionRegularizacion.DataSource = tipoConcesionDA.obtenerTipoConcesion(0);
                    TipoConsecionRegularizacion.DataTextField = "TipoConcesion";
                    TipoConsecionRegularizacion.DataValueField = "IdTipoConcesion";
                    TipoConsecionRegularizacion.DataBind();
                    TipoConsecionRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoArchivoAntEspaciales":
                    TipoArchivoAntEspaciales.Items.Clear();
                    TipoArchivoAntEspaciales.DataSource = tipoDa.ListarTipo("TIPO_DOC_ANTEC_ESPACIALES");
                    TipoArchivoAntEspaciales.DataTextField = "descripcion";
                    TipoArchivoAntEspaciales.DataValueField = "id";
                    TipoArchivoAntEspaciales.DataBind();
                    TipoArchivoAntEspaciales.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoArchivoAntTerreno":
                    TipoArchivoAntTerreno.Items.Clear();
                    TipoArchivoAntTerreno.DataSource = tipoDa.ListarTipo("TIPO_DOC_ANTEC_TERRENO");
                    TipoArchivoAntTerreno.DataTextField = "descripcion";
                    TipoArchivoAntTerreno.DataValueField = "id";
                    TipoArchivoAntTerreno.DataBind();
                    TipoArchivoAntTerreno.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;
                case "TipoArchivoRegularizacion":
                    TipoArchivoRegularizacion.Items.Clear();
                    TipoArchivoRegularizacion.DataSource = tipoDa.ListarTipo("TIPO_DOC_ANTEC_REGULARIZACION");
                    TipoArchivoRegularizacion.DataTextField = "descripcion";
                    TipoArchivoRegularizacion.DataValueField = "id";
                    TipoArchivoRegularizacion.DataBind();
                    TipoArchivoRegularizacion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

            }
        }


        #endregion




        #region SelectedIndexChanged


        protected void Region_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Provincia");
            Carga_Combobox("TipoCuerpoAgua");
            Carga_Combobox("CuerpoDeAgua");

            if (antecedentesSectorService.existeInconsistenciaBarrioRegion(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(Region.SelectedItem.Value)))
            {

                GuardarUbicacionGeografica.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de modificar la región? ACS y ACM serán eliminadas (si éstas fueron ingresadas).')");
                UpdatePanelGuardarUbicacionGeografica.Update();
            }
            else
            {
                GuardarUbicacionGeografica.Attributes.Remove("onclick");
                UpdatePanelGuardarUbicacionGeografica.Update();
            }
        }

        protected void Provincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("Comuna");
            Carga_Combobox("TipoCuerpoAgua");
            Carga_Combobox("CuerpoDeAgua");

        }

        protected void Comuna_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("TipoCuerpoAgua");
            Carga_Combobox("CuerpoDeAgua");
        }

        protected void TipoCuerpoAgua_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("CuerpoDeAgua");
        }

        protected void CartaBaseAntecedentesEspeciales_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("DATUMAntEspaciales");
        }


        #endregion



        #region TextChanged



        protected void LatitudSegundoAntecedentesTerreno_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LatitudSegundoAntecedentesTerreno);
        }


        protected void LongitudSegundoAntecedentesTerreno_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LongitudSegundoAntecedentesTerreno);
        }


        protected void LatitudSegundoRegularizacion_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LatitudSegundoRegularizacion);
        }


        protected void LongitudSegundoRegularizacion_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LongitudSegundoRegularizacion);
        }


        protected void LatitudSegundoAntecedentesEspeciales_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);
        }


        protected void LongitudSegundoAntecedentesEspeciales_TextChanged(object sender, EventArgs e)
        {
            completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);
        }


        /**
        * Método que carga los otros datos relacionados a la carga de antecedentes especiales.
        */
        protected void NombreCartaAntecedentesEspaciales_TextChanged(object sender, EventArgs e)
        {

            if (NombreCartaAntecedentesEspaciales.Text != null && !NombreCartaAntecedentesEspaciales.Text.Equals(""))
            {
                Carta carta = cartaDA.ObtieneCartaFiltroDescripcion(null, Convert.ToString(NombreCartaAntecedentesEspaciales.Text));

                if (carta != null)
                {
                    IdCartaAntecedentesEspaciales.Value = Convert.ToString(carta.idCarta);
                    NumeroCartaAntEspaciales.Text = Convert.ToString(carta.numeroCarta);
                    EdicionCartaAntEspaciales.Text = Convert.ToString(carta.numeroEdicion);
                    AnioCartaAntEspaciales.Text = Convert.ToString(carta.anioEdicion);
                    if (carta.datum != null && !carta.datum.descripcion.Equals(""))
                    {
                        DatumCartaAntEspaciales.Text = Convert.ToString(carta.datum.descripcion);
                        IdDatumCartaAntEspaciales.Value = Convert.ToString(carta.datum.id);
                    }
                    else
                    {
                        IdDatumCartaAntEspaciales.Value = Convert.ToString("0");
                    }
                    if (carta.huso != null && !carta.huso.descripcion.Equals(""))
                    {
                        HusoCartaAntEspaciales.Text = Convert.ToString(carta.huso.descripcion);
                        IdHusoCartaAntEspaciales.Value = Convert.ToString(carta.huso.id);
                    }
                    else
                    {
                        IdHusoCartaAntEspaciales.Value = Convert.ToString("0");
                    }
                    UpdatePanel3.Update();
                }
                else
                {
                    IdCartaAntecedentesEspaciales.Value = Convert.ToString("0");
                    NumeroCartaAntEspaciales.Text = "";
                    EdicionCartaAntEspaciales.Text = "";
                    AnioCartaAntEspaciales.Text = "";
                    DatumCartaAntEspaciales.Text = "";
                    IdDatumCartaAntEspaciales.Value = Convert.ToString("0");
                    HusoCartaAntEspaciales.Text = "";
                    IdHusoCartaAntEspaciales.Value = Convert.ToString("0");
                    SeUsaParaBancoRegularizacion.Checked = false;

                    UpdatePanel3.Update();
                }
            }
            else
            {
                IdCartaAntecedentesEspaciales.Value = Convert.ToString("0");
                NumeroCartaAntEspaciales.Text = "";
                EdicionCartaAntEspaciales.Text = "";
                AnioCartaAntEspaciales.Text = "";
                DatumCartaAntEspaciales.Text = "";
                IdDatumCartaAntEspaciales.Value = Convert.ToString("0");
                HusoCartaAntEspaciales.Text = "";
                IdHusoCartaAntEspaciales.Value = Convert.ToString("0");
                SeUsaParaBancoRegularizacion.Checked = false;

                UpdatePanel3.Update();
            }

            string script = "closeAutocomplete()";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), NombreCartaAntecedentesEspaciales.ClientID, script.ToString(), true);

        }


        /**
        * Método que carga los otros datos relacionados a la carga de regularización.
        */
        protected void NombreCartaRegul_TextChanged(object sender, EventArgs e)
        {
            if (NombreCartaRegul.Text != null && !NombreCartaRegul.Text.Equals(""))
            {
                Carta carta = cartaDA.ObtieneCartaFiltroDescripcion(null, Convert.ToString(NombreCartaRegul.Text));

                if (carta != null)
                {
                    IdCartaRegul.Value = Convert.ToString(carta.idCarta);
                    NumeroCartaRegul.Text = Convert.ToString(carta.numeroCarta);
                    EdicionCartaRegul.Text = Convert.ToString(carta.numeroEdicion);
                    AnioCartaRegul.Text = Convert.ToString(carta.anioEdicion);
                    if (carta.datum != null && !carta.datum.descripcion.Equals(""))
                    {
                        DatumRegul.Text = Convert.ToString(carta.datum.descripcion);
                        IdDatumRegul.Value = Convert.ToString(carta.datum.id);
                    }
                    else
                    {
                        IdDatumRegul.Value = Convert.ToString("0");
                    }
                    if (carta.huso != null && !carta.huso.descripcion.Equals(""))
                    {
                        HusoHorarioRegul.Text = Convert.ToString(carta.huso.descripcion);
                        IdHusoHorarioRegul.Value = Convert.ToString(carta.huso.id);
                    }
                    else
                    {
                        IdHusoHorarioRegul.Value = Convert.ToString("0");
                    }
                    UpdatePanel7.Update();
                }
                else
                {
                    IdCartaRegul.Value = Convert.ToString("0");
                    NumeroCartaRegul.Text = "";
                    EdicionCartaRegul.Text = "";
                    AnioCartaRegul.Text = "";
                    DatumRegul.Text = "";
                    IdDatumRegul.Value = Convert.ToString("0");
                    HusoHorarioRegul.Text = "";
                    IdHusoHorarioRegul.Value = Convert.ToString("0");
                    SeUsaParaBancoRegularizacion.Checked = false;

                    UpdatePanel7.Update();
                }
            }
            else
            {
                IdCartaRegul.Value = Convert.ToString("0");
                NumeroCartaRegul.Text = "";
                EdicionCartaRegul.Text = "";
                AnioCartaRegul.Text = "";
                DatumRegul.Text = "";
                IdDatumRegul.Value = Convert.ToString("0");
                HusoHorarioRegul.Text = "";
                IdHusoHorarioRegul.Value = Convert.ToString("0");
                SeUsaParaBancoRegularizacion.Checked = false;

                UpdatePanel7.Update();
            }

            string script = "closeAutocomplete()";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), NombreCartaAntecedentesEspaciales.ClientID, script.ToString(), true);
        }



        #endregion



        #region ubicacion geografica

        /**
         * Método que guarda la sección Ubicación geográfica del formulario de antecedentes del sector.
         */
        protected void GuardarUbicacionGeografica_Click(object sender, ImageClickEventArgs e)
        {


            //SolicitudConcesion solicitudConcesion = new SolicitudConcesion();

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            if (solicitudConcesion == null) {
                solicitudConcesion = new SolicitudConcesion();
            }

            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedValue), Convert.ToString(Region.SelectedItem.Text));

            solicitudConcesion.provincia = new ParametroGenerico(Convert.ToInt32(Provincia.SelectedValue), Convert.ToString(Provincia.SelectedItem.Text));
            solicitudConcesion.comuna = new List<ParametroGenerico>();
            solicitudConcesion.reqAntecTerreno = false;
            solicitudConcesion.reqRegularizacion = false;
            solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico();
            solicitudConcesion.tipoUnidadEspacial.id = rbTipo.CONCESION_DE_ACUICULTURA;

            solicitudConcesion.cuerpoAgua = new CuerpoDeAgua();


            if (Convert.ToInt32(CuerpoDeAgua.SelectedValue) > 0)
            {
                solicitudConcesion.cuerpoAgua.idCuerpoDeAgua = Convert.ToInt32(Convert.ToInt32(CuerpoDeAgua.SelectedValue));
            }

            if (Convert.ToInt32(TipoCuerpoAgua.SelectedValue) > 0)
            {
                solicitudConcesion.cuerpoAgua.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(TipoCuerpoAgua.SelectedValue), Convert.ToString(TipoCuerpoAgua.SelectedItem.Text));
            }


            foreach (ListItem item in Comuna.Items)
            {
                if (item.Selected && Convert.ToInt32(item.Value) > 0)
                {
                    ParametroGenerico comuna = new ParametroGenerico();
                    comuna.id = Convert.ToInt32(item.Value);
                    comuna.descripcion = Convert.ToString(item.Text);
                    solicitudConcesion.comuna.Add(comuna);
                }
            }

            List<String> listaErroresUbicacionGeografica = antecedDelSectorValidacion.validaUbicacionGeografica(solicitudConcesion);

            if (listaErroresUbicacionGeografica != null && listaErroresUbicacionGeografica.Count <= 0)
            {
                bool resp = antecedentesSectorService.guardarUbicacionGeografica(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    msgGrillaGral_1.Text = "Se ha guardado la Ubicación Geográfica exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;

                    //SI EXISTE INCONSISTENCIA ENTRE EL BARRIO Y LA REGION SE DEBEN BORRAR LOS BARRIOS
                    if (antecedentesSectorService.existeInconsistenciaBarrioRegion(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(Region.SelectedItem.Value)))
                    {
                        antecedentesSectorService.borrarBarrio(solicitudConcesion.idSolConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    }

                    //COMO LA COMUNA PUEDE HABER CAMBIADO, SE DEBE DETERMINAR SI APLICA O NO DIFROL
                    SolicitudConcesion solicitudDifrol = solicitudDA.ObtieneSolicitudConcesion(solicitudConcesion.idSolConcesion, ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                    SolicitudConcesion solicitudEnSession = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                

                    solicitudEnSession.comunaFronteriza = solicitudDifrol.comunaFronteriza;
                    solicitudEnSession.barrio = solicitudDifrol.barrio;
                    solicitudEnSession.acm = solicitudDifrol.acm;

                    Session[ViewState["solicitudSession"].ToString()] = (SolicitudConcesion)solicitudEnSession;

                    /* Agrupación de Concesion ACS */
                    //Carga_Combobox("BarrioACS");
                    //Carga_Combobox("BarrioACM");

                    this.despliegaBarrio(solicitudEnSession);
                    UpdatePanelDatosBarrio.Update();

                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha guardado la Ubicación Geográfica.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                }

                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                UpdatePanelMensaje.Update();
            }
            else
            {

                foreach (String error in listaErroresUbicacionGeografica)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
                UpdatePanelMensaje.Update();
                Region.Focus();
            }

            //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
            UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
            informacionSolicitud.Instance.RecargarInformacion();
            UpdatePanelInformacionSolictud.Update();

        }


        #endregion


        #region Agrupación de Concesiones

        /**
         * Método que guarda la sección Barrio del formulario de antecedentes del sector.
         */
        protected void GuardarDatosBarrio_Click(object sender, ImageClickEventArgs e)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

            //SolicitudConcesion solicitudConcesion = new SolicitudConcesion();

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            if (solicitudConcesion == null)
            {
                solicitudConcesion = new SolicitudConcesion();
            }

            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.region = new ParametroGenerico(Convert.ToInt32(Region.SelectedValue), Convert.ToString(Region.SelectedItem.Text));


            solicitudConcesion.barrio = new Barrio();
            solicitudConcesion.barrio.id_barrio = Convert.ToInt32(BarrioACS.SelectedValue);

            solicitudConcesion.acm = new Barrio();
            solicitudConcesion.acm.id_barrio = Convert.ToInt32(BarrioACM.SelectedValue);


            SolicitudConcesion solicitudConcesionSession = (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];

            solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico();
            solicitudConcesion.tipoUnidadEspacial.id = solicitudConcesionSession.tipoUnidadEspacial.id;



            List<String> listaErroresUbicacionGeografica = antecedDelSectorValidacion.validaBarrio(solicitudConcesion);

            if (listaErroresUbicacionGeografica != null && listaErroresUbicacionGeografica.Count <= 0)
            {
                bool resp = antecedentesSectorService.guardarBarrio(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    msgGrillaGral_1.Text = "Se ha guardado la Agrupación de Concesiones exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha guardado la Agrupación de Concesiones.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;
                }

                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                UpdatePanelMensaje.Update();
            }
            else
            {
                foreach (String error in listaErroresUbicacionGeografica)
                {
                    Page.Validators.Add(new ValidationError("grupo2", error));
                }

                UpdatePanelMensaje.Update();
            }
        }


        #endregion


        #region Otra Definición Geográfica

        /**
         * Método que guarda la Administración de Planos del formulario 
         * Antecedentes del Sector.
         * 
         */
        protected void GuardarDatosPlanos_Click(object sender, ImageClickEventArgs e)
        {

            //SolicitudConcesion solicitudConcesion = new SolicitudConcesion();

            SolicitudConcesion solicitudConcesion = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            if (solicitudConcesion == null)
            {
                solicitudConcesion = new SolicitudConcesion();
            }


            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico();
            solicitudConcesion.tipoUnidadEspacial.id = rbTipo.CONCESION_DE_ACUICULTURA;

            for (int i = 0; i < CheckBoxListNecesita.Items.Count; i++)
            {
                if (CheckBoxListNecesita.Items[i].Selected)
                {
                    if (i == 0)
                    {
                        solicitudConcesion.reqAntecTerreno = true;
                    }
                    if (i == 1)
                    {
                        solicitudConcesion.reqRegularizacion = true;
                    }

                }
            }

            List<String> listaErroresUbicacionGeografica = antecedDelSectorValidacion.validaOtraDefinicionGeografica(solicitudConcesion);

            if (listaErroresUbicacionGeografica != null && listaErroresUbicacionGeografica.Count <= 0)
            {
                bool resp = antecedentesSectorService.guardarOtraDefinicionGeografica(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    msgGrillaGral_1.Text = "Se ha guardado Otra Definición Geográfica exitosamente.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;

                    despliegaPestanias(solicitudConcesion);

                    if (!solicitudConcesion.reqAntecTerreno)
                    {
                        limpiaPestanias("AntecedentesTerreno");
                    }

                    if (!solicitudConcesion.reqRegularizacion)
                    {
                        limpiaPestanias("Regularizacion");
                    }

                }
                else
                {
                    msgGrillaGral_1.Text = "No se ha guardado Otra Definición Geográfica.";
                    msgGrillaGral_1.Focus();
                    Content_msgGrillaGral_1.Visible = true;

                }
                Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                UpdatePanelMensaje.Update();
            }
            else
            {
                foreach (String error in listaErroresUbicacionGeografica)
                {
                    Page.Validators.Add(new ValidationError("grupo2", error));
                }
            }
        }

        #endregion



        #region Referencias Geograficas

        /**
         * Guardar Referencias Geográficas de Antecedentes Espaciales. 
         */
        protected void GuardarReferenciasGeograficasAntecedentesEspeciales_Click(object sender, ImageClickEventArgs e)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

            SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.coordenadaGeografica = new List<CoordenadaGeografica>();

            CoordenadaGeografica coordenada = new CoordenadaGeografica();
            coordenada.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            coordenada.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);
            coordenada.estado = new ParametroGenerico();
            coordenada.estado.id = rbEstadosGenerales.VIGENTE;

            if (IdCartaAntecedentesEspaciales.Value != null && !IdCartaAntecedentesEspaciales.Value.Equals(""))
            {
                coordenada.carta = new Carta();
                coordenada.carta.idCarta = Convert.ToInt32(IdCartaAntecedentesEspaciales.Value);
            }
            
            //coordenada.tipoHuso = new ParametroGenerico();
            //coordenada.tipoHuso.id = Convert.ToInt32(IdHusoCartaAntEspaciales.Value);
            //coordenada.datum = new ParametroGenerico();
            //coordenada.datum.id = Convert.ToInt32(IdDatumCartaAntEspaciales.Value);
            coordenada.aplicaBanco = Convert.ToBoolean(SeUsaParaBancoAntEspaciales.Checked);
            coordenada.aplicaVisualizadorDeMapas = Convert.ToBoolean(SeUsaParaVisualizadorDeMapasAntEspeciales.Checked);

            coordenada.areaTotalCalculada = Convert.ToSingle(AreaTotalCalculadaAntecedentesEspeciales.Text);
            coordenada.areaTotalSolicitada = Convert.ToSingle(AreaTotalSolicitadaAntecedentesEspeciales.Text);
            coordenada.areaTotalRegularizacion = 0;
            coordenada.tipoCoordgeografica = new ParametroGenerico();
            coordenada.tipoCoordgeografica.id = rbTipo.ANTECEDENTES_ESPACIALES;
            solicitudConcesion.coordenadaGeografica.Add(coordenada);

            List<String> listaErroresReferenciaGeograficaAntEspacial = antecedDelSectorValidacion.validaReferenciasGeograficas(solicitudConcesion);

            if (listaErroresReferenciaGeograficaAntEspacial != null && listaErroresReferenciaGeograficaAntEspacial.Count <= 0)
            {
                bool resp = antecedentesSectorService.guardarCoordenadaGeografica(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {

                    foreach (CoordenadaGeografica coordenadaGeografica in solicitudConcesion.coordenadaGeografica)
                    {
                        IdCoordenadaGeoAntEspaciales.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);
                        break;
                    }
                    msgGrillaGral_2.Text = "Se ha guardado la Referencia Geográfica (Coord. Originales) exitosamente.";
                    msgGrillaGral_2.Focus();
                    Content_msgGrillaGral_2.Visible = true;

                }
                else
                {
                    msgGrillaGral_2.Text = "No se ha guardado la Referencia Geográfica (Coord. Originales).";
                    msgGrillaGral_2.Focus();
                    Content_msgGrillaGral_2.Visible = true;
                }
                IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                updPestanaAntecedentesEspaciales.Update();
            }
            else
            {
                foreach (String error in listaErroresReferenciaGeograficaAntEspacial)
                {
                    Page.Validators.Add(new ValidationError("grupo3", error));
                }
                SeUsaParaBancoAntEspaciales.Checked = false;
                SeUsaParaVisualizadorDeMapasAntEspeciales.Checked = false;
                updPestanaAntecedentesEspaciales.Update();
            }
        }


        /**
         * Método que guarda las referencias geográficas de Antecedentes de Terreno.
         */
        protected void GuardarReferenciasGeograficasAntTerreno_Click(object sender, ImageClickEventArgs e)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

            SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.coordenadaGeografica = new List<CoordenadaGeografica>();

            CoordenadaGeografica coordenada = new CoordenadaGeografica();
            coordenada.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            coordenada.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);
            coordenada.estado = new ParametroGenerico();
            coordenada.estado.id = rbEstadosGenerales.VIGENTE;
            coordenada.datum = new ParametroGenerico();
            coordenada.datum.id = Convert.ToInt32(DATUMAntTerreno.SelectedValue);
            coordenada.tipoHuso = new ParametroGenerico();
            coordenada.tipoHuso.id = Convert.ToInt32(HusoHorarioAntecedentesTerreno.SelectedValue);
            
            coordenada.aplicaBanco = Convert.ToBoolean(SeUsaParaBancoAntTerreno.Checked);
            coordenada.aplicaVisualizadorDeMapas = Convert.ToBoolean(SeUsaParaVisualizadorDeMapasAntTerreno.Checked);

            coordenada.areaTotalCalculada = Convert.ToSingle(AreaTotalCalculadaAntecedentesTerreno.Text);
            coordenada.areaTotalSolicitada = Convert.ToSingle(AreaTotalSolicitadaAntecedentesTerreno.Text);
            coordenada.areaTotalRegularizacion = 0;
            coordenada.tipoCoordgeografica = new ParametroGenerico();
            coordenada.tipoCoordgeografica.id = rbTipo.ANTECEDENTES_TERRENO;
            solicitudConcesion.coordenadaGeografica.Add(coordenada);

            List<String> listaErroresReferenciaGeograficaAntTerreno = antecedDelSectorValidacion.validaReferenciasGeograficas(solicitudConcesion);

            if (listaErroresReferenciaGeograficaAntTerreno != null && listaErroresReferenciaGeograficaAntTerreno.Count <= 0)
            {

                bool resp = antecedentesSectorService.guardarCoordenadaGeografica(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    foreach (CoordenadaGeografica coordenadaGeografica in solicitudConcesion.coordenadaGeografica)
                    {
                        IdCoordenadaGeoAntTerreno.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);
                        break;
                    }
                    msgGrillaGral_3.Text = "Se ha guardado la Referencia Geográfica (Coord. 14 TER) exitosamente.";
                    Content_msgGrillaGral_3.Visible = true;

                }
                else
                {
                    msgGrillaGral_3.Text = "No se ha guardado la Referencia Geográfica (Coord. 14 TER).";
                    Content_msgGrillaGral_3.Visible = true;
                }
                IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                UpdateAntecedentesTerreno.Update();

            }
            else
            {
                foreach (String error in listaErroresReferenciaGeograficaAntTerreno)
                {
                    Page.Validators.Add(new ValidationError("grupo7", error));
                }
                SeUsaParaBancoAntTerreno.Checked = false;
                UpdateAntecedentesTerreno.Update();
            }
        }


        /**
         * Guarda Referencias Geográficas de la pestaña de Regularización.
         */
        protected void GuardarReferenciasGeograficasRegul_Click(object sender, ImageClickEventArgs e)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

            SolicitudConcesion solicitudConcesion = new SolicitudConcesion();
            solicitudConcesion.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            solicitudConcesion.coordenadaGeografica = new List<CoordenadaGeografica>();

            CoordenadaGeografica coordenada = new CoordenadaGeografica();
            coordenada.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            coordenada.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);
            coordenada.estado = new ParametroGenerico();
            coordenada.estado.id = rbEstadosGenerales.VIGENTE;

            if (IdCartaRegul.Value != null && !IdCartaRegul.Value.Equals(""))
            {
                coordenada.carta = new Carta();
                coordenada.carta.idCarta = Convert.ToInt32(IdCartaRegul.Value);
            }
            
            coordenada.aplicaBanco = Convert.ToBoolean(SeUsaParaBancoRegularizacion.Checked);
            coordenada.aplicaVisualizadorDeMapas = Convert.ToBoolean(SeUsaParaVisualizadorDeMapasRegularizacion.Checked);

            //coordenada.datum = new ParametroGenerico();
            //coordenada.datum.id = Convert.ToInt32(IdDatumRegul.Value);
            //coordenada.tipoHuso = new ParametroGenerico();
            //coordenada.tipoHuso.id = Convert.ToInt32(IdHusoHorarioRegul.Value);
            coordenada.areaTotalRegularizacion = Convert.ToSingle(AreaTotalRegularizacion.Text);
            coordenada.areaTotalCalculada = 0;
            coordenada.areaTotalSolicitada = 0;
            coordenada.tipoCoordgeografica = new ParametroGenerico();
            coordenada.tipoCoordgeografica.id = rbTipo.REGULARIZACION;
            solicitudConcesion.coordenadaGeografica.Add(coordenada);

            List<String> listaErroresReferenciaGeograficaRegul = antecedDelSectorValidacion.validaReferenciasGeograficas(solicitudConcesion);

            if (listaErroresReferenciaGeograficaRegul != null && listaErroresReferenciaGeograficaRegul.Count <= 0)
            {

                bool resp = antecedentesSectorService.guardarCoordenadaGeografica(solicitudConcesion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                if (resp)
                {
                    foreach (CoordenadaGeografica coordenadaGeografica in solicitudConcesion.coordenadaGeografica)
                    {
                        IdReferenciaGeograficaRegularizacion.Value = Convert.ToString(coordenadaGeografica.idCoordenadaGeo);
                        break;
                    }
                    msgGrillaGral_4.Text = "Se ha guardado la Referencia Geográfica (Regularización) exitosamente.";
                    Content_msgGrillaGral_4.Visible = true;

                }
                else
                {
                    msgGrillaGral_4.Text = "No se ha guardado la Referencia Geográfica (Regularización).";
                    Content_msgGrillaGral_4.Visible = true;
                }

                IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                UpdatePanelPanelRegularizacion.Update();
            }
            else
            {
                foreach (String error in listaErroresReferenciaGeograficaRegul)
                {
                    Page.Validators.Add(new ValidationError("grupo10", error));
                }
                SeUsaParaBancoRegularizacion.Checked = false;
                SeUsaParaVisualizadorDeMapasRegularizacion.Checked = false;
                UpdatePanelPanelRegularizacion.Update();
            }
        }


        #endregion


        #region Archivo Adjunto


        protected void GuardarArchivoAdjuntoAntEspaciales_Click(object sender, ImageClickEventArgs e)
        {

            if (ArchivoAdjuntoAntEspaciales.HasFile)
            {

                CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();
                coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);

                ArchivoCoordenadaGeo archivoCoordenadaGeo = new ArchivoCoordenadaGeo();
                archivoCoordenadaGeo.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                archivoCoordenadaGeo.tipoDocumento = new ParametroGenerico();
                archivoCoordenadaGeo.tipoDocumento.id = Convert.ToInt32(TipoArchivoAntEspaciales.SelectedValue);
                archivoCoordenadaGeo.tipoDocumento.descripcion = Convert.ToString(TipoArchivoAntEspaciales.SelectedItem.Text);

                ArchivoBinario archivoBinario = new ArchivoBinario();

                archivoBinario.nombreArchivo = Convert.ToString(NombreArchivoAntEspaciales.Text);
                archivoBinario.nombreFisico = ArchivoAdjuntoAntEspaciales.PostedFile.FileName.Substring(0, ArchivoAdjuntoAntEspaciales.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.formato = ArchivoAdjuntoAntEspaciales.PostedFile.FileName.Substring(ArchivoAdjuntoAntEspaciales.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                archivoBinario.tamano = ArchivoAdjuntoAntEspaciales.PostedFile.InputStream.Length;
                archivoBinario.archivo = ArchivoAdjuntoAntEspaciales.PostedFile;

                archivoCoordenadaGeo.archivoBinario = archivoBinario;

                coordenadaGeografica.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                coordenadaGeografica.listaArchivoCoordGeo.Add(archivoCoordenadaGeo);

                List<String> listaErroresArchivoAdjunto = antecedDelSectorValidacion.validaArchivoAdjuntoAntEspaciales(coordenadaGeografica);

                if (listaErroresArchivoAdjunto != null && listaErroresArchivoAdjunto.Count <= 0)
                {

                    bool resp = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeografica);
                    if (resp)
                    {
                        TipoArchivoAntEspaciales.SelectedValue = "-1";
                        NombreArchivoAntEspaciales.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoAntEspacial.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoAntEspacial.DataBind();
                        GridArchivoAdjuntoAntEspacial.Visible = true;

                        msgGrillaGral_2.Text = "Se ha guardado el Archivo (Coord. Originales) exitosamente.";
                        Content_msgGrillaGral_2.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_2.Text = "No se ha guardado el Archivo (Coord. Originales).";
                        Content_msgGrillaGral_2.Visible = true;
                    }

                    IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    TipoArchivoAntEspaciales.Focus();
                }
                else
                {
                    foreach (String error in listaErroresArchivoAdjunto)
                    {
                        Page.Validators.Add(new ValidationError("grupo20", error));
                    }

                }
            }
        }

        protected void GuardarArchivoAdjuntoAntTerreno_Click(object sender, ImageClickEventArgs e)
        {

            if (ArchivoAdjuntoAntTerreno.HasFile)
            {
                CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();
                coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);

                ArchivoCoordenadaGeo archivoCoordenadaGeo = new ArchivoCoordenadaGeo();
                archivoCoordenadaGeo.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                archivoCoordenadaGeo.tipoDocumento = new ParametroGenerico();
                archivoCoordenadaGeo.tipoDocumento.id = Convert.ToInt32(TipoArchivoAntTerreno.SelectedValue);
                archivoCoordenadaGeo.tipoDocumento.descripcion = Convert.ToString(TipoArchivoAntTerreno.SelectedItem.Text);

                ArchivoBinario archivoBinario = new ArchivoBinario();

                archivoBinario.nombreArchivo = Convert.ToString(NombreArchivoAntTerreno.Text);
                archivoBinario.nombreFisico = ArchivoAdjuntoAntTerreno.PostedFile.FileName.Substring(0, ArchivoAdjuntoAntTerreno.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.formato = ArchivoAdjuntoAntTerreno.PostedFile.FileName.Substring(ArchivoAdjuntoAntTerreno.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                archivoBinario.tamano = ArchivoAdjuntoAntTerreno.PostedFile.InputStream.Length;
                archivoBinario.archivo = ArchivoAdjuntoAntTerreno.PostedFile;

                archivoCoordenadaGeo.archivoBinario = archivoBinario;

                coordenadaGeografica.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                coordenadaGeografica.listaArchivoCoordGeo.Add(archivoCoordenadaGeo);

                List<String> listaErroresArchivoAdjunto = antecedDelSectorValidacion.validaArchivoAdjuntoAntTerreno(coordenadaGeografica);

                if (listaErroresArchivoAdjunto.Count <= 0)
                {

                    bool resp = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeografica);
                    if (resp)
                    {
                        TipoArchivoAntTerreno.SelectedValue = "-1";
                        NombreArchivoAntTerreno.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoAntTerreno.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoAntTerreno.DataBind();
                        GridArchivoAdjuntoAntTerreno.Visible = true;

                        msgGrillaGral_3.Text = "Se ha guardado el Archivo (Coord. 14 TER) exitosamente.";
                        Content_msgGrillaGral_3.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_3.Text = "No se ha guardado el Archivo (Coord. 14 TER).";
                        Content_msgGrillaGral_3.Visible = true;
                    }

                    TipoArchivoAntTerreno.Focus();
                    IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                }
                else
                {
                    foreach (String error in listaErroresArchivoAdjunto)
                    {
                        Page.Validators.Add(new ValidationError("grupo21", error));
                    }
                    TipoArchivoAntTerreno.Focus();
                }
            }
        }

        protected void GuardarArchivoAdjuntoRegularizacion_Click(object sender, ImageClickEventArgs e)
        {

            if (ArchivoAdjuntoRegularizacion.HasFile)
            {

                CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();
                coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);

                ArchivoCoordenadaGeo archivoCoordenadaGeo = new ArchivoCoordenadaGeo();
                archivoCoordenadaGeo.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                archivoCoordenadaGeo.tipoDocumento = new ParametroGenerico();
                archivoCoordenadaGeo.tipoDocumento.id = Convert.ToInt32(TipoArchivoRegularizacion.SelectedValue);
                archivoCoordenadaGeo.tipoDocumento.descripcion = Convert.ToString(TipoArchivoRegularizacion.SelectedItem.Text);

                ArchivoBinario archivoBinario = new ArchivoBinario();

                archivoBinario.nombreArchivo = Convert.ToString(NombreArchivoRegularizacion.Text);
                archivoBinario.nombreFisico = ArchivoAdjuntoRegularizacion.PostedFile.FileName.Substring(0, ArchivoAdjuntoRegularizacion.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.formato = ArchivoAdjuntoRegularizacion.PostedFile.FileName.Substring(ArchivoAdjuntoRegularizacion.PostedFile.FileName.LastIndexOf(".") + 1).ToLower(); ;
                archivoBinario.tamano = ArchivoAdjuntoRegularizacion.PostedFile.InputStream.Length;
                archivoBinario.archivo = ArchivoAdjuntoRegularizacion.PostedFile;

                archivoCoordenadaGeo.archivoBinario = archivoBinario;

                coordenadaGeografica.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                coordenadaGeografica.listaArchivoCoordGeo.Add(archivoCoordenadaGeo);

                List<String> listaErroresArchivoAdjunto = antecedDelSectorValidacion.validaArchivoAdjuntoRegularizacion(coordenadaGeografica);

                if (listaErroresArchivoAdjunto != null && listaErroresArchivoAdjunto.Count <= 0)
                {

                    bool resp = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeografica);
                    if (resp)
                    {
                        TipoArchivoRegularizacion.SelectedValue = "-1";
                        NombreArchivoRegularizacion.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoRegularizacion.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoRegularizacion.DataBind();
                        GridArchivoAdjuntoRegularizacion.Visible = true;

                        msgGrillaGral_4.Text = "Se ha guardado el Archivo (Regularización) exitosamente.";
                        Content_msgGrillaGral_4.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_4.Text = "No se ha guardado el Archivo (Regularización).";
                        Content_msgGrillaGral_4.Visible = true;
                    }

                    IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    TipoArchivoRegularizacion.Focus();
                }
                else
                {
                    foreach (String error in listaErroresArchivoAdjunto)
                    {
                        Page.Validators.Add(new ValidationError("grupo22", error));
                    }
                    TipoArchivoRegularizacion.Focus();
                }
            }
        }


        protected void GridArchivoAdjuntoAntEspacial_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosAntEspaciales = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Archivos Adjuntos (Ant. Espaciales)";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivosAdjuntosAntEspaciales.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridArchivoAdjuntoAntEspacial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idArchivo = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    GridArchivoAdjuntoAntEspacial.EditIndex = -1;
                    EliminarGrillaArchivo(idArchivo, "AntecedentesEspaciales");
                    CargaGrillaArchivo("AntecedentesEspaciales");
                    break;
                case "Desasociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("AntecedentesEspaciales", idArchivo);
                    CargaGrillaArchivo("AntecedentesEspaciales");
                    break;
                case "Asociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("AntecedentesEspaciales", idArchivo);
                    CargaGrillaArchivo("AntecedentesEspaciales");
                    break;
                case "Descargar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    /*
                    long dataToRead;

                    try
                    {
                        // Longitud del archivo: 
                        int length;

                        byte[] buffer = new Byte[1000];
                        System.IO.Stream iStream = new System.IO.MemoryStream(archivoBinario.bytes);
                        //for (int i = 0; i < archivo.Length; i++)
                        //    iStream.WriteByte(archivo[0]);

                        HttpContext.Current.Response.ContentType = "application/"+ archivoBinario.formato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                        dataToRead = iStream.Length;

                        Response.Clear();

                        while (dataToRead > 0)
                        { // Comprobar que el cliente está conectado. 
                            if (HttpContext.Current.Response.IsClientConnected)
                            {
                                // Read the data in buffer. 
                                length = iStream.Read(buffer, 0, 1000);
                                
                                // Escribir los datos en la secuencia de salida actual. 
                                HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

                                // Vaciar los datos en la salida HTML. 
                                HttpContext.Current.Response.Flush();

                                buffer = new Byte[1000]; dataToRead = dataToRead - length;
                            }
                            else
                            { //impedir un bucle infinito si el usuario se desconecta 
                                dataToRead = -1;
                            }
                        }

                        if (iStream != null)
                        { //Cerrar el archivo. 
                            iStream.Close();
                            iStream.Dispose();
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.Close();
                            //HttpContext.Current.Response.End();
                            System.Web.HttpContext.Current.Response.Close();
                        }

                    }catch(Exception ex){
                        Response.Write("Error : " + ex.Message);
                    }
                    */

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();

                    break;
            };
        }

        protected void GridArchivoAdjuntoAntEspacial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                        {
                            boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el archivo?')");
                            boton_eliminar.Visible = true;
                        }
                    };

                    int idArchivo = Convert.ToInt32(GridArchivoAdjuntoAntEspacial.DataKeys[e.Row.RowIndex].Value);
                    ArchivoCoordenadaGeo archivoCoordenadaGeo = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);

                    if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Desasociar
                        ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                        if (boton_desasociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                            {
                                boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                                boton_desasociar.Visible = true;
                            }
                        };
                    }

                    else if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Asociar
                        ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                        if (boton_asociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                            {
                                boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                                boton_asociar.Visible = true;
                            }
                        };
                    }

                    //Descargar
                    String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                    ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                    if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESCARGAR))
                        {
                            boton_descargar.Visible = true;
                        }
                    };

                };
            }
            catch (Exception)
            {

            }
        }


        protected void GridArchivoAdjuntoAntTerreno_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosAntTerreno = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Archivos Adjuntos (Ant. Terreno)";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivosAdjuntosAntTerreno.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridArchivoAdjuntoAntTerreno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idArchivo = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    GridArchivoAdjuntoAntTerreno.EditIndex = -1;
                    EliminarGrillaArchivo(idArchivo, "AntecedentesTerreno");
                    CargaGrillaArchivo("AntecedentesTerreno");
                    break;
                case "Desasociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("AntecedentesTerreno", idArchivo);
                    CargaGrillaArchivo("AntecedentesTerreno");
                    break;
                case "Asociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("AntecedentesTerreno", idArchivo);
                    CargaGrillaArchivo("AntecedentesTerreno");
                    break;
                case "Descargar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();
                    break;
            };
        }

        protected void GridArchivoAdjuntoAntTerreno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Archivo?')");
                        boton_eliminar.Visible = true;
                    }
                };

                int idArchivo = Convert.ToInt32(GridArchivoAdjuntoAntTerreno.DataKeys[e.Row.RowIndex].Value);
                ArchivoCoordenadaGeo archivoCoordenadaGeo = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);

                if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.VIGENTE)
                {
                    //Desasociar
                    ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                    if (boton_desasociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                        {
                            boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_desasociar.Visible = true;
                        }
                    };
                }

                else if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    //Asociar
                    ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                    if (boton_asociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                        {
                            boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_asociar.Visible = true;
                        }
                    };
                }

                //Descargar
                String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESCARGAR))
                    {
                        boton_descargar.Visible = true;
                    }
                };
            };
        }


        protected void GridArchivoAdjuntoRegularizacion_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridArchivosAdjuntosRegularizacion = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Archivos Adjuntos (Regularización)";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridArchivosAdjuntosRegularizacion.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridArchivoAdjuntoRegularizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idArchivo = 0;
            switch (e.CommandName)
            {
                case "Eliminar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    GridArchivoAdjuntoRegularizacion.EditIndex = -1;
                    EliminarGrillaArchivo(idArchivo, "Regularizacion");
                    CargaGrillaArchivo("Regularizacion");
                    break;
                case "Desasociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("Regularizacion", idArchivo);
                    CargaGrillaArchivo("Regularizacion");
                    break;
                case "Asociar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoArchivo("Regularizacion", idArchivo);
                    CargaGrillaArchivo("Regularizacion");
                    break;
                case "Descargar":
                    idArchivo = Convert.ToInt32(e.CommandArgument);
                    ArchivoBinario archivoBinario = archivoBinarioSolicitudDA.ObtenerArchivoBinarioSolicitud(idArchivo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinario.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinario.nombreArchivo + "." + archivoBinario.formato);
                    Response.BinaryWrite(archivoBinario.bytes);
                    Response.Flush();
                    Response.End();
                    break;
            };
        }

        protected void GridArchivoAdjuntoRegularizacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Archivo?')");
                        boton_eliminar.Visible = true;
                    }
                };

                int idArchivo = Convert.ToInt32(GridArchivoAdjuntoRegularizacion.DataKeys[e.Row.RowIndex].Value);
                ArchivoCoordenadaGeo archivoCoordenadaGeo = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);

                if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.VIGENTE)
                {
                    //Desasociar
                    ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                    if (boton_desasociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                        {
                            boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_desasociar.Visible = true;
                        }
                    };
                }

                else if (archivoCoordenadaGeo != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    //Asociar
                    ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                    if (boton_asociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                        {
                            boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_asociar.Visible = true;
                        }
                    };
                }

                //Descargar
                String idArchivoBinario = DataBinder.Eval(e.Row.DataItem, "idArchivoBinario").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinario != null && !idArchivoBinario.Equals("") && Convert.ToInt32(idArchivoBinario) > 0)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVOS_ADJUNTOS_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESCARGAR))
                    {
                        boton_descargar.Visible = true;
                    }
                };
            };
        }


        protected void EliminarGrillaArchivo(int idArchivo, String nombrePestania)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":
                    TipoArchivoAntEspaciales.SelectedValue = "-1";
                    NombreArchivoAntEspaciales.Text = "";
                    //ArchivoAdjuntoAntEspaciales.FileBytes = byte[0];

                    CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();
                    coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);
                    coordenadaGeografica.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();

                    ArchivoCoordenadaGeo archivoCoordenadaGeo = new ArchivoCoordenadaGeo();

                    ArchivoBinario archivoBinario = new ArchivoBinario();
                    archivoBinario.idArchivo = idArchivo;

                    archivoCoordenadaGeo.archivoBinario = archivoBinario;

                    coordenadaGeografica.listaArchivoCoordGeo.Add(archivoCoordenadaGeo);

                    bool resp = antecedentesSectorService.eliminarArchivoAdjuntoCoordenadaGeo(coordenadaGeografica);
                    if (resp)
                    {
                        Content_msgGrillaGral_2.Visible = true;
                        msgGrillaGral_2.Text = "Se ha eliminado exitosamente el archivo (Ant Espaciales).";
                    }
                    else
                    {
                        Content_msgGrillaGral_2.Visible = true;
                        msgGrillaGral_2.Text = "No se ha eliminado el archivo (Ant Espaciales).";
                    }

                    IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                    break;

                case "AntecedentesTerreno":
                    TipoArchivoAntTerreno.SelectedValue = "-1";
                    NombreArchivoAntTerreno.Text = "";
                    //ArchivoAdjuntoAntTerreno.FileBytes = byte[0];

                    CoordenadaGeografica coordenadaGeograficaAnTerreno = new CoordenadaGeografica();
                    coordenadaGeograficaAnTerreno.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);
                    coordenadaGeograficaAnTerreno.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();

                    ArchivoCoordenadaGeo archivoCoordenadaGeoAntTerreno = new ArchivoCoordenadaGeo();

                    ArchivoBinario archivoBinarioAntTerreno = new ArchivoBinario();
                    archivoBinarioAntTerreno.idArchivo = idArchivo;

                    archivoCoordenadaGeoAntTerreno.archivoBinario = archivoBinarioAntTerreno;

                    coordenadaGeograficaAnTerreno.listaArchivoCoordGeo.Add(archivoCoordenadaGeoAntTerreno);

                    bool respAnTerreno = antecedentesSectorService.eliminarArchivoAdjuntoCoordenadaGeo(coordenadaGeograficaAnTerreno);
                    if (respAnTerreno)
                    {
                        Content_msgGrillaGral_3.Visible = true;
                        msgGrillaGral_3.Text = "Se ha eliminado exitosamente el archivo (Ant Terreno).";
                    }
                    else
                    {
                        Content_msgGrillaGral_3.Visible = true;
                        msgGrillaGral_3.Text = "No se ha eliminado el archivo (Ant Terreno).";
                    }

                    IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                    break;
                case "Regularizacion":
                    TipoArchivoRegularizacion.SelectedValue = "-1";
                    NombreArchivoRegularizacion.Text = "";
                    //ArchivoAdjuntoRegularizacion.FileBytes = byte[0];

                    CoordenadaGeografica coordenadaGeograficaRegul = new CoordenadaGeografica();
                    coordenadaGeograficaRegul.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);
                    coordenadaGeograficaRegul.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();

                    ArchivoCoordenadaGeo archivoCoordenadaGeRegul = new ArchivoCoordenadaGeo();

                    ArchivoBinario archivoBinarioRegul = new ArchivoBinario();
                    archivoBinarioRegul.idArchivo = idArchivo;

                    archivoCoordenadaGeRegul.archivoBinario = archivoBinarioRegul;

                    coordenadaGeograficaRegul.listaArchivoCoordGeo.Add(archivoCoordenadaGeRegul);

                    bool respRegul = antecedentesSectorService.eliminarArchivoAdjuntoCoordenadaGeo(coordenadaGeograficaRegul);
                    if (respRegul)
                    {
                        Content_msgGrillaGral_4.Visible = true;
                        msgGrillaGral_4.Text = "Se ha eliminado exitosamente el archivo (Regularización).";
                    }
                    else
                    {
                        Content_msgGrillaGral_4.Visible = true;
                        msgGrillaGral_4.Text = "No se ha eliminado el archivo (Regularización).";
                    }

                    IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";

                    break;
            }
        }

        private void CambiarEstadoArchivo(string pestania, int idArchivo)
        {

            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();

            switch (pestania)
            {
                case "AntecedentesEspaciales":

                    ArchivoCoordenadaGeo archivoCoordenadaGeo = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);
                    if (archivoCoordenadaGeo.estado != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        archivoCoordenadaGeo.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (archivoCoordenadaGeo.estado != null && archivoCoordenadaGeo.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        archivoCoordenadaGeo.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    archivoCoordenadaGeo.modificaEstado = true;

                    CoordenadaGeografica coordenadaGeografica = new CoordenadaGeografica();

                    coordenadaGeografica.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);
                    coordenadaGeografica.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                    coordenadaGeografica.listaArchivoCoordGeo.Add(archivoCoordenadaGeo);

                    bool resp = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeografica);
                    if (resp)
                    {
                        TipoArchivoAntEspaciales.SelectedValue = "-1";
                        NombreArchivoAntEspaciales.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoAntEspacial.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoAntEspacial.DataBind();
                        GridArchivoAdjuntoAntEspacial.Visible = true;

                        msgGrillaGral_2.Text = "Se ha cambiado el estado del Archivo (Coord. Originales) exitosamente.";
                        Content_msgGrillaGral_2.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_2.Text = "No se ha cambiado el estado el Archivo (Coord. Originales).";
                        Content_msgGrillaGral_2.Visible = true;
                    }

                    IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    TipoArchivoAntEspaciales.Focus();

                    break;
                case "AntecedentesTerreno":

                    ArchivoCoordenadaGeo archivoCoordenadaGeoTerreno = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);
                    if (archivoCoordenadaGeoTerreno.estado != null && archivoCoordenadaGeoTerreno.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        archivoCoordenadaGeoTerreno.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (archivoCoordenadaGeoTerreno.estado != null && archivoCoordenadaGeoTerreno.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        archivoCoordenadaGeoTerreno.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    archivoCoordenadaGeoTerreno.modificaEstado = true;

                    CoordenadaGeografica coordenadaGeograficaTerreno = new CoordenadaGeografica();

                    coordenadaGeograficaTerreno.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);
                    coordenadaGeograficaTerreno.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                    coordenadaGeograficaTerreno.listaArchivoCoordGeo.Add(archivoCoordenadaGeoTerreno);

                    bool respTerreno = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeograficaTerreno);
                    if (respTerreno)
                    {
                        TipoArchivoAntTerreno.SelectedValue = "-1";
                        NombreArchivoAntTerreno.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeograficaTerreno.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoAntTerreno.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoAntTerreno.DataBind();
                        GridArchivoAdjuntoAntTerreno.Visible = true;

                        msgGrillaGral_3.Text = "Se ha cambiado el estado del Archivo (Coord. 14 TER) exitosamente.";
                        Content_msgGrillaGral_3.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_3.Text = "No se ha cambiado el estado el Archivo (Coord. 14 TER).";
                        Content_msgGrillaGral_3.Visible = true;
                    }

                    IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    TipoArchivoAntTerreno.Focus();

                    break;

                case "Regularizacion":

                    ArchivoCoordenadaGeo archivoCoordenadaGeoRegul = coordenadaGeograficaDA.ObtenerArchivoBinarioCoordenada(0, idArchivo);
                    if (archivoCoordenadaGeoRegul.estado != null && archivoCoordenadaGeoRegul.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        archivoCoordenadaGeoRegul.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (archivoCoordenadaGeoRegul.estado != null && archivoCoordenadaGeoRegul.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        archivoCoordenadaGeoRegul.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    archivoCoordenadaGeoRegul.modificaEstado = true;

                    CoordenadaGeografica coordenadaGeograficaRegul = new CoordenadaGeografica();

                    coordenadaGeograficaRegul.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);
                    coordenadaGeograficaRegul.listaArchivoCoordGeo = new List<ArchivoCoordenadaGeo>();
                    coordenadaGeograficaRegul.listaArchivoCoordGeo.Add(archivoCoordenadaGeoRegul);

                    bool respRegul = antecedentesSectorService.guardarArchivoAdjuntoCoordenadaGeo(coordenadaGeograficaRegul);
                    if (respRegul)
                    {
                        TipoArchivoRegularizacion.SelectedValue = "-1";
                        NombreArchivoRegularizacion.Text = "";

                        List<ArchivoCoordenadaGeo> listaArchivoBinario = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeograficaRegul.idCoordenadaGeo, 0);

                        GridArchivoAdjuntoRegularizacion.DataSource = listaArchivoBinario;
                        GridArchivoAdjuntoRegularizacion.DataBind();
                        GridArchivoAdjuntoRegularizacion.Visible = true;

                        msgGrillaGral_4.Text = "Se ha cambiado el estado del Archivo (Regularización) exitosamente.";
                        Content_msgGrillaGral_4.Visible = true;

                    }
                    else
                    {

                        msgGrillaGral_4.Text = "No se ha cambiado el estado el Archivo (Regularización).";
                        Content_msgGrillaGral_4.Visible = true;
                    }

                    IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    TipoArchivoRegularizacion.Focus();

                    break;
            }
        }

        private void CargaGrillaArchivo(string nombrePestania)
        {

            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<ArchivoCoordenadaGeo> List_ArchivosAntEspaciales = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);

                    GridArchivoAdjuntoAntEspacial.DataSource = List_ArchivosAntEspaciales;
                    GridArchivoAdjuntoAntEspacial.DataBind();
                    GridArchivoAdjuntoAntEspacial.Visible = true;
                    break;
                case "AntecedentesTerreno":

                    List<ArchivoCoordenadaGeo> List_ArchivosAntTerreno = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);

                    GridArchivoAdjuntoAntTerreno.DataSource = List_ArchivosAntTerreno;
                    GridArchivoAdjuntoAntTerreno.DataBind();
                    GridArchivoAdjuntoAntTerreno.Visible = true;
                    break;
                case "Regularizacion":

                    List<ArchivoCoordenadaGeo> List_ArchivosRegul = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);

                    GridArchivoAdjuntoRegularizacion.DataSource = List_ArchivosRegul;
                    GridArchivoAdjuntoRegularizacion.DataBind();
                    GridArchivoAdjuntoRegularizacion.Visible = true;
                    break;
            }
        }



        #endregion



        #region Poligono


        protected void GuardarPoligono_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPoligono("AntecedentesEspaciales");

            //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
            UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
            informacionSolicitud.Instance.RecargarInformacion();
            UpdatePanelInformacionSolictud.Update();
        }

        protected void GuardarPoligonoAntecedentesTerreno_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPoligono("AntecedentesTerreno");
        }

        protected void GuardarPoligonoRegul_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPoligono("Regularizacion");
        }



        protected void GridPoligonosAntecedentesEspaciales_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridPoligonoAntEspaciales = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Polígonos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridPoligonoAntEspaciales.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridPoligonosAntecedentesEspaciales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idPoligono = Convert.ToInt32(GridPoligonosAntecedentesEspaciales.DataKeys[e.Row.RowIndex].Value);

                // Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    //{
                    boton_ver.Visible = true;
                    //}
                };

                /* Solo si no tiene comparación con poligonos de inspección de terreno entonces puede ser borrado, modificado o cambiado de estado */
                ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, idPoligono);
                if (comparacionPoligono == null)
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                        {
                            boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Poligono?')");
                            boton_eliminar.Visible = true;
                        }
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_modificar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                        {
                            boton_modificar.Visible = true;
                        }
                    };

                    Poligono poligono = poligonoDA.ObtienePoligono(0, idPoligono);

                    if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Desasociar
                        ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                        if (boton_desasociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                            {
                                boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_desasociar.Visible = true;
                            }
                        };
                    }

                    else if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Asociar
                        ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                        if (boton_asociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ORIGINAL"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                            {
                                boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_asociar.Visible = true;
                            }
                        };

                    }
                }
            };
        }

        protected void GridPoligonosAntecedentesEspaciales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Accion.Value = Convert.ToString(rbAccion.NINGUNA);

            int idPoligono = 0;
            List<Poligono> listaPoligonos = null;
            CoordenadaGeografica coordenadaGeografica = null;

            idPoligono = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Eliminar":

                    GridPoligonosAntecedentesEspaciales.EditIndex = -1;
                    EliminarGrillaPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), idPoligono, "AntecedentesEspaciales");

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value));
                    ActualizaAreasTotal("AntecedentesEspaciales", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);
                    GridPoligonosAntecedentesEspaciales.DataSource = listaPoligonos;
                    GridPoligonosAntecedentesEspaciales.DataBind();
                    GridPoligonosAntecedentesEspaciales.Visible = true;

                    limpiarPoligono("AntecedentesEspaciales");

                    limpiarVertice("AntecedentesEspaciales");

                    IdPoligonoAntEspaciales.Value = Convert.ToString("0");
                    ocultarGrillaVerticeAntEspaciales();

                    //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                    UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                    informacionSolicitud.Instance.RecargarInformacion();
                    UpdatePanelInformacionSolictud.Update();

                    break;

                case "Modificar":

                    CargarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), idPoligono, "AntecedentesEspaciales");

                    TipoConcesionAntecedentesEspaciales.Enabled = true;
                    TipoUsoAntecedentesEspeciales.Enabled = true;
                    ToponimioAntecedentesEspeciales.Enabled = true;
                    AreaCalculadaAntecedentesEspeciales.Enabled = true;
                    AreaSolicitadaAntecedentesEspeciales.Enabled = true;

                    LimpiarVertice_Click(null, null);

                    break;

                case "Desasociar":

                    CambiarEstadoPoligono("AntecedentesEspaciales", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value));
                    ActualizaAreasTotal("AntecedentesEspaciales", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);

                    GridPoligonosAntecedentesEspaciales.DataSource = listaPoligonos;
                    GridPoligonosAntecedentesEspaciales.DataBind();
                    GridPoligonosAntecedentesEspaciales.Visible = true;

                    break;

                case "Asociar":

                    CambiarEstadoPoligono("AntecedentesEspaciales", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value));
                    ActualizaAreasTotal("AntecedentesEspaciales", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);

                    GridPoligonosAntecedentesEspaciales.DataSource = listaPoligonos;
                    GridPoligonosAntecedentesEspaciales.DataBind();
                    GridPoligonosAntecedentesEspaciales.Visible = true;

                    break;

                case "Ver":

                    CargarPoligonoVer(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), idPoligono, "AntecedentesEspaciales");

                    break;
            };
        }



        protected void GridPoligonoAntecedentesTerreno_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridPoligonoAntTerreno = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Polígonos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridPoligonoAntTerreno.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridPoligonoAntecedentesTerreno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idPoligono = Convert.ToInt32(GridPoligonoAntecedentesTerreno.DataKeys[e.Row.RowIndex].Value);

                // Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    //{
                    boton_ver.Visible = true;
                    //}
                };

                /* Solo si no tiene comparación con poligonos de inspección de terreno entonces puede ser borrado, modificado o cambiado de estado */
                ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, idPoligono);
                if (comparacionPoligono == null)
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                        {
                            boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Poligono?')");
                            boton_eliminar.Visible = true;
                        }
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_modificar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                        {
                            boton_modificar.Visible = true;
                        }
                    };

                    Poligono poligono = poligonoDA.ObtienePoligono(0, idPoligono);

                    if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Desasociar
                        ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                        if (boton_desasociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                            {
                                boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_desasociar.Visible = true;
                            }
                        };
                    }

                    else if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Asociar
                        ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                        if (boton_asociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_ENTREGA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                            {
                                boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_asociar.Visible = true;
                            }
                        };
                    }
                }
            };
        }

        protected void GridPoligonoAntecedentesTerreno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Accion2.Value = Convert.ToString(rbAccion.NINGUNA);

            int idPoligono = 0;
            List<Poligono> listaPoligonos = null;
            CoordenadaGeografica coordenadaGeografica = null;
            UpdatePanel UpdatePanelInformacionSolictud = null;

            switch (e.CommandName)
            {
                case "Eliminar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    GridPoligonoAntecedentesTerreno.EditIndex = -1;
                    EliminarGrillaPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), idPoligono, "AntecedentesTerreno");

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value));
                    ActualizaAreasTotal("AntecedentesTerreno", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);
                    GridPoligonoAntecedentesTerreno.DataSource = listaPoligonos;
                    GridPoligonoAntecedentesTerreno.DataBind();
                    GridPoligonoAntecedentesTerreno.Visible = true;

                    limpiarPoligono("AntecedentesTerreno");

                    limpiarVertice("AntecedentesTerreno");

                    IdPoligonoAntTerreno.Value = Convert.ToString("0");

                    ocultarGrillaVerticeAntTerreno();

                    break;

                case "Modificar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CargarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), idPoligono, "AntecedentesTerreno");

                    TipoConcesionAntecedentesTerreno.Enabled = true;
                    TipoUsoAntecedentesTerreno.Enabled = true;
                    ToponimioAntecedentesTerreno.Enabled = true;
                    AreaCalculadaAntecedentesTerreno.Enabled = true;
                    AreaSolicitadaAntecedentesTerreno.Enabled = true;

                    LimpiarVerticeAntTerreno_Click(null, null);

                    break;

                case "Desasociar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoPoligono("AntecedentesTerreno", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value));
                    ActualizaAreasTotal("AntecedentesTerreno", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);

                    GridPoligonoAntecedentesTerreno.DataSource = listaPoligonos;
                    GridPoligonoAntecedentesTerreno.DataBind();
                    GridPoligonoAntecedentesTerreno.Visible = true;

                    //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                    UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                    informacionSolicitud.Instance.RecargarInformacion();
                    UpdatePanelInformacionSolictud.Update();

                    break;

                case "Asociar":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoPoligono("AntecedentesTerreno", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value));
                    ActualizaAreasTotal("AntecedentesTerreno", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);

                    GridPoligonoAntecedentesTerreno.DataSource = listaPoligonos;
                    GridPoligonoAntecedentesTerreno.DataBind();
                    GridPoligonoAntecedentesTerreno.Visible = true;

                    //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                    UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                    informacionSolicitud.Instance.RecargarInformacion();
                    UpdatePanelInformacionSolictud.Update();

                    break;

                case "Ver":
                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CargarPoligonoVer(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), idPoligono, "AntecedentesTerreno");

                    break;

            };
        }



        protected void GridPoligonoRegul_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView GridPoligonoRegul = (GridView)sender;


                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Adding Ambito Column
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Polígonos";
                HeaderCell.CssClass = "customGeneralTitle";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 14;

                HeaderRow.Cells.Add(HeaderCell);

                //Adding the Row at the 0th position (first row) in the Grid
                GridPoligonoRegul.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }

        protected void GridPoligonoRegul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int idPoligono = Convert.ToInt32(GridPoligonoRegul.DataKeys[e.Row.RowIndex].Value);

                // Ver
                ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                if (boton_ver != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.VER))
                    //{
                    boton_ver.Visible = true;
                    //}
                };

                /* Solo si no tiene comparación con poligonos de inspección de terreno entonces puede ser borrado, modificado o cambiado de estado */
                ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, idPoligono);
                if (comparacionPoligono == null)
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                        {
                            boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Poligono?')");
                            boton_eliminar.Visible = true;
                        }
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_modificar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                        {
                            boton_modificar.Visible = true;
                        }
                    };

                    Poligono poligono = poligonoDA.ObtienePoligono(0, idPoligono);

                    if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        //Desasociar
                        ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                        if (boton_desasociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                            {
                                boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_desasociar.Visible = true;
                            }
                        };
                    }

                    else if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        //Asociar
                        ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                        if (boton_asociar != null)
                        {
                            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_POLIGONO_REGULARIZACION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                            {
                                boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Poligono?')");
                                boton_asociar.Visible = true;
                            }
                        };
                    }
                }
            };
        }

        protected void GridPoligonoRegul_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Accion.Value = Convert.ToString(rbAccion.NINGUNA);

            int idPoligono = 0;
            List<Poligono> listaPoligonos = null;

            switch (e.CommandName)
            {
                case "Eliminar":

                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    GridPoligonoRegul.EditIndex = -1;
                    EliminarGrillaPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), idPoligono, "Regularizacion");

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);
                    GridPoligonoRegul.DataSource = listaPoligonos;
                    GridPoligonoRegul.DataBind();
                    GridPoligonoRegul.Visible = true;

                    limpiarPoligono("Regularizacion");

                    limpiarVertice("Regularizacion");

                    IdPoligonoRegul.Value = Convert.ToString("0");

                    ocultarGrillaVerticeRegularizacion();

                    break;

                case "Modificar":

                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CargarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), idPoligono, "Regularizacion");

                    TipoConsecionRegularizacion.Enabled = true;
                    TipoUsoRegularizacion.Enabled = true;
                    ToponimioRegularizacion.Enabled = true;
                    AreaRegularizacion.Enabled = true;

                    LimpiarVerticeRegul_Click(null, null);

                    break;

                case "Desasociar":

                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoPoligono("Regularizacion", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);

                    GridPoligonoRegul.DataSource = listaPoligonos;
                    GridPoligonoRegul.DataBind();
                    GridPoligonoRegul.Visible = true;

                    break;

                case "Asociar":

                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CambiarEstadoPoligono("Regularizacion", idPoligono, Convert.ToInt32(IdSolicitud.Value));

                    listaPoligonos = poligonoDA.ListarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);

                    GridPoligonoRegul.DataSource = listaPoligonos;
                    GridPoligonoRegul.DataBind();
                    GridPoligonoRegul.Visible = true;

                    break;

                case "Ver":

                    idPoligono = Convert.ToInt32(e.CommandArgument);
                    CargarPoligonoVer(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), idPoligono, "Regularizacion");

                    break;
            };
        }



        private void AgregarGrillaPoligono(string nombrePestania)
        {

            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<Poligono> List_Poligonos = new List<Poligono>();
                    Poligono poligono = new Poligono();

                    poligono.idPoligono = Convert.ToInt32(IdPoligonoAntEspaciales.Value);
                    poligono.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
                    poligono.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);
                    poligono.tipoUso = new ParametroGenerico(Convert.ToInt32(TipoUsoAntecedentesEspeciales.SelectedValue), Convert.ToString(TipoUsoAntecedentesEspeciales.SelectedItem.Text));
                    poligono.toponimio = Convert.ToString(ToponimioAntecedentesEspeciales.Text);
                    poligono.areaCalculada = Convert.ToSingle(AreaCalculadaAntecedentesEspeciales.Text);
                    poligono.areaSolicitada = Convert.ToSingle(AreaSolicitadaAntecedentesEspeciales.Text);
                    poligono.areaRegularizacion = 0;
                    poligono.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);

                    if (Convert.ToInt32(IdPoligonoAntEspaciales.Value) > 0)
                    {
                        poligono.existePoligono = true;
                        poligono.poligonoOriginal = poligonoDA.ObtienePoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), Convert.ToInt32(IdPoligonoAntEspaciales.Value));

                    }

                    poligono.tipoConcesion = new List<ParametroGenerico>();

                    foreach (ListItem item in TipoConcesionAntecedentesEspaciales.Items)
                    {
                        if (item.Selected && Convert.ToInt32(item.Value) != -1)
                        {
                            ParametroGenerico tipoConcesion = new ParametroGenerico();
                            tipoConcesion.id = Convert.ToInt32(item.Value);
                            tipoConcesion.descripcion = Convert.ToString(item.Text);
                            poligono.tipoConcesion.Add(tipoConcesion);
                        }
                    }
                    List<Vertice> lista_vertices = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                    poligono.lista_vertices = lista_vertices;

                    List_Poligonos.Add(poligono);

                    List<String> listaErroresPoligono = antecedDelSectorValidacion.validaPoligonoAntEspaciales(poligono);

                    if (listaErroresPoligono != null && listaErroresPoligono.Count <= 0)
                    {
                        bool resp = antecedentesSectorService.guardarPoligono(poligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (resp)
                        {
                            ViewState["Vertices_AntEspaciales"] = null;
                            GridVerticeAntEspaciales.Visible = false;
                            TipoConcesionAntecedentesEspaciales.Focus();

                            /* Se eliminan los Vértices y sus datos del formulario */
                            limpiarVertice("AntecedentesEspaciales");
                            limpiarPoligono("AntecedentesEspaciales");

                            /* Se debe actualizar los campos del formulario y los registros de los campos en BD */

                            CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value));
                            //ActualizaAreasTotal("AntecedentesEspaciales", poligono.areaCalculada, poligono.areaSolicitada, poligono.areaRegularizacion);
                            ActualizaAreasTotal("AntecedentesEspaciales", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                            /* Se despliegan todos los Polígonos almacenados */
                            List<Poligono> listaPoligonos = antecedentesSectorService.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), 0);

                            GridPoligonosAntecedentesEspaciales.DataSource = listaPoligonos;
                            GridPoligonosAntecedentesEspaciales.DataBind();
                            GridPoligonosAntecedentesEspaciales.Visible = true;

                            msgGrillaGral_2.Text = "Se ha guardado el Poligono (Coord. Originales) exitosamente.";
                            Content_msgGrillaGral_2.Visible = true;

                            IdPoligonoAntEspaciales.Value = Convert.ToString("0");

                        }
                        else
                        {
                            msgGrillaGral_2.Text = "No se ha guardado el Polígono (Coord. Originales).";
                            Content_msgGrillaGral_2.Visible = true;
                        }
                        IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        UpdateAntecedentesEspeciales.Update();
                    }
                    else
                    {

                        foreach (String error in listaErroresPoligono)
                        {
                            Page.Validators.Add(new ValidationError("grupo5", error));
                        }
                        UpdateAntecedentesEspeciales.Update();
                    }

                    break;

                case "AntecedentesTerreno":

                    List<Poligono> List_PoligonosAntTerreno = new List<Poligono>();
                    Poligono poligonoAntTerreno = new Poligono();
                    poligonoAntTerreno.idPoligono = Convert.ToInt32(IdPoligonoAntTerreno.Value);
                    poligonoAntTerreno.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
                    poligonoAntTerreno.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);
                    poligonoAntTerreno.tipoUso = new ParametroGenerico(Convert.ToInt32(TipoUsoAntecedentesTerreno.SelectedValue), Convert.ToString(TipoUsoAntecedentesTerreno.SelectedItem.Text));
                    poligonoAntTerreno.toponimio = Convert.ToString(ToponimioAntecedentesTerreno.Text);
                    poligonoAntTerreno.areaCalculada = Convert.ToSingle(AreaCalculadaAntecedentesTerreno.Text);
                    poligonoAntTerreno.areaSolicitada = Convert.ToSingle(AreaSolicitadaAntecedentesTerreno.Text);
                    poligonoAntTerreno.areaRegularizacion = 0;
                    poligonoAntTerreno.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);

                    if (Convert.ToInt32(IdPoligonoAntTerreno.Value) > 0)
                    {
                        poligonoAntTerreno.existePoligono = true;
                        poligonoAntTerreno.poligonoOriginal = poligonoDA.ObtienePoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), Convert.ToInt32(IdPoligonoAntTerreno.Value));
                    }

                    poligonoAntTerreno.tipoConcesion = new List<ParametroGenerico>();

                    foreach (ListItem item in TipoConcesionAntecedentesTerreno.Items)
                    {
                        if (item.Selected && Convert.ToInt32(item.Value) != -1)
                        {
                            ParametroGenerico tipoConcesion = new ParametroGenerico();
                            tipoConcesion.id = Convert.ToInt32(item.Value);
                            tipoConcesion.descripcion = Convert.ToString(item.Text);
                            poligonoAntTerreno.tipoConcesion.Add(tipoConcesion);
                        }
                    }
                    List<Vertice> lista_verticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];
                    poligonoAntTerreno.lista_vertices = lista_verticesAntTerreno;

                    List_PoligonosAntTerreno.Add(poligonoAntTerreno);

                    List<String> listaErroresPoligonoTerreno = antecedDelSectorValidacion.validaPoligonoAntTerreno(poligonoAntTerreno);

                    if (listaErroresPoligonoTerreno != null && listaErroresPoligonoTerreno.Count <= 0)
                    {
                        Boolean respAntTerreno = antecedentesSectorService.guardarPoligono(poligonoAntTerreno, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (respAntTerreno)
                        {
                            /* Se eliminan los Vértices y sus datos del formulario */

                            ViewState["Vertices_AntTerreno"] = null;
                            GridVerticeAntTerreno.Visible = false;
                            TipoConcesionAntecedentesTerreno.Focus();

                            limpiarVertice("AntecedentesTerreno");
                            limpiarPoligono("AntecedentesTerreno");

                            CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value));
                            //ActualizaAreasTotal("AntecedentesTerreno", poligonoAntTerreno.areaCalculada, poligonoAntTerreno.areaSolicitada, poligonoAntTerreno.areaRegularizacion);
                            ActualizaAreasTotal("AntecedentesTerreno", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                            /* Se despliegan todos los Polígonos almacenados */
                            List<Poligono> listaPoligonos = antecedentesSectorService.ListarPoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), 0);

                            GridPoligonoAntecedentesTerreno.DataSource = listaPoligonos;
                            GridPoligonoAntecedentesTerreno.DataBind();
                            GridPoligonoAntecedentesTerreno.Visible = true;

                            msgGrillaGral_3.Text = "Se ha guardado el Poligono (Coord. 14 TER) exitosamente.";
                            Content_msgGrillaGral_3.Visible = true;

                            IdPoligonoAntTerreno.Value = Convert.ToString("0");

                        }
                        else
                        {
                            msgGrillaGral_3.Text = "No se ha guardado el Polígono (Coord. 14 TER).";
                            Content_msgGrillaGral_3.Visible = true;
                        }
                        IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        UpdateAntecedentesTerreno.Update();
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoTerreno)
                        {
                            Page.Validators.Add(new ValidationError("grupo8", error));
                        }
                        UpdateAntecedentesTerreno.Update();
                    }

                    break;

                case "Regularizacion":

                    List<Poligono> List_PoligonosRegularizacion = new List<Poligono>();
                    Poligono poligonoRegularizacion = new Poligono();
                    poligonoRegularizacion.idPoligono = Convert.ToInt32(IdPoligonoRegul.Value);
                    poligonoRegularizacion.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
                    poligonoRegularizacion.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);
                    poligonoRegularizacion.tipoUso = new ParametroGenerico(Convert.ToInt32(TipoUsoRegularizacion.SelectedValue), Convert.ToString(TipoUsoRegularizacion.SelectedItem.Text));
                    poligonoRegularizacion.toponimio = Convert.ToString(ToponimioRegularizacion.Text);
                    poligonoRegularizacion.areaRegularizacion = Convert.ToSingle(AreaRegularizacion.Text);
                    poligonoRegularizacion.areaCalculada = 0;
                    poligonoRegularizacion.areaSolicitada = 0;
                    poligonoRegularizacion.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);

                    if (Convert.ToInt32(IdPoligonoRegul.Value) > 0)
                    {
                        poligonoRegularizacion.existePoligono = true;
                        poligonoRegularizacion.poligonoOriginal = poligonoDA.ObtienePoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), Convert.ToInt32(IdPoligonoRegul.Value));
                    }

                    poligonoRegularizacion.tipoConcesion = new List<ParametroGenerico>();

                    foreach (ListItem item in TipoConsecionRegularizacion.Items)
                    {
                        if (item.Selected && Convert.ToInt32(item.Value) != -1)
                        {
                            ParametroGenerico tipoConcesion = new ParametroGenerico();
                            tipoConcesion.id = Convert.ToInt32(item.Value);
                            tipoConcesion.descripcion = Convert.ToString(item.Text);
                            poligonoRegularizacion.tipoConcesion.Add(tipoConcesion);
                        }
                    }
                    List<Vertice> lista_verticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];
                    poligonoRegularizacion.lista_vertices = lista_verticesRegularizacion;

                    List_PoligonosRegularizacion.Add(poligonoRegularizacion);

                    List<String> listaErroresPoligonoRegul = antecedDelSectorValidacion.validaPoligonoRegularizacion(poligonoRegularizacion);

                    if (listaErroresPoligonoRegul != null && listaErroresPoligonoRegul.Count <= 0)
                    {
                        Boolean respRegularizacion = antecedentesSectorService.guardarPoligono(poligonoRegularizacion, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (respRegularizacion)
                        {
                            /* Se eliminan los Vértices y sus datos del formulario */
                            ViewState["Vertices_Regularizacion"] = null;
                            GridVerticeRegularizacion.Visible = false;
                            TipoConsecionRegularizacion.Focus();

                            limpiarVertice("Regularizacion");
                            limpiarPoligono("Regularizacion");

                            CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(Convert.ToInt32(IdSolicitud.Value), Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value));
                            //ActualizaAreasTotal("Regularizacion", poligonoRegularizacion.areaCalculada, poligonoRegularizacion.areaSolicitada, poligonoRegularizacion.areaRegularizacion);
                            ActualizaAreasTotal("Regularizacion", coordenadaGeografica.areaTotalCalculada, coordenadaGeografica.areaTotalSolicitada, coordenadaGeografica.areaTotalRegularizacion);

                            /* Se despliegan todos los Polígonos almacenados */
                            List<Poligono> listaPoligonosRegul = antecedentesSectorService.ListarPoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), 0);

                            GridPoligonoRegul.DataSource = listaPoligonosRegul;
                            GridPoligonoRegul.DataBind();
                            GridPoligonoRegul.Visible = true;

                            msgGrillaGral_4.Text = "Se ha guardado el Poligono (Regularización) exitosamente.";
                            Content_msgGrillaGral_4.Visible = true;

                            IdPoligonoRegul.Value = Convert.ToString("0");
                        }
                        else
                        {
                            msgGrillaGral_4.Text = "No se ha guardado el Polígono (Regularización).";
                            Content_msgGrillaGral_4.Visible = true;
                        }

                        IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        UpdatePanelPanelRegularizacion.Update();
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoRegul)
                        {
                            Page.Validators.Add(new ValidationError("grupo12", error));
                        }
                        UpdatePanelPanelRegularizacion.Update();
                    }
                    break;
            }
        }

        private void CargarPoligono(int idCoordenadaGeo, int idPoligono, string pestana)
        {
            Poligono poligono = poligonoDA.ObtienePoligono(idCoordenadaGeo, idPoligono);
            switch (pestana)
            {
                case "AntecedentesEspaciales":


                    foreach (ListItem listItem in TipoConcesionAntecedentesEspaciales.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;
                            }

                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }
                    }

                    TipoUsoAntecedentesEspeciales.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    ToponimioAntecedentesEspeciales.Text = Convert.ToString(poligono.toponimio);
                    AreaCalculadaAntecedentesEspeciales.Text = Convert.ToString(poligono.areaCalculada);
                    AreaSolicitadaAntecedentesEspeciales.Text = Convert.ToString(poligono.areaSolicitada);
                    IdPoligonoAntEspaciales.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeoAntEspaciales.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_Vertices;

                    GridVerticeAntEspaciales.DataSource = List_Vertices;
                    GridVerticeAntEspaciales.DataBind();
                    GridVerticeAntEspaciales.Visible = true;

                    PanelBotonesPoligonoAntEspaciales.Visible = true;

                    PanelBotonesVerticeAntEspaciales.Visible = true;

                    break;

                case "AntecedentesTerreno":

                    TipoUsoAntecedentesTerreno.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    ToponimioAntecedentesTerreno.Text = Convert.ToString(poligono.toponimio);
                    AreaCalculadaAntecedentesTerreno.Text = Convert.ToString(poligono.areaCalculada);
                    AreaSolicitadaAntecedentesTerreno.Text = Convert.ToString(poligono.areaSolicitada);
                    IdPoligonoAntTerreno.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeoAntTerreno.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_VerticesAntTerreno = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_VerticesAntTerreno == null)
                    {
                        List_VerticesAntTerreno = new List<Vertice>();
                    }

                    ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;

                    GridVerticeAntTerreno.DataSource = List_VerticesAntTerreno;
                    GridVerticeAntTerreno.DataBind();
                    GridVerticeAntTerreno.Visible = true;


                    foreach (ListItem listItem in TipoConcesionAntecedentesTerreno.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;
                            }
                        }
                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }
                    }

                    break;

                case "Regularizacion":

                    TipoUsoRegularizacion.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    ToponimioRegularizacion.Text = Convert.ToString(poligono.toponimio);
                    AreaRegularizacion.Text = Convert.ToString(poligono.areaRegularizacion);
                    IdPoligonoRegul.Value = Convert.ToString(idPoligono);
                    IdReferenciaGeograficaRegularizacion.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_VerticesRegularizacion = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_VerticesRegularizacion == null)
                    {
                        List_VerticesRegularizacion = new List<Vertice>();
                    }

                    ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegularizacion;

                    GridVerticeRegularizacion.DataSource = List_VerticesRegularizacion;
                    GridVerticeRegularizacion.DataBind();
                    GridVerticeRegularizacion.Visible = true;


                    foreach (ListItem listItem in TipoConsecionRegularizacion.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;
                            }

                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }
                    }

                    break;
            }
        }

        private void CargarPoligonoVer(int idCoordenadaGeo, int idPoligono, string pestana)
        {
            Poligono poligono = poligonoDA.ObtienePoligono(idCoordenadaGeo, idPoligono);
            switch (pestana)
            {
                case "AntecedentesEspaciales":

                    foreach (ListItem listItem in TipoConcesionAntecedentesEspaciales.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;
                            }

                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }

                        listItem.Attributes.Add("disabled", "");
                    }
                    TipoConcesionAntecedentesEspaciales.Enabled = false;
                    TipoUsoAntecedentesEspeciales.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    TipoUsoAntecedentesEspeciales.Enabled = false;
                    ToponimioAntecedentesEspeciales.Text = Convert.ToString(poligono.toponimio);
                    ToponimioAntecedentesEspeciales.Enabled = false;
                    AreaCalculadaAntecedentesEspeciales.Text = Convert.ToString(poligono.areaCalculada);
                    AreaCalculadaAntecedentesEspeciales.Enabled = false;
                    AreaSolicitadaAntecedentesEspeciales.Text = Convert.ToString(poligono.areaSolicitada);
                    AreaSolicitadaAntecedentesEspeciales.Enabled = false;

                    IdPoligonoAntEspaciales.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeoAntEspaciales.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_Vertices;

                    Accion.Value = Convert.ToString(rbAccion.VER);

                    GridVerticeAntEspaciales.DataSource = List_Vertices;
                    GridVerticeAntEspaciales.DataBind();
                    GridVerticeAntEspaciales.Visible = true;

                    PanelBotonesPoligonoAntEspaciales.Visible = false;
                    PanelBotonesVerticeAntEspaciales.Visible = false;

                    break;

                case "AntecedentesTerreno":

                    foreach (ListItem listItem in TipoConcesionAntecedentesTerreno.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;

                            }

                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }

                        listItem.Attributes.Add("disabled", "");
                    }

                    TipoConcesionAntecedentesTerreno.Enabled = false;
                    TipoUsoAntecedentesTerreno.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    TipoUsoAntecedentesTerreno.Enabled = false;
                    ToponimioAntecedentesTerreno.Text = Convert.ToString(poligono.toponimio);
                    ToponimioAntecedentesTerreno.Enabled = false;
                    AreaCalculadaAntecedentesTerreno.Text = Convert.ToString(poligono.areaCalculada);
                    AreaCalculadaAntecedentesTerreno.Enabled = false;
                    AreaSolicitadaAntecedentesTerreno.Text = Convert.ToString(poligono.areaSolicitada);
                    AreaSolicitadaAntecedentesTerreno.Enabled = false;

                    IdPoligonoAntTerreno.Value = Convert.ToString(idPoligono);
                    IdCoordenadaGeoAntTerreno.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_VerticesAntTerreno = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_VerticesAntTerreno == null)
                    {
                        List_VerticesAntTerreno = new List<Vertice>();
                    }

                    ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;

                    Accion2.Value = Convert.ToString(rbAccion.VER);

                    GridVerticeAntTerreno.DataSource = List_VerticesAntTerreno;
                    GridVerticeAntTerreno.DataBind();
                    GridVerticeAntTerreno.Visible = true;

                    PanelBotonesAntecedentesTerreno.Visible = false;
                    PanelBotonesVerticeAntTerreno.Visible = false;

                    break;

                case "Regularizacion":

                    foreach (ListItem listItem in TipoConsecionRegularizacion.Items)
                    {
                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                        {
                            if (tipoConcesion != null && listItem.Value.Equals(Convert.ToString(tipoConcesion.id)))
                            {
                                listItem.Selected = true;

                            }

                        }

                        if (listItem.Value.Equals(Convert.ToString("-1")))
                        {
                            listItem.Selected = false;
                        }

                        listItem.Attributes.Add("disabled", "");
                    }
                    TipoConsecionRegularizacion.Enabled = false;
                    TipoUsoRegularizacion.SelectedValue = Convert.ToString(poligono.tipoUso.id);
                    TipoUsoRegularizacion.Enabled = false;
                    ToponimioRegularizacion.Text = Convert.ToString(poligono.toponimio);
                    ToponimioRegularizacion.Enabled = false;
                    AreaRegularizacion.Text = Convert.ToString(poligono.areaRegularizacion);
                    AreaRegularizacion.Enabled = false;

                    IdPoligonoRegul.Value = Convert.ToString(idPoligono);

                    IdReferenciaGeograficaRegularizacion.Value = Convert.ToString(idCoordenadaGeo);

                    List<Vertice> List_VerticesRegul = verticeDA.ListarVertice(idPoligono, 0);

                    if (List_VerticesRegul == null)
                    {
                        List_VerticesRegul = new List<Vertice>();
                    }

                    ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegul;

                    Accion3.Value = Convert.ToString(rbAccion.VER);

                    GridVerticeRegularizacion.DataSource = List_VerticesRegul;
                    GridVerticeRegularizacion.DataBind();
                    GridVerticeRegularizacion.Visible = true;

                    PanelBotonesPoligonosRegul.Visible = false;

                    PanelBotonesVerticesRegul.Visible = false;

                    break;
            }

        }

        private void CambiarEstadoPoligono(string pestana, int idPoligono, int idSolicitud)
        {

            switch (pestana)
            {
                case "AntecedentesEspaciales":

                    Poligono poligono = poligonoDA.ObtienePoligono(Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value), idPoligono);
                    if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        poligono.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        poligono.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    poligono.existePoligono = true;
                    poligono.cambiaEstado = true;
                    poligono.idSolicitud = idSolicitud;
                    poligono.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntEspaciales.Value);

                    List<String> listaErroresPoligono = antecedDelSectorValidacion.validaPoligonoAntEspaciales(poligono);

                    if (listaErroresPoligono.Count <= 0)
                    {
                        bool resp = antecedentesSectorService.guardarPoligono(poligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                        if (resp)
                        {
                            Content_msgGrillaGral_2.Visible = true;
                            msgGrillaGral_2.Text = "Se ha cambiado el estado del Poligono (Ant. Espaciales) exitosamente.";
                        }
                        else
                        {
                            Content_msgGrillaGral_2.Visible = true;
                            msgGrillaGral_2.Text = "No se ha cambiado el estado del Poligono (Ant. Espaciales).";
                        }

                        IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {

                        foreach (String error in listaErroresPoligono)
                        {
                            Page.Validators.Add(new ValidationError("grupo5", error));
                        }

                    }


                    break;
                case "AntecedentesTerreno":

                    Poligono poligonoAntTerreno = poligonoDA.ObtienePoligono(Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value), idPoligono);
                    if (poligonoAntTerreno.estado != null && poligonoAntTerreno.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        poligonoAntTerreno.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (poligonoAntTerreno.estado != null && poligonoAntTerreno.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        poligonoAntTerreno.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    poligonoAntTerreno.existePoligono = true;
                    poligonoAntTerreno.cambiaEstado = true;
                    poligonoAntTerreno.idSolicitud = idSolicitud;
                    poligonoAntTerreno.idCoordenadaGeo = Convert.ToInt32(IdCoordenadaGeoAntTerreno.Value);

                    List<String> listaErroresPoligonoAnTerreno = antecedDelSectorValidacion.validaPoligonoAntTerreno(poligonoAntTerreno);

                    if (listaErroresPoligonoAnTerreno.Count <= 0)
                    {

                        bool respAntTerreno = antecedentesSectorService.guardarPoligono(poligonoAntTerreno, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        if (respAntTerreno)
                        {
                            Content_msgGrillaGral_3.Visible = true;
                            msgGrillaGral_3.Text = "Se ha cambiado el estado del Poligono (Ant. Terreno) exitosamente.";
                        }
                        else
                        {
                            Content_msgGrillaGral_3.Visible = true;
                            msgGrillaGral_3.Text = "No se ha cambiado el estado del Poligono (Ant. Terreno).";
                        }

                        IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoAnTerreno)
                        {
                            Page.Validators.Add(new ValidationError("grupo8", error));
                        }

                    }
                    break;

                case "Regularizacion":

                    Poligono poligonoRegul = poligonoDA.ObtienePoligono(Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value), idPoligono);
                    if (poligonoRegul.estado != null && poligonoRegul.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        poligonoRegul.estado.id = rbEstadosGenerales.NO_VIGENTE;
                    }
                    else if (poligonoRegul.estado != null && poligonoRegul.estado.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        poligonoRegul.estado.id = rbEstadosGenerales.VIGENTE;
                    }

                    poligonoRegul.existePoligono = true;
                    poligonoRegul.cambiaEstado = true;
                    poligonoRegul.idSolicitud = idSolicitud;
                    poligonoRegul.idCoordenadaGeo = Convert.ToInt32(IdReferenciaGeograficaRegularizacion.Value);

                    List<String> listaErroresPoligonoRegul = antecedDelSectorValidacion.validaPoligonoRegularizacion(poligonoRegul);

                    if (listaErroresPoligonoRegul.Count <= 0)
                    {
                        bool respRegul = antecedentesSectorService.guardarPoligono(poligonoRegul, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        if (respRegul)
                        {
                            Content_msgGrillaGral_4.Visible = true;
                            msgGrillaGral_4.Text = "Se ha cambiado el estado del Poligono (Regularización) exitosamente.";
                        }
                        else
                        {
                            Content_msgGrillaGral_4.Visible = true;
                            msgGrillaGral_4.Text = "No se ha cambiado el estado del Poligono (Regularización).";
                        }

                        IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoRegul)
                        {
                            Page.Validators.Add(new ValidationError("grupo12", error));
                        }
                    }
                    break;
            }
        }

        protected void EliminarGrillaPoligono(int idCoordenadaGeo, int idPoligono, String nombrePestania)
        {

            Poligono poligono = new Poligono();
            poligono.idPoligono = idPoligono;
            poligono.accion = accion.ELIMINAR;
            poligono.idSolicitud = Convert.ToInt32(IdSolicitud.Value);

            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<String> listaErroresPoligono = antecedDelSectorValidacion.validaPoligonoAntEspaciales(poligono);

                    if (listaErroresPoligono.Count <= 0)
                    {
                        bool resp = antecedentesSectorService.eliminarPoligonoCoordenadaGeo(Convert.ToInt32(IdSolicitud.Value), idCoordenadaGeo, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        if (resp)
                        {
                            Content_msgGrillaGral_2.Visible = true;
                            msgGrillaGral_2.Text = "Se ha eliminado exitosamente el Poligono (Ant. Espaciales).";

                        }
                        else
                        {
                            Content_msgGrillaGral_2.Visible = true;
                            msgGrillaGral_2.Text = "No se ha eliminado el Poligono (Ant. Espaciales).";
                        }

                        IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {

                        foreach (String error in listaErroresPoligono)
                        {
                            Page.Validators.Add(new ValidationError("grupo5", error));
                        }
                    }

                    break;

                case "AntecedentesTerreno":

                    List<String> listaErroresPoligonoAntTerreno = antecedDelSectorValidacion.validaPoligonoAntTerreno(poligono);

                    if (listaErroresPoligonoAntTerreno.Count <= 0)
                    {

                        bool respAntTerreno = antecedentesSectorService.eliminarPoligonoCoordenadaGeo(Convert.ToInt32(IdSolicitud.Value), idCoordenadaGeo, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        if (respAntTerreno)
                        {
                            Content_msgGrillaGral_3.Visible = true;
                            msgGrillaGral_3.Text = "Se ha eliminado exitosamente el Poligono (Ant. Terreno).";
                        }
                        else
                        {
                            Content_msgGrillaGral_3.Visible = true;
                            msgGrillaGral_3.Text = "No se ha eliminado el Poligono (Ant. Terreno).";
                        }

                        IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoAntTerreno)
                        {
                            Page.Validators.Add(new ValidationError("grupo8", error));
                        }
                    }

                    break;
                case "Regularizacion":

                    List<String> listaErroresPoligonoRegul = antecedDelSectorValidacion.validaPoligonoRegularizacion(poligono);

                    if (listaErroresPoligonoRegul.Count <= 0)
                    {

                        bool respRegul = antecedentesSectorService.eliminarPoligonoCoordenadaGeo(Convert.ToInt32(IdSolicitud.Value), idCoordenadaGeo, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                        if (respRegul)
                        {
                            Content_msgGrillaGral_4.Visible = true;
                            msgGrillaGral_4.Text = "Se ha eliminado exitosamente el Poligono (Regularización).";
                        }
                        else
                        {
                            Content_msgGrillaGral_4.Visible = true;
                            msgGrillaGral_4.Text = "No se ha eliminado el Poligono (Regularización).";
                        }

                        IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                    }
                    else
                    {
                        foreach (String error in listaErroresPoligonoRegul)
                        {
                            Page.Validators.Add(new ValidationError("grupo12", error));
                        }
                    }
                    break;
            }
        }


        #endregion



        #region Vertice


        protected void GuardarVerticeAntecedentesEspeciales_Click(object sender, ImageClickEventArgs e)
        {
            bool agregar = AgregarGrillaVertice("AntecedentesEspaciales");

            if (agregar)
            {
                CargaGrillaVertice("AntecedentesEspaciales");
            }

        }

        protected void GuardarVerticeAntTerreno_Click(object sender, ImageClickEventArgs e)
        {
            bool agregar = AgregarGrillaVertice("AntecedentesTerreno");

            if (agregar)
            {
                CargaGrillaVertice("AntecedentesTerreno");
            }
        }

        protected void GuardarVerticeRegul_Click(object sender, ImageClickEventArgs e)
        {
            bool agregar = AgregarGrillaVertice("Regularizacion");

            if (agregar)
            {
                CargaGrillaVertice("Regularizacion");
            }

        }


        protected void GridVerticeAntEspaciales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');


            int idVertice = 0;
            int index = 0;

            if (arg != null && arg.Length > 1)
            {

                idVertice = Convert.ToInt32(arg[0]);
                index = Convert.ToInt32(arg[1]);

            }


            switch (e.CommandName)
            {
                case "Ver":
                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeAntecedentesEspeciales.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        VerticeAntecedentesEspeciales.Enabled = false;

                        LatitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudHoraAntecedentesEspeciales.Enabled = false;

                        LatitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        LatitudMinutoAntecedentesEspeciales.Enabled = false;

                        LatitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);
                        LatitudSegundoAntecedentesEspeciales.Enabled = false;

                        //LatitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        //LatitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificar.latitudDecimal);

                        LongitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudHoraAntecedentesEspeciales.Enabled = false;

                        LongitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        LongitudMinutoAntecedentesEspeciales.Enabled = false;

                        LongitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);
                        LongitudSegundoAntecedentesEspeciales.Enabled = false;

                        //LongitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        //LongitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificar.longitudDecimal);

                        UTMEAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.utmE);
                        UTMEAntecedentesEspeciales.Enabled = false;

                        UtmNAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.utmN);
                        UtmNAntecedentesEspeciales.Enabled = false;

                        IdVerticeAntEspaciales.Value = Convert.ToString(idVertice);

                        PanelBotonesVerticeAntEspaciales.Visible = false;

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeAntecedentesEspeciales.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            VerticeAntecedentesEspeciales.Enabled = false;

                            LatitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudHoraAntecedentesEspeciales.Enabled = false;

                            LatitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudMinuto);
                            LatitudMinutoAntecedentesEspeciales.Enabled = false;

                            LatitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);
                            LatitudSegundoAntecedentesEspeciales.Enabled = false;

                            //LatitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            //LatitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificado.latitudDecimal);

                            LongitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudHoraAntecedentesEspeciales.Enabled = false;

                            LongitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudMinuto);
                            LongitudMinutoAntecedentesEspeciales.Enabled = false;

                            LongitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);
                            LongitudSegundoAntecedentesEspeciales.Enabled = false;

                            //LongitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            //LongitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificado.longitudDecimal);

                            UTMEAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmE);
                            UTMEAntecedentesEspeciales.Enabled = false;

                            UtmNAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmN);
                            UtmNAntecedentesEspeciales.Enabled = false;

                            IdVerticeAntEspaciales.Value = Convert.ToString("0");

                            PanelBotonesVerticeAntEspaciales.Visible = false;
                        }
                    }

                    break;

                case "Eliminar":

                    if (idVertice > 0)
                    {
                        //int indexEliminar = Convert.ToInt32(e.CommandArgument);
                        GridVerticeAntEspaciales.EditIndex = -1;
                        EliminarGrillaVertice(Convert.ToInt32(IdPoligonoAntEspaciales.Value), idVertice, "AntecedentesEspaciales");
                        CargaGrillaVertice("AntecedentesEspaciales");
                    }
                    else
                    {

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                List_VerticesAntEspaciales.Remove(vertice);
                                break;
                            }
                        }

                        GridVerticeAntEspaciales.DataSource = List_VerticesAntEspaciales;
                        GridVerticeAntEspaciales.DataBind();
                        GridVerticeAntEspaciales.Visible = true;

                        ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_VerticesAntEspaciales;
                    }

                    break;

                case "Modificar":

                    //int indexModificar = Convert.ToInt32(e.CommandArgument);

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.idVertice.Equals(Convert.ToInt32(idVertice)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }

                        if (verticeModificado != null)
                        {
                            VerticeAntecedentesEspeciales.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);

                            LongitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);

                            UTMEAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmE);
                            UtmNAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmN);

                        }

                        /*
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeAntecedentesEspeciales.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        LatitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        
                        LatitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);

                        //LatitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        //LatitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificar.latitudDecimal);

                        LongitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudMinuto);

                        LongitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);

                        //LongitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        //LongitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificar.longitudDecimal);

                        UTMEAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.utmE);
                        UtmNAntecedentesEspeciales.Text = Convert.ToString(verticeModificar.utmN);
                        */

                        IdVerticeAntEspaciales.Value = Convert.ToString(idVertice);

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeAntecedentesEspeciales.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesEspeciales);

                            //LatitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            //LatitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificado.latitudDecimal);

                            LongitudHoraAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesEspeciales);

                            //LongitudAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            //LongitudAntecedentesEspecialesReadOnly.Text = Convert.ToString(verticeModificado.longitudDecimal);

                            UTMEAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmE);
                            UtmNAntecedentesEspeciales.Text = Convert.ToString(verticeModificado.utmN);

                            IdVerticeAntEspaciales.Value = Convert.ToString("0");
                        }
                    }

                    PanelBotonesVerticeAntEspaciales.Visible = true;
                    break;
            };
        }

        protected void GridVerticeAntEspaciales_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridVerticeAntEspaciales.PageIndex = e.NewPageIndex;
            GridVerticeAntEspaciales.DataBind();
            CargaGrillaVertice("AntecedentesEspaciales");
        }

        protected void GridVerticeAntEspaciales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, Convert.ToInt32(IdPoligonoAntEspaciales.Value));
                //if (comparacionPoligono != null && Convert.ToInt32(IdPoligonoAntEspaciales.Value) > 0)

                int accion = Convert.ToInt32(Accion.Value);
                if (accion == rbAccion.VER)
                {
                    // Ver
                    ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                    if (boton_ver != null)
                    {
                        boton_ver.Visible = true;
                    };
                }
                else
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Vértice?')");
                        boton_eliminar.Visible = true;
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_eliminar != null)
                    {
                        boton_modificar.Visible = true;
                    };
                }

            }
        }


        protected void GridVerticeAntTerreno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');


            int idVertice = 0;
            int index = 0;

            if (arg != null && arg.Length > 1)
            {

                idVertice = Convert.ToInt32(arg[0]);
                index = Convert.ToInt32(arg[1]);

            }

            switch (e.CommandName)
            {
                case "Ver":

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeAntecedentesTerreno.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        VerticeAntecedentesTerreno.Enabled = false;

                        LatitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudHoraAntecedentesTerreno.Enabled = false;

                        LatitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        LatitudMinutoAntecedentesTerreno.Enabled = false;

                        LatitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoAntecedentesTerreno);
                        LatitudSegundoAntecedentesTerreno.Enabled = false;

                        //LatitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        //LatitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificar.latitudDecimal);

                        LongitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudHoraAntecedentesTerreno.Enabled = false;

                        LongitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        LongitudMinutoAntecedentesTerreno.Enabled = false;

                        LongitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoAntecedentesTerreno);
                        LongitudSegundoAntecedentesTerreno.Enabled = false;

                        //LongitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        //LongitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificar.longitudDecimal);

                        UTMEAntecedentesTerreno.Text = Convert.ToString(verticeModificar.utmE);
                        UTMEAntecedentesTerreno.Enabled = false;

                        UTMNAntecedentesTerreno.Text = Convert.ToString(verticeModificar.utmN);
                        UTMNAntecedentesTerreno.Enabled = false;

                        IdVerticeAntTerreno.Value = Convert.ToString(idVertice);

                        PanelBotonesVerticeAntTerreno.Visible = false;

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {

                            VerticeAntecedentesTerreno.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            VerticeAntecedentesTerreno.Enabled = false;

                            LatitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudHoraAntecedentesTerreno.Enabled = false;

                            LatitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudMinuto);
                            LatitudMinutoAntecedentesTerreno.Enabled = false;

                            LatitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesTerreno);
                            LatitudSegundoAntecedentesTerreno.Enabled = false;

                            //LatitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            //LatitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificado.latitudDecimal);

                            LongitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudHoraAntecedentesTerreno.Enabled = false;

                            LongitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudMinuto);
                            LongitudMinutoAntecedentesTerreno.Enabled = false;

                            LongitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesTerreno);
                            LongitudSegundoAntecedentesTerreno.Enabled = false;

                            //LongitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            //LongitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificado.longitudDecimal);

                            UTMEAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmE);
                            UTMEAntecedentesTerreno.Enabled = false;

                            UTMNAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmN);
                            UTMNAntecedentesTerreno.Enabled = false;

                            IdVerticeAntTerreno.Value = Convert.ToString("0");

                            PanelBotonesVerticeAntTerreno.Visible = false;
                        }
                    }

                    break;

                case "Eliminar":

                    if (idVertice > 0)
                    {
                        //int indexEliminar = Convert.ToInt32(e.CommandArgument);
                        GridVerticeAntTerreno.EditIndex = -1;
                        EliminarGrillaVertice(Convert.ToInt32(IdPoligonoAntTerreno.Value), idVertice, "AntecedentesTerreno");
                        CargaGrillaVertice("AntecedentesTerreno");
                    }
                    else
                    {

                        List<Vertice> List_VerticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];
                        foreach (Vertice vertice in List_VerticesAntTerreno)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                List_VerticesAntTerreno.Remove(vertice);
                                break;
                            }
                        }

                        GridVerticeAntTerreno.DataSource = List_VerticesAntTerreno;
                        GridVerticeAntTerreno.DataBind();
                        GridVerticeAntTerreno.Visible = true;

                        ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;
                    }

                    break;

                case "Modificar":

                    //int indexModificar = Convert.ToInt32(e.CommandArgument);

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        List<Vertice> List_VerticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntTerreno)
                        {
                            if (vertice.idVertice.Equals(Convert.ToInt32(idVertice)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeAntecedentesTerreno.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesTerreno);

                            LongitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesTerreno);

                            UTMEAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmE);
                            UTMNAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmN);

                        }
                        /*
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeAntecedentesTerreno.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        LatitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        
                        LatitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoAntecedentesTerreno);

                        //LatitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        //LatitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        
                        LongitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        
                        LongitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoAntecedentesTerreno);

                        //LongitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        //LongitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificar.longitudDecimal);

                        UTMEAntecedentesTerreno.Text = Convert.ToString(verticeModificar.utmE);
                        UTMNAntecedentesTerreno.Text = Convert.ToString(verticeModificar.utmN);
                        */

                        IdVerticeAntTerreno.Value = Convert.ToString(idVertice);

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntTerreno)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeAntecedentesTerreno.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoAntecedentesTerreno);

                            //LatitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            //LatitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificado.latitudDecimal);

                            LongitudHoraAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoAntecedentesTerreno);

                            //LongitudDecimalAntecedentesTerreno.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            //LongitudDecimalAntecedentesTerrenoReadOnly.Text = Convert.ToString(verticeModificado.longitudDecimal);

                            UTMEAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmE);
                            UTMNAntecedentesTerreno.Text = Convert.ToString(verticeModificado.utmN);

                            IdVerticeAntTerreno.Value = Convert.ToString("0");
                        }
                    }

                    PanelBotonesVerticeAntTerreno.Visible = true;
                    break;
            };

        }

        protected void GridVerticeAntTerreno_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridVerticeAntTerreno.PageIndex = e.NewPageIndex;
            GridVerticeAntTerreno.DataBind();
            CargaGrillaVertice("AntecedentesTerreno");
        }

        protected void GridVerticeAntTerreno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, Convert.ToInt32(IdPoligonoAntTerreno.Value));

                //if (comparacionPoligono != null && Convert.ToInt32(IdPoligonoAntTerreno.Value) > 0)
                int accion2 = Convert.ToInt32(Accion2.Value);
                if (accion2 == rbAccion.VER)
                {
                    // Ver
                    ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                    if (boton_ver != null)
                    {
                        boton_ver.Visible = true;
                    };

                }
                else
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Vértice?')");
                        boton_eliminar.Visible = true;
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_eliminar != null)
                    {
                        boton_modificar.Visible = true;
                    };
                }
            };
        }


        protected void GridVerticeRegularizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');


            int idVertice = 0;
            int index = 0;

            if (arg != null && arg.Length > 1)
            {

                idVertice = Convert.ToInt32(arg[0]);
                index = Convert.ToInt32(arg[1]);
            }

            switch (e.CommandName)
            {
                case "Ver":

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeRegularizacion.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        VerticeRegularizacion.Enabled = false;

                        LatitudHoraRegularizacion.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudHoraRegularizacion.Enabled = false;

                        LatitudMinutoRegularizacion.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        LatitudMinutoRegularizacion.Enabled = false;

                        LatitudSegundoRegularizacion.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoRegularizacion);
                        LatitudSegundoRegularizacion.Enabled = false;

                        //LatitudDecimalRegularizacion.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        //LatitudDecimalRegularizacionReadOnly.Text = Convert.ToString(verticeModificar.latitudDecimal);

                        LongitudHoraRegularizacion.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudHoraRegularizacion.Enabled = false;

                        LongitudMinutoRegularizacion.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        LongitudMinutoRegularizacion.Enabled = false;

                        LongitudSegundoRegularizacion.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoRegularizacion);
                        LongitudSegundoRegularizacion.Enabled = false;

                        //LongitudDecimalRegularizacion.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        //LongitudDecimalRegularizacionReadOnly.Text = Convert.ToString(verticeModificar.longitudDecimal);

                        UTMERegularizacion.Text = Convert.ToString(verticeModificar.utmE);
                        UTMERegularizacion.Enabled = false;

                        UTMNRegularizacion.Text = Convert.ToString(verticeModificar.utmN);
                        UTMNRegularizacion.Enabled = false;

                        IdVerticeRegul.Value = Convert.ToString(idVertice);

                        PanelBotonesVerticesRegul.Visible = false;

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesAntEspaciales)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeRegularizacion.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            VerticeRegularizacion.Enabled = false;

                            LatitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudHoraRegularizacion.Enabled = false;

                            LatitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.latitudMinuto);
                            LatitudMinutoRegularizacion.Enabled = false;

                            LatitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoRegularizacion);
                            LatitudSegundoRegularizacion.Enabled = false;

                            //LatitudDecimalRegularizacion.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            //LatitudDecimalRegularizacionReadOnly.Text = Convert.ToString(verticeModificado.latitudDecimal);

                            LongitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudHoraRegularizacion.Enabled = false;

                            LongitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.longitudMinuto);
                            LongitudMinutoRegularizacion.Enabled = false;

                            LongitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoRegularizacion);
                            LongitudSegundoRegularizacion.Enabled = false;

                            //LongitudDecimalRegularizacion.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            //LongitudDecimalRegularizacionReadOnly.Text = Convert.ToString(verticeModificado.longitudDecimal);

                            UTMERegularizacion.Text = Convert.ToString(verticeModificado.utmE);
                            UTMERegularizacion.Enabled = false;

                            UTMNRegularizacion.Text = Convert.ToString(verticeModificado.utmN);
                            UTMNRegularizacion.Enabled = false;

                            IdVerticeRegul.Value = Convert.ToString("0");

                            PanelBotonesVerticesRegul.Visible = false;
                        }
                    }

                    break;

                case "Eliminar":

                    if (idVertice > 0)
                    {
                        //int indexEliminar = Convert.ToInt32(e.CommandArgument);
                        GridVerticeRegularizacion.EditIndex = -1;
                        EliminarGrillaVertice(Convert.ToInt32(IdPoligonoRegul.Value), idVertice, "Regularizacion");
                        CargaGrillaVertice("Regularizacion");
                    }
                    else
                    {

                        List<Vertice> List_VerticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];
                        foreach (Vertice vertice in List_VerticesRegularizacion)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                List_VerticesRegularizacion.Remove(vertice);
                                break;
                            }
                        }

                        GridVerticeRegularizacion.DataSource = List_VerticesRegularizacion;
                        GridVerticeRegularizacion.DataBind();
                        GridVerticeRegularizacion.Visible = true;

                        ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegularizacion;
                    }

                    break;

                case "Modificar":

                    //int indexModificar = Convert.ToInt32(e.CommandArgument);

                    if (idVertice > 0) //Si existe en la Base de Datos
                    {
                        List<Vertice> List_VerticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesRegularizacion)
                        {
                            if (vertice.idVertice.Equals(Convert.ToInt32(idVertice)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeRegularizacion.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoRegularizacion);

                            LongitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoRegularizacion);

                            UTMERegularizacion.Text = Convert.ToString(verticeModificado.utmE);
                            UTMNRegularizacion.Text = Convert.ToString(verticeModificado.utmN);

                        }

                        /*
                        Vertice verticeModificar = verticeDA.ObtieneVertice(0, idVertice);

                        VerticeRegularizacion.SelectedValue = Convert.ToString(verticeModificar.vertice.id);
                        LatitudHoraRegularizacion.Text = Convert.ToString(verticeModificar.latitudHora);
                        LatitudMinutoRegularizacion.Text = Convert.ToString(verticeModificar.latitudMinuto);
                        
                        LatitudSegundoRegularizacion.Text = Convert.ToString(verticeModificar.latitudSegundo);
                        completarConCerosCampo(LatitudSegundoRegularizacion);
                        
                        //LatitudDecimalRegularizacion.Text = Convert.ToString(verticeModificar.latitudDecimal);
                        LongitudHoraRegularizacion.Text = Convert.ToString(verticeModificar.longitudHora);
                        LongitudMinutoRegularizacion.Text = Convert.ToString(verticeModificar.longitudMinuto);
                        
                        LongitudSegundoRegularizacion.Text = Convert.ToString(verticeModificar.longitudSegundo);
                        completarConCerosCampo(LongitudSegundoRegularizacion);
                        
                        //LongitudDecimalRegularizacion.Text = Convert.ToString(verticeModificar.longitudDecimal);
                        UTMERegularizacion.Text = Convert.ToString(verticeModificar.utmE);
                        UTMNRegularizacion.Text = Convert.ToString(verticeModificar.utmN);

                         */


                        IdVerticeRegul.Value = Convert.ToString(idVertice);

                    }
                    else
                    { //Si no existe en la BD

                        List<Vertice> List_VerticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];
                        Vertice verticeModificado = null;
                        foreach (Vertice vertice in List_VerticesRegularizacion)
                        {
                            if (vertice.index.Equals(Convert.ToInt32(index)))
                            {
                                verticeModificado = vertice;
                                break;
                            }
                        }
                        if (verticeModificado != null)
                        {
                            VerticeRegularizacion.SelectedValue = Convert.ToString(verticeModificado.vertice.id);
                            LatitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.latitudHora);
                            LatitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.latitudMinuto);

                            LatitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.latitudSegundo);
                            completarConCerosCampo(LatitudSegundoRegularizacion);

                            //LatitudDecimalRegularizacion.Text = Convert.ToString(verticeModificado.latitudDecimal);
                            LongitudHoraRegularizacion.Text = Convert.ToString(verticeModificado.longitudHora);
                            LongitudMinutoRegularizacion.Text = Convert.ToString(verticeModificado.longitudMinuto);

                            LongitudSegundoRegularizacion.Text = Convert.ToString(verticeModificado.longitudSegundo);
                            completarConCerosCampo(LongitudSegundoRegularizacion);

                            //LongitudDecimalRegularizacion.Text = Convert.ToString(verticeModificado.longitudDecimal);
                            UTMERegularizacion.Text = Convert.ToString(verticeModificado.utmE);
                            UTMNRegularizacion.Text = Convert.ToString(verticeModificado.utmN);

                            IdVerticeRegul.Value = Convert.ToString("0");
                        }
                    }
                    PanelBotonesVerticesRegul.Visible = true;
                    break;
            };

        }

        protected void GridVerticeRegularizacion_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridVerticeRegularizacion.PageIndex = e.NewPageIndex;
            GridVerticeRegularizacion.DataBind();
            CargaGrillaVertice("Regularizacion");
        }

        protected void GridVerticeRegularizacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(Convert.ToInt32(IdSolicitud.Value), 0, Convert.ToInt32(IdPoligonoRegul.Value));

                //if (comparacionPoligono != null && Convert.ToInt32(IdPoligonoRegul.Value) > 0)
                int accion3 = Convert.ToInt32(Accion3.Value);
                if (accion3 == rbAccion.VER)
                {
                    // Ver
                    ImageButton boton_ver = (ImageButton)e.Row.FindControl("gVer");
                    if (boton_ver != null)
                    {
                        boton_ver.Visible = true;
                    };

                }
                else
                {

                    // Borrar
                    ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                    if (boton_eliminar != null)
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar el Vértice?')");
                        boton_eliminar.Visible = true;
                    };

                    // Modificar
                    ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                    if (boton_eliminar != null)
                    {
                        boton_modificar.Visible = true;
                    };
                }
            };
        }


        private void ocultarGrillaVerticeAntEspaciales()
        {
            GridVerticeAntEspaciales.Visible = false;
            ViewState["Vertices_AntEspaciales"] = null;
            IdVerticeAntEspaciales.Value = Convert.ToString("0");

        }

        private void ocultarGrillaVerticeAntTerreno()
        {
            GridVerticeAntTerreno.Visible = false;
            ViewState["Vertices_AntTerreno"] = null;
            IdVerticeAntTerreno.Value = Convert.ToString("0");

        }

        private void ocultarGrillaVerticeRegularizacion()
        {
            GridVerticeRegularizacion.Visible = false;
            ViewState["Vertices_Regularizacion"] = null;
            IdVerticeRegul.Value = Convert.ToString("0");

        }


        private bool AgregarGrillaVertice(string nombrePestania)
        {
            int index = 0;
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<Vertice> List_VerticesAntEspaciales = (List<Vertice>)ViewState["Vertices_AntEspaciales"];

                    index = 0;
                    if (List_VerticesAntEspaciales == null)
                    {
                        List_VerticesAntEspaciales = new List<Vertice>();
                    }
                    else
                    {
                        int indexAux = 0;
                        foreach (Vertice verticeAux in List_VerticesAntEspaciales)
                        {
                            verticeAux.index = indexAux;
                            indexAux++;
                        }
                        index = List_VerticesAntEspaciales.Count;
                    }

                    Vertice vertice = new Vertice();

                    vertice.idVertice = Convert.ToInt32(IdVerticeAntEspaciales.Value);
                    vertice.index = Convert.ToInt32(index);
                    vertice.vertice = new ParametroGenerico(Convert.ToInt32(VerticeAntecedentesEspeciales.SelectedValue), Convert.ToString(VerticeAntecedentesEspeciales.SelectedItem.Text));
                    vertice.latitudHora = Convert.ToInt32(LatitudHoraAntecedentesEspeciales.Text);
                    vertice.latitudMinuto = Convert.ToInt32(LatitudMinutoAntecedentesEspeciales.Text);

                    vertice.latitudSegundo = Convert.ToDouble(LatitudSegundoAntecedentesEspeciales.Text);

                    vertice.longitudHora = Convert.ToInt32(LongitudHoraAntecedentesEspeciales.Text);
                    vertice.longitudMinuto = Convert.ToInt32(LongitudMinutoAntecedentesEspeciales.Text);
                    vertice.longitudSegundo = Convert.ToDouble(LongitudSegundoAntecedentesEspeciales.Text);

                    //vertice.latitudDecimal = Convert.ToDouble(LatitudAntecedentesEspeciales.Text);
                    //vertice.longitudDecimal = Convert.ToDouble(LongitudAntecedentesEspeciales.Text);

                    vertice.utmE = Convert.ToDouble(UTMEAntecedentesEspeciales.Text);
                    vertice.utmN = Convert.ToDouble(UtmNAntecedentesEspeciales.Text);

                    List<String> listaErroresVerticeAntEspacial = antecedDelSectorValidacion.validaVertice(vertice, List_VerticesAntEspaciales);

                    if (listaErroresVerticeAntEspacial != null && listaErroresVerticeAntEspacial.Count <= 0)
                    {

                        /**
                        * En una modificación, el vértice ya existe, por lo que se quita antes de agregarlo nuevamente. 
                        */
                        if (Convert.ToInt32(IdVerticeAntEspaciales.Value) > 0)
                        {
                            foreach (Vertice verticeRemover in List_VerticesAntEspaciales)
                            {
                                if (verticeRemover.idVertice.Equals(Convert.ToInt32(IdVerticeAntEspaciales.Value)))
                                {
                                    vertice.index = verticeRemover.index;
                                    List_VerticesAntEspaciales.Remove(verticeRemover);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (Vertice verticeRemover in List_VerticesAntEspaciales)
                            {
                                if (verticeRemover.vertice.id.Equals(Convert.ToInt32(vertice.vertice.id)))
                                {
                                    vertice.index = verticeRemover.index;
                                    List_VerticesAntEspaciales.Remove(verticeRemover);
                                    break;
                                }
                            }

                        }

                        List_VerticesAntEspaciales.Add(vertice);

                        GridVerticeAntEspaciales.DataSource = List_VerticesAntEspaciales;
                        GridVerticeAntEspaciales.DataBind();
                        GridVerticeAntEspaciales.Visible = true;

                        ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_VerticesAntEspaciales;
                        IdVerticeAntEspaciales.Value = Convert.ToString("0");


                    }
                    else
                    {
                        foreach (String error in listaErroresVerticeAntEspacial)
                        {
                            Page.Validators.Add(new ValidationError("grupo4", error));
                        }
                        //NombreCartaAntecedentesEspaciales.Focus();
                        UpdateAntecedentesEspeciales.Update();
                        return false;
                    }
                    break;

                case "AntecedentesTerreno":

                    List<Vertice> List_VerticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];

                    index = 0;
                    if (List_VerticesAntTerreno == null)
                    {
                        List_VerticesAntTerreno = new List<Vertice>();
                    }
                    else
                    {
                        int indexAux = 0;
                        foreach (Vertice verticeAux in List_VerticesAntTerreno)
                        {
                            verticeAux.index = indexAux;
                            indexAux++;
                        }
                        index = List_VerticesAntTerreno.Count;
                    }

                    Vertice verticeAntTerreno = new Vertice();

                    verticeAntTerreno.idVertice = Convert.ToInt32(IdVerticeAntTerreno.Value);
                    verticeAntTerreno.index = Convert.ToInt32(index);
                    verticeAntTerreno.vertice = new ParametroGenerico(Convert.ToInt32(VerticeAntecedentesTerreno.SelectedValue), Convert.ToString(VerticeAntecedentesTerreno.SelectedItem.Text));
                    verticeAntTerreno.latitudHora = Convert.ToInt32(LatitudHoraAntecedentesTerreno.Text);
                    verticeAntTerreno.latitudMinuto = Convert.ToInt32(LatitudMinutoAntecedentesTerreno.Text);

                    verticeAntTerreno.latitudSegundo = Convert.ToDouble(LatitudSegundoAntecedentesTerreno.Text);

                    verticeAntTerreno.longitudHora = Convert.ToInt32(LongitudHoraAntecedentesTerreno.Text);
                    verticeAntTerreno.longitudMinuto = Convert.ToInt32(LongitudMinutoAntecedentesTerreno.Text);

                    verticeAntTerreno.longitudSegundo = Convert.ToDouble(LongitudSegundoAntecedentesTerreno.Text);

                    //verticeAntTerreno.latitudDecimal = Convert.ToDouble(LatitudDecimalAntecedentesTerreno.Text);
                    //verticeAntTerreno.longitudDecimal = Convert.ToDouble(LongitudDecimalAntecedentesTerreno.Text);

                    verticeAntTerreno.utmE = Convert.ToDouble(UTMEAntecedentesTerreno.Text);
                    verticeAntTerreno.utmN = Convert.ToDouble(UTMNAntecedentesTerreno.Text);

                    List<String> listaErroresVerticeAntTerreno = antecedDelSectorValidacion.validaVertice(verticeAntTerreno, List_VerticesAntTerreno);

                    if (listaErroresVerticeAntTerreno != null && listaErroresVerticeAntTerreno.Count <= 0)
                    {

                        if (Convert.ToInt32(IdVerticeAntTerreno.Value) > 0)
                        {
                            foreach (Vertice verticeRemover in List_VerticesAntTerreno)
                            {
                                if (verticeRemover.idVertice.Equals(Convert.ToInt32(IdVerticeAntTerreno.Value)))
                                {
                                    verticeAntTerreno.index = verticeRemover.index;
                                    List_VerticesAntTerreno.Remove(verticeRemover);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (Vertice verticeRemover in List_VerticesAntTerreno)
                            {
                                if (verticeRemover.vertice.id.Equals(Convert.ToInt32(verticeAntTerreno.vertice.id)))
                                {
                                    verticeAntTerreno.index = verticeRemover.index;
                                    List_VerticesAntTerreno.Remove(verticeRemover);
                                    break;
                                }
                            }

                        }

                        List_VerticesAntTerreno.Add(verticeAntTerreno);

                        GridVerticeAntTerreno.DataSource = List_VerticesAntTerreno;
                        GridVerticeAntTerreno.DataBind();
                        GridVerticeAntTerreno.Visible = true;

                        ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;
                        IdVerticeAntTerreno.Value = Convert.ToString("0");
                    }
                    else
                    {
                        foreach (String error in listaErroresVerticeAntTerreno)
                        {
                            Page.Validators.Add(new ValidationError("grupo9", error));
                        }
                        DATUMAntTerreno.Focus();
                        UpdateAntecedentesTerreno.Update();
                        return false;
                    }
                    break;

                case "Regularizacion":

                    List<Vertice> List_VerticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];

                    index = 0;
                    if (List_VerticesRegularizacion == null)
                    {
                        List_VerticesRegularizacion = new List<Vertice>();
                    }
                    else
                    {
                        int indexAux = 0;
                        foreach (Vertice verticeAux in List_VerticesRegularizacion)
                        {
                            verticeAux.index = indexAux;
                            indexAux++;
                        }
                        index = List_VerticesRegularizacion.Count;
                    }

                    Vertice verticeRegularizacion = new Vertice();

                    verticeRegularizacion.idVertice = Convert.ToInt32(IdVerticeRegul.Value);
                    verticeRegularizacion.index = Convert.ToInt32(index);
                    verticeRegularizacion.vertice = new ParametroGenerico(Convert.ToInt32(VerticeRegularizacion.SelectedValue), Convert.ToString(VerticeRegularizacion.SelectedItem.Text));
                    verticeRegularizacion.latitudHora = Convert.ToInt32(LatitudHoraRegularizacion.Text);
                    verticeRegularizacion.latitudMinuto = Convert.ToInt32(LatitudMinutoRegularizacion.Text);

                    verticeRegularizacion.latitudSegundo = Convert.ToDouble(LatitudSegundoRegularizacion.Text);

                    verticeRegularizacion.longitudHora = Convert.ToInt32(LongitudHoraRegularizacion.Text);
                    verticeRegularizacion.longitudMinuto = Convert.ToInt32(LongitudMinutoRegularizacion.Text);

                    verticeRegularizacion.longitudSegundo = Convert.ToDouble(LongitudSegundoRegularizacion.Text);

                    //verticeRegularizacion.latitudDecimal = Convert.ToDouble(LatitudDecimalRegularizacion.Text);
                    //verticeRegularizacion.longitudDecimal = Convert.ToDouble(LongitudDecimalRegularizacion.Text);

                    verticeRegularizacion.utmE = Convert.ToDouble(UTMERegularizacion.Text);
                    verticeRegularizacion.utmN = Convert.ToDouble(UTMNRegularizacion.Text);

                    List<String> listaErroresVerticeRegul = antecedDelSectorValidacion.validaVertice(verticeRegularizacion, List_VerticesRegularizacion);

                    if (listaErroresVerticeRegul != null && listaErroresVerticeRegul.Count <= 0)
                    {
                        if (Convert.ToInt32(IdVerticeRegul.Value) > 0)
                        {
                            foreach (Vertice verticeRemover in List_VerticesRegularizacion)
                            {
                                if (verticeRemover.idVertice.Equals(Convert.ToInt32(IdVerticeRegul.Value)))
                                {
                                    verticeRegularizacion.index = verticeRemover.index;
                                    List_VerticesRegularizacion.Remove(verticeRemover);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (Vertice verticeRemover in List_VerticesRegularizacion)
                            {
                                if (verticeRemover.vertice.id.Equals(Convert.ToInt32(verticeRegularizacion.vertice.id)))
                                {
                                    verticeRegularizacion.index = verticeRemover.index;
                                    List_VerticesRegularizacion.Remove(verticeRemover);
                                    break;
                                }
                            }

                        }

                        List_VerticesRegularizacion.Add(verticeRegularizacion);

                        GridVerticeRegularizacion.DataSource = List_VerticesRegularizacion;
                        GridVerticeRegularizacion.DataBind();
                        GridVerticeRegularizacion.Visible = true;

                        ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegularizacion;
                        IdVerticeRegul.Value = Convert.ToString("0");
                    }
                    else
                    {
                        foreach (String error in listaErroresVerticeRegul)
                        {
                            Page.Validators.Add(new ValidationError("grupo11", error));
                        }
                        NombreCartaRegul.Focus();
                        UpdatePanelPanelRegularizacion.Update();
                        return false;
                    }
                    break;
            }


            return true;
        }

        private void CargaGrillaVertice(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<Vertice> List_Vertices = (List<Vertice>)ViewState["Vertices_AntEspaciales"];

                    if (List_Vertices == null)
                    {
                        List_Vertices = new List<Vertice>();
                    }

                    GridVerticeAntEspaciales.DataSource = List_Vertices;
                    GridVerticeAntEspaciales.DataBind();
                    GridVerticeAntEspaciales.Visible = true;

                    ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_Vertices;

                    limpiarVertice("AntecedentesEspaciales");

                    break;

                case "AntecedentesTerreno":

                    List<Vertice> List_VerticesAntTerreno = (List<Vertice>)ViewState["Vertices_AntTerreno"];

                    if (List_VerticesAntTerreno == null)
                    {
                        List_VerticesAntTerreno = new List<Vertice>();
                    }

                    GridVerticeAntTerreno.DataSource = List_VerticesAntTerreno;
                    GridVerticeAntTerreno.DataBind();
                    GridVerticeAntTerreno.Visible = true;

                    ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;

                    limpiarVertice("AntecedentesTerreno");

                    break;

                case "Regularizacion":

                    List<Vertice> List_VerticesRegularizacion = (List<Vertice>)ViewState["Vertices_Regularizacion"];

                    if (List_VerticesRegularizacion == null)
                    {
                        List_VerticesRegularizacion = new List<Vertice>();
                    }

                    GridVerticeRegularizacion.DataSource = List_VerticesRegularizacion;
                    GridVerticeRegularizacion.DataBind();
                    GridVerticeRegularizacion.Visible = true;

                    ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegularizacion;

                    limpiarVertice("Regularizacion");

                    break;

            }
        }

        protected void EliminarGrillaVertice(List<Vertice> lista_vertices, String nombrePestania)
        {
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    VerticeAntecedentesEspeciales.SelectedIndex = -1;
                    LatitudHoraAntecedentesEspeciales.Text = "";
                    LatitudMinutoAntecedentesEspeciales.Text = "";
                    LatitudSegundoAntecedentesEspeciales.Text = "";
                    //LatitudAntecedentesEspeciales.Text = "";
                    LongitudHoraAntecedentesEspeciales.Text = "";
                    LongitudMinutoAntecedentesEspeciales.Text = "";
                    LongitudSegundoAntecedentesEspeciales.Text = "";
                    //LongitudAntecedentesEspeciales.Text = "";
                    UTMEAntecedentesEspeciales.Text = "";
                    UtmNAntecedentesEspeciales.Text = "";

                    ViewState["Vertices_AntEspaciales"] = null;

                    break;

                case "AntecedentesTerreno":

                    VerticeAntecedentesTerreno.SelectedIndex = -1;
                    LatitudHoraAntecedentesTerreno.Text = "";
                    LatitudMinutoAntecedentesTerreno.Text = "";
                    LatitudSegundoAntecedentesTerreno.Text = "";
                    //LatitudDecimalAntecedentesTerreno.Text = "";
                    LongitudHoraAntecedentesTerreno.Text = "";
                    LongitudMinutoAntecedentesTerreno.Text = "";
                    LongitudSegundoAntecedentesTerreno.Text = "";
                    //LongitudDecimalAntecedentesTerreno.Text = "";
                    UTMEAntecedentesTerreno.Text = "";
                    UTMNAntecedentesTerreno.Text = "";

                    ViewState["Vertices_AntTerreno"] = null;

                    break;

                case "Regularizacion":

                    VerticeRegularizacion.SelectedIndex = -1;
                    LatitudHoraRegularizacion.Text = "";
                    LatitudMinutoRegularizacion.Text = "";
                    LatitudSegundoRegularizacion.Text = "";
                    //LatitudDecimalRegularizacion.Text = "";
                    LongitudHoraRegularizacion.Text = "";
                    LongitudMinutoRegularizacion.Text = "";
                    LongitudSegundoRegularizacion.Text = "";
                    //LongitudDecimalRegularizacion.Text = "";
                    UTMERegularizacion.Text = "";
                    UTMNRegularizacion.Text = "";

                    ViewState["Vertices_Regularizacion"] = null;

                    break;
            }
        }

        protected void EliminarGrillaVertice(int idPoligono, int idVertice, string nombrePestania)
        {
            //AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    List<Vertice> List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    foreach (Vertice vertice in List_Vertices)
                    {
                        if (vertice.idVertice.Equals(idVertice))
                        {
                            bool resp = antecedentesSectorService.eliminarVertice(idVertice, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                            if (resp)
                            {
                                msgGrillaGral_2.Text = "Se ha eliminado el vértice (Coord. Inspección de Terreno) exitosamente.";
                                Content_msgGrillaGral_2.Visible = true;

                            }
                            else
                            {
                                msgGrillaGral_2.Text = "No se ha eliminado el vértice (Coord. Inspección de Terreno).";
                                Content_msgGrillaGral_2.Visible = true;
                            }
                            IcoGral_2.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        }
                    }

                    List_Vertices = verticeDA.ListarVertice(idPoligono, 0);

                    ViewState["Vertices_AntEspaciales"] = (List<Vertice>)List_Vertices;
                    break;

                case "AntecedentesTerreno":

                    List<Vertice> List_VerticesAntTerreno = verticeDA.ListarVertice(idPoligono, 0);

                    foreach (Vertice vertice in List_VerticesAntTerreno)
                    {
                        if (vertice.idVertice.Equals(idVertice))
                        {
                            bool resp = antecedentesSectorService.eliminarVertice(idVertice, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                            if (resp)
                            {
                                msgGrillaGral_3.Text = "Se ha eliminado el vértice (Ant. Terreno) exitosamente.";
                                Content_msgGrillaGral_3.Visible = true;

                            }
                            else
                            {
                                msgGrillaGral_3.Text = "No se ha eliminado el vértice (Ant. Terreno).";
                                Content_msgGrillaGral_3.Visible = true;
                            }
                            IcoGrillaGral_3.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        }
                    }

                    List_VerticesAntTerreno = verticeDA.ListarVertice(idPoligono, 0);

                    ViewState["Vertices_AntTerreno"] = (List<Vertice>)List_VerticesAntTerreno;
                    break;

                case "Regularizacion":

                    List<Vertice> List_VerticesRegul = verticeDA.ListarVertice(idPoligono, 0);

                    foreach (Vertice vertice in List_VerticesRegul)
                    {
                        if (vertice.idVertice.Equals(idVertice))
                        {
                            bool resp = antecedentesSectorService.eliminarVertice(idVertice, idPoligono, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);

                            if (resp)
                            {
                                msgGrillaGral_4.Text = "Se ha eliminado el vértice (Regularización) exitosamente.";
                                Content_msgGrillaGral_4.Visible = true;

                            }
                            else
                            {
                                msgGrillaGral_4.Text = "No se ha eliminado el vértice (Regularización).";
                                Content_msgGrillaGral_4.Visible = true;
                            }
                            IcoGrillaGral_4.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
                        }
                    }

                    List_VerticesRegul = verticeDA.ListarVertice(idPoligono, 0);

                    ViewState["Vertices_Regularizacion"] = (List<Vertice>)List_VerticesRegul;
                    break;
            }
        }


        #endregion



        #region Observaciones

        /**
         * Método que guarda las observaciones de toda la funcionalidad antecedentes del sector.
         */
        protected void GuardarObservaciones_Click(object sender, ImageClickEventArgs e)
        {

            ObsPestaniaInforme obsPestaniaInforme = new ObsPestaniaInforme();
            obsPestaniaInforme.idObsPestInf = 0;
            obsPestaniaInforme.idSolConcesion = Convert.ToInt32(IdSolicitud.Value);
            obsPestaniaInforme.tipoGrupo = new ParametroGenerico();
            obsPestaniaInforme.tipoGrupo.id = rbTipo.ANTEC_SECTOR;
            obsPestaniaInforme.observaciones = Convert.ToString(observaciones.Text);

            bool resp = antecedentesSectorService.guardarObservacionesAntSector(obsPestaniaInforme);
            if (resp)
            {
                MensajeObservaciones.Text = "Se han guardado las observaciones de Ant. del Sector exitosamente.";
                PanelObservaciones.Visible = true;
            }
            else
            {
                MensajeObservaciones.Text = "No se han guardado las observaciones de Ant. del Sector.";
                PanelObservaciones.Visible = true;
            }
            IcoObservaciones.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            observaciones.Focus();
        }



        #endregion



        #region Validaciones

        private void validarLatitudAntecedentesEspaciales()
        {
            RangeValidatorLatitudHoraAntecedentesEspeciales.Validate();
            RangeValidatorLatitudMinutoAntecedentesEspeciales.Validate();
            RegularExpressionValidatorLatitudSegundoAntecedentesEspeciales.Validate();
        }

        private void validarLongitudAntecedentesEspaciales()
        {
            RangeValidatorLongitudHoraAntecedentesEspeciales.Validate();
            RangeValidatorLongitudMinutoAntecedentesEspeciales.Validate();
            RegularExpressionValidatorLongitudSegundoAntecedentesEspeciales.Validate();
        }

        private void validarLatitudAntecedentesTerreno()
        {
            RangeValidatorLatitudHoraAntecedentesTerreno.Validate();
            RangeValidatorLatitudMinutoAntecedentesTerreno.Validate();
            RegularExpressionValidatorLatitudSegundoAntecedentesTerreno.Validate();
        }

        private void validarLongitudAntecedentesTerreno()
        {
            RangeValidatorLongitudHoraAntecedentesTerreno.Validate();
            RangeValidatorLongitudMinutoAntecedentesTerreno.Validate();
            RegularExpressionValidatorLongitudSegundoAntecedentesTerreno.Validate();
        }

        private void validarLatitudRegularizacion()
        {
            RangeValidatorLatitudHoraRegularizacion.Validate();
            RangeValidatorLatitudMinutoRegularizacion.Validate();
            RegularExpressionValidatorLatitudSegundoRegularizacion.Validate();
        }

        private void validarLongitudRegularizacion()
        {
            RangeValidatorLongitudHoraRegularizacion.Validate();
            RangeValidatorLongitudMinutoRegularizacion.Validate();
            RegularExpressionValidatorLongitudSegundoRegularizacion.Validate();
        }

        #endregion



        #region Limpiar Formularios

        protected void LimpiarVertice_Click(object sender, ImageClickEventArgs e)
        {
            limpiarVertice("AntecedentesEspaciales");
            IdVerticeAntEspaciales.Value = Convert.ToString("0");

            VerticeAntecedentesEspeciales.Enabled = true;
            LatitudHoraAntecedentesEspeciales.Enabled = true;
            LatitudMinutoAntecedentesEspeciales.Enabled = true;
            LatitudSegundoAntecedentesEspeciales.Enabled = true;
            LongitudHoraAntecedentesEspeciales.Enabled = true;
            LongitudMinutoAntecedentesEspeciales.Enabled = true;
            LongitudSegundoAntecedentesEspeciales.Enabled = true;
            UTMEAntecedentesEspeciales.Enabled = true;
            UtmNAntecedentesEspeciales.Enabled = true;

            PanelBotonesVerticeAntEspaciales.Visible = true;
        }

        protected void LimpiarPoligono_Click(object sender, ImageClickEventArgs e)
        {
            limpiarPoligono("AntecedentesEspaciales");
            IdPoligonoAntEspaciales.Value = Convert.ToString("0");

            TipoConcesionAntecedentesEspaciales.Enabled = true;
            TipoUsoAntecedentesEspeciales.Enabled = true;
            ToponimioAntecedentesEspeciales.Enabled = true;
            AreaSolicitadaAntecedentesEspeciales.Enabled = true;
            AreaCalculadaAntecedentesEspeciales.Enabled = true;

            ViewState["Vertices_AntEspaciales"] = null;

            GridVerticeAntEspaciales.DataSource = null;
            GridVerticeAntEspaciales.DataBind();
            GridVerticeAntEspaciales.Visible = false;

            LimpiarVertice_Click(null, null);

            PanelBotonesPoligonoAntEspaciales.Visible = true;
        }

        protected void LimpiarVerticeAntTerreno_Click(object sender, ImageClickEventArgs e)
        {
            limpiarVertice("AntecedentesTerreno");
            IdVerticeAntTerreno.Value = Convert.ToString("0");

            VerticeAntecedentesTerreno.Enabled = true;
            LatitudHoraAntecedentesTerreno.Enabled = true;
            LatitudMinutoAntecedentesTerreno.Enabled = true;
            LatitudSegundoAntecedentesTerreno.Enabled = true;
            LongitudHoraAntecedentesTerreno.Enabled = true;
            LongitudMinutoAntecedentesTerreno.Enabled = true;
            LongitudSegundoAntecedentesTerreno.Enabled = true;
            UTMEAntecedentesTerreno.Enabled = true;
            UTMNAntecedentesTerreno.Enabled = true;

            PanelBotonesVerticeAntTerreno.Visible = true;

        }

        protected void LimpiarPoligonoAntTerreno_Click(object sender, ImageClickEventArgs e)
        {
            limpiarPoligono("AntecedentesTerreno");
            IdPoligonoAntTerreno.Value = Convert.ToString("0");

            TipoConcesionAntecedentesTerreno.Enabled = true;
            TipoUsoAntecedentesTerreno.Enabled = true;
            ToponimioAntecedentesTerreno.Enabled = true;
            AreaCalculadaAntecedentesTerreno.Enabled = true;
            AreaSolicitadaAntecedentesTerreno.Enabled = true;

            ViewState["Vertices_AntTerreno"] = null;

            GridVerticeAntTerreno.DataSource = null;
            GridVerticeAntTerreno.DataBind();
            GridVerticeAntTerreno.Visible = false;

            LimpiarVerticeAntTerreno_Click(null, null);

            PanelBotonesAntecedentesTerreno.Visible = true;
        }

        protected void LimpiarVerticeRegul_Click(object sender, ImageClickEventArgs e)
        {
            limpiarVertice("Regularizacion");
            IdVerticeRegul.Value = Convert.ToString("0");

            VerticeRegularizacion.Enabled = true;
            LatitudHoraRegularizacion.Enabled = true;
            LatitudMinutoRegularizacion.Enabled = true;
            LatitudSegundoRegularizacion.Enabled = true;
            LongitudHoraRegularizacion.Enabled = true;
            LongitudMinutoRegularizacion.Enabled = true;
            LongitudSegundoRegularizacion.Enabled = true;
            UTMNRegularizacion.Enabled = true;
            UTMERegularizacion.Enabled = true;

            PanelBotonesVerticesRegul.Visible = true;
        }

        protected void LimpiarPoligonoRegul_Click(object sender, ImageClickEventArgs e)
        {
            limpiarPoligono("Regularizacion");
            IdPoligonoRegul.Value = Convert.ToString("0");

            TipoConsecionRegularizacion.Enabled = true;
            TipoUsoRegularizacion.Enabled = true;
            ToponimioRegularizacion.Enabled = true;
            AreaRegularizacion.Enabled = true;

            ViewState["Regularizacion"] = null;

            GridVerticeRegularizacion.DataSource = null;
            GridVerticeRegularizacion.DataBind();
            GridVerticeRegularizacion.Visible = false;

            LimpiarVerticeRegul_Click(null, null);

            PanelBotonesPoligonosRegul.Visible = true;
        }

        private void limpiarPoligono(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":
                    TipoConcesionAntecedentesEspaciales.SelectedIndex = -1;
                    TipoUsoAntecedentesEspeciales.SelectedIndex = -1;
                    ToponimioAntecedentesEspeciales.Text = "";
                    AreaCalculadaAntecedentesEspeciales.Text = "";
                    AreaSolicitadaAntecedentesEspeciales.Text = "";

                    break;
                case "AntecedentesTerreno":
                    TipoConcesionAntecedentesTerreno.SelectedIndex = -1;
                    TipoUsoAntecedentesTerreno.SelectedIndex = -1;
                    ToponimioAntecedentesTerreno.Text = "";
                    AreaCalculadaAntecedentesTerreno.Text = "";
                    AreaSolicitadaAntecedentesTerreno.Text = "";

                    break;

                case "Regularizacion":
                    TipoConsecionRegularizacion.SelectedIndex = -1;
                    TipoUsoRegularizacion.SelectedIndex = -1;
                    ToponimioRegularizacion.Text = "";
                    AreaRegularizacion.Text = "";

                    break;
            }
        }

        private void limpiarVertice(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":

                    VerticeAntecedentesEspeciales.SelectedIndex = -1;
                    LatitudHoraAntecedentesEspeciales.Text = "";
                    LatitudMinutoAntecedentesEspeciales.Text = "";
                    LatitudSegundoAntecedentesEspeciales.Text = "";
                    //LatitudAntecedentesEspeciales.Text = "";
                    //LatitudAntecedentesEspecialesReadOnly.Text = "";
                    LongitudHoraAntecedentesEspeciales.Text = "";
                    LongitudMinutoAntecedentesEspeciales.Text = "";
                    LongitudSegundoAntecedentesEspeciales.Text = "";
                    //LongitudAntecedentesEspeciales.Text = "";
                    //LongitudAntecedentesEspecialesReadOnly.Text = "";
                    UTMEAntecedentesEspeciales.Text = "";
                    UtmNAntecedentesEspeciales.Text = "";
                    break;
                case "AntecedentesTerreno":

                    VerticeAntecedentesTerreno.SelectedIndex = -1;
                    LatitudHoraAntecedentesTerreno.Text = "";
                    LatitudMinutoAntecedentesTerreno.Text = "";
                    LatitudSegundoAntecedentesTerreno.Text = "";
                    //LatitudDecimalAntecedentesTerreno.Text = "";
                    //LatitudDecimalAntecedentesTerrenoReadOnly.Text = "";
                    LongitudHoraAntecedentesTerreno.Text = "";
                    LongitudMinutoAntecedentesTerreno.Text = "";
                    LongitudSegundoAntecedentesTerreno.Text = "";
                    //LongitudDecimalAntecedentesTerreno.Text = "";
                    //LongitudDecimalAntecedentesTerrenoReadOnly.Text = "";
                    UTMEAntecedentesTerreno.Text = "";
                    UTMNAntecedentesTerreno.Text = "";
                    break;

                case "Regularizacion":

                    VerticeRegularizacion.SelectedIndex = -1;
                    LatitudHoraRegularizacion.Text = "";
                    LatitudMinutoRegularizacion.Text = "";
                    LatitudSegundoRegularizacion.Text = "";
                    //LatitudDecimalRegularizacion.Text = "";
                    //LatitudDecimalRegularizacionReadOnly.Text = "";
                    LongitudHoraRegularizacion.Text = "";
                    LongitudMinutoRegularizacion.Text = "";
                    LongitudSegundoRegularizacion.Text = "";
                    //LongitudDecimalRegularizacion.Text = "";
                    //LongitudDecimalRegularizacionReadOnly.Text = "";
                    UTMERegularizacion.Text = "";
                    UTMNRegularizacion.Text = "";
                    break;
            }
        }

        private void limpiaPestanias(string nombrePestania)
        {
            switch (nombrePestania)
            {

                case "AntecedentesEspaciales":
                    break;

                case "AntecedentesTerreno":

                    /* Referencias Geograficas */
                    Content_msgGrillaGral_3.Visible = false;
                    msgGrillaGral_3.Text = "";
                    IcoGrillaGral_3.Visible = false;

                    DATUMAntTerreno.SelectedValue = "-1";
                    HusoHorarioAntecedentesTerreno.SelectedValue = "-1";
                    AreaTotalCalculadaAntecedentesTerreno.Text = "0";
                    AreaTotalSolicitadaAntecedentesTerreno.Text = "0";

                    SeUsaParaBancoAntTerreno.Checked = false;

                    /* Archivo Adjunto */
                    //PanelAntTerrenoArchivoAdjunto.Visible = false;
                    //mensajeAntTerrenoArchivoAdjunto.Text = "";

                    TipoArchivoAntTerreno.SelectedValue = "-1";
                    NombreArchivoAntTerreno.Text = "";
                    GridArchivoAdjuntoAntTerreno.DataSource = null;
                    GridArchivoAdjuntoAntTerreno.DataBind();
                    GridArchivoAdjuntoAntTerreno.Visible = false;

                    /* Poligono */
                    //msgGrillaPoligonoAntTerreno.Text = "";
                    //Content_msgGrillaPoligonoAntTerreno.Visible = false;

                    TipoConcesionAntecedentesTerreno.SelectedValue = "-1";
                    TipoUsoAntecedentesTerreno.SelectedValue = "-1";
                    ToponimioAntecedentesTerreno.Text = "";
                    AreaCalculadaAntecedentesTerreno.Text = "";
                    AreaSolicitadaAntecedentesTerreno.Text = "";

                    /* Vértice */
                    VerticeAntecedentesTerreno.SelectedValue = "-1";
                    LatitudHoraAntecedentesTerreno.Text = "";
                    LatitudMinutoAntecedentesTerreno.Text = "";
                    LatitudSegundoAntecedentesTerreno.Text = "";
                    //LatitudDecimalAntecedentesTerreno.Text = "";
                    LongitudHoraAntecedentesTerreno.Text = "";
                    LongitudMinutoAntecedentesTerreno.Text = "";
                    LongitudSegundoAntecedentesTerreno.Text = "";
                    //LongitudDecimalAntecedentesTerreno.Text = "";
                    UTMEAntecedentesTerreno.Text = "";
                    UTMNAntecedentesTerreno.Text = "";

                    GridVerticeAntTerreno.DataSource = null;
                    GridVerticeAntTerreno.DataBind();
                    GridVerticeAntTerreno.Visible = false;

                    GridPoligonoAntecedentesTerreno.DataSource = null;
                    GridPoligonoAntecedentesTerreno.DataBind();
                    GridPoligonoAntecedentesTerreno.Visible = false;


                    break;
                case "Regularizacion":

                    /* Referencias Geográficas */
                    msgGrillaGral_4.Text = "";
                    Content_msgGrillaGral_4.Visible = false;
                    IcoGrillaGral_4.Visible = false;

                    //DATUMRegul.SelectedValue = "-1";
                    //HusoHorarioRegularizacion.SelectedValue = "-1";

                    NombreCartaRegul.Text = "";
                    NumeroCartaRegul.Text = "";
                    EdicionCartaRegul.Text = "";
                    AnioCartaRegul.Text = "";
                    DatumRegul.Text = "";
                    HusoHorarioRegul.Text = "";

                    SeUsaParaBancoRegularizacion.Checked = false;

                    AreaTotalRegularizacion.Text = "";

                    /* Archivo Adjunto */
                    //PanelRegulArchivoAdjunto.Visible = false;
                    //mensajeRegulArchivoAdjunto.Text = "";

                    TipoArchivoRegularizacion.SelectedValue = "-1";
                    NombreArchivoRegularizacion.Text = "";

                    GridArchivoAdjuntoRegularizacion.DataSource = null;
                    GridArchivoAdjuntoRegularizacion.DataBind();
                    GridArchivoAdjuntoRegularizacion.Visible = false;

                    /* Poligono */
                    //Content_msgGrillaPoligonoRegul.Visible = false;
                    //msgGrillaPoligonoRegul.Text = "";

                    TipoConsecionRegularizacion.SelectedValue = "-1";
                    TipoUsoRegularizacion.SelectedValue = "-1";
                    ToponimioRegularizacion.Text = "";
                    AreaRegularizacion.Text = "";

                    /* Vértice */
                    VerticeRegularizacion.SelectedValue = "-1";
                    LatitudHoraRegularizacion.Text = "";
                    LatitudMinutoRegularizacion.Text = "";
                    LatitudSegundoRegularizacion.Text = "";
                    //LatitudDecimalRegularizacion.Text = "";
                    LongitudHoraRegularizacion.Text = "";
                    LongitudMinutoRegularizacion.Text = "";
                    LongitudSegundoRegularizacion.Text = "";
                    //LongitudDecimalRegularizacion.Text = "";
                    UTMERegularizacion.Text = "";
                    UTMNRegularizacion.Text = "";

                    GridVerticeRegularizacion.DataSource = null;
                    GridVerticeRegularizacion.DataBind();
                    GridVerticeRegularizacion.Visible = false;

                    GridPoligonoRegul.DataSource = null;
                    GridPoligonoRegul.DataBind();
                    GridPoligonoRegul.Visible = false;

                    break;
            }
        }



        #endregion



        #region Utilidades


        private void completarConCerosCampo(System.Web.UI.WebControls.TextBox campo)
        {

            string segundo = campo.Text;
            string[] parteDecimal = null;

            if (segundo != null && !segundo.Equals(""))
            {
                if (!segundo.Contains(","))
                {
                    segundo = segundo + ",0000";

                }
                else
                {

                    parteDecimal = segundo.Split(',');
                    int numeroDigitos = parteDecimal[1].Length;

                    if (numeroDigitos < 4)
                    {
                        string aux = "";
                        for (int i = numeroDigitos; i < 4; i++)
                        {
                            aux = aux + "0";
                        }

                        segundo = segundo + aux;
                    }
                }
                campo.Text = segundo;
            }
        }

        protected void cambiaPestania_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            switch (boton.ID)
            {
                case "lnk_AntecedentesEspaciales":

                    lnk_AntecedentesEspaciales.CssClass = "tab1_selected";
                    lnk_AntecedentesTerreno.CssClass = "tab2";
                    lnk_Regularizacion.CssClass = "tab3";

                    PanelAntecedentesEspeciales.Visible = true;
                    PanelAntecedentesTerreno.Visible = false;
                    PanelRegularizacion.Visible = false;

                    break;

                case "lnk_AntecedentesTerreno":

                    lnk_AntecedentesEspaciales.CssClass = "tab1";
                    lnk_AntecedentesTerreno.CssClass = "tab2_selected";
                    lnk_Regularizacion.CssClass = "tab3";

                    PanelAntecedentesEspeciales.Visible = false;
                    PanelAntecedentesTerreno.Visible = true;
                    PanelRegularizacion.Visible = false;

                    break;

                case "lnk_Regularizacion":

                    lnk_AntecedentesEspaciales.CssClass = "tab1";
                    lnk_AntecedentesTerreno.CssClass = "tab2";
                    lnk_Regularizacion.CssClass = "tab3_selected";

                    PanelAntecedentesEspeciales.Visible = false;
                    PanelAntecedentesTerreno.Visible = false;
                    PanelRegularizacion.Visible = true;
                    break;
            };
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

        /**
        * Método que actualiza los saldos de las areas de las coordenadas geograficas por cada poligono
        * ingresado por el usuario.
        */
        private void ActualizaAreasTotal(string nombrePestania, float areaCalculada, float areaSolicitada, float areaRegularizacion)
        {
            switch (nombrePestania)
            {
                case "AntecedentesEspaciales":

                    AreaTotalCalculadaAntecedentesEspeciales.Text = Convert.ToString(areaCalculada);
                    AreaTotalSolicitadaAntecedentesEspeciales.Text = Convert.ToString(areaSolicitada);

                    UpdatePanel3.Update();

                    break;

                case "AntecedentesTerreno":

                    AreaTotalSolicitadaAntecedentesTerreno.Text = Convert.ToString(areaSolicitada);
                    AreaTotalCalculadaAntecedentesTerreno.Text = Convert.ToString(areaCalculada);

                    UpdatePanel6.Update();

                    break;

                case "Regularizacion":

                    AreaTotalRegularizacion.Text = Convert.ToString(areaRegularizacion);

                    UpdatePanel7.Update();

                    break;
            }

        }


        #endregion


    }
}
