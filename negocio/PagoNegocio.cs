﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PagoNegocio
    {
        private List<Pago> pagos = new List<Pago>();
        public void ProcesarPago(Pago pago)
        {
            // Falta crear logica para procesar el pago
        }

        public void CambiarEstado(int id, string nuevoEstado)
        {
            // Falta crear logica para cambiar el estado del pago
        }
        public List<Pago> listar()
        {
            return pagos;
        }

       
    }
}
