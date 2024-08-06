using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Webcertificado.Datos;
using Newtonsoft.Json.Linq;

namespace Webcertificado.Models
{
    public class DatosMas
    {
        public int NumRut { get; set; }
        public List<FOLTERM> FOLTERM { get; set; }
        public static Respuesta mostrar(DatosMas datos)
        {
            //string js = System.Text.Json.JsonSerializer.Serialize(j.json);
            string js = Newtonsoft.Json.JsonConvert.SerializeObject(datos.FOLTERM);
            js = "{\"FOLTERM\":" + js + "}";
            List<Parametro> parame = new List<Parametro>
            {
                new Parametro("ClaveOperador",datos.NumRut),
                new Parametro("FoliosT",js)
            };
            return DBDatos.Listar("SPQRY_Operador_ViajesSMES", parame);
        }
    }
    public class FOLTERM
    {
        public int CFV { get; set; }
        public int CES { get; set; }
    }
}