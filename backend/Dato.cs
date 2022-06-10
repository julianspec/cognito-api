using api.Global;
using CognitowebBE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api
{
    public  class Dato
    {
        public Boolean SpQueryReader(string comando, ref SqlDataReader dr)
        {
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                SqlDataReader myDataReader;

                myConnection = new SqlConnection(Connection.ConnectionString);
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

        public SqlDataReader SpDataReader(string comando)
        {
            try
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                SqlDataReader myDataReader;

                myConnection = new SqlConnection(Connection.ConnectionString);
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

        public String[] SpString(string comando)
        {
            try
            {
                SqlCommand comandoSql;
                SqlDataReader reader;
                string[] dato = new string[100];
                int total;
            
                using (SqlConnection dbConnection = new SqlConnection(Connection.ConnectionString))
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
          

                    reader.Close();
                    reader.Dispose();

                    return dato;
                }
            }
            catch (SqlException ex)
            {
                string[] dato = new string[100];
         
                dato[0] = "ERROR - Mensaje de la Base de Datos: " + ex.Message;
                return dato;
            }
        }

        public Boolean VerificaAcceso(string acceso)
        {
            

            string comando = "execute sp_accesos @i_operacion = 'ACCESO', @i_acceso = " + acceso + ", @i_usuario = '" +ModGlb.UserLoged + "'";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public System.Data.DataSet SpDataset(string comando)
        {
            comando = LimpiaComando(comando);
            //System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            //Abrir Coneccion
            connection.Open();

            SqlConnection objConn = new SqlConnection(Connection.ConnectionString);
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

        public String[] TablaParam(string tabla)
        {
            Dato dato = new Dato();

            string comando = "execute usp_tabla @i_operacion = 'TABLA_PARAM', @i_usuario = '" + ModGlb.UserLoged + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    resultado[i] = "0";
                }
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
        }

        public string ArmaRadio(string texto, string valorDato)
        {
            if (valorDato == "S")
            {
                texto = texto.Replace("@SI@", "checked");
                texto = texto.Replace("@NO@", "");
            }
            else
            {
                texto = texto.Replace("@SI@", "");
                texto = texto.Replace("@NO@", "checked");
            }

            return texto;
        }

        public string ArmaCombo(string tabla, string campo, string valorDato, string modo)
        {
            Dato dato = new Dato();

            string texto = "";
            string linea = "";
            string reemplazo = "";
            valorDato = valorDato.Trim();

            string comando = "execute usp_tabla @i_operacion = 'ARMA_CATALOGO', @i_usuario = '" + ModGlb.UserLoged + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_id1", campo, "S");
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");


            SqlDataReader dr;
            //Dato dato = new Dato();
            dr = dato.SpDataReader(comando);

            //System.Data.DataSet DS1 = new System.Data.DataSet();
            //DS1 = dato.SpDataset(comando);

            //BUSCA EL VALOR DEL CAMPO DEL REGISTRO
            //foreach (System.Data.DataRow renglon in DS1.Tables["tabla"].Rows)
            //{
            while (dr.Read()) //LEE CADA FILA
            {
                // ARMA LA LINEA DEL COMBO
                //linea = renglon["valor"].ToString();
                linea = dr[0].ToString();

                // SI TIENE UN VALOR SELECCIONADO BUSCA LA 
                if (valorDato != null)
                {
                    if (valorDato != "")
                    {
                        if (reemplazo == "")
                        {
                            if (linea.Substring(15, valorDato.Length) == valorDato && linea.Substring(15 + valorDato.Length, 2) == "\">")
                            {
                                linea = linea.Replace("<option ", "<option selected ");
                                reemplazo = "S";
                            }
                        }
                    }
                }

                texto = texto + linea;
            }

            // SI TIENE UN VALOR ASIGNADO Y NO SE ENCUETRA EN LA LISTA LE COLOCA UN VALOR GENERICO
            if (valorDato != null && reemplazo == "")
            {
                linea = linea + "<option selected value='" + valorDato + "'>Dato no encontrado o borado</option>";
            }

            //DS1.Tables["tabla"].Clear();
            dr.Dispose();

            return texto;
        }

        public Boolean VerificaAcceso(int acceso)
        {
         

            string comando = "execute sp_accesos @i_operacion = 'ACCESO', @i_acceso = " + acceso + ", @i_usuario = '" + ModGlb.UserLoged + "'";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
