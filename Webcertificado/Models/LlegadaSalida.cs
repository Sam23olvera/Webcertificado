using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Webcertificado.Datos;
using Newtonsoft.Json.Linq;


namespace Webcertificado.Models
{
    public class LlegadaSalida
    {
        public int ClaveServicio { get; set; }
        public int ClaveEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public int PersonaId { get; set; }
        public string PersonaNombre { get; set; }
        public string Firma { get; set; }
        public string Observaciones { get; set; }
        public int Sincronizado { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public static Respuesta Agregar(LlegadaSalida llegaSal)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("ClaveServicio",llegaSal.ClaveServicio),
                new Parametro("ClaveEvento",llegaSal.ClaveEvento),
                new Parametro("FechaEvento","'" +llegaSal.FechaEvento.ToString("yyyy-MM-dd HH:mm:ss") +"'"),
                new Parametro("PersonaId",llegaSal.PersonaId),
                new Parametro("PersonaNombre","'" +llegaSal.PersonaNombre+"'"),
                new Parametro("Firma","'" +llegaSal.Firma +"'"),
                new Parametro("Observaciones", "'" + llegaSal.Observaciones +"'"),
                //new Parametro("Sincronizado",llegaSal.Sincronizado),
                new Parametro("Latitud",llegaSal.Latitud),
                new Parametro("Longitud",llegaSal.Longitud)
            };
            return DBDatos.Ejecutar("SPINS_LlegadaSalida", parametros);
            //return DBDatos.Ejecutar("spInsertLlegadaSalida", parametros);
        }
        
    }
}