using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Poligono
    {
        public Usuario usuario { get; set; }
        public int idPoligono { get; set; }
        public int idPoligAntecSector { get; set; }
        public int idCoordenadaGeo { get; set; }
        public int idSolicitud { get; set; }
        public List<ParametroGenerico> tipoConcesion { get; set; }
        public ParametroGenerico tipoUso { get; set; }
        public ParametroGenerico estado { get; set; }
        public String toponimio { get; set; }
        public float areaCalculada { get; set; }
        public float areaSolicitada { get; set; }
        public float areaRegularizacion { get; set; }
        public List<Vertice> lista_vertices { get; set; }

        public bool existePoligono { get; set; }
        public bool cambiaEstado { get; set; }

        public Poligono poligonoOriginal { get; set; }
        public CoordenadaGeografica coordenadaPoligono { get; set; }

        public int _index;
        public int _accion;
        public String _tipoConcesionString;
        public String _poligonoAplicaBcoString;

        // MÉTODOS (Constructores)
        public Poligono()
        {
        }

        public int index
        {
            get { return _index; }
            set { _index = value; }
        }

        public int accion
        {
            get { return _accion; }
            set { _accion = value; }
        }

        public String tipoConcesionString{

            get
            {
                String tipoConcesionString = "";
                int i = 0;
                foreach (ParametroGenerico tipoConcesion in this.tipoConcesion)
                {
                    if (i == 0)
                    {
                        tipoConcesionString = tipoConcesion.descripcion;
                    }
                    else {
                        tipoConcesionString = tipoConcesionString + "," + tipoConcesion.descripcion;
                    }
                    i++;
                    
                }

                _tipoConcesionString = tipoConcesionString;

                return _tipoConcesionString;
            }
        }

        public String poligonoAplicaBcoString{

            get
            {
                String poligonoAplicaBcoString = "";
                //int i = 0;

                //poligonoAplicaBcoString = "Nº Poligono:" + this.idPoligono;

                if (this.tipoUso != null && this.tipoUso.descripcion != null){
                    poligonoAplicaBcoString = poligonoAplicaBcoString + "-" + this.tipoUso.descripcion;
                }

                if(this.toponimio != null){
                    poligonoAplicaBcoString = poligonoAplicaBcoString + "-" + this.toponimio;
                }

                if(this.areaSolicitada > 0){
                    poligonoAplicaBcoString = poligonoAplicaBcoString + "- Area Solicitada:" + this.areaSolicitada;

                }else if(this.areaRegularizacion > 0){
                    poligonoAplicaBcoString = poligonoAplicaBcoString + "- Area Regularizacion:" + this.areaRegularizacion;
                }


                foreach (ParametroGenerico tipoConcesion in this.tipoConcesion)
                {
                    poligonoAplicaBcoString = poligonoAplicaBcoString + "-" + tipoConcesion.descripcion;

                }

                _poligonoAplicaBcoString = poligonoAplicaBcoString;

                return _poligonoAplicaBcoString;
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idPoligono { get; set; }
            public int idPoligAntecSector { get; set; }
            public int idCoordenadaGeo { get; set; }
            public List<ParametroGenerico> tipoConcesion { get; set; }
            public ParametroGenerico tipoUso { get; set; }
            public ParametroGenerico estado { get; set; }
            public String toponimio { get; set; }
            public float areaCalculada { get; set; }
            public float areaSolicitada { get; set; }
            public float areaRegularizacion { get; set; }
            public List<Vertice> lista_vertices { get; set; }
            public CoordenadaGeografica coordenadaPoligono { get; set; }

            

        }
    }
}
