using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Normaliza
    {
        public String Convierte(string valor)
        {
            try
            {
                valor = valor.Replace("Á", "A");
                valor = valor.Replace("É", "E");
                valor = valor.Replace("Í", "I");
                valor = valor.Replace("Ó", "O");
                valor = valor.Replace("Ú", "U");
                valor = valor.Replace("É", "E");
                valor = valor.Replace("Ñ", "N");
                valor = valor.Replace("ñ", "n");
                valor = valor.Replace("á", "a");
                valor = valor.Replace("é", "e");
                valor = valor.Replace("í", "i");
                valor = valor.Replace("ó", "o");
                valor = valor.Replace("ú", "u");
                valor = valor.Replace("'", "''");
                valor = valor.Replace("&nbsp;", " ");
                valor = valor.Replace("<", "(");
                valor = valor.Replace(">", ")");
                valor = valor.Replace("[", "(");
                valor = valor.Replace("]|", ")");
            }
            catch
            {
                
            }

            return valor;
        }
    }
}
