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
    public class LoginController : ApiController
    {
        [Route("api/Log/Access")]
        [HttpPost]
        // POST api/<controller>
        public dynamic Post(Login log)
        {
            return Login.ListarIdentificador(log.ClaveIdentificador.ToString());
        }

        [Route("api/Log/User")]
        [HttpPost]
        public dynamic AccessLog(userlog userlog, HttpRequestMessage request) 
        {
            Respuesta respuesta = new Respuesta();
            string tolenValido = validarToke(request);
            if (tolenValido == "401")
            {
                respuesta.status = 401;
                respuesta.exito = false;
                respuesta.message = "Volver a generar otro Token";
            }
            else if (tolenValido == "200")
            {
                return userlog.listarxUserlog(userlog);
                //respuesta.result = LlegadaSalida.Agregar(llegaSal).result;
                //respuesta.message = "Correcto";
            }
            else if (tolenValido == "400")
            {
                respuesta.status = 400;
                respuesta.exito = false;
                respuesta.message = "ERROR en el Token";
            }
            return respuesta;
        }

        public string validarToke(HttpRequestMessage request)
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