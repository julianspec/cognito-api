using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using CognitowebBE;
using CognitowebDA;
using System.Text;
using System.Data.SqlClient;

namespace CognitowebBR
{
    public class NegSeguridad
    {
        private Usuario usuario = new Usuario();

        public Boolean VerificaAcceso(int acceso)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_accesos @i_operacion = 'ACCESO', @i_acceso = " + acceso + ", @i_usuario = '" + usuario.usuario + "'";

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

        public void ArmaGrid(string comando, ref System.Web.UI.WebControls.GridView grid)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            if (ds.Tables.Count > 0)
            {
                grid.DataBind();
            }
        }

        public Boolean VerificaSecionActiva()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            if (usuario == null)
            {
                return false;
            }

            return true;
        }

        public SqlDataReader ArmaMenuAcordion()
        {
            try
            {
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                if (usuario.usuario == null)
                {
                    usuario.usuario = "Usuario no Valido";
                }
                if (usuario.usuario == "")
                {
                    usuario.usuario = "Usuario no Valido";
                }
                string comando = "execute usp_acceso @i_operacion = 'MENU', @i_usuario = '" + usuario.usuario + "'";

                SqlDataReader dr;
                Dato dato = new Dato();
                dr = dato.SpDataReader(comando);
                return dr;
            }
            catch
            {
                SqlDataReader dr;
                dr = null;
                return dr;
            }
        }
    }
}
