using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using Webcertificado.Models;
using Webcertificado.Datos;
using System.Text;


namespace Webcertificado.Controllers
{
    public class llegadaSalidaController : ApiController
    {
        //"http://localhost:55535/api/LLegadaSalidas/Insert"
        // GET api/<controller>
        [Route("api/LLegadaSalidas/Insert")]
        [HttpPost]
        public dynamic Agregar(LlegadaSalida llegaSal, HttpRequestMessage request)
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

        [Route("api/LLegadaSalidas/InstGroupES")]
        [HttpPost]
        public dynamic InsertComplet(LlegaSalidaGroup llegaSal, HttpRequestMessage request) 
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
                return LlegaSalidaGroup.Insert(llegaSal);
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

        [Route("api/LLegadaSalidas/Folios")]
        [HttpPost]
        public dynamic ListadoFull(DatosMas Dat, HttpRequestMessage request)
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
               return DatosMas.mostrar(Dat);
            }
            else if (ValidToken == "400")
            {
                respuesta.status = 400;
                respuesta.exito = false;
                respuesta.message = "ERROR en el Token";
            }
            return respuesta;
        }
    }
}