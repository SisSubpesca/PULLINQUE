using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
     [Serializable()]
    public class ValidacionDocumentacion
    {

        public int idValDocumentacion { get; set; }
        public ParametroGenerico flujoDocumental { get; set; }
        public ParametroGenerico tipoIO { get; set; }
        public ParametroGenerico tipoDestinatario { get; set; }
        public ParametroGenerico ambito { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico seccion { get; set; }
        public ParametroGenerico subRequerimiento { get; set; }
        
        public int numero { get; set; }
        public int fecha { get; set; }
        public int nuevaFecha { get; set; }//extensionFecha indica si debe aparecer el campo de extension fecha en la interfaz.
        public int numeroCI { get; set; }
        public int fechaCI { get; set; }

        public int archivoBinario { get; set; }
        public bool reqInterno { get; set; }
        public bool verificaConforme { get; set; }
        public int reqInternoFiltro { get; set; }
        public int verificaConformeFiltro { get; set; }
        public int aplicaConcesion  { get; set; }
        public int aplicaModAmpliacion { get; set; }
        public int aplicaModReduccion { get; set; }
        public int aplicaModEspeciePT { get; set; }
        public int aplicaModRegularizacion { get; set; }
        public int aplicaRelocalizacion { get; set; }
        public int aplicaAmerb { get; set; }
        public int aplicaExperimentalesAmerb { get; set; }
        public int aplicaFaenamiento { get; set; }
        public int aplicaAcopio { get; set; }
        public int aplicaColectores { get; set; }
        public int verificaAmpPlazo { get; set; }
        public int verificaAmpExtension { get; set; }
        public int aplicaExpConcesion { get; set; }
        public int aplicaAcuiculturaEcmpo { get; set; }
        public int aplicaModPT { get; set; }
        public int aplicaModECMPOAmpl { get; set; }
        public int aplicaModECMPOReduc { get; set; }
        public int aplicaModECMPOEspecie { get; set; }
        public int aplicaModECMPO_PT { get; set; }
        public int aplicaModECMPO_Regulariz { get; set; }
        public int aplicaModAcopioAmpl { get; set; }
        public int aplicaModAcopioReduc { get; set; }
        public int aplicaModAcopioEspecie { get; set; }
        public int aplicaModAcopio_PT { get; set; }
        public int aplicaModAcopioRegulariz { get; set; }
        public int aplicaModFaenamAmpl { get; set; }
        public int aplicaModFaenamReduc { get; set; }
        public int aplicaModFaenamEspecie { get; set; }
        public int aplicaModFaenam_PT { get; set; }
        public int aplicaModFaenamRegulariz { get; set; }
        public int aplicaModAmerbAmpl { get; set; }
        public int aplicaModAmerbReduc { get; set; }
        public int aplicaModAmerbEspecie { get; set; }
        public int aplicaModAmerb_PT { get; set; }
        public int aplicaModAmerbRegulariz { get; set; }


        public string seccionString {
            get {
                return seccion.descripcion;
            }
        }

        public string nuevaFechaString
        {

            get
            {
                if (nuevaFecha > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string verificaAmpPlazoString
        {

            get
            {
                if (verificaAmpPlazo > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string verificaAmpExtensionString
        {

            get
            {
                if (verificaAmpExtension > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string archivoString {
            get {
                if (archivoBinario == 0)
                {
                    return "Campo No Aplica";
                }
                else if (archivoBinario == 1)
                {
                    return "Campo Aplica y es Obligatorio";
                }
                else
                {
                    return "Campo Aplica y es Opcional";

                }
            }
        }

        public int idPestana
        {
            get {
                return ambito.id;
            }
        }

        public string flujoDocumentalString {
            get {
                return flujoDocumental.descripcion;

            }
        }

        public string nombreTipoIO {
            get {
                return tipoIO.descripcion;
            }
        }

        public string nombreTipoOrigen {
            get
            {
                return tipoDestinatario.descripcion;
            }
        }


        public string nombreTipoDestinatario {
            get {
                return tipoDestinatario.descripcion;
            }
        }

        public string nombrePestana {
            get {
                return ambito.descripcion;
            }
        }

        public string nombreSubRequerimiento {
            get {
                return subRequerimiento.descripcion;
            }
        }

        public string nombreTipoDocResp {
            get {
                return tipoDocumento.descripcion;
            }
        }


        public string numeroString {

            get { 
                if(numero == 0){
                    return "Campo No Aplica";
                }
                else if (numero == 1)
                {
                    return "Campo Aplica y es Obligatorio";
                }
                else {
                    return "Campo Aplica y es Opcional";
                
                }
            }
        }

        public string fechaString
        {

            get
            {
                if (fecha == 0)
                {
                    return "Campo No Aplica";
                }
                else if (fecha == 1)
                {
                    return "Campo Aplica y es Obligatorio";
                }
                else
                {
                    return "Campo Aplica y es Opcional";

                }
            }
        }

        public string numeroCIString
        {

            get
            {
                if (numeroCI == 0)
                {
                    return "Campo No Aplica";
                }
                else if (numeroCI == 1)
                {
                    return "Campo Aplica y es Obligatorio";
                }
                else
                {
                    return "Campo Aplica y es Opcional";

                }
            }
        }

        public string fechaCIString
        {

            get
            {
                if (fechaCI == 0)
                {
                    return "Campo No Aplica";
                }
                else if (fechaCI == 1)
                {
                    return "Campo Aplica y es Obligatorio";
                }
                else
                {
                    return "Campo Aplica y es Opcional";

                }
            }
        }

        public string verificaConformeString {

            get
            {
                if (verificaConforme)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaConcesionString
        {

            get
            {
                if (aplicaConcesion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAmpliacionString
        {

            get
            {
                if (aplicaModAmpliacion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModReduccionString
        {

            get
            {
                if (aplicaModReduccion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModEspeciePTString
        {

            get
            {
                if (aplicaModEspeciePT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModRegularizacionString
        {

            get
            {
                if (aplicaModRegularizacion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaRelocalizacionString
        {

            get
            {
                if (aplicaRelocalizacion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaAmerbString
        {

            get
            {
                if (aplicaAmerb > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaExperimentalesAmerbString
        {

            get
            {
                if (aplicaExperimentalesAmerb > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaExperimentalesConcesionString
        {

            get
            {
                if (aplicaExpConcesion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaFaenamientoString
        {

            get
            {
                if (aplicaFaenamiento > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaAcopioString
        {

            get
            {
                if (aplicaAcopio > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaColectoresString
        {

            get
            {
                if (aplicaColectores > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModPTString
        {

            get
            {
                if (aplicaModPT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaExpAmerbString
        {

            get
            {
                if (aplicaExperimentalesAmerb > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaExpConcesionString
        {

            get
            {
                if (aplicaExpConcesion > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaECMPOString
        {

            get
            {
                if (aplicaAcuiculturaEcmpo > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModECMPOAmpliacionString
        {

            get
            {
                if (aplicaModECMPOAmpl > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModECMPOReduccionString
        {

            get
            {
                if (aplicaModECMPOReduc > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModECMPOEspecieString
        {

            get
            {
                if (aplicaModECMPOEspecie > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModECMPOPTString
        {

            get
            {
                if (aplicaModECMPO_PT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModECMPORegularizacionString
        {

            get
            {
                if (aplicaModECMPO_Regulariz > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAcopioAmpliacionString
        {

            get
            {
                if (aplicaModAcopioAmpl > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModAcopioReduccionString
        {

            get
            {
                if (aplicaModAcopioReduc > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAcopioEspecieString
        {

            get
            {
                if (aplicaModAcopioEspecie > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModAcopioPTString
        {

            get
            {
                if (aplicaModAcopio_PT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAcopioRegularizacionString
        {

            get
            {
                if (aplicaModAcopioRegulariz > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModFaenamientoAmpliacionString
        {

            get
            {
                if (aplicaModFaenamAmpl > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModFaenamientoReduccionString
        {

            get
            {
                if (aplicaModFaenamReduc > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModFaenamientoEspecieString
        {

            get
            {
                if (aplicaModFaenamEspecie > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModFaenamientoPTString
        {

            get
            {
                if (aplicaModFaenam_PT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModFaenamientoRegularizacionString
        {

            get
            {
                if (aplicaModFaenamRegulariz > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAmerbAmpliacionString
        {

            get
            {
                if (aplicaModAmerbAmpl > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaModAmerbReduccionString
        {

            get
            {
                if (aplicaModAmerbReduc > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModAmerbEspecieString
        {

            get
            {
                if (aplicaModAmerbEspecie > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModAmerbPTString
        {

            get
            {
                if (aplicaModAmerb_PT > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string aplicaModAmerbRegularizacionString
        {

            get
            {
                if (aplicaModAmerbRegulariz > 0)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public ValidacionDocumentacion() {

        }


        public void reseteaFiltros()
        {

            numero = -1;
            fecha = -1;
            nuevaFecha = -1; 
            numeroCI = -1;
            fechaCI = -1;
            archivoBinario = -1;
            aplicaConcesion = -1;
            aplicaModAmpliacion = -1;
            aplicaModReduccion = -1;
            aplicaModEspeciePT = -1;
            aplicaModRegularizacion = -1;
            aplicaRelocalizacion = -1;
            aplicaAmerb = -1;
            aplicaExperimentalesAmerb = -1;
            aplicaFaenamiento = -1;
            aplicaAcopio = -1;
            aplicaColectores = -1;
            verificaAmpPlazo = -1;
            verificaAmpExtension = -1;
            aplicaExpConcesion = -1;
            aplicaAcuiculturaEcmpo = -1;
            aplicaModPT = -1;
            aplicaModECMPOAmpl = -1;
            aplicaModECMPOReduc = -1;
            aplicaModECMPOEspecie = -1;
            aplicaModECMPO_PT = -1;
            aplicaModECMPO_Regulariz = -1;
            aplicaModAcopioAmpl = -1;
            aplicaModAcopioReduc = -1;
            aplicaModAcopioEspecie = -1;
            aplicaModAcopio_PT = -1;
            aplicaModAcopioRegulariz = -1;
            aplicaModFaenamAmpl = -1;
            aplicaModFaenamReduc = -1;
            aplicaModFaenamEspecie = -1;
            aplicaModFaenam_PT = -1;
            aplicaModFaenamRegulariz = -1;
            aplicaModAmerbAmpl = -1;
            aplicaModAmerbReduc = -1;
            aplicaModAmerbEspecie = -1;
            aplicaModAmerb_PT = -1;
            aplicaModAmerbRegulariz = -1;
        }


        //SUBCLASE
        [Serializable]
         public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {
             public int idValDocumentacion { get; set; }
             public ParametroGenerico flujoDocumental { get; set; }
             public ParametroGenerico tipoIO { get; set; }
             public ParametroGenerico tipoDestinatario { get; set; }
             public ParametroGenerico ambito { get; set; }
             public ParametroGenerico tipoDocumento { get; set; }
             public ParametroGenerico seccion { get; set; }
             public ParametroGenerico subRequerimiento { get; set; }
             public int numero { get; set; }
             public ParametroGenerico tipoDoc { get; set; }
             public int fecha { get; set; }
             public int nuevaFecha { get; set; }
             public int numeroCI { get; set; }
             public int fechaCI { get; set; }
             public int archivoBinario { get; set; }
             public bool reqInterno { get; set; }
             public bool verificaConforme { get; set; }
             public int aplicaConcesion { get; set; }
             public int aplicaModAmpliacion { get; set; }
             public int aplicaModReduccion { get; set; }
             public int aplicaModEspeciePT { get; set; }
             public int aplicaModRegularizacion { get; set; }
             public int aplicaRelocalizacion { get; set; }
             public int aplicaAmerb { get; set; }
             public int aplicaExperimentalesAmerb { get; set; }
             public int aplicaFaenamiento { get; set; }
             public int aplicaAcopio { get; set; }
             public int aplicaColectores { get; set; }
             public int verificaAmpPlazo { get; set; }
             public int verificaAmpExtension { get; set; }
             public int aplicaExpConcesion { get; set; }
             public int aplicaAcuiculturaEcmpo { get; set; }
             public int aplicaModPT { get; set; }
             public int aplicaModECMPOAmpl { get; set; }
             public int aplicaModECMPOReduc { get; set; }
             public int aplicaModECMPOEspecie { get; set; }
             public int aplicaModECMPO_PT { get; set; }
             public int aplicaModECMPO_Regulariz { get; set; }
             public int aplicaModAcopioAmpl { get; set; }
             public int aplicaModAcopioReduc { get; set; }
             public int aplicaModAcopioEspecie { get; set; }
             public int aplicaModAcopio_PT { get; set; }
             public int aplicaModAcopioRegulariz { get; set; }
             public int aplicaModFaenamAmpl { get; set; }
             public int aplicaModFaenamReduc { get; set; }
             public int aplicaModFaenamEspecie { get; set; }
             public int aplicaModFaenam_PT { get; set; }
             public int aplicaModFaenamRegulariz { get; set; }
             public int aplicaModAmerbAmpl { get; set; }
             public int aplicaModAmerbReduc { get; set; }
             public int aplicaModAmerbEspecie { get; set; }
             public int aplicaModAmerb_PT { get; set; }
             public int aplicaModAmerbRegulariz { get; set; }
             public int reqInternoFiltro { get; set; }
             public int verificaConformeFiltro { get; set; }

         }


    }
}
