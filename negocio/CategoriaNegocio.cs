using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        private List<Categoria> categorias = new List<Categoria>();

        public void Agregar(Categoria categoria)
        {
            //Falta crear logica para agregar categoria
        }

        public void Actualizar(Categoria categoria)
        {
            // Falta crear logica para actualizar categoria
        }

        public void Eliminar(int id)
        {
            // Falta crear logica para eliminar categoria
        }

        public List<Categoria> listar()
        {
            return categorias;
        }

    }
}
