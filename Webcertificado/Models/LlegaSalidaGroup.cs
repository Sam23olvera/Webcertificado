using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Webcertificado.Datos;

namespace Webcertificado.Models
{
    public class LlegaSalidaGroup
    {
        public object[] LLEGADAS { get; set; }
        public static Respuesta Insert(LlegaSalidaGroup llega)
        {
            string x = Newtonsoft.Json.JsonConvert.SerializeObject(llega);
            //var des = Newtonsoft.Json.JsonConvert.DeserializeObject(x);
            string json = Regex.Replace(x.ToString(), "\r\n",string.Empty);
            json = Regex.Replace(json,@"\s",string.Empty).Trim();
            json = "'" + json + "'";
            return DBDatos.Insert("SPINS_GpoLlegadaSalida", json);
        }
    }
}