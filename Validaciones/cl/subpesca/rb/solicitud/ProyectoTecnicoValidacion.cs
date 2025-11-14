using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class ProyectoTecnicoValidacion
    {

        ProyectoTecnicoService proyService = new ProyectoTecnicoService();
        MantenedorDA mantenedorDA = new MantenedorDA();


        public List<String> validaEspecieAutorizada(EspecieAutorizadaPT especieAutorizada)
        {
            List<String> listaErrroresEspecieAutorizada = new List<String>();
            if (especieAutorizada != null)
            {
                if (especieAutorizada.especieCheck)
                {
                    if (especieAutorizada.especie == null || especieAutorizada.especie.id == -1)
                    {
                        listaErrroresEspecieAutorizada.Add("Seleccione Especie");
                    }
                }
                if (especieAutorizada.grupoCheck)
                {
                    if (especieAutorizada.grupoEspecieAutoriz == null || especieAutorizada.grupoEspecieAutoriz.id == -1)
                    {
                        listaErrroresEspecieAutorizada.Add("Seleccione Grupo");
                    }
                }
                //if(especieAutorizada.etapaCultivo == null || especieAutorizada.etapaCultivo.id == -1){
                    
                //    listaErrroresEspecieAutorizada.Add("Seleccione Etapa de Cultivo");
                //}

                if (especieAutorizada.etapaCultivoList == null || especieAutorizada.etapaCultivoList.Count <= 0)
                {
                    listaErrroresEspecieAutorizada.Add("Seleccione Etapa de Cultivo");
                }

                if (especieAutorizada.tipoCultivo == null || especieAutorizada.tipoCultivo.id == -1)
                {
                    listaErrroresEspecieAutorizada.Add("Seleccione Tipo de Cultivo");
                }

                if (especieAutorizada.tipoAlimento == null || especieAutorizada.tipoAlimento.id == -1)
                {
                    listaErrroresEspecieAutorizada.Add("Seleccione Tipo de Alimento");
                }

                if (especieAutorizada.tipoAlimento != null && especieAutorizada.tipoAlimento.id == 74) //Otro
                {
                    if (especieAutorizada.nombreOtroTipoAlimento == null || especieAutorizada.nombreOtroTipoAlimento.Equals(""))
                    {
                        listaErrroresEspecieAutorizada.Add("Si selecciona Otro Tipo de Alimento, debe ingresar el Nombre del Otro Tipo de Alimento.");
                    }
                }

            }
            return listaErrroresEspecieAutorizada;
        }

        public List<String> validaEspecieAutorizadaLista(EspecieAutorizadaPT especieAutorizada, List<EspecieAutorizadaPT> especieGrilla)
        {
            List<String> listaErrroresEspecieAutorizada = new List<String>();
            ParametroGenerico especie = null;
            
            
            if (especieAutorizada.especie != null && especieAutorizada.especie.id > 0)
            {
                especie = new ParametroGenerico(especieAutorizada.especie.id, "");
            }
            else if (especieAutorizada.grupoEspecieAutoriz != null && especieAutorizada.grupoEspecieAutoriz.id > 0)
            {
                especie = new ParametroGenerico(especieAutorizada.grupoEspecieAutoriz.id, "");
            }

            if (especie == null)
            {
                listaErrroresEspecieAutorizada.Add("Validacion especie-etapa no pudo ser realizada");
            }

            else
            {
                if (especieGrilla != null && especieGrilla.Count > 0)
                {
                    //foreach (EspecieAutorizadaPT especieAux in especieGrilla)
                    //{
                    //    if ((especieAux.accion == accion.LISTADO || especieAux.accion == accion.INGRESAR) && (especieAux.especie.id == especie.id && especieAux.etapaCultivo != null && especieAutorizada.etapaCultivo != null && especieAux.etapaCultivo.id == especieAutorizada.etapaCultivo.id) && (especieAux.index != especieAutorizada.index))
                    //    {
                    //        listaErrroresEspecieAutorizada.Add("Combinación especie-etapa ya ingresada");
                    //    }
                    //}

                    foreach (EspecieAutorizadaPT especieAux in especieGrilla)
                    {
                        if (especieAux.accion == accion.LISTADO || especieAux.accion == accion.INGRESAR)
                        {
                            foreach (EtapaCultivo etapaCultivo in especieAux.etapaCultivoList)
                            {
                                /* Combinación especie-etapa ya ingresada */
                                if (especieAutorizada != null && especieAutorizada.IDEspecie != null && Convert.ToInt32(especieAutorizada.IDEspecie) > 0){

                                    foreach (EtapaCultivo etapaCultivoEspecieAutorizada in especieAutorizada.etapaCultivoList)
                                    {
                                        if (especieAutorizada.IDEspecie == especieAux.IDEspecie && etapaCultivoEspecieAutorizada.id_etapaDesarrollo == etapaCultivo.id_etapaDesarrollo)
                                        {
                                            listaErrroresEspecieAutorizada.Add("Combinación especie-etapa ya ingresada");
                                        }
                                    }
                                }

                                /* Combinación grupo-etapa ya ingresada */
                                if (especieAutorizada != null && especieAutorizada.grupoEspecie != null && especieAutorizada.grupoEspecie.id > 0)
                                {
                                    foreach (EtapaCultivo etapaCultivoEspecieAutorizada in especieAutorizada.etapaCultivoList)
                                    {
                                        if (especieAux.grupoEspecie != null && especieAutorizada.grupoEspecie.id == especieAux.grupoEspecie.id && etapaCultivoEspecieAutorizada.id_etapaDesarrollo == etapaCultivo.id_etapaDesarrollo)
                                        {
                                            listaErrroresEspecieAutorizada.Add("Combinación grupo especies-etapa ya ingresada");
                                        }
                                    }
                                }

                                /* La especie ingresada no debe pertenecer a un grupo ya ingresado */
                                if (especieAutorizada != null && especieAutorizada.IDEspecie != null && Convert.ToInt32(especieAutorizada.IDEspecie) > 0) {

                                    if (especieAux != null && especieAux.grupoEspecieAutoriz != null && especieAux.grupoEspecieAutoriz.id > 0)
                                    {
                                        bool especiePerteceAlGrupo = mantenedorDA.AplicaEspecieCultivo_Grupo(Convert.ToInt32(especieAutorizada.IDEspecie), especieAux.grupoEspecieAutoriz.id);
                                        if (especiePerteceAlGrupo)
                                        {
                                            listaErrroresEspecieAutorizada.Add("La especie ingresada no debe pertenecer a un grupo ya ingresado.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return listaErrroresEspecieAutorizada;
        }

        public List<String> validaEspecieAutorizada_Eliminar(EspecieAutorizadaPT especieAutorizada)
        {
            List<String> listaErrroresEspecieAutorizada = new List<String>();

            if (especieAutorizada.list_ProgrProd != null && especieAutorizada.list_ProgrProd.Count>0)
            {
                foreach (ProgrProduccionPT progAux in especieAutorizada.list_ProgrProd)
                {
                    if ((progAux.especie!=null) && (progAux.accion == accion.INGRESAR || progAux.accion == accion.MODIFICAR || progAux.accion == accion.LISTADO) && (progAux.especie.id == especieAutorizada.especie.id))
                    {
                        listaErrroresEspecieAutorizada.Add("La especie a eliminar se encuentra asociada en el Programa de producción, elimine el registro asociado");
                        break;
                    }
                    
                }
            }

            return listaErrroresEspecieAutorizada;
        }

        public List<String> validaEstructuraTecnica(EstructuraTecnicaPT estructuraTecnica)
        {
            List<String> listaErrroresEstructuraTecnica = new List<String>();

            if (estructuraTecnica != null && estructuraTecnica.tipoEstructura != null && estructuraTecnica.tipoEstructura.id != tipoEstructura.DIRECTO_AL_SUSTRATO) //16 - Directo al Sustrato
            {

                if (estructuraTecnica.formaEstructura != null && estructuraTecnica.formaEstructura.id <= 0) {
                    listaErrroresEstructuraTecnica.Add("Ingrese Forma Estructura");
                }

                if (estructuraTecnica.unidadMedida != null && estructuraTecnica.unidadMedida.id <= 0)
                {
                    listaErrroresEstructuraTecnica.Add("Ingrese Unidad de Medida");
                }

                HashSet<int> resp = proyService.ListaEstructuraMedidas(estructuraTecnica.tipoEstructura.id, estructuraTecnica.formaEstructura.id);

                if (resp != null && resp.Count > 0)
                {
                    foreach (int aux in resp)
                    {
                        if (aux == rbTipo.ESTRUCT_MEDIDA_ALTO && estructuraTecnica.alto <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese Alto");
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_ANCHO && estructuraTecnica.ancho <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese Ancho");
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_DIAMETRO && estructuraTecnica.diametro <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese Diámetro");
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_LARGO && estructuraTecnica.largo <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese Largo");
                        }
                        else if (aux == rbTipo.ESTRUCT_MEDIDA_VOLUMEN && estructuraTecnica.volumenValorMedida <= 0)
                        {
                            if (estructuraTecnica.volumenUnidadMedida.id == -1)
                            {
                                listaErrroresEstructuraTecnica.Add("Seleccione Volumen Unidad de Medida");
                            }
                            listaErrroresEstructuraTecnica.Add("Ingrese Volumen Valor de Medida");

                        }
                    }
                }

                if (estructuraTecnica.anios == null || estructuraTecnica.anios.Count < 2)
                {
                    listaErrroresEstructuraTecnica.Add("Ingrese estructuras por año");
                }

                if (estructuraTecnica.tipoAnio.id == rbTipo.ESTRUCTURAS_ANIO)//debe setear todos los años
                {
                    foreach (ValorParametroAnioPT anioAux in estructuraTecnica.anios)
                    {
                        if (anioAux.anio == 1 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 1");
                        }
                        if (anioAux.anio == 2 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 2");
                        }
                        if (anioAux.anio == 3 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 3");
                        }
                        if (anioAux.anio == 4 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 4");
                        }
                        if (anioAux.anio == 5 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 5");
                        }
                    }

                }
                if (estructuraTecnica.tipoAnio.id == rbTipo.ESTRUCTURAS_ANIO_MAXIMO)//debe setear primer y ultimo año
                {
                    foreach (ValorParametroAnioPT anioAux in estructuraTecnica.anios)
                    {
                        if (anioAux.anio == 1 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año 1");
                        }
                        if (anioAux.anio == 4 && anioAux.valor <= 0)
                        {
                            listaErrroresEstructuraTecnica.Add("Ingrese estructuras en año máximo");
                        }
                    }
                }
            }

            return listaErrroresEstructuraTecnica;
        }

        public List<String> validaProgramaProduccion(ProgrProduccionPT programaProd)
        {
            List<String> listaErrroresProgramaProduccion = new List<String>();

            if(programaProd.especie.id==-1 && programaProd.grupo.id==-1){
                listaErrroresProgramaProduccion.Add("Ingrese especie o grupo");
            }
            if (programaProd.especie.id >0 && programaProd.grupo.id >0)
            {
                listaErrroresProgramaProduccion.Add("No puede seleccionar especie y grupo simultáneamente");
            }
            if(programaProd.tipoPesoPromEjemplares.id==rbTipo.PESO_PROM_SIN_RANGO && programaProd.pesoPromSR==0){
                listaErrroresProgramaProduccion.Add("Ingrese peso Promedio");
            }
            if (programaProd.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_RANGO && programaProd.pesoPromR1==0)
            {
                listaErrroresProgramaProduccion.Add("Ingrese Peso mínimo Promedio");
            }
            if (programaProd.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_RANGO && programaProd.pesoPromR2==0)
            {
                listaErrroresProgramaProduccion.Add("Ingrese Peso máximo Promedio");
            }
            if (programaProd.tipoPesoPromEjemplares.id == rbTipo.PESO_PROM_RANGO && (programaProd.pesoPromR1 > 0 && programaProd.pesoPromR2 > 0) && (programaProd.pesoPromR1 > programaProd.pesoPromR2))
            {
                listaErrroresProgramaProduccion.Add("El Peso mínimo Promedio debe ser menor que el Peso máximo Promedio");
            }

            return listaErrroresProgramaProduccion;
        }

        public List<String> validaProgrProduccionLista(ProgrProduccionPT programaProd, List<ProgrProduccionPT> programaProdGrilla)
        {
            List<String> listaErrroresProduccion = new List<String>();
            
            int especieGrupoOb = 0;

            if (programaProd.especie != null && programaProd.especie.id > 0)
            {
                especieGrupoOb = programaProd.especie.id;
            }
            else if (programaProd.grupo != null && programaProd.grupo.id > 0)
            {
                especieGrupoOb = programaProd.grupo.id;
            }
            
            //if (programaProdGrilla != null && programaProdGrilla.Count > 0)
            //{
            //   foreach (ProgrProduccionPT ppAux in programaProdGrilla)
            //   {
            //       if (ppAux.accion != accion.ELIMINAR || ppAux.accion != accion.IGNORAR)
            //       {

            //           if (ppAux.especie != null && ppAux.especie.id == especieGrupoOb && ppAux.etapaCultivo.id == programaProd.etapaCultivo.id && (ppAux.index != programaProd.index))
            //            {
            //                listaErrroresProduccion.Add("Combinación especie-etapa ya ingresada");
            //                break;
            //            }
            //           else if (ppAux.grupo != null && ppAux.grupo.id == especieGrupoOb && ppAux.etapaCultivo.id == programaProd.etapaCultivo.id && (ppAux.index != programaProd.index))
            //            {
            //                listaErrroresProduccion.Add("Combinación grupo-etapa ya ingresada");
            //                break;
            //            }  
            //       }
            //   }
            //}

            return listaErrroresProduccion;
        }




        public List<String> validaGrupoAutorizada(GrupoPT especieAutorizada)
        {
            List<String> listaErrroresGrupoAutorizada = new List<String>();
            if (especieAutorizada != null)
            {

                if (especieAutorizada.grupo == null || especieAutorizada.grupo.id == -1)
                {
                    listaErrroresGrupoAutorizada.Add("Seleccione Grupo");
                }
                
                if (especieAutorizada.etapaCultivo == null || especieAutorizada.etapaCultivo.id == -1)
                {

                    listaErrroresGrupoAutorizada.Add("Seleccione Etapa de Cultivo");
                }

            }
            return listaErrroresGrupoAutorizada;
        }


        public List<String> validaGrupoAutorizadaLista(GrupoPT grupoAutorizado, List<GrupoPT> grupoGrilla)
        {
            List<String> listaErrroresGrupoAutorizada = new List<String>();
            ParametroGenerico grupo = null;


            if (grupoAutorizado.grupo != null && grupoAutorizado.grupo.id > 0)
            {
                grupo = new ParametroGenerico(grupoAutorizado.grupo.id, "");
            }

            if (grupo == null)
            {
                listaErrroresGrupoAutorizada.Add("Validacion especie-etapa no pudo ser realizada");
            }

            else
            {
                if (grupoGrilla != null && grupoGrilla.Count > 0)
                {
                    foreach (GrupoPT grupoAux in grupoGrilla)
                    {
                        if ((grupoAux.accion == accion.LISTADO || grupoAux.accion == accion.INGRESAR) && (grupoAux.grupo.id == grupo.id && grupoAux.etapaCultivo != null && grupoAux.etapaCultivo.id == grupoAutorizado.etapaCultivo.id) && (grupoAux.index != grupoAutorizado.index))
                        {
                            listaErrroresGrupoAutorizada.Add("Combinación grupo-etapa ya ingresada");
                        }
                    }
                }
            }

            return listaErrroresGrupoAutorizada;
        }



        public List<String> validaGrupoAutorizada_Eliminar(GrupoPT grupoAutorizada)
        {
            List<String> listaErrroresGrupoAutorizada = new List<String>();

            if (grupoAutorizada.list_ProgrProd != null && grupoAutorizada.list_ProgrProd.Count > 0)
            {
                foreach (ProgrProduccionPT progAux in grupoAutorizada.list_ProgrProd)
                {
                    if ((progAux.grupo != null) && (progAux.accion == accion.INGRESAR || progAux.accion == accion.MODIFICAR || progAux.accion == accion.LISTADO) && (progAux.grupo.id == grupoAutorizada.grupo.id))
                    {
                        listaErrroresGrupoAutorizada.Add("El grupo a eliminar se encuentra asociada en el Programa de producción, elimine el registro asociado");
                        break;
                    }

                }
            }

            return listaErrroresGrupoAutorizada;
        }

        public List<string> validaEstructuraTecnicaDirectoSust(EstructuraTecnicaPT estructTec)
        {
            return new List<string>();
        }

        public List<string> validaArchivoAdjuntoProyectoTecnico(ArchivosAdjPT archivosAdjPT, List<ArchivosAdjPT> List_ArchivoBinario)
        {
            List<String> listaErrroresArchivo = new List<String>();
            if (archivosAdjPT != null && archivosAdjPT.idPT <= 0)
            {
                listaErrroresArchivo.Add("Debe guardar el Proyecto Técnico previamente para guardar archivos adjuntos.");
            }

            return listaErrroresArchivo;
        }





    }
}

