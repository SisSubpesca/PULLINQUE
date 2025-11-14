using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;

namespace Datos.Entidades
{

    [Serializable()]
    public class AsociacionSolicitudBarrio
    {

        public int idSolConcesion { get; set; }
        public ParametroGenerico region { get; set; }
        public ParametroGenerico barrioACS { get; set; }
        public ParametroGenerico barrioACM { get; set; }
        public ParametroGenerico tipo { get; set; }
        public ParametroGenerico tipoUnidadEspacial { get; set; }
        public ParametroGenerico tipoSolicitud { get; set; }
        public ParametroGenerico tipoModificacionUE { get; set; }//indica el tipo de modificación, si es PT, Especie, etc.
        public String codigoCentro { get; set; }
        public String numPert { get; set; }
        public String numeroIdentificador { get; set; }
        public Int32 numSector { get; set; }
        public string DescripcionNumSector { get { return numSectorToString(); } }

        public List<ParametroGenerico> tipoModificacionesTram { get; set; }
        public string DescripcionTipoModificacion { get { return tipoModificacionString(); } }

        public string numSectorToString()
        {

            string numSector = "";

            if (this.tipoSolicitud != null && (this.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || this.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA))
            {
                numSector = Convert.ToString(this.numSector);
            }

            return numSector;
        }


        public string tipoModificacionString()
        {
            string tipoModificacionLista = "";
            int i = 0;

            if (tipoModificacionesTram != null)
            {
                foreach (ParametroGenerico tipoModificacion in tipoModificacionesTram)
                {
                    if (tipoModificacion != null && tipoModificacion.id == 90)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_ESPECIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 91)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_PT;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 92)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_AMPLIA_SUPERFICIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 93)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_REDUCE_SUPERFICIE;
                    }
                    else if (tipoModificacion != null && tipoModificacion.id == 94)
                    {
                        tipoModificacion.descripcion = cadenas.MOD_CONCESION_REGULARIZACION;
                    }

                    if (i == 0)
                    {
                        tipoModificacionLista = tipoModificacion.descripcion;
                    }
                    else
                    {
                        tipoModificacionLista = tipoModificacionLista + ", " + tipoModificacion.descripcion;
                    }
                    i++;
                }
            }

            return tipoModificacionLista;
        }



    }
}
