using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO.MemoryMappedFiles;
using Datos.Entidades;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.antecedentesSector;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using LogicaNegocio.cl.subpesca.rb.common;
using SubPesca.Utilidades;
using System.Configuration;
using System.Globalization;
using LogicaNegocio.cl.subpesca.rb.servicios.acceso;
using System.Reflection;


namespace SubPesca.Mantenedores.PoligonosMasivos
{
    public partial class PoligonosMasivos : System.Web.UI.Page
    {
         //= new Datos.Entidades.Usuario.Serializable(); // Usuario logueado en el sistema

        AntecedentesSectorService antecedentesSectorService = new AntecedentesSectorService();
        SolicitudConcesion Solicitud = null;
        CoordenadaGeografica Coordenada = null;
        Poligono _Poligono = null;
        SolicitudDA _SolucitudDA = new SolicitudDA();
        DatumDA _DatumDA = new DatumDA();
        CartaDA _CartaDA = new CartaDA();
        VerticeDA _VerticeDA = new VerticeDA();
        CoordenadaGeograficaDA _coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        TipoConcesionDA _TipoConcesionDA = new TipoConcesionDA();
        TipoDA tipoDa = new TipoDA();
        PermisosService permisosService = new PermisosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            /* Evaluar si el usuario es valido para ver esta pagina */
            
            if (!Page.IsPostBack)
            {
                Datos.Entidades.Usuario.Serializable usuario_logeado = (Datos.Entidades.Usuario.Serializable)HttpContext.Current.Session["Usuario"];

                ViewState["Usuario"] = usuario_logeado;

                if (!permisosService.tieneAccesoA2(new int[] { rbSeccionUnidadEspacial.Carga_Masiva_Poligonos }, usuario_logeado, null, rbAccion.EDITAR))
                {
                    Response.Redirect("~/Administrador/principal.aspx");
                }
            }
        }

        protected void Descargar_Click(object sender, EventArgs e)
        {
            //string Plantilla_ = "PlanillaPoligonos/PlanillaPoligonosMasivos.xlsx";

            ArchivoBinario PlantillaPoligonos = _coordenadaGeograficaDA.ObtieneArchivoPlanilla(rbAccion.PlanillaPoligonos);

            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = PlantillaPoligonos.formato;
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + PlantillaPoligonos.nombreFisico + "." + PlantillaPoligonos.formato);
                Response.BinaryWrite(PlantillaPoligonos.bytes);
                Response.Flush();
                Response.End();
                
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error, No se pudo realizar la acción');", true);
            }  
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (ArchivoAdjunto.HasFile)
            {
                ArchivoBinario _ArchivoBinario = new ArchivoBinario();

                HttpPostedFile _archivo = ArchivoAdjunto.PostedFile;
                byte[] _byte = new byte[_archivo.InputStream.Length];
                Stream _Stream = ArchivoAdjunto.PostedFile.InputStream;

                using (MemoryStream ms = new MemoryStream())
                {

                    ms.Read(_byte, 0, (int)_byte.Length);
                    using (SpreadsheetDocument wordDoc = SpreadsheetDocument.Open(_Stream, false))
                    {

                        ms.Seek(0, SeekOrigin.Begin);
                        WorkbookPart workbookPart = wordDoc.WorkbookPart;
                        SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().Last();
                        SharedStringTable sst = sstpart.SharedStringTable;

                        WorksheetPart worksheetPart = workbookPart.WorksheetParts.Last();
                        Worksheet sheet = worksheetPart.Worksheet;
                        var cells = sheet.Descendants<Cell>();
                        var rows = sheet.Descendants<Row>();

                        #region constantes
                        string tipocoordenada = ""; 
                        string error = "";
                        
                        string str = "";
                        int identificador = 0;
                        bool tipobusqueda = true; // permite buscar el tipo de pert relacionado
                        bool confirmacion = false;
                        Hashtable _hashtable = new Hashtable();
                        DataTable Para_TipoCon = _TipoConcesionDA.obtenerTipoConcesion(0);
                        List<String> ListaDeErrores = new List<string>();
                        List<ParametroGenerico> Para_Datum = _DatumDA.ListaDatum(0);
                        List<ParametroGenerico> Para_Huso = _coordenadaGeograficaDA.ListaHuso(0);
                        List<ParametroGenerico> Para_TipoConcesion = new List<ParametroGenerico>();
                        List<ParametroGenerico> Para_TipoVertice = tipoDa.ListarTipo("TIPO_VERTICE");
                        ParametroGenerico obj = new ParametroGenerico();
                        Vertice _vertice = null;
                        
                        foreach (DataRow row in Para_TipoCon.Rows)
                        {
                           ParametroGenerico  param = new ParametroGenerico(Convert.ToInt32(row["idTipoConcesion"]), row["TipoConcesion"].ToString());
                           Para_TipoConcesion.Add(param);
                        }
                       
                       // Total celdas 20 de a-z a derecha. 1 = A
                       //
                        #endregion

                        try
                        {

                            foreach (Row row in rows)
                            {
                                foreach (Cell c in row.Elements<Cell>())
                                {
                                    if (c.CellValue != null)
                                    {
                                        string cellvall = c.CellReference.ToString();

                                        int columna = ColumnIndex(c.CellReference);

                                        if (c.CellReference.ToString().Equals("A1") || c.CellReference.ToString().Equals("B1") || c.CellReference.ToString().Equals("C1") || c.CellReference.ToString().Equals("D1") || c.CellReference.ToString().Equals("E1") ||
                                            c.CellReference.ToString().Equals("F1") || c.CellReference.ToString().Equals("G1") || c.CellReference.ToString().Equals("H1") || c.CellReference.ToString().Equals("I1") || c.CellReference.ToString().Equals("J1") ||
                                            c.CellReference.ToString().Equals("K1") || c.CellReference.ToString().Equals("L1") || c.CellReference.ToString().Equals("M1") || c.CellReference.ToString().Equals("N1") || c.CellReference.ToString().Equals("O1") ||
                                            c.CellReference.ToString().Equals("P1") || c.CellReference.ToString().Equals("Q1") || c.CellReference.ToString().Equals("R1") || c.CellReference.ToString().Equals("S1") || c.CellReference.ToString().Equals("T1") ||
                                            c.CellReference.ToString().Equals("U1") || c.CellReference.ToString().Equals("V1"))
                                        {
                                            //son los titulos por ende no deberia realizar nada :(
                                        }
                                        else
                                        {
                                            #region if's
                                            //verificar si el dato str en un pert/identificador.
                                            #region Columna1

                                            if (cellvall[0].Equals('A'))
                                            {
                                                str = valorstr(columna, c, sst);
                                                if (str.Equals("PERT/ Identificador")) // verificamos si es pert o codigo de centro.
                                                {
                                                    tipobusqueda = false;
                                                }
                                                if (str.Equals("Codigo Centro"))
                                                {
                                                    tipobusqueda = true;
                                                }
                                            }
                                            #endregion
                                            #region Columna2
                                            if (cellvall[0].Equals('B'))
                                            {
                                                str = valorstr(columna, c, sst);

                                                identificador = _SolucitudDA.obtenerIdSolicitudFiltro(str, tipobusqueda);
                                                if (identificador == 0)
                                                {
                                                    //indicar error!
                                                    error = " Error!, No existe ese pert/identificador o codigo de centro en el sistema, Celda: " + c.CellReference.ToString() + "\n ";
                                                    ListaDeErrores.Add(error);
                                                    Solicitud = new SolicitudConcesion();
                                                    _hashtable.Add(identificador, Solicitud);
                                                    Solicitud.coordenadaGeografica = new List<CoordenadaGeografica>();

                                                }
                                                else
                                                {
                                                    //verificar si existe ya insertado el identificador en el hash
                                                    if (_hashtable[identificador] == null)
                                                    {
                                                        try
                                                        {
                                                            //ingresamos el pert en el hashtable
                                                            Solicitud = new SolicitudConcesion();
                                                            _hashtable.Add(identificador, Solicitud);
                                                            Solicitud.tipoUnidadEspacial = new ParametroGenerico();
                                                            Solicitud.numPert = str;
                                                            //se cae por no tener tipoUnidadEspacial.id
                                                            SolicitudConcesion Solicitud_obtenertipoConcesion = _SolucitudDA.ObtieneSolicitudConcesion(identificador, 0);
                                                            Solicitud.tipoUnidadEspacial.id = Solicitud_obtenertipoConcesion.tipoUnidadEspacial.id;
                                                            Solicitud.idSolConcesion = identificador;
                                                        }
                                                        catch
                                                        {
                                                            error = "Error pert incorrecto.";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        error = " Error!, Ese pert/identificador o codigo de centro ya fue ingresado, Celda: " + c.CellReference.ToString() + "\n ";
                                                        ListaDeErrores.Add(error);
                                                        Solicitud = new SolicitudConcesion();
                                                    }
                                                }
                                            }
                                            #endregion
                                            //coordenada geografica.
                                            #region Columna3
                                            if (cellvall[0].Equals('C'))
                                            {
                                                if (Solicitud == null)
                                                {
                                                    error = " Error!, NO ingreso pert/identificador o codigo de centro, Celda: " + c.CellReference.ToString();
                                                    ListaDeErrores.Add(error);
                                                    Solicitud = new SolicitudConcesion();
                                                }
                                                str = valorstr(columna, c, sst);
                                                if (TipoCoordenadaGeografica(str) == 0)
                                                {
                                                    error = " Error, debe ingresar un Tipo de coordenada existente en el sistema, Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                                else
                                                {
                                                    tipocoordenada = str;
                                                    if (Solicitud.coordenadaGeografica == null)
                                                    {
                                                        Solicitud.coordenadaGeografica = new List<CoordenadaGeografica>();
                                                        Coordenada = new CoordenadaGeografica();
                                                        Coordenada.tipoCoordgeografica = new ParametroGenerico();
                                                        Coordenada.tipoCoordgeografica.id = TipoCoordenadaGeografica(str);
                                                        Coordenada.idSolConcesion = identificador;
                                                        Coordenada.listaPoligono = new List<Poligono>();
                                                        Coordenada.areaTotalCalculada = 0;
                                                        Solicitud.coordenadaGeografica.Add(Coordenada);
                                                    }
                                                    else
                                                    {
                                                        foreach (CoordenadaGeografica Coor in Solicitud.coordenadaGeografica)
                                                        {
                                                            if (Coor.tipoCoordgeografica.id == TipoCoordenadaGeografica(str))
                                                            {
                                                                error = " Error!, NO puede poner el mismo tipo de coordenada por solicitud, Celda: " + c.CellReference.ToString() + "\n ";
                                                                ListaDeErrores.Add(error);
                                                            }
                                                        }

                                                        Coordenada = new CoordenadaGeografica();
                                                        Coordenada.tipoCoordgeografica = new ParametroGenerico();
                                                        Coordenada.tipoCoordgeografica.id = TipoCoordenadaGeografica(str);
                                                        Coordenada.idSolConcesion = identificador;
                                                        Coordenada.listaPoligono = new List<Poligono>();
                                                        Coordenada.areaTotalCalculada = 0;
                                                        Solicitud.coordenadaGeografica.Add(Coordenada);  
                  
                                                    }
                                                    if (Coordenada.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO)
                                                    {
                                                        Solicitud.reqAntecTerreno = true;
                                                    }
                                                    if (Coordenada.tipoCoordgeografica.id == rbTipo.REGULARIZACION)
                                                    {
                                                        Solicitud.reqRegularizacion = true;
                                                    }
                                                }
                                            }
                                            #endregion
                                            #region Columna4
                                            if (cellvall[0].Equals('D'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    List<Carta> List_Carta = _CartaDA.ListaCartaDA(null, " ");
                                                    Carta _Carta = new Carta();
                                                    _Carta = List_Carta.Find(item => item.descripcionCarta.Equals(str));
                                                    Coordenada.carta = new Carta();
                                                    Coordenada.carta.idCarta = _Carta.idCarta;
                                                }
                                                catch (Exception ex)
                                                {
                                                    error = " Error!, No Existe esa carta, Celda: " + c.CellReference.ToString() + "\n ";
                                                    ListaDeErrores.Add(error);
                                                    Coordenada.carta = new Carta();
                                                }
                                                //error con los nulls consultar, debido a que esto sale directamente de la base de datos
                                            }
                                            #endregion
                                            #region Columna5
                                            if (cellvall[0].Equals('E'))
                                            {
                                                str = valorstr(columna, c, sst);
                                                if (str.Equals("Si"))
                                                {
                                                    Coordenada.aplicaBanco = true;
                                                }
                                                else
                                                {
                                                    Coordenada.aplicaBanco = false;
                                                }
                                            }

                                            #endregion
                                            #region Columna6
                                            if (cellvall[0].Equals('F'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    obj = Para_Datum.Find(item => item.descripcion.Equals(str));
                                                    Coordenada.datum = new ParametroGenerico();
                                                    Coordenada.datum.id = obj.id;
                                                }
                                                catch
                                                {
                                                    error = " NO existe ese Datum Celda: " + cellvall;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }

                                            #endregion
                                            #region Columna7
                                            if (cellvall[0].Equals('G'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    obj = Para_Huso.Find(item => item.descripcion == str);
                                                    Coordenada.tipoHuso = new ParametroGenerico();
                                                    Coordenada.tipoHuso.id = obj.id;

                                                }
                                                catch
                                                {
                                                    error = " NO existe ese Huso Celda: " + cellvall;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            //N° Poligono.
                                            #region Columna8
                                            if (cellvall[0].Equals('H'))
                                            {
                                                _Poligono = new Poligono();
                                                _Poligono.lista_vertices = new List<Vertice>();
                                                Coordenada.listaPoligono.Add(_Poligono);
                                                _Poligono.idSolicitud = identificador;
                                                _Poligono.estado = new ParametroGenerico(rbEstadosGenerales.VIGENTE);

                                                if (Coordenada.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO)
                                                {
                                                    _Poligono.idCoordenadaGeo = rbTipo.ANTECEDENTES_TERRENO;
                                                }
                                                if (Coordenada.tipoCoordgeografica.id == rbTipo.REGULARIZACION)
                                                {
                                                    _Poligono.idCoordenadaGeo = rbTipo.REGULARIZACION;
                                                    //_Poligono.
                                                }
                                                if (tipocoordenada.Equals("Coordenadas 14 TER") && (Coordenada.tipoHuso == null))
                                                {
                                                    //error no puede existir un 14 ter sin Huso
                                                    error = " Error!, Coordenadas 14 ter necesitan tener Huso. Celda: " + c.CellReference.ToString() + "\n ";
                                                    ListaDeErrores.Add(error);
                                                }
                                                if (tipocoordenada.Equals("Coordenadas 14 TER") && (Coordenada.datum == null))
                                                {
                                                    //error no puede existir un 14ter sin Datum :(
                                                    error = " Error!, Coordenadas 14 ter necesitan tener datum. Celda: " + c.CellReference.ToString() + "\n ";
                                                    ListaDeErrores.Add(error);
                                                }
                                                if ((tipocoordenada.Equals("Coordenadas Originales") || tipocoordenada.Equals("Coordenadas 14 TER")) && (Coordenada.aplicaBanco == false))
                                                {
                                                    //error ellos necesitan banco asociado.
                                                    error = " Error!, Coordenadas Originales y 14 ter necesitan tener un banco. Celda: " + c.CellReference.ToString() + "\n ";
                                                    ListaDeErrores.Add(error);
                                                }
                                                //con esta solicitud verificaremos si la solicitud y la coordenada geografica ya estan ingresadas anteriormente en la bdd
                                                CoordenadaGeografica CoordenadaGeoVerificacion = new CoordenadaGeografica();
                                                CoordenadaGeoVerificacion = _coordenadaGeograficaDA.ObtieneCoordenadaGeografica(identificador, Coordenada.idCoordenadaGeo);
                                                if (CoordenadaGeoVerificacion != null)
                                                {
                                                    error = " Ya existe la coordenada Geogragica " + tipocoordenada + " asociada al Identificador: " + identificador;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna9
                                            if (cellvall[0].Equals('I'))
                                            {
                                                //_Poligono.tipoConcesion
                                                str = valorstr(columna, c, sst);
                                                String[] ListaTipoConcesion = str.Split(',');
                                                for (int a = 0; a < ListaTipoConcesion.Length; a++)
                                                {
                                                    string valor = ListaTipoConcesion[a].Trim();

                                                    if (valor.Equals("PLAYA"))
                                                    {
                                                        valor = "PLAYA ";
                                                    }

                                                    obj = Para_TipoConcesion.Find(item => item.descripcion.Equals(valor));
                                                    //agregar la lista de tipo de concesion
                                                    if (obj != null)
                                                    {
                                                        if (_Poligono.tipoConcesion != null)
                                                        {
                                                            _Poligono.tipoConcesion.Add(obj);
                                                        }
                                                        else
                                                        {
                                                            _Poligono.tipoConcesion = new List<ParametroGenerico>();
                                                            _Poligono.tipoConcesion.Add(obj);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        error = " Error, Debe ingresar un tipoConcesion correcto Celda: " + c.CellReference;
                                                    }
                                                }
                                            }
                                            #endregion
                                            #region Columna10
                                            if (cellvall[0].Equals('J'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    _Poligono.tipoUso = new ParametroGenerico();
                                                    if (TipoUso(str) == 0)
                                                    {
                                                        error = " Error, Debe ingresar un tipoUso correcto: " + c.CellReference;
                                                        ListaDeErrores.Add(error);
                                                    }
                                                    else
                                                    {
                                                        _Poligono.tipoUso.id = TipoUso(str);
                                                    }
                                                }
                                                catch
                                                {
                                                    error = " Debe Contener Un Tipo Uso correcto, Celda: " + cellvall;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna11
                                            if (cellvall[0].Equals('K'))
                                            {
                                                str = valorstr(columna, c, sst);
                                                _Poligono.toponimio = str;
                                            }
                                            #endregion
                                            #region Columna12
                                            if (cellvall[0].Equals('L'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Replace(',', '.');


                                                    if (Coordenada.tipoCoordgeografica.id == 41)
                                                    {
                                                        float number = float.Parse(str, CultureInfo.InvariantCulture); //float.Parse("0.54", culture);
                                                        _Poligono.areaRegularizacion = float.Parse(str, CultureInfo.InvariantCulture);
                                                        _Poligono.areaSolicitada = 0;
                                                        Coordenada.areaTotalCalculada = 0;
                                                        Coordenada.areaTotalSolicitada = 0;
                                                    }
                                                    else
                                                    {
                                                            _Poligono.areaSolicitada = float.Parse(str, CultureInfo.InvariantCulture);
                                                            _Poligono.areaCalculada = float.Parse(str, CultureInfo.InvariantCulture);
                                                            Coordenada.areaTotalRegularizacion = 0;
                                                            Coordenada.areaTotalSolicitada = 0;
                                                            Coordenada.areaTotalCalculada = 0;
                                                    }
                                                }
                                                catch
                                                {
                                                    error = " Error al ingresar AreaSolicitada/AreaRegularizacion, Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna13
                                            if (cellvall[0].Equals('M'))
                                            {
                                                try
                                                {
                                                    if (Coordenada.tipoCoordgeografica.id == 41)
                                                    {
                                                        _Poligono.areaCalculada = 0;
                                                        Coordenada.areaTotalCalculada = 0;
                                                    }
                                                    else
                                                    {
                                                        str = valorstr(columna, c, sst);
                                                        str = str.Replace(',', '.');
                                                        _Poligono.areaCalculada = float.Parse(str, CultureInfo.InvariantCulture);
                                                        //Coordenada.areaTotalCalculada = float.Parse(str, CultureInfo.InvariantCulture);

                                                    }
                                                }
                                                catch
                                                {
                                                    error = " Error ingresando area calculada, Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna14
                                            if (cellvall[0].Equals('N'))
                                            {
                                                //aqui vemos el tema de los vertices.
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Trim();
                                                    _vertice = new Vertice();
                                                    _Poligono.lista_vertices.Add(_vertice);
                                                    obj = Para_TipoVertice.Find(item => item.descripcion.Equals(str));
                                                    _vertice.vertice = new ParametroGenerico();
                                                    _vertice.vertice.id = obj.id;
                                                }
                                                catch(Exception ex)
                                                {
                                                    error = " El vertice no contiene Denominacion Vertice Celda: " + cellvall;
                                                    ListaDeErrores.Add(error);
                                                }
                                                if (_Poligono.tipoUso == null)
                                                {
                                                    error = " El campo Uso es Obligatorio" + cellvall;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna15
                                            if (cellvall[0].Equals('O'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    _vertice.latitudHora = Convert.ToInt32(str);
                                                }
                                                catch
                                                {
                                                    error = " Error Con los vertices Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna16
                                            if (cellvall[0].Equals('P'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    _vertice.latitudMinuto = Convert.ToInt32(str);
                                                }
                                                catch
                                                {
                                                    error = "Error Con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna17
                                            if (cellvall[0].Equals('Q'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Replace(',', '.');
                                                    _vertice.latitudSegundo = Convert.ToDouble(str, CultureInfo.InvariantCulture);
                                                }
                                                catch
                                                {
                                                    error = " Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna18
                                            if (cellvall[0].Equals('R'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    _vertice.longitudHora = Convert.ToInt32(str);
                                                }
                                                catch
                                                {
                                                    error = " Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna19
                                            if (cellvall[0].Equals('S'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    _vertice.longitudMinuto = Convert.ToInt32(str);
                                                }
                                                catch
                                                {
                                                    error = " Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna20
                                            if (cellvall[0].Equals('T'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Replace(',', '.');
                                                    _vertice.longitudSegundo = Convert.ToDouble(str, CultureInfo.InvariantCulture);
                                                }
                                                catch
                                                {
                                                    error = " Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna21
                                            if (cellvall[0].Equals('U'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Replace(',' , '.');
                                                    _vertice.utmE = Convert.ToDouble(str, CultureInfo.InvariantCulture);
                                                }
                                                catch
                                                {
                                                    error = "Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #region Columna22
                                            if (cellvall[0].Equals('V'))
                                            {
                                                try
                                                {
                                                    str = valorstr(columna, c, sst);
                                                    str = str.Replace(',', '.');
                                                    _vertice.utmN = Convert.ToDouble(str, CultureInfo.InvariantCulture);
                                                }
                                                catch
                                                {
                                                    error = "Error con el vertice Celda: " + c.CellReference;
                                                    ListaDeErrores.Add(error);
                                                }
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }


                                }//foreach cell

                            }//foreach row
                        }
                        catch 
                        {
                            error = "Error formato en el excel";
                        }

                        //Validaciones
                        if (error.Length > 0)
                        {
                            foreach (String Problemas in ListaDeErrores)
                            {
                            Page.Validators.Add(new ValidationError("grupo1", Problemas));
                            }
                        }
                        else
                        {
                            //si no hay errores guardar la informacion
                            #region GuardarSolicitudes
                            /*
                             guardamos datos en archivobinario para enviar al metodo de guardado
                             */
                            /********/
                            Datos.Entidades.Usuario.Serializable usuario_logeado = (Usuario.Serializable)ViewState["Usuario"];
                            /********/
                            
                            _ArchivoBinario.usuario = new Usuario();
                            _ArchivoBinario.archivo = ArchivoAdjunto.PostedFile;
                            _ArchivoBinario.usuario.id_usuario = usuario_logeado.id_usuario;
                            _ArchivoBinario.nombreArchivo = NombrePlantilla.Text;
                            _ArchivoBinario.nombreFisico = ArchivoAdjunto.PostedFile.FileName.Substring(0, ArchivoAdjunto.PostedFile.FileName.LastIndexOf("."));
                            _ArchivoBinario.formato = ArchivoAdjunto.PostedFile.FileName.Substring(ArchivoAdjunto.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                            _ArchivoBinario.tamano = ArchivoAdjunto.PostedFile.InputStream.Length;
                            _ArchivoBinario.bytes = ArchivoAdjunto.FileBytes;

                            confirmacion = antecedentesSectorService.GuardarPlanilla_Datos(_hashtable, usuario_logeado.id_usuario, _ArchivoBinario, Session["Usuario"] == null ? 0 : ((Datos.Entidades.Usuario.Serializable)Session["Usuario"]).id_usuario);
                            if (!confirmacion)
                            {
                                error = "Error no se pudo Guardar la planilla";
                                ErrorLabel.Visible = true;
                                ErrorLabel.Text = error;
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Poligonos Ingresados." + "');", true);
                                //enviar pop-up todo cambio fue realizado con exito.
                            }
                            #endregion
                        }
                    }
                }//using spreadsheetdocument
            }//using memorystream
         }


        int ColumnIndex(string reference) 
        { 
            int ci=0; 
            reference=reference.ToUpper(); 
            for (int ix = 0; ix < reference.Length && reference[ix] >= 'A';ix++ )
                ci = (ci * 26) + ((int)reference[ix] - 64); 
            
            return ci; 
        }

        string valorstr(int index, Cell c, SharedStringTable sst)
        {
            string datocelda = "";
            if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
            {
                int ssid = int.Parse(c.CellValue.Text);
                datocelda = sst.ChildElements[ssid].InnerText;
            }
            else if (c.CellValue != null)
            {
                datocelda = c.CellValue.Text;
            }
            return datocelda;
        }

        int TipoCoordenadaGeografica(string str)
        {
            if (str.Equals("Coordenadas Originales"))
            {
                return rbTipo.ANTECEDENTES_ESPACIALES;
            }
            else if (str.Equals("Coordenadas 14 TER"))
            {
                return rbTipo.ANTECEDENTES_TERRENO;
            }
            else if (str.Equals("Regularización"))
            {
                return rbTipo.REGULARIZACION;
            }
            else
            {
                return 0;
            }
        }

        int TipoUso(string str)
        {
            if (str.Equals("Cultivo"))
            {
                return 16;
            }
            if (str.Equals("Apoyo")) 
            {
                return 17;
            }
            return 0;
            
        }
     

    }
}