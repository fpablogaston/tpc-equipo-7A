using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public override string ToString()
        {
            return $"{Nombre} - {Descripcion}";
        }

        public void AgregarCategoria()
        {
            // Falta crear logica para agregar categoria
        }

        public void ActualizarCategoria()
        {
            // Falta crear logica para actualizar categoria
        }
        public void EliminarCategoria()
        {
            // Falta crear logica para eliminar categoria
        }
    }
}
