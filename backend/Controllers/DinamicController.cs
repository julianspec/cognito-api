using api.DbFuntions;
using api.Entidades;
using api.Global;
using CognitowebBR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;

namespace api.Controllers
{

    [ApiController]
    [Route("api/dinamic")]
    public class DinamicController : ControllerBase
    {
		DinamicOption objDinamic = new DinamicOption();

        DinamicFormResponse objFormResponse = new DinamicFormResponse();

        Dato dato = new Dato();

        [HttpGet("gethtml")]
        public string GetHtml(string opcion)
		{
			objDinamic.name = opcion;
			ModGlb.Query = "select Cabecera from angular_gridheader where CONVERT(VARCHAR, Opcion) = '"+ opcion + "'";
			objDinamic.gridheader = MethodClass.getgridheader(ModGlb.Query);
            string tabla = string.Empty;
            switch (objDinamic.name)
            {
                //Seguridad
                case "Accesos x Perfil": tabla = "perfil_acceso"; break;
                case "Perfiles": tabla = "perfil"; break;
                case "Usuario": tabla = "usuario"; break;
                case "Usuarios x Perfil":tabla = "perfil_usuario";break;

                //Administracion
                case "Config.Alarmas":tabla = "alarma_estado";break;
                case "Grupo Afinidad":tabla = "grupo_afinidad_cab";break;
                case "Log Importacion":tabla = "log_importa_Web";break;
                case "Notificaciones":tabla = "notificacion";break;
                case "Sectores Derivacion":tabla = "sucursal_derivacion";break;
                case "Tipo Oper x Sec":tabla = "tipo_oper_sucursal";break;
                case "Sucursales": tabla = "sucursal";break;
                case "Sucursal Tipo":tabla = "sucursal_tipo";break;

                //Encuestas
                case "Cuestionarios":tabla = "cuestionario";break;

                //BCRA
                case "Parametria BCRA":tabla = "producto_bcra";break;
                case "Productos Excuidos":tabla = "producto_exclusion_bcra";break;

                //Configura Mail
                case "Mail Plantilla":tabla = "mail_param";break;
                case "Mail Prod/Estado":tabla = "mail_producto_estado";break;
                case "Plantilla HTML":tabla = "mail_plantilla_html";break;

                //Configuracion
                case "Parametros Generales":tabla = "parametro";break;
                case "Conversion Documentos":tabla = "conv_tipo_doc";break;
                case "Estado Inicial":tabla = "estado_inicial";break;
                case "Estado Pend x Perfil":tabla = "perfil_estado_pend";break;
                case "Rel WEB/Cognito":tabla = "rel_Web_cognito";break;
                case "Secuencia Estados":tabla = "estado_secuencia";break;
                case "Estados":tabla = "estado";break;
                case "Prov/Localidad":tabla = "provincia_localidad";break;
                case "Categ.Cta Cobis":tabla = "categoria_cuenta_cobis";break;

                //Reportes
                case "Reportes Config":tabla = "reporte_cognito";break;
                case "Reportes Perfiles":tabla = "reporte_cognito_permiso";break;
            }

            objDinamic.table = tabla;    
            ModGlb.Query = "execute usp_tabla @i_operacion='VER_TABLA', @i_usuario='" + ModGlb.UserLoged + "', @i_tabla='" + tabla + "'";
            objDinamic.htmlbody = MethodClass.getgridBody(ModGlb.Query);
            objDinamic.count = ModGlb.HtmlGridCount;
            ModGlb.Response = JsonConvert.SerializeObject(objDinamic, Formatting.Indented);
			return ModGlb.Response;
		}

        [HttpGet("gethtmlform")]
        public string gethtmlform(string tabla,string modo,string id1,string id2,string id3,string id4,string id5,string id6,string id7,string id8, string id9, string id10)
        {

            try
            {
                NegFunciones negFunciones = new NegFunciones();

                // ********** INICIALIZA VARIABLES ********** 
                int fila = 0;
                int cabecera = 0;
                string texto = "";
                string valorDato = "";
                string obligatorio = "";
                string nombre = "";
                string label = "";
                string valor = "";
                string tipo = "";
                string modificaAlta = "";
                string modificaEdit = "";
                string soloLectura = "";
                string combo = "";
                string catalogo = "";
                string CampoUsuario = "";
                string campoFAlata = "";
                string campoFModif = "";
                string permisoEditar = "";
                string permisoBorrar = "";
                string parametroPagina = "";
                string columnSpan = "";
                string terminaFila = "";
                string tagFijo = "";
                string tooltip = "";
                int TRFlag = 0;
 

                // ********** OBTIENE LOS DATOS PARA ARMAR LA PAGINA DE LA PAGINA, MUCHOS REGISTROS **********
                string comando = "execute usp_tabla @i_operacion = 'ARMA_PAGINA', @i_usuario = '" + ModGlb.UserLoged + "', @i_tabla = '" + tabla + "', @i_nuevo = 'S'";
                SqlDataReader dr;
                Dato dato = new Dato();
                dr = dato.SpDataReader(comando);
                string[] datosTabla = new string[100];
                StringBuilder sb = new System.Text.StringBuilder("");
                System.Data.DataSet DS1 = new System.Data.DataSet();
                // FIN ********** OBTIENE LOS DATOS DE LA PAGINA **********

                //********** OBTIENE LOS PARAMETROS DE LA TABLA, 1 REGISTRO **********
                datosTabla = dato.TablaParam(tabla);

                //Obtiene y carga el Seteo de la Tabla
                if (datosTabla[19] != null)
                {
                    CampoUsuario = datosTabla[19].Trim();
                }
                if (datosTabla[20] != null)
                {
                    campoFAlata = datosTabla[20].Trim();
                }
                if (datosTabla[21] != null)
                {
                    campoFModif = datosTabla[21].Trim();
                }
                if (datosTabla[11] != null)
                {
                    permisoBorrar = datosTabla[11].Trim();
                }
                if (datosTabla[12] != null)
                {
                    permisoEditar = datosTabla[12].Trim();
                }
                //FIN ********** OBTIENE LOS PARAMETROS DE LA TABLA, 1 REGISTRO **********

                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********
                while (dr.Read()) //LEE CADA FILA
                {
                    // ********** LA PRIMERA VEZ ARMA LA CABECERA DE LA TABLA **********
                    if (cabecera == 0)
                    {
                        if (modo == "edit") //SI ES MODOD EDICION RECUPERA EL VALOR DEL REGISTRO COMPLETO
                        {
                            string[] tablaParam = new string[100];
                            comando = "execute usp_tabla @i_operacion = 'TABLA_PARAM', @i_usuario = 'usuario', @i_tabla = '" + tabla + "'";
                            tablaParam = dato.SpString(comando);
                            id1 = ObtieneClave(tablaParam[1], id1, id2, id3, id4, id5, id6, id7, id8, id9, id10);
                            id2 = ObtieneClave(tablaParam[2], id1, id2, id3, id4, id5, id6, id7, id8, id9, id10);
                            id3 = ObtieneClave(tablaParam[3], id1, id2, id3, id4, id5, id6, id7, id8, id9, id10);

                            comando = "execute usp_tabla @i_operacion = 'SELECT', @i_usuario = '" + ModGlb.UserLoged + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
                            if (id2 != "")
                            {
                                comando = comando + ", @i_id2 = '" + id2 + "'";
                            }
                            if (id3 != "")
                            {
                                comando = comando + ", @i_id3 = '" + id3 + "'";
                            }
                            DS1 = dato.SpDataset(comando);
                            texto = "Pantalla de Edición";
                        }
                        else
                        {
                            texto = "Pantalla de Alta";
                        }

                        // ESCRIBE LOS CAMPOS OCULTOS
                        //sb.Append("<input id='titulo' name='titulo' type='hidden' value='" + titulo + "'/>");
                        sb.Append("<input id='tabla' name='tabla' type='hidden' value='" + tabla + "'/>");
                        sb.Append("<input id='modo' name='modo' type='hidden' value='" + modo + "'/>");
                        sb.Append("<input id='id1' name='id1' type='hidden' value='" + id1 + "'/>");
                        sb.Append("<input id='id2' name='id2' type='hidden' value='" + id2 + "'/>");
                        sb.Append("<input id='id3' name='id3' type='hidden' value='" + id3 + "'/>");
                        sb.Append("<input id='campoU' name='campoU' type='hidden' value='" + CampoUsuario + "'/>");
                        sb.Append("<input id='campoFA' name='campoFA' type='hidden' value='" + campoFAlata + "'/>");
                        sb.Append("<input id='campoFM' name='campoFM' type='hidden' value='" + campoFModif + "'/>");


                        cabecera++;
                    } // FIN ********** LA PRIMERA VEZ ARMA LA CABECERA DE LA TABLA **********

                    // ARMA CADA FILA CON LOS CAMPOS
                    obligatorio = dr[5].ToString();
                    nombre = dr[1].ToString();
                    label = dr[2].ToString();
                    tipo = dr[19].ToString();
                    valor = dr[20].ToString();
                    modificaAlta = dr[6].ToString();
                    modificaEdit = dr[7].ToString();
                    catalogo = dr[8].ToString();
                    //parametroPagina = dr[13].ToString();    //Significa que tiene que tomar un parametro que le llega a la pagina
                    columnSpan = dr[14].ToString();           //Indica si hay que agregar un Column Span
                    terminaFila = dr[15].ToString();          //S=Agrega un </tr><tr> para comenzar una nueva linea
                    tagFijo = dr[16].ToString();              //Indica que es un valor fijo que debe ir sin tomar en cuenta ningun valor del campo
                    tooltip = dr[17].ToString();              //Indica si el campo tiene un tooltip
                    texto = "";
                    combo = "";

                    if (tagFijo.Trim() == "") // No tiene Tag Fijo, es valor de la tabla
                    {
                        // SI ES MODO EDICION RECUPERA EL VALOR DE CADA CAMPO Y CARGA O NO LA PROPIEDAD SOLO LECTURA
                        soloLectura = "";
                        if (modo == "edit")
                        {
                            //BUSCA EL VALOR DEL CAMPO DEL REGISTRO
                            foreach (System.Data.DataRow renglon in DS1.Tables["tabla"].Rows)
                            {
                                valorDato = renglon[nombre].ToString();
                                valorDato = negFunciones.TextoAcento(valorDato);
                            }
                            if (modificaEdit == "N")
                            {
                                soloLectura = " disabled='disabled' ";
                            }
                        }
                        else
                        {
                            valorDato = "";
                            //if (parametroPagina != "")
                            //{
                            //    valorDato = parametro;
                            //}
                            if (modificaAlta == "N")
                            {
                                if (parametroPagina != "")
                                {
                                    soloLectura = " ReadOnly='true' ";
                                }
                                else
                                {
                                    soloLectura = " disabled='disabled' ";
                                }
                            }
                        }

                        // *** ARMA EL RENGLON ***
                        if (TRFlag == 0) // Campo Obligatorio
                        {
                            sb.Append("<div class='form-group col-md-4'>");
                            TRFlag = 1;
                        }
                        // FIN *** ARMA EL RENGLON ***


                        // *** ARMA EL LABEL ***
                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            texto = "style='color:red'";
                        }
                        else
                        {
                            texto = "";
                        }

                        sb.Append("<label "+texto+" >" + label + "</label>");
                        // FIN - *** ARMA EL LABEL ***

                        // *** ARMA EL OBJETO ***
                        //texto = ArmaObjeto(dr[1].ToString(), dr[4].ToString(), dr[5].ToString(), modo, valorDato);
                        texto = valor;  //CARGA EL OBJETO
                        texto = texto.Replace("@ID@", nombre);  // LE COLOCA EL ID

                        if (tooltip.Trim().Length > 1) // VERIFICA SI TIENE TOOLTIP
                        {
                            texto = texto.Replace("@TOOLTIP@", " title='" + tooltip + "' ");  // COLOCA TOOLTIP
                        }
                        else
                        {
                            texto = texto.Replace("@TOOLTIP@", "");  // NO COLOCA TOOLTIP
                        }

                        // CARGA EL VALOR AL OBJETO SEGUN EL TIPO
                        switch (tipo)
                        {
                            case "combo": //COMBO BOX
                                combo = dato.ArmaCombo(tabla, nombre, valorDato,modo);
                                texto = texto.Replace("@VAL@", combo);  // CARGA EL VALOR
                                break;
                            case "radio_si_no": //RADIO BUTTON
                                texto = dato.ArmaRadio(texto, valorDato); // CARGA EL VALOR
                                break;
                            default: //RESTO DE LOS OBJETOS
                                valorDato = negFunciones.TextoAcento(valorDato);
                                texto = texto.Replace("@VAL@", valorDato);  // CARGA EL VALOR
                                break;
                        }
                        // FIN: LE CARGA EL VALOR

                        // Verifica Solo Lectura
                        if (soloLectura != null)
                        {
                            texto = texto.Replace("@RO@", soloLectura);  // CARGA EL ATRIBUTO DESHABILITADO
                        }

                        //ESCRIBE EL OBJETO
                        sb.Append(texto + "<br/>");

                        // FIN - *** ARMA EL CAMPO ***

                        // *** FIN DEL RENGLON ***
                        //if (terminaFila == "S" || terminaFila == "") // Campo Obligatorio
                        //{
                            sb.Append("</div>");
                            TRFlag = 0;
                        //}
                        // *** FIN DEL RENGLON ***
                    }
                    else    // Tiene TAG FIJO se escribe solo el tag, no es valor de una tabla
                    {
                        sb.Append(tagFijo);
                    }

                    fila++;
                }
           
                objFormResponse.html = sb.ToString();
                return JsonConvert.SerializeObject(objFormResponse, Formatting.Indented);
            }
            catch (Exception e)
            {
                StringBuilder sb = new System.Text.StringBuilder("");
                sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sb.Append("<head id='Head1'><title>Sistema PegasoWebWeb</title><link href='estilo.css' rel='stylesheet' type='text/css' /></head>");
                sb.Append("<body style='background-image:url(imagenes/fondo_menu.jpg)'>");
                sb.Append("<form method='post' id='form1'><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>");
                sb.Append("<p><center>" + e + "</center></p>");
                sb.Append("</form></body></html>");

                objFormResponse.html = sb.ToString();
                return JsonConvert.SerializeObject(objFormResponse, Formatting.Indented);
            }
        }

        string ObtieneClave(string parametro, string id1, string id2, string id3, string id4, string id5, string id6, string id7, string id8, string id9, string id10)
        {
            string resultado = "";
            switch (parametro)
            {
                case "1": resultado = id1; break;
                case "2": resultado = id2; break;
                case "3": resultado = id3; break;
                case "4": resultado = id4; break;
                case "5": resultado = id5; break;
                case "6": resultado = id6; break;
                case "7": resultado = id7; break;
                case "8": resultado = id8; break;
                case "9": resultado = id9; break;
                case "10": resultado = id10; break;
                default: resultado = ""; break;
            }

            return resultado;
        }

        [HttpGet("saveParams")]
        public string getForm(string formParams, string modo)
        {
            string mensaje = "";
            formParams = formParams.Replace("[", "");
            formParams = formParams.Replace("]", "");
            formParams = formParams.Replace("\",\"", "|");
            formParams = formParams.Replace("\"", "");

            List<string> listParams = new List<string>();
            char[] delimiterChars = { '|' };

            string[] parameters = formParams.Split(delimiterChars);
   
            foreach (var param in parameters)
            {
                listParams.Add(param);
            }

            string[] propname  = new string[listParams.Count];
            string[] propvalue = new string[listParams.Count];

            for (int i = 0; i < listParams.Count; i++)
            {
                propname[i] =  listParams[i].Split(':')[0];
                propvalue[i] = listParams[i].Split(':')[1];
            }

            switch (modo)
            {
               
                case "edit":
                    CreateEditController editController = new CreateEditController();
                    mensaje = editController.SetData(propname, propvalue,"edit");
                    break;

                case "alta":
                    CreateEditController createController = new CreateEditController(); 
                    mensaje = createController.SetData(propname, propvalue,"alta");
                    break;

                case "delete":
                    DeleteController deleteController = new DeleteController();
                    mensaje = deleteController.SetData(propvalue); 
                    break;
            }

            ModGlb.Response = JsonConvert.SerializeObject(mensaje, Formatting.Indented);
            return ModGlb.Response;
        }
  
    }
}