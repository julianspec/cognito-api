using api.DbFuntions;
using api.Global;

namespace api.Controllers
{
    public class CreateEditController
    {

        public string SetData(string[] formKeys,string[] formValues, string _modo)
        {

            string tabla = formValues[0];
            string modo = _modo;
            string id1 = formValues[2];
            string id2 = formValues[3];
            string id3 = formValues[4];
            string campoUsuario = formValues[5];
            string campoFAlta = formValues[6];
            string campoFModif = formValues[7];
            string comando1 = "";
            string comando2 = "";
            string separador ="";
            string primeraVez = "S";
            string comilla = "";
            string prefijo = "";
            string campo = "";
            string valor = "";

            // COMIENZA A ARMAR EL COMANDO SQL
            if (modo == "edit")
            {
                comando1 = "update " + tabla + " set ";
            }
            else
            {
                comando1 = "insert into " + tabla + " ( ";
                comando2 = " values ( ";
            }

            // RECORRE EL FORMULARIO RECIBIDO 
            for (int i = 0; i < formKeys.Length; i++)
            {

                if (formKeys[i].Length > 8)
                {
                    // OBTIENE LOS DATOS DEL FORMULARIO
                    prefijo = formKeys[i].Substring(0, 8);
                    campo = formKeys[i].Substring(8, formKeys[i].Length - 8);
                    valor = formValues[i];

                    // ARMA EL SEPARADOR 
                    if (modo == "edit" || (modo != "edit" && valor.Trim() != ""))
                    {
                        if (primeraVez == "S")
                        {
                            primeraVez = "N";
                        }
                        else
                        {
                            separador = ", ";
                        }

                        // ARMA LA COMILLA
                        comilla = "";
                        if (prefijo == "CAMPO_S_")
                        {
                            comilla = "''";
                        }

                        if ((prefijo == "CAMPO_I_" || prefijo == "CAMPO_N_") && modo != "edit") //CAMPO IDENTITY Y MODO ALTA
                        {
                            comilla = "";
                        }

                        if (modo == "edit")
                        {
                            if (valor.Trim() == "")
                            {
                                comilla = "";
                                valor = "null";
                            }
                            comando1 = comando1 + separador + campo + " = " + comilla + valor + comilla;
                        }
                        else
                        {
                            if (valor.Trim() != "")
                            {
                                comando1 = comando1 + separador + campo;
                                comando2 = comando2 + separador + comilla + valor + comilla;
                            }
                        }
                    }
                }
            }

            // TERMINA LA SENTENCIA SQL
            if (primeraVez == "N")
            {
                if (modo != "edit") //CIERRA LOS PARENTESIS DEL ALTA
                {
                    if (campoUsuario != null)
                    {
                        if (campoUsuario != "")
                        {
                            comando1 = comando1 + ", " + campoUsuario;
                            comando2 = comando2 + ", ''" + ModGlb.UserLoged + "''";
                        }
                    }
                    if (campoFAlta != null)
                    {
                        if (campoFAlta != "")
                        {
                            comando1 = comando1 + ", " + campoFAlta;
                            comando2 = comando2 + ", getdate()";
                        }
                    }

                    comando1 = comando1 + ")";
                    comando2 = comando2 + ")";
                }
                else  // UPDATE
                {
                    if (campoUsuario != null)
                    {
                        if (campoUsuario != "")
                        {
                            comando1 = comando1 + ", " + campoUsuario + " = ''" + ModGlb.UserLoged + "''";
                        }
                    }
                    if (campoFModif != null)
                    {
                        if (campoFAlta != "")
                        {
                            comando1 = comando1 + ", " + campoFModif + " = getdate()";
                        }
                    }
                }
            }

            string resultado =  GrabaRegistro(tabla, modo, id1, id2, id3, comando1, comando2);    
            return resultado;
        }

        public string GrabaRegistro(string tabla, string modo, string id1, string id2, string id3, string sql1, string sql2)
        {

            string titulo = ""; //????


            try
            {


                string comando = "execute usp_tabla @i_operacion = 'GRABA_REGISTRO', @i_usuario = '" + ModGlb.UserLoged + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
                if (id2 != null)
                {
                    if (id2.Length > 0)
                    {
                        comando = comando + ", @i_id2 = '" + id2 + "'";
                    }
                }
                if (id3 != null)
                {
                    if (id3.Length > 0)
                    {
                        comando = comando + ", @i_id3 = '" + id3 + "'";
                    }
                }
                comando = comando + ", @i_modo = '" + modo + "'";
                comando = comando + ", @i_titulo = '" + titulo + "'";
                comando = comando + ", @i_sql1 = '" + sql1 + "'";
                if (sql2 != null)
                {
                    if (sql2.Length > 0)
                    {
                        comando = comando + ", @i_sql2 = '" + sql2 + "'";
                    }
                }
                MethodClass.update(comando);
                return "";
            }

            catch
            {
                return "OCURRIO UN ERROR AL GRABAR";
            }
        }

    }
}

