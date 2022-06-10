using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System;
using System.Web;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using CognitowebBE;
using BancoCiudad.Canales.Framework.Encrypt;

namespace CognitowebDA
{
    public class Dato
    {
        public Dato()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public System.Data.DataSet SpQuery(string comando)
        {
            try
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["ConexionCognito"].ConnectionString;
                string connectionString = ObtieneConexion();
                using (System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    string queryString = comando;
                    System.Data.IDbCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                    dbCommand.CommandText = queryString;
                    dbCommand.CommandTimeout = 360;
                    dbCommand.Connection = dbConnection;
                    
                    System.Data.IDbDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
                    dataAdapter.SelectCommand = dbCommand;
                    dataAdapter.SelectCommand.CommandTimeout = 360;

                    System.Data.DataSet dataSet = new System.Data.DataSet();
                    dataAdapter.Fill(dataSet);

                    CargaResultado(true, 0, "OK", dataSet.Tables.Count);
                    dataSet.Dispose();
                    
                    return dataSet;
                }
            }
            catch (SqlException ex)
            {
                CargaResultado(false, ex.Number, ex.Message, 0);
                System.Data.DataSet ds1 = new System.Data.DataSet();
                return ds1;
            }
        }

        public Boolean SpQueryReader(string comando, ref SqlDataReader dr)
        {
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                SqlDataReader myDataReader;
                
                myConnection = new SqlConnection(ObtieneConexion());
                myConnection.Open();

                //prepare sql statements
                myCommand = new SqlCommand(comando, myConnection);
                myCommand.CommandTimeout = 360;
                myDataReader = myCommand.ExecuteReader();

                //Asigna el objeto
                dr = myDataReader;
                return true;
            }
            catch (Exception ex)
            {
                dr = null;
                return false;
            }
        }

        public System.Data.DataTable spTable(string comando)
        {
            try
            {
                DataSet dss = new DataSet();
                string connectionString = ObtieneConexion();
                SqlDataAdapter da = new SqlDataAdapter(comando, connectionString);
                da.SelectCommand.CommandTimeout = 360;
                da.Fill(dss);
                DataTable dt = dss.Tables[0];
                da.Dispose();
                dss.Dispose();
                return dt;
            }
            catch(Exception e)
            {
                comando = "select Error = 'Ocurrio un error en la Base de Datos: " + e.Message + "'";
                DataSet dss = new DataSet();
                string connectionString = ObtieneConexion();
                comando = "select '" + comando.Replace("'","|") + "'";
                SqlDataAdapter da = new SqlDataAdapter(comando, connectionString);
                da.SelectCommand.CommandTimeout = 360;
                da.Fill(dss);
                DataTable dt = dss.Tables[0];
                da.Dispose();
                dss.Dispose();
                return dt;
            }
        }
        
        public int SpExec(string comando)
        {
            try
            {
                string connectionString = ObtieneConexion();
                using (System.Data.IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    string queryString = comando;
                    System.Data.IDbCommand dbCommand = new System.Data.SqlClient.SqlCommand();

                    dbCommand.CommandText = queryString;
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandTimeout = 360;
                    dbConnection.Open();
                    int rowsAffected = 0;
                    rowsAffected = dbCommand.ExecuteNonQuery();

                    CargaResultado(true, 0, "OK", rowsAffected);

                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                CargaResultado(false, ex.Number, ex.Message, 0);
                return 0;
            }
        }

        public String[] SpString(string comando)
        {
            try
            {
                SqlCommand comandoSql; 
                SqlDataReader reader;
                string[] dato = new string[100];
                int total;
                string connectionString = ObtieneConexion();
                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    comandoSql = dbConnection.CreateCommand();
                    comandoSql.CommandText = comando;
                    comandoSql.CommandTimeout = 360;
                    reader = comandoSql.ExecuteReader();

                    total = reader.FieldCount - 1;
                    string tipo = "";
                    if (reader.FieldCount > 0)
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i <= total; i++)
                            {
                                if (reader.GetValue(i) == DBNull.Value)
                                {
                                    dato[i] = "";
                                }
                                else
                                {
                                    tipo = reader.GetDataTypeName(i);

                                    switch (tipo)
                                    {
                                        case "int":
                                        case "smallint":
                                            dato[i] = reader.GetInt32(i).ToString();
                                            break;
                                        case "tinyint":
                                            dato[i] = reader.GetSqlInt16(i).ToString();
                                            break;
                                        case "datetime":
                                        case "smalldatetime":
                                            dato[i] = reader.GetDateTime(i).ToString("dd/mm/yyyy");
                                            break;
                                        case "money":
                                            dato[i] = reader.GetDecimal(i).ToString("0.00");
                                            break;
                                        default:
                                            dato[i] = reader.GetString(i).ToString().Trim(); //  .Item(dr.GetName(i)).ToString
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        dato[0] = " ";
                    }
                    CargaResultado(true, 0, "OK", reader.FieldCount);

                    reader.Close();
                    reader.Dispose();
                   
                    return dato;
                }
            }
            catch (SqlException ex)
            {
                string[] dato = new string[100];
                CargaResultado(false, ex.Number, ex.Message, 0);
                dato[0] = "ERROR - Mensaje de la Base de Datos: " + ex.Message;
                return dato;
            }
        }

        protected void CargaResultado(Boolean resultado, int codigo, String mensaje, double filas)
        {
            BaseDatos baseDatos = new BaseDatos();
            baseDatos.resultado = resultado.ToString();
            mensaje = "Error de la Base de Datos: " + codigo.ToString() + "-" + mensaje;
            mensaje = mensaje.Replace("'", "");
            baseDatos.mensaje = mensaje;
            baseDatos.filas = filas.ToString();
            System.Web.HttpContext.Current.Session.Add("baseDatos", baseDatos);
        }

        protected String ObtieneConexion()
        {
            //Toma la Conexion de Sesion
            String conexion = (String)System.Web.HttpContext.Current.Session["conexion"];
                        
            //Si no esta cargada la obtiene del archivo INI
            if (conexion == "" || conexion == null)
            {
                conexion = ArmaConexion();
                conexion = conexion + DesencriptaClave();

                //if (conexion.IndexOf("Connection Timeout") < 0)
                //{
                //    conexion = conexion + ";Connection Timeout=600";
                //}

                System.Web.HttpContext.Current.Session.Add("conexion", conexion);
            }

            // Devuelve la Conexion
            return conexion;
        }

        //protected String ObtieneConexion2()
        //{
        //    String conexion = (String)System.Web.HttpContext.Current.Session["conexion"];

        //    if (conexion == "" || conexion == null)
        //    {
        //        // OBTIENE LA CLAVE YA DESENCRIPTADA
        //        String pwd = (String)System.Web.HttpContext.Current.Session["pwd"];

        //        conexion = ArmaConexion();
        //        conexion = conexion + "PWD=" + pwd + ";";

        //        System.Web.HttpContext.Current.Session.Add("conexion", conexion);
        //    }
        //    return conexion;
        //}

        protected string ArmaConexion()
        {
            // OBTIENE LA RUTA DEL ARCHIVO DE CONFIGURACION
            string archivoIni = System.Web.HttpContext.Current.Server.MapPath("./conexion.ini");
            int contador = 0;
            string conexion = "";
            // ABRE EL ARCHIVO DE CONFIGURACION
            using (System.IO.StreamReader sr = System.IO.File.OpenText(archivoIni))
            {
                String fila; //Variable para el texto de la linea
                while ((fila = sr.ReadLine()) != null)
                {
                    if (fila.Length > 3)
                    {
                        // Excluye las lineas que comienzan con  '
                        if (fila.Substring(0, 1) != "'")
                        {
                            // BUSCA EL TAG [CONEXION]
                            if (fila.Substring(0, 1) == "[")
                            {
                                if (fila.Substring(0, 10) == "[CONEXION]")
                                {
                                    contador = 1;
                                }
                                else
                                {
                                    contador = 0;
                                }
                            }
                            else
                            {
                                // SI ESTA DENTRO DEL TAG CONEXION CARGA LOS VALORES
                                if (contador == 1)
                                {
                                    fila = fila.Trim();
                                    conexion = conexion + fila + "; ";
                                }
                            }
                        }
                    }
                }
                sr.Close();
            }
            return conexion;
        }

        protected String DesencriptaClave()
        {
            string conexion = "";

            IEncrypt encrypter = new BcbaEncryption();

            string semilla;
            string clave;

            semilla = encrypter.Decrypt(LeerFilaIni("SEMILLA"), string.Empty);
            clave = encrypter.Decrypt(LeerFilaIni("CLAVE"), semilla);

            //datos.UsuarioLogin = encrypter.Decrypt(ini.IniReadValue("BASE_DATOS", "LOGIN"), clave, semilla);
            conexion = "PWD=" + encrypter.Decrypt(LeerFilaIni("PASSWORD"), clave, semilla);

            return conexion;
        }

        public String LeerFilaIni(string clave)
        {
            String auxiliar = "";

            string archivoIni = System.Web.HttpContext.Current.Server.MapPath("./conexion.ini");
            using (System.IO.StreamReader sr = System.IO.File.OpenText(archivoIni))
            {
                String fila; //Variable para el texto de la linea
                while ((fila = sr.ReadLine()) != null)
                {
                    if (fila.Length > 3)
                    {
                        // Excluye las lineas que comienzan con [ o '
                        if (fila.Substring(0, 1) != "[" && fila.Substring(0, 1) != "'")
                        {
                            fila = fila.Trim();
                            if (fila.Length > clave.Length)
                            {
                                if (fila.Substring(0, clave.Length) == clave)
                                {
                                    int inicio = clave.Length + 1;
                                    int longitud = fila.Length - clave.Length - 1;
                                    auxiliar = fila.Substring(inicio, longitud);
                                    auxiliar = auxiliar.Trim();
                                }
                            }
                        }
                    }
                }
                sr.Close();
                sr.Dispose();
            }
            return auxiliar;
        }

        /// <summary>
        /// Recibe un valor para enviar a un parámetro de un SP, si el valor está vacio, no lo envia
        /// </summary>
        /// <param name="parametro">parametro del SP</param>
        /// <param name="valor">valor a enviar al SP</param>
        /// <param name="tipo">S=String N=Numerico (Tipo de Parametro del SP)</param>
        /// <returns></returns>
        public String ArmarParametro(String parametro, String valor, String tipo)
        {
            string comando = "";
            string separador = "";

            if (valor == null)
            {
                return "";
            }

            if (valor.Trim() == "")
            {
                return "";
            }
            if (valor == "&nbsp;")
            {
                valor = " ";
            }

            if (tipo == "S")
            {
                separador = "'";
            }
            if (tipo == "N")
            {
                double v;
                if (!Double.TryParse(valor.Trim(), out v))
                {
                    return "";
                }
  
            }

            comando = ", " + parametro + "=" + separador + valor + separador;

            return comando;
        }

        public SqlDataReader SpDataReader(string comando)
        {
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                SqlDataReader myDataReader;

                myConnection = new SqlConnection(ObtieneConexion());
                myConnection.Open();

                //prepare sql statements
                myCommand = new SqlCommand(comando, myConnection);
                myCommand.CommandTimeout = 360;
                myDataReader = myCommand.ExecuteReader();

                //Asigna el objeto
                return myDataReader;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public System.Data.DataSet SpDataset(string comando)
        {
            comando = LimpiaComando(comando);
            //System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection();
            SqlConnection connection = new SqlConnection(ObtieneConexion());
            //Abrir Coneccion
            connection.Open();

            SqlConnection objConn = new SqlConnection(ObtieneConexion());
            objConn.Open();

            //Crear un objeto DataAdapter y proporcionar el string de Consulta.
            //System.Data.OleDb.OleDbDataAdapter MyDataAdapter = new System.Data.OleDb.OleDbDataAdapter(comando, connection);
            SqlDataAdapter da = new SqlDataAdapter(comando, objConn);
            da.SelectCommand.CommandTimeout = 360;

            //Crear un nuevo objeto DataSet para alojar los registros.
            DataSet ds = new DataSet();
            ds.Reset();

            //Rellenar el DataSet con las filas devueltas.
            da.FillSchema(ds, SchemaType.Source, "tabla");
            da.Fill(ds, "tabla");

            objConn.Close();
            objConn.Dispose();
            da.Dispose();
            return ds;
        }

        private string LimpiaComando(string comando)
        {
            comando = comando.Replace(";", "");
            comando = comando.Replace("--", "-");
            comando = comando.Replace("/*", "");
            comando = comando.Replace("*/", "");
            comando = comando.Replace("xp_", "");
            comando = comando.Replace("where", "");
            comando = comando.Replace("drop", "");
            comando = comando.Replace("declare", "");
            return comando;
        }
    }
}