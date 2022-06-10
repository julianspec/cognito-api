using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using CognitowebBE;
using CognitowebBR;
using CognitowebDA;
//using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration; 

namespace CognitowebBR
{
    public class NegTelefonoMail
    {
        private Usuario usuario = new Usuario();
        private Dato dato = new Dato();

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

        public System.Data.DataSet TipoReporte()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_telefono_mail @i_operacion = 'TIPO_REPORTE', @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public String ValidaFiltro(string desde, string hasta, string reporte, string tipoOperacion)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            
            string comando = "execute sp_telefono_mail @i_operacion='VALIDA_FILTRO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_fecha_desde", desde, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_hasta", hasta, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_reporte", reporte, "N");
            comando = comando + dato.ArmarParametro("@i_tipo_operacion", tipoOperacion, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public System.Data.DataTable GeneraReporte(string desde, string hasta, string reporte, string tipoOperacion)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_telefono_mail @i_operacion='GENERA_REPORTE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_fecha_desde", desde, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_hasta", hasta, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_reporte", reporte, "N");
            comando = comando + dato.ArmarParametro("@i_tipo_operacion", tipoOperacion, "N");
            return ArmaDataTable(comando);
        }

        public Boolean GeneraReporte(string desde, string hasta, string reporte, string tipoOperacion, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_telefono_mail @i_operacion='GENERA_ARCHIVO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_fecha_desde", desde, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_hasta", hasta, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_reporte", reporte, "N");
            comando = comando + dato.ArmarParametro("@i_tipo_operacion", tipoOperacion, "N");
            ArmaGrid(comando, ref grid);
            return true;
        }

        public System.Data.DataTable ArmaDataTable(string comando)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            Dato dato = new Dato();
            dt = dato.spTable(comando);
            return dt;
        }
    }
}
