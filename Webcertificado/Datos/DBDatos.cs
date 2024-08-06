using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using static System.Configuration.ConfigurationManager;
using NETDAcces;
using NETBT_Tools;
using System.Text.RegularExpressions;

namespace Webcertificado.Datos
{
    public class DBDatos
    {
        public static string Conexion = ConnectionStrings["SM"].ConnectionString;
        public static Respuesta Ejecutar(string nombreProcedimiento, List<Parametro> parametros, string stringConexion = "")
        {
            Respuesta respuesta = new Respuesta();
            //respuesta.message = "";
            //SqlConnection conexion = new SqlConnection(string.IsNullOrEmpty(stringConexion) ? Conexion : stringConexion);
            //conexion.Open();

            try
            {
                /*
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        if (!parametro.Salida)
                        { cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor); }
                        else
                        { cmd.Parameters.Add(parametro.Nombre, SqlDbType.VarChar, 100).Direction = ParameterDirection.Output; }
                    }
                }*/

                //int e = cmd.ExecuteNonQuery();
                string datos = "";
                for (int i = 0; i < parametros.Count; i++)
                {
                    datos = datos + parametros[i].Valor.ToString() + ",";
                }
                datos = datos.TrimEnd(',');
                respuesta.message = DBDataAcces.zMetExecCommand_TsSQL("SM", nombreProcedimiento + " " + datos);
                /*
                for (int i = 0; i < parametros.Count; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        string mensaje = cmd.Parameters[i].Value.ToString();

                        if (!string.IsNullOrEmpty(mensaje))
                        {
                            respuesta.message = mensaje;
                        }
                    }
                }*/
                if (respuesta.message == "200")
                {
                    respuesta.status = 200;
                    respuesta.exito = true;
                    respuesta.message = "Registrado";
                }
                else 
                {
                    respuesta.status = 400;
                    respuesta.exito = false;
                    respuesta.message = respuesta.message.ToString();
                }
            }
            catch (Exception EX)
            {
                respuesta.status = 400;
                respuesta.exito = false;
                respuesta.message = respuesta.message.ToString() + EX.Message; 
                //respuesta.message = EX.Message + Error;
            }
            finally
            {
                //conexion.Close();
            }

            return respuesta;
        }
        public static Respuesta Listar(string nombreProcedimiento, List<Parametro> parametros = null, string stringConexion = "")
        {
            Respuesta respuesta = new Respuesta();

            List<BO_Parametros> parame = new List<BO_Parametros>();
            for (int i = 0; i < parametros.Count; i++) 
            {
                parame.Add(new BO_Parametros()
                {
                    NombreParametro = parametros[i].Nombre.ToString(),
                    ValueParametro = parametros[i].Valor.ToString()
                });
            }
            try
            {

                DataTable tabla = DBDataAcces.zMetExecCommand_SPSQL("SM", nombreProcedimiento, parame, "data");

            //SqlConnection conexion = new SqlConnection(string.IsNullOrEmpty(stringConexion) ? Conexion : stringConexion);
            //conexion.Open();

            

                //SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //
                //if (parametros != null)
                //{
                //    foreach (var parametro in parametros)
                //    {
                //        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                //    }
                //}
                //DataTable tabla = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    respuesta.exito = true;
                    respuesta.message = "Registros";
                    respuesta.result = tabla;
                }
                else
                {
                    respuesta.exito = false;
                    respuesta.message = "No hay registros";
                    respuesta.result = tabla;
                }

                //respuesta.exito = true;
                //respuesta.message = tabla.Rows.Count > 0 ? "Registros" : "No hay registros";
                //respuesta.result = tabla;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.message = ex.Message;
                return respuesta;
            }
            finally
            {
                //conexion.Close();
            }
        }

        public static Respuesta Insert(string nombreProcedimiento, string parametro, string stringConexion = "") 
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                //new BO_EventoError(z_enumTiposError.BaseDatos, " pruebita", "base datos", "zMetExecCommand_SPSQL", false);
                respuesta.message = DBDataAcces.zMetExecCommand_TsSQL("SM", nombreProcedimiento + " " + parametro);
                if (respuesta.message == "200")
                {
                    respuesta.status = 200;
                    respuesta.exito = true;
                    respuesta.message = "Registrado";
                }
                else
                {
                    respuesta.status = 400;
                    respuesta.exito = false;
                    respuesta.message = respuesta.message.ToString();
                }
            }
            catch (Exception EX)
            {
                respuesta.status = 400;
                respuesta.exito = false;
                respuesta.message = respuesta.message.ToString() + EX.Message;
            }
            finally
            {
                //conexion.Close();
            }

            return respuesta;
        }
    }
}