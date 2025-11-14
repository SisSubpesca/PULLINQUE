using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.common;
using Validaciones.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using System.Text.RegularExpressions;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.modificacion;
using System.Drawing;


namespace SubPesca.Solicitudes.Registrar
{
    public partial class proyTecnicoComponente : System.Web.UI.UserControl
    {
        Datos.Entidades.Usuario.Serializable usuario_logeado = new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema
        TipoDA tipoDa = new TipoDA();
        ParametroGenericoDA parametroDa = new ParametroGenericoDA();
        DespliegueMenuSeccionDA despliegueMenuSeccionDA = new DespliegueMenuSeccionDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        ProyectoTecnicoDA proyectoTecnicoDA = new ProyectoTecnicoDA();
        MantenedorDA mantenedorDA = new MantenedorDA();

        ProyectoTecnicoValidacion pTValidacion = new ProyectoTecnicoValidacion();
        Funciones funciones = new Funciones();
        
        RelocalizacionService relocalizacionService = new RelocalizacionService();
        DespliegueSeccionService despliegueSeccionService = new DespliegueSeccionService();
        PermisosService permisosService = new PermisosService();
        SolicitudModificacionService solicitudModificacionService = new SolicitudModificacionService();
        ProyectoTecnicoService proyectoTecnicoService = new ProyectoTecnicoService();




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

                ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_CONCESION };
                ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_CONCESION };
                ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_CONCESION };
                ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_CONCESION };
                ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_CONCESION };
                ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_CONCESION };
                ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_CONCESION };

                ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_CONCESION };
                
            }
            else if(funciones.retornaModulo().Equals("Concesion")){

                ViewState["URL_VER"] = paginas.URL_VER_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLCONCESION;
                ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLCONCESION;
                ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLCONCESION;
                ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLCONCESION;
                ViewState["solicitudSession"] = paginas.solicitudConcesionSession;

                ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_CONCESION_CREADO };
                ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_CONCESION_CREADA };
                ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_CONCESION_CREADO };

                ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_CONCESION_CREADA };
                
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
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_CREA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_CREA };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_CREA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                {
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_FUSIONA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_FUSIONA };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_FUSIONA };
                    
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO)
                {
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_SECTOR_CERO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_SECTOR_CERO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_SECTOR_CERO };
                    
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
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_CREA_RESA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_CREA_RESA };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_CREA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA_RESA)
                {
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_FUSIONA_RESA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_FUSIONA_RESA };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_RELOCALIZACION_FUSIONA_RESA };
                }
                if (solicitudConcesion != null && solicitudConcesion.sectorRelocalizacion.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA)
                {
                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_RELOCALIZACION_SECTOR_CERO_RESA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_RELOCALIZACION_SECTOR_CERO_RESA };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_RELOCALIZACION_RELOCALIZACION_SECTOR_CERO_RESA };
                    
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_ACOPIO_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_ACOPIO_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_ACOPIO_CREADO };
                    
                }
                else {

                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_ACOPIO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_ACOPIO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_ACOPIO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_ACOPIO;
                    ViewState["solicitudSession"] = paginas.solicitudAcopioSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_ACOPIO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_ACOPIO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_ACOPIO };
                    
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

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_FAENAMIENTO_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_FAENAMIENTO_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_FAENAMIENTO_CREADO };
                
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_CENTRO_DE_FAENAMIENTO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_CENTRO_DE_FAENAMIENTO;
                    ViewState["solicitudSession"] = paginas.solicitudFaenamientoSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_FAENAMIENTO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_FAENAMIENTO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_FAENAMIENTO };
                    
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_AMERB_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_AMERB_CREADA };
                    
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudAmerbSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_AMERB };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_AMERB };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_AMERB };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_AMERB };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_AMERB };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_AMERB };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_AMERB };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_AMERB };
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_EXPERIMENTALES_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_EXPERIMENTALES_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_EXPERIMENTALES_AMERB_CREADA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_EXPERIMENTALES_AMERB_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_EXPERIMENTALES_AMERB_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_EXPERIMENTALES_AMERB_CREADO };
                }
                else
                {

                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_AMERB;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesAmerbSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_EXPERIMENTALES_AMERB };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_EXPERIMENTALES_AMERB };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_EXPERIMENTALES_AMERB };
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_EXPERIMENTALES_CONCESION_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_EXPERIMENTALES_CONCESION_CREADA };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_EXPERIMENTALES_CONCESION_CREADA };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_EXPERIMENTALES_CONCESION_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_EXPERIMENTALES_CONCESION_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_EXPERIMENTALES_CONCESION_CREADO };
                }
                else 
                {

                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_EXPERIMENTALES_CONCESION;
                    ViewState["solicitudSession"] = paginas.solicitudExperimentalesConcesionSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_EXPERIMENTALES_CONCESION };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_EXPERIMENTALES_CONCESION };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_EXPERIMENTALES_CONCESION };
                    
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_ECMPO_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_ECMPO_CREADO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_ECMPO_CREADO };
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_SOLICITUD_ECMPO;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_SOLICITUD_SOLICITUD_ECMPO;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_SOLICITUD_ECMPO;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_SOLICITUD_ECMPO;
                    ViewState["solicitudSession"] = paginas.solicitudECMPOSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_ECMPO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_ECMPO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_ECMPO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_ECMPO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_ECMPO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_ECMPO };

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_ECMPO };
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


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_COLECTOR_CREADO };

                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA_COLECTORES"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_COLECTORES_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_COLECTOR_CREADO };
                    ViewState["SECCION_ESPECIFICA_FECHA_SOLICITADA_TITULAR"] = new int[] { rbSeccionUnidadEspacial.PT_FECHA_SOLICITADA_TITULAR_COLECTOR_CREADO };
                
                }
                else
                {
                    ViewState["URL_VER"] = paginas.URL_VER_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_DOCUMENTO"] = paginas.URL_ADMINISTRAR_DOCUMENTO_COLECTORES_SEMILLA;
                    ViewState["URL_ADMINISTRAR_SOLICITUD"] = paginas.URL_ADMINISTRAR_COLECTORES_SEMILLA;
                    ViewState["URL_EVALUAR"] = paginas.URL_EVALUAR_COLECTORES_SEMILLA;
                    ViewState["URL_ERROR"] = paginas.URL_ERROR_COLECTORES_SEMILLA;
                    ViewState["solicitudSession"] = paginas.solicitudColectorSession;


                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = new int[] { rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = new int[] { rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = new int[] { rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = new int[] { rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = new int[] { rbSeccionUnidadEspacial.PT_OBSERVACIONES_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = new int[] { rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_COLECTOR };

                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA_COLECTORES"] = new int[] { rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_COLECTORES_COLECTOR };
                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = new int[] { rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_COLECTOR};
                    ViewState["SECCION_ESPECIFICA_FECHA_SOLICITADA_TITULAR"] = new int[] { rbSeccionUnidadEspacial.PT_FECHA_SOLICITADA_TITULAR_COLECTOR };
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

                    int[] tiposModificacionFormaCultivo = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEspecieAutorizada = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEstructuraTecnica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionProgramaProduccion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionCultivoAlgas = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservaciones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionBotonGuardar = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int[] tiposModificacionArchivoAdjunto = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    
                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_AMERB_AMPLIA_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_AMERB_AMPLIA_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_AMERB_AMPLIA_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_AMERB_ESPECIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_AMERB_ESPECIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_AMERB_ESPECIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_AMERB_ESPECIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_AMERB_ESPECIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_AMERB_ESPECIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_AMERB_ESPECIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_AMERB_ESPECIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_AMERB_PT;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_AMERB_PT;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_AMERB_PT;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_AMERB_PT;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_AMERB_PT;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_AMERB_PT;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_AMERB_PT;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_AMERB_PT;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_AMERB_REDUCE_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_AMERB_REDUCE_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_AMERB_REDUCE_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_AMERB_REGULARIZACION;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_AMERB_REDUCE_REGULARIZACION;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_AMERB_REDUCE_REGULARIZACION;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_AMERB_REDUCE_REGULARIZACION;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_AMERB_REDUCE_REGULARIZACION;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_AMERB_REDUCE_REGULARIZACION;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_AMERB_REDUCE_REGULARIZACION;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_AMERB_REDUCE_REGULARIZACION;
                            
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = tiposModificacionFormaCultivo;
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = tiposModificacionEspecieAutorizada;
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = tiposModificacionEstructuraTecnica;
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = tiposModificacionProgramaProduccion;
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = tiposModificacionCultivoAlgas;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservaciones;
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = tiposModificacionBotonGuardar;

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = tiposModificacionArchivoAdjunto;
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

                    int[] tiposModificacionFormaCultivo = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEspecieAutorizada = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEstructuraTecnica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionProgramaProduccion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionCultivoAlgas = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservaciones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionBotonGuardar = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int[] tiposModificacionArchivoAdjunto = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_ACOPIO_ESPECIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_ACOPIO_ESPECIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_ACOPIO_ESPECIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_ACOPIO_PT;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_ACOPIO_PT;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_ACOPIO_PT;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_ACOPIO_REGULARIZACION;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_ACOPIO_REDUCE_REGULARIZACION;
                            
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = tiposModificacionFormaCultivo;
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = tiposModificacionEspecieAutorizada;
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = tiposModificacionEstructuraTecnica;
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = tiposModificacionProgramaProduccion;
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = tiposModificacionCultivoAlgas;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservaciones;
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = tiposModificacionBotonGuardar;

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = tiposModificacionArchivoAdjunto;
                    
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

                    int[] tiposModificacionFormaCultivo = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEspecieAutorizada = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEstructuraTecnica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionProgramaProduccion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionCultivoAlgas = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservaciones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionBotonGuardar = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int[] tiposModificacionArchivoAdjunto = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    
                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_FAENAMIENTO_ESPECIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_FAENAMIENTO_ESPECIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_PT;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_FAENAMIENTO_PT;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_FAENAMIENTO_PT;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CENTRO_FAENAMIENTO_REGULARIZACION;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CENTRO_FAENAMIENTO_REDUCE_REGULARIZACION;
                            
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = tiposModificacionFormaCultivo;
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = tiposModificacionEspecieAutorizada;
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = tiposModificacionEstructuraTecnica;
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = tiposModificacionProgramaProduccion;
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = tiposModificacionCultivoAlgas;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservaciones;
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = tiposModificacionBotonGuardar;

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = tiposModificacionArchivoAdjunto;
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

                    int[] tiposModificacionFormaCultivo = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEspecieAutorizada = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEstructuraTecnica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionProgramaProduccion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionCultivoAlgas = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservaciones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionBotonGuardar = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int[] tiposModificacionArchivoAdjunto = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    
                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_ECMPO_AMPLIA_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_ECMPO_AMPLIA_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_ECMPO_ESPECIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_ECMPO_ESPECIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_ECMPO_ESPECIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_ECMPO_ESPECIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_ECMPO_ESPECIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_ECMPO_ESPECIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_ECMPO_ESPECIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_ECMPO_ESPECIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_ECMPO_PT;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_ECMPO_PT;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_ECMPO_PT;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_ECMPO_PT;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_ECMPO_PT;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_ECMPO_PT;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_ECMPO_PT;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_ECMPO_PT;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_ECMPO_REDUCE_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_ECMPO_REDUCE_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_ECMPO_REDUCE_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_ECMPO_REGULARIZACION;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_ECMPO_REDUCE_REGULARIZACION;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_ECMPO_REDUCE_REGULARIZACION;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_ECMPO_REDUCE_REGULARIZACION;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_ECMPO_REDUCE_REGULARIZACION;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_ECMPO_REDUCE_REGULARIZACION;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_ECMPO_REDUCE_REGULARIZACION;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_ECMPO_REDUCE_REGULARIZACION;
                            
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = tiposModificacionFormaCultivo;
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = tiposModificacionEspecieAutorizada;
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = tiposModificacionEstructuraTecnica;
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = tiposModificacionProgramaProduccion;
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = tiposModificacionCultivoAlgas;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservaciones;
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = tiposModificacionBotonGuardar;

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = tiposModificacionArchivoAdjunto;
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

                    int[] tiposModificacionFormaCultivo = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEspecieAutorizada = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionEstructuraTecnica = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionProgramaProduccion = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionCultivoAlgas = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionObservaciones = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    int[] tiposModificacionBotonGuardar = new int[solicitudConcesion.tipoModificacionesTram.Count];

                    int[] tiposModificacionArchivoAdjunto = new int[solicitudConcesion.tipoModificacionesTram.Count];
                    
                    int contador = 0;
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CONCESION_AMPLIA_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CONCESION_AMPLIA_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CONCESION_ESPECIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CONCESION_ESPECIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CONCESION_ESPECIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CONCESION_ESPECIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CONCESION_ESPECIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CONCESION_ESPECIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CONCESION_ESPECIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CONCESION_ESPECIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CONCESION_PT;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CONCESION_PT;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CONCESION_PT;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CONCESION_PT;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CONCESION_PT;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CONCESION_PT;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CONCESION_PT;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CONCESION_PT;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_MOD_CONCESION_REDUCE_SUPERFICIE;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_MOD_CONCESION_REDUCE_SUPERFICIE;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_MOD_CONCESION_REDUCE_SUPERFICIE;
                            
                            contador++;
                        }
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            tiposModificacionFormaCultivo[contador] = rbSeccionUnidadEspacial.PT_FORMA_DE_CULTIVO_CONCESION_REGULARIZACION;
                            tiposModificacionEspecieAutorizada[contador] = rbSeccionUnidadEspacial.PT_ESPECIE_AUTORIZADA_CONCESION_REGULARIZACION;
                            tiposModificacionEstructuraTecnica[contador] = rbSeccionUnidadEspacial.PT_ESTRUCTURA_TECNICA_CONCESION_REGULARIZACION;
                            tiposModificacionProgramaProduccion[contador] = rbSeccionUnidadEspacial.PT_PROGRAMA_PRODUCCION_CONCESION_REGULARIZACION;
                            tiposModificacionCultivoAlgas[contador] = rbSeccionUnidadEspacial.PT_CULTIVO_ALGAS_CONCESION_REGULARIZACION;
                            tiposModificacionObservaciones[contador] = rbSeccionUnidadEspacial.PT_OBSERVACIONES_CONCESION_REGULARIZACION;
                            tiposModificacionBotonGuardar[contador] = rbSeccionUnidadEspacial.PT_BOTON_GUARDAR_CONCESION_REGULARIZACION;

                            tiposModificacionArchivoAdjunto[contador] = rbSeccionUnidadEspacial.PT_ARCHIVO_ADJUNTO_CONCESION_REGULARIZACION;
                            
                            contador++;
                        }
                    }

                    ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"] = tiposModificacionFormaCultivo;
                    ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"] = tiposModificacionEspecieAutorizada;
                    ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"] = tiposModificacionEstructuraTecnica;
                    ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"] = tiposModificacionProgramaProduccion;
                    ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"] = tiposModificacionCultivoAlgas;
                    ViewState["SECCION_ESPECIFICA_OBSERVACIONES"] = tiposModificacionObservaciones;
                    ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"] = tiposModificacionBotonGuardar;

                    ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"] = tiposModificacionArchivoAdjunto;
                    
                }
            }
        }

        //protected void Page_Init(object sender, System.EventArgs e)
        //{
        //    string script = "invoca_calendarios(\"proyectoTecnico\");";
        //    ScriptManager.RegisterStartupScript(Parent.Page, Parent.Page.GetType(), "scriptCalentario", script.ToString(), true);
        //}
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                setearModulo();

                SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
                usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

                if (solicitudInicial == null || usuario_logeado == null)
                {
                    Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
                }

                //REGLAS POR TIPO DE MODIFICACION VS QUE SE PUEDE MODIFICAR (PASO 1)
                bool tieneAsignadaSolicitudAux = solicitudInicial.tieneAsignadaSolicitud;
                Funciones.AplicarReglaTipoModificacion(solicitudInicial, Funciones.PROYECTO_TECNICO);


                // Inicializamos el formulario
                Initialize_Form();
                
                


                //FORMA CULTIVO INGRESO
                //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_FORMA_DE_CULTIVO"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                //{
                //    TipoCultivo.Enabled = true;
                //    AlimentoAlgaFresca.Enabled = true;
                //    AlimentoPellet.Enabled = true;
                //    AlimentoOtro.Enabled = true;
                //    NombreAlimentoOtro.Enabled = true;
                //}
                //else
                //{
                //    TipoCultivo.Enabled = false;
                //    AlimentoAlgaFresca.Enabled = false;
                //    AlimentoPellet.Enabled = false;
                //    AlimentoOtro.Enabled = false;
                //    NombreAlimentoOtro.Enabled = false;
                //}

                //ESPECIE AUTORIZADA O GRUPO AUTORIZADO FORMULARIO DE INGRESO
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelFormularioIngresoEspecieAutorizada.Visible = true;
                    //PanelFormularioIngresoGrupoAutorizada.Visible = true;
                }
                else
                {
                    PanelFormularioIngresoEspecieAutorizada.Visible = false;
                    //PanelFormularioIngresoGrupoAutorizada.Visible = false;
                }


                //ESTRUCTURAS A INSTALAR FORMULARIO DE INGRESO
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelFormularioIngresoEstructuraTecnica.Visible = true;
                }
                else
                {
                    PanelFormularioIngresoEstructuraTecnica.Visible = false;
                }

                //ESTRUCTURAS A INSTALAR COLECTORES
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA_COLECTORES"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelEstructuraColector.Visible = true;
                }
                else
                {
                    PanelEstructuraColector.Visible = false;
                }


                //PROGRAMA DE PRODUCCION FORMULARIO DE INGRESO
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelFormularioIngresoProgramaProduccion.Visible = true;
                }
                else
                {
                    PanelFormularioIngresoProgramaProduccion.Visible = false;
                }


                //CULTIVO ALGAS
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_CULTIVA_ALGAS"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    //Algas_DirSustrato.Enabled = true;
                    //Algas_IndirSustrato.Enabled = true;
                    //Algas_Suspendido.Enabled = true;
                    //Algas_Estanque.Enabled = true;
                    //Algas_Otro.Enabled = true;
                    //AlgaOtroDef.ReadOnly = false;

                    TipoCultivoAlgas.Enabled = true;
                    RadioButtonListUtilizaMangasPlasticas1.Enabled = true;
                    RadioButtonListUtilizaMangasPlasticas2.Enabled = true;

                    //DensidadSiembra.ReadOnly = false;

                    //TipoFondoDuro.Enabled = true;
                    //TipoFondoSemi.Enabled = true;
                    //TipoFondoBlando.Enabled = true;
                    //TipoFondoOtro.Enabled = true;
                    //FondoOtroDef.ReadOnly = false;

                }
                else
                {
                    //Algas_DirSustrato.Enabled = false;
                    //Algas_IndirSustrato.Enabled = false;
                    //Algas_Suspendido.Enabled = false;
                    //Algas_Estanque.Enabled = false;
                    //Algas_Otro.Enabled = false;
                    //AlgaOtroDef.ReadOnly = true;

                    TipoCultivoAlgas.Enabled = false;
                    RadioButtonListUtilizaMangasPlasticas1.Enabled = false;
                    RadioButtonListUtilizaMangasPlasticas2.Enabled = false;

                    //DensidadSiembra.ReadOnly = true;

                    //TipoFondoDuro.Enabled = false;
                    //TipoFondoSemi.Enabled = false;
                    //TipoFondoBlando.Enabled = false;
                    //TipoFondoOtro.Enabled = false;
                    //FondoOtroDef.ReadOnly = true;
                }

                //FECHA SOLICITADA TITULAR
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_FECHA_SOLICITADA_TITULAR"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelFechaSolicitadaTitular.Visible = true;
                }
                else
                {
                    PanelFechaSolicitadaTitular.Visible = false;
                }

                //OBSERVACIONES INGRESO
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_OBSERVACIONES"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    observaciones.ReadOnly = false;
                }
                else
                {
                    observaciones.ReadOnly = true;
                }

                //ARCHIVOS ADJUNTOS
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    PanelFormularioIngresoArchivoAdjunto.Visible = true;
                }
                else
                {
                    PanelFormularioIngresoArchivoAdjunto.Visible = false;
                }

                //BOTON GUARDAR
                if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_BOTON_GUARDAR"], this.usuario_logeado, solicitudInicial, rbAccion.EDITAR))
                {
                    Guardar.Enabled = true;
                    Modificar.Enabled = true;
                }
                else
                {
                    Guardar.Enabled = false;
                    Modificar.Enabled = false;
                }


                //SECCCIONES QUE DEBEN APARECER 
                if (solicitudInicial != null && solicitudInicial.tipoTramite != null)
                {
                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA)
                    {
                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = true;
                        UpdatePanel_EspecieAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = true;
                        UpdatePanel_EstructuraTecnica.Update();

                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = true;
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = true;
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = true;
                        UpdatePanel_ArchivoAdjunto.Update();

                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA)
                    {

                        //SECTOR
                        DetalleSector aDetalleSector = relocalizacionService.obtenerDetalleSector_Solicitud(solicitudInicial.idSolConcesion);
                        ViewState["aDetalleSector"] = aDetalleSector;

                        //FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, aDetalleSector.tipoRelocalizacion.id);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, aDetalleSector.tipoRelocalizacion.id);
                        UpdatePanel_EspecieAutorizada.Update();

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, aDetalleSector.tipoRelocalizacion.id);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, aDetalleSector.tipoRelocalizacion.id);
                        UpdatePanel_EstructuraTecnica.Update();
                        
                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, aDetalleSector.tipoRelocalizacion.id);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, aDetalleSector.tipoRelocalizacion.id);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, aDetalleSector.tipoRelocalizacion.id);
                        UpdatePanel_ArchivoAdjunto.Update();
                    
                    }


                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        UpdatePanel_EspecieAutorizada.Update();

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        UpdatePanel_EstructuraTecnica.Update();

                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO);
                        UpdatePanel_ArchivoAdjunto.Update();

                    }


                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        UpdatePanel_EspecieAutorizada.Update();

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        UpdatePanel_EstructuraTecnica.Update();
                        
                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO);
                        UpdatePanel_ArchivoAdjunto.Update();

                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        UpdatePanel_EstructuraTecnica.Update();

                        
                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB);
                        UpdatePanel_ArchivoAdjunto.Update();


                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        UpdatePanel_EstructuraTecnica.Update();

                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB);
                        UpdatePanel_ArchivoAdjunto.Update();


                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        UpdatePanel_EstructuraTecnica.Update();
                        
                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION);
                        UpdatePanel_ArchivoAdjunto.Update();
                    }


                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        UpdatePanel_EstructuraTecnica.Update();
                        
                        //PROGRAMA DE PRODUCCION
                        Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO);
                        UpdatePanel_ArchivoAdjunto.Update();


                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
                    {
                        ////FORMA DE CULTIVO
                        //PanelFormaCultivo.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FORMA_CULIVO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        //UpdatePanelFormaCultivo.Update();

                        //ESPECIE AUTORIZADA
                        PanelEspecieAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        UpdatePanel_EspecieAutorizada.Update();

                        ////GRUPO ESPECIE AUTORIZADA
                        //PanelGrupoAutorizada.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESPECIES_AUTORIZADAS, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        //UpdatePanel_GrupoAutorizada.Update();

                        //ESTRUCTURAS A INSTALAR
                        Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        UpdatePanel_EstructuraTecnica.Update();
                        
                        ////ESTRUCTURAS A INSTALAR COLECTORES
                        //Panel_EstructuraTecnica.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO_COLECTOR, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        //UpdatePanel_EstructuraTecnica.Update();

                        //FECHA SOLICITADA TITULAR
                        PanelEstructuraColector.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.FECHA_SOLICITADA_TITULAR_COLECTOR, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        UpdatePanelEstructuraColector.Update();

                        //PROGRAMA DE PRODUCCION
                        //Panel_ProgrProduccionPT.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.PROGRAMA_DE_PRODUCCION, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        //UpdatePanel_ProgrProduccionPT.Update();

                        //OBSERVACIONES PROYECTO TECNICO
                        PanelObservacionesProyectoTecnico.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.OBSERVACIONES_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        UpdatePanelObservacionesProyectoTecnico.Update();

                        //ARCHIVO ADJUNTO PROYECTO TECNICO
                        Panel_ArchivoAdjunto.Visible = despliegueSeccionService.ObtieneDespliegueSeccion(rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO, rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA);
                        UpdatePanel_ArchivoAdjunto.Update();
                    }


                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                    {

                        List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0);

                        //if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                        //{
                        //    PanelFormaCultivo.Visible = true;
                        //    UpdatePanelFormaCultivo.Update();
                        //}

                        /* Especies Autorizadas */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                        {
                            PanelEspecieAutorizada.Visible = true;
                            UpdatePanel_EspecieAutorizada.Update();

                            //PanelGrupoAutorizada.Visible = true;
                            //UpdatePanel_GrupoAutorizada.Update();
                        }

                        /* Estructura Técnica a Instalar cada Año */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                        {
                            Panel_EstructuraTecnica.Visible = true;
                            UpdatePanel_EstructuraTecnica.Update();
                        }

                        /* Programa de Producción */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                        {
                            Panel_ProgrProduccionPT.Visible = true;
                            UpdatePanel_ProgrProduccionPT.Update();
                        }

                        /* Observaciones de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                        {
                            PanelObservacionesProyectoTecnico.Visible = true;
                            UpdatePanelObservacionesProyectoTecnico.Update();
                        }

                        /* Archivo Adjunto de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO))
                        {
                            Panel_ArchivoAdjunto.Visible = true;
                            UpdatePanel_ArchivoAdjunto.Update();
                        }
                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB)
                    {
                        List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0,rbTipo.UNID_ESPACIAL_MOD_AMERB);

                        //if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                        //{
                        //    PanelFormaCultivo.Visible = true;
                        //    UpdatePanelFormaCultivo.Update();
                        //}

                        /* Especies Autorizadas */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                        {
                            PanelEspecieAutorizada.Visible = true;
                            UpdatePanel_EspecieAutorizada.Update();

                            //PanelGrupoAutorizada.Visible = true;
                            //UpdatePanel_GrupoAutorizada.Update();
                        }

                        /* Estructura Técnica a Instalar cada Año */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                        {
                            Panel_EstructuraTecnica.Visible = true;
                            UpdatePanel_EstructuraTecnica.Update();
                        }

                        /* Programa de Producción */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                        {
                            Panel_ProgrProduccionPT.Visible = true;
                            UpdatePanel_ProgrProduccionPT.Update();
                        }

                        /* Observaciones de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                        {
                            PanelObservacionesProyectoTecnico.Visible = true;
                            UpdatePanelObservacionesProyectoTecnico.Update();
                        }

                        /* Archivo Adjunto de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO))
                        {
                            Panel_ArchivoAdjunto.Visible = true;
                            UpdatePanel_ArchivoAdjunto.Update();
                        }
                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO)
                    {
                        List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_ACOPIO);

                        //if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                        //{
                        //    PanelFormaCultivo.Visible = true;
                        //    UpdatePanelFormaCultivo.Update();
                        //}

                        /* Especies Autorizadas */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                        {
                            PanelEspecieAutorizada.Visible = true;
                            UpdatePanel_EspecieAutorizada.Update();

                            //PanelGrupoAutorizada.Visible = true;
                            //UpdatePanel_GrupoAutorizada.Update();
                        }

                        /* Estructura Técnica a Instalar cada Año */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                        {
                            Panel_EstructuraTecnica.Visible = true;
                            UpdatePanel_EstructuraTecnica.Update();
                        }

                        /* Programa de Producción */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                        {
                            Panel_ProgrProduccionPT.Visible = true;
                            UpdatePanel_ProgrProduccionPT.Update();
                        }

                        /* Observaciones de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                        {
                            PanelObservacionesProyectoTecnico.Visible = true;
                            UpdatePanelObservacionesProyectoTecnico.Update();
                        }

                        /* Archivo Adjunto de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO))
                        {
                            Panel_ArchivoAdjunto.Visible = true;
                            UpdatePanel_ArchivoAdjunto.Update();
                        }
                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO)
                    {
                        List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO);

                        //if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                        //{
                        //    PanelFormaCultivo.Visible = true;
                        //    UpdatePanelFormaCultivo.Update();
                        //}

                        /* Especies Autorizadas */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                        {
                            PanelEspecieAutorizada.Visible = true;
                            UpdatePanel_EspecieAutorizada.Update();

                            //PanelGrupoAutorizada.Visible = true;
                            //UpdatePanel_GrupoAutorizada.Update();
                        }

                        /* Estructura Técnica a Instalar cada Año */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                        {
                            Panel_EstructuraTecnica.Visible = true;
                            UpdatePanel_EstructuraTecnica.Update();
                        }

                        /* Programa de Producción */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                        {
                            Panel_ProgrProduccionPT.Visible = true;
                            UpdatePanel_ProgrProduccionPT.Update();
                        }

                        /* Observaciones de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                        {
                            PanelObservacionesProyectoTecnico.Visible = true;
                            UpdatePanelObservacionesProyectoTecnico.Update();
                        }

                        /* Archivo Adjunto de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO))
                        {
                            Panel_ArchivoAdjunto.Visible = true;
                            UpdatePanel_ArchivoAdjunto.Update();
                        }
                    }

                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                    {
                        List<DespliegueMenuSeccion> despliegueMenuSeccionList = despliegueMenuSeccionDA.ListarDespliegueMenuSeccion_UE(rbMenu.PROYECTO_TECNICO_MODIFICACION, 0, rbTipo.UNID_ESPACIAL_MOD_ECMPO);

                        //if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.FORMA_CULIVO))
                        //{
                        //    PanelFormaCultivo.Visible = true;
                        //    UpdatePanelFormaCultivo.Update();
                        //}

                        /* Especies Autorizadas */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESPECIES_AUTORIZADAS))
                        {
                            PanelEspecieAutorizada.Visible = true;
                            UpdatePanel_EspecieAutorizada.Update();

                            //PanelGrupoAutorizada.Visible = true; 
                            //UpdatePanel_GrupoAutorizada.Update();
                        }

                        /* Estructura Técnica a Instalar cada Año */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO))
                        {
                            Panel_EstructuraTecnica.Visible = true;
                            UpdatePanel_EstructuraTecnica.Update();
                        }

                        /* Programa de Producción */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.PROGRAMA_DE_PRODUCCION))
                        {
                            Panel_ProgrProduccionPT.Visible = true;
                            UpdatePanel_ProgrProduccionPT.Update();
                        }

                        /* Observaciones de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.OBSERVACIONES_PROYECTO_TECNICO))
                        {
                            PanelObservacionesProyectoTecnico.Visible = true;
                            UpdatePanelObservacionesProyectoTecnico.Update();
                        }

                        /* Archivo Adjunto de Proyecto Técnico */
                        if (solicitudModificacionService.despligueSeccionesMenu(solicitudInicial, despliegueMenuSeccionList, rbSeccion.ARCHIVO_ADJUNTO_PROYECTO_TECNICO))
                        {
                            Panel_ArchivoAdjunto.Visible = true;
                            UpdatePanel_ArchivoAdjunto.Update();
                        }
                    }
                }

                //REGLAS POR TIPO DE MODIFICACION VS QUE SE PUEDE MODIFICAR (PASO 1)
                solicitudInicial.tieneAsignadaSolicitud = tieneAsignadaSolicitudAux;

            }
        }


        protected void Initialize_Form()
        {
            // Cargamos los combobox
            Initialize_Comboboxs();
            UpdatePanelPesoProm.Visible = false;
            UpdatePanelPesoPromR1.Visible = false;
            UpdatePanelPesoPromR2.Visible = false;
            
            //PanelTipoAlimento.Visible = false;
            
            Panel_Agregar_EstructuraTecnica.Visible = true;
            
            //NombreAlimentoOtro.Text = "";
            //NombreAlimentoOtro.ReadOnly = true;
            //AlgaOtroDef.Text = "";
            //AlgaOtroDef.ReadOnly = true;
            //FondoOtroDef.Text = "";
            //FondoOtroDef.ReadOnly = true;
            PanelCultivoAlgas.Visible = false;

            SolicitudConcesion solicitudInicial = (SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()];
            /* solicitudInicial = new SolicitudConcesion();
             solicitudInicial.idSolConcesion = Convert.ToInt32(1);*/
            if (solicitudInicial != null && solicitudInicial.idSolConcesion > 0)
            {
                IdSolicitud.Value = Convert.ToString(solicitudInicial.idSolConcesion);
                ProyectoTecnicoService proyService = new ProyectoTecnicoService();
                ProyectoTecnico proyTecnicoForm = proyService.ObtenerProyectoTecnico(Convert.ToInt32(IdSolicitud.Value), 0);

                if (proyTecnicoForm != null)
                {
                    IdProyectoTecnico.Value = Convert.ToString(proyTecnicoForm.IdProyectoTecnico);
                    
                    CargarGrillaPT("EspecieAutorizada", proyTecnicoForm);
                    //CargarGrillaPT("GrupoAutorizada", proyTecnicoForm);
                    
                    CargarGrillaPT("EstructuraTecnica", proyTecnicoForm);

                    //CargarGrillaPT("EstructuraTecnicaDirectoSust", proyTecnicoForm);
                    
                    CargarGrillaPT("ProgramaProd", proyTecnicoForm);
                    
                    CargarGrillaPT("ArchivoBinario", proyTecnicoForm);

                    Carga_Combobox("EspecieProgramaProduccion");

                    Carga_Combobox("GrupoProgramaProduccion");

                    //Carga_Combobox("GrupoProgramaProduccion2");
                    
                    despliegaObservaciones(proyTecnicoForm);
                    //despliegaFormaCultivo(proyTecnicoForm);
                    
                    despliegaFechaSolicitadaTitular(proyTecnicoForm);

                    despliegaEstructuraTecnicaColectores(proyTecnicoForm);

                    ModificarProyTecnico.Visible = true;
                    GuardarProyTecnico.Visible = false;
                }
                else
                {
                    IdProyectoTecnico.Value = Convert.ToString(0);
                    CargarGrillaPT("EspecieAutorizada", null);
                    CargarGrillaPT("GrupoAutorizada", null);
                    CargarGrillaPT("EstructuraTecnica", null);
                    CargarGrillaPT("EstructuraTecnicaDirectoSust", null);

                    CargarGrillaPT("ProgramaProd", null);
                    CargarGrillaPT("ArchivoBinario", null);

                    Carga_Combobox("EspecieProgramaProduccion");
                    Carga_Combobox("GrupoProgramaProduccion");
                    //Carga_Combobox("GrupoProgramaProduccion2");
                    
                    despliegaObservaciones(null);
                    //despliegaFormaCultivo(null);
                    despliegaFechaSolicitadaTitular(null);

                    despliegaEstructuraTecnicaColectores(null);

                    ModificarProyTecnico.Visible = false;
                    GuardarProyTecnico.Visible = true;
                }
            }

            else
            {
                // Response.Redirect("~/Solicitudes/Registrar/errorGeneral.aspx");
                Response.Redirect(ViewState["URL_ADMINISTRAR_SOLICITUD"].ToString());
            }

        }

        private void despliegaEstructuraTecnicaColectores(ProyectoTecnico proyTecnicoForm)
        {

            if (proyTecnicoForm != null && proyTecnicoForm.estructTecnicaColector != null)
            {
                idEstructuraProyectoTecnico.Value = Convert.ToString(proyTecnicoForm.estructTecnicaColector.idEstructPT);
                
                NumeroColectores.Text = Convert.ToString(proyTecnicoForm.estructTecnicaColector.numColectores);

                NumeroLineasColectores.Text = Convert.ToString(proyTecnicoForm.estructTecnicaColector.numLineas);
            }
            
        }
        
        private void despliegaFechaSolicitadaTitular(ProyectoTecnico proyTecnicoForm)
        {
            if (proyTecnicoForm != null)
            {

                if (proyTecnicoForm != null && proyTecnicoForm.fechaInicio != null && !proyTecnicoForm.fechaInicio.Equals("") && proyTecnicoForm.fechaInicio != default(DateTime))
                {
                    FechaRecepcion.Text = proyTecnicoForm.fechaInicio.ToShortDateString().ToString();
                }

                if (proyTecnicoForm != null && proyTecnicoForm.fechaTermino != null && !proyTecnicoForm.fechaTermino.Equals("") && proyTecnicoForm.fechaTermino != default(DateTime))
                {
                    FechaIngresoTramite.Text = proyTecnicoForm.fechaTermino.ToShortDateString().ToString();
                }
            }
        }


        private void despliegaObservaciones(ProyectoTecnico pt)
        {
            if (pt != null && pt.observaciones != null && !pt.observaciones.Equals(""))
            {

                observaciones.Text = Convert.ToString(pt.observaciones);

            }
        }
       

        //private void despliegaFormaCultivo(ProyectoTecnico pt)
        //{
        //    if (pt != null && pt.tipoCultivo != null && pt.tipoCultivo.id > 0)
        //    {
        //        TipoCultivo.SelectedValue = Convert.ToString(pt.tipoCultivo.id);
        //        AlimentoPorTC_OnSelectedIndexChanged(null, null);
        //        if (pt.tipoAlimento != null && pt.tipoAlimento.Count > 0)
        //        {
        //            foreach (TipoAlimentoProyecto aux in pt.tipoAlimento)
        //            {
        //                if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_ALGA))
        //                {
        //                    AlimentoAlgaFresca.Checked = true;
        //                    despliegaMetodoCultivoAlgas(pt);

        //                }
        //                if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_PELLET))
        //                {
        //                    AlimentoPellet.Checked = true;
        //                }
        //                if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.TIPO_ALIMENTO_PT_OTRO))
        //                {
        //                    AlimentoOtro.Checked = true;
        //                    NombreAlimentoOtro.ReadOnly = false;
        //                    NombreAlimentoOtro.Text = aux.detalle;
        //                }
        //            }
        //        }
        //    }

        //}

        
        //private void despliegaMetodoCultivoAlgas(ProyectoTecnico pt)
        //{
        //    if (pt.metodoCultivoAlgas != null && pt.metodoCultivoAlgas.Count > 0)
        //    {
        //        foreach (TipoAlimentoProyecto aux in pt.metodoCultivoAlgas)
        //        {
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_DIR_SUSTRATO))
        //            {
        //                Algas_DirSustrato.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_INDIR_SUSTRATO))
        //            {
        //                Algas_IndirSustrato.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_SUSPENDIDO))
        //            {
        //                Algas_Suspendido.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_ESTANQUE))
        //            {
        //                Algas_Estanque.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_OTRO))
        //            {
        //                Algas_Otro.Checked = true;
        //                AlgaOtroDef.ReadOnly = false;
        //                AlgaOtroDef.Text = aux.detalle;
        //            }
        //        }
        //    }
        //    if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(true))
        //    {
        //        RadioButtonListUtilizaMangasPlasticas1.Checked = true;
        //    }
        //    if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(false))
        //    {
        //        RadioButtonListUtilizaMangasPlasticas2.Checked = true;
        //    }

        //    if (pt.densidadSiembra != null && pt.densidadSiembra > 0)
        //    {
        //        TextBoxDensidadSiembra.Text = Convert.ToString(pt.densidadSiembra);
        //    }
        //    if (pt.tipoFondo != null && pt.tipoFondo.Count > 0)
        //    {
        //        foreach (TipoAlimentoProyecto aux in pt.tipoFondo)
        //        {
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_DURO))
        //            {
        //                TipoFondoDuro.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_SEMIDURO))
        //            {
        //                TipoFondoSemi.Checked = true;
        //            }
        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_BLANDO))
        //            {
        //                TipoFondoBlando.Checked = true;
        //            }

        //            if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_OTRO))
        //            {
        //                TipoFondoOtro.Checked = true;
        //                FondoOtroDef.ReadOnly = false;
        //                FondoOtroDef.Text = aux.detalle;
        //            }
        //        }
        //    }

        //    PanelCultivoAlgas.Visible = true;

        //}
        

        /**
         * CARGA LA INFORMACION GUARDADA DEL METODO DE CULTIVO DE ALGAS, EN ESTE PUNTO YA
         * SE SABE QUE SE DEBE MOSTRAR
         */ 
        private void CargaMetodoCultivoAlgas(ProyectoTecnico pt)
        {

            //if (pt.metodoCultivoAlgas != null && pt.metodoCultivoAlgas.Count > 0)
            //{
            //    foreach (TipoAlimentoProyecto aux in pt.metodoCultivoAlgas)
            //    {
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_DIR_SUSTRATO))
            //        {
            //            Algas_DirSustrato.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_INDIR_SUSTRATO))
            //        {
            //            Algas_IndirSustrato.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_SUSPENDIDO))
            //        {
            //            Algas_Suspendido.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_ESTANQUE))
            //        {
            //            Algas_Estanque.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.MET_ALGAS_OTRO))
            //        {
            //            Algas_Otro.Checked = true;
            //            AlgaOtroDef.ReadOnly = false;
            //            AlgaOtroDef.Text = aux.detalle;
            //        }
            //    }
            //}

            if (pt.tipoCultivoAlgas != null && pt.tipoCultivoAlgas.id > 0)
            {
                TipoCultivoAlgas.SelectedValue = Convert.ToString(pt.tipoCultivoAlgas.id);
            }

            if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(true))
            {
                RadioButtonListUtilizaMangasPlasticas1.Checked = true;
            }
            if (pt.mangasPlasticas != null && pt.mangasPlasticas.Equals(false))
            {
                RadioButtonListUtilizaMangasPlasticas2.Checked = true;
            }
            
            //if (pt.densidadSiembra != null && pt.densidadSiembra > 0)
            //{
            //    DensidadSiembra.Text = Convert.ToString(pt.densidadSiembra);
            //}
            //if (pt.tipoFondo != null && pt.tipoFondo.Count > 0)
            //{
            //    foreach (TipoAlimentoProyecto aux in pt.tipoFondo)
            //    {
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_DURO))
            //        {
            //            TipoFondoDuro.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_SEMIDURO))
            //        {
            //            TipoFondoSemi.Checked = true;
            //        }
            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_BLANDO))
            //        {
            //            TipoFondoBlando.Checked = true;
            //        }

            //        if (aux.tipoAlimento != null && (aux.tipoAlimento.id == rbTipo.T_FONDO_OTRO))
            //        {
            //            TipoFondoOtro.Checked = true;
            //            FondoOtroDef.ReadOnly = false;
            //            FondoOtroDef.Text = aux.detalle;
            //        }
            //    }
            //}

            PanelCultivoAlgas.Visible = true;

        }


        protected void Initialize_Comboboxs()
        {

            //NombreAlimentoOtro.ReadOnly = true;
            //Carga_Combobox("GrupoEspecieAutorizadas2");
            //GrupoEspecieAutorizadas2.SelectedValue = "-1";
            
            Carga_Combobox("EspecieAutorizada");
            EspecieAutorizada.SelectedValue = "-1";
            //EspecieAutorizada.Enabled = false;

            Carga_Combobox("GrupoEspecieAutorizadas");
            GrupoEspecieAutorizadas.SelectedValue = "-1";
            GrupoEspecieAutorizadas.Enabled = false;

            Carga_Combobox("EtapaDeCultivoAutorizadas");
            EtapaDeCultivoAutorizadas.SelectedValue = "-1";

            Carga_Combobox("TipoCultivo");
            TipoCultivo.SelectedValue = "-1";

            Carga_Combobox("TipoAlimento");
            TipoAlimento.SelectedValue = "-1";

            Carga_Combobox("TipoCultivoAlgas");
            TipoCultivoAlgas.SelectedValue = "-1";

            //Carga_Combobox("EtapaDeCultivoAutorizadas2");
            //EtapaDeCultivoAutorizadas2.SelectedValue = "-1";

            Carga_Combobox("TipoEstructura");
            TipoEstructura.SelectedValue = "-1";

            Carga_Combobox("FormaEstructura");
            FormaEstructura.SelectedValue = "-1";

            Carga_Combobox("UnidadDeMedida");
            UnidadDeMedida.SelectedValue = "-1";

            Carga_Combobox("VolumenUnidadMedida");
            VolumenUnidadMedida.SelectedValue = "-1";

            Carga_Combobox("Anio");
            Anio.SelectedValue = "-1";
            
            Carga_Combobox("EspecieProgramaProduccion");
            EspecieProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("AnioProgramaProducc");
            AnioProgramaProducc.SelectedValue = "-1";

            //Carga_Combobox("GrupoProgramaProduccion2");
            //GrupoProgramaProduccion.SelectedValue = "-1";

            //Carga_Combobox("EtapaCultivoProgramaProduccion");
            //EtapaCultivoProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("UnidadProgramaProduccion");
            UnidadProgramaProduccion.SelectedValue = "-1";

            Carga_Combobox("PesoPromedioEjemplares");
            PesoPromedioEjemplares.SelectedValue = "-1";

            Carga_Combobox("TipoArchivo");
            TipoArchivo.SelectedValue = "-1";
        }


        protected void Carga_Combobox(string combobox)
        {
            switch (combobox)
            {
                    
                //case "GrupoEspecieAutorizadas2":
                //    // Cargamos el combobox: GrupoEspecieAutorizadas
                //    GrupoEspecieAutorizadas2.Items.Clear();
                //    GrupoEspecieAutorizadas2.DataSource = parametroDa.ListarGrupoEspecie(0);
                //    GrupoEspecieAutorizadas2.DataTextField = "descripcion";
                //    GrupoEspecieAutorizadas2.DataValueField = "id";
                //    GrupoEspecieAutorizadas2.DataBind();
                //    GrupoEspecieAutorizadas2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                //    break;

                case "EspecieAutorizada":
                    // Cargamos el combobox: EspecieAutorizada
                    EspecieAutorizada.Items.Clear();
                    EspecieAutorizada.DataSource = parametroDa.ListarEspecies(0, "", 0);
                    EspecieAutorizada.DataTextField = "descripcion";
                    EspecieAutorizada.DataValueField = "id";
                    EspecieAutorizada.DataBind();
                    EspecieAutorizada.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "GrupoEspecieAutorizadas":
                    // Cargamos el combobox: GrupoEspecieAutorizadas
                    GrupoEspecieAutorizadas.Items.Clear();
                    GrupoEspecieAutorizadas.DataSource = parametroDa.ListarGrupoEspecie(0);
                    GrupoEspecieAutorizadas.DataTextField = "descripcion";
                    GrupoEspecieAutorizadas.DataValueField = "id";
                    GrupoEspecieAutorizadas.DataBind();
                    GrupoEspecieAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "EtapaDeCultivoAutorizadas":
                    // Cargamos el combobox: EtapaDeCultivoAutorizadas
                    EtapaDeCultivoAutorizadas.Items.Clear();
                    if (Convert.ToInt32(EspecieAutorizada.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieAutorizada.SelectedValue), 0);
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    }
                    else if (Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue) > 0)
                    {
                        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue));
                        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                        EtapaDeCultivoAutorizadas.DataValueField = "id";
                        EtapaDeCultivoAutorizadas.DataBind();
                    };
                    EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoCultivo":
                    // Cargamos el combobox: TipoCultivo
                    TipoCultivo.Items.Clear();

                    TipoCultivo.DataSource = tipoDa.ListarTipo("TIPO_CULTIVO");
                    TipoCultivo.DataTextField = "descripcion";
                    TipoCultivo.DataValueField = "id";
                    TipoCultivo.DataBind();
                    TipoCultivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "TipoAlimento":
                    // Cargamos el combobox: TipoAlimento Se debe cambiar a los pl que me dara la gabi
                    TipoAlimento.Items.Clear();
                    if (Convert.ToInt32(EspecieAutorizada.SelectedValue) > 0)
                    {
                        TipoAlimento.DataSource = tipoDa.ListarTipoAlimentoEspecie(Convert.ToInt32(EspecieAutorizada.SelectedValue),false);
                        TipoAlimento.DataTextField = "descripcion";
                        TipoAlimento.DataValueField = "id";
                        TipoAlimento.DataBind();
                    }
                    else if (Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue) > 0)
                    {
                        TipoAlimento.DataSource = tipoDa.ListarTipoAlimentoEspecie(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue), true);
                        TipoAlimento.DataTextField = "descripcion";
                        TipoAlimento.DataValueField = "id";
                        TipoAlimento.DataBind();
                    }
                    TipoAlimento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "TipoCultivoAlgas":
                    // Cargamos el combobox: TipoCultivoAlgas
                    TipoCultivoAlgas.Items.Clear();

                    TipoCultivoAlgas.DataSource = tipoDa.ListarTipo("MET_CULTIVO_ALGAS");
                    TipoCultivoAlgas.DataTextField = "descripcion";
                    TipoCultivoAlgas.DataValueField = "id";
                    TipoCultivoAlgas.DataBind();
                    TipoCultivoAlgas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                //case "EtapaDeCultivoAutorizadas2":
                //    // Cargamos el combobox: EtapaDeCultivoAutorizadas
                //    EtapaDeCultivoAutorizadas2.Items.Clear();
                //    if (Convert.ToInt32(GrupoEspecieAutorizadas2.SelectedValue) > 0)
                //    {
                //        EtapaDeCultivoAutorizadas2.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoEspecieAutorizadas2.SelectedValue));
                //        EtapaDeCultivoAutorizadas2.DataTextField = "descripcion";
                //        EtapaDeCultivoAutorizadas2.DataValueField = "id";
                //        EtapaDeCultivoAutorizadas2.DataBind();
                //    };
                //    EtapaDeCultivoAutorizadas2.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                //    break;

                case "TipoEstructura":
                    // Cargamos el combobox: TipoEstructura
                    TipoEstructura.Items.Clear();
                    TipoEstructura.DataSource = parametroDa.ListarEstructuraTecnica(0);
                    TipoEstructura.DataTextField = "descripcion";
                    TipoEstructura.DataValueField = "id";
                    TipoEstructura.DataBind();
                    TipoEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "FormaEstructura":
                    // Cargamos el combobox: FormaEstructura
                    FormaEstructura.Items.Clear();
                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        FormaEstructura.DataSource = parametroDa.ListarFormaPorEstructura(Convert.ToInt32(TipoEstructura.SelectedValue), 0, "");
                        FormaEstructura.DataTextField = "descripcion";
                        FormaEstructura.DataValueField = "id";
                        FormaEstructura.DataBind();
                    };
                    FormaEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "UnidadDeMedida":
                    // Cargamos el combobox: UnidadDeMedida
                    UnidadDeMedida.Items.Clear();
                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        UnidadDeMedida.DataSource = parametroDa.ListarGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), "paSelRbTipoMedidaEstructura", "@idEstructuraTecnica", "idTipoMedida", "siglaMedida");
                        UnidadDeMedida.DataTextField = "descripcion";
                        UnidadDeMedida.DataValueField = "id";
                        UnidadDeMedida.DataBind();
                    };
                    UnidadDeMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                    break;

                case "VolumenUnidadMedida":
                    // Cargamos el combobox: VolumenUnidadMedida
                    VolumenUnidadMedida.Items.Clear();
                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        List<ParametroGenerico> listVolumenUnidadMedida = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "VOLUMEN_UNID_MEDIDA", "idTipo", "nombreTipo");

                        foreach (ParametroGenerico volumenUnidMedida in listVolumenUnidadMedida)
                        {
                            VolumenUnidadMedida.Items.Add(new ListItem(volumenUnidMedida.descripcion, Convert.ToString(volumenUnidMedida.id)));
                        }

                        VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                        VolumenUnidadMedida.SelectedValue = "-1";
                        VolumenUnidadMedida.DataBind();
                    };
                    
                    break;

                case "Anio":
                    // Cargamos el combobox: Anio
                    Anio.Items.Clear();
                    Anio.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "ESTRUCTURAS_POR_AÑO", "idTipo", "nombreTipo");
                    Anio.DataTextField = "descripcion";
                    Anio.DataValueField = "id";
                    Anio.DataBind();
                    Anio.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                
                case "EspecieProgramaProduccion":
                    // Cargamos el combobox: EspecieProgramaProduccion
                    EspecieProgramaProduccion.Items.Clear();
                    List<EspecieAutorizadaPT> List_EspecieAut2 = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    HashSet<int> especieHash = new HashSet<int>();
                    if (List_EspecieAut2 != null && List_EspecieAut2.Count > 0)
                    {
                        foreach (EspecieAutorizadaPT esp in List_EspecieAut2)
                        {
                            if (esp.accion == accion.INGRESAR || esp.accion == accion.MODIFICAR || esp.accion == accion.LISTADO)
                            {
                                if (esp.especie != null && esp.especie.id > 0 && (especieHash.Count == 0 || !especieHash.Contains(esp.especie.id)))
                                {
                                    especieHash.Add(esp.especie.id);
                                    EspecieProgramaProduccion.Items.Add(new ListItem(esp.DescripcionEspecie, Convert.ToString(esp.IDEspecie)));
                                }

                            }

                        }
                    }
                    EspecieProgramaProduccion.Items.Insert(0, new ListItem("-- Especie --", "-1"));
                    EspecieProgramaProduccion.SelectedValue = "-1";
                    EspecieProgramaProduccion.DataBind();

                    break;

                case "GrupoProgramaProduccion":
                    // Cargamos el combobox: GrupoProgramaProduccion
                    GrupoProgramaProduccion.Items.Clear();
                    //string especies = "";
                    List<EspecieAutorizadaPT> List_EspecieAutGr = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    if (List_EspecieAutGr != null && List_EspecieAutGr.Count > 0)
                    {
                        //foreach (EspecieAutorizadaPT esp in List_EspecieAutGr)
                        //{
                        //    if (esp.accion == accion.INGRESAR || esp.accion == accion.MODIFICAR || esp.accion == accion.LISTADO)
                        //    {
                        //        if (especies.Equals(""))
                        //        {
                        //            especies = "(" + esp.IDEspecie;
                        //        }
                        //        else
                        //        {
                        //            especies = especies + "," + esp.IDEspecie;
                        //        }
                        //    }
                        //}
                        //especies = especies + ")";

                        //List<ParametroGenerico> gruposEspeciePP = parametroDa.ListarGrupoEspecie(especies);
                        //if (gruposEspeciePP != null && gruposEspeciePP.Count > 0)
                        //{
                        //    foreach (ParametroGenerico gepp in gruposEspeciePP)
                        //    {
                        //        GrupoProgramaProduccion.Items.Add(new ListItem(gepp.descripcion, Convert.ToString(gepp.id)));
                        //    }
                        //}
                        foreach (EspecieAutorizadaPT esp in List_EspecieAutGr) {
                            if (esp.accion == accion.INGRESAR || esp.accion == accion.MODIFICAR || esp.accion == accion.LISTADO)
                            {
                                if (esp.grupoEspecieAutoriz != null)
                                {
                                    GrupoProgramaProduccion.Items.Add(new ListItem(esp.grupoEspecieAutoriz.descripcion, esp.grupoEspecieAutoriz.id.ToString()));
                                }
                            }
                        }

                    }
                    GrupoProgramaProduccion.Items.Insert(0, new ListItem("-- Grupo --", "-1"));
                    GrupoProgramaProduccion.SelectedValue = "-1";
                    GrupoProgramaProduccion.DataBind();
                    break;

                //case "GrupoProgramaProduccion2":
                //    // Cargamos el combobox: GrupoProgramaProduccion
                //    GrupoProgramaProduccion.Items.Clear();
                //    string especies2 = "";

                //    List<EspecieAutorizadaPT> ListEspeciesAutorizados = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                //    List<GrupoPT> ListGruposAutorizados = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];

                //    if ((ListEspeciesAutorizados != null && ListEspeciesAutorizados.Count() > 0) || (ListGruposAutorizados != null && ListGruposAutorizados.Count() > 0))
                //    {

                //        //GRUPOS POR LAS ESPECIES AGREGADAS
                //        if (ListEspeciesAutorizados != null && ListEspeciesAutorizados.Count() > 0) {

                //            foreach (EspecieAutorizadaPT esp in ListEspeciesAutorizados)
                //            {
                //                if (esp.accion == accion.INGRESAR || esp.accion == accion.MODIFICAR || esp.accion == accion.LISTADO)
                //                {
                //                    if (especies2.Equals(""))
                //                    {
                //                        especies2 = "(" + esp.IDEspecie;
                //                    }
                //                    else
                //                    {
                //                        especies2 = especies2 + "," + esp.IDEspecie;
                //                    }
                //                }
                //            }
                //        }

                //        //GRUPOS POR LAS GRUPOS AGREGADAS
                //        if (ListGruposAutorizados != null && ListGruposAutorizados.Count() > 0)
                //        {

                //            foreach (GrupoPT grupo in ListGruposAutorizados)
                //            {
                //                if (grupo.accion == accion.INGRESAR || grupo.accion == accion.MODIFICAR || grupo.accion == accion.LISTADO)
                //                {


                //                    List<ParametroGenerico> especiesGrupo = new List<ParametroGenerico>();
                //                    especiesGrupo = parametroDa.ListarEspecies(0, "", grupo.grupo.id);

                //                    if (especiesGrupo != null && especiesGrupo.Count() > 0) {

                //                        foreach (ParametroGenerico especieEnGrupo in especiesGrupo) {

                //                            if (especies2.Equals(""))
                //                            {
                //                                especies2 = "(" + especieEnGrupo.id;
                //                            }
                //                            else
                //                            {
                //                                especies2 = especies2 + "," + especieEnGrupo.id;
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                        

                //        especies2 = especies2 + ")";


                //        List<ParametroGenerico> gruposEspeciePP = parametroDa.ListarGrupoEspecie(especies2);
                //        if (gruposEspeciePP != null && gruposEspeciePP.Count > 0)
                //        {
                //            foreach (ParametroGenerico gepp in gruposEspeciePP)
                //            {
                //                GrupoProgramaProduccion.Items.Add(new ListItem(gepp.descripcion, Convert.ToString(gepp.id)));
                //            }
                //        }
                //    }
                //    GrupoProgramaProduccion.Items.Insert(0, new ListItem("-- Grupo --", "-1"));
                //    GrupoProgramaProduccion.SelectedValue = "-1";
                //    GrupoProgramaProduccion.DataBind();
                //    break;


                //case "EtapaCultivoProgramaProduccion":
                //    // Cargamos el combobox: EtapaCultivoProgramaProduccion
                //    EtapaCultivoProgramaProduccion.Items.Clear();
                //    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
                //    {
                //        EtapaCultivoProgramaProduccion.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), 0);
                //        EtapaCultivoProgramaProduccion.DataTextField = "descripcion";
                //        EtapaCultivoProgramaProduccion.DataValueField = "id";
                //        EtapaCultivoProgramaProduccion.DataBind();
                //    }
                //    else if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
                //    {
                //        EtapaCultivoProgramaProduccion.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue));
                //        EtapaCultivoProgramaProduccion.DataTextField = "descripcion";
                //        EtapaCultivoProgramaProduccion.DataValueField = "id";
                //        EtapaCultivoProgramaProduccion.DataBind();

                //    }
                //    EtapaCultivoProgramaProduccion.Items.Insert(0, new ListItem("-- Etapa --", "-1"));



                //    /*
                //      EtapaDeCultivoAutorizadas.Items.Clear();
                //    if (Convert.ToInt32(EspecieAutorizada.SelectedValue) > 0)
                //    {
                //        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloEspecie(Convert.ToInt32(EspecieAutorizada.SelectedValue), 0);
                //        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                //        EtapaDeCultivoAutorizadas.DataValueField = "id";
                //        EtapaDeCultivoAutorizadas.DataBind();
                //    }
                //    else if (Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue) > 0)
                //    {
                //        EtapaDeCultivoAutorizadas.DataSource = parametroDa.ListarEtapaDesarrolloGrupo(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue));
                //        EtapaDeCultivoAutorizadas.DataTextField = "descripcion";
                //        EtapaDeCultivoAutorizadas.DataValueField = "id";
                //        EtapaDeCultivoAutorizadas.DataBind();
                //    };
                //    EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                     
                //     */


                //    break;
                case "UnidadProgramaProduccion":
                    // Cargamos el combobox: UnidadProgramaProduccion
                    UnidadProgramaProduccion.Items.Clear();
                    UnidadProgramaProduccion.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "UNID_EJEMPLARES", "idTipo", "nombreTipo");
                    UnidadProgramaProduccion.DataTextField = "descripcion";
                    UnidadProgramaProduccion.DataValueField = "id";
                    UnidadProgramaProduccion.DataBind();
                    UnidadProgramaProduccion.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
                case "PesoPromedioEjemplares":
                    // Cargamos el combobox: PesoPromedioEjemplares
                    PesoPromedioEjemplares.Items.Clear();
                    PesoPromedioEjemplares.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "RANGO_PESO_EJEMPLARES", "idTipo", "nombreTipo");
                    PesoPromedioEjemplares.DataTextField = "descripcion";
                    PesoPromedioEjemplares.DataValueField = "id";
                    PesoPromedioEjemplares.DataBind();
                    PesoPromedioEjemplares.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "AnioProgramaProducc":
                    // Cargamos el combobox: AnioProgramaProducc
                    AnioProgramaProducc.Items.Clear();
                    AnioProgramaProducc.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "ESTRUCTURAS_POR_AÑO", "idTipo", "nombreTipo");
                    AnioProgramaProducc.DataTextField = "descripcion";
                    AnioProgramaProducc.DataValueField = "id";
                    AnioProgramaProducc.DataBind();
                    AnioProgramaProducc.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;

                case "TipoArchivo":
                    // Cargamos el combobox: TipoArchivo
                    TipoArchivo.Items.Clear();
                    TipoArchivo.DataSource = parametroDa.ListarTiposGenerico("paSelRbTipo", "@grupo", "TIPO_ARCHIVO_PT", "idTipo", "nombreTipo");
                    TipoArchivo.DataTextField = "descripcion";
                    TipoArchivo.DataValueField = "id";
                    TipoArchivo.DataBind();
                    TipoArchivo.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

                    break;
            }
        }


        protected void RadGrupoEspecieChecked(object sender, EventArgs e)
        {
            EtapaDeCultivoAutorizadas.Items.Clear();
            EtapaDeCultivoAutorizadas.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

            TipoAlimento.Items.Clear();
            TipoAlimento.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));

            if (EspeciesRad.Checked == true)
            {
                GrupoEspecieAutorizadas.SelectedIndex = -1;
                GrupoEspecieAutorizadas.Enabled = false;
                EspecieAutorizada.Enabled = true;

            }
            else if (GrupoEspeciesRad.Checked == true)
            {
                EspecieAutorizada.SelectedIndex = -1;
                EspecieAutorizada.Enabled = false;
                GrupoEspecieAutorizadas.Enabled = true;
            }

        }


        //protected void TipoAlimentoChecked(object sender, EventArgs e)
        //{
        //    if (AlimentoOtro.Checked == true)
        //    {
        //        NombreAlimentoOtro.ReadOnly = false;
        //    }
        //    else
        //    {
        //        NombreAlimentoOtro.Text = "";
        //        NombreAlimentoOtro.ReadOnly = true;
        //    }

        //}
        
        
        //protected void AlgaOtroChecked(object sender, EventArgs e)
        //{
            //if (Algas_Otro.Checked == true)
            //{
            //    AlgaOtroDef.ReadOnly = false;
            //}
            //else
            //{
            //    AlgaOtroDef.Text = "";
            //    AlgaOtroDef.ReadOnly = true;
            //}

        //}
       
        
        //protected void FondoOtroChecked(object sender, EventArgs e)
        //{
            //if (TipoFondoOtro.Checked == true)
            //{
            //    FondoOtroDef.ReadOnly = false;
            //}
            //else
            //{
            //    FondoOtroDef.Text = "";
            //    FondoOtroDef.ReadOnly = true;
            //}

        //}


        
        //protected void AlgaChecked(object sender, EventArgs e)
        //{
        //    Algas_DirSustrato.Checked = false;
        //    Algas_IndirSustrato.Checked = false;
        //    Algas_Suspendido.Checked = false;
        //    Algas_Estanque.Checked = false;
        //    Algas_Otro.Checked = false;
        //    AlgaOtroDef.Text = "";
        //    AlgaOtroDef.ReadOnly = true;
        //    RadioButtonListUtilizaMangasPlasticas1.Checked = false;
        //    RadioButtonListUtilizaMangasPlasticas2.Checked = false;
        //    TextBoxDensidadSiembra.Text = "";
        //    TipoFondoDuro.Checked = false;
        //    TipoFondoSemi.Checked = false;
        //    TipoFondoBlando.Checked = false;
        //    TipoFondoOtro.Checked = false;
        //    FondoOtroDef.Text = "";
        //    FondoOtroDef.ReadOnly = true;

        //    if (AlimentoAlgaFresca.Checked == true)
        //    {
        //        PanelCultivoAlgas.Visible = true;
        //    }
        //    else
        //    {
        //        PanelCultivoAlgas.Visible = false;
        //    }
        //    UpdatePanel_PanelCultivoAlgas.Update();

        //}
        


        /**
         * DETERMINA SI CULTIVO DE ALGAS DEBE APARECER 
         * SI EL PROYECTO TECNICO NO ES NULL ENTONCES REVISA SI TIENE ALGAS (ES AL CARGAR LA PAGINA POR PRIMERA VEZ)
         * SI EL PROYECTO TECNICO ES NULL ENTONCES  SE DEBE REVISAR EL VIEW STATE
         */ 
        protected void DespliegaCultivoAlgas(ProyectoTecnico proyectoTecnico)
        {
            //Algas_DirSustrato.Checked = false;
            //Algas_IndirSustrato.Checked = false;
            //Algas_Suspendido.Checked = false;
            //Algas_Estanque.Checked = false;
            //Algas_Otro.Checked = false;
            //AlgaOtroDef.Text = "";
            //AlgaOtroDef.ReadOnly = true;
            RadioButtonListUtilizaMangasPlasticas1.Checked = false;
            RadioButtonListUtilizaMangasPlasticas2.Checked = false;
            //TextBoxDensidadSiembra.Text = "";
            //TipoFondoDuro.Checked = false;
            //TipoFondoSemi.Checked = false;
            //TipoFondoBlando.Checked = false;
            //TipoFondoOtro.Checked = false;
            //FondoOtroDef.Text = "";
            //FondoOtroDef.ReadOnly = true;

            //DETERMINAR SI SE ESTA CULTIVANDO ALGAS

            bool mostrarCultivoAlgas = false;

            //ESPECIES
            List<EspecieAutorizadaPT> especiesProyectoTecnico = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];

            if (especiesProyectoTecnico != null)
            {

                foreach (EspecieAutorizadaPT especieInList in especiesProyectoTecnico)
                {

                    if (especieInList.accion == accion.INGRESAR || especieInList.accion == accion.MODIFICAR || especieInList.accion == accion.LISTADO) {

                        //if (especieInList.grupoEspecieAutoriz != null && especieInList.grupoEspecieAutoriz.id == grupoEspecie.ALGAS)
                        if (especieInList.grupoEspecie != null && especieInList.grupoEspecie.id == grupoEspecie.ALGAS)
                        {
                            mostrarCultivoAlgas = true;
                            break;
                        }
                    }
                }
            }

            //GRUPOS
            //List<GrupoPT> grupoProyectoTecnico = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];

            //if (grupoProyectoTecnico != null)
            //{

            //    foreach (GrupoPT especieInList in grupoProyectoTecnico)
            //    {

            //        if (especieInList.accion == accion.INGRESAR || especieInList.accion == accion.MODIFICAR || especieInList.accion == accion.LISTADO)
            //        {

            //            if (especieInList.grupo != null && especieInList.grupo.id == grupoEspecie.ALGAS)
            //            {
            //                mostrarCultivoAlgas = true;
            //                break;
            //            }
            //        }
            //    }
            //}


            if (mostrarCultivoAlgas)
            {
                //YA EXISTE UN PROTECTO TECNICO, SE INTENTA CARGAR LO QUE EXISTE
                if (proyectoTecnico != null)
                {
                    this.CargaMetodoCultivoAlgas(proyectoTecnico);
                }

                PanelCultivoAlgas.Visible = true;
            }
            else
            {
                PanelCultivoAlgas.Visible = false;

            }

            
            UpdatePanel_PanelCultivoAlgas.Update();

        }


        protected void TipoAnioEstructuraTecnica_Selected(object sender, EventArgs e)
        {

            TextBoxAnio1.Text = "";
            TextBoxAnio1.ReadOnly = true;
            TextBoxAnio2.Text = "";
            TextBoxAnio2.ReadOnly = true;
            TextBoxAnio3.Text = "";
            TextBoxAnio3.ReadOnly = true;
            TextBoxAnio4.Text = "";
            TextBoxAnio4.ReadOnly = true;
            TextBoxAnio5.Text = "";
            TextBoxAnio5.ReadOnly = true;

            if (Convert.ToInt32(Anio.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)
            {
                TextBoxAnio1.Text = "";
                TextBoxAnio1.ReadOnly = false;

                TextBoxAnio5.Text = "";
                TextBoxAnio5.Visible = false;
                divAnio5.Visible = false;
                TextBoxAnio5.ReadOnly = true;

                TextBoxAnio3.Text = "";
                TextBoxAnio3.Visible = false;
                divAnio3.Visible = false;
                TextBoxAnio3.ReadOnly = true;

                TextBoxAnio4.Text = "";
                TextBoxAnio4.Visible = false;
                divAnio4.Visible = false;
                TextBoxAnio4.ReadOnly = true;

                TextBoxAnio2.Text = "";
                labelAnioMaximo.Text = "Máximo";
                TextBoxAnio2.ReadOnly = false;
            }
            else if (Convert.ToInt32(Anio.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO)
            {
                TextBoxAnio1.Text = "";
                TextBoxAnio1.ReadOnly = false;

                TextBoxAnio5.Text = "";
                TextBoxAnio5.Visible = true;
                divAnio5.Visible = true;
                TextBoxAnio5.ReadOnly = false;

                TextBoxAnio3.Text = "";
                TextBoxAnio3.Visible = true;
                divAnio3.Visible = true;
                TextBoxAnio3.ReadOnly = false;

                TextBoxAnio4.Text = "";
                TextBoxAnio4.Visible = true;
                divAnio4.Visible = true;
                TextBoxAnio4.ReadOnly = false;

                TextBoxAnio2.Text = "";
                labelAnioMaximo.Text = "Año 2";
                TextBoxAnio2.ReadOnly = false;
            }


        }


        

        protected void TipoAnioProgramaProduccion_Selected(object sender, EventArgs e)
        {

            AnioProd1.Text = "";
            AnioProd1.ReadOnly = true;
            AnioProd2.Text = "";
            AnioProd2.ReadOnly = true;
            AnioProd3.Text = "";
            AnioProd3.ReadOnly = true;
            AnioProd4.Text = "";
            AnioProd4.ReadOnly = true;
            AnioProd5.Text = "";
            AnioProd5.ReadOnly = true;

            if (Convert.ToInt32(AnioProgramaProducc.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)
            {
                AnioProd1.Text = "";
                AnioProd1.ReadOnly = false;

                AnioProd5.Text = "";
                AnioProd5.Visible = false;
                divAnio5ProgProd.Visible = false;
                AnioProd5.ReadOnly = true;

                AnioProd3.Text = "";
                AnioProd3.Visible = false;
                divAnio3ProgProd.Visible = false;
                AnioProd3.ReadOnly = true;

                AnioProd4.Text = "";
                AnioProd4.Visible = false;
                divAnio4ProgProd.Visible = false;
                AnioProd4.ReadOnly = true;

                AnioProd2.Text = "";
                Anio2ProgProd.Text = "Máximo";
                AnioProd2.ReadOnly = false;
            }
            else if (Convert.ToInt32(AnioProgramaProducc.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO)
            {
                AnioProd1.Text = "";
                AnioProd1.ReadOnly = false;

                AnioProd5.Text = "";
                AnioProd5.Visible = true;
                divAnio5ProgProd.Visible = true;
                AnioProd5.ReadOnly = false;

                AnioProd3.Text = "";
                AnioProd3.Visible = true;
                divAnio3ProgProd.Visible = true;
                AnioProd3.ReadOnly = false;

                AnioProd4.Text = "";
                AnioProd4.Visible = true;
                divAnio4ProgProd.Visible = true;
                AnioProd4.ReadOnly = false;

                AnioProd2.Text = "";
                Anio2ProgProd.Text = "Año 2";
                AnioProd2.ReadOnly = false;
            }

            UpdatePanel_ProgrProduccionPT.Update();
        }

        protected void CalculoVolumenAutomaticoChange(object sender, EventArgs e)
        {
            this.calcularVolumenAutomatico();
        }


        protected void calcularVolumenAutomatico()
        {
            float volumen;
            volumen = 0;
            TextBoxVolumenValorMedida.ReadOnly = false;

            this.validaDataMedidasEstructura();

            if (Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
            {

                if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CUADRADA)){
                  
                    if (!TextBoxLargoM.Text.Equals(TextBoxAnchoM.Text))
                    {
                        TextBoxAnchoM.Text = TextBoxLargoM.Text;
                    }
                 
                }

                if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CUADRADA) && (!TextBoxLargoM.Text.Equals("") && !TextBoxAltoM.Text.Equals("") && this.validaDec(TextBoxAltoM.Text)))  //Cuadrada  -> Ancho  * Largo * Alto
                {

                    volumen = Convert.ToSingle(TextBoxAnchoM.Text) * Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxAltoM.Text);
                    TextBoxVolumenValorMedida.Text = volumen.ToString();
                    TextBoxVolumenValorMedida.ReadOnly = true;
                    
                    this.calcularDimensionAcumulado();
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CIRCULAR) && (!TextBoxDiametroM.Text.Equals("") && !TextBoxAltoM.Text.Equals("") && this.validaDec(TextBoxDiametroM.Text) && this.validaDec(TextBoxAltoM.Text)))//Circular -> PI * (Diámetro/2)^2 * Alto
                {
                    volumen = Convert.ToSingle(Math.PI) * ((Convert.ToSingle(TextBoxDiametroM.Text) / 2) * (Convert.ToSingle(TextBoxDiametroM.Text) / 2)) * (Convert.ToSingle(TextBoxAltoM.Text));
                    TextBoxVolumenValorMedida.Text = volumen.ToString();
                    TextBoxVolumenValorMedida.ReadOnly = true;
                    this.calcularDimensionAcumulado();
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_RECTANGULAR) && (!TextBoxAnchoM.Text.Equals("") && !TextBoxLargoM.Text.Equals("") && !TextBoxAltoM.Text.Equals("") && this.validaDec(TextBoxAnchoM.Text) && this.validaDec(TextBoxAltoM.Text)))  //Rectangular  -> Ancho  * Largo * Alto
                {
                    volumen = Convert.ToSingle(TextBoxAnchoM.Text) * Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxAltoM.Text);
                    TextBoxVolumenValorMedida.Text = volumen.ToString();
                    TextBoxVolumenValorMedida.ReadOnly = true;
                    this.calcularDimensionAcumulado();
                }
            }
        }


        protected void CalculoDimensionAcumuladoChange(object sender, EventArgs e)
        {
            this.calcularDimensionAcumulado();
        }


        protected void calcularDimensionAcumulado()
        {

            CompareValidator_TextBoxVolumenValorMedida.Validate();

            if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && !TotalAcumuladoNumero.Text.Equals(""))
            {
                ProyectoTecnicoService proyectoService = new ProyectoTecnicoService();
                List<EstructuraTecnica> estructuraTecnicas = proyectoService.ListaEstructuraTecnica(0);
                float total = 0;
                float area = 0;
                if (estructuraTecnicas != null && estructuraTecnicas.Count > 0)
                {
                    foreach (EstructuraTecnica estructAux in estructuraTecnicas)
                    {
                        if (estructAux.idEstructura == Convert.ToInt32(TipoEstructura.SelectedValue) && (estructAux.aplicaArea && !estructAux.aplicaVolumen))
                        {
                            //SE MANEJA POR AREA, SE DEBE CALCULAR EL AREA Y MULTIPLICARLA POR LA CANTIDAD
                            area = this.calcularArea();

                            if (area > -1 && !TotalAcumuladoNumero.Text.Equals(""))
                            {
                                total = Convert.ToSingle(area) * Convert.ToInt32(TotalAcumuladoNumero.Text);
                                //TotalAcumuladoDimension.Text = Convert.ToString(total);
                                UpdatePanel_TotalAcumuladoDimension.Update();
                            }
                            break;
                        }
                        else if (estructAux.idEstructura == Convert.ToInt32(TipoEstructura.SelectedValue) && (!estructAux.aplicaArea && estructAux.aplicaVolumen))
                        {
                            //SI MANEJA POR VOLUMEN, SOLO SE DEBE MULTIPLICAR VOLUMEN POR LA CANTIDAD
                            if (!TextBoxVolumenValorMedida.Text.Equals("") && !TotalAcumuladoNumero.Text.Equals(""))
                            {
                                total = Convert.ToSingle(TextBoxVolumenValorMedida.Text) * Convert.ToInt32(TotalAcumuladoNumero.Text);
                                //TotalAcumuladoDimension.Text = Convert.ToString(total);
                                UpdatePanel_TotalAcumuladoDimension.Update();
                            }
                            break;
                        }
                    }
                }
                TotalAcumuladoDimension.Text = String.Format("{0:0.00}", total);
            }
        }


        public float calcularArea()
        {
            float area;
            area = -1;

            if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
            {

                if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CUADRADA) && (!TextBoxLargoM.Text.Equals("")))//Cuadrada  -> Largo * Largo 
                {
                    area = Convert.ToSingle(TextBoxLargoM.Text) * Convert.ToSingle(TextBoxLargoM.Text);
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_CIRCULAR) && (!TextBoxDiametroM.Text.Equals("")))//Circular -> PI * (Diámetro/2)^2
                {
                    area = Convert.ToSingle(Math.PI) * ((Convert.ToSingle(TextBoxDiametroM.Text) / 2) * (Convert.ToSingle(TextBoxDiametroM.Text) / 2));
                }
                else if ((Convert.ToInt32(FormaEstructura.SelectedValue) == rbTipo.TIPO_FORMA_RECTANGULAR) && (!TextBoxAnchoM.Text.Equals("") && !TextBoxLargoM.Text.Equals(""))) //Rectangular  -> Ancho * Largo
                {
                    area = Convert.ToSingle(TextBoxAnchoM.Text) * Convert.ToSingle(TextBoxLargoM.Text);
                }

                if (Convert.ToInt32(TipoEstructura.SelectedValue) == 1 && !TextBoxLargoM.Text.Equals(""))//LONG-LINE
                {
                    area = Convert.ToSingle(TextBoxLargoM.Text);
                }
            }

            return area;
        }


        protected void ManejoCamposEstructuraMedidas()
        {

            TextBoxLargoM.Text = "";
            TextBoxLargoM.ReadOnly = true;
            TextBoxLargoM.BackColor = System.Drawing.SystemColors.ControlLight;

            TextBoxAnchoM.Text = "";
            TextBoxAnchoM.ReadOnly = true;
            TextBoxAnchoM.BackColor = System.Drawing.SystemColors.ControlLight;

            TextBoxAltoM.Text = "";
            TextBoxAltoM.ReadOnly = true;
            TextBoxAltoM.BackColor = System.Drawing.SystemColors.ControlLight;

            TextBoxDiametroM.Text = "";
            TextBoxDiametroM.ReadOnly = true;
            TextBoxDiametroM.BackColor = System.Drawing.SystemColors.ControlLight;

            VolumenUnidadMedida.Items.Clear();
            VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
            VolumenUnidadMedida.BackColor = System.Drawing.SystemColors.ControlLight;

            TextBoxVolumenValorMedida.Text = "";
            TextBoxVolumenValorMedida.ReadOnly = true;
            TextBoxVolumenValorMedida.BackColor = System.Drawing.SystemColors.ControlLight;

            if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0 && Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
            {
                ProyectoTecnicoService proyService = new ProyectoTecnicoService();
                HashSet<int> resp = proyService.ListaEstructuraMedidas(Convert.ToInt32(TipoEstructura.SelectedValue), Convert.ToInt32(FormaEstructura.SelectedValue));

                if (resp != null && resp.Count > 0)
                {
                    foreach (int aux in resp)
                    {
                        if (aux == rbTipo.ESTRUCT_MEDIDA_ALTO)
                        {
                            TextBoxAltoM.Text = "";
                            TextBoxAltoM.ReadOnly = false;
                            TextBoxAltoM.BackColor = System.Drawing.SystemColors.Window;
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_ANCHO)
                        {
                            TextBoxAnchoM.Text = "";
                            TextBoxAnchoM.ReadOnly = false;
                            TextBoxAnchoM.BackColor = System.Drawing.SystemColors.Window;
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_DIAMETRO)
                        {
                            TextBoxDiametroM.Text = "";
                            TextBoxDiametroM.ReadOnly = false;
                            TextBoxDiametroM.BackColor = System.Drawing.SystemColors.Window;
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_LARGO)
                        {
                            TextBoxLargoM.Text = "";
                            TextBoxLargoM.ReadOnly = false;
                            TextBoxLargoM.BackColor = System.Drawing.SystemColors.Window;
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_VOLUMEN)
                        {
                            Carga_Combobox("VolumenUnidadMedida");
                            VolumenUnidadMedida.SelectedValue = "-1";
                            VolumenUnidadMedida.BackColor = System.Drawing.SystemColors.Window;

                            TextBoxVolumenValorMedida.Text = "";
                            TextBoxVolumenValorMedida.ReadOnly = false;
                            TextBoxVolumenValorMedida.BackColor = System.Drawing.SystemColors.Window;
                        }
                    }
                }
                EstructMedidas_UpdatePanel.Update();

            }

        }


        protected void CalculoTotalAcumuladoChange(object sender, EventArgs e)
        {

            int totalAcum = 0;

            if (Convert.ToInt32(Anio.SelectedValue) == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)
            {
                if (!TextBoxAnio1.Text.Equals("") && Convert.ToInt32(TextBoxAnio1.Text) != null)
                {
                    totalAcum = Convert.ToInt32(TextBoxAnio1.Text);
                }

                if (!TextBoxAnio2.Text.Equals("") && Convert.ToInt32(TextBoxAnio2.Text) > 0)
                {
                    totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio2.Text);
                }

            }
            else {

                if (!TextBoxAnio1.Text.Equals("") && Convert.ToInt32(TextBoxAnio1.Text) != null)
                {
                    totalAcum = Convert.ToInt32(TextBoxAnio1.Text);
                }
                if (!TextBoxAnio2.Text.Equals("") && Convert.ToInt32(TextBoxAnio2.Text) != null)
                {
                    totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio2.Text);
                }
                if (!TextBoxAnio3.Text.Equals("") && Convert.ToInt32(TextBoxAnio3.Text) != null)
                {
                    totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio3.Text);
                }
                if (!TextBoxAnio4.Text.Equals("") && Convert.ToInt32(TextBoxAnio4.Text) != null)
                {
                    totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio4.Text);
                }
                if (!TextBoxAnio5.Text.Equals("") && Convert.ToInt32(TextBoxAnio5.Text) != null)
                {
                    totalAcum = totalAcum + Convert.ToInt32(TextBoxAnio5.Text);
                }
            
            }

            

            TotalAcumuladoNumero.Text = Convert.ToString(totalAcum);
            this.calcularDimensionAcumulado();


        }

        

        protected void EtapaPorEspecieAutorizada_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("EtapaDeCultivoAutorizadas");
            Carga_Combobox("TipoAlimento");
        }


        protected void ManejoCamposEstructuraMedidasChanged(object sender, EventArgs e)
        {
            ManejoCamposEstructuraMedidas();
        }


        protected void FormaPorEstructura_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Combobox("FormaEstructura");
            Carga_Combobox("UnidadDeMedida");
            ManejoCamposTipoEstructura();
        }

        private void ManejoCamposTipoEstructura()
        {
            if (Convert.ToInt32(TipoEstructura.SelectedItem.Value) == tipoEstructura.DIRECTO_AL_SUSTRATO)
            {
                FormaEstructura.Items.Clear();
                FormaEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                FormaEstructura.Enabled = false;
                FormaEstructura.BackColor = System.Drawing.SystemColors.ControlLight;

                UnidadDeMedida.Items.Clear();
                UnidadDeMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                UnidadDeMedida.Enabled = false;
                UnidadDeMedida.BackColor = System.Drawing.SystemColors.ControlLight;

                TextBoxLargoM.Text = "";
                TextBoxLargoM.ReadOnly = true;
                TextBoxLargoM.BackColor = System.Drawing.SystemColors.ControlLight;

                TextBoxAnchoM.Text = "";
                TextBoxAnchoM.ReadOnly = true;
                TextBoxAnchoM.BackColor = System.Drawing.SystemColors.ControlLight;

                TextBoxAltoM.Text = "";
                TextBoxAltoM.ReadOnly = true;
                TextBoxAltoM.BackColor = System.Drawing.SystemColors.ControlLight;

                TextBoxDiametroM.Text = "";
                TextBoxDiametroM.ReadOnly = true;
                TextBoxDiametroM.BackColor = System.Drawing.SystemColors.ControlLight;

                VolumenUnidadMedida.Items.Clear();
                VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                VolumenUnidadMedida.Enabled = false;
                VolumenUnidadMedida.BackColor = System.Drawing.SystemColors.ControlLight;

                TextBoxVolumenValorMedida.Text = "";
                TextBoxVolumenValorMedida.ReadOnly = true;
                TextBoxVolumenValorMedida.BackColor = System.Drawing.SystemColors.ControlLight;

                DensidadSiembra.Text = "";
                DensidadSiembra.ReadOnly = false;
                DensidadSiembra.BackColor = System.Drawing.SystemColors.Window;

                TotalAcumuladoDimension.Text = "";
                TotalAcumuladoDimension.ReadOnly = true;
                TotalAcumuladoDimension.BackColor = System.Drawing.SystemColors.ControlLight;

                //EstructMedidas_UpdatePanel.Update();
                UpdatePanel_EstructuraTecnica.Update();
            }
            else {

                Carga_Combobox("FormaEstructura");
                FormaEstructura.SelectedValue = "-1";
                FormaEstructura.Enabled = true;
                FormaEstructura.BackColor = System.Drawing.SystemColors.Window;

                Carga_Combobox("UnidadDeMedida");
                UnidadDeMedida.SelectedValue = "-1";
                UnidadDeMedida.Enabled = true;
                UnidadDeMedida.BackColor = System.Drawing.SystemColors.Window;

                TextBoxLargoM.Text = "";
                TextBoxLargoM.ReadOnly = false;
                TextBoxLargoM.BackColor = System.Drawing.SystemColors.Window;

                TextBoxAnchoM.Text = "";
                TextBoxAnchoM.ReadOnly = false;
                TextBoxAnchoM.BackColor = System.Drawing.SystemColors.Window;

                TextBoxAltoM.Text = "";
                TextBoxAltoM.ReadOnly = false;
                TextBoxAltoM.BackColor = System.Drawing.SystemColors.Window;

                TextBoxDiametroM.Text = "";
                TextBoxDiametroM.ReadOnly = false;
                TextBoxDiametroM.BackColor = System.Drawing.SystemColors.Window;

                Carga_Combobox("VolumenUnidadMedida");
                VolumenUnidadMedida.SelectedValue = "-1";
                VolumenUnidadMedida.Enabled = true;
                VolumenUnidadMedida.BackColor = System.Drawing.SystemColors.Window;

                TextBoxVolumenValorMedida.Text = "";
                TextBoxVolumenValorMedida.ReadOnly = false;
                TextBoxVolumenValorMedida.BackColor = System.Drawing.SystemColors.Window;

                DensidadSiembra.Text = "";
                DensidadSiembra.ReadOnly = false;
                DensidadSiembra.BackColor = System.Drawing.SystemColors.Window;

                TotalAcumuladoDimension.Text = "";
                TotalAcumuladoDimension.ReadOnly = false;
                TotalAcumuladoDimension.BackColor = System.Drawing.SystemColors.Window;

                //EstructMedidas_UpdatePanel.Update();
                UpdatePanel_EstructuraTecnica.Update();
            }

            
            
        }
        

        //------------------------------------------------------------------------------------


        protected void GuardarEspecieAutorizada_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EspecieAutorizada");
            CargarGrillaPT("EspecieAutorizada", null);
        }


        protected void GuardarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EstructuraTecnica");
            CargarGrillaPT("EstructuraTecnica", null);
        }


        protected void ModificarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            ModificarGrillaPT("EstructuraTecnica");
            CargarGrillaPT("EstructuraTecnica", null);
        }


        protected void GuardarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("ProgramaProd");
            CargarGrillaPT("ProgramaProd", null);
        }


        protected void ModificarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            ModificarGrillaPT("ProgramaProd");
            CargarGrillaPT("ProgramaProd", null);
        }


        //protected void GuardarEjemplar_Click(object sender, ImageClickEventArgs e)
        //{
        //    AgregarGrillaPT("EjemplarPT");
        //    CargarGrillaPT("EjemplarPT", null);
        //}

        //------------------------------------------------------------------------------------



        private Object CargaObjeto(string seccion, string accionS)
        {

            Object objeto = null;

            switch (seccion)
            {
                case "EspecieAutorizada":

                    EspecieAutorizadaPT esp_aut = new EspecieAutorizadaPT();

                    if (EspeciesRad.Checked == true)
                    {
                        esp_aut.especieCheck = true;
                        esp_aut.especie = new ParametroGenerico(Convert.ToInt32(EspecieAutorizada.SelectedValue), Convert.ToString(EspecieAutorizada.SelectedItem.Text));
                    }
                    if (GrupoEspeciesRad.Checked == true)
                    {
                        esp_aut.grupoCheck = true;
                        esp_aut.grupoEspecieAutoriz = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas.SelectedItem.Text));
                    }
                    //esp_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas.SelectedItem.Text));
                    
                     esp_aut.etapaCultivoList = new List<EtapaCultivo>();
                     foreach (ListItem item in EtapaDeCultivoAutorizadas.Items)
                     {
                         if (item.Selected && Convert.ToInt32(item.Value) > 0)
                         {
                             EtapaCultivo etapaCultivo = new EtapaCultivo();
                             etapaCultivo.id_etapaDesarrollo = Convert.ToInt32(item.Value);
                             etapaCultivo.nombreEtapaDesarrollo = Convert.ToString(item.Text);
                             esp_aut.etapaCultivoList.Add(etapaCultivo);
                         }
                     }

                     esp_aut.tipoAlimento = new ParametroGenerico();
                     esp_aut.tipoAlimento.id = Convert.ToInt32(TipoAlimento.SelectedItem.Value);

                     esp_aut.tipoCultivo = new ParametroGenerico();
                     esp_aut.tipoCultivo.id = Convert.ToInt32(TipoCultivo.SelectedItem.Value);

                     esp_aut.nombreOtroTipoAlimento = NombreOtroTipoAlimento.Text;

                     objeto = esp_aut;

                    break;


                //case "GrupoAutorizada":

                //    GrupoPT gru_aut = new GrupoPT();

                //    //gru_aut.grupoCheck = true;
                //    gru_aut.grupo = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas2.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas2.SelectedItem.Text));
                //    gru_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas2.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas2.SelectedItem.Text));
                //    objeto = gru_aut;

                //    break;




            }


            return objeto;


        }



        protected void EliminarGrillaPT(int index, string seccion)
        {
            switch (seccion)
            {

                case "EspecieAutorizada":
                    List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    foreach (EspecieAutorizadaPT espAut in List_EspecieAut)
                    {
                        if (espAut.index.Equals(index))
                        {
                            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                            if (List_ProgrProd != null && List_ProgrProd.Count > 0)
                            {
                                espAut.list_ProgrProd = List_ProgrProd;
                            }

                            List<String> listaErroresEspAu = pTValidacion.validaEspecieAutorizada_Eliminar(espAut);

                            if (listaErroresEspAu.Count <= 0)
                            {
                                if (espAut.accion == accion.INGRESAR)
                                {
                                    //List_EspecieAut.Remove(espAut);
                                    espAut.accion = accion.IGNORAR;
                                    GridEspecieAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                                }
                                if (espAut.accion == accion.LISTADO)
                                {
                                    espAut.accion = accion.ELIMINAR;
                                    GridEspecieAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                                }

                                Carga_Combobox("EspecieProgramaProduccion");
                                //Carga_Combobox("GrupoProgramaProduccion2");
                                Carga_Combobox("GrupoProgramaProduccion");

                                UpdatePanel_EspecieProgramaProduccion.Update();
                                UpdatePanel_GrupoProgramaProduccion.Update();

                            }
                            else
                            {

                                foreach (String error in listaErroresEspAu)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                }

                                UpdatePanel_MSG_EspecieAu.Update();

                                break;
                            }
                        }
                    }

                    ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;
                    CargarGrillaPT("EspecieAutorizada", null);

                    break;

                    

                //case "GrupoAutorizada":

                //    List<GrupoPT> List_GrupoAut = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];
                //    foreach (GrupoPT gruAut in List_GrupoAut)
                //    {
                //        if (gruAut.index.Equals(index))
                //        {
                //            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                //            if (List_ProgrProd != null && List_ProgrProd.Count > 0)
                //            {
                //                gruAut.list_ProgrProd = List_ProgrProd;
                //            }

                //            List<String> listaErroresGruAu = pTValidacion.validaGrupoAutorizada_Eliminar(gruAut);

                //            if (listaErroresGruAu.Count <= 0)
                //            {
                //                if (gruAut.accion == accion.INGRESAR)
                //                {
                //                    //List_EspecieAut.Remove(espAut);
                //                    gruAut.accion = accion.IGNORAR;
                //                    GridGrupoAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                //                }
                //                if (gruAut.accion == accion.LISTADO)
                //                {
                //                    gruAut.accion = accion.ELIMINAR;
                //                    GridGrupoAutorizadaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                //                }


                //                Carga_Combobox("EspecieProgramaProduccion");
                //                Carga_Combobox("GrupoProgramaProduccion2");

                //                UpdatePanel_EspecieProgramaProduccion.Update();
                //                UpdatePanel_GrupoProgramaProduccion.Update();
                //            }
                //            else
                //            {

                //                foreach (String error in listaErroresGruAu)
                //                {
                //                    Page.Validators.Add(new ValidationError("grupo1", error));
                //                }

                //                UpdatePanel_MSG_GrupoAu.Update();

                //                break;
                    //        }
                    //    }
                    //}

                    //ViewState["GrupoAut_ProyTecnico"] = (List<GrupoPT>)List_GrupoAut;
                    //CargarGrillaPT("GrupoAutorizada", null);

                    //break;

                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    foreach (EstructuraTecnicaPT estructTec in List_EstructuraTecnica)
                    {
                        if (estructTec.index.Equals(index))
                        {
                            if (estructTec.accion == accion.INGRESAR)
                            {
                                estructTec.accion = accion.IGNORAR;
                                GridEstructuraTecnicaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            if (estructTec.accion == accion.LISTADO || estructTec.accion == accion.MODIFICAR)
                            {
                                estructTec.accion = accion.ELIMINAR;
                                GridEstructuraTecnicaProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            break;
                        }
                    }

                    ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                    CargarGrillaPT("EstructuraTecnica", null);
                    break;

                
                //case "EjemplarPT":
                //    List<EjemplarPT> List_EjemplarAut = (List<EjemplarPT>)ViewState["Ejemplar_ProyTecnico"];

                //    foreach (EjemplarPT ejemp in List_EjemplarAut)
                //    {
                //        if (ejemp.index.Equals(index))
                //        {
                //            break;
                //        }
                //    }

                //    ViewState["Ejemplar_ProyTecnico"] = (List<EjemplarPT>)List_EjemplarAut;
                //    CargarGrillaPT("EjemplarPT", null);
                //    break;

                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProduc = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    foreach (ProgrProduccionPT progrp in List_ProgrProduc)
                    {
                        if (progrp.index.Equals(index))
                        {
                            if (progrp.accion == accion.INGRESAR)
                            {
                                progrp.accion = accion.IGNORAR;
                                GridProgrProdProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            if (progrp.accion == accion.LISTADO || progrp.accion == accion.MODIFICAR)
                            {
                                progrp.accion = accion.ELIMINAR;
                                GridProgrProdProyTecnico.Rows[index].Attributes["style"] = "display:none";
                            }
                            break;
                        }

                    }

                    ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProduc;
                    CargarGrillaPT("ProgramaProd", null);

                    break;
               
            }

        }


        //protected void GridEjemplarProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "Eliminar":
        //            int index = Convert.ToInt32(e.CommandArgument);
        //            GridEspecieAutorizadaProyTecnico.EditIndex = -1;
        //            EliminarGrillaPT(index, "EjemplarPT");
        //            CargarGrillaPT("EjemplarPT", null);
        //            break;
        //    };
        //}


        //protected void GridEjemplarProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        // Borrar
        //        ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
        //        if (boton_eliminar != null)
        //        {
        //            boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
        //            boton_eliminar.Visible = true;
        //        };

        //        /*  //Lista anidada

        //           GridView gridViewAnioEjemplares = (GridView)e.Row.FindControl("gridViewAnioEjemplares");
        //           gridViewAnioEjemplares.DataSource = (List<ValorParametroAnioPT>)e.Row.
        //           gridViewAnioEjemplares.DataBind();*/



        //    };
        //}



        //protected void EtapaPorEspeciePP_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
        //    {
        //        GrupoProgramaProduccion.SelectedValue = "-1";
        //        UpdatePanel_GrupoProgramaProduccion.Update();
        //    }
        //    Carga_Combobox("EtapaCultivoProgramaProduccion");

        //}


        //protected void EtapaPorGrupoPP_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
        //    {
        //        EspecieProgramaProduccion.SelectedValue = "-1";
        //        UpdatePanel_EspecieProgramaProduccion.Update();

        //    }
        //    Carga_Combobox("EtapaCultivoProgramaProduccion");

        //}


        protected void DespliegaPesoProm_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            PesoPromSinRango.Text = "";
            PesoRango1.Text = "";
            PesoRango2.Text = "";
            UpdatePanelPesoProm.Visible = false;
            UpdatePanelPesoPromR1.Visible = false;
            UpdatePanelPesoPromR2.Visible = false;

            if ((Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) > 0) && (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_RANGO))
            {
                UpdatePanelPesoProm.Visible = false;
                UpdatePanelPesoPromR1.Visible = true;
                UpdatePanelPesoPromR2.Visible = true;

            }
            else if ((Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) > 0) && (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.NINGUNO_RANGO_PESO_EJEMPLAR)) 
            {
                UpdatePanelPesoProm.Visible = false;
                UpdatePanelPesoPromR1.Visible = false;
                UpdatePanelPesoPromR2.Visible = false;
            }
            else if ((Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) > 0) && (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_SIN_RANGO))
            {
                UpdatePanelPesoProm.Visible = true;
                UpdatePanelPesoPromR1.Visible = false;
                UpdatePanelPesoPromR2.Visible = false;
            }
            else
            {
                UpdatePanelPesoProm.Visible = false;
                UpdatePanelPesoPromR1.Visible = false;
                UpdatePanelPesoPromR2.Visible = false;
            }

        }


        //protected void AlimentoPorTC_OnSelectedIndexChanged(object sender, EventArgs e)
        //{


        //    AlimentoAlgaFresca.Checked = false;
        //    AlimentoPellet.Checked = false;
        //    AlimentoOtro.Checked = false;
        //    NombreAlimentoOtro.ReadOnly = true;

        //    if ((Convert.ToInt32(TipoCultivo.SelectedValue) > 0) && (Convert.ToInt32(TipoCultivo.SelectedValue) == rbTipo.TIPO_CULT_EXTENSIVO || Convert.ToInt32(TipoCultivo.SelectedValue) == -1))
        //    {
        //        PanelTipoAlimento.Visible = false;
        //        AlimentoAlgaFresca.Checked = false;
        //        AlimentoPellet.Checked = false;
        //        AlimentoOtro.Checked = false;
        //    }
        //    else if (Convert.ToInt32(TipoCultivo.SelectedValue) == -1)
        //    {
        //        PanelTipoAlimento.Visible = false;
        //        AlimentoAlgaFresca.Checked = false;
        //        AlimentoPellet.Checked = false;
        //        AlimentoOtro.Checked = false;
        //    }
        //    else
        //    {
        //        PanelTipoAlimento.Visible = true;
        //    }
        //    TipoAlimentoChecked(null, null);

        //}


        protected void Guardar_ProyTecnico_Click(object sender, EventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            ProyectoTecnico proyTecnicoSolicitud = new ProyectoTecnico();

            proyTecnicoSolicitud.idSolicitud = Convert.ToInt32(IdSolicitud.Value);

            //proyTecnicoSolicitud.tipoAlimento = new List<TipoAlimentoProyecto>();
            //proyTecnicoSolicitud.densidadSiembra = -1;
            //proyTecnicoSolicitud.mangasPlasticasVal = -1;

            //TipoAlimentoProyecto tipoAlimentoProy = new TipoAlimentoProyecto();

            if (PanelCultivoAlgas.Visible)
            {
                //tipoAlimentoProy = new TipoAlimentoProyecto();
                //tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_ALGA);
                //proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);

                ////SET METODO CULTIVO ALGAS
                //proyTecnicoSolicitud.metodoCultivoAlgas = new List<TipoAlimentoProyecto>();
                //TipoAlimentoProyecto metCultivoAlgas = new TipoAlimentoProyecto();
                //if (Algas_DirSustrato.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_DIR_SUSTRATO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_IndirSustrato.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_INDIR_SUSTRATO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Suspendido.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_SUSPENDIDO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Estanque.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_ESTANQUE);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Otro.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_OTRO);
                //    metCultivoAlgas.detalle = AlgaOtroDef.Text;
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 1;
                }
                if (RadioButtonListUtilizaMangasPlasticas2.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 0;
                }
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == false && RadioButtonListUtilizaMangasPlasticas2.Checked == false)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = -1;
                }

                if (Convert.ToInt32(TipoCultivoAlgas.SelectedValue) > 0)
                {
                    proyTecnicoSolicitud.tipoCultivoAlgas = new ParametroGenerico(Convert.ToInt32(TipoCultivoAlgas.SelectedValue));
                }

                //CompareValidator_DensidadSiembra.Validate();

                //if (!DensidadSiembra.Text.Equals(""))
                //{
                //    proyTecnicoSolicitud.densidadSiembra = Convert.ToSingle(DensidadSiembra.Text);
                //}


                //SET TIPO FONDO
                //proyTecnicoSolicitud.tipoFondo = new List<TipoAlimentoProyecto>();
                //TipoAlimentoProyecto tFondo = new TipoAlimentoProyecto();
                //if (TipoFondoDuro.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_DURO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoSemi.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_SEMIDURO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoBlando.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_BLANDO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoOtro.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_OTRO);
                //    tFondo.detalle = FondoOtroDef.Text;
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}

            }

            if (PanelEstructuraColector.Visible) {

                EstructuraTecnicaPT estructuraTecnica = new EstructuraTecnicaPT();
                estructuraTecnica.idEstructPT = 0;
                estructuraTecnica.accion = accion.INGRESAR;

                if (NumeroColectores.Text != null && !NumeroColectores.Text.Equals(""))
                {
                    estructuraTecnica.numColectores = Convert.ToInt32(NumeroColectores.Text);
                }

                if (NumeroLineasColectores.Text != null && !NumeroLineasColectores.Text.Equals(""))
                {
                    estructuraTecnica.numLineas = Convert.ToInt32(NumeroLineasColectores.Text);
                }

                proyTecnicoSolicitud.estructTecnicaColector = estructuraTecnica;

            }

            if (PanelFechaSolicitadaTitular.Visible) {

                if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
                {
                    proyTecnicoSolicitud.fechaInicio = Convert.ToDateTime(FechaRecepcion.Text);
                }

                if (FechaIngresoTramite.Text != null && !FechaIngresoTramite.Text.Equals(""))
                {
                    proyTecnicoSolicitud.fechaTermino = Convert.ToDateTime(FechaIngresoTramite.Text);
                }

            }

            //if (AlimentoOtro.Checked == true)
            //{
            //    tipoAlimentoProy = new TipoAlimentoProyecto();
            //    tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_OTRO);
            //    tipoAlimentoProy.detalle = NombreAlimentoOtro.Text;
            //    proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            //}
            //if (AlimentoPellet.Checked == true)
            //{
            //    tipoAlimentoProy = new TipoAlimentoProyecto();
            //    tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_PELLET);
            //    proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            //}


            proyTecnicoSolicitud.observaciones = observaciones.Text;
            
            List<EspecieAutorizadaPT> List_EspecieAut           = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
            //List<GrupoPT> List_GrupoAut                         = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];

            List<EstructuraTecnicaPT> List_EstructuraTecnica    = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
            List<EstructuraTecnicaPT> List_EstructuraTecnicaDirectoSustrato = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnicaDirectoSust_ProyTecnico"];

            List<ProgrProduccionPT> List_ProgrProd              = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
            //List<ArchivosAdjPT> List_ArchivoBinario = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];

            proyTecnicoSolicitud.especieAutProyTecnico      = List_EspecieAut;
            //proyTecnicoSolicitud.grupoAutProyTecnico        = List_GrupoAut;

            proyTecnicoSolicitud.estructTecnicaProyTecnico  = List_EstructuraTecnica;
            proyTecnicoSolicitud.estructTecnicaProyTecnicoDirectoSustrato = List_EstructuraTecnicaDirectoSustrato;
            
            proyTecnicoSolicitud.progrProduccionProyTecnico = List_ProgrProd;
            //proyTecnicoSolicitud.archivoBinarioList = List_ArchivoBinario;

            bool resp = proyService.guardarProyectoTecnico(proyTecnicoSolicitud, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
            if (resp)
            {
                msgGrillaGral_1.Text = "Proyecto Técnico guardado exitosamente.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
                GuardarProyTecnico.Visible = false;
                ModificarProyTecnico.Visible = true;

                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();


                //RECARGAR EL FORMULARIO (para que se seteen  los id)
                Initialize_Form();
            }
            else
            {
                msgGrillaGral_1.Text = "No se ha guardado el Proyecto Técnico.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
            }

            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensaje.Update();

        }


        protected void Modificar_ProyTecnico_Click(object sender, EventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            ProyectoTecnico proyTecnicoSolicitud = new ProyectoTecnico();

            proyTecnicoSolicitud.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            proyTecnicoSolicitud.IdProyectoTecnico = proyService.ObtieneClaveProyectoTecnico(proyTecnicoSolicitud.idSolicitud);
           /* if (!IdProyectoTecnico.Value.Equals(""))
            {
                proyTecnicoSolicitud.IdProyectoTecnico = Convert.ToInt32(IdProyectoTecnico.Value);
            }*/
            //proyTecnicoSolicitud.tipoAlimento = new List<TipoAlimentoProyecto>();
            //proyTecnicoSolicitud.densidadSiembra = -1;
            //proyTecnicoSolicitud.mangasPlasticasVal = -1;

            //TipoAlimentoProyecto tipoAlimentoProy = new TipoAlimentoProyecto();


            if (PanelCultivoAlgas.Visible)
            {
                //tipoAlimentoProy = new TipoAlimentoProyecto();
                //tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_ALGA);
                //proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);

                ////SET METODO CULTIVO ALGAS
                //proyTecnicoSolicitud.metodoCultivoAlgas = new List<TipoAlimentoProyecto>();
                //TipoAlimentoProyecto metCultivoAlgas = new TipoAlimentoProyecto();
                //if (Algas_DirSustrato.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_DIR_SUSTRATO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_IndirSustrato.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_INDIR_SUSTRATO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Suspendido.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_SUSPENDIDO);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Estanque.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_ESTANQUE);
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                //if (Algas_Otro.Checked == true)
                //{
                //    metCultivoAlgas = new TipoAlimentoProyecto();
                //    metCultivoAlgas.tipoAlimento = new ParametroGenerico(rbTipo.MET_ALGAS_OTRO);
                //    metCultivoAlgas.detalle = AlgaOtroDef.Text;
                //    proyTecnicoSolicitud.metodoCultivoAlgas.Add(metCultivoAlgas);
                //}
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 1;
                }
                if (RadioButtonListUtilizaMangasPlasticas2.Checked == true)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = 0;
                }
                if (RadioButtonListUtilizaMangasPlasticas1.Checked == false && RadioButtonListUtilizaMangasPlasticas2.Checked == false)
                {
                    proyTecnicoSolicitud.mangasPlasticasVal = -1;
                }

                if (Convert.ToInt32(TipoCultivoAlgas.SelectedValue) > 0)
                {
                    proyTecnicoSolicitud.tipoCultivoAlgas = new ParametroGenerico(Convert.ToInt32(TipoCultivoAlgas.SelectedValue));
                }

                //CompareValidator_DensidadSiembra.Validate();

                //if (!DensidadSiembra.Text.Equals(""))
                //{
                //    proyTecnicoSolicitud.densidadSiembra = Convert.ToSingle(DensidadSiembra.Text);
                //}

                ////SET TIPO FONDO
                //proyTecnicoSolicitud.tipoFondo = new List<TipoAlimentoProyecto>();
                //TipoAlimentoProyecto tFondo = new TipoAlimentoProyecto();
                //if (TipoFondoDuro.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_DURO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoSemi.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_SEMIDURO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoBlando.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_BLANDO);
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}
                //if (TipoFondoOtro.Checked == true)
                //{
                //    tFondo = new TipoAlimentoProyecto();
                //    tFondo.tipoAlimento = new ParametroGenerico(rbTipo.T_FONDO_OTRO);
                //    tFondo.detalle = FondoOtroDef.Text;
                //    proyTecnicoSolicitud.tipoFondo.Add(tFondo);
                //}

            }

            if (PanelEstructuraColector.Visible)
            {

                EstructuraTecnicaPT estructuraTecnica = new EstructuraTecnicaPT();
                estructuraTecnica.idProyTec = proyTecnicoSolicitud.IdProyectoTecnico;
                estructuraTecnica.idEstructPT = Convert.ToInt32(idEstructuraProyectoTecnico.Value);

                if (NumeroColectores.Text != null && !NumeroColectores.Text.Equals(""))
                {
                    estructuraTecnica.numColectores = Convert.ToInt32(NumeroColectores.Text);
                }
                else {
                    estructuraTecnica.numColectores = 0;
                }

                if (NumeroLineasColectores.Text != null && !NumeroLineasColectores.Text.Equals(""))
                {
                    estructuraTecnica.numLineas = Convert.ToInt32(NumeroLineasColectores.Text);
                }
                else {
                    estructuraTecnica.numLineas = 0;
                
                }

                proyTecnicoSolicitud.estructTecnicaColector = estructuraTecnica;
            }


            if (PanelFechaSolicitadaTitular.Visible)
            {

                if (FechaRecepcion.Text != null && !FechaRecepcion.Text.Equals(""))
                {
                    proyTecnicoSolicitud.fechaInicio = Convert.ToDateTime(FechaRecepcion.Text);
                }
                else {
                    proyTecnicoSolicitud.fechaTermino = new DateTime();
                }

                if (FechaIngresoTramite.Text != null && !FechaIngresoTramite.Text.Equals(""))
                {
                    proyTecnicoSolicitud.fechaTermino = Convert.ToDateTime(FechaIngresoTramite.Text);
                }
                else {
                    proyTecnicoSolicitud.fechaTermino = new DateTime();
                }

            }

            //if (AlimentoOtro.Checked == true)
            //{
            //    tipoAlimentoProy = new TipoAlimentoProyecto();
            //    tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_OTRO);
            //    tipoAlimentoProy.detalle = NombreAlimentoOtro.Text;
            //    proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            //}
            //if (AlimentoPellet.Checked == true)
            //{
            //    tipoAlimentoProy = new TipoAlimentoProyecto();
            //    tipoAlimentoProy.tipoAlimento = new ParametroGenerico(rbTipo.TIPO_ALIMENTO_PT_PELLET);
            //    proyTecnicoSolicitud.tipoAlimento.Add(tipoAlimentoProy);
            //}


            proyTecnicoSolicitud.observaciones = observaciones.Text;


            //if (Convert.ToInt32(TipoCultivo.SelectedValue) > 0)
            //{
            //    proyTecnicoSolicitud.tipoCultivo = new ParametroGenerico(Convert.ToInt32(TipoCultivo.SelectedValue));
            //}

            List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
            //List<GrupoPT> List_GrupoAut                         = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];

            List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
            List<EstructuraTecnicaPT> List_EstructuraTecnicaDirectoSustrato = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnicaDirectoSust_ProyTecnico"];

            List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
            List<ArchivosAdjPT> List_ArchivoBinario = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];

            proyTecnicoSolicitud.especieAutProyTecnico = List_EspecieAut;
            //proyTecnicoSolicitud.grupoAutProyTecnico        = List_GrupoAut;
            proyTecnicoSolicitud.estructTecnicaProyTecnico = List_EstructuraTecnica;
            proyTecnicoSolicitud.estructTecnicaProyTecnicoDirectoSustrato = List_EstructuraTecnicaDirectoSustrato;

            proyTecnicoSolicitud.progrProduccionProyTecnico = List_ProgrProd;
            proyTecnicoSolicitud.archivoBinarioList = List_ArchivoBinario;


            usuario_logeado = (Datos.Entidades.Usuario.Serializable)Session["Usuario"];

            bool resp = proyService.modificarProyectoTecnico(proyTecnicoSolicitud, usuario_logeado.id_usuario, proyTecnicoSolicitud.idSolicitud);
            if (resp)
            {
                msgGrillaGral_1.Text = "Proyecto Técnico modificado exitosamente.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;


                //RECARGANDO LA INFORMACION DE LA SOLICITUD, POR SI CAMBIO EL ESTADO
                UpdatePanel UpdatePanelInformacionSolictud = informacionSolicitud.Instance.UpdatePanelInfo;
                informacionSolicitud.Instance.RecargarInformacion();
                UpdatePanelInformacionSolictud.Update();

                //RECARGAR EL FORMULARIO (para que se seteen  los id)
                Initialize_Form();
            }
            else
            {
                msgGrillaGral_1.Text = "No se ha modificado el Proyecto Técnico.";
                msgGrillaGral_1.Focus();
                Content_msgGrillaGral_1.Visible = true;
            }

            Ico_msgGrillaGral_1.ImageUrl = "~/App_Themes/admin_style/images/info.gif";
            UpdatePanelMensaje.Update();

        }

        
        protected void LimpiarEstructuraTecnica_Click(object sender, ImageClickEventArgs e)
        {
            this.limpiarProyTecnico("EstructuraTecnica");
        }
        
        
        protected void LimpiarProgramaProd_Click(object sender, ImageClickEventArgs e)
        {
            this.limpiarProyTecnico("ProgramaProd");
        }


        private void limpiarProyTecnico(string seccion)
        {
            switch (seccion)
            {

                case "EspecieAutorizada":
                    Carga_Combobox("EspecieAutorizada");
                    EspecieAutorizada.SelectedValue = "-1";
                    Carga_Combobox("GrupoEspecieAutorizadas");
                    GrupoEspecieAutorizadas.SelectedValue = "-1";
                    Carga_Combobox("EtapaDeCultivoAutorizadas");
                    EtapaDeCultivoAutorizadas.SelectedValue = "-1";
                    Carga_Combobox("TipoCultivo");
                    TipoCultivo.SelectedValue = "-1";
                    Carga_Combobox("TipoAlimento");
                    TipoAlimento.SelectedValue = "-1";
                    NombreOtroTipoAlimento.Text = "";
                    PanelNombreOtroTipoAlimento.Visible = false;

                    break;

                case "EstructuraTecnica":
                    Carga_Combobox("TipoEstructura");
                    TipoEstructura.SelectedValue = "-1";
                    Carga_Combobox("FormaEstructura");
                    FormaEstructura.SelectedValue = "-1";
                    Carga_Combobox("UnidadDeMedida");
                    UnidadDeMedida.SelectedValue = "-1";
                    TextBoxLargoM.Text = "";
                    TextBoxAnchoM.Text = "";
                    TextBoxAltoM.Text = "";
                    TextBoxDiametroM.Text = "";
                    Carga_Combobox("VolumenUnidadMedida");
                    VolumenUnidadMedida.SelectedValue = "-1";
                    Carga_Combobox("Anio");
                    Anio.SelectedValue = "-1";
                    DensidadSiembra.Text = "";
                    TotalAcumuladoDimension.Text = "";
                    TotalAcumuladoNumero.Text = "";
                    TextBoxVolumenValorMedida.Text = "";
                    TipoAnioEstructuraTecnica_Selected(null, null);
                    Panel_Agregar_EstructuraTecnica.Visible = true;
                    Panel_Modificar_EstructuraTecnica.Visible = false;
                    UpdatePanel_EstructuraTecnica.Update();
                    break;

                case "ProgramaProd":
                    Carga_Combobox("EspecieProgramaProduccion");
                    EspecieProgramaProduccion.SelectedValue = "-1";
                    //EtapaPorEspeciePP_OnSelectedIndexChanged(null, null);

                    //Carga_Combobox("GrupoProgramaProduccion2");
                    //GrupoProgramaProduccion.SelectedValue = "-1";
                    //EtapaPorGrupoPP_OnSelectedIndexChanged(null, null);

                    //Carga_Combobox("EtapaCultivoProgramaProduccion");
                    //EtapaCultivoProgramaProduccion.SelectedValue = "-1";

                    Carga_Combobox("UnidadProgramaProduccion");
                    UnidadProgramaProduccion.SelectedValue = "-1";

                    Carga_Combobox("PesoPromedioEjemplares");
                    PesoPromedioEjemplares.SelectedValue = "-1";
                    DespliegaPesoProm_OnSelectedIndexChanged(null, null);

                    //DensidadProgProd.Text = "";
                    ProdUltimoAnio.Text = "";
                    AnioProgramaProducc.SelectedValue = "-1";
                    AnioProd1.Text = "";
                    AnioProd2.Text = "";
                    AnioProd3.Text = "";
                    AnioProd4.Text = "";
                    AnioProd5.Text = "";
                    
                    Panel_AgregarProgrProd.Visible = true;
                    Panel_ModificarProgrProd.Visible = false;
                    UpdatePanel_ProgrProduccionPT.Update();
                    break;

                case "ArchivoBinario":
                    Carga_Combobox("TipoArchivo");
                    TipoArchivo.SelectedValue = "-1";
                    NombreArchivo.Text = "";
                    NumeroCI.Text = "";
                    FechaTextRecepcion.Text = "";
                    break;
            }

        }


        protected bool validaDec(string cadena)
        {

            Regex regex = new Regex(@"^[0-9]+(\,[0-9]{1,4})?$");

            if (regex.IsMatch(cadena))
            {
                return true;
            }

            return false;
        }


        private void validaDataMedidasEstructura()
        {

            CompareValidator_TextBoxLargoM.Validate();
            CompareValidator_TextBoxAnchoM.Validate();
            CompareValidator_TextBoxAltoM.Validate();
            CompareValidator_TextBoxDiametroM.Validate();
        }


        /**
         * AGREGAR UNA NUEVA FILA A SECCION QUE SE INDICA
         */ 
        private void AgregarGrillaPT(string seccion)
        {
            int index = 0;
            switch (seccion)
            {
                case "EspecieAutorizada":

                    List<EspecieAutorizadaPT> List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];

                    index = 0;
                    if (List_EspecieAut == null)
                    {
                        List_EspecieAut = new List<EspecieAutorizadaPT>();
                    }
                    else
                    {
                        index = List_EspecieAut.Count;
                    }

                    EspecieAutorizadaPT esp_aut = (EspecieAutorizadaPT)this.CargaObjeto("EspecieAutorizada", "");
                    List<String> listaErroresEspAu = pTValidacion.validaEspecieAutorizada(esp_aut);
                    bool validaOk = false;

                    if (listaErroresEspAu.Count <= 0)
                    {
                        //Si selecciono especie
                        if (EspeciesRad.Checked == true)
                        {
                            esp_aut.idEspeciePT = 0;
                            esp_aut.accion = accion.INGRESAR;
                            esp_aut.index = Convert.ToInt32(index);
                            esp_aut.especie = new ParametroGenerico(Convert.ToInt32(EspecieAutorizada.SelectedValue), Convert.ToString(EspecieAutorizada.SelectedItem.Text));
                            
                            //esp_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas.SelectedItem.Text));
                            //esp_aut.grupoEspecie = new ParametroGenerico();
                            //esp_aut.grupoEspecie = parametroDa.ObtenerEspecies(esp_aut.especie.id, null, 0);

                            Especies especies = mantenedorDA.obtenerEspeciesCultivo_Mantenedor(esp_aut.especie.id);

                            if (especies != null && especies.grupoEspecie != null && especies.grupoEspecie.id_grupoEspecie > 0)
                            {
                                esp_aut.grupoEspecie = new ParametroGenerico(especies.grupoEspecie.id_grupoEspecie, especies.grupoEspecie.grupoEspecie);
                            }

                            esp_aut.etapaCultivoList = new List<EtapaCultivo>();
                            foreach (ListItem item in EtapaDeCultivoAutorizadas.Items)
                            {
                                if (item.Selected && Convert.ToInt32(item.Value) > 0)
                                {
                                    EtapaCultivo etapaCultivo = new EtapaCultivo();
                                    etapaCultivo.id_etapaDesarrollo = Convert.ToInt32(item.Value);
                                    etapaCultivo.nombreEtapaDesarrollo = Convert.ToString(item.Text);
                                    esp_aut.etapaCultivoList.Add(etapaCultivo);
                                }
                            }

                            esp_aut.tipoCultivo = new ParametroGenerico(Convert.ToInt32(TipoCultivo.SelectedValue), Convert.ToString(TipoCultivo.SelectedItem.Text));
                            esp_aut.tipoAlimento = new ParametroGenerico(Convert.ToInt32(TipoAlimento.SelectedValue), Convert.ToString(TipoAlimento.SelectedItem.Text));
                            esp_aut.detalle = NombreOtroTipoAlimento.Text;

                            List<String> listaErroresEspAuLista = pTValidacion.validaEspecieAutorizadaLista(esp_aut, List_EspecieAut);
                            if (listaErroresEspAuLista.Count <= 0)
                            {
                                List_EspecieAut.Add(esp_aut);
                                validaOk = true;
                            }
                            else
                            {
                                foreach (String error in listaErroresEspAuLista)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                    validaOk = false;
                                }
                                UpdatePanel_MSG_EspecieAu.Update();
                            }


                        }

                        //Si selecciono grupo especie
                        if (GrupoEspeciesRad.Checked == true)
                        {
                            esp_aut.idEspeciePT = 0;
                            esp_aut.accion = accion.INGRESAR;
                            esp_aut.index = Convert.ToInt32(index);
                            esp_aut.grupoEspecie = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas.SelectedItem.Text));

                            esp_aut.etapaCultivoList = new List<EtapaCultivo>();
                            foreach (ListItem item in EtapaDeCultivoAutorizadas.Items)
                            {
                                if (item.Selected && Convert.ToInt32(item.Value) > 0)
                                {
                                    EtapaCultivo etapaCultivo = new EtapaCultivo();
                                    etapaCultivo.id_etapaDesarrollo = Convert.ToInt32(item.Value);
                                    etapaCultivo.nombreEtapaDesarrollo = Convert.ToString(item.Text);
                                    esp_aut.etapaCultivoList.Add(etapaCultivo);
                                }
                            }

                            esp_aut.tipoCultivo = new ParametroGenerico(Convert.ToInt32(TipoCultivo.SelectedValue), Convert.ToString(TipoCultivo.SelectedItem.Text));
                            esp_aut.tipoAlimento = new ParametroGenerico(Convert.ToInt32(TipoAlimento.SelectedValue), Convert.ToString(TipoAlimento.SelectedItem.Text));
                            esp_aut.detalle = NombreOtroTipoAlimento.Text;

                            List<String> listaErroresEspAuLista = pTValidacion.validaEspecieAutorizadaLista(esp_aut, List_EspecieAut);
                            if (listaErroresEspAuLista.Count <= 0)
                            {
                                List_EspecieAut.Add(esp_aut);
                                validaOk = true;
                            }
                            else
                            {
                                foreach (String error in listaErroresEspAuLista)
                                {
                                    Page.Validators.Add(new ValidationError("grupo1", error));
                                    validaOk = false;
                                }
                                UpdatePanel_MSG_EspecieAu.Update();
                            }

                        }

                        if (validaOk)
                        {
                            GridEspecieAutorizadaProyTecnico.DataSource = List_EspecieAut;
                            GridEspecieAutorizadaProyTecnico.DataBind();

                            ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;
                            //Carga_Combobox("EspeciesEjemplar");
                            
                            Carga_Combobox("EspecieProgramaProduccion");
                            //Carga_Combobox("GrupoProgramaProduccion2");
                            Carga_Combobox("GrupoProgramaProduccion");
                            
                            limpiarProyTecnico("EspecieAutorizada");
                            UpdatePanel_ProgrProduccionPT.Update();

                        }


                    }
                    else
                    {
                        foreach (String error in listaErroresEspAu)
                        {
                            Page.Validators.Add(new ValidationError("grupo1", error));
                        }

                        UpdatePanel_MSG_EspecieAu.Update();

                    }

                     

                    break;

                //case "GrupoAutorizada":
                //    List<GrupoPT> List_GrupoAut = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];

                //    index = 0;
                //    if (List_GrupoAut == null)
                //    {
                //        List_GrupoAut = new List<GrupoPT>();
                //    }
                //    else
                //    {
                //        index = List_GrupoAut.Count;
                //    }

                //    GrupoPT gru_aut = (GrupoPT)this.CargaObjeto("GrupoAutorizada", "");
                //    List<String> listaErroresGruAu = pTValidacion.validaGrupoAutorizada(gru_aut);
                //    bool validaGrupoOk = false;

                //    if (listaErroresGruAu.Count <= 0)
                //    {

                //        gru_aut = new GrupoPT();
                //        gru_aut.idGrupoPT = 0;
                //        gru_aut.accion = accion.INGRESAR;
                //        gru_aut.index = Convert.ToInt32(index);
                //        gru_aut.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaDeCultivoAutorizadas2.SelectedValue), Convert.ToString(EtapaDeCultivoAutorizadas2.SelectedItem.Text));
                //        gru_aut.grupo = new ParametroGenerico(Convert.ToInt32(GrupoEspecieAutorizadas2.SelectedValue), Convert.ToString(GrupoEspecieAutorizadas2.SelectedItem.Text));

                //        List<String> listaErroresEspAuLista = pTValidacion.validaGrupoAutorizadaLista(gru_aut, List_GrupoAut);
                //        if (listaErroresEspAuLista.Count <= 0)
                //        {
                //            List_GrupoAut.Add(gru_aut);
                //            validaGrupoOk = true;
                //            index++;
                //        }
                //        else
                //        {
                //            foreach (String error in listaErroresEspAuLista)
                //            {
                //                Page.Validators.Add(new ValidationError("grupo1", error));
                //            }
                //            UpdatePanel_MSG_EspecieAu.Update();
                //            validaGrupoOk = false;
                //            break;
                //        }
                        

                //        if (validaGrupoOk)
                //        {
                //            GridGrupoAutorizadaProyTecnico.DataSource = List_GrupoAut;
                //            GridGrupoAutorizadaProyTecnico.DataBind();

                //            ViewState["GrupoAut_ProyTecnico"] = (List<GrupoPT>)List_GrupoAut;
                //            Carga_Combobox("GrupoProgramaProduccion2");
                //            limpiarProyTecnico("GrupoAutorizada");
                //            UpdatePanel_ProgrProduccionPT.Update();
                            
                         
                //        }

                //    }
                //    else
                //    {
                //        foreach (String error in listaErroresGruAu)
                //        {
                //            Page.Validators.Add(new ValidationError("grupo1", error));
                //        }

                //        UpdatePanel_MSG_GrupoAu.Update();

                //    }

                //    break;



                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];

                    index = 0;
                    if (List_EstructuraTecnica == null)
                    {
                        List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    }
                    else
                    {
                        index = List_EstructuraTecnica.Count;
                    }

                    EstructuraTecnicaPT estructTec = new EstructuraTecnicaPT();

                    estructTec.accion = accion.INGRESAR;
                    estructTec.idEstructPT = 0;

                    if (Convert.ToInt32(TipoEstructura.SelectedValue) > 0)
                    {
                        estructTec.tipoEstructura = new ParametroGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), Convert.ToString(TipoEstructura.SelectedItem.Text));
                    }

                    if (Convert.ToInt32(FormaEstructura.SelectedValue) > 0)
                    {
                        estructTec.formaEstructura = new ParametroGenerico(Convert.ToInt32(FormaEstructura.SelectedValue), Convert.ToString(FormaEstructura.SelectedItem.Text));
                    }

                    if (Convert.ToInt32(UnidadDeMedida.SelectedValue) > 0)
                    {
                        estructTec.unidadMedida = new ParametroGenerico(Convert.ToInt32(UnidadDeMedida.SelectedValue), Convert.ToString(UnidadDeMedida.SelectedItem.Text));
                    }
                    
                    if (!TextBoxLargoM.Text.Equals(""))
                    {
                        estructTec.largo = Convert.ToSingle(TextBoxLargoM.Text);
                    }
                    if (!TextBoxAnchoM.Text.Equals(""))
                    {
                        estructTec.ancho = Convert.ToSingle(TextBoxAnchoM.Text);
                    }
                    if (!TextBoxAltoM.Text.Equals(""))
                    {
                        estructTec.alto = Convert.ToSingle(TextBoxAltoM.Text);
                    }
                    if (!TextBoxDiametroM.Text.Equals(""))
                    {
                        estructTec.diametro = Convert.ToSingle(TextBoxDiametroM.Text);
                    }

                    estructTec.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(VolumenUnidadMedida.SelectedValue), Convert.ToString(VolumenUnidadMedida.SelectedItem.Text));
                    if (!TextBoxVolumenValorMedida.Text.Equals(""))
                    {
                        estructTec.volumenValorMedida = Convert.ToSingle(TextBoxVolumenValorMedida.Text);
                    }

                    estructTec.tipoAnio = new ParametroGenerico(Convert.ToInt32(Anio.SelectedValue), Convert.ToString(Anio.SelectedItem.Text));
                    estructTec.anios = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAux = null;

                    if (TextBoxAnio1.Text != null && !TextBoxAnio1.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 1;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio1.Text);
                        estructTec.anios.Add(valorParamAux);
                    }


                    if (TextBoxAnio2.Text != null && !TextBoxAnio2.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 2;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio2.Text);
                        estructTec.anios.Add(valorParamAux);
                    }
                    if (TextBoxAnio3.Text != null && !TextBoxAnio3.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 3;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio3.Text);
                        estructTec.anios.Add(valorParamAux);

                    }
                    if (TextBoxAnio4.Text != null && !TextBoxAnio4.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 4;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio4.Text);
                        estructTec.anios.Add(valorParamAux);
                    }

                    if (TextBoxAnio5.Text != null && !TextBoxAnio5.Text.Equals(""))
                    {
                        valorParamAux = new ValorParametroAnioPT();
                        valorParamAux.idRegistro = 0;
                        valorParamAux.idclaveParametro = 0;
                        valorParamAux.anio = 5;
                        valorParamAux.valor = Convert.ToInt32(TextBoxAnio5.Text);
                        estructTec.anios.Add(valorParamAux);
                    }

                    /* Densidad de la siembra */
                    if (DensidadSiembra.Text != null && !DensidadSiembra.Text.Equals(""))
                    {
                        estructTec.densidadSiembra = Convert.ToSingle(DensidadSiembra.Text);
                    }

                    if (TotalAcumuladoNumero.Text != null && !TotalAcumuladoNumero.Text.Equals(""))
                    {
                        estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoNumero.Text);
                    }
                    if (TotalAcumuladoDimension.Text != null && !TotalAcumuladoDimension.Text.Equals(""))
                    {
                        estructTec.totalAcumDim = Convert.ToSingle(TotalAcumuladoDimension.Text);
                    }

                    List<String> listaErroresEstructTecnica = pTValidacion.validaEstructuraTecnica(estructTec);

                    if (listaErroresEstructTecnica.Count <= 0)
                    {
                       // estructTec.idEstructPT = Convert.ToInt32(IdEstructProyTecnico.Value);
                        estructTec.index = Convert.ToInt32(index);

                        List_EstructuraTecnica.Add(estructTec);

                        GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                        GridEstructuraTecnicaProyTecnico.DataBind();

                        ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                        limpiarProyTecnico("EstructuraTecnica");

                    }
                    else
                    {

                        foreach (String error in listaErroresEstructTecnica)
                        {
                            Page.Validators.Add(new ValidationError("grupo2", error));
                        }

                        UpdatePanel_MSG_EstructuraTecnica.Update();

                    }


                    break;

                

                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    index = 0;
                    if (List_ProgrProd == null)
                    {
                        List_ProgrProd = new List<ProgrProduccionPT>();
                    }
                    else
                    {
                        index = List_ProgrProd.Count;
                    }

                    ProgrProduccionPT progrProduccion = new ProgrProduccionPT();

                    progrProduccion.accion = accion.INGRESAR;
                    progrProduccion.idProgrProduccion = 0;
                    progrProduccion.index = Convert.ToInt32(index);
                    progrProduccion.accion = accion.INGRESAR;
                    progrProduccion.especie = new ParametroGenerico(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.especie.descripcion = Convert.ToString(EspecieProgramaProduccion.SelectedItem.Text);
                    }
                    progrProduccion.grupo = new ParametroGenerico(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.grupo.descripcion = Convert.ToString(GrupoProgramaProduccion.SelectedItem.Text);
                    }
                    //progrProduccion.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaCultivoProgramaProduccion.SelectedValue), Convert.ToString(EtapaCultivoProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(UnidadProgramaProduccion.SelectedValue), Convert.ToString(UnidadProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(PesoPromedioEjemplares.SelectedValue), Convert.ToString(PesoPromedioEjemplares.SelectedItem.Text));
                    if (PesoPromSinRango != null && !PesoPromSinRango.Text.Equals(""))
                    {
                        progrProduccion.pesoPromSR = Convert.ToSingle(PesoPromSinRango.Text);
                    }
                    if (PesoRango1 != null && !PesoRango1.Text.Equals(""))
                    {
                        progrProduccion.pesoPromR1 = Convert.ToSingle(PesoRango1.Text);
                    }
                    if (PesoRango2 != null && !PesoRango2.Text.Equals(""))
                    {
                        progrProduccion.pesoPromR2 = Convert.ToSingle(PesoRango2.Text);
                    }
                    /*
                    if (!DensidadProgProd.Text.Equals(""))
                    {
                        progrProduccion.densidad = Convert.ToSingle(DensidadProgProd.Text);
                    }
                     * */
                    if (!ProdUltimoAnio.Text.Equals(""))
                    {
                        progrProduccion.produccionUltimoAnio = Convert.ToSingle(ProdUltimoAnio.Text);
                    }

                    progrProduccion.tipoAnio = new ParametroGenerico(Convert.ToInt32(AnioProgramaProducc.SelectedValue), Convert.ToString(AnioProgramaProducc.SelectedItem.Text));
                    progrProduccion.aniosProgrProd = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAnioPP = null;
                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 1;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd1.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 2;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd2.Text);
                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    if (AnioProd3 != null && !AnioProd3.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 3;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd3.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }
                    if (AnioProd4 != null && !AnioProd4.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 4;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd4.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }

                    if (AnioProd5 != null && !AnioProd5.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 5;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd5.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }

                    List<String> listaErroresProgrProd = pTValidacion.validaProgramaProduccion(progrProduccion);

                    if (listaErroresProgrProd.Count <= 0)
                    {
                        List<String> listaErroresProgrProdLista = pTValidacion.validaProgrProduccionLista(progrProduccion, List_ProgrProd);
                        if (listaErroresProgrProdLista.Count <= 0)
                        {
                            List_ProgrProd.Add(progrProduccion);

                            GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                            GridProgrProdProyTecnico.DataBind();

                            ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                            this.limpiarProyTecnico("ProgramaProd");
                        }
                        else
                        {
                            foreach (String error in listaErroresProgrProdLista)
                            {
                                Page.Validators.Add(new ValidationError("grupo4", error));
                            }

                            UpdatePanel_MSG_ProgrProduccionPT.Update();

                        }


                    }
                    else
                    {
                        foreach (String error in listaErroresProgrProd)
                        {
                            Page.Validators.Add(new ValidationError("grupo4", error));
                        }
                        UpdatePanel_MSG_ProgrProduccionPT.Update();

                    }

                    break;


            }

        }


        protected void ModificarGrillaPT(string seccion)
        {

            int index = 0;
            switch (seccion)
            {
                case "EstructuraTecnica":

                    index = Convert.ToInt32(IndexEstructProyTecnico.Value);

                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    foreach (EstructuraTecnicaPT estructTec in List_EstructuraTecnica)
                    {
                        if (estructTec.index.Equals(index))
                        {
                            estructTec.index = Convert.ToInt32(index);
                            estructTec.tipoEstructura = new ParametroGenerico(Convert.ToInt32(TipoEstructura.SelectedValue), Convert.ToString(TipoEstructura.SelectedItem.Text));
                            estructTec.formaEstructura = new ParametroGenerico(Convert.ToInt32(FormaEstructura.SelectedValue), Convert.ToString(FormaEstructura.SelectedItem.Text));
                            estructTec.unidadMedida = new ParametroGenerico(Convert.ToInt32(UnidadDeMedida.SelectedValue), Convert.ToString(UnidadDeMedida.SelectedItem.Text));

                            estructTec.largo = 0;
                            estructTec.ancho = 0;
                            estructTec.alto = 0;
                            estructTec.diametro = 0;
                                
                            if (!TextBoxLargoM.Text.Equals(""))
                            {
                                estructTec.largo = Convert.ToSingle(TextBoxLargoM.Text);
                            }
                            if (!TextBoxAnchoM.Text.Equals(""))
                            {
                                estructTec.ancho = Convert.ToSingle(TextBoxAnchoM.Text);
                            }
                            if (!TextBoxAltoM.Text.Equals(""))
                            {
                                estructTec.alto = Convert.ToSingle(TextBoxAltoM.Text);
                            }
                            if (!TextBoxDiametroM.Text.Equals(""))
                            {
                                estructTec.diametro = Convert.ToSingle(TextBoxDiametroM.Text);
                            }
                                                       
                            estructTec.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(VolumenUnidadMedida.SelectedValue), Convert.ToString(VolumenUnidadMedida.SelectedItem.Text));
                            estructTec.volumenValorMedida = Convert.ToSingle(TextBoxVolumenValorMedida.Text);
                            estructTec.tipoAnio = new ParametroGenerico(Convert.ToInt32(Anio.SelectedValue), Convert.ToString(Anio.SelectedItem.Text));
                            estructTec.anios = new List<ValorParametroAnioPT>();

                            ValorParametroAnioPT valorParamAux = null;

                            if (TextBoxAnio1.Text != null && !TextBoxAnio1.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 1;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio1.Text);
                                estructTec.anios.Add(valorParamAux);
                            }

                            if (TextBoxAnio2.Text != null && !TextBoxAnio2.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 2;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio2.Text);
                                estructTec.anios.Add(valorParamAux);
                            }
                            if (TextBoxAnio3.Text != null && !TextBoxAnio3.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 3;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio3.Text);
                                estructTec.anios.Add(valorParamAux);

                            }

                            if (TextBoxAnio4.Text != null && !TextBoxAnio4.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 4;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio4.Text);
                                estructTec.anios.Add(valorParamAux);

                            }
                            if (TextBoxAnio5.Text != null && !TextBoxAnio5.Text.Equals(""))
                            {
                                valorParamAux = new ValorParametroAnioPT();
                                valorParamAux.idRegistro = 0;
                                valorParamAux.idclaveParametro = 0;
                                valorParamAux.anio = 5;
                                valorParamAux.valor = Convert.ToInt32(TextBoxAnio5.Text);
                                estructTec.anios.Add(valorParamAux);
                            }

                            /* Densidad de la siembra */
                            if (DensidadSiembra.Text != null && !DensidadSiembra.Text.Equals(""))
                            {
                                estructTec.densidadSiembra = Convert.ToSingle(DensidadSiembra.Text);
                            }

                            estructTec.totalAcumNumero = Convert.ToSingle(TotalAcumuladoNumero.Text);
                            if (TotalAcumuladoDimension.Text != null && !TotalAcumuladoDimension.Text.Equals(""))
                            {
                                estructTec.totalAcumDim = Convert.ToSingle(TotalAcumuladoDimension.Text);
                            }
                            List<String> listaErroresEstructTecnica = pTValidacion.validaEstructuraTecnica(estructTec);

                            if (listaErroresEstructTecnica.Count <= 0)
                            {
                                if (estructTec.idEstructPT > 0)
                                {
                                    estructTec.accion = accion.MODIFICAR;
                                }


                                GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                                GridEstructuraTecnicaProyTecnico.DataBind();

                                ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                                this.limpiarProyTecnico("EstructuraTecnica");
                                UpdatePanel_EstructuraTecnica.Update();

                            }
                            else
                            {

                                foreach (String error in listaErroresEstructTecnica)
                                {
                                    Page.Validators.Add(new ValidationError("grupo2", error));
                                }

                                UpdatePanel_MSG_EstructuraTecnica.Update();

                            }
                            break;
                        }
                    }


                    break;

                

                case "ProgramaProd":

                    index = Convert.ToInt32(IndexProd_ProyTecnico.Value);
                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];

                    ProgrProduccionPT progrProdFila = new ProgrProduccionPT();
                    progrProdFila = (ProgrProduccionPT)this.obtenerFormularioSeccion("ProgramaProd");
                    progrProdFila.index = Convert.ToInt32(index);

                    List<String> listaErroresProgrProd = pTValidacion.validaProgramaProduccion(progrProdFila);
                    if (listaErroresProgrProd.Count <= 0)
                    {
                        List<String> listaErroresProgrProdLista = pTValidacion.validaProgrProduccionLista(progrProdFila, List_ProgrProd);
                        if (listaErroresProgrProdLista.Count <= 0)
                        {
                            foreach (ProgrProduccionPT progrProduccion in List_ProgrProd)
                            {
                                if (progrProduccion.index.Equals(index))
                                {
                                    progrProduccion.especie = progrProdFila.especie;
                                    progrProduccion.grupo = progrProdFila.grupo;
                                    progrProduccion.etapaCultivo = progrProdFila.etapaCultivo;
                                    progrProduccion.tipoUnidProgramaProd = progrProdFila.tipoUnidProgramaProd;
                                    progrProduccion.tipoPesoPromEjemplares = progrProdFila.tipoPesoPromEjemplares;
                                    progrProduccion.pesoPromR1 = progrProdFila.pesoPromR1;
                                    progrProduccion.pesoPromR2 = progrProdFila.pesoPromR2;
                                    progrProduccion.pesoPromSR = progrProdFila.pesoPromSR;
                                    progrProduccion.densidad = progrProdFila.densidad;
                                    progrProduccion.produccionUltimoAnio = progrProdFila.produccionUltimoAnio;
                                    progrProduccion.tipoAnio = progrProdFila.tipoAnio;

                                    progrProduccion.aniosProgrProd = progrProdFila.aniosProgrProd;

                                    if (progrProduccion.idProgrProduccion > 0)
                                    {
                                        progrProduccion.accion = accion.MODIFICAR;
                                    }

                                    break;

                                }
                            }

                            GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                            GridProgrProdProyTecnico.DataBind();

                            ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                            this.limpiarProyTecnico("ProgramaProd");
                        }
                        else
                        {
                            foreach (String error in listaErroresProgrProdLista)
                            {
                                Page.Validators.Add(new ValidationError("grupo4", error));
                            }

                            UpdatePanel_MSG_ProgrProduccionPT.Update();
                        }
                    }
                    else
                    {
                        foreach (String error in listaErroresProgrProd)
                        {
                            Page.Validators.Add(new ValidationError("grupo4", error));
                        }

                        UpdatePanel_MSG_ProgrProduccionPT.Update();
                    }

                    break;

            }

        }


        private void CargarGrillaPT(string seccion, ProyectoTecnico proyectoTec)
        {
            switch (seccion)
            {

                case "EspecieAutorizada":

                    List<EspecieAutorizadaPT> List_EspecieAut = new List<EspecieAutorizadaPT>();

                    if (proyectoTec != null && proyectoTec.especieAutProyTecnico != null && proyectoTec.especieAutProyTecnico.Count > 0)
                    {
                        List_EspecieAut = proyectoTec.especieAutProyTecnico;
                    }
                    else
                    {
                        List_EspecieAut = (List<EspecieAutorizadaPT>)ViewState["EspecieAut_ProyTecnico"];
                    }

                    if (List_EspecieAut == null)
                    {
                        List_EspecieAut = new List<EspecieAutorizadaPT>();
                    }

                    GridEspecieAutorizadaProyTecnico.DataSource = List_EspecieAut;
                    GridEspecieAutorizadaProyTecnico.DataBind();
                    // GridEspecieAutorizadaProyTecnico.Visible = true;

                    ViewState["EspecieAut_ProyTecnico"] = (List<EspecieAutorizadaPT>)List_EspecieAut;

                    UpdatePanel_EspecieAutorizada.Update();

                    /* Si alguna de las especies es alga entonces debe desplegarse esta sección */
                    this.DespliegaCultivoAlgas(proyectoTec);

                    break;

                //case "GrupoAutorizada":


                //    List<GrupoPT> List_GrupoAut = new List<GrupoPT>();

                //    if (proyectoTec != null && proyectoTec.grupoAutProyTecnico != null && proyectoTec.grupoAutProyTecnico.Count > 0)
                //    {
                //        List_GrupoAut = proyectoTec.grupoAutProyTecnico;
                //    }
                //    else
                //    {
                //        List_GrupoAut = (List<GrupoPT>)ViewState["GrupoAut_ProyTecnico"];
                //    }

                //    if (List_GrupoAut == null)
                //    {
                //        List_GrupoAut = new List<GrupoPT>();
                //    }
                //    GridGrupoAutorizadaProyTecnico.DataSource = List_GrupoAut;
                //    GridGrupoAutorizadaProyTecnico.DataBind();
                //    // GridEspecieAutorizadaProyTecnico.Visible = true;

                //    ViewState["GrupoAut_ProyTecnico"] = (List<GrupoPT>)List_GrupoAut;
                //    this.DespliegaCultivoAlgas(proyectoTec);

                //    break;

                case "EstructuraTecnica":
                    List<EstructuraTecnicaPT> List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    if (proyectoTec != null && proyectoTec.estructTecnicaProyTecnico != null && proyectoTec.estructTecnicaProyTecnico.Count > 0)
                    {
                        List_EstructuraTecnica = proyectoTec.estructTecnicaProyTecnico;
                    }
                    else
                    {
                        List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];
                    }
                    if (List_EstructuraTecnica == null)
                    {
                        List_EstructuraTecnica = new List<EstructuraTecnicaPT>();
                    }

                    GridEstructuraTecnicaProyTecnico.DataSource = List_EstructuraTecnica;
                    GridEstructuraTecnicaProyTecnico.DataBind();
                    //GridEstructuraTecnicaProyTecnico.Visible = true;

                    ViewState["EstructuraTecnica_ProyTecnico"] = (List<EstructuraTecnicaPT>)List_EstructuraTecnica;
                    UpdatePanel_EstructuraTecnica.Update();

                    break;

               
                case "ProgramaProd":

                    List<ProgrProduccionPT> List_ProgrProd = new List<ProgrProduccionPT>();
                    if (proyectoTec != null && proyectoTec.progrProduccionProyTecnico != null && proyectoTec.progrProduccionProyTecnico.Count > 0)
                    {
                        List_ProgrProd = proyectoTec.progrProduccionProyTecnico;
                    }
                    else
                    {
                        List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];
                    }
                    if (List_ProgrProd == null)
                    {
                        List_ProgrProd = new List<ProgrProduccionPT>();
                    }
                    GridProgrProdProyTecnico.DataSource = List_ProgrProd;
                    GridProgrProdProyTecnico.DataBind();
                    //     GridProgrProdProyTecnico.Visible = true;

                    ViewState["ProgrProd_ProyTecnico"] = (List<ProgrProduccionPT>)List_ProgrProd;
                    UpdatePanel_ProgrProduccionPT.Update();

                    break;


                case "ArchivoBinario":

                    ExportarGrilla.Visible = false;

                    /*
                    List<ArchivosAdjPT> List_ArchivoBinarioEspecial = new List<ArchivosAdjPT>();
                    if (proyectoTec != null && proyectoTec.archivoBinarioList != null && proyectoTec.archivoBinarioList.Count > 0)
                    {
                        List_ArchivoBinarioEspecial = proyectoTec.archivoBinarioList;
                    }
                    else
                    {
                        List_ArchivoBinarioEspecial = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];
                    }
                    if (List_ArchivoBinarioEspecial == null)
                    {
                        List_ArchivoBinarioEspecial = new List<ArchivosAdjPT>();
                    }
                     */

                    List<ArchivosAdjPT> List_ArchivoBinarioEspecial = proyectoTecnicoService.ListarArchivosAdjuntoPT(Convert.ToInt32(IdProyectoTecnico.Value));

                    GridArchivoAdjunto.DataSource = List_ArchivoBinarioEspecial;
                    GridArchivoAdjunto.DataBind();

                    if (List_ArchivoBinarioEspecial != null && List_ArchivoBinarioEspecial.Count > 0)
                    {
                        ExportarGrilla.Visible = true;
                    }

                    ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinarioEspecial;
                    UpdatePanel_ArchivoAdjunto.Update();

                    break;
            }

        }

        
        
        //---------------------------------------------------

        protected void GridEspecieAutorizadaProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Eliminar":
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridEspecieAutorizadaProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "EspecieAutorizada");
                    // CargarGrillaPT("EspecieAutorizada", null);
                    break;
            };
        }


        protected void GridEspecieAutorizadaProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                };

            };
        }


        protected void GridEstructuraTecnicaProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProyectoTecnicoService proyService = new ProyectoTecnicoService();

            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idEstructProyTecnico = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridEspecieAutorizadaProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "EstructuraTecnica");
                    break;
                case "Modificar":

                    List<EstructuraTecnicaPT> List_EstructuraTecnica = (List<EstructuraTecnicaPT>)ViewState["EstructuraTecnica_ProyTecnico"];

                    foreach (EstructuraTecnicaPT estructuraTec in List_EstructuraTecnica)
                    {
                        if (estructuraTec.index.Equals(Convert.ToInt32(index)))
                        {
                            TipoEstructura.SelectedValue = Convert.ToString(estructuraTec.tipoEstructura.id);
                            FormaPorEstructura_OnSelectedIndexChanged(null, null);

                            if (estructuraTec.formaEstructura != null && estructuraTec.formaEstructura.id > 0)
                            {
                                FormaEstructura.SelectedValue = Convert.ToString(estructuraTec.formaEstructura.id);
                            }
                            else {
                                FormaEstructura.Items.Clear();
                                FormaEstructura.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                                FormaEstructura.Enabled = false;
                            }
                            
                            ManejoCamposEstructuraMedidasChanged(null, null);

                            if (estructuraTec.unidadMedida != null && estructuraTec.unidadMedida.id > 0)
                            {
                                UnidadDeMedida.SelectedValue = Convert.ToString(estructuraTec.unidadMedida.id);
                            }
                            else {
                                UnidadDeMedida.Items.Clear();
                                UnidadDeMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                                UnidadDeMedida.Enabled = false;
                            }

                            TextBoxLargoM.Text = Convert.ToString(estructuraTec.largo);
                            TextBoxAnchoM.Text = Convert.ToString(estructuraTec.ancho);
                            TextBoxAltoM.Text = Convert.ToString(estructuraTec.alto);
                            TextBoxDiametroM.Text = Convert.ToString(estructuraTec.diametro);
                            if (estructuraTec.volumenUnidadMedida != null && estructuraTec.volumenUnidadMedida.id > 0)
                            {
                                VolumenUnidadMedida.SelectedValue = Convert.ToString(estructuraTec.volumenUnidadMedida.id);
                            }
                            else
                            {
                                VolumenUnidadMedida.Items.Clear();
                                VolumenUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
                                VolumenUnidadMedida.Enabled = false;
                            }

                            TextBoxVolumenValorMedida.Text = Convert.ToString(estructuraTec.volumenValorMedida);
                            Anio.SelectedValue = Convert.ToString(estructuraTec.tipoAnio.id);
                            TipoAnioEstructuraTecnica_Selected(null, null);
                            if (estructuraTec.anios != null && estructuraTec.anios.Count > 0)
                            {
                                foreach (ValorParametroAnioPT param in estructuraTec.anios)
                                {
                                    if (param.anio == 1)
                                    {
                                        TextBoxAnio1.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 2)
                                    {
                                        TextBoxAnio2.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 3)
                                    {
                                        TextBoxAnio3.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 4)
                                    {
                                        TextBoxAnio4.Text = Convert.ToString(param.valor);
                                    }
                                    else if (param.anio == 5)
                                    {
                                        TextBoxAnio5.Text = Convert.ToString(param.valor);
                                    }
                                }
                            }

                            DensidadSiembra.Text = Convert.ToString(estructuraTec.densidadSiembra);

                            TotalAcumuladoNumero.Text = Convert.ToString(estructuraTec.totalAcumNumero);
                            this.CalculoTotalAcumuladoChange(null, null); //Recalculamos el total acumulado 

                            TotalAcumuladoDimension.Text = Convert.ToString(estructuraTec.totalAcumDim);
                            CalculoVolumenAutomaticoChange(null, null);

                            IdEstructProyTecnico.Value = Convert.ToString(estructuraTec.idEstructPT);
                            IndexEstructProyTecnico.Value = Convert.ToString(index);

                            Panel_Agregar_EstructuraTecnica.Visible = false;
                            Panel_Modificar_EstructuraTecnica.Visible = true;

                            break;
                        }
                    }
                    break;

            };
        }


        protected void GridEstructuraTecnicaProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                };

                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                };

            };
        }


       

        protected void GridEstructuraTecnica_Directo_Sustrato_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };


                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    //{
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    //}
                };

                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_eliminar != null)
                {
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESTRUCTURA_TECNICA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                    //{
                        boton_modificar.Visible = true;
                    //}
                };

            };
        }

        protected void GridProgrProdProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                // Borrar
                ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
                if (boton_eliminar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    {
                        boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
                        boton_eliminar.Visible = true;
                    }
                };
                // Modificar
                ImageButton boton_modificar = (ImageButton)e.Row.FindControl("gModificar");
                if (boton_modificar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_PROGRAMA_PRODUCCION"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.EDITAR))
                    {
                        boton_modificar.Visible = true;
                    }
                };


            };
        }


        protected void GridProgrProdProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idEstructProyTecnico = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Eliminar":
                    GridProgrProdProyTecnico.EditIndex = -1;
                    EliminarGrillaPT(index, "ProgramaProd");
                    // CargarGrillaPT("ProgramaProd", null);
                    break;
                case "Modificar":

                    List<ProgrProduccionPT> List_ProgrProd = (List<ProgrProduccionPT>)ViewState["ProgrProd_ProyTecnico"];

                    foreach (ProgrProduccionPT progrProduccionAux in List_ProgrProd)
                    {
                        if (progrProduccionAux.index.Equals(Convert.ToInt32(index)))
                        {
                            if (progrProduccionAux.especie != null && progrProduccionAux.especie.id > 0)
                            {
                                EspecieProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.especie.id);
                                //EtapaPorEspeciePP_OnSelectedIndexChanged(null, null);
                            }
                            else
                            {
                                EspecieProgramaProduccion.SelectedValue = "-1";
                            }

                            if (progrProduccionAux.grupo != null && progrProduccionAux.grupo.id > 0)
                            {
                                GrupoProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.grupo.id);
                                //EtapaPorGrupoPP_OnSelectedIndexChanged(null, null);
                            }
                            else
                            {
                                GrupoProgramaProduccion.SelectedValue = "-1";
                            }

                            //EtapaCultivoProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.etapaCultivo.id);

                            UnidadProgramaProduccion.SelectedValue = Convert.ToString(progrProduccionAux.tipoUnidProgramaProd.id);
                            PesoPromedioEjemplares.SelectedValue = Convert.ToString(progrProduccionAux.tipoPesoPromEjemplares.id);
                            DespliegaPesoProm_OnSelectedIndexChanged(null, null);

                            if (progrProduccionAux.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_SIN_RANGO)
                            {
                                PesoPromSinRango.Text = Convert.ToString(progrProduccionAux.pesoPromSR);
                            }
                            else if (progrProduccionAux.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_RANGO)
                            {
                                PesoRango1.Text = Convert.ToString(progrProduccionAux.pesoPromR1);
                                PesoRango2.Text = Convert.ToString(progrProduccionAux.pesoPromR2);
                            }

                            //DensidadProgProd.Text = Convert.ToString(progrProduccionAux.densidad);
                            ProdUltimoAnio.Text = Convert.ToString(progrProduccionAux.produccionUltimoAnio);

                            AnioProgramaProducc.SelectedValue = Convert.ToString(progrProduccionAux.tipoAnio.id);
                            
                            foreach (ValorParametroAnioPT valorAnio in progrProduccionAux.aniosProgrProd)
                            {
                                if (valorAnio.anio == 1)
                                {
                                    AnioProd1.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 2)
                                {
                                    AnioProd2.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 3)
                                {
                                    AnioProd3.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 4)
                                {
                                    AnioProd4.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                                else if (valorAnio.anio == 5)
                                {
                                    AnioProd5.Text = Convert.ToString(valorAnio.valorProgrProd);
                                }
                            }

                            IdProgrProd_ProyTecnico.Value = Convert.ToString(progrProduccionAux.idProgrProduccion);
                            IndexProd_ProyTecnico.Value = Convert.ToString(index);
                            Panel_AgregarProgrProd.Visible = false;
                            Panel_ModificarProgrProd.Visible = true;


                        }
                    }

                    break;

            };
        }


        protected Object obtenerFormularioSeccion(string seccion)
        {

            Object ob = new Object();

            switch (seccion)
            {
                case "ProgramaProd":
                    ProgrProduccionPT progrProduccion = new ProgrProduccionPT();
                    progrProduccion.especie = new ParametroGenerico(Convert.ToInt32(EspecieProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(EspecieProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.especie.descripcion = Convert.ToString(EspecieProgramaProduccion.SelectedItem.Text);
                    }
                    progrProduccion.grupo = new ParametroGenerico(Convert.ToInt32(GrupoProgramaProduccion.SelectedValue), "");
                    if (Convert.ToInt32(GrupoProgramaProduccion.SelectedValue) > 0)
                    {
                        progrProduccion.grupo.descripcion = Convert.ToString(GrupoProgramaProduccion.SelectedItem.Text);
                    }

                    //progrProduccion.etapaCultivo = new ParametroGenerico(Convert.ToInt32(EtapaCultivoProgramaProduccion.SelectedValue), Convert.ToString(EtapaCultivoProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(UnidadProgramaProduccion.SelectedValue), Convert.ToString(UnidadProgramaProduccion.SelectedItem.Text));
                    progrProduccion.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(PesoPromedioEjemplares.SelectedValue), Convert.ToString(PesoPromedioEjemplares.SelectedItem.Text));
                    if (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_RANGO)
                    {
                        if (PesoRango1 != null && !PesoRango1.Text.Equals(""))
                        {
                            progrProduccion.pesoPromR1 = Convert.ToSingle(PesoRango1.Text);
                        }
                        if (PesoRango2 != null && !PesoRango2.Text.Equals(""))
                        {
                            progrProduccion.pesoPromR2 = Convert.ToSingle(PesoRango2.Text);
                        }

                        progrProduccion.pesoPromSR = Convert.ToSingle(0);
                    }
                    if (Convert.ToInt32(PesoPromedioEjemplares.SelectedValue) == rbTipo.PESO_PROM_SIN_RANGO)
                    {
                        if (PesoPromSinRango != null && !PesoPromSinRango.Text.Equals(""))
                        {
                            progrProduccion.pesoPromSR = Convert.ToSingle(PesoPromSinRango.Text);
                        }
                        progrProduccion.pesoPromR1 = Convert.ToSingle(0);
                        progrProduccion.pesoPromR2 = Convert.ToSingle(0);
                    }

                    /*
                    if (!DensidadProgProd.Text.Equals(""))
                    {
                        progrProduccion.densidad = Convert.ToSingle(DensidadProgProd.Text);
                    }
                     * */
                    if (!ProdUltimoAnio.Text.Equals(""))
                    {
                        progrProduccion.produccionUltimoAnio = Convert.ToSingle(ProdUltimoAnio.Text);
                    }

                    if (AnioProgramaProducc != null && !AnioProgramaProducc.Text.Equals(""))
                    {
                        progrProduccion.tipoAnio = new ParametroGenerico();
                        progrProduccion.tipoAnio.id = Convert.ToInt32(AnioProgramaProducc.SelectedItem.Value);
                    }

                    progrProduccion.aniosProgrProd = new List<ValorParametroAnioPT>();

                    ValorParametroAnioPT valorParamAnioPP = null;
                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 1;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd1.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    valorParamAnioPP = new ValorParametroAnioPT();
                    valorParamAnioPP.idRegistro = 0;
                    valorParamAnioPP.idclaveParametro = 0;
                    valorParamAnioPP.anio = 2;
                    valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd2.Text);

                    progrProduccion.aniosProgrProd.Add(valorParamAnioPP);

                    if (AnioProd3.Text != null && !AnioProd3.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 3;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd3.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }

                    if (AnioProd4.Text != null && !AnioProd4.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 4;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd4.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }

                    if (AnioProd5.Text != null && !AnioProd5.Text.Equals(""))
                    {
                        valorParamAnioPP = new ValorParametroAnioPT();
                        valorParamAnioPP.idRegistro = 0;
                        valorParamAnioPP.idclaveParametro = 0;
                        valorParamAnioPP.anio = 5;
                        valorParamAnioPP.valorProgrProd = Convert.ToSingle(AnioProd5.Text);
                        progrProduccion.aniosProgrProd.Add(valorParamAnioPP);
                    }

                    ob = progrProduccion;

                    break;

            }
            return ob;



        }



        ////GRUPO DE ESPECIES
        //protected void GridGrupoAutorizadaProyTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "Eliminar":
        //            int index = Convert.ToInt32(e.CommandArgument);
        //            GridGrupoAutorizadaProyTecnico.EditIndex = -1;
        //            EliminarGrillaPT(index, "GrupoAutorizada");
        //            break;
        //    };
        //}

        ////GRUPO DE ESPECIES
        //protected void GridGrupoAutorizadaProyTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //ACCION
        //        HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
        //        if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
        //        {
        //            e.Row.Attributes["style"] = "display:none";
        //        };
        //        // Borrar
        //        ImageButton boton_eliminar = (ImageButton)e.Row.FindControl("gEliminar");
        //        if (boton_eliminar != null)
        //        {
        //            if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ESPECIE_AUTORIZADA"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
        //            {
        //                boton_eliminar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro de eliminar?')");
        //                boton_eliminar.Visible = true;
        //            }
        //        };

        //    };
        //}


        //protected void GuardarGrupoAutorizada_Click(object sender, ImageClickEventArgs e)
        //{
        //    AgregarGrillaPT("GrupoAutorizada");
        //    CargarGrillaPT("GrupoAutorizada", null);
        //}



        //protected void EtapaPorGrupoAutorizada_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_Combobox("EtapaDeCultivoAutorizadas2");

        //}

        protected void GridArchivoAdjunto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //ACCION
                HiddenField hidden_accion = (HiddenField)e.Row.FindControl("gAccion");
                if (hidden_accion != null && !hidden_accion.Value.Equals("") && (Convert.ToInt32(hidden_accion.Value) == accion.ELIMINAR || Convert.ToInt32(hidden_accion.Value) == accion.IGNORAR))
                {
                    e.Row.Attributes["style"] = "display:none";
                };
                
                //Eliminar
                ImageButton boton_borrar = (ImageButton)e.Row.FindControl("gBorrar");
                if (boton_borrar != null)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ELIMINAR))
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ELIMINAR))
                    {
                        boton_borrar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea eliminar el Archivo Adjunto?')");
                        boton_borrar.Visible = true;
                    }

                }

                int idArchivoBin = Convert.ToInt32(GridArchivoAdjunto.DataKeys[e.Row.RowIndex].Value);
                ArchivosAdjPT archivoAdjPT = proyectoTecnicoDA.ObtenerArchivosAdjuntoPT(Convert.ToInt32(IdProyectoTecnico.Value), idArchivoBin);

                if (archivoAdjPT != null && archivoAdjPT.estadoVigencia.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    //Desasociar
                    ImageButton boton_desasociar = (ImageButton)e.Row.FindControl("gDesasociar");
                    if (boton_desasociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESASOCIAR))
                        //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESASOCIAR))
                        {
                            boton_desasociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_desasociar.Visible = true;
                        }
                    };
                }

                else if (archivoAdjPT != null && archivoAdjPT.estadoVigencia.id == rbEstadosGenerales.VIGENTE)
                {
                    //Asociar
                    ImageButton boton_asociar = (ImageButton)e.Row.FindControl("gAsociar");
                    if (boton_asociar != null)
                    {
                        if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.ASOCIAR))
                        //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.ASOCIAR))
                        {
                            boton_asociar.Attributes.Add("onclick", "javascript:return " + "confirm('¿Está seguro que desea cambiar de Estado al Archivo?')");
                            boton_asociar.Visible = true;
                        }
                    };
                }

                //Descargar
                String idArchivoBinarioString = DataBinder.Eval(e.Row.DataItem, "idPT").ToString();
                ImageButton boton_descargar = (ImageButton)e.Row.FindControl("gDescargar");
                if (boton_descargar != null && idArchivoBinarioString != null && !idArchivoBinarioString.Equals("") && Convert.ToInt32(idArchivoBinarioString) > 0)
                {
                    if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], (Datos.Entidades.SolicitudConcesion)Session[ViewState["solicitudSession"].ToString()], rbAccion.DESCARGAR))
                    //if (permisosService.tieneAccesoA2((int[])ViewState["SECCION_ESPECIFICA_ARCHIVO_ADJUNTO"], (Datos.Entidades.Usuario.Serializable)Session["Usuario"], null, rbAccion.DESCARGAR))
                    {
                        boton_descargar.Visible = true;
                    }
                };

            }
        }

        protected void GridArchivoAdjunto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idArchivoAdjunto = Convert.ToInt32(arg[0]);
            int index = Convert.ToInt32(arg[1]);

            switch (e.CommandName)
            {
                case "Descargar":

                    List<ArchivosAdjPT> List_ArchivoBinario = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];
                    ArchivoBinarioEspecial archivoBinarioEspecial = null;

                    foreach (ArchivosAdjPT archivo in List_ArchivoBinario)
                    {
                        if (archivo.index.Equals(index))
                        {
                            archivoBinarioEspecial = archivo.archivoBinario;
                        }
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/" + archivoBinarioEspecial.formato;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + archivoBinarioEspecial.nombreFisico + "." + archivoBinarioEspecial.formato);
                    Response.BinaryWrite(archivoBinarioEspecial.bytes);
                    Response.Flush();
                    Response.End();

                    break;

                case "Eliminar":
                    GridArchivoAdjunto.EditIndex = -1;
                    EliminarArchivoAdjunto(index);
                    
                    break;

                case "Desasociar":
                    CambiarEstadoArchivo(idArchivoAdjunto);
                    CargarGrillaPT("ArchivoBinario",null);
                    UpdatePanelArchivoAdjuntoGrilla.Update();

                    break;

                case "Asociar":
                    CambiarEstadoArchivo(idArchivoAdjunto);
                    CargarGrillaPT("ArchivoBinario",null);
                    UpdatePanelArchivoAdjuntoGrilla.Update();

                    break;
            }
        }

        private void EliminarArchivoAdjunto(int index)
        {
            List<ArchivosAdjPT> List_ArchivoBinario = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];
            foreach (ArchivosAdjPT archivo in List_ArchivoBinario)
            {
                if (archivo.index.Equals(index))
                {
                    if (archivo.accion == accion.INGRESAR)
                    {
                        archivo.accion = accion.IGNORAR;
                        //GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";
                    }
                    if (archivo.accion == accion.LISTADO || archivo.accion == accion.MODIFICAR)
                    {
                        archivo.accion = accion.ELIMINAR;
                        //GridArchivoAdjunto.Rows[index].Attributes["style"] = "display:none";

                        bool resp = proyectoTecnicoService.eliminarArchivoBinarioPT(archivo.idPT, archivo.archivoBinario.idArchivo);
                        if (resp)
                        {
                            CargarGrillaPT("ArchivoBinario", null);

                            /*
                            GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                            GridArchivoAdjunto.DataBind();
                            GridArchivoAdjunto.Visible = true;

                            ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinario;

                            UpdatePanelArchivoAdjuntoGrilla.Update();
                             **/
                        }
                    }
                }

            }

            ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinario;
            CargarGrillaPT("ArchivoBinario", null);

        }

        private void CambiarEstadoArchivo(int idArchivoAdjunto)
        {
            List<ArchivosAdjPT> List_ArchivoBinario = (List<ArchivosAdjPT>)ViewState["ArchivoBinario"];
            foreach (ArchivosAdjPT archivo in List_ArchivoBinario)
            {
                if (archivo != null && archivo.archivoBinario != null && archivo.archivoBinario.idArchivo == idArchivoAdjunto)
                {

                    if (archivo.estadoVigencia.id == rbEstadosGenerales.NO_VIGENTE)
                    {
                        archivo.estadoVigencia.id = rbEstadosGenerales.VIGENTE;
                        archivo.estadoVigencia.descripcion = "Vigente";
                    }
                    else
                    {
                        archivo.estadoVigencia.id = rbEstadosGenerales.NO_VIGENTE;
                        archivo.estadoVigencia.descripcion = "No Vigente";
                    }

                    archivo.cambiaEstado = true;

                    bool resp = proyectoTecnicoService.ActualizarEstadoVigenciaArchivoPT(archivo);
                    if (resp)
                    {
                        CargarGrillaPT("ArchivoBinario", null);
                        /*
                        GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                        GridArchivoAdjunto.DataBind();
                        GridArchivoAdjunto.Visible = true;

                        ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinario;

                        UpdatePanelArchivoAdjuntoGrilla.Update();
                         * */
                    }
                }
            }
        }

        protected void GuardarArchivoAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            
            List<ArchivosAdjPT> List_ArchivoBinario = proyectoTecnicoService.ListarArchivosAdjuntoPT(Convert.ToInt32(IdProyectoTecnico.Value));
            
            int index = 0;
            if (List_ArchivoBinario == null)
            {
                List_ArchivoBinario = new List<ArchivosAdjPT>();
            }
            else
            {
                index = List_ArchivoBinario.Count;
            }
            
 
            ProyectoTecnico proyTecnicoSolicitud = new ProyectoTecnico();

            proyTecnicoSolicitud.idSolicitud = Convert.ToInt32(IdSolicitud.Value);
            proyTecnicoSolicitud.IdProyectoTecnico = proyectoTecnicoService.ObtieneClaveProyectoTecnico(proyTecnicoSolicitud.idSolicitud);

            ArchivosAdjPT archivosAdjPT = new ArchivosAdjPT();
            archivosAdjPT.idPT = proyTecnicoSolicitud.IdProyectoTecnico;
            archivosAdjPT.tipoDocumento = new ParametroGenerico(Convert.ToInt32(TipoArchivo.SelectedValue), TipoArchivo.SelectedItem.Text);

            if (NumeroCI.Text != null && !NumeroCI.Text.Equals(""))
            {
                archivosAdjPT.numCI = Convert.ToInt32(NumeroCI.Text);
            }

            if (FechaTextRecepcion.Text != null && !FechaTextRecepcion.Text.Equals(""))
            {
                archivosAdjPT.fechaCI = Convert.ToDateTime(FechaTextRecepcion.Text);
            }

            archivosAdjPT.estadoVigencia = new ParametroGenerico();
            archivosAdjPT.estadoVigencia.id = rbEstadosGenerales.VIGENTE;

            ArchivoBinarioEspecial archivoBinario = new ArchivoBinarioEspecial();
            archivoBinario.accion = accion.INGRESAR;
            //archivoBinario.index = Convert.ToInt32(index);
            archivoBinario.nombreArchivo = NombreArchivo.Text;


            List<String> listaErroresArchivoAdjunto = pTValidacion.validaArchivoAdjuntoProyectoTecnico(archivosAdjPT, List_ArchivoBinario);

            if (ArchivoAdjunto.HasFile)
            {

                archivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                //archivoBinario.nombreArchivo = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                archivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                archivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                archivoBinario.bytes = ArchivoAdjunto.FileBytes;
            }
            else 
            {
                listaErroresArchivoAdjunto.Add("Seleccione Archivo");
            }

            archivosAdjPT.archivoBinario = archivoBinario;

           

            if (listaErroresArchivoAdjunto.Count <= 0)
            {
                if (proyTecnicoSolicitud.IdProyectoTecnico > 0)
                {
                    bool resp = proyectoTecnicoService.guardarArchivoBinarioPT(proyTecnicoSolicitud, archivosAdjPT);
                    if (resp)
                    {
                        CargarGrillaPT("ArchivoBinario", null);

                        /*
                        List_ArchivoBinario.Add(archivosAdjPT);

                        GridArchivoAdjunto.DataSource = List_ArchivoBinario;
                        GridArchivoAdjunto.DataBind();
                        GridArchivoAdjunto.Visible = true;

                        ViewState["ArchivoBinario"] = (List<ArchivosAdjPT>)List_ArchivoBinario;

                        limpiarProyTecnico("ArchivoBinario");

                        UpdatePanelArchivoAdjuntoGrilla.Update();
                         **/
                    }
                }
            }
            else
            {
                foreach (String error in listaErroresArchivoAdjunto)
                {
                    Page.Validators.Add(new ValidationError("grupoArchivo", error));
                }
            }
        }

        protected void ImgAdd_PreRender(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            ScriptManager sc = ScriptManager.GetCurrent(this.Page);
            sc.RegisterPostBackControl(btn);
        }

        protected void TipoAlimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TipoAlimento.SelectedItem.Value) == rbTipo.TIPO_ALIMENTO_PT_OTRO)
            {
                PanelNombreOtroTipoAlimento.Visible = true;
            }
            else {
                PanelNombreOtroTipoAlimento.Visible = false;
            }
            UpdatePanel_EspecieAutorizada.Update();
        }

        
        protected void GuardarBotonEstructuraTecnica_Directo_Sustrato_Click(object sender, ImageClickEventArgs e)
        {
            AgregarGrillaPT("EstructuraTecnicaDirectoSust");
            CargarGrillaPT("EstructuraTecnicaDirectoSust", null);
        }

        protected void LimpiarBotonEstructuraTecnica_Directo_Sustrato_Click(object sender, ImageClickEventArgs e)
        {
            this.limpiarProyTecnico("EstructuraTecnicaDirectoSust");
        }

        protected void BotonModificarEstructuraTecnica_Directo_Sustrato_Click(object sender, ImageClickEventArgs e)
        {
            ModificarGrillaPT("EstructuraTecnicaDirectoSust");
            CargarGrillaPT("EstructuraTecnicaDirectoSust", null);

        }

        protected void ExportarGrilla_Click(object sender, EventArgs e)
        {
            GridView grilla = new GridView();

            CargarGrillaPT("ArchivoBinario", null);

            int cantidad = GridArchivoAdjunto.Columns.Count;

            if (cantidad > 1)
            {
                cantidad = cantidad - 1;
            }

            GridArchivoAdjunto.Columns.RemoveAt(cantidad);
            grilla = GridArchivoAdjunto;

            grilla.AllowPaging = false;
            grilla.DataBind();
            SubPesca.Utilidades.GridViewExportUtil.Export("ProyectosTecnicos.xls", grilla);
        }

    }
}