using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CognitowebBE;
using CognitowebDA;
using System.Data;

namespace CognitowebBR
{
    public class NegReporte
    {
        private Usuario usuario = new Usuario();
        private NegSeguridad negSeguridad = new NegSeguridad();

        public NegReporte()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string ConsultaGrafico()
        {
            return "";
        }

        public string ConsultaGrafico(String reporte, String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente, String nroDocumento, String usuarioFiltro)
        {
            try 
	        {	        
            Dato dato = new Dato();
            string comando = ArmaComandoFiltro(reporte, origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente, nroDocumento, usuarioFiltro);
            comando = comando + ", @i_destino='grafico' ";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);
            string res = "";

            if (resultado[0] != "")
            {
                res = "<script type='text/javascript'>";
                res += " google.charts.load('current', { 'packages': ['corechart'] });";
                res += " google.charts.setOnLoadCallback(drawChart);";
                res += " function drawChart() { ";


                DataTable Datos = ConsultaFiltroReporte(reporte, origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente, nroDocumento, usuarioFiltro);

                Datos.Columns.Add(new DataColumn("Columna1", typeof(string)));
                Datos.Columns.Add(new DataColumn("Columna2", typeof(string)));

                string strDatos;
                strDatos = "var data = google.visualization.arrayToDataTable(";
                strDatos = strDatos + "[['Task', 'Hours per Day'], ";
                int contador;

                contador = Datos.Rows.Count;
                foreach (DataRow dr in Datos.Rows)
                {
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
                    if (contador > 1)
                    {
                        strDatos = strDatos + "],";
                        contador = contador - 1;
                    }
                    else
                    {
                        strDatos = strDatos + "]";
                    }

                }


                strDatos = strDatos + "]);";
                res += strDatos;
                res += "var options = { " + resultado[0] + "};";
                res += "var chart = new google.visualization." + resultado[1] + "(document.getElementById('chart_div'));";
                res += " chart.draw(data, options); } </script>";
            }
            return res;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public System.Data.DataSet Catalogo()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_reporte_cognito @i_operacion = 'CATALOGO', @i_usuario = '" + usuario.usuario + "'";
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public Boolean ConsultaFiltroGrilla(ref System.Web.UI.WebControls.GridView grid, String reporte, String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente, String nroDocumento, String usuarioFiltro)
        {
            string comando = ArmaComandoFiltro(reporte, origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente, nroDocumento, usuarioFiltro);
            comando = comando + ", @i_destino='grilla' ";
            ArmaGrid(comando, ref grid);
            return true;
        }

        public Boolean ReporteCBU(ref System.Web.UI.WebControls.GridView grid, String fechaDesde, String fechaHasta)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_reporte_cbu @i_operacion = 'REPORTE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_fec_desde", fechaDesde, "S");
            comando = comando + dato.ArmarParametro("@i_fec_hasta", fechaHasta, "S");
            
            ArmaGrid(comando, ref grid);
            return true;
        }

        public System.Data.DataTable ConsultaFiltroReporte(String reporte, String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente, String nroDocumento, String usuarioFiltro)
        {
            string comando = ArmaComandoFiltro(reporte, origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente, nroDocumento, usuarioFiltro);
            comando = comando + ", @i_destino='reporte' ";
            return ArmaDataTable(comando);
        }

        public string ArmaComandoFiltro(String reporte, String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente, String nroDocumento, String usuarioFiltro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            Dato dato = new Dato();
            string comando = "execute sp_reporte_cognito @i_operacion = 'CONSULTA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_id_reporte", reporte, "N");
            comando = comando + dato.ArmarParametro("@i_origen_actual", origenActual, "S");
            comando = comando + dato.ArmarParametro("@i_tipo", tipo, "N");
            comando = comando + dato.ArmarParametro("@i_sucursal", sucursal, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_ingreso", fechaIngreso, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_desde", fecDesde, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_hasta", fecHasta, "S");
            comando = comando + dato.ArmarParametro("@i_apellido", apellido, "S");
            comando = comando + dato.ArmarParametro("@i_estado", estado, "S");
            comando = comando + dato.ArmarParametro("@i_nro_documento", nroDocumento, "S");
            comando = comando + dato.ArmarParametro("@i_usuario_filtro", usuarioFiltro, "S");

            if (marcaHora == true)
            {
                comando = comando + dato.ArmarParametro("@i_hora_desde", horaDesde, "S");
                comando = comando + dato.ArmarParametro("@i_hora_hasta", horaHasta, "S");
            }
            if (TipoOperacion != "0")
            {
                comando = comando + dato.ArmarParametro("@i_tipo_operacion", TipoOperacion, "N");
            }
            if (producto != "0")
            {
                comando = comando + dato.ArmarParametro("@i_producto", producto, "N");
            }
            if (subProducto != "0")
            {
                comando = comando + dato.ArmarParametro("@i_subproducto", subProducto, "N");
            }
            if (motivo != "0")
            {
                comando = comando + dato.ArmarParametro("@i_motivo", motivo, "N");
            }
            if (marcaCliente == true)
            {
                comando = comando + dato.ArmarParametro("@i_cliente", cliente, "S");
            }
            
            return comando;
        }

        public void ArmaGrid(string comando, ref System.Web.UI.WebControls.GridView grid)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            if (ds.Tables.Count > 0)
            //{
                grid.DataBind();
            //}
        }

        public System.Data.DataTable ArmaDataTable(string comando)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            Dato dato = new Dato();
            dt = dato.spTable(comando);
            return dt;
        }

        public Boolean Hola ()
        {
            return false;
        }

        public string[] ObtieneTipoGrafico(String TipoGrafico)
        {
            try
            {
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                Dato dato = new Dato();
                string comando = "execute sp_reporte_cognito @i_operacion = 'TIPO DE GRAFICOS', @i_usuario='" + usuario.usuario + "'";
                comando = comando + dato.ArmarParametro("@i_id_reporte", TipoGrafico, "N");

                string[] resultado = new string[100];
                resultado = dato.SpString(comando);
                return resultado;
            }
            catch (Exception)
            {
                string[] resultado = new string[100];
                return resultado;
            }
        }

        public Boolean ReporteGerencia(ref System.Web.UI.WebControls.GridView grid, String[] parametro)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_reporte_gerencia @i_operacion = 'CONSULTA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_id_reporte", parametro[0], "N");
            comando = comando + dato.ArmarParametro("@i_fec_desde", parametro[1], "S");
            comando = comando + dato.ArmarParametro("@i_fec_hasta", parametro[2], "S");
            comando = comando + dato.ArmarParametro("@i_tipo_reporte", parametro[3], "N");
            comando = comando + dato.ArmarParametro("@i_anio", parametro[4], "N");
            comando = comando + dato.ArmarParametro("@i_trimestre", parametro[5], "N");
            
            ArmaGrid(comando, ref grid);
            return true;
        }

       // public void doPrint(string grafico)
       // {
       //     var f = new Date();
       //     fechaimpresion = "Fecha del Listado:&nbsp" + f.getDate() + "/" + (f.getMonth() +1) + "/" + f.getFullYear();
       //     var prtContent = document.getElementById("<%= GridView1.ClientID %>");
       //     var titulo = "titulo";
       //     var horizontal = "S";
       //     var msgGrilla = "Mensaje";
       //     var barra = "/";
        
       //     var WinChart = window.open("", "", "left=100,top=100,width=900,height=800,toolbar=0,scrollbars=1,status=0,resizable=1");
       //     WinChart.document.write("<html><head><link href='estilo.css' rel='stylesheet' />");
       //     WinChart.document.write("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'><" + barra + "script>");
       //     WinChart.document.write=("<%=obtenerDatos()%>");
       //     WinChart.document.write=(grafico);            
       //     WinChart.document.write("</head><body><div id='chart_div'></div><div class='pagina'> <table width='100%' border='0'><tr><td width='30%' style='vertical-align:central'><img alt='logo' src='imagenes/logo_ciudad.jpg' style='width: 201px; height: 33px;'/></td><td width='60%' align='center' valign= 'middle class='titulo'>" + titulo.value + "</td><td width='30%' style='vertical-align:middle; text-align:center' class='label'>" + fechaimpresion +"</td></tr></table>");
       //     WinChart.document.write("<div>&nbsp;</div><div class='titulo'>" + msgGrilla.value + "</div>");

       //     WinChart.document.write(prtContent.outerHTML.replace("/font-size: 14px;/g,font-size: 18px; padding:10px;"));
       //     WinChart.document.write("</div></body></html>");
       //     WinChart.document.close();
       //     WinChart.focus();

       //}



    }
}
