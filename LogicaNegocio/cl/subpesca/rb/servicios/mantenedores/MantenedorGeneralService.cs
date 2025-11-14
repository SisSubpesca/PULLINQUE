using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using Datos.Entidades.Resolucion;

namespace SubPesca.Mantenedores.Generales
{
    public class MantenedorGeneralService
    {
        
        Logger logger = new Logger();
        
        MantenedorDA mantenedorDA = new MantenedorDA();
        RegionDA regionDA = new RegionDA();
        BarrioDA barrioDA = new BarrioDA();
        RequerimientoDA requerimientoDA = new RequerimientoDA();
        ParametroGenericoDA parametroGenericoDA = new ParametroGenericoDA();
        TemplateAvisoDA templateAvisoDA = new TemplateAvisoDA();
        SolicitudDA solicitudDA = new SolicitudDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();
        


        public List<Region> listarRegiones(Datos.Entidades.Region regionFiltro)
        {
            return mantenedorDA.ListarRegion(regionFiltro.id_region, regionFiltro.codigo, regionFiltro.region);
        }

        public System.Data.DataTable guardarRegion(Datos.Entidades.Region region)
        {
            return mantenedorDA.GuardarRegion_Mantenedor(region.id_region,region.codigo, region.region);
        }

        public System.Data.DataTable eliminarRegion(int id)
        {
            return mantenedorDA.EliminarRegion_Mantenedor(id);
        }

        public System.Data.DataTable actualizarRegion(Region region)
        {
            return mantenedorDA.GuardarRegion_Mantenedor(region.id_region,region.codigo, region.region);
        }

        public System.Data.DataTable guardarMacrozona(Macrozona macrozona)
        {
            return mantenedorDA.GuardarMacrozona_Mantenedor(macrozona.id_macrozona, macrozona.macrozona, macrozona.regionMacrozona.id);
        }

        public List<Macrozona> listarMacrozona(Macrozona macrozonaFiltro)
        {
            /*
            List<ParametroGenerico> listaParametroGenerico = mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro.id_macrozona);
            if(listaParametroGenerico != null && listaParametroGenerico.Count > 0){
                
                List<Macrozona> listaMacrozona = new List<Macrozona>();
                Macrozona macrozona = null;
                foreach(ParametroGenerico parametroGenerico in listaParametroGenerico){
                    macrozona = new Macrozona();
                    macrozona.id_macrozona = parametroGenerico.id;
                    macrozona.macrozona = parametroGenerico.descripcion;
                    listaMacrozona.Add(macrozona);
                }
                return listaMacrozona;
            }
            return null;
            */
            //return mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro.id_macrozona, macrozonaFiltro.id_region);
            return mantenedorDA.ListarMacrozona_Mantenedor(macrozonaFiltro);
        }

        public System.Data.DataTable actualizarMacrozona(Macrozona macrozona)
        {
            return mantenedorDA.GuardarMacrozona_Mantenedor(macrozona.id_macrozona, macrozona.macrozona, macrozona.regionMacrozona.id);
        }

        public System.Data.DataTable eliminarMacrozona(int id)
        {
            return mantenedorDA.EliminarMacrozona_Mantenedor(id);
        }

        public System.Data.DataTable guardarHuso(ParametroGenerico huso)
        {
            return mantenedorDA.GuardarHuso_Mantenedor(huso.id, huso.descripcion);
        }

        public List<ParametroGenerico> listarHuso(ParametroGenerico husoFiltro)
        {
            //return mantenedorDA.ListarHuso_Mantenedor(husoFiltro.id);
            return mantenedorDA.ListarHuso_Mantenedor(husoFiltro);
        }

        public System.Data.DataTable actualizarHuso(ParametroGenerico huso)
        {
            return mantenedorDA.GuardarHuso_Mantenedor(huso.id, huso.descripcion);
        }

        public System.Data.DataTable eliminarHuso(int id)
        {
            return mantenedorDA.EliminarHuso_Mantenedor(id);
        }

        public System.Data.DataTable guardarTipoConcesion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoConcesion_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public List<ParametroGenerico> listarTipoConcesion(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipoConcesion_Mantenedor(parametroGenericoFiltro);
        }

        public System.Data.DataTable actualizarTipoConcesion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoConcesion_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public System.Data.DataTable eliminarTipoConcesion(int id)
        {
            return mantenedorDA.EliminarTipoConcesion_Mantenedor(id);
        }

        public System.Data.DataTable guardarFormaEstructura(ParametroGenerico formaEstructura)
        {
            return mantenedorDA.GuardarTipoFormaEstructura_Mantenedor(formaEstructura.id, formaEstructura.descripcion);
        }

        public List<ParametroGenerico> listarFormaEstructura(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipoFormaEstructura_Mantenedor(parametroGenericoFiltro);
        }

        public System.Data.DataTable actualizarFormaEstructura(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoFormaEstructura_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public System.Data.DataTable eliminaTipoFormaEstructura(int id)
        {
            return mantenedorDA.EliminarTipoFormaEstructura_Mantenedor(id);
        }

        public DataTable guardarProvincia(Provincia provincia)
        {
            return mantenedorDA.GuardarProvincia_Mantenedor(provincia);
        }

        public List<Provincia> listarProvincia(Provincia provinciaFiltro)
        {
            //return mantenedorDA.ListarProvincia_Mantenedor(provinciaFiltro.id_provincia, provinciaFiltro.id_region);
            return mantenedorDA.ListarProvincia_Mantenedor(provinciaFiltro);
        }

        public DataTable actualizarProvincia(Provincia provincia)
        {
            return mantenedorDA.GuardarProvincia_Mantenedor(provincia);
        }

        public DataTable eliminarProvincia(int id)
        {
            return mantenedorDA.EliminarProvincia_Mantenedor(id);
        }

        public DataTable guardarBarrio(Barrio barrio)
        {
            return mantenedorDA.GuardarBarrio_Mantenedor(barrio);
        }

        public List<Barrio> listarBarrio(Barrio barrioFiltro)
        {
            return mantenedorDA.ListarBarrio_Mantenedor(barrioFiltro);
        }

        public DataTable actualizarBarrio(Barrio barrio)
        {
            return mantenedorDA.GuardarBarrio_Mantenedor(barrio);
        }

        public DataTable eliminarBarrio(int id)
        {
            return mantenedorDA.EliminarBarrio_Mantenedor(id);
        }

        public List<ParametroGenerico> listarParametroGenerico(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable actualizarTipo(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarTipo(int id)
        {
            return mantenedorDA.EliminarTipo_Mantenedor(id);
        }

        public DataTable guardarCapitaniaPuerto(CapitaniaDePuerto capitaniaDePuerto)
        {
            return mantenedorDA.GuardarCapitaniaPuerto_Mantenedor(capitaniaDePuerto);
        }

        public List<CapitaniaDePuerto> listarCapitaniaPuerto(CapitaniaDePuerto capitaniaDePuertoFiltro)
        {
            //return mantenedorDA.ListarCapitaniaPuerto_Mantenedor(capitaniaDePuertoFiltro.idCapitaDePuerto);
            return mantenedorDA.ListarCapitaniaPuerto_Mantenedor(capitaniaDePuertoFiltro);
        }

        public DataTable actualizarCapitaniaPuerto(CapitaniaDePuerto capitaniaDePuerto)
        {
            return mantenedorDA.GuardarCapitaniaPuerto_Mantenedor(capitaniaDePuerto);
        }

        public DataTable eliminarCapitaniaPuerto(int id)
        {
            return mantenedorDA.EliminarCapitaniaPuerto_Mantenedor(id);
        }

        public DataTable guardarComuna(Comuna comuna)
        {
            return mantenedorDA.GuardarComuna_Mantenedor(comuna);
        }

        public List<Comuna> listarComuna(Comuna comunaFiltro)
        {
            //return mantenedorDA.ListarComuna_Mantenedor(comunaFiltro.id_comuna, comunaFiltro.id_provincia);
            return mantenedorDA.ListarComuna_Mantenedor(comunaFiltro);
        }

        public DataTable actualizarComuna(Comuna comuna)
        {
            return mantenedorDA.GuardarComuna_Mantenedor(comuna);
        }

        public DataTable eliminarComuna(int id)
        {
            return mantenedorDA.EliminarComuna_Mantenedor(id);
        }

        public DataTable guardarEspecie(Especies especies)
        {
            return mantenedorDA.GuardarEspecieCultivo_Mantenedor(especies);
        }

        public List<Especies> listarEspecies(Especies especieFiltro)
        {
            //return mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro.id_especie, especieFiltro.grupoEspecie.id_grupoEspecie);
            return mantenedorDA.ListarEspecieCultivo_Mantenedor(especieFiltro);
        }

        public DataTable eliminarEspecie(int id)
        {
            return mantenedorDA.EliminarEspecieCultivo_Mantenedor(id);
        }

        public DataTable actualizarEspecies(Especies especies)
        {
            return mantenedorDA.GuardarEspecieCultivo_Mantenedor(especies);
        }

        public DataTable guardarEstructuraTecnica(EstructuraTecnica estructuraTecnica)
        {
            return mantenedorDA.GuardarEstructuraTecnica_Mantenedor(estructuraTecnica);
        }

        public List<EstructuraTecnica> listarEstructuraTecnica(EstructuraTecnica estructuraTecnicaFiltro)
        {
            //return mantenedorDA.ListarEstructuraTecnica_Mantenedor(estructuraTecnicaFiltro.idEstructura);
            return mantenedorDA.ListarEstructuraTecnica_Mantenedor(estructuraTecnicaFiltro);
        }

        public DataTable actualizarEstructuraTecnica(EstructuraTecnica estructuraTecnica)
        {
            return mantenedorDA.GuardarEstructuraTecnica_Mantenedor(estructuraTecnica);
        }

        public DataTable eliminarEstructuraTecnica(int id)
        {
            return mantenedorDA.EliminarEstructuraTecnica_Mantenedor(id);
        }

        public DataTable guardarEtapaDeDesarrollo(EtapaCultivo etapaCultivo)
        {
            //return mantenedorDA.GuardarEtapaCultivo_Mantenedor(etapaCultivo.id_etapaDesarrollo, etapaCultivo.nombreEtapaDesarrollo, etapaCultivo.idEspecie);
            //return mantenedorDA.GuardarEtapaCultivo_Mantenedor(etapaCultivo);
            return mantenedorDA.GuardarEtapaDesarrollo_Mantenedor(etapaCultivo);
        }

        //public List<EtapaCultivo> listarEtapaCultivo(ParametroGenerico etapaCultivoFiltro)
        //{

        //    //List<ParametroGenerico> listaParametricoGenerico = mantenedorDA.ListarEtapaCultivo_Mantenedor(etapaCultivoFiltro.id_etapaDesarrollo);
        //    List<ParametroGenerico> listaParametricoGenerico = mantenedorDA.ListarEtapaCultivo_Mantenedor(etapaCultivoFiltro);
            
        //    if(listaParametricoGenerico != null && listaParametricoGenerico.Count > 0){
        //        List<EtapaCultivo> listaEtapaCultivo = new List<EtapaCultivo>();
        //        EtapaCultivo etapaCultivo = null;
        //        foreach (ParametroGenerico parametroGenerico in listaParametricoGenerico)
        //        {
        //            etapaCultivo = new EtapaCultivo();
        //            etapaCultivo.id_etapaDesarrollo = parametroGenerico.id;
        //            etapaCultivo.nombreEtapaDesarrollo = parametroGenerico.descripcion;
        //            listaEtapaCultivo.Add(etapaCultivo);
        //        }
        //        return listaEtapaCultivo;
        //    }

        //    return null;
        //}

        public DataTable actualizarEtapaCultivo(EtapaCultivo etapaCultivo)
        {
            //return mantenedorDA.GuardarEtapaCultivo_Mantenedor(etapaCultivo.id_etapaDesarrollo, etapaCultivo.nombreEtapaDesarrollo, etapaCultivo.idEspecie);
            //return mantenedorDA.GuardarEtapaCultivo_Mantenedor(etapaCultivo);
            return mantenedorDA.GuardarEtapaDesarrollo_Mantenedor(etapaCultivo);
        }

        public DataTable eliminarEtapaCultivo(int idEtapaDesarrollo, int idEspecie, int idGrupoEsp)
        {
            //return mantenedorDA.EliminarEtapaCultivo_Mantenedor(id);
            return mantenedorDA.EliminarEtapaDesarrollo_Mantenedor(idEtapaDesarrollo, idEspecie, idGrupoEsp);
        }

        public DataTable guardarGrupoEspecie(GrupoEspecie grupoEspecie)
        {
            return mantenedorDA.GuardarGrupoEspecie_Mantenedor(grupoEspecie);
        }

        public List<GrupoEspecie> listarGrupoEspecie(GrupoEspecie grupoEspecieFiltro)
        {
            //return mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro.id_grupoEspecie);
            return mantenedorDA.ListarGrupoEspecie_Mantenedor(grupoEspecieFiltro);
        }

        public DataTable actualizarGrupoEspecie(GrupoEspecie grupoEspecie)
        {
            return mantenedorDA.GuardarGrupoEspecie_Mantenedor(grupoEspecie);
        }

        public DataTable eliminarGrupoEspecie(int id)
        {
            return mantenedorDA.EliminarGrupoEspecie_Mantenedor(id);
        }

        public DataTable guardarTipoArchivoCoordenada(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public List<ParametroGenerico> listarTipoArchivoCoordenada(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable eliminarTipoArchivoCoordenada(int id)
        {
            return mantenedorDA.EliminarTipo_Mantenedor(id);
        }

        public DataTable actualizarTipoArchivoCoordenada(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public DataTable guardarTipo(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public DataTable guardarUnidadMedidaEstructuraTecnica(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoMedida_Mantenedor(parametroGenerico);
        }

        public List<ParametroGenerico> listarUnidadMedidaEstructuraTecnica(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipoMedida_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable actualizarUnidadMedidaEstructuraTecnica(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoMedida_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarUnidadMedidaEstructuraTecnica(int id)
        {
            return mantenedorDA.EliminarTipoMedida_Mantenedor(id);
        }

        public List<ParametroGenerico> listarResultado(ParametroGenerico parametroGenericoFiltro)
        {
            //return mantenedorDA.ListarEstadosGenerales_Mantenedor(parametroGenericoFiltro.id);
            return mantenedorDA.ListarEstadosGenerales_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable guardarResultado(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarEstadosGenerales_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public DataTable eliminarResultado(int id)
        {
            return mantenedorDA.EliminarEstadosGenerales_Mantenedor(id);
        }

        public DataTable actualizarResultado(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarEstadosGenerales_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public DataTable guardarSubrequerimientoResultado(DocumentoAmbito documentoAmbito)
        {
            return mantenedorDA.GuardarEstadoSubRequerimiento_Mantenedor(documentoAmbito.tipo.id, documentoAmbito.estadoResultadoResp.id);
        }

        public List<DocumentoAmbito> listarSubrequerimientoResultado(DocumentoAmbito documentoAmbitoFiltro)
        {
            //return mantenedorDA.ListarEstadoSubReqEstado_Mantenedor(documentoAmbitoFiltro.tipo.id);
            return mantenedorDA.ListarEstadoSubReqEstado_Mantenedor(documentoAmbitoFiltro);
        }

        public DataTable listarResultado(int idSubrequerimiento)
        {
            return mantenedorDA.ListarEstadoSubRequerimiento_Mantenedor(idSubrequerimiento);
        }

        public DataTable actualizarSubrequerimientoResultado(DocumentoAmbito documentoAmbito)
        {
            return mantenedorDA.GuardarEstadoSubRequerimiento_Mantenedor(documentoAmbito.tipo.id, documentoAmbito.estadoResultadoResp.id);
        }

        public DataTable eliminarSubrequerimientoResultado(int idResultado, int idSubrequerimiento)
        {
            return new DataTable();
        }

        public DataTable guardarResponsableDAC(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardaResponsableDAC_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public List<ParametroGenerico> listarResponsableDAC(ParametroGenerico parametroGenericoFiltro)
        {
            //return mantenedorDA.ListarResponsableDAC_Mantenedor(parametroGenericoFiltro.id);
            return mantenedorDA.ListarResponsableDAC_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable actualizarResponsableDAC(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardaResponsableDAC_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public DataTable eliminarResponsableDAC(int id)
        {
            return mantenedorDA.EliminarResponsableDAC_Mantenedor(id);
        }

        public DataTable guardarPreferenciaRelocalizacion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarPreferenciaRel_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public List<ParametroGenerico> listarPreferenciaRelocalizacion(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarPreferenciaRel_Mantenedor(parametroGenericoFiltro.id, parametroGenericoFiltro.descripcion);
        }

        public DataTable actualizarPreferenciaRelocalizacion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarPreferenciaRel_Mantenedor(parametroGenerico.id, parametroGenerico.descripcion);
        }

        public DataTable eliminarPreferenciaRelocalizacion(int id)
        {
            return mantenedorDA.EliminarPreferenciaRel_Mantenedor(id);
        }

        public List<ParametroGenerico> listarDatum(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarDatum_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable actualizarDatum(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarDatum_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarDatum(int id)
        {
            return mantenedorDA.EliminarDatum_Mantenedor(id);
        }

        public DataTable guardarDatum(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarDatum_Mantenedor(parametroGenerico);
        }

        public DataTable guardarCarta(Carta carta)
        {
            return mantenedorDA.GuardarCarta_Mantenedor(carta);
        }

        public List<Carta> listarCarta(Carta cartaFiltro)
        {
            //return mantenedorDA.ListarCarta_Mantenedor(cartaFiltro.idCarta);
            return mantenedorDA.ListarCarta_Mantenedor(cartaFiltro);
        }

        public DataTable actualizarCarta(Carta carta)
        {
            return mantenedorDA.GuardarCarta_Mantenedor(carta);
        }

        public DataTable eliminarCarta(int id)
        {
            return mantenedorDA.EliminarCarta_Mantenedor(id);
        }

        public List<ParametroGenerico> listarTipoCarta(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.ListarTipoCarta_Mantenedor(parametroGenerico.id);
        }

        //public DataTable eliminarAsocBarrioTipo(int idTipoBarrio, int idBarrio)
        //{
        //    return mantenedorDA.EliminarAsocBarrioTipo_Mantenedor(idBarrio, idTipoBarrio);
        //}

        public DataTable actualizarBarrioTipo(Barrio barrio)
        {
            return new DataTable();
        }

        public bool ActualizaSolicitudConcesion_ACS_ACM(int idSolicitud, int IdBarrio, int idACM, int idUsuario)
        {
            return solicitudDA.ActualizaSolicitudConcesion_ACS_ACM(idSolicitud, IdBarrio, idACM, idUsuario);
        }

        //public DataTable guardarBarrioTipo(Barrio barrio)
        //{
        //    return mantenedorDA.GuardarAsocBarrioTipo_Mantenedor(barrio.id_barrio, barrio.tipo_barrio.id);
        //}

        public List<Barrio> listarBarrioTipo(Barrio barrioFiltro)
        {

            DataTable dt = (DataTable)barrioDA.obtenerBarrio(barrioFiltro.tipo_barrio.id, barrioFiltro.id_barrio, barrioFiltro.id_region, barrioFiltro.id_macrozona);
            List<Barrio> listaBarrios = new List<Barrio>();

            if (dt != null)
            {
                Barrio barrio = null;
                foreach (DataRow row in dt.Rows)
                {

                    barrio = new Barrio();
                    barrio.tipo_barrio = new ParametroGenerico(Convert.ToInt32(row["idTipoBarrio"]), Convert.ToString(row["nombreTipo"]));
                    barrio.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                    barrio.barrio = Convert.ToString(row["Barrio"]);
                    barrio.id_region = Convert.ToInt32(row["IdRegion"]);
                    barrio.id_macrozona = Convert.ToInt32(row["IdMacrozona"]);
                    
                    listaBarrios.Add(barrio);
                }
            }
            return listaBarrios;
        }


        public List<AsociacionSolicitudBarrio> listarSolicitudBarrio(AsociacionSolicitudBarrio barrioFiltro)
        {
            return barrioDA.listarSolicitudBarrio(barrioFiltro);
        }

        public DataTable guardarTemaSubrequerimiento(SubRequerimiento subRequerimiento)
        {
            return mantenedorDA.GuardarSubRequerimiento_Mantenedor(subRequerimiento.idSubRequerimiento, subRequerimiento.nombreSubRequerimiento, subRequerimiento.aplicaReitera, subRequerimiento.aplicaComplementario, subRequerimiento.aplicaVisacionMasiva);
        }

        public List<SubRequerimiento> listarTemaSubrequerimiento(SubRequerimiento subRequerimiento)
        {
            return requerimientoDA.ListarSubRequerimiento(subRequerimiento);
        }

        public DataTable actualizarTemaSubrequerimiento(SubRequerimiento subRequerimiento)
        {
            return mantenedorDA.GuardarSubRequerimiento_Mantenedor(subRequerimiento.idSubRequerimiento, subRequerimiento.nombreSubRequerimiento, subRequerimiento.aplicaReitera, subRequerimiento.aplicaComplementario, subRequerimiento.aplicaVisacionMasiva);
        }

        public DataTable eliminarTemaSubrequerimiento(int id)
        {
            return mantenedorDA.EliminarSubRequerimiento_Mantenedor(id);
        }

        public List<ParametroGenerico> listarTipoCuerpoAgua(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable actualizarTipoCuerpoAgua(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarTipoCuerpoAgua(int id)
        {
            return mantenedorDA.EliminarTipo_Mantenedor(id);
        }

        public DataTable guardarTipoCuerpoAgua(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipo_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarCuerpoDeAgua(int id)
        {
            return mantenedorDA.EliminarCuerpoAgua_Mantenedor(id);
        }

        public DataTable guardarCuerpoDeAgua(CuerpoDeAgua cuerpoDeAgua)
        {
            return mantenedorDA.GuardarCuerpoAgua_Mantenedor(cuerpoDeAgua);
        }

        public List<CuerpoDeAgua> listarCuerpoDeAgua(CuerpoDeAgua cuerpoDeAguaFiltro)
        {
            return mantenedorDA.ListarCuerpoAgua_Mantenedor(cuerpoDeAguaFiltro);
        }

        public DataTable actualizarCuerpoDeAgua(CuerpoDeAgua cuerpoDeAgua)
        {
            return mantenedorDA.GuardarCuerpoAgua_Mantenedor(cuerpoDeAgua);
        }

        public object listarPlazoNominal(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarTipo_Mantenedor(parametroGenericoFiltro);
        }

        

        public DataTable guardarTipoOrganizacion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoPersonaJuridica_Mantenedor(parametroGenerico);
        }

        public List<ParametroGenerico> listarTipoOrganizacion(ParametroGenerico parametroGenerico)
        {
            //return parametroGenericoDA.ListarTipoPersonaJuridica(0);
            return parametroGenericoDA.ListarTipoPersonaJuridica(parametroGenerico);
        }

        public DataTable actualizarTipoOrganizacion(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarTipoPersonaJuridica_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarTipoOrganizacion(int id)
        {
            return mantenedorDA.EliminarTipoPersonaJuridica_Mantenedor(id);
        }

        public DataTable guardarHolding(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarHolding_Mantenedor(parametroGenerico);
        }

        public List<ParametroGenerico> listarHolding(ParametroGenerico parametroGenerico)
        {
            //return mantenedorDA.ListarHolding_Mantenedor(0);
            return mantenedorDA.ListarHolding_Mantenedor(parametroGenerico);
        }

        public DataTable actualizarHolding(ParametroGenerico parametroGenerico)
        {
            return mantenedorDA.GuardarHolding_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarHolding(int id)
        {
            return mantenedorDA.EliminarHolding_Mantenedor(id);
        }

        public DescansoSanitario ObtenerUltimaOperacionDescanso_Mantenedor(int idBarrio)
        {
            return mantenedorDA.ObtenerUltimaOperacionDescanso_Mantenedor(idBarrio);
        }

        public List<DescansoSanitario> listarDescansoSanitario(DescansoSanitario descansoSanitario)
        {
            return mantenedorDA.ListarDescansoSanitario_Mantenedor(descansoSanitario.idDescanso, descansoSanitario.barrio.id, descansoSanitario.fechaInicio, descansoSanitario.fechaFin, descansoSanitario.fechaInicioProduccionCero);
        }

        public DataTable guardarDescansoSanitario(DescansoSanitario descansoSanitario)
        {
            return mantenedorDA.GuardarDescansoSanitario_Mantenedor(descansoSanitario);
        }

        public DataTable eliminarDescansoSanitario(int id)
        {
            return mantenedorDA.EliminarDescansoSanitario_Mantenedor(id);
        }

        public DataTable guardarEspecieEtapaDeDesarrollo(EtapaCultivo etapaCultivo)
        {
            return mantenedorDA.GuardarEtapaDesarrollo_Mantenedor(etapaCultivo);
        }

        public List<EtapaCultivo> listarEspecieEtapaDesarrollo(EtapaCultivo etapaCultivoFiltro)
        {
            //return mantenedorDA.ListarEtapaDesarrollo_Mantenedor(etapaCultivoFiltro.idEspecie, etapaCultivoFiltro.codigo);
            return mantenedorDA.ListarEtapaDesarrollo_Mantenedor(etapaCultivoFiltro);
        }

        public DataTable actualizarEspecieEtapaDesarrollo(EtapaCultivo etapaCultivo)
        {
            return new DataTable();
        }

        public DataTable eliminarEspecieEtapaDesarrollo(int idEspecie, int codigoEtapa, int idGrupoEsp)
        {
            return mantenedorDA.EliminarEtapaDesarrollo_Mantenedor(idEspecie, codigoEtapa, idGrupoEsp);
        }

        public DataTable actualizarCorreoElectronico(TemplateAviso templateAviso)
        {
            return new DataTable();
        }

        public List<TemplateAviso> listarCorreoElectronico(TemplateAviso templateAviso)
        {

            List<TemplateAviso> templateAvisoList = templateAvisoDA.ListarTemplateAvisoDestinatarioAdmin(templateAviso.claveTemplateAviso);
            List<TemplateAviso> templateAvisoListAux = new List<TemplateAviso>();
            List<string> claveList = new List<string>();

            foreach (TemplateAviso templateAvisoAux in templateAvisoList)
            {

                if (templateAvisoAux != null && !claveList.Contains(templateAvisoAux.claveTemplateAviso)) {
                    
                    claveList.Add(templateAvisoAux.claveTemplateAviso);
                    templateAvisoListAux.Add(templateAvisoAux);
                }
            }

            return templateAvisoListAux;
        }

        public DataTable obtenerCapitaniaPuerto(CapitaniaDePuerto capitaniaDePuerto)
        {
            return new DataTable();
        }

        public TemplateAviso obtieneCorreoElectronico(TemplateAviso templateAviso)
        {
            return templateAvisoDA.ObtieneTemplateAvisoDestinatarioAdmin(templateAviso.claveTemplateAviso);
        }

        public bool modificarCorreoElectronico(TemplateAviso templateAviso)
        {
            try
            {
                /* Actualizar información del correo electrónico */
                if (!templateAvisoDA.ActualizaTemplateAviso(templateAviso))
                {
                    return false;
                }

                TemplateAviso templateAvisoAux = templateAvisoDA.ObtieneTemplateAvisoDestinatarioAdmin(templateAviso.claveTemplateAviso);

                /* Eliminar la información del correo electrónico si existente previamente */
                if (templateAvisoAux != null && (templateAvisoAux.destinatariosRol.Count > 0 || templateAvisoAux.destinatariosTemplate.Count > 0))
                {
                    if (!templateAvisoDA.EliminarDestinatarioTemplate(templateAviso.idDestTemplate, templateAviso.claveTemplateAviso))
                    {
                        return false;
                    }
                }
                
                /* Agregar Destinatarios por Email */
                foreach (DestinatarioTemplate destinatarioEmail in templateAviso.destinatariosTemplate)
                {
                    if (!templateAvisoDA.GuardarDestinatarioTemplate(destinatarioEmail))
                    {
                        return false;
                    }
                }

                /* Agregar Destinatarios por Rol */
                foreach (DestinatarioTemplate destinatarioRol in templateAviso.destinatariosRol)
                {
                    if (!templateAvisoDA.GuardarDestinatarioTemplate(destinatarioRol))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;

            }
        }


        /**
         * Modifica la vigencia de un barrio
         */
        public bool ActualizarVigenciaBarrio(int idBarrio, int idVigencia)
        {
            
                try
                {

                    if (!solicitudDA.ActualizarVigenciaBarrio(idBarrio, idVigencia))
                    {
                        return false;
                    }

                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            
        }
        public bool guardarArchivoGrupoSuspendido(ArchivoBinario Archivo, int idGrupoSuspendido)
        {
            using (TransactionScope Transaction = new TransactionScope())
            {
                if (Archivo.docFinal == true)
                {
                    grupoSuspendidoDA.ActualizaGrupoSuspendidoVigencia(idGrupoSuspendido, 94);
                }
                bool resp = grupoSuspendidoDA.GuardarArchivoGrupoSusp(Archivo);
                if (resp)
                {
                    resp = grupoSuspendidoDA.GuardaAsocArchivoGrupoSusp(idGrupoSuspendido, Archivo.idArchivo, Archivo.docFinal,6);
                    if (resp)
                    {
                        Transaction.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
        }
        public bool guardarGrupoSuspendidoAsoc(AsocGrupoSolicitud AsocGrupoSol)
        {
            return grupoSuspendidoDA.GuardarAsocGrupoSolicitud(AsocGrupoSol);
        }

        public bool guardarGrupoSuspendido(GrupoSuspendidos grupoSuspendido)
        {
                return grupoSuspendidoDA.GuardarGrupoSuspendido(grupoSuspendido); 
        }

        public DataTable guardarTipoAlimentoEspecie(EspecieTipoAlimento especieTipoAlimento)
        {
            return mantenedorDA.GuardarTipoAlimentoEspecie_Mantenedor(especieTipoAlimento.especie.id, especieTipoAlimento.tipoAlimento.id);
        }

        public List<EspecieTipoAlimento> listarTipoAlimentoEspecie(EspecieTipoAlimento especieTipoAlimento)
        {
            return mantenedorDA.ListarTipoAlimentoEspecie(especieTipoAlimento.tipoAlimento.id, especieTipoAlimento.especie.id);
        }

        public DataTable actualizarTipoAlimentoEspecie(EspecieTipoAlimento especieTipoAlimento)
        {
            return mantenedorDA.GuardarTipoAlimentoEspecie_Mantenedor(especieTipoAlimento.especie.id, especieTipoAlimento.tipoAlimento.id);
        }

        public DataTable eliminarTipoAlimentoEspecie(int idTipoAlimento, int idEspecie)
        {
            return mantenedorDA.EliminarTipoAlimentoEspecie_Mantenedor(idEspecie,idTipoAlimento);
        }

        public List<EtapaCultivo> listarEtapaCultivo(EtapaCultivo etapaCultivoFiltro)
        {
            //List<ParametroGenerico> listaParametricoGenerico = mantenedorDA.ListarEtapaCultivo_Mantenedor(etapaCultivoFiltro.id_etapaDesarrollo);
            //List<ParametroGenerico> listaParametricoGenerico = mantenedorDA.ListarEtapaCultivo_Mantenedor(etapaCultivoFiltro);

            //if (listaParametricoGenerico != null && listaParametricoGenerico.Count > 0)
            //{
            //    List<EtapaCultivo> listaEtapaCultivo = new List<EtapaCultivo>();
            //    EtapaCultivo etapaCultivo = null;
            //    foreach (ParametroGenerico parametroGenerico in listaParametricoGenerico)
            //    {
            //        etapaCultivo = new EtapaCultivo();
            //        etapaCultivo.id_etapaDesarrollo = parametroGenerico.id;
            //        etapaCultivo.nombreEtapaDesarrollo = parametroGenerico.descripcion;
            //        listaEtapaCultivo.Add(etapaCultivo);
            //    }
            //    return listaEtapaCultivo;
            //}

            List<EtapaCultivo> listaEtapaCultivo = mantenedorDA.ListarEtapaDesarrollo_Mantenedor(etapaCultivoFiltro);
            if (listaEtapaCultivo != null && listaEtapaCultivo.Count > 0) {
                return listaEtapaCultivo;
            }

            return null;
        }

        public bool CambioEstado_AsocSolConGrupoSuspendido(int idGrupoSuspendido, int idsolConcecion, int idvigencia)
        {
            try
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    if (idsolConcecion > 0)
                    {
                        if (grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(0, idGrupoSuspendido, idsolConcecion, 0, idvigencia, 0))
                        {
                            _TransactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else 
                    {
                        return false;
                    }
                    
                }
            }
            catch (Exception e)
            {
                //error :(
                logger.PrintError(e);
                logger.SendMailError(e);
                return false;
            }
            
        }
        public bool EliminarArchivoGrupoSuspendido(int idGrupoSusp, int idarchivo)
        { 
            using(TransactionScope Transaction = new TransactionScope())
            {
                bool verdadero = grupoSuspendidoDA.EliminarAsocArchivoGrupoSusp(idGrupoSusp, idarchivo);
                if (verdadero)
                {
                    Transaction.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }

        public bool EliminarGrupoSuspendido(int idGrupoSuspendido)
        {
            using (TransactionScope Transaction = new TransactionScope())
            {
                List<ArchivoBinario> Archivos = new List<ArchivoBinario>();
                Archivos = grupoSuspendidoDA.ListarArchivoGrupoSusp(idGrupoSuspendido, 0);
                bool verdadero = false;
                //realizamos el cambio de vigencia de los archivos del grupo suspendido. 
                if (Archivos.Count > 0)
                {
                    foreach (ArchivoBinario Arch in Archivos)
                    {
                        verdadero = grupoSuspendidoDA.ActualizarVigenciaAsocArchivoGrupoSusp(idGrupoSuspendido,Arch.idArchivo,7);
                        if (!verdadero)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //no contiene archivos a eliminar
                    verdadero = true;
                }
                
                if (verdadero)
                {
                    List<AsocGrupoSolicitud> resp = grupoSuspendidoDA.ListarAsocGrupoSolicitud(idGrupoSuspendido, 0, 0);
                    if (resp.Count > 0)
                    {
                        verdadero = grupoSuspendidoDA.ActualizaAsocGrupoSolicitudVigencia(0, idGrupoSuspendido, 0, 0, 7, 0);
                    }
                    if (verdadero)
                    {
                        verdadero = grupoSuspendidoDA.ActualizaGrupoSuspendidoVigencia(idGrupoSuspendido, 7);
                        if (verdadero)
                        {
                            Transaction.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable guardarEntidadDeAnalisis(EntidadMuestreador entidadMuestreador)
        {
            return mantenedorDA.GuardarEntidadAnalisis_Mantenedor(entidadMuestreador);
        }

        public List<EntidadMuestreador> listarEntidadDeAnalisis(EntidadMuestreador entidadMuestreadorFiltro)
        {
            return mantenedorDA.ListarEntidadAnalisis_Mantenedor(entidadMuestreadorFiltro);
        }

        public DataTable actualizarEntidadDeAnalisis(EntidadMuestreador entidadMuestreador)
        {
            return mantenedorDA.GuardarEntidadAnalisis_Mantenedor(entidadMuestreador);
        }

        public DataTable eliminarEntidadDeAnalisis(int id)
        {
            return mantenedorDA.EliminarEntidadAnalisis_Mantenedor(id);
        }

        public DataTable guardarEntidadDeMuestreo(EntidadMuestreador entidadMuestreador)
        {
            return mantenedorDA.GuardarMuestreadorAmbiental_Mantenedor(entidadMuestreador);
        }

        public List<EntidadMuestreador> listarEntidadDeMuestreo(EntidadMuestreador entidadMuestreador)
        {
            return mantenedorDA.ListarMuestreadorAmbiental_Mantenedor(entidadMuestreador);
        }

        public DataTable eliminarEntidadDeMuestreo(int id)
        {
           return mantenedorDA.EliminarMuestreadorAmbiental_Mantenedor(id);
        }

        public DataTable actualizarEntidadDeMuestreo(EntidadMuestreador entidadMuestreador)
        {
            return mantenedorDA.GuardarMuestreadorAmbiental_Mantenedor(entidadMuestreador);
        }

        public DataTable guardarConsultor(ConsultorAmbiental consultorAmbiental)
        {
            return mantenedorDA.GuardarConsultorAmbiental_Mantenedor(consultorAmbiental);
        }

        public List<ConsultorAmbiental> listarConsultores(ConsultorAmbiental consultorAmbiental)
        {
            return mantenedorDA.ListarConsultorAmbiental_Mantenedor(consultorAmbiental);
        }

        public DataTable actualizarConsultor(ConsultorAmbiental consultorAmbiental)
        {
            return mantenedorDA.GuardarConsultorAmbiental_Mantenedor(consultorAmbiental);
        }

        public DataTable eliminarConsultor(int id)
        {
            return mantenedorDA.EliminarConsultorAmbiental_Mantenedor(id);
        }

        public DataTable GuardarDireccionZonal(DireccionZonal direccionZonal)
        {
            return mantenedorDA.GuardarDireccionZonal_Mantenedor(direccionZonal);
        }

        public List<DireccionZonal> listarDireccionZonal(DireccionZonal direccionZonal)
        {
            return mantenedorDA.ListarDireccionZonal_Mantenedor(direccionZonal);
        }

        public DataTable actualizarDireccionZonal(DireccionZonal direccionZonal)
        {
            return mantenedorDA.GuardarDireccionZonal_Mantenedor(direccionZonal);
        }

        public DataTable eliminarDireccionZonal(string id)
        {
            return mantenedorDA.EliminarDireccionZonal_Mantenedor(id);
        }

        public DataTable guardarDireccionZonalRegion(DireccionZonal direccionZonal)
        {
            return mantenedorDA.GuardarDireccionZonalRegion_Mantenedor(direccionZonal);
        }

        public List<DireccionZonal> listarDireccionZonalRegion(DireccionZonal direccionZonal)
        {
            return mantenedorDA.ListarDireccionZonalRegion_Mantenedor(direccionZonal.codDirZonal, direccionZonal.region.id);
        }

        public DataTable eliminarDireccionZonalRegion(string codDirZonal, int idRegion)
        {
            return mantenedorDA.EliminarDireccionZonalRegion_Mantenedor(codDirZonal,idRegion);
        }

        public DataTable GuardarPlazosDocumentos(PlazoDocumentoSolicitud plazoDocumentoSolicitud)
        {
            return requerimientoDA.GuardarSubRequerimientoPlazo(plazoDocumentoSolicitud);
        }

        public List<PlazoDocumentoSolicitud> listarPlazosDocumentos(PlazoDocumentoSolicitud plazoDocumentoSolicitud)
        {
            return requerimientoDA.ListarSubRequerimientoPlazo(plazoDocumentoSolicitud);
        }

        public DataTable actualizarPlazosDocumentos(PlazoDocumentoSolicitud plazoDocumentoSolicitud)
        {
            return requerimientoDA.GuardarSubRequerimientoPlazo(plazoDocumentoSolicitud);
        }

        public DataTable eliminarPlazosDocumentos(int id)
        {
            return requerimientoDA.EliminarSubRequerimientoPlazo(id);
        }

        public DataTable guardarFeriado(Feriado feriado)
        {
            return mantenedorDA.GuardarFeriados_Mantenedor(feriado.idFeriado, feriado.fecha, feriado.descripcion);
        }

        public List<Feriado> listarFeriado(Feriado feriado)
        {
            return mantenedorDA.ListarFeriados(feriado.idFeriado, feriado.fecha, feriado.descripcion);
        }

        public DataTable actualizarFeriado(Feriado feriado)
        {
            return mantenedorDA.GuardarFeriados_Mantenedor(feriado.idFeriado, feriado.fecha, feriado.descripcion);
        }

        public DataTable eliminarFeriado(int id)
        {
            return mantenedorDA.EliminarFeriados_Mantenedor(id);
        }

        public DataTable guardarMateria(Materia materia)
        {
            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.descripcion = materia.nombreMateria;

            return mantenedorDA.GuardarMateria_Mantenedor(parametroGenerico);
        }

        public List<ParametroGenerico> listarMateria(Materia materia)
        {
            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.id = materia.idMateria;
            parametroGenerico.descripcion = materia.nombreMateria;

            return mantenedorDA.ListarMateria_Mantenedor(parametroGenerico);
        }

        public DataTable actualizarMateria(Materia materia)
        {
            ParametroGenerico parametroGenerico = new ParametroGenerico();
            parametroGenerico.id = materia.idMateria;
            parametroGenerico.descripcion = materia.nombreMateria;

            return mantenedorDA.GuardarMateria_Mantenedor(parametroGenerico);
        }

        public DataTable eliminarMateria(int id)
        {
            return mantenedorDA.EliminarMateria_Mantenedor(id);
        }

        public DataTable guardarMateriaSubrequerimiento(Datos.Entidades.Resolucion.ResolucionValidacion resolucionValidacion)
        {
            return mantenedorDA.GuardarMateriaSubRequerimiento_Mantenedor(resolucionValidacion);
        }

        public List<ResolucionValidacion> listarMateriaSubrequerimiento(Datos.Entidades.Resolucion.ResolucionValidacion resolucionValidacion)
        {
            return mantenedorDA.ListarMateriaSubRequerimiento_Mantenedor(resolucionValidacion);
        }

        public DataTable eliminarMateriaSubrequerimiento(int id)
        {
            return mantenedorDA.EliminarMateriaSubRequerimiento_Mantenedor(id);
        }

        public DataTable actualizarMateriaSubrequerimiento(ResolucionValidacion resolucionValidacion)
        {
            return mantenedorDA.GuardarMateriaSubRequerimiento_Mantenedor(resolucionValidacion);
        }

        public DataTable guardarEstadoUOT(ParametroGenerico estadosUOT)
        {
            return mantenedorDA.GuardarEstadosUOT_Mantenedor(estadosUOT);
        }

        public List<ParametroGenerico> listarEstadoUOT(ParametroGenerico parametroGenericoFiltro)
        {
            return mantenedorDA.ListarEstadosUOT_Mantenedor(parametroGenericoFiltro);
        }

        public DataTable eliminarEstadoUOT(int id)
        {
            return mantenedorDA.EliminarEstadosUOT_Mantenedor(id);
        }

        public DataTable actualizarEstadoUOT(ParametroGenerico estadosUOT)
        {
            return mantenedorDA.GuardarEstadosUOT_Mantenedor(estadosUOT);
        }

        public List<EquivalenciaUOT> listarEquivalenciaUOT(EquivalenciaUOT equivalenciaUOT)
        {
            return mantenedorDA.ListarEquivalenciaEstadoUOT_Mantenedor(equivalenciaUOT.estadoUOT.id, equivalenciaUOT.estadoSolicitud.id, equivalenciaUOT.tipoUE.id);
        }

        public DataTable GuardarEquivalenciaEstadoUOT(EquivalenciaUOT equivalenciaUOT)
        {
            return mantenedorDA.GuardarEquivalenciaEstadoUOT_Mantenedor(equivalenciaUOT.estadoUOT.id, equivalenciaUOT.estadoSolicitud.id, equivalenciaUOT.tipoUE.id);
        }

        public DataTable eliminarEquivalenciaEstadoUOT(int idEstadoUOT, int idEstadoSolicitud)
        {
            return mantenedorDA.EliminarEquivalenciaEstadoUOT_Mantenedor(idEstadoUOT, idEstadoSolicitud);
        }

        public DataTable actualizarEquivalenciaEstadoUOT(EquivalenciaUOT equivalenciaUOT)
        {
            return mantenedorDA.GuardarEquivalenciaEstadoUOT_Mantenedor(equivalenciaUOT.estadoUOT.id, equivalenciaUOT.estadoSolicitud.id, equivalenciaUOT.tipoUE.id);
        }
    }
}
