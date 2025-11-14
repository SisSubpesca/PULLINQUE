using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.common;
using System.IO;
using System.Configuration;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using LogicaNegocio.cl.subpesca.rb.doc_planilla;
using DocumentFormat.OpenXml;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using LogicaNegocio.cl.subpesca.rb.visacion;
using SubPesca.Utilidades;

namespace SubPesca.Solicitudes.GeneradorDocumental
{
    public partial class GeneradorDocumental : System.Web.UI.Page
    {
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        DocPlanillaDA _DocPlanillaDA = new DocPlanillaDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        PermisosService permisosService = new PermisosService();
        VisacionDA _VisacionDA = new VisacionDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];
                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.GeneradorDocumental }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }

                Cargar_combobox();
            }
        }
        protected void Cargar_combobox()
        {

            UnidadEsp.Items.Clear();
            UnidadEsp.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" -- Seleccione Unidad Espacial --","0"));
            UnidadEsp.DataSource = parametroGenericoDA.ListarTipoTramiteEstadoSolicitud(0);
            UnidadEsp.DataTextField = "nombreTipoInterfaz";
            UnidadEsp.DataValueField = "idTipoTramite";
            UnidadEsp.DataBind();
            UpdatePanelUnidadEsp.Update();

            UnidadEsp.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" -- Seleccione Unidad Espacial --", "0"));
            UnidadEsp.SelectedIndex = 0;


            TipoDoc.Items.Clear();
            TipoDoc.DataBind();
            TipoDoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "0"));

        }    
       
        protected void UnidadEsp_changed(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(UnidadEsp.SelectedValue);
            
            //llenar la tabla.
            TipoDoc.Items.Clear();
            TipoDoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Documento --","0"));
            TipoDoc.DataSource = _DocPlanillaDA.ListarDocPlanillaFiltro(0,Index);
            TipoDoc.DataTextField = "nombreDocPlanilla";
            TipoDoc.DataValueField = "idDocPlanilla";
            TipoDoc.DataBind();
            UpdatePanelDoc.Update();

            TipoDoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "0"));
            TipoDoc.SelectedIndex = 0;
        }


        protected void Generar_Doc(object sender, EventArgs e)
        {
            string _Identificador = "";
            int _TipoDoc = 0;
            //bool Correcto = true;
            string Error = "";
            List<String> ListaErrores = new List<String>();

            if (!Identificador.Text.Trim().Equals(""))
            {
                _Identificador = Identificador.Text.ToString();
            }
            else
            {
                Error = "Debe Poner un Pert/Identificador";
                ListaErrores.Add(Error);
            }

            VisacionMasiva visacion = new VisacionMasiva();
            visacion.pertFiltro = _Identificador;
            visacion.tipoTramite = new ParametroGenerico();
            visacion.tipoTramite.id = Convert.ToInt32(UnidadEsp.SelectedValue);

            string mensaje = "";
            mensaje = _VisacionDA.ValidarExistenciaPerts(visacion);
            if (mensaje != "")
            {
                Error = "El pert y el tipo de solicitud no concuerdan";
                ListaErrores.Add(Error);
            }

            if (Convert.ToInt32(TipoDoc.SelectedValue) > 0)
            {
                _TipoDoc = Convert.ToInt32(TipoDoc.SelectedValue);
            }
            else
            {
                Error = "Debe Seleccionar un Documento";
                ListaErrores.Add(Error);
            }


            if (ListaErrores.Count > 0)
            {
                //error X(
                foreach (String error in ListaErrores)
                {
                    Page.Validators.Add(new ValidationError("grupo1", error));
                }
            }
            else
            {
                GenerarDoc(_Identificador, _TipoDoc);
            }
            
        }

        protected void GenerarDoc(string Npert,int idDoc)
        {

            ParametroGenerico FuenteDocumento = parametroGenericoDA.ObtenerParametro("FuenteGenDocumental");

            DocPlanilla _DocPlanilla = _DocPlanillaDA.ObtenerDocPlanilla(idDoc);

            int solicitud = solicitudDA.obtieneSolicitudIdPert(Npert);
            byte[] wordcontent = _DocPlanilla.bytes;
            //try
            //{
            using (MemoryStream memorystream = new MemoryStream())
            {
                memorystream.Write(wordcontent, 0, (int)wordcontent.Length);
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(memorystream, true))
                {
                    //tomamos el cuerpo del documento
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    //creamos un elemento de paragrafos
                    var paras = body.Elements<Paragraph>();
                    //recorremos los paragrafos del documento
                    foreach (var para in paras)
                    {
                        //a su vez recorremos los run dentro del XML
                        foreach (var run in para.Elements<Run>())
                        {
                            string font = run.RunProperties.RunFonts.Ascii.ToString();

                            //y por ultimo recorremos el texto.
                            foreach (var text in run.Elements<Text>())
                            {
                                /* iniciamos el color negro para que si algun momento existiera
                                un cambio de color no nos imprima todas las demas variables con color rojo */

                                RunProperties Runpro = new RunProperties();
                                Color color = new Color() { Val = "#000000" };
                                run.Append(color);

                                //posiblemente a modificacion, dejando una pestaña de abrir etiqueta y otra para cerrar
                                Regex regexText = new Regex("REM_(.*)");

                                 
                                Match r = Regex.Match(text.Text.ToString(), "REM_(.*)");

                                string tag;
                                if (r.Success)
                                {
                                    tag = r.Value.ToString();
                                    try
                                    {
                                        DataTable DT = new DataTable();
                                        DT = _DocPlanillaDA.ObtenerReemplazoPlanilla(solicitud, tag);
                                        //vemos si Datatalbe tiene data
                                        if (DT != null && DT.Rows.Count > 0)
                                        {
                                            //para identificar si la tabla era un solo dato o una tabla en si
                                            if (DT.Rows[0][0].ToString().Equals("1"))
                                            {
                                                #region CodigoReemplazoString
                                                //cambio cuando es solo un texto

                                                string prueba = DT.Rows[0][1].ToString();

                                                if (!prueba.Equals("") && prueba != null)
                                                {
                                                    text.Text = regexText.Replace(tag, DT.Rows[0][1].ToString());
                                                }
                                                else
                                                {
                                                    text.Text = regexText.Replace(tag, " ");
                                                    Text _text = new Text("Sin Informacion");
                                                    color = new Color() { Val = "#FF0000" };
                                                    Runpro.Append(color);
                                                    Runpro.Append(_text);
                                                    run.Append(Runpro);
                                                }
                                                #endregion
                                            }
                                            // si no era el valor anterior era tabla
                                            else
                                            {

                                                    #region TablaXML
                                                    DocumentFormat.OpenXml.Wordprocessing.Table tabla = new DocumentFormat.OpenXml.Wordprocessing.Table();
                                                    
                                                    //Eliminamos la columna de tipo despliegue para no mostrarla dentro de la tabla, como es la primera se 
                                                    //saca la columna 0.-
                                                    DT.Columns.Remove(DT.Columns[0]);

                                                    #region PropiedadesTabla
                                                    TableProperties propiedades = new TableProperties
                                                    (
                                                        new TableBorders
                                                        (
                                                            new TopBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            },
                                                            new BottomBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            },
                                                            new LeftBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            },
                                                            new RightBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            },
                                                            new InsideHorizontalBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            },
                                                            new InsideVerticalBorder
                                                            {
                                                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                                                Size = 10
                                                            } 
                                                        ),
                                                        //esta propiedad centra la tabla ojo no ponerla dentro tableborders ya que no funcionara.
                                                        new TableJustification() 
                                                            {
                                                                Val = TableRowAlignmentValues.Center
                                                            }
                                                    );
                                                    #endregion
                                                    tabla.AppendChild<TableProperties>(propiedades);
                                                    DataRow dr = DT.Rows[0];
                                                    //recorremos la DataTable para llenar la tabla de word
                                                    //recorrido por sus columnas
                                                    #region PoblacionTabla
                                                    for (var i = 0; i < DT.Rows.Count; i++)
                                                    {
                                                        var tr = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                                                        //recorrido por sus rows
                                                        for (var j = 0; j < DT.Columns.Count; j++)
                                                        {
                                                            var tc = new DocumentFormat.OpenXml.Wordprocessing.TableCell();
                                                                //imprimir
                                                            Paragraph para2 = new Paragraph();
                                                            Run run2 = new Run();
                                                            RunFonts font1 = new RunFonts();
                                                            if (font == null)
                                                            {
                                                                font1 = new RunFonts() { Ascii = font };
                                                            }
                                                            else
                                                            {
                                                                font1 = new RunFonts() { Ascii = FuenteDocumento.descripcion };
                                                            }
                                                            run2.Append(font1);
                                                            Text text2 = new Text();

                                                            text2.Text = DT.Rows[i][j].ToString(); 
   
                                                            //tc.Append(new Paragraph(new Run(new Text(text.Text))));
                                                            run2.Append(text2);
                                                            para2.Append(run2);
                                                            tc.Append(para2);

                                                            // asume el sistema que las columnas seran autoajustadas.
                                                            tc.Append(new TableCellProperties(
                                                                new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                                            tr.Append(tc);
                                                        }
                                                        tabla.Append(tr);
                                                    }
                                                    #endregion

                                                    if (tabla.InnerText.Equals("") && tabla.InnerText != null)
                                                    {
                                                        text.Text = regexText.Replace(tag, " ");
                                                        Text _text = new Text("Sin Informacion");
                                                        color = new Color() { Val = "#FF0000" };
                                                        Runpro.Append(color);
                                                        Runpro.Append(_text);
                                                        run.Append(Runpro);
                                                    }
                                                    else
                                                    {

                                                        var run2 = new Run(tabla);
                                                        para.RemoveAllChildren();
                                                        ParagraphProperties paraProp = new ParagraphProperties();
                                                        Justification CenterHeading = new Justification() { Val = JustificationValues.Center };
                                                        paraProp.Append(CenterHeading);
                                                        para.Append(paraProp);
                                                        para.Append(run2);
                                                    }
                                                #endregion
                                            }
                                        }
                                        /* Se encontro el match pero no la data, se reemplaza el texto por un vacio y luego se imprime el mismo tag 
                                         * pero cambiandole el color de este a rojo, para ello se agregan propiedades al run properties para cambiar el color
                                         * de este #000000
                                         */
                                        else
                                        {
                                            #region CambioColor
                                            text.Text = regexText.Replace(tag, " ");
                                            Text _text = new Text("Sin Informacion");
                                            color = new Color() { Val = "#FF0000" };
                                            Runpro.Append(color);
                                            Runpro.Append(_text);
                                            run.Append(Runpro);
                                            #endregion
                                        }
                                    }
                                    catch(Exception e)
                                    {
                                        //si la DT nos arroja un error imprimimos el tag de un color amarillo
                                        //para indicar que es un error como no existe el tag.
                                        #region CambioColor
                                        text.Text = regexText.Replace(tag, " ");
                                        Text _text = new Text(tag);
                                        color = new Color() { Val = "#FFFF00" };
                                        Runpro.Append(color);
                                        Runpro.Append(_text);
                                        run.Append(Runpro);
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
                //Descarga del archivo 
                byte[] DocumentoModificado = memorystream.ToArray();
                Descarga(DocumentoModificado, _DocPlanilla);
                memorystream.Close();
            }
            //}
            //catch
            //{

            //}

        }

        protected void Descarga(byte[] file, DocPlanilla _DocPlanilla)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ".docx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + _DocPlanilla.nombreDocPlanilla + "." + "docx");
            Response.BinaryWrite(file);
            Response.Flush();
            Response.End();
            Response.Close();
        }  
    }
}