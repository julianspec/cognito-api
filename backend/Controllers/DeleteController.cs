using api.DbFuntions;
using api.Global;
using CognitowebBR;

namespace api.Controllers
{
    public class DeleteController
    {
        string tabla = "";
        string modo = "";
        string titulo = "";
        string id1 = "";
        string id2 = "";
        string id3 = "";
   
        public string  SetData(string[] formValues)
        {
            tabla = formValues[0];
            id1 = formValues[2];    
            id2 = formValues[3];    
            id3 = formValues[4];
            string mensaje = BorraRegistro(tabla, "borrar", id1, id2, id3);
            return mensaje;
        }

        public string BorraRegistro(string tabla, string modo, string id1, string id2, string id3)
        {
           
            string[] resultado = new string[100];

            try
            {
                string comando = "execute usp_tabla @i_operacion = 'BORRAR_REGISTRO', @i_usuario = '" + ModGlb.UserLoged 
                    + "', @i_tabla = '" + tabla 
                    + "', @i_id1 = '" + id1 + "'";
                
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
                comando = comando + ", @i_sql1 = 'delete from " + tabla + " '"; ;
                MethodClass.update(comando);
                return "";
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

    }
}
