using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.common;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.mantenedores
{
    public class MantenedorTitularService
    {

        SolicitanteDA solicitanteDA = new SolicitanteDA();
        OperadorDA operadorDA = new OperadorDA();
        RepLegalDA repLegalDA = new RepLegalDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        MatrizSucursalDA matrizSucursalDA = new MatrizSucursalDA();
        ContactoDA contactoDA = new ContactoDA();

        Logger logger = new Logger();

        /* Método que busca los datos sernapesca de la persona jurídica */
        public Solicitante buscarPersonaJuridica(int rutPersona, char dvPersona)
        {
            try
            {
                return solicitanteDA.ObtenerVistaRepLegal_Persona(rutPersona, dvPersona);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<MatrizSucursal> obtenerContactoMatrizSucursal(MatrizSucursal matrizSucursal)
        {
            
                try
                {

                    if (matrizSucursal != null)
                    {

                        if (matrizSucursal.bp == 1)//Titular
                        {
                            return null;
                        }
                        else if (matrizSucursal.bp == 2)//Representante Legal
                        {
                            return null;
                        }
                        else if (matrizSucursal.bp == 3)//Operador
                        {
                            return null;
                        }
                        
                    
                    }
                    
                return null;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return null;
                }
            
        }



        public bool eliminaTitular(int rutPersona)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    Solicitante solicitante = new Solicitante();
                    solicitante.rut = rutPersona;

                    solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                    /* Eliminar todos los nombres de la persona titular */
                    if (!solicitanteDA.EliminarNombrePersona(solicitante.rut))
                    {
                        return false;
                    }

                    solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                    solicitante.matrizSucursales = matrizSucursalDA.ListarTitularMatrizSuc(solicitante.rut);
                    if (solicitante.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in solicitante.matrizSucursales)
                        {
                            //matrizSucursal.contactosMatrizSuc = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);
                            //if (matrizSucursal.contactosMatrizSuc != null)
                            //{
                            //    foreach (Contacto contacto in matrizSucursal.contactosMatrizSuc)
                            //    {
                            //        /* Eliminar Relación entre Matriz y Contacto */
                            //        if (!matrizSucursalDA.EliminarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                            //        /* Eliminar Relación entre Titular y Contacto */
                            //        if (!solicitanteDA.EliminarContactoPersona(solicitante.rut, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                            //        /* Eliminar Contacto */
                            //        if (!contactoDA.EliminarContacto(contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);
                            //    }
                            //}

                            /* Eliminar Relación Titular y Matriz Sucursal */
                            if (!solicitanteDA.EliminarTitularMatrizSuc(matrizSucursal.idMatrizSuc, solicitante.rut))
                            {
                                return false;
                            }

                            solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                            /* Eliminar Matriz Sucursal */
                            if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }

                            solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);
                        }
                    }

                    /* Se eliminan los contactos del titular */
                    solicitante.listaContacto = solicitanteDA.ListarContactoPersona(solicitante.rut, 0);
                    if (solicitante.listaContacto != null)
                    {
                        foreach (Contacto contacto in solicitante.listaContacto)
                        {
                            /* Eliminar Relación entre titular y Contacto */
                            if (!solicitanteDA.EliminarContactoPersona(solicitante.rut,contacto.idContacto))
                            {
                                return false;
                            }

                            /* Eliminar Contactos */
                            if (!contactoDA.EliminarContacto(contacto.idContacto))
                            {
                                return false;
                            }
                        }
                    }

                    solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);

                    solicitante.listaRepresentanteLegal = repLegalDA.ListarRepresentanteTitular(solicitante.rut);
                    if (solicitante.listaRepresentanteLegal != null)
                    {
                        foreach (RepLegal repLegal in solicitante.listaRepresentanteLegal)
                        {

                            /* Eliminar Relación Representante Legal Titular */
                            if (!repLegalDA.EliminarRepLegalTitular(solicitante.rut, repLegal.representanteLegal.rut))
                            {
                                return false;
                            }
                        }
                    }

                    solicitante.listaOperador = operadorDA.ListarOperadorTitular(solicitante.rut, 0);
                    if (solicitante.listaOperador != null)
                    {
                        foreach (Operador operador in solicitante.listaOperador)
                        {

                            /* Eliminar Relación Operador Titular */
                            if (!operadorDA.EliminarOperadorTitular(solicitante.rut, operador.operador.rut))
                            {
                                return false;
                            }
                        }
                    }

                    solicitante.listaArchivosAdjTitular = solicitanteDA.ListarArchivosAdjuntoTitular(solicitante.rut, 0);
                    if (solicitante.listaArchivosAdjTitular != null)
                    {
                        foreach (ArchivosAdjTitular archivosAdjTitular in solicitante.listaArchivosAdjTitular)
                        {
                            /* Eliminar Relación del Archivo Físico con Titular */
                            if (!solicitanteDA.EliminarArchivosAdjTitular(solicitante.rut, archivosAdjTitular.idArchivo))
                            {
                                return false;
                            }
                        }
                    }

                    /* Elimina Titular */
                    if (!solicitanteDA.EliminarPersona(rutPersona))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool eliminarRepresentanteLegal(int rutRepresentanteLegal)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    RepLegal repLegal = new RepLegal();
                    repLegal.representanteLegal = new Solicitante();
                    repLegal.representanteLegal.rut = rutRepresentanteLegal;

                    if (!repLegalDA.EliminarNombreRepresentante(repLegal.representanteLegal.rut))
                    {
                        return false;
                    }

                    repLegal.representanteLegal.matrizSucursales = repLegalDA.ListarRepresentanteMatrizSuc(rutRepresentanteLegal);
                    if (repLegal.representanteLegal.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in repLegal.representanteLegal.matrizSucursales)
                        {

                            //matrizSucursal.contactosMatrizSuc = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);
                            //if(matrizSucursal.contactosMatrizSuc != null){
                            //    foreach (Contacto contacto in matrizSucursal.contactosMatrizSuc)
                            //    {

                            //        /* Eliminar Relación entre Matriz y Contacto */
                            //        if (!matrizSucursalDA.EliminarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        /* Eliminar Relación entre Representante Legal y Contacto */
                            //        if (!repLegalDA.EliminarContactoRepresentante(repLegal.representanteLegal.rut, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        /* Eliminar Contactos */
                            //        if (!contactoDA.EliminarContacto(contacto.idContacto))
                            //        {
                            //            return false;
                            //        }
                            //    }
                            //}

                            /* Eliminar Relación Matriz y Representante Legal */
                            if (!repLegalDA.EliminarTitularMatrizSuc(matrizSucursal.idMatrizSuc, repLegal.representanteLegal.rut))
                            {
                                return false;
                            }

                            /* Eliminar Matriz Sucursal */
                            if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }
                        }
                    }

                    /* Se eliminan los contactos del representante legal */
                    repLegal.representanteLegal.listaContacto = repLegalDA.ListarContactoRepresentante(repLegal.representanteLegal.rut, 0);
                    if (repLegal.representanteLegal.listaContacto != null)
                    {
                        foreach (Contacto contacto in repLegal.representanteLegal.listaContacto)
                        {
                            /* Eliminar Relación entre representante legal y Contacto */
                            if (!repLegalDA.EliminarContactoRepresentante(repLegal.representanteLegal.rut,contacto.idContacto))
                            {
                                return false;
                            }

                            /* Eliminar Contactos */
                            if (!contactoDA.EliminarContacto(contacto.idContacto))
                            {
                                return false;
                            }
                        }
                    }

                    /* Guarda Archivo Adjunto */
                    repLegal.representanteLegal.listaArchivosAdjRep = repLegalDA.ListarArchivosAdjuntoPersona(repLegal.representanteLegal.rut,0);
                    if (repLegal.representanteLegal.listaArchivosAdjRep != null)
                    {
                        foreach (ArchivosAdjRepLegal archivosAdjRepLegal in repLegal.representanteLegal.listaArchivosAdjRep)
                        {
                            /* Elimina Relación del Archivo Físico con Representante Legal */
                            if (!repLegalDA.EliminarRepresentanteArchivo(repLegal.representanteLegal.rut, archivosAdjRepLegal.idArchivo))
                            {
                                return false;
                            }

                        }
                    }

                    /* Elimina Representante Legal */
                    if (!repLegalDA.EliminarRepresentanteLegal(rutRepresentanteLegal))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool eliminaOperador(int rutOperador)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    Operador operador = new Operador();
                    operador.operador = new Solicitante();
                    operador.operador.rut = rutOperador;

                    if (!operadorDA.EliminarNombreOperador(operador.operador.rut)) {
                        return false;
                    }

                    operador.operador.matrizSucursales = operadorDA.ListarOperadorMatrizSuc(rutOperador);
                    if (operador.operador.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in operador.operador.matrizSucursales)
                        {

                            //matrizSucursal.contactosMatrizSuc = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);
                            //foreach (Contacto contacto in matrizSucursal.contactosMatrizSuc)
                            //{

                            //    /* Eliminar Relación entre Matriz y Contacto */
                            //    if (!matrizSucursalDA.EliminarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                            //    {
                            //        return false;
                            //    }

                            //    /* Eliminar Relación entre Operador y Contacto */
                            //    if (!operadorDA.EliminarContactoOperador(operador.operador.rut, contacto.idContacto))
                            //    {
                            //        return false;
                            //    }

                            //    /* Eliminar Contactos */
                            //    if (!contactoDA.EliminarContacto(contacto.idContacto))
                            //    {
                            //        return false;
                            //    }

                            //}

                            /* Eliminar Relación entre Matriz Sucursal y Operador */
                            if (!operadorDA.EliminarOperadorMatrizSuc(matrizSucursal.idMatrizSuc, operador.operador.rut))
                            {
                                return false;
                            }

                            /* Eliminar Matriz Sucursal */
                            if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }
                        }
                    }
                    
                    /* Se eliminan los contactos del operador */
                    operador.operador.listaContacto = operadorDA.ListarContactoOperador(rutOperador,0);
                    if (operador.operador.listaContacto != null)
                    {
                        foreach (Contacto contacto in operador.operador.listaContacto)
                        {
                            /* Eliminar Relación entre Operador y Contacto */
                            if (!operadorDA.EliminarContactoOperador(operador.operador.rut, contacto.idContacto))
                            {
                                return false;
                            }

                            /* Eliminar Contactos */
                            if (!contactoDA.EliminarContacto(contacto.idContacto))
                            {
                                return false;
                            }
                        }
                    }

                    /* Guarda Archivo Adjunto */
                    operador.operador.listaArchivosAdjOperador = operadorDA.ListarArchivosAdjuntoPersona(rutOperador,0);
                    if (operador.operador.listaArchivosAdjOperador != null)
                    {
                        foreach (ArchivosAdjOperador archivosAdjOperador in operador.operador.listaArchivosAdjOperador)
                        {
                            /* Eliminar Relación del Archivo Físico con Operador */
                            if (!operadorDA.EliminarOperadorArchivo(operador.rutOperador, archivosAdjOperador.idArchivo))
                            {
                                return false;
                            }
                        }
                    }

                    /* Eliminar Operador */
                    if (!operadorDA.EliminarOperador(rutOperador))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        
        
        
        public bool guardarTitular(Solicitante solicitante)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Guarda Titular */
                    if (!solicitanteDA.GuardarTitular(solicitante))
                    {
                        return false;
                    }

                    /* Guarda Nombre Persona */
                    NombrePersona nombrePersona = new NombrePersona();
                    nombrePersona.rut = solicitante.rut;
                    nombrePersona.nombre = solicitante.nombreSolicitante;
                    //nombrePersona.numeroCI = solicitante.numeroControlIngreso;
                    //nombrePersona.fechaCI = solicitante.fechaControlIngreso;
                    nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    if (!solicitanteDA.GuardarNombrePersona(nombrePersona))
                    {
                        return false;
                    }


                    if (solicitante.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in solicitante.matrizSucursales)
                        {

                            ///* Guardar Archivo Físico de Matriz Sucursal */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                            //{
                            //    return false;
                            //}

                            /* Guarda Matriz Sucursal */
                            if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                            {
                                return false;
                            }

                            /*  Guarda Relación entre Matriz y Titular */
                            if (!matrizSucursalDA.GuardarTitularMatrizSuc(matrizSucursal.idMatrizSuc,solicitante.rut))
                            {
                                return false;
                            }
                        }
                    }

                    if (solicitante.listaContacto != null)
                    {
                        foreach (Contacto contacto in solicitante.listaContacto)
                        {

                            if (!contactoDA.GuardarContacto(contacto))
                            {
                                return false;
                            }

                            /* Guarda Relación entre Titular y Contacto */
                            if (!solicitanteDA.GuardarContactoPersona(solicitante.rut, contacto.idContacto))
                            {
                                return false;
                            }

                        }
                    }

                    if (solicitante.listaRepresentanteLegal != null)
                    {
                        foreach (RepLegal repLegal in solicitante.listaRepresentanteLegal)
                        {

                            /* Guardar Archivo Físico del Representante Legal */
                            //if (repLegal.archivoAsoc != null)
                            //{
                            //    if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(repLegal.archivoAsoc))
                            //    {
                            //        return false;
                            //    }
                            //}

                            /* Guarda Relación Representante Legal Titular */
                            repLegal.titular = new Solicitante();
                            repLegal.titular.rut = solicitante.rut;
                            if (!repLegalDA.GuardarRepLegalTitular(repLegal))
                            {
                                return false;
                            }

                        }
                    }

                    /* Guarda Relación Operador Titular */
                    if (solicitante.listaOperador != null)
                    {
                        foreach (Operador operador in solicitante.listaOperador)
                        {

                            ///* Guardar Archivo Físico del Operador */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(operador.archivoAsoc))
                            //{
                            //    return false;
                            //}

                            /* Guarda Relación Operador Titular */
                            operador.titular = new Solicitante();
                            operador.titular.rut = solicitante.rut;
                            if (!operadorDA.GuardarOperadorTitular(operador))
                            {
                                return false;
                            }

                        }
                    }

                    if (solicitante.listaArchivosAdjTitular != null)
                    {
                        foreach (ArchivosAdjTitular archivosAdjTitular in solicitante.listaArchivosAdjTitular)
                        {

                            /* Guarda Archivo Adjuntos Físico del Titular */
                            if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjTitular.archivoBinario))
                            {
                                return false;
                            }

                            /* Guarda Relación del Archivo Físico con Titular */
                            if (!solicitanteDA.GuardarArchivosAdjTitular(solicitante.rut, archivosAdjTitular.archivoBinario.idArchivo, archivosAdjTitular.tipoDocumento.id, archivosAdjTitular.numCI, archivosAdjTitular.fechaCI, rbEstadosGenerales.VIGENTE))
                            {
                                return false;
                            }

                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool guadarRepresentanteLegal(RepLegal repLegal)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Guarda Representante Legal */
                    if (!repLegalDA.GuardarRepresentanteLegal(repLegal.representanteLegal))
                    {
                        return false;
                    }

                    /* Guarda Nombre Persona */
                    NombrePersona nombrePersona = new NombrePersona();
                    nombrePersona.rut = repLegal.representanteLegal.rut;
                    nombrePersona.nombre = repLegal.representanteLegal.nombreSolicitante;
                    nombrePersona.numeroCI = repLegal.representanteLegal.numeroControlIngreso;
                    nombrePersona.fechaCI = repLegal.representanteLegal.fechaControlIngreso;
                    nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    if (!repLegalDA.GuardarNombreRepresentante(nombrePersona))
                    {
                        return false;
                    }


                    /* Listado de direcciones */
                    if (repLegal.representanteLegal.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in repLegal.representanteLegal.matrizSucursales)
                        {

                            ///* Guardar Archivo Físico de Matriz Sucursal */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                            //{
                            //    return false;
                            //}

                            /* Guarda Matriz Sucursal */
                            if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                            {
                                return false;
                            }

                            /*  Guarda Relación entre Matriz y Representante Legal */
                            if (!repLegalDA.GuardarRepresentanteMatrizSuc(repLegal.representanteLegal.rut, matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }
                        }
                    }

                    /* Listado de Contactos*/
                    if (repLegal.representanteLegal.listaContacto != null)
                    {
                        foreach (Contacto contacto in repLegal.representanteLegal.listaContacto)
                        {

                            /* Guarda Contactos */
                            if (!contactoDA.GuardarContacto(contacto))
                            {
                                return false;
                            }

                            /* Guarda Relación entre Representante Legal y Contacto */
                            if (!repLegalDA.GuardarContactoRepresentante(repLegal.representanteLegal.rut, contacto.idContacto))
                            {
                                return false;
                            }
                        }
                    }

                    if (repLegal.representanteLegal.listaArchivosAdjRep != null)
                    {
                        /* Guarda Archivo Adjunto */
                        foreach (ArchivosAdjRepLegal archivosAdjRepLegal in repLegal.representanteLegal.listaArchivosAdjRep)
                        {

                            /* Guarda Archivo Adjuntos Físico del Representante Legal */
                            if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjRepLegal.archivoBinario))
                            {
                                return false;
                            }

                            /* Guarda Relación del Archivo Físico con Representante Legal */
                            if (!repLegalDA.GuardarRepresentanteArchivo(repLegal.representanteLegal.rut, archivosAdjRepLegal.archivoBinario.idArchivo, archivosAdjRepLegal.tipoDocumento.id, archivosAdjRepLegal.numCI, archivosAdjRepLegal.fechaCI))
                            {
                                return false;
                            }

                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool guardarOperador(Operador operador)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    /* Guarda Operador */
                    if (!operadorDA.GuardarOperador(operador.operador))
                    {
                        return false;
                    }

                    /* Guarda Nombre Persona */
                    NombrePersona nombrePersona = new NombrePersona();
                    nombrePersona.rut = operador.operador.rut;
                    nombrePersona.nombre = operador.operador.nombreSolicitante;
                    nombrePersona.numeroCI = operador.operador.numeroControlIngreso;
                    nombrePersona.fechaCI = operador.operador.fechaControlIngreso;
                    nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                    if (!operadorDA.GuardarNombreOperador(nombrePersona))
                    {
                        return false;
                    }

                    if (operador.operador.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in operador.operador.matrizSucursales)
                        {

                            ///* Guardar Archivo Físico de Matriz Sucursal */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                            //{
                            //    return false;
                            //}

                            /* Guarda Matriz Sucursal */
                            if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                            {
                                return false;
                            }

                            if (!operadorDA.GuardarOperadorMatrizSuc(operador.operador.rut, matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }

                            //if (matrizSucursal.contactosMatrizSuc != null)
                            //{
                            //    foreach (Contacto contacto in matrizSucursal.contactosMatrizSuc)
                            //    {

                            //        /* Guarda Contactos */
                            //        if (!contactoDA.GuardarContacto(contacto))
                            //        {
                            //            return false;
                            //        }

                            //        /* Guarda Relación entre Matriz y Contacto */
                            //        if (!matrizSucursalDA.GuardarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }

                            //        /* Guarda Relación entre Operador y Contacto */
                            //        if (!operadorDA.GuardarContactoOperador(operador.operador.rut, contacto.idContacto))
                            //        {
                            //            return false;
                            //        }
                            //    }
                            //}

                        }
                    }

                    if (operador.operador.listaContacto != null)
                    {
                        foreach (Contacto contacto in operador.operador.listaContacto)
                        {
                            /* Guarda Contactos */
                            if (!contactoDA.GuardarContacto(contacto))
                            {
                                return false;
                            }

                            /* Guarda Relación entre Operador y Contacto */
                            if (!operadorDA.GuardarContactoOperador(operador.operador.rut, contacto.idContacto))
                            {
                                return false;
                            }
                        }
                    }

                    if (operador.operador.listaArchivosAdjOperador != null)
                    {
                        /* Guarda Archivo Adjunto */
                        foreach (ArchivosAdjOperador archivosAdjOperador in operador.operador.listaArchivosAdjOperador)
                        {

                            /* Guarda Archivo Adjuntos Físico del Representante Legal */
                            if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjOperador.archivoBinario))
                            {
                                return false;
                            }

                            /* Guarda Relación del Archivo Físico con Representante Legal */
                            if (!operadorDA.GuardarOperadorArchivo(operador.operador.rut, archivosAdjOperador.archivoBinario.idArchivo, archivosAdjOperador.tipoDocumento.id, archivosAdjOperador.numCI, archivosAdjOperador.fechaCI))
                            {
                                return false;
                            }

                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }

        }

        

        public bool cambiarEstadoTitular(Solicitante solicitante)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!solicitanteDA.ActualizarTitularEstado(solicitante.rut, solicitante.estadoPersona.id))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool cambiarEstadoRepresentante(RepLegal repLegal)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!repLegalDA.ActualizarRepresentanteLegalEstado(repLegal.representanteLegal.rut, repLegal.representanteLegal.estadoPersona.id))
                    {
                        return true;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool cambiarEstadoOperador(Operador operador)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!operadorDA.ActualizarOperadorEstado(operador.operador.rut, operador.operador.estadoPersona.id))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool modificarTitular(Solicitante solicitante)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Modificar Titular */
                    if (!solicitanteDA.GuardarTitular(solicitante))
                    {
                        return false;
                    }

                    /* Modificar Nombre Persona, será agregado un registro solo si fue realizada la acción desde la funcionalidad Modificar Nombre Solicitante. */


                    if (solicitante.listaNombrePersona != null)
                    {
                        foreach (NombrePersona nombrePersona in solicitante.listaNombrePersona)
                        {

                            ///* Guardar Archivo Físico del Nombre */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(nombrePersona.archivo))
                            //{
                            //    return false;
                            //}
                            /* Guardar Nombre de la Persona */
                            nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                            nombrePersona.rut = solicitante.rut;
                            if (!solicitanteDA.GuardarNombrePersona(nombrePersona))
                            {
                                return false;
                            }
                        }
                    }
                    else {
                        /* Solo actualiza el registro ya existente */
                        if (solicitante.nombreSolicitante != null && solicitante.nombreTitular != null && !solicitante.nombreSolicitante.Equals(solicitante.nombreTitular))
                        {
                            NombrePersona nombrePersona = solicitanteDA.ObtenerNombrePersona(solicitante.rut, rbEstadosGenerales.VIGENTE);
                            nombrePersona.nombre = solicitante.nombreSolicitante;

                            if (!solicitanteDA.GuardarNombrePersona(nombrePersona))
                            {
                                return false;
                            }
                        }
                    }
                    

                    if (solicitante.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in solicitante.matrizSucursales)
                        {

                            if (matrizSucursal.accion == accion.INGRESAR)
                            {

                                ///* Guardar Archivo Físico de Matriz Sucursal */
                                //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                                //{
                                //    return false;
                                //}

                                /* Guardar Matriz Sucursal */
                                if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                                {
                                    return false;
                                }

                                /* Guardar Relación Titular Matriz Sucursal */
                                if (!matrizSucursalDA.GuardarTitularMatrizSuc(matrizSucursal.idMatrizSuc, solicitante.rut))
                                {
                                    return false;
                                }
                            }

                            else if (matrizSucursal.accion == accion.ELIMINAR)
                            {
                                /* Eliminar Relación Titular y Matriz Sucursal */
                                if (!solicitanteDA.EliminarTitularMatrizSuc(matrizSucursal.idMatrizSuc, solicitante.rut))
                                {
                                    return false;
                                }

                                /* Eliminar Matriz Sucursal */
                                if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                                {
                                    return false;
                                }

                            }
                        }
                    }

                    if (solicitante.listaContacto != null)
                    {
                        foreach (Contacto contacto in solicitante.listaContacto)
                        {
                            if (contacto.accion == accion.INGRESAR)
                            {
                                if (!contactoDA.GuardarContacto(contacto))
                                {
                                    return false;
                                }

                                /* Guarda Relación entre Titular y Contacto */
                                if (!solicitanteDA.GuardarContactoPersona(solicitante.rut, contacto.idContacto))
                                {
                                    return false;
                                }
                            }

                            if (contacto.accion == accion.ELIMINAR)
                            {
                                /* Eliminar Relación entre Titular y Contacto */
                                if (!solicitanteDA.EliminarContactoPersona(solicitante.rut, contacto.idContacto))
                                {
                                    return false;
                                }

                                /* Eliminar Contacto */
                                if (!contactoDA.EliminarContacto(contacto.idContacto))
                                {
                                    return false;
                                }
                            }
                        }
                    }


                    if (solicitante.listaRepresentanteLegal != null)
                    {
                        foreach (RepLegal repLegal in solicitante.listaRepresentanteLegal)
                        {
                            if (repLegal.accion == accion.INGRESAR)
                            {
                                ///* Guardar Archivo Físico del Representante Legal */
                                //if (repLegal.archivoAsoc != null)
                                //{
                                //    if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(repLegal.archivoAsoc))
                                //    {
                                //        return false;
                                //    }
                                //}

                                /* Guarda Relación Representante Legal Titular */
                                repLegal.titular = new Solicitante();
                                repLegal.titular.rut = solicitante.rut;
                                if (!repLegalDA.GuardarRepLegalTitular(repLegal))
                                {
                                    return false;
                                }
                            }
                            else if (repLegal.accion == accion.ELIMINAR)
                            {
                                /* Eliminar Relación Representante Legal Titular */
                                if (!repLegalDA.EliminarRepLegalTitular(solicitante.rut, repLegal.representanteLegal.rut))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    /* Guarda Relación Operador Titular */
                    if (solicitante.listaOperador != null)
                    {
                        foreach (Operador operador in solicitante.listaOperador)
                        {

                            if (operador.accion == accion.INGRESAR)
                            {
                                ///* Guardar Archivo Físico del Operador */
                                //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(operador.archivoAsoc))
                                //{
                                //    return false;
                                //}

                                /* Guarda Relación Operador Titular */
                                operador.titular = new Solicitante();
                                operador.titular.rut = solicitante.rut;
                                if (!operadorDA.GuardarOperadorTitular(operador))
                                {
                                    return false;
                                }
                            }

                            else if (operador.accion == accion.ELIMINAR)
                            {

                                /* Eliminar Relación Operador Titular */
                                if (!operadorDA.EliminarOperadorTitular(solicitante.rut, operador.operador.rut))
                                {
                                    return false;
                                }
                            }

                        }
                    }

                    if (solicitante.listaArchivosAdjTitular != null)
                    {
                        foreach (ArchivosAdjTitular archivosAdjTitular in solicitante.listaArchivosAdjTitular)
                        {
                            if (archivosAdjTitular.accion == accion.INGRESAR)
                            {
                                /* Guarda Archivo Adjuntos Físico del Titular */
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjTitular.archivoBinario))
                                {
                                    return false;
                                }

                                /* Guarda Relación del Archivo Físico con Titular */
                                if (!solicitanteDA.GuardarArchivosAdjTitular(solicitante.rut, archivosAdjTitular.archivoBinario.idArchivo, archivosAdjTitular.tipoDocumento.id, archivosAdjTitular.numCI, archivosAdjTitular.fechaCI, rbEstadosGenerales.VIGENTE))
                                {
                                    return false;
                                }
                            }
                            else if (archivosAdjTitular.accion == accion.MODIFICAR)
                            {

                                /* Eliminar Relación del Archivo Físico con Titular */
                                if (!solicitanteDA.ActualizarEstadoArchivosAdjTitular(solicitante.rut, archivosAdjTitular.idArchivo, archivosAdjTitular.estadoVigencia.id))
                                {
                                    return false;
                                }
                            }

                            else if (archivosAdjTitular.accion == accion.ELIMINAR)
                            {

                                /* Eliminar Relación del Archivo Físico con Titular */
                                if (!solicitanteDA.EliminarArchivosAdjTitular(solicitante.rut, archivosAdjTitular.idArchivo))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool modificarRepresentanteLegal(RepLegal repLegal)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Modificar Representante Legal */
                    if (!repLegalDA.GuardarRepresentanteLegal(repLegal.representanteLegal))
                    {
                        return false;
                    }

                    if (repLegal.representanteLegal.listaNombrePersona != null)
                    {
                        foreach (NombrePersona nombrePersona in repLegal.representanteLegal.listaNombrePersona)
                        {

                            ///* Guarda Archivo Físico del Nombre */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(nombrePersona.archivo))
                            //{
                            //    return false;
                            //}

                            /* Guarda Nombre Persona */
                            nombrePersona.rut = repLegal.representanteLegal.rut;
                            nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                            if (!repLegalDA.GuardarNombreRepresentante(nombrePersona))
                            {
                                return false;
                            }
                        }
                    }
                    else {
                        /* Solo actualiza el registro ya existente */
                        if (repLegal.representanteLegal.nombreSolicitante != null && repLegal.representanteLegal.nombreTitular != null && !repLegal.representanteLegal.nombreSolicitante.Equals(repLegal.representanteLegal.nombreTitular))
                        {
                            NombrePersona nombrePersona = repLegalDA.ObtenerNombreRepresentante(repLegal.representanteLegal.rut, rbEstadosGenerales.VIGENTE);
                            nombrePersona.nombre = repLegal.representanteLegal.nombreSolicitante;

                            if (!repLegalDA.GuardarNombreRepresentante(nombrePersona))
                            {
                                return false;
                            }
                        }
                    
                    
                    }

                    foreach (MatrizSucursal matrizSucursal in repLegal.representanteLegal.matrizSucursales)
                    {

                        if (matrizSucursal.accion == accion.INGRESAR)
                        {
                            ///* Guardar Archivo Físico de Matriz Sucursal */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                            //{
                            //    return false;
                            //}

                            /* Guarda Matriz Sucursal */
                            if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                            {
                                return false;
                            }

                            /*  Guarda Relación entre Matriz y Representante Legal */
                            if (!repLegalDA.GuardarRepresentanteMatrizSuc(repLegal.representanteLegal.rut, matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }
                        }
                        else if (matrizSucursal.accion == accion.ELIMINAR) {

                            //List<Contacto> listContactos = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);

                            //foreach (Contacto contacto in listContactos)
                            //{

                            //    ///* Eliminar Relación entre Matriz y Contacto */
                            //    //if (!matrizSucursalDA.EliminarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                            //    //{
                            //    //    return false;
                            //    //}

                            //    /* Eliminar Relación entre Representante Legal y Contacto */
                            //    if (!repLegalDA.EliminarContactoRepresentante(repLegal.representanteLegal.rut, contacto.idContacto))
                            //    {
                            //        return false;
                            //    }

                            //    /* Eliminar Contactos */
                            //    if (!contactoDA.EliminarContacto(contacto.idContacto))
                            //    {
                            //        return false;
                            //    }

                            //}

                            /* Eliminar Relación Matriz y Representante Legal */
                            if (!repLegalDA.EliminarTitularMatrizSuc(matrizSucursal.idMatrizSuc, repLegal.representanteLegal.rut))
                            {
                                return false;
                            }

                            /* Eliminar Matriz Sucursal */
                            if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                            {
                                return false;
                            }
                        }

                    }

                    /* Guardar Contactos al Representante Legal */
                    if (repLegal.representanteLegal.listaContacto != null && repLegal.representanteLegal.listaContacto.Count > 0) {
                        foreach (Contacto contacto in repLegal.representanteLegal.listaContacto)
                        {
                            if (contacto.accion == accion.INGRESAR)
                            {

                                /* Guarda Contactos */
                                if (!contactoDA.GuardarContacto(contacto))
                                {
                                    return false;
                                }

                                /* Guarda Relación entre Representante Legal y Contacto */
                                if (!repLegalDA.GuardarContactoRepresentante(repLegal.representanteLegal.rut, contacto.idContacto))
                                {
                                    return false;
                                }

                            }
                            else if (contacto.accion == accion.ELIMINAR) {

                                if (!repLegalDA.EliminarContactoRepresentante(repLegal.representanteLegal.rut, contacto.idContacto))
                                {
                                    return false;
                                }

                                if (!contactoDA.EliminarContacto(contacto.idContacto))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    
                    /* Guarda Archivo Adjunto */
                    if (repLegal.representanteLegal.listaArchivosAdjRep != null && repLegal.representanteLegal.listaArchivosAdjRep.Count > 0)
                    {
                        foreach (ArchivosAdjRepLegal archivosAdjRepLegal in repLegal.representanteLegal.listaArchivosAdjRep)
                        {
                            if (archivosAdjRepLegal.accion == accion.INGRESAR)
                            {
                                /* Guarda Archivo Adjuntos Físico del Representante Legal */
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjRepLegal.archivoBinario))
                                {
                                    return false;
                                }

                                /* Guarda Relación del Archivo Físico con Representante Legal */
                                if (!repLegalDA.GuardarRepresentanteArchivo(repLegal.representanteLegal.rut, archivosAdjRepLegal.archivoBinario.idArchivo, archivosAdjRepLegal.tipoDocumento.id, archivosAdjRepLegal.numCI, archivosAdjRepLegal.fechaCI))
                                {
                                    return false;
                                }
                            }
                            else if (archivosAdjRepLegal.accion == accion.ELIMINAR)
                            {

                                /* Elimina Relación del Archivo Físico con Representante Legal */
                                if (!repLegalDA.EliminarRepresentanteArchivo(repLegal.representanteLegal.rut, archivosAdjRepLegal.idArchivo))
                                {
                                    return false;
                                }
                            }

                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool modificarOperador(Operador operador)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    /* Modificar Operador */
                    if (!operadorDA.GuardarOperador(operador.operador))
                    {
                        return false;
                    }

                    if (operador.operador.listaNombrePersona != null)
                    {
                        foreach (NombrePersona nombrePersona in operador.operador.listaNombrePersona)
                        {

                            ///* Guardar Archivo Físico del Nombre */
                            //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(nombrePersona.archivo))
                            //{
                            //    return false;
                            //}

                            /* Guarda Nombre Persona */
                            nombrePersona.estadoActual = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                            nombrePersona.rut = operador.operador.rut;
                            if (!operadorDA.GuardarNombreOperador(nombrePersona))
                            {
                                return false;
                            }
                        }
                    }
                    else {
                        /* Solo actualiza el registro ya existente */
                        if (operador.operador.nombreSolicitante != null && operador.operador.nombreTitular != null && !operador.operador.nombreSolicitante.Equals(operador.operador.nombreTitular))
                        {
                            NombrePersona nombrePersona = operadorDA.ObtenerNombreOperador(operador.operador.rut, rbEstadosGenerales.VIGENTE);
                            nombrePersona.nombre = operador.operador.nombreSolicitante;

                            if (!operadorDA.GuardarNombreOperador(nombrePersona))
                            {
                                return false;
                            }
                        }
                    
                    }

                    if (operador.operador.matrizSucursales != null)
                    {
                        foreach (MatrizSucursal matrizSucursal in operador.operador.matrizSucursales)
                        {

                            if (matrizSucursal.accion == accion.INGRESAR)
                            {
                                ///* Guardar Archivo Físico de Matriz Sucursal */
                                //if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(matrizSucursal.archivoBinarioEspecial))
                                //{
                                //    return false;
                                //}

                                /* Guarda Matriz Sucursal */
                                if (!matrizSucursalDA.GuardarMatrizSucursal(matrizSucursal))
                                {
                                    return false;
                                }

                                /* Guardar Relación entre Operador Matriz y Sucursal*/
                                if (!operadorDA.GuardarOperadorMatrizSuc(operador.operador.rut, matrizSucursal.idMatrizSuc))
                                {
                                    return false;
                                }

                                
                            }
                            else if (matrizSucursal.accion == accion.ELIMINAR)
                            {

                                //List<Contacto> listaContactos = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);

                                //foreach (Contacto contacto in listaContactos)
                                //{

                                //    ///* Eliminar Relación entre Matriz y Contacto */
                                //    //if (!matrizSucursalDA.EliminarContactoMatrizSuc(matrizSucursal.idMatrizSuc, contacto.idContacto))
                                //    //{
                                //    //    return false;
                                //    //}

                                //    /* Eliminar Relación entre Operador y Contacto */
                                //    if (!operadorDA.EliminarContactoOperador(operador.operador.rut, contacto.idContacto))
                                //    {
                                //        return false;
                                //    }

                                //    /* Eliminar Contactos */
                                //    if (!contactoDA.EliminarContacto(contacto.idContacto))
                                //    {
                                //        return false;
                                //    }

                                //}

                                /* Eliminar Relación entre Matriz Sucursal y Operador */
                                if (!operadorDA.EliminarOperadorMatrizSuc(matrizSucursal.idMatrizSuc, operador.operador.rut))
                                {
                                    return false;
                                }

                                /* Eliminar Matriz Sucursal */
                                if (!matrizSucursalDA.EliminarMatrizSucursal(matrizSucursal.idMatrizSuc))
                                {
                                    return false;
                                }
                            }

                        }
                    }

                    /* Guardar Contactos del Operador */
                    if (operador.operador.listaContacto != null)
                    {
                        foreach (Contacto contacto in operador.operador.listaContacto)
                        {
                            if (contacto.accion == accion.INGRESAR)
                            {
                                if (!contactoDA.GuardarContacto(contacto))
                                {
                                    return false;
                                }

                                /* Guarda Relación entre Operador y Contacto */
                                if (!operadorDA.GuardarContactoOperador(operador.operador.rut, contacto.idContacto))
                                {
                                    return false;
                                }
                            }
                            else if (contacto.accion == accion.ELIMINAR) {

                                if (!operadorDA.EliminarContactoOperador(operador.operador.rut, contacto.idContacto))
                                {
                                    return false;
                                }

                                if (!contactoDA.EliminarContacto(contacto.idContacto))
                                {
                                    return false;
                                }
                            }
                        }
                    }


                    /* Guarda Archivo Adjunto */
                    if (operador.operador.listaArchivosAdjOperador != null)
                    {
                        foreach (ArchivosAdjOperador archivosAdjOperador in operador.operador.listaArchivosAdjOperador)
                        {

                            if (archivosAdjOperador.accion == accion.INGRESAR)
                            {
                                /* Guarda Archivo Adjuntos Físico del Operador */
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitudEspecial(archivosAdjOperador.archivoBinario))
                                {
                                    return false;
                                }

                                /* Guarda Relación del Archivo Físico con Operador */
                                if (!operadorDA.GuardarOperadorArchivo(operador.operador.rut, archivosAdjOperador.archivoBinario.idArchivo, archivosAdjOperador.tipoDocumento.id, archivosAdjOperador.numCI, archivosAdjOperador.fechaCI)) 
                                {
                                    return false;
                                }
                            }
                            else if (archivosAdjOperador.accion == accion.ELIMINAR)
                            {

                                /* Eliminar Relación del Archivo Físico con Operador */
                                if (!operadorDA.EliminarOperadorArchivo(operador.operador.rut, archivosAdjOperador.idArchivo))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public List<MatrizSucursal> listarMatrizConContactos(int rutTitular)
        {
            try
            {

                List<MatrizSucursal> listaMatrizSucursalTitular = matrizSucursalDA.ListarTitularMatrizSuc(rutTitular);
                //foreach (MatrizSucursal matrizSucursal in listaMatrizSucursalTitular)
                //{
                //    List<Contacto> listaContactos = matrizSucursalDA.ListarContactoMatrizSuc(matrizSucursal.idMatrizSuc);
                //    matrizSucursal.contactosMatrizSuc = listaContactos;
                //}

                return listaMatrizSucursalTitular;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<Contacto> listarContactosTitular(int rutTitular)
        {
            try
            {

                List<Contacto> listaContactoTitular = solicitanteDA.ListarContactoPersona(rutTitular,0);

                return listaContactoTitular;
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