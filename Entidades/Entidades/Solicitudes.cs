using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;

namespace Datos.Entidades
{
    public class Solicitudes
    {


        #region RESUMEN DE ESTADOS

        /**
        * Obtiene el resumen de estados (Solicitudes en trámite)
        */
        public DataTable Solicitudes_Estados_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, int id_estado, string numPert, bool checkAvanzaAprueba)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudesEstadosResumenListar";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };

            cnn.parametros.Add("@id_usuario", id_usuario);
            
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };

            if (id_estado > 0)
            {
                cnn.parametros.Add("@id_estado", id_estado);
            };

            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };

            
         

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }

        /**
        * Obtiene el resumen de estados para las islas (Solicitudes en trámite)
        */
        public DataTable Solicitudes_Estados_Resumen_Listar_RB_ISLA(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, int id_estado, string numPert, bool checkAvanzaAprueba)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudesEstadosResumenListarIslas";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };

            cnn.parametros.Add("@id_usuario", id_usuario);

            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };

            if (id_estado > 0)
            {
                cnn.parametros.Add("@id_estado", id_estado);
            };

            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };


            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);
            


            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene el resumen de estados (Solicitudes rechazadas)
        */
        public DataTable Solicitudes_Estados_Rechazadas_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, int id_estado, string numPert, bool checkAvanzaAprueba)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudesEstadosRechazadasResumenListar";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };

            cnn.parametros.Add("@id_usuario", id_usuario);

            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };

            if (id_estado > 0)
            {
                cnn.parametros.Add("@id_estado", id_estado);
            };

            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };


            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }

        /**
        * Obtiene el resumen de estados ISLA (Solicitudes rechazadas)
        */
        public DataTable Solicitudes_Estados_Rechazadas_Resumen_Listar_RB_ISLA(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, int id_estado, string numPert, bool checkAvanzaAprueba)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudesEstadosRechazadasResumenListarIslas";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };

            cnn.parametros.Add("@id_usuario", id_usuario);

            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };

            if (id_estado > 0)
            {
                cnn.parametros.Add("@id_estado", id_estado);
            };

            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };


        
            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }

        /**
        * Obtiene el resumen de estados (Solicitudes en recurso de reposicion)
        */
        public DataTable Solicitudes_Estados_Reposicion_Resumen_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, int id_estado, string numPert, bool checkAvanzaAprueba)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudesReposicionEstadosResumenListar";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };

            cnn.parametros.Add("@id_usuario", id_usuario);

            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };

            if (id_estado > 0)
            {
                cnn.parametros.Add("@id_estado", id_estado);
            };

            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };


          

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene las solicitudes en un determinado estado  (Solicitudes en trámite)
        */
        public DataTable Solicitudes_Listar_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, string numPert, bool checkAvanzaAprueba)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paselRbSolicitudesEstadoListar";
            cnn.parametros.Add("@id_estado", id_estado);
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);

            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene las solicitudes en un determinado estado ISLA  (Solicitudes en trámite)
        */
        public DataTable Solicitudes_Listar_RB_ISLAS(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, string numPert, bool checkAvanzaAprueba)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paselRbSolicitudesEstadoListarIslas";
            
            cnn.parametros.Add("@id_estado", id_estado);
            
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene las solicitudes en un determinado estado  (Solicitudes rechazadas)
        */
        public DataTable Solicitudes_Listar_Rechazadas_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, string numPert, bool checkAvanzaAprueba)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paselRbSolicitudesRechazadasEstadoListar";
            cnn.parametros.Add("@id_estado", id_estado);
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);

            DataTable dt = cnn.Execute();
            return dt;
        }

        /**
       * Obtiene las solicitudes en un determinado estado  (Solicitudes rechazadas)
       */
        public DataTable Solicitudes_Listar_Rechazadas_RB_ISLA(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, string numPert, bool checkAvanzaAprueba)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paselRbSolicitudesRechazadasEstadoListarIslas";
            cnn.parametros.Add("@id_estado", id_estado);
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };
            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);


            DataTable dt = cnn.Execute();
            return dt;
        }



        /**
        * Obtiene las solicitudes en un determinado estado  (Solicitudes en recurso de reposicion)
        */
        public DataTable Solicitudes_Listar_Reposicion_RB(int id_estado, int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int id_usuarioSec, string numPert, bool checkAvanzaAprueba)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paselRbSolicitudesReposicionEstadoListar";
            cnn.parametros.Add("@id_estado", id_estado);
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            if (id_usuarioSec > 0)
            {
                cnn.parametros.Add("@id_usuarioSec", id_usuarioSec);
            };
            if (numPert != null && !numPert.Trim().Equals(""))
            {
                cnn.parametros.Add("@numPert", numPert);
            };

            cnn.parametros.Add("@checkAvanzaAprueba", checkAvanzaAprueba);

            DataTable dt = cnn.Execute();
            return dt;
        }


        #endregion



        #region Requerimientos con Plazos


        /**
        * Obtiene listado de requerimientos que estan por vencer.
        * idSubTipoTramite = tipo de modificacion o tipo de relocalizacion
        */
        public DataTable Requerimientos_Por_Vencer_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRequerimientosPorVencerListar";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);

            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            if (idSubTipoTramite > 0)
            {
                cnn.parametros.Add("@idSubTipoTramite", idSubTipoTramite);
            };
            if (id_tipodestinatario > 0)
            {
                cnn.parametros.Add("@idTipoDestinatario", id_tipodestinatario);
            };

            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene listado de requerimientos que estan vencidos.
        * idSubTipoTramite = tipo de modificacion o tipo de relocalizacion
        */
        public DataTable Requerimientos_Vencidos_Listar_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRequerimientosVencidosListar";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);


            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            if (idSubTipoTramite > 0)
            {
                cnn.parametros.Add("@idSubTipoTramite", idSubTipoTramite);
            };
            if (id_tipodestinatario > 0)
            {
                cnn.parametros.Add("@idTipoDestinatario", id_tipodestinatario);
            };

            DataTable dt = cnn.Execute();
            return dt;
        }


        /**
        * Obtiene detalle de solicitudes que tienen un determinado requerimiento que esta por vencer.
        * idSubTipoTramite = tipo de modificacion o tipo de relocalizacion
        */
        public DataTable Requerimientos_Vencidos_Ver_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int idSubRequerimiento, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            Conexion cnn = new Conexion();

            cnn.procedimiento = "paSelRequerimientosVencidosVer";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            if (idSubTipoTramite > 0)
            {
                cnn.parametros.Add("@idSubTipoTramite", idSubTipoTramite);
            };
            if (id_tipodestinatario > 0)
            {
                cnn.parametros.Add("@idTipoDestinatario", id_tipodestinatario);
            };

            DataTable dt = cnn.Execute();
            return dt;
        }



        /**
        * Obtiene detalle de solicitudes que tienen un determinado requerimiento que estan vencidos.
        * idSubTipoTramite = tipo de modificacion o tipo de relocalizacion
        */
        public DataTable Requerimientos_Por_Vencer_Ver_RB(int id_region, int id_provincia, int id_comuna, int id_tiposolic, int id_usuario, int idSubRequerimiento, int idTipoTramite, int idSubTipoTramite, int id_tipodestinatario)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRequerimientosPorVencerVer";
            if (id_region > 0)
            {
                cnn.parametros.Add("@id_region", id_region);
            };
            if (id_provincia > 0)
            {
                cnn.parametros.Add("@id_provincia", id_provincia);
            };
            if (id_comuna > 0)
            {
                cnn.parametros.Add("@id_comuna", id_comuna);
            };
            if (id_tiposolic > 0)
            {
                cnn.parametros.Add("@id_tiposolic", id_tiposolic);
            };
            cnn.parametros.Add("@id_usuario", id_usuario);
            cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            if (idSubTipoTramite > 0)
            {
                cnn.parametros.Add("@idSubTipoTramite", idSubTipoTramite);
            };
            if (id_tipodestinatario > 0)
            {
                cnn.parametros.Add("@idTipoDestinatario", id_tipodestinatario);
            };

            DataTable dt = cnn.Execute();
            return dt;
        }


        #endregion




        public DataTable ObtieneTipoSolicitud(int id_tiposolic)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbTipoSolicitud";
            cnn.parametros.Add("@id_tiposolic", id_tiposolic);

            DataTable dt = cnn.Execute();
            return dt;
        }

       

        public DataTable ObtieneEstadoSolicitud(int id_estado)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbEstadosSolicitud";
            cnn.parametros.Add("@id_estado", id_estado);

            DataTable dt = cnn.Execute();
            return dt;
        }




        public DataTable ObtieneSolicitudVer(int id_solicitud)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudConcesionVer";
            cnn.parametros.Add("@id_solicitud", id_solicitud);

            DataTable dt = cnn.Execute();
            return dt;
        }

        

        //Detalle de la Solicitud - indicadores
        public DataTable ObtieneEstructurasTecnicasSolicitud(string KeySort, int id_solicitud)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbEstructuraProyectoTecnicoSolicitud";
            cnn.parametros.Add("@keysort", KeySort);
            cnn.parametros.Add("@id_solicitud", id_solicitud);

            DataTable dt = cnn.Execute();
            return dt;
        }



        //Detalle de la Solicitud - indicadores
        public DataTable ObtieneProgramaProduccionSolicitud(string KeySort, int id_solicitud)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbProgrProduccionSolicitud";
            cnn.parametros.Add("@keysort", KeySort);
            cnn.parametros.Add("@id_solicitud", id_solicitud);

            DataTable dt = cnn.Execute();
            return dt;
        }


        //Detalle de la Solicitud - indicadores
        public DataTable RB_Solicitudes_Vertices_Listar(string KeySort, int id_solicitud)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelrbVerticeSolicitud";
            cnn.parametros.Add("@keysort", KeySort);
            cnn.parametros.Add("@id_solicitud", id_solicitud);

            DataTable dt = cnn.Execute();
            return dt;
        }





       
    }
}
