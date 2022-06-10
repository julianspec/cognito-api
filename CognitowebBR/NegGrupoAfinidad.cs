using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System.Text;
using System.Data.SqlClient;
using CognitowebBE;
using CognitowebDA;

namespace CognitowebBR
{
    public class NegGrupoAfinidad
    {
        private CognitowebBE.Usuario usuario = new CognitowebBE.Usuario();
        Dato dato = new Dato();

        public Boolean ListaAutorizada(string grupo, string modo, ref System.Web.UI.WebControls.GridView gridView)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_grupo_afinidad @i_operacion='LISTA AUTORIZADA' ";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_grupo", grupo, "S");
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");

            ArmaGrid(comando, ref gridView);
            return true;
        }

        public String AutorizaSucursal(string grupo, string sucursal, string modo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            Dato dato = new Dato();

            string comando = "execute sp_grupo_afinidad @i_operacion='AUTORIZA GRUPO' ";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");
            comando = comando + dato.ArmarParametro("@i_grupo", grupo, "S");
            comando = comando + dato.ArmarParametro("@i_suc", sucursal, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public void ArmaGrid(string comando, ref System.Web.UI.WebControls.GridView grid)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds = dato.SpQuery(comando);

                grid.DataSource = ds;
                if (ds.Tables.Count > 0)
                {
                    grid.DataBind();
                }
                else
                {
                    System.Web.UI.WebControls.GridView grid1 = new System.Web.UI.WebControls.GridView();
                    grid = grid1;
                }
            }
            catch
            {

            }
        }
    }
}
