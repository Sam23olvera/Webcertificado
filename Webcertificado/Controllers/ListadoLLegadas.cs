using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webcertificado.Models;
using Webcertificado.Datos;
using System.Data;

namespace Webcertificado.Controllers
{
    public class ListadoLLegadas : ApiController
    {
        // GET api/<controller>
        [Route("api/LLegadaSalidas/Busc")]
        [HttpPost]
        public dynamic Mostrar(LlegadaSalida llegaSal, HttpRequestMessage request)
        {
            Respuesta respuesta = new Respuesta();

            string ValidToken = validarToken(request);

            if (ValidToken == "401")
            {
                respuesta.status = 401;
                respuesta.exito = false;
                respuesta.message = "Volver a generar otro Token";
            }
            else if (ValidToken == "200")
            {
                return LlegadaSalida.Agregar(llegaSal);
                //respuesta.result = LlegadaSalida.Agregar(llegaSal).result;
                //respuesta.status = 200;
                //respuesta.message = "Correcto";
            }
            else if (ValidToken == "400")
            {
                respuesta.status = 400;
                respuesta.exito = false;
                respuesta.message = "ERROR en el Token";
            }
            return respuesta;
            //llegaSal.ClaveEvento = Convert.ToInt32(ClaveServicio);
        }
        public string validarToken(HttpRequestMessage request)
        {
            string token = "";

            foreach (var item in request.Headers)
            {
                if (item.Key.Equals("Authorization"))
                {
                    token = item.Value.First();
                    break;
                }
            }

            DataTable table = Login.listarxToken(token).result;

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["Res"].ToString();
            }
            return "";
        }
    }
}