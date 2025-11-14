using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class CartaDA
    {

        public Logger Log { get; set; }

        public CartaDA() { }

        /**
    * Lista cartas.
    */
        public List<Carta> ListaCartaDA(Carta cartaFiltro, string campoFiltro)
        {
            try
            {
                Carta carta = null;
                List<Carta> resp = new List<Carta>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCartaFiltro";
                
                if(campoFiltro!= null && !campoFiltro.Equals("")){
                    cnn.parametros.Add("@descripcionCarta", campoFiltro);
                }else{
                    if(cartaFiltro.idCarta>0){
                        cnn.parametros.Add("@idCarta", cartaFiltro.idCarta);
                    }
                    if(cartaFiltro.region!=null && cartaFiltro.region.id>0){
                        cnn.parametros.Add("@idRegion", cartaFiltro.region.id);
                    }
                    if(cartaFiltro.tipoCarta!=null && cartaFiltro.tipoCarta.id>0){
                        cnn.parametros.Add("@idTipoCarta", cartaFiltro.tipoCarta.id);
                    }
                    if(cartaFiltro.datum!=null && cartaFiltro.datum.id>0){
                        cnn.parametros.Add("@idDatum", cartaFiltro.datum.id);
                    }
                    if(cartaFiltro.huso!=null && cartaFiltro.huso.id>0){
                        cnn.parametros.Add("@idHuso", cartaFiltro.huso.id);
                    }
                
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        carta = new Carta();
                        carta.idCarta = Convert.ToInt32(row["idCarta"]);
                        carta.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["nombreRegion"].ToString());
                        carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        carta.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["nombreDatum"].ToString());
                        if (!row.IsNull("idHuso")){
                            carta.huso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }
                        if (!row.IsNull("numeroCarta"))
                        {
                            carta.numeroCarta = row["numeroCarta"].ToString();
                        }
                        if (!row.IsNull("numEdicion"))
                        {
                            carta.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                        }
                        if (!row.IsNull("anioEdicion"))
                        {
                            carta.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                        }
                        if (!row.IsNull("escala"))
                        {
                            carta.escala = row["escala"].ToString();
                        }
                        if (!row.IsNull("A_A_A")) {
                            carta.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                        }
                        
                        carta.descripcionCarta = row["descripcionCarta"].ToString();

                        resp.Add(carta);
                    }
                }

                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public Carta ObtieneCartaDA(Carta cartaFiltro, string campoFiltro)
        {
            try
            {
                Carta carta = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCartaFiltro";

                if (campoFiltro != null && !campoFiltro.Equals(""))
                {
                    cnn.parametros.Add("@descripcionCarta", campoFiltro);
                }
                else
                {
                    if (cartaFiltro.idCarta > 0)
                    {
                        cnn.parametros.Add("@idCarta", cartaFiltro.idCarta);
                    }
                    if (cartaFiltro.region != null && cartaFiltro.region.id > 0)
                    {
                        cnn.parametros.Add("@idRegion", cartaFiltro.region.id);
                    }
                    if (cartaFiltro.tipoCarta != null && cartaFiltro.tipoCarta.id > 0)
                    {
                        cnn.parametros.Add("@idTipoCarta", cartaFiltro.tipoCarta.id);
                    }
                    if (cartaFiltro.datum != null && cartaFiltro.datum.id > 0)
                    {
                        cnn.parametros.Add("@idDatum", cartaFiltro.datum.id);
                    }
                    if (cartaFiltro.huso != null && cartaFiltro.huso.id > 0)
                    {
                        cnn.parametros.Add("@idHuso", cartaFiltro.huso.id);
                    }

                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        carta = new Carta();
                        carta.idCarta = Convert.ToInt32(row["idCarta"]);
                        carta.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["nombreRegion"].ToString());
                        carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        carta.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["nombreDatum"].ToString());

                        if (!row.IsNull("idHuso"))
                        {
                            carta.huso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }
                        if (!row.IsNull("numeroCarta"))
                        {
                            carta.numeroCarta = row["numeroCarta"].ToString();
                        }
                        if (!row.IsNull("numEdicion"))
                        {
                            carta.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                        }
                        if (!row.IsNull("anioEdicion"))
                        {
                            carta.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                        }
                        if (!row.IsNull("escala"))
                        {
                            carta.escala = row["escala"].ToString();
                        }
                        if (!row.IsNull("A_A_A"))
                        {
                            carta.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                        }
                        carta.descripcionCarta = row["descripcionCarta"].ToString();

                    }
                }

                return carta;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public Carta ObtieneCartaFiltroDescripcion(Carta cartaFiltro, string campoFiltro)
        {
            try
            {
                Carta carta = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCartaFiltroDescripcion";

                if (campoFiltro != null && !campoFiltro.Equals(""))
                {
                    cnn.parametros.Add("@descripcionCarta", campoFiltro);
                }
                else
                {
                    if (cartaFiltro.idCarta > 0)
                    {
                        cnn.parametros.Add("@idCarta", cartaFiltro.idCarta);
                    }
                    if (cartaFiltro.region != null && cartaFiltro.region.id > 0)
                    {
                        cnn.parametros.Add("@idRegion", cartaFiltro.region.id);
                    }
                    if (cartaFiltro.tipoCarta != null && cartaFiltro.tipoCarta.id > 0)
                    {
                        cnn.parametros.Add("@idTipoCarta", cartaFiltro.tipoCarta.id);
                    }
                    if (cartaFiltro.datum != null && cartaFiltro.datum.id > 0)
                    {
                        cnn.parametros.Add("@idDatum", cartaFiltro.datum.id);
                    }
                    if (cartaFiltro.huso != null && cartaFiltro.huso.id > 0)
                    {
                        cnn.parametros.Add("@idHuso", cartaFiltro.huso.id);
                    }

                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        carta = new Carta();
                        carta.idCarta = Convert.ToInt32(row["idCarta"]);
                        carta.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["nombreRegion"].ToString());
                        carta.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        carta.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["nombreDatum"].ToString());

                        if (!row.IsNull("idHuso"))
                        {
                            carta.huso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }
                        if (!row.IsNull("numeroCarta"))
                        {
                            carta.numeroCarta = row["numeroCarta"].ToString();
                        }
                        if (!row.IsNull("numEdicion"))
                        {
                            carta.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                        }
                        if (!row.IsNull("anioEdicion"))
                        {
                            carta.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                        }
                        if (!row.IsNull("escala"))
                        {
                            carta.escala = row["escala"].ToString();
                        }
                        if (!row.IsNull("A_A_A"))
                        {
                            carta.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                        }
                        carta.descripcionCarta = row["descripcionCarta"].ToString();

                    }
                }

                return carta;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

    }
}
