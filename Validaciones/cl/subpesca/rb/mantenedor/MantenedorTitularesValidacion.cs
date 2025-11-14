using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;

namespace Validaciones.cl.subpesca.rb.mantenedor
{
    public class MantenedorTitularesValidacion
    {
        
        
        
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        RepLegalDA repLegalDA = new RepLegalDA();
        OperadorDA operadorDA = new OperadorDA();
        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        
     

        public List<string> validaRepresentanteLegal(Datos.Entidades.RepLegal repLegal)
        {
            List<String> listaErrroresRepLegal = new List<String>();
            if (repLegal != null)
            {
                if (repLegal.representanteLegal != null && repLegal.representanteLegal.accion == accion.INGRESAR)
                {
                    /* El representante legal no puede existir previamente en el sistema */
                    RepLegal representante = repLegalDA.ObtieneRepresentanteLegal(repLegal.representanteLegal.rut,"");
                    if (representante != null)
                    {
                        listaErrroresRepLegal.Add("El Representante Legal ingresado ya se encuentra almacenado en el sistema.");
                    }
                    
                    ///* El representante legal debe tener al menos una matriz y sucursal */
                    //if (repLegal.representanteLegal != null && (repLegal.representanteLegal.matrizSucursales == null || repLegal.representanteLegal.matrizSucursales.Count <= 0))
                    //{
                    //    listaErrroresRepLegal.Add("El Representante Legal debe tener al menos una matriz y sucursal.");
                    //}

                    ///* El representante legal debe tener al menos un archivo adjunto*/
                    //if (repLegal.representanteLegal != null && (repLegal.representanteLegal.listaArchivosAdjRep == null || repLegal.representanteLegal.listaArchivosAdjRep.Count <= 0))
                    //{
                    //    listaErrroresRepLegal.Add("El Representante Legal debe tener al menos un archivo adjunto.");
                    //}
                }

                if (repLegal.representanteLegal != null && repLegal.representanteLegal.accion == accion.MODIFICAR)
                {
                    

                    ///* El representante legal debe tener al menos una matriz y sucursal. */
                    //if (repLegal.representanteLegal != null && (repLegal.representanteLegal.matrizSucursales == null || repLegal.representanteLegal.matrizSucursales.Count <= 0))
                    //{
                    //    listaErrroresRepLegal.Add("El Representante Legal debe tener al menos una matriz y sucursal.");
                    //}

                    ///* El representante legal debe tener al menos un archivo adjunto. */
                    //if (repLegal.representanteLegal != null && (repLegal.representanteLegal.listaArchivosAdjRep == null || repLegal.representanteLegal.listaArchivosAdjRep.Count <= 0))
                    //{
                    //    listaErrroresRepLegal.Add("El Representante Legal debe tener al menos un archivo adjunto.");
                    //}
                }

                if (repLegal.representanteLegal != null && repLegal.representanteLegal.accion == accion.ELIMINAR)
                {
                    /* No puede eliminar si tiene un titular asociado */
                    System.Data.DataTable dt = solicitanteDA.ObtenerRepresentantesLegales(0, repLegal.representanteLegal.rut);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        listaErrroresRepLegal.Add("No puede Eliminar el Representante Legal por estar asociado a un Titular.");
                    }
                }
            }
            return listaErrroresRepLegal;
        }

        public List<string> validaOperador(Datos.Entidades.Operador operador)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (operador != null)
            {
                if (operador.operador != null && operador.operador.accion == accion.INGRESAR)
                {
                    /* El operador no debe estar ingresado en el sistema previamente*/
                    Operador op = operadorDA.ObtenerOperador(operador.operador.rut,"");
                    if (op != null)
                    {
                        listaErrroresOperador.Add("El Operador ingresado ya se encuentra almacenado en el sistema.");
                    }

                    ///* El Operador debe tener al menos un archivo adjunto */

                    //if (operador.operador != null && (operador.operador.listaArchivosAdjOperador == null || operador.operador.listaArchivosAdjOperador.Count <= 0))
                    //{
                    //    listaErrroresOperador.Add("El ingreso de archivo archivo adjunto del Operador es obligatorio.");
                    //}
                }

                if (operador.operador != null && operador.operador.accion == accion.MODIFICAR)
                {
                    ///* El Operador debe tener al menos un archivo adjunto */
                    //if (operador.operador != null && (operador.operador.listaArchivosAdjOperador == null || operador.operador.listaArchivosAdjOperador.Count <= 0))
                    //{
                    //    listaErrroresOperador.Add("El ingreso de archivo archivo adjunto del Operador es obligatorio.");
                    //}
                }

                if (operador.operador != null && operador.operador.accion == accion.ELIMINAR)
                {
                    /* No puede eliminar si tiene un titular asociado */
                    List<Operador> listaOperadoresTitular = operadorDA.ListarOperadorTitular(0, operador.operador.rut);
                    if (listaOperadoresTitular != null && listaOperadoresTitular.Count > 0)
                    {
                        listaErrroresOperador.Add("No puede Eliminar el Operador por estar asociado a un Titular.");
                    }
                }
            }
            return listaErrroresOperador;
        }

        public List<string> validaArchivoAdjuntoTitular(ArchivosAdjTitular archivosAdjTitular, List<Datos.Entidades.ArchivosAdjTitular> List_ArchivoBinario)
        {
            List<String> listaErrroresArchivoAdjuntoTitular = new List<String>();
            if(archivosAdjTitular != null && List_ArchivoBinario != null){
            
                

                /* El Archivo Adjunto es obligatorio */
                if (archivosAdjTitular.archivoBinario != null && archivosAdjTitular.archivoBinario.tamano <= 0)
                {
                    listaErrroresArchivoAdjuntoTitular.Add("Debe ingresar el Archivo Adjunto del Titular.");
                }

                /* No debe repetirse los tipos de archivos */
                bool existeTipoArchivo = false;
                foreach (ArchivosAdjTitular archivosAdjTitularAux in List_ArchivoBinario)
                {
                    if (archivosAdjTitularAux.tipoDocumento != null && archivosAdjTitular.tipoDocumento != null && archivosAdjTitularAux.tipoDocumento.id == archivosAdjTitular.tipoDocumento.id)
                    {
                        existeTipoArchivo = true;
                    }
                }
                if (existeTipoArchivo)
                {
                    //listaErrroresArchivoAdjuntoTitular.Add("El Tipo de Archivo Adjunto ingresado ya se encuentra en el sistema.");
                }
            }
            return listaErrroresArchivoAdjuntoTitular;
        }

        public List<string> validaMatrizSucursal(Datos.Entidades.MatrizSucursal matrizSucursal)
        {
            List<String> listaErrroresmatrizSucursal = new List<String>();
            if (matrizSucursal != null)
            {
                //if (matrizSucursal.archivoBinarioEspecial == null || matrizSucursal.archivoBinarioEspecial.tamano <=0)
                //{
                //    listaErrroresmatrizSucursal.Add("Debe ingresar Archivo Adjunto asociado a la Matriz Sucursal.");
                //}

                //if (matrizSucursal.contactosMatrizSuc == null || matrizSucursal.contactosMatrizSuc.Count <= 0)
                //{
                //    listaErrroresmatrizSucursal.Add("Debe ingresar al menos un Contacto para la Matriz Sucursal.");
                //}

                //DateTime systemDate = DateTime.Now;

                ///* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                //if (matrizSucursal.fechaCI != default(DateTime) && matrizSucursal.fechaCI > systemDate)
                //{
                //    listaErrroresmatrizSucursal.Add("La Fecha CI no debe ser mayor a la fecha de hoy.");
                //}


            }
            return listaErrroresmatrizSucursal;
        }

        public List<string> validaRutTitular(int rut)
        {
            List<String> listaErrroresTitular = new List<String>();

            if (rut > 0)
            {
                /* Titular ya se encuentra almacenado en el sistema */
                Solicitante titular = solicitanteDA.ObtenerPersona(rut, 0);
                if (titular != null)
                {
                    listaErrroresTitular.Add("Titular ingresado ya se encuentra almacenado en el sistema.");
                }
            }
            else {
                listaErrroresTitular.Add("Debe ingresar rut del Titular.");
            }

            return listaErrroresTitular;
        }

        public List<string> validaTitular(Datos.Entidades.Solicitante solicitante)
        {
            List<String> listaErrroresTitular = new List<String>();

            if (solicitante != null)
            {
                if (solicitante.accion == accion.INGRESAR)
                {
                    
                    /* Para crear un nuevo titular, los requisitos min son: rut, nombre, genero (si es que aplica), 1 direccion (esto para personas naturales), para
                       personas jurídicas rut, nombre 1 direccion y 1 representante legal */

                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                    {
                        if (solicitante.rut <= 0)
                        {
                            listaErrroresTitular.Add("Se debe ingresar el rut del Titular.");
                        }
                        if (solicitante.nombreSolicitante == null || solicitante.nombreSolicitante.Equals(""))
                        {
                            listaErrroresTitular.Add("Se debe ingresar el nombre del Titular.");
                        }

                        //if (solicitante.matrizSucursales == null || solicitante.matrizSucursales.Count <= 0)
                        //{
                        //    listaErrroresTitular.Add("Se debe ingresar al menos una matriz y sucursal.");
                        //}
                    }

                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                    {
                        if (solicitante.rut <= 0)
                        {
                            listaErrroresTitular.Add("Se debe ingresar el rut del Titular.");
                        }
                        if (solicitante.nombreSolicitante == null || solicitante.nombreSolicitante.Equals(""))
                        {
                            listaErrroresTitular.Add("Se debe ingresar el nombre del Titular.");
                        }

                        /*
                        if (solicitante.numeroControlIngreso <= 0)
                        {
                            listaErrroresTitular.Add("Se debe ingresar el número control de ingreso del Titular.");
                        }
                        if (solicitante.fechaControlIngreso == null || solicitante.nombreSolicitante.Equals(""))
                        {
                            listaErrroresTitular.Add("Se debe ingresar la fecha control de ingreso del Titular.");
                        }
                        */

                        //if (solicitante.listaRepresentanteLegal == null || solicitante.listaRepresentanteLegal.Count<= 0)
                        //{
                        //    listaErrroresTitular.Add("Se debe asociar al menos un Representante Legal.");
                        //}
                    }

                }

                if (solicitante.accion == accion.MODIFICAR)
                {

                    /* Para crear un nuevo titular, los requisitos min son: rut, nombre, genero (si es que aplica), 1 direccion (esto para personas naturales), para
                       personas jurídicas rut, nombre 1 direccion y 1 representante legal */

                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_NATURAL)
                    {
                        if (solicitante.rut <= 0)
                        {
                            listaErrroresTitular.Add("Se debe ingresar el rut del Titular.");
                        }
                        if (solicitante.nombreSolicitante == null || solicitante.nombreSolicitante.Equals(""))
                        {
                            listaErrroresTitular.Add("Se debe ingresar el nombre del Titular.");
                        }

                        //if (solicitante.matrizSucursales == null || solicitante.matrizSucursales.Count <= 0)
                        //{
                        //    listaErrroresTitular.Add("Se debe ingresar al menos una matriz y sucursal.");
                        //}
                    }

                    if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                    {
                        if (solicitante.rut <= 0)
                        {
                            listaErrroresTitular.Add("Se debe ingresar el rut del Titular.");
                        }
                        if (solicitante.nombreSolicitante == null || solicitante.nombreSolicitante.Equals(""))
                        {
                            listaErrroresTitular.Add("Se debe ingresar el nombre del Titular.");
                        }
                        //if (solicitante.numeroControlIngreso <= 0)
                        //{
                        //    listaErrroresTitular.Add("Se debe ingresar el número control de ingreso del Titular.");
                        //}
                        //if (solicitante.fechaControlIngreso == null || solicitante.nombreSolicitante.Equals(""))
                        //{
                        //    listaErrroresTitular.Add("Se debe ingresar la fecha control de ingreso del Titular.");
                        //}
                        //if (solicitante.listaRepresentanteLegal == null || solicitante.listaRepresentanteLegal.Count <= 0)
                        //{
                        //    listaErrroresTitular.Add("Se debe asociar al menos un Representante Legal.");
                        //}
                    }

                }

                if (solicitante.accion == accion.ELIMINAR)
                {
                    /* Titular no debe estar asociado a una solicitud */
                    Solicitante titular = solicitanteDA.ObtenerSolicitante(0, solicitante.rut, "", 0);
                    if (titular != null)
                    {
                        listaErrroresTitular.Add("No puede Eliminar el Titular por estar asociado a una Solicitud.");
                    }
                }
            }
            return listaErrroresTitular;
        }

        public List<string> validaNombrePersona(Datos.Entidades.NombrePersona nombrePersona)
        {
            List<String> listaErrroresArchivoAdjuntoNomprePers = new List<String>();
            if (nombrePersona != null)
            {
            
            }
            return listaErrroresArchivoAdjuntoNomprePers;
        }

        public List<string> validaArchivoAdjuntoRepresentanteLegal(ArchivosAdjRepLegal archivosAdjRepLegal, List<Datos.Entidades.ArchivosAdjRepLegal> List_ArchivoBinario)
        {
            List<String> listaErrroresArchivoAdjuntoRepLegal = new List<String>();
            if (archivosAdjRepLegal != null && List_ArchivoBinario != null)
            {

                /* El Archivo Adjunto es obligatorio */
                if (archivosAdjRepLegal.archivoBinario != null && archivosAdjRepLegal.archivoBinario.tamano <= 0)
                {
                    listaErrroresArchivoAdjuntoRepLegal.Add("Debe ingresar el Archivo Adjunto del Representante Legal.");
                }

                bool existeTipoArchivo = false;
                foreach (ArchivosAdjRepLegal archivosAdjRepLegalAux in List_ArchivoBinario)
                {
                    if (archivosAdjRepLegalAux.tipoDocumento != null && archivosAdjRepLegal.tipoDocumento != null && archivosAdjRepLegalAux.tipoDocumento.id == archivosAdjRepLegal.tipoDocumento.id)
                    {
                        existeTipoArchivo = true;
                    }
                }
                if (existeTipoArchivo)
                {
                    listaErrroresArchivoAdjuntoRepLegal.Add("Ya existe el tipo de archivo asociado al Representante Legal.");
                }

            }

            return listaErrroresArchivoAdjuntoRepLegal;
        }

        public List<string> validaArchivoAdjuntoOperador(Datos.Entidades.ArchivosAdjOperador archivosAdjOperador, List<Datos.Entidades.ArchivosAdjOperador> List_ArchivoBinario)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (archivosAdjOperador != null && List_ArchivoBinario != null)
            {
                /* El Archivo Adjunto es obligatorio */
                if (archivosAdjOperador.archivoBinario != null && archivosAdjOperador.archivoBinario.tamano <= 0)
                {
                    listaErrroresOperador.Add("Debe ingresar el Archivo Adjunto del Operador.");
                }

                foreach (ArchivosAdjOperador archivosAdjOperadorAux in List_ArchivoBinario)
                {
                    if (archivosAdjOperadorAux.tipoDocumento != null && archivosAdjOperador.tipoDocumento != null && archivosAdjOperadorAux.tipoDocumento.id == archivosAdjOperador.tipoDocumento.id)
                    {
                        listaErrroresOperador.Add("Ya existe tipo de archivo seleccionado asociado al Operador.");
                    }
                }
            }
            return listaErrroresOperador;
        }

        public List<string> validaCambiarEstadoTitular(Datos.Entidades.Solicitante solicitante)
        {
            List<String> listaErrroresTitular = new List<String>();

            if (solicitante != null)
            {
                if (solicitante.estadoPersona != null && solicitante.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    Solicitante titular = solicitanteDA.ObtenerSolicitante(0, solicitante.rut,"",0);
                    if (titular != null)
                    {
                        listaErrroresTitular.Add("No puede Cambiar de Estado No Vigente el Titular por estar asociado a una Solicitud.");
                    }
                }
            }

            return listaErrroresTitular;
        }

        public List<string> validaEstadoRepresentanteLegal(RepLegal repLegal)
        {
            List<String> listaErrroresRepLegal = new List<String>();
            if (repLegal != null)
            {
                if (repLegal.representanteLegal.estadoPersona != null && repLegal.representanteLegal.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    /* No puede eliminar si tiene un titular asociado */
                    System.Data.DataTable dt = solicitanteDA.ObtenerRepresentantesLegales(0, repLegal.representanteLegal.rut);
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        listaErrroresRepLegal.Add("No puede Cambiar de Estado a No Vigente el Representante Legal por estar asociado a un Titular.");
                    }
                }
            }
            return listaErrroresRepLegal;
        }

        public List<string> validaCambiarEstadoOperador(Operador operador)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (operador != null)
            {
                if (operador.operador.estadoPersona != null && operador.operador.estadoPersona.id == rbEstadosGenerales.NO_VIGENTE)
                {
                    /* No puede cambiar de estado a no vigente si tiene un titular asociado */
                    List<Operador> listaOperadoresTitular = operadorDA.ListarOperadorTitular(0, operador.operador.rut);
                    if (listaOperadoresTitular != null && listaOperadoresTitular.Count > 0)
                    {
                        listaErrroresOperador.Add("No puede Cambiar de Estado a No Vigente el Operador por estar asociado a un Titular.");
                    }
                }
            }
            return listaErrroresOperador;
        }

        public List<string> validaAsociacionRepresentanteLegalTitular(RepLegal repLegal, List<RepLegal> listaRepresentantesLegales)
        {
            List<String> listaErrroresRepLegal = new List<String>();
            if (repLegal != null)
            {
                /*
                
                if (repLegal.archivoAsoc == null || repLegal.archivoAsoc.tamano <= 0) {
                    listaErrroresRepLegal.Add("Debe ingresar del Archivo Adjunto de la asociación entre el Titular y el Representante Legal");
                }
                */


                if (listaRepresentantesLegales != null)
                {

                    foreach (RepLegal repLegalAux in listaRepresentantesLegales)
                    {

                        if (repLegalAux.accion == accion.INGRESAR || repLegalAux.accion == accion.LISTADO || repLegalAux.accion == accion.MODIFICAR)
                        {
                            if (repLegalAux.rutPersonaRepLegal == repLegal.rutPersonaRepLegal)
                            {
                                listaErrroresRepLegal.Add("Esta asociación ya existe");
                                break;
                            }
                        }
                    }
                }
            }


            return listaErrroresRepLegal;
        }

        public List<string> validaAsociacionOperadorTitular(Operador operador, List<Operador> listaOperadores)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (operador != null)
            {
                ///* Es obligatorio el ingreso del archivo adjunto de la asociación entre el titular y el Operador*/
                //if (operador.archivoAsoc == null || operador.archivoAsoc.tamano <= 0)
                //{
                //    listaErrroresOperador.Add("Debe ingresar del Archivo Adjunto de la asociación entre el Titular y el Operador");
                //}


                if (listaOperadores != null)
                {

                    foreach (Operador operadorAux in listaOperadores)
                    {

                        if (operadorAux.accion == accion.INGRESAR || operadorAux.accion == accion.LISTADO || operadorAux.accion == accion.MODIFICAR)
                        {
                            if (operadorAux.rutOperador == operador.rutOperador)
                            {
                                listaErrroresOperador.Add("Esta asociación ya existe");
                                break;
                            }
                        }
                    }
                }
            }
            return listaErrroresOperador;
        }

        public List<string> validaRutRepresentanteLegal(RepLegal repLegal)
        {
            List<String> listaErrroresRepLegal = new List<String>();
            if (repLegal != null)
            {
                /* El representante debe existir en el sistema */
                RepLegal representante = null;

                if (repLegal.representanteLegal != null && repLegal.representanteLegal.rut > 0)
                {
                    representante = repLegalDA.ObtieneRepresentanteLegal(repLegal.representanteLegal.rut, "");
                }

                if (representante == null)
                {
                    listaErrroresRepLegal.Add("El Representante Legal no se encuentra registrado en el sistema.");

                }
                else
                {

                    try
                    {
                        repLegal.representanteLegal.nombreSolicitante = representante.nombreRepresentante;
                    }
                    catch (Exception ex) { }

                }
            }
            return listaErrroresRepLegal;
        }

        public List<string> validaRutOperador(Operador operador)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (operador != null)
            {

                Operador op = null;
                if (operador.operador != null && operador.operador.rut > 0)
                {
                    op = operadorDA.ObtenerOperador(operador.operador.rut, "");
                }
                if (op == null)
                {
                    listaErrroresOperador.Add("El Operador no se encuentra registrado en el sistema.");
                }
                else
                {

                    try
                    {
                        operador.operador.nombreSolicitante = op.nombreOperador;
                    }
                    catch (Exception ex) { }

                }
            }
            return listaErrroresOperador;
        }

        public List<string> validaEliminacionArchivoAdjuntoTitular(ArchivosAdjTitular archivosAdjTitular, List<ArchivosAdjTitular> List_ArchivosAdjTitular)
        {
            return new List<string>();
        }

        public List<string> validaEliminacionArchivoAdjuntoRepresentanteLegal(ArchivosAdjRepLegal archivosAdjRepLegal, List<ArchivosAdjRepLegal> List_ArchivosAdjRepLegal)
        {
            return new List<string>();
        }

        public List<string> validaEliminacionArchivoAdjuntoOperador(ArchivosAdjOperador archivosAdjOperador, List<ArchivosAdjOperador> List_ArchivosAdjOperador)
        {
            return new List<string>();
        }

        public List<string> validaEliminacionSucursalMatrizSucursal(MatrizSucursal matrizSucursal)
        {
            return new List<string>();
        }

        public List<string> validaEliminacionRepresentanteLegal(RepLegal repLegal)
        {
            return new List<string>();
        }

        public List<string> validaEliminacionOperador(Operador operador)
        {
            return new List<string>();
        }

        public List<string> validaContactoMatrizSucursal(Contacto contacto, List<Contacto> List_DireccionMatriz)
        {
            List<String> listaErrroresOperador = new List<String>();
            if (contacto != null && List_DireccionMatriz != null)
            {
            
               
            }
            return listaErrroresOperador;
        }

        public List<string> validaEliminacionMatrizSucursal(MatrizSucursal matrizSucursal)
        {
            List<String> listaErrroresMatrizSucursal = new List<String>();
            if (matrizSucursal != null && listaErrroresMatrizSucursal != null)
            {


            }
            return listaErrroresMatrizSucursal;
        }

        public List<string> validaContacto(List<Contacto> listaContacto, Contacto contacto)
        {
            return new List<string>();   
        }

        public List<string> validaEliminacionContacto(Contacto contacto)
        {
            return new List<string>(); 
        }
    }
}
