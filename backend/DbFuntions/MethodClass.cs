using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using api.Global;
using api.Entidades;

namespace api.DbFuntions
{
    public class MethodClass
    {
       
        public static string exec(string request)
        {
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(request,Connection.ConnectionString);
            da.Fill(dt);
            ModGlb.Response = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return ModGlb.Response;
        }

        public static string getgridheader(string request)
        {
       
            string cabeceras = string.Empty;
            string returnHtml = string.Empty;
       
            SqlConnection conexion = new SqlConnection(Connection.ConnectionString);
            conexion.Open();
       
            SqlCommand comando = new SqlCommand(request, conexion);
            SqlDataReader registros = comando.ExecuteReader();
       
            while (registros.Read())
            {
                try
                {
                    cabeceras = registros["Cabecera"].ToString();
                }
                catch (Exception)
                {

                    
                }
            }
       
            string[] columnas = cabeceras.Split(',');
            DinamicHtml gridheader = new DinamicHtml();
       
            gridheader.gridheader.Add("<td></td>");
            foreach (var col in columnas)
            {
                gridheader.gridheader.Add("<td>"+ col + "</td>");
                
            }
       
            conexion.Close();
       
            for (int i = 0; i < gridheader.gridheader.Count; i++)
            {
                returnHtml += gridheader.gridheader[i];
            }
       
            return returnHtml;
        }

        public static string getgridBody(string request)
        {

            string returnJedi = string.Empty;

            SqlDataAdapter da;
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(request, Connection.ConnectionString);
            da.Fill(dt);

            DinamicHtml gridBody = new DinamicHtml();
    
            foreach (DataRow row in dt.Rows)
            {
                gridBody.gribody.Add("<tr>");
                gridBody.gribody.Add("<td><a  href='main/dinamicpageform' onclick='getData();' class='btn  text-warning'><i class='fas fa-folder'></i></a></td>");
                foreach (DataColumn column in dt.Columns)
                {
                    gridBody.gribody.Add("<td>" + row[column] + "</td>");
                }
                gridBody.gribody.Add("</tr>");
            }

            for (int i = 0; i < gridBody.gribody.Count; i++)
            {
                returnJedi += gridBody.gribody[i];
            }

            ModGlb.HtmlGridCount = dt.Rows.Count.ToString(); 

            return returnJedi;
        }

        public static string update(string request)
        {
            SqlConnection conn = new SqlConnection(Connection.ConnectionString);
            conn.Open();
            SqlCommand comando = new SqlCommand(request, conn);
            comando.ExecuteNonQuery();
            conn.Close();
            ModGlb.Response = JsonConvert.SerializeObject("ok", Formatting.Indented);
            return ModGlb.Response;
        }
    }
}
