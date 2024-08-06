using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webcertificado.Datos
{
    public class Respuesta
    {
        public int status { get; set; }
        public bool exito { get; set; }
        public string message { get; set; }
        public dynamic result { get; set; }

        public Respuesta()
        {

        }
    }
}