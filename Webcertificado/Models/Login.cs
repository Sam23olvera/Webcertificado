using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webcertificado.Datos;
using System.Data;

namespace Webcertificado.Models
{
    public class Login
    {
        public string ClaveIdentificador { get; set; }
        public string ClaveAcceso { get; set; }

        public static Respuesta ListarIdentificador(string imei)
        {
            Respuesta respuesta = new Respuesta();

            Guid z_varguidToken = Guid.NewGuid();

            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("Identificador", imei),
                new Parametro("CodGenerado",z_varguidToken.ToString())
            };

            respuesta = DBDatos.Listar("SQRY_BusCadu", parametros);
            //respuesta = DBDatos.Listar("SP_BusCadu", parametros);
            DataTable table = respuesta.result;
            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["Token"].ToString() == "400")
                {
                    respuesta.status = 400;
                    respuesta.exito = false;
                    respuesta.result = table.Rows[0]["Token"].ToString();
                }
                else
                {
                    respuesta.status = 200;
                    respuesta.exito = true;
                    //respuesta.result = DBDatos.Listar("SP_BusCadu", parametros).result;
                    respuesta.result = DBDatos.Listar("SQRY_BusCadu", parametros).result;
                }
            }
            return respuesta;
        }

        public static Respuesta listarxToken(string token)
        {
            Respuesta respuesta = new Respuesta();
            Guid z_varguidToken = Guid.NewGuid();
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("CodGenerado", token),
                new Parametro("newToken",z_varguidToken)
            };
            //return DBDatos.Listar("spCheckLogToken", parametros);
            return DBDatos.Listar("SPMTP_CheckIdentificador", parametros);
        }

    }

    public class userlog 
    {
        public int NumeroOperador { get; set; }
        public string Clave { get; set; }

        public static Respuesta listarxUserlog(userlog us) 
        {
            Respuesta respuesta = new Respuesta();
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("NumeroOperador", us.NumeroOperador),
                new Parametro("Clave", us.Clave)
            };
            //return DBDatos.Listar("spCheckLogToken", parametros);
            return DBDatos.Listar("[SidMovil].[SPQRY_ValidaCredencial]", parametros);
        }
    }

}