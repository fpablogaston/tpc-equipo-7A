using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class EstadoPedido
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public int IdEnvio { get; set; }
        public string DescripcionEnvio { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}