using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Utilidades;

namespace LogicaNegocio.cl.subpesca.rb.common
{
   public class BarrioDA 
    {

       Logger logger = new Logger();

       public object obtenerBarrio(int idTipoBarrio, int IdBarrio, int IdRegion, int IdMacrozona)
       {
           Conexion cnn = new Conexion();
           cnn.procedimiento = "paSelBarrio";

           if (idTipoBarrio > 0)
           {
               cnn.parametros.Add("@idTipoBarrio", idTipoBarrio);
           }
           if (IdBarrio > 0)
           {
               cnn.parametros.Add("@IdBarrio", IdBarrio);
           }
           if (IdRegion > 0)
           {
               cnn.parametros.Add("@IdRegion", IdRegion);
           }
           if (IdMacrozona > 0)
           {
               cnn.parametros.Add("@IdMacrozona", IdMacrozona);
           }

           DataTable dt = cnn.Execute();
           return dt;
       }


        public List<AsociacionSolicitudBarrio> listarSolicitudBarrio(AsociacionSolicitudBarrio barrioFiltro)
        {
            
            try
            {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbSolicitudBarrio";

            if (barrioFiltro.tipo != null)
            {
                cnn.parametros.Add("@idTipo", barrioFiltro.tipo.id);
            }
            if (barrioFiltro.tipoSolicitud != null && barrioFiltro.tipoSolicitud.id > 0)
            {
                cnn.parametros.Add("@idTipoTramite", barrioFiltro.tipoSolicitud.id);
            }
            if (barrioFiltro.tipoUnidadEspacial != null && barrioFiltro.tipoUnidadEspacial.id > 0)
            {
                cnn.parametros.Add("@idTipoUnidEspacial", barrioFiltro.tipoUnidadEspacial.id);
            }
            if (barrioFiltro.barrioACS != null && barrioFiltro.barrioACS.id > 0)
            {
                cnn.parametros.Add("@idBarrioACS", barrioFiltro.barrioACS.id);
            }
            if (barrioFiltro.barrioACM != null && barrioFiltro.barrioACM.id > 0)
            {
                cnn.parametros.Add("@idBarrioACM", barrioFiltro.barrioACM.id);
            }
            if (barrioFiltro.numPert != null && !barrioFiltro.numPert.Trim().Equals(""))
            {
                if (barrioFiltro.numSector > -1)
                {
                    cnn.parametros.Add("@numPert", barrioFiltro.numPert + "-" + barrioFiltro.numSector.ToString());
                }
                else 
                {
                    cnn.parametros.Add("@numPert", barrioFiltro.numPert);
                }
            }
            if (barrioFiltro.codigoCentro != null && !barrioFiltro.codigoCentro.Trim().Equals(""))
            {
                cnn.parametros.Add("@codigoCentro", barrioFiltro.codigoCentro);
            }

            List<AsociacionSolicitudBarrio> resultado = new List<AsociacionSolicitudBarrio>();
            AsociacionSolicitudBarrio asociacionSolicitudBarrio = null;

            DataTable dt = cnn.Execute();
            int idSolConcesionVar = 0;
            int idSolConcesionAux = 0;
            int idTipoTramiteAux = 0;
            bool traspasoOKAux = false;

            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {

                    idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                    if (idSolConcesionVar != idSolConcesionAux)
                    {

                        
                        asociacionSolicitudBarrio = new AsociacionSolicitudBarrio();

                        asociacionSolicitudBarrio.idSolConcesion = idSolConcesionVar;
                        traspasoOKAux = Convert.ToBoolean(row["traspasoOk"]);


                        idTipoTramiteAux =  Convert.ToInt32(row["idTipoTramite"]);

                        if (traspasoOKAux && Funciones.EsTramiteUE(idTipoTramiteAux)) //aparte de estar marcado como traspaso ok, debe ser un tramite de UE, sin considerar eso se considerarian UE aprobadas que son tramites de mod o rel aprobados
                        {
                            asociacionSolicitudBarrio.tipo = new ParametroGenerico(0, "Unidad Espacial");
                            asociacionSolicitudBarrio.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                            asociacionSolicitudBarrio.codigoCentro = row["codigoCentro"].ToString();
                            if (!row.IsNull("numPert"))
                            {
                                asociacionSolicitudBarrio.numPert = row["numPert"].ToString();
                            }
                        }
                        else {
                            asociacionSolicitudBarrio.tipo = new ParametroGenerico(0, "Trámite");
                            asociacionSolicitudBarrio.tipoSolicitud = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoSol"].ToString());
                            asociacionSolicitudBarrio.numPert = row["numPert"].ToString();
                        }
                        

                        if (!row.IsNull("IdRegion"))
                        {
                            asociacionSolicitudBarrio.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                        }
                        

                        if (!row.IsNull("IdBarrio"))
                        {
                            asociacionSolicitudBarrio.barrioACS = new ParametroGenerico(Convert.ToInt32(row["IdBarrio"]), row["BarrioACS"].ToString());
                        }

                        if (!row.IsNull("idACM"))
                        {
                            asociacionSolicitudBarrio.barrioACM = new ParametroGenerico(Convert.ToInt32(row["idACM"]), row["BarrioACM"].ToString());
                        }

                        asociacionSolicitudBarrio.tipoModificacionesTram = new List<ParametroGenerico>();
                        resultado.Add(asociacionSolicitudBarrio);
                    }

                    if (!row.IsNull("idTipoModificacion"))
                    {
                        asociacionSolicitudBarrio.tipoModificacionesTram.Add(new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nombreTipoMod"].ToString()));
                    }


                    idSolConcesionAux = idSolConcesionVar;
                }
            }

            return resultado;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
            
        }
    }
}
