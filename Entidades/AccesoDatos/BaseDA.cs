using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Utilidades;

namespace Datos.AccesoDatos
{
    public class BaseDA
    {

        protected DBAmbientalDataContext GetDC() {

            DBAmbientalDataContext DC = new DBAmbientalDataContext();
            DC.DeferredLoadingEnabled = false;

            return DC;
            
        }

        protected List<T> ConsultaALista<T>(IQueryable<T> query) {

            List<T> lista = new List<T>();
            foreach (T item in query) {
                lista.Add(item);
            }

            return lista;

        }
    }
}
