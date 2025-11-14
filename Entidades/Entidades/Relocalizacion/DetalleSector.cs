using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Datos.Entidades.Relocalizacion
{

    [Serializable()]
    public class DetalleSector
    {

        public int index { get; set; }
        public int accion { get; set; }
        public string numPert { get; set; }
        
        public int idDetalleSector { get; set; }
        public TramiteRelocalizacion tramiteRel { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico tipoRelocalizacion { get; set; }
        public ParametroGenerico estadoSector { get; set; }
        public int numSector { get; set; }
        public bool esSectorCero { get; set; }
        public SolicitudConcesion concesionDestino { get; set; }

        public SolicitudConcesion tramiteEnCurso { get; set; }

        public float superficieSector { get; set; }
        public int version;
        public List<OrigenSector> origenes { get; set; }
        public String origenesDetalle { get { return origenesToString();  } }
        public String titularesOrigenesDetalle { get { return titularesOrigenesToString(); } }
        public bool contieneSSp { get; set; }
        public ParametroGenerico estadoSSp { get; set; }
        public ParametroGenerico resultadoResolucion { get; set; }

        public String regionOrigenes { get { return regionOrigenesToString(); } }
        public String comunaOrigenes { get { return comunaOrigenesToString(); } }


        public string regionOrigenesToString()
        {

            string detalleOrigenes = "";

            int i = 0;
            Hashtable regiones = new Hashtable();

            if (origenes != null)
            {

                foreach (OrigenSector aOrigenSector in origenes)
                {
                    if (aOrigenSector.accion == Datos.Contantes.accion.INGRESAR || aOrigenSector.accion == Datos.Contantes.accion.MODIFICAR || aOrigenSector.accion == Datos.Contantes.accion.LISTADO)
                    {

                        if (aOrigenSector.concesionOrigen.region != null && aOrigenSector.concesionOrigen.region.id > 0 && aOrigenSector.concesionOrigen.region.descripcion != null && !regiones.ContainsKey(aOrigenSector.concesionOrigen.region.id))
                        {
                            if (i == 0)
                            {
                                detalleOrigenes = aOrigenSector.concesionOrigen.region.descripcion;
                            }
                            else
                            {
                                detalleOrigenes = detalleOrigenes + " <br>" + aOrigenSector.concesionOrigen.region.descripcion;
                            }

                            i++;

                            regiones.Add(aOrigenSector.concesionOrigen.region.id, aOrigenSector.concesionOrigen.region.id);
                        }
                    }
                }
            }

            return detalleOrigenes;
        }

        public string comunaOrigenesToString()
        {

            string detalleOrigenes = "";

            int i = 0;
            Hashtable comunas = new Hashtable();

            if (origenes != null)
            {
                foreach (OrigenSector aOrigenSector in origenes)
                {
                    if (aOrigenSector.accion == Datos.Contantes.accion.INGRESAR || aOrigenSector.accion == Datos.Contantes.accion.MODIFICAR || aOrigenSector.accion == Datos.Contantes.accion.LISTADO)
                    {

                        if (aOrigenSector.concesionOrigen.comuna != null)
                        {
                            foreach (ParametroGenerico aComuna in aOrigenSector.concesionOrigen.comuna)
                            {

                                if (aComuna != null && aComuna.id > 0 && aComuna.descripcion != null && !comunas.ContainsKey(aComuna.id))
                                {
                                    if (i == 0)
                                    {
                                        detalleOrigenes = aComuna.descripcion;
                                    }
                                    else
                                    {
                                        detalleOrigenes = detalleOrigenes + " <br>" + aComuna.descripcion;
                                    }

                                    i++;

                                    comunas.Add(aComuna.id, aComuna.id);
                                }
                            }
                        }
                    }
                }
            }

            return detalleOrigenes;
        }


        public string origenesToString()
        {

            string detalleOrigenes = "";

            int i = 0;
            foreach (OrigenSector aOrigenSector in origenes)
            {

                if (aOrigenSector.accion == Datos.Contantes.accion.INGRESAR || aOrigenSector.accion == Datos.Contantes.accion.MODIFICAR || aOrigenSector.accion == Datos.Contantes.accion.LISTADO)
                {

                    if (i == 0)
                    {
                        detalleOrigenes = aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro;
                    }
                    else
                    {
                        detalleOrigenes = detalleOrigenes + " <br>" + aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro;
                    }

                    i++;

                }

                
            }

            return detalleOrigenes;
        }

        public string titularesOrigenesToString()
        {

            string titularesOrigenes = "";
            Hashtable hashRut = new Hashtable();

            int i = 0;
            foreach (OrigenSector aOrigenSector in origenes)
            {
                if (aOrigenSector.accion == Datos.Contantes.accion.INGRESAR || aOrigenSector.accion == Datos.Contantes.accion.MODIFICAR || aOrigenSector.accion == Datos.Contantes.accion.LISTADO)
                {

                    if (aOrigenSector.concesionOrigen != null && aOrigenSector.concesionOrigen.titularesSolConcesion != null)
                    {

                        foreach (Persona aPersona in aOrigenSector.concesionOrigen.titularesSolConcesion)
                        {

                            if (!hashRut.ContainsKey(aPersona.rutPersona))
                            {

                                if (i == 0)
                                {
                                    titularesOrigenes = aPersona.rutPersona + "-" + aPersona.dvPersona + " " + aPersona.nombreSolicitante;
                                }
                                else
                                {
                                    titularesOrigenes = titularesOrigenes + " <br>" + aPersona.rutPersona + "-" + aPersona.dvPersona + " " + aPersona.nombreSolicitante;
                                }

                                i++;

                                hashRut.Add(aPersona.rutPersona, aPersona);

                            }
                        }
                    }
                }
            }

            return titularesOrigenes;
        }

          // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int index { get; set; }
            public int accion { get; set; }
        
            public int idDetalleSector { get; set; }
            public TramiteRelocalizacion tramiteRel { get; set; }
            public int idSolConcesion { get; set; }
            public ParametroGenerico tipoRelocalizacion { get; set; }
            public ParametroGenerico estadoSector { get; set; }
            public int numSector { get; set; }
            public bool esSectorCero { get; set; }
            public SolicitudConcesion concesionDestino { get; set; }
            public float superficieSector { get; set; }
            public int version;
            public List<OrigenSector> origenes { get; set; }
            public string numPert { get; set; }
            public bool contieneSSp { get; set; }
            public ParametroGenerico estadoSSp { get; set; }
            public ParametroGenerico resultadoResolucion { get; set; }

            public SolicitudConcesion tramiteEnCurso { get; set; }
        }
    }
}
