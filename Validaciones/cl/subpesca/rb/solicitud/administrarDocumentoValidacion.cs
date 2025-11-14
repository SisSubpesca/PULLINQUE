using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Data;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class AdministrarDocumentoValidacion
    {


        RequerimientoDA requerimientoDA = new RequerimientoDA();

        public List<String> validaModificacionRequerimiento(Requerimiento requerimiento)
        {

            List<String> errores = new List<String>();

            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA){


            }


            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {
                    //SOLO SE ESTAN ITERANDO LOS QUE SELECCIONO
                    foreach(DocumentoAmbito documentoAmbito in  requerimiento.ambitoTipo){
                        if (documentoAmbito.idDocGeneralResp > 0 && documentoAmbito.idDocGeneralResp != requerimiento.idRequerimiento){
                            errores.Add("No puede seleccionar un requerimiento que ya haya sido respondido por otra entrada. (" + documentoAmbito.idDocGeneralResp + ")");
                        }

                        //SI TIENE ASOCIADA RESPUESTAS, ENTONCES DEBER SELECIONAR UNO
                        DataTable data = requerimientoDA.ListarPosiblesRespuestasSubRequerimiento(Convert.ToInt32(documentoAmbito.tipo.id), 0);

                        if (data != null && data.Rows.Count > 0) {
                            if (documentoAmbito.estadoResultadoResp.id <= 0) {
                                errores.Add("Debe seleccionar el resultado");
                            }
                        }
                    }
                }
            }

            return errores;
            
        }

        public List<String> validaEliminacionDocumentoAsociado(DocumentoAmbito documentoAsociado)
        {

            List<String> errores = new List<String>();

            if (documentoAsociado.idDocGeneralResp > 0)
            {
                errores.Add("No puede eliminar un documento asociado que haya sido respondido.");
            }

            return errores;

        }



        public List<String> validaAdicionDocumentoAsociado(DocumentoAmbito documentoAsociadoAgregar, List<DocumentoAmbito> lista)
        {

            List<String> errores = new List<String>();

            if (lista != null && lista.Count > 0)
            {
                foreach (DocumentoAmbito documentoAmbitoLista in lista) {
                    if (documentoAmbitoLista.accion == accion.INGRESAR || documentoAmbitoLista.accion == accion.LISTADO || documentoAmbitoLista.accion == accion.MODIFICAR)
                    {
                        if (documentoAmbitoLista.ambito.id == documentoAsociadoAgregar.ambito.id && documentoAmbitoLista.tipo.id == documentoAsociadoAgregar.tipo.id)
                        {
                            errores.Add("Documento ya ingresado en lista.");
                            break;
                        }
                    }
                }
            }
            return errores;

        }


        public List<String> validarEliminacionDeRequerimiento(Requerimiento requerimiento)
        {

            List<String> errores = new List<String>();


            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {

                if (requerimiento.tipoSalida.id == rbTipo.INFORMATIVO)
                {
                    return errores;
                }

                //NO DEBEN ESTAR CONTESTADOS LOS REQUERIMIENTOS
                if (requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                {

                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        if (documentoAmbito.idDocGeneralResp > 0)
                        {
                            errores.Add("No puede eliminar requerimientos que tengan asociados una respuesta.");
                            break;
                        }
                    }

                    return errores;
                }
            }


            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {

                if (requerimiento.tipoEntrada.id == rbTipo.INGRESO_SIN_REQUERIMIENTO)
                {
                    return errores;
                }

                if (requerimiento.tipoEntrada.id == rbTipo.RESPUESTA_A_UN_REQUERIMIENTO)
                {
                    return errores;
                }
            }


            return errores;

        }


        public List<string> validarNoVigenteDeRequerimiento(Requerimiento requerimiento)
        {
            List<String> errores = new List<String>();

            return errores;
        }

    }
}
