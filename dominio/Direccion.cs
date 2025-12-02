using System;

namespace dominio
{
    public class Direccion
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Alias { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return $"{Alias}: {Calle}, {Ciudad}";
        }
    }
}