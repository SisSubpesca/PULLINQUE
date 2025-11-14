using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades.Relocalizacion;

namespace Datos.Entidades
{
     [Serializable()]
    public class ErroresRelocalizacion
    {
         public int idError { get; set; }
         public TramiteRelocalizacion tramiteRel { get; set; }
         public DetalleSector detSector { get; set; }
         public ParametroGenerico tipoError { get; set; }
         public DateTime fechaIngreso { get; set; }
         public DateTime fechaUltimaMod { get; set; }
         public ParametroGenerico estadoError { get; set; }
         public Boolean invalidante { get; set; }
         public ParametroGenerico centro { get; set; }
         public OrigenSector origenSec { get; set; }
         public String detalleError { get { return detalleErrorToString(); } }

         public ErroresRelocalizacion() { }


         public String detalleErrorToString()
         {


            String detalle = "";

            if (this.detSector != null && this.detSector.idDetalleSector > 0)
            {
                detalle = detalle + "Sector " + detSector.numSector + ". ";
            }

            if (this.centro != null && this.centro.id > 0)
            {
                detalle = detalle + "Centro " + centro.id + ".";
            }

            if (!detalle.Equals("")) {
                detalle = "(" + detalle + ")";
            }

            return detalle;
        }


         // SUBCLASE 
         [Serializable]
         public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
         {

             public int idError { get; set; }
             public TramiteRelocalizacion tramiteRel { get; set; }
             public DetalleSector detSector { get; set; }
             public ParametroGenerico tipoError { get; set; }
             public DateTime fechaIngreso { get; set; }
             public DateTime fechaUltimaMod { get; set; }
             public ParametroGenerico estadoError { get; set; }
             public Boolean invalidante { get; set; }
             public ParametroGenerico centro { get; set; }
             public OrigenSector origenSec { get; set; }

         }

    }

}
