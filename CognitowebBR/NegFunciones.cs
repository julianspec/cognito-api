using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CognitowebBE;
using CognitowebDA;

namespace CognitowebBR
{
    public class NegFunciones
    {
        //Devuelve un String con la Fecha Actual del Sistema
        public string FechaHoy()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        public string CheckBoxGet(System.Web.UI.WebControls.CheckBox CK)
        {
            if (CK.Checked)
            {
                return "S";
            }
            else
            {
                return "N";
            }
        }

        public void CheckBoxSet(ref System.Web.UI.WebControls.CheckBox CK, string valor)
        {
            if (valor == "S")
            {
                CK.Checked = true;
            }
            else
            {
                CK.Checked = false;
            }
        }

        public Boolean CargaCombo(ref System.Web.UI.WebControls.DropDownList combo, string valor)
        {
            try
            {
                if (valor != null && valor != "" && valor != "0")
                {
                    for (int i = 0; i < combo.Items.Count; i++)
                    {
                        if (combo.Items[i].Value == valor)
                        {
                            combo.SelectedValue = valor;
                        }
                    }
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch
            {
                combo.SelectedIndex = -1;
            }
            return true;
        }

        public Boolean CargaComboDescripcion(ref System.Web.UI.WebControls.DropDownList combo, string valor)
        {
            try
            {
                if (valor != null && valor != "" && valor != "0")
                {
                    for (int i = 0; i < combo.Items.Count; i++)
                    {
                        if (combo.Items[i].Text == valor)
                        {
                            combo.SelectedIndex = i;
                        }
                    }
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch
            {
                combo.SelectedIndex = -1;
            }
            return true;
        }

        public Boolean ComboSet(ref System.Web.UI.WebControls.DropDownList combo, string valor)
        {
            try
            {
                //if (valor != null && valor != "" && valor != "0")
                if (valor != null && valor != "")
                {
                    for (int i = 0; i < combo.Items.Count; i++)
                    {
                        if (combo.Items[i].Value == valor)
                        {
                            combo.SelectedValue = valor;
                        }
                    }
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch
            {
                combo.SelectedIndex = -1;
            }
            return true;
        }

        public string retornaVariable(string variable)
        {
            try
            {
                string retorno = System.Web.HttpContext.Current.Request.QueryString[variable];
                return retorno;
            }
            catch
            {
                return "";
            }
        }

        public string TextoAcento(string texto)
        {
            try
            {
                // Reemplazo de Acentos
                texto = texto.Replace("Ã¡", "á");
                texto = texto.Replace("Ã©", "é");
                texto = texto.Replace("Ã­", "í");
                texto = texto.Replace("Ã³", "ó");
                texto = texto.Replace("Ãº", "ú");

                texto = texto.Replace("Ã±", "ñ");
                texto = texto.Replace("Ã‘", "Ñ");

                texto = texto.Replace("Ã", "Á");
                texto = texto.Replace("Ã‰", "É");
                texto = texto.Replace("Ã", "Í");
                texto = texto.Replace("Ã“", "Ó");
                texto = texto.Replace("Ãš", "Ú");

                //Reemplazo de caracteres especiales
                texto = texto.Replace("&nbsp;", "");
                texto = texto.Replace("&#225;", "á");
                texto = texto.Replace("&#233;", "é");
                texto = texto.Replace("&#237;", "í");
                texto = texto.Replace("&#243;", "ó");
                texto = texto.Replace("&#250;", "ú");
                texto = texto.Replace("&#241;", "ñ");
                texto = texto.Replace("&#209;", "Ñ");
                texto = texto.Replace("&#193;", "Á");
                texto = texto.Replace("&#201;", "É");
                texto = texto.Replace("&#205;", "Í");
                texto = texto.Replace("&#211;", "Ó");
                texto = texto.Replace("&#218;", "Ú");
                texto = texto.Replace("&#191;", "¿");

                texto = texto.Replace("ÃƒÂ¡", "á");
                texto = texto.Replace("ÃƒÂ©", "é");
                texto = texto.Replace("ÃƒÂ­", "í");
                texto = texto.Replace("ÃƒÂ³", "ó");
                texto = texto.Replace("ÃƒÂº", "ú");

                texto = texto.Replace("ÃƒÂ±", "ñ");
                texto = texto.Replace("Ãƒâ€˜", "Ñ");

                return texto;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Funnción que devuelve si el contenido de un texto es cero
        /// </summary>
        /// <param name="pTexto"></param>
        /// <returns></returns>
        public Boolean EsCero(string pTexto)
        {
            try
            {
                if (pTexto == "." || pTexto == "," || pTexto == "")
                {
                    return true;
                }

                double result;
                result = Convert.ToDouble(pTexto);
                if (result == 0)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Boolean EsVacio(string pTexto)
        {
            try
            {
                pTexto = pTexto.Trim();
                pTexto = pTexto.Replace(".", "");
                pTexto = pTexto.Replace(",", "");

                if (pTexto == "")
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
