using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.AccesoDatos
{
    public interface IDataAccess<T>{

        List<T> GetALL();
        Boolean Adicionar(T item);
        Boolean Eliminar(T item);
        Boolean Actualizar(T nuevoItem, T originalItem);

    }
}
