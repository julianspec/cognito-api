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
    public class NegTabla
    {
        private Usuario usuario = new Usuario();
        private Formulario formulario = new Formulario();
        private NegSeguridad negSeguridad = new NegSeguridad();

        public NegTabla()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public System.Data.DataSet Acceso()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_accesos @i_operacion = 'DOCUMENTOS', @i_usuario = '" + usuario.usuario + "'";

                System.Data.DataSet ds = new System.Data.DataSet();
                Dato dato = new Dato();
                ds = dato.SpQuery(comando);

                return ds;
        }

        public String DatosUsuario()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_accesos @i_operacion = 'DATOS_USUARIO', @i_usuario = '" + usuario.usuario + "'";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            usuario.nombre = resultado[1];
            usuario.sucursal = resultado[2];
            usuario.sucursalNombre = resultado[3];
            usuario.estado = resultado[4];
            usuario.estadoDescripcion = resultado[5];
            usuario.responsable = resultado[6];
            usuario.fichaCensal = resultado[7];

            System.Web.HttpContext.Current.Session.Add("usuario", usuario);

            return usuario.nombre;
        }

        public System.Data.DataSet TipoPend()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'SEL_TIPO_PEND', @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }
        
        public System.Data.DataSet Sucursales(string tipoPend)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'SEL_SUCURSAL', @i_usuario = '" + usuario.usuario + "'";
            if (tipoPend == null)
            {
                tipoPend = "0";
            }
            comando = comando + ", @i_tipo_pend = " + tipoPend;

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }
        
        public System.Data.DataSet Estado()
        {
            string comando = "execute sp_tabla @i_operacion = 'SEL_ESTADO'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }
        
        public System.Data.DataSet EstadoConsulta()
        {
            string comando = "execute sp_tabla @i_operacion = 'SEL_ESTADO_CONSULTA'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet EstadoFlow()
        {
            formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'ESTADO_FLOW'";
            comando = comando + ", @i_estado  = '" + formulario.estado + "'";
            comando = comando + ", @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet EstadosTodos()
        {
            formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'ESTADOS_TODOS'";
            comando = comando + ", @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public String ObtieneEstadoIncial(string tipoOperacion)
        {
            string comando = "execute sp_tabla @i_operacion = 'ESTADO_INICIAL'";
            comando = comando + ", @i_tipo_operacion = " + tipoOperacion;

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] == "")
            {
                resultado[0] = "P";
            }

            return resultado[0];
        }

        public String VerificaEstadoActual()
        {
            formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'VERIFICA_ESTADO_ACTUAL'";
            comando = comando + ", @i_estado  = '" + formulario.estado + "'";
            comando = comando + ", @i_usuario = '" + usuario.usuario + "'";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public System.Data.DataSet Catalogo(string tabla)
        {
            string comando = "";
            if (tabla == "TIPOS DE DOCUMENTOS"){
                comando = "execute sp_catalogo @i_operacion = '" + tabla + "', @i_tabla = '" + tabla + "'";
            }
            else {
                comando = "execute sp_catalogo @i_operacion = 'SEL_TABLA', @i_tabla = '" + tabla + "'";
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet Catalogo(string tabla, string producto)
        {
            string comando = "";
            // SI ES DEBITO CAMBIA EL CATALOGO DE TARJETAS DE CREDITO POR DEBITO. CASO CONTRARIO QUEDA EL DE CREDITO
            if (producto == "DEBITO")
            {
                tabla = "MARCAS DE TARJETAS DE DEBITO";
            }
            
            comando = "execute sp_catalogo @i_operacion = 'SEL_TABLA', @i_tabla = '" + tabla + "'";
            
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet CatalogoConReferencia(string tipoOperacion, string producto, string subproducto, string motivo)
        {
            string comando = "";
            comando = "execute sp_catalogo @i_operacion = 'CATALOGO_REFERENCIA', @i_desc = '" + tipoOperacion + "-" + producto + "-" + subproducto + "-" + motivo + "'";
            
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public string CatalogoValor(string tabla, string descripcion)
        {
            string comando = "";
            comando = "execute sp_catalogo @i_operacion = 'BUSCA_DESC', @i_tabla = '" + tabla + "', @i_desc = '" + descripcion + "' ";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public System.Data.DataSet TipoOperacion()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'PRODUCTO_1', @i_usuario = '" + usuario.usuario + "'";
            
            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet TipoOperacionCargaInicial()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'PRODUCTO_CARGA', @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet TipoOperacionSinFiltro()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'TIPO_OPERACION', @i_usuario = '" + usuario.usuario + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public System.Data.DataSet Producto(string producto)
        {
            string comando = "execute sp_tabla @i_operacion = 'PRODUCTO_2', @i_producto = " + producto ;

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public Boolean CargaCombo(ref System.Web.UI.WebControls.DropDownList combo, string valor)
        {
            try
            {
                if (valor != null && valor != "" && valor != "0")
                {
                    for(int i=0;i<combo.Items.Count;i++) 
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

        public System.Data.DataSet UsuarioSucursal(string sucursal, string estado)
        {
            string comando = "execute sp_tabla @i_operacion = 'USUARIO_SUC', @i_sucursal = " + sucursal ;
            if (estado != "")
            {
                comando = comando + ", @i_estado = '" + estado + "' ";
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            Dato dato = new Dato();
            ds = dato.SpQuery(comando);

            return ds;
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

        public String Parametro(string tabla)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            Dato dato = new Dato();

            string comando = "execute sp_catalogo @i_operacion = 'PARAMETRO' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public StringBuilder ArmaPagina(string tabla, string modo, string titulo, string id1, string id2, string id3, string volver)
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
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                // FIN ********** INICIALIZA VARIABLES ********** 


                // ********** OBTIENE LOS DATOS PARA ARMAR LA PAGINA DE LA PAGINA, MUCHOS REGISTROS **********
                string comando = "execute usp_tabla @i_operacion = 'ARMA_PAGINA', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "'";
                SqlDataReader dr;
                Dato dato = new Dato();
                dr = dato.SpDataReader(comando);
                string[] datosTabla = new string[100];
                StringBuilder sb = new System.Text.StringBuilder("");
                System.Data.DataSet DS1 = new System.Data.DataSet();
                // FIN ********** OBTIENE LOS DATOS DE LA PAGINA **********


                //********** OBTIENE LOS PARAMETROS DE LA TABLA, 1 REGISTRO **********
                datosTabla = TablaParam(tabla);

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


                // ********** CABECERA DE LA PAGINA **********
                sb.Append("<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'>");
                sb.Append("<head id='Head1'><meta http-equiv='Content-Type' content='text/html; charset=windows-1252'/><meta http-equiv='X-UA-Compatible' content='IE=7,8,9,10,11'/><title>Sistema PegasoWebWeb</title><link href='estilo.css' rel='stylesheet' type='text/css' /></head>");
                sb.Append("<body style='background-image:url(imagenes/fondo.jpg)'>");
                sb.Append("<form method=post id='form1' action='FormGraba.aspx'>");

                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********
                while (dr.Read()) //LEE CADA FILA
                {
                    // ********** LA PRIMERA VEZ ARMA LA CABECERA DE LA TABLA **********
                    if (cabecera == 0)
                    {
                        if (modo == "edit") //SI ES MODOD EDICION RECUPERA EL VALOR DEL REGISTRO COMPLETO
                        {
                            texto = "execute usp_tabla @i_operacion = 'SELECT', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
                            if (id2 != null)
                            {
                                texto = texto + ", @i_id2 = '" + id2 + "'";
                            }
                            if (id3 != null)
                            {
                                texto = texto + ", @i_id3 = '" + id3 + "'";
                            }
                            DS1 = dato.SpDataset(texto);
                            texto = "Pantalla de Edición";
                        }
                        else
                        {
                            texto = "Pantalla de Alta";
                        }
                        // ESCRIBE EL TITULO
                        sb.Append("<div class='tituloform'>" + titulo + "</div>");
                        sb.Append("<p class='mensajeazul'>" + texto + "</p>");

                        // ESCRIBE LOS CAMPOS OCULTOS
                        volver = volver + "?tabla=" + tabla + "&titulo=" + titulo;
                        sb.Append("<p>");
                        sb.Append("<a href='" + volver + "' style='border:none'><input id='volver' type='text' value='Volver' readonly='true' class='botoner' style='text-align:center'/></a>");
                        sb.Append("<input id='titulo' name='titulo' type='hidden' value='" + titulo + "'/>");
                        sb.Append("<input id='tabla' name='tabla' type='hidden' value='" + tabla + "'/>");
                        sb.Append("<input id='modo' name='modo' type='hidden' value='" + modo + "'/>");
                        sb.Append("<input id='id1' name='id1' type='hidden' value='" + id1 + "'/>");
                        sb.Append("<input id='id2' name='id2' type='hidden' value='" + id2 + "'/>");
                        sb.Append("<input id='id3' name='id3' type='hidden' value='" + id3 + "'/>");
                        sb.Append("<input id='campoU' name='campoU' type='hidden' value='" + CampoUsuario + "'/>");
                        sb.Append("<input id='campoFA' name='campoFA' type='hidden' value='" + campoFAlata + "'/>");
                        sb.Append("<input id='campoFM' name='campoFM' type='hidden' value='" + campoFModif + "'/>");
                        sb.Append("</p>");

                        // ARMA LA TABLA
                        sb.Append("<table  width='90%' border='0' align='center' cellspacing='3'>");

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
                    columnSpan = dr[14].ToString();         //Indica si hay que agregar un Column Span
                    terminaFila = dr[15].ToString();        //S=Agrega un </tr><tr> para comenzar una nueva linea
                    tagFijo = dr[16].ToString();            //Indica que es un valor fijo que debe ir sin tomar en cuenta ningun valor del campo
                    tooltip = dr[17].ToString();            //Indica si el campo tiene un tooltip
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
                            sb.Append("<tr width='99%'>");
                            TRFlag = 1;
                        }
                        // FIN *** ARMA EL RENGLON ***


                        // *** ARMA EL LABEL ***
                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            texto = "labelRojo";
                        }
                        else
                        {
                            texto = "label";
                        }
                        sb.Append("<td class='" + texto + "' style='text-align:right'>" + label + "</td>");
                        // FIN - *** ARMA EL LABEL ***

                        // *** ARMA EL OBJETO ***
                        //texto = ArmaObjeto(dr[1].ToString(), dr[4].ToString(), dr[5].ToString(), modo, valorDato);
                        texto = valor;  //CARGA EL OBJETO
                        texto = texto.Replace("@ID@", nombre);  // LE COLOCA EL ID

                        if (tooltip.Trim().Length > 1) // VERIFICA SI TIENE TOOLTIP
                        {
                            texto = texto.Replace("@TOOLTIP@", " title='"+ tooltip +"' ");  // COLOCA TOOLTIP
                        }
                        else
                        {
                            texto = texto.Replace("@TOOLTIP@", "");  // NO COLOCA TOOLTIP
                        }

                        // CARGA EL VALOR AL OBJETO SEGUN EL TIPO
                        switch (tipo)
                        {
                            case "combo": //COMBO BOX
                                combo = ArmaCombo(tabla, nombre, valorDato, modo);
                                texto = texto.Replace("@VAL@", combo);  // CARGA EL VALOR
                                break;
                            case "radio_si_no": //RADIO BUTTON
                                texto = ArmaRadio(texto, valorDato); // CARGA EL VALOR
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
                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            texto = texto + "<span class='labelRojo'>  *</span>";
                        }

                        // Verifica ColumnmSpan
                        if (columnSpan == "" || columnSpan == "0")
                        {
                            columnSpan = "";
                        }
                        else
                        {
                            columnSpan = " colspan='" + columnSpan + "' ";
                        }


                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            sb.Append("<td class='labelObligatorio'" + columnSpan + ">" + texto + "</td>");
                        }
                        else
                        {
                            sb.Append("<td class='label'" + columnSpan + ">" + texto + "</td>");
                        }
                        // FIN - *** ARMA EL CAMPO ***

                        // *** FIN DEL RENGLON ***
                        if (terminaFila == "S" || terminaFila == "") // Campo Obligatorio
                        {
                            sb.Append("</tr>");
                            TRFlag = 0;
                        }
                        // *** FIN DEL RENGLON ***
                    }
                    else    // Tiene TAG FIJO se escribe solo el tag, no es valor de una tabla
                    {
                        sb.Append(tagFijo);
                    }

                    fila++;
                }
                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********


                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********
                if (fila > 0)
                {
                    // *** ARMA EL FIN DE LA TABLA DE DATOS ***
                    if (TRFlag == 1) // Campo Obligatorio
                    {
                        sb.Append("</tr>");
                    }
                    sb.Append("<table  width='90%' border='0' align='center' cellspacing='3'>");
                    // FIN *** ARMA EL FIN DE LA TABLA DE DATOS ***

                    //ARMA LA ULTIMA LINEA CON LOS BOTONES
                    //sb.Append("<tr class='botoner'><td style='text-align:right'><img src='imagenes/volver.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px' onclick='history.back();' /><div>Atras</div></td>");
                    sb.Append("<tr class='botoner'><td style='text-align:right'><a href='" + volver + "'><img src='imagenes/anterior.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Atras</div></td>");
                    if (permisoEditar != "" || modo == "alta")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoEditar))) // Valida si permite editar o no el registro
                        {
                            sb.Append("<td style='text-align:center'><a class='btnsubmit'><img src='imagenes/guardar_anim.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Guardar</div></td>");
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                    }
                    if (permisoBorrar != "" && modo == "edit")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoBorrar))) // Valida si permite editar o no el registro
                        {
                            texto = "FormBorrar.aspx?tabla=" + tabla.Trim() + "&titulo=" + titulo.Trim() + "&modo=borrar&ID1=" + id1.Trim();
                            if (id2 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID2=" + id2.Trim();
                            }
                            if (id3 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID3=" + id3.Trim();
                            }
                            sb.Append("<td><div style='text-align:center'><a href='" + texto + "'><img src='imagenes/borrar.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Borrar</div></td>");
                        }

                    }
                    sb.Append("</tr> <tr><td>&nbsp;</td><td><div id='validacion' class='errores'>Falta Completar Datos Obligatorios</div></td></tr>");
                    sb.Append("</table>");
                }
                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********


                // FIN DE LA PAGINA
                sb.Append("</form></body></html>");
                return sb;
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

                return sb;
            }
        }

        public StringBuilder ArmaPaginaAngular(string tabla, string modo, string titulo, string id1, string id2, string id3, string volver)
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
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                // FIN ********** INICIALIZA VARIABLES ********** 


                // ********** OBTIENE LOS DATOS PARA ARMAR LA PAGINA DE LA PAGINA, MUCHOS REGISTROS **********
                string comando = "execute usp_tabla @i_operacion = 'ARMA_PAGINA', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "'";
                SqlDataReader dr;
                Dato dato = new Dato();
                dr = dato.SpDataReader(comando);
                string[] datosTabla = new string[100];
                StringBuilder sb = new System.Text.StringBuilder("");
                System.Data.DataSet DS1 = new System.Data.DataSet();
                // FIN ********** OBTIENE LOS DATOS DE LA PAGINA **********


                //********** OBTIENE LOS PARAMETROS DE LA TABLA, 1 REGISTRO **********
                datosTabla = TablaParam(tabla);

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


                // ********** CABECERA DE LA PAGINA **********
                //sb.Append("<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'>");
                //sb.Append("<head id='Head1'><meta http-equiv='Content-Type' content='text/html; charset=windows-1252'/><meta http-equiv='X-UA-Compatible' content='IE=7,8,9,10,11'/><title>Sistema PegasoWebWeb</title><link href='estilo.css' rel='stylesheet' type='text/css' /></head>");
                //sb.Append("<body style='background-image:url(imagenes/fondo.jpg)'>");
                //sb.Append("<form method=post id='form1' action='FormGraba.aspx'>"); ********

                sb.Append("<div class='card'>");
                sb.Append("<div class='card-header bg-light'>");
                sb.Append("<h4>" + titulo + " - Pantalla de Edicion</h4></div><br>");
                sb.Append("<div class='container-fluid'>");
                sb.Append("<div class='row'>");
                sb.Append("<div class='col'>");
                sb.Append("<button class='btn btn-primary' style='align-items: center;' (click)='volver()'><i class='fa fa-user' aria-hidden='true'></i>&nbsp;Volver</button>&nbsp;&nbsp;");
                sb.Append("</div></div></div><div class='card-body'>");
                sb.Append("<div class='row container centrar-form'>");



                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********
                while (dr.Read()) //LEE CADA FILA
                {
                    // ********** LA PRIMERA VEZ ARMA LA CABECERA DE LA TABLA **********
                    if (cabecera == 0)
                    {
                        if (modo == "edit") //SI ES MODOD EDICION RECUPERA EL VALOR DEL REGISTRO COMPLETO
                        {
                            texto = "execute usp_tabla @i_operacion = 'SELECT', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
                            if (id2 != null)
                            {
                                texto = texto + ", @i_id2 = '" + id2 + "'";
                            }
                            if (id3 != null)
                            {
                                texto = texto + ", @i_id3 = '" + id3 + "'";
                            }
                            DS1 = dato.SpDataset(texto);
                            texto = "Pantalla de Edición";
                        }
                        else
                        {
                            texto = "Pantalla de Alta";
                        }

                        // ESCRIBE LOS CAMPOS OCULTOS
                        sb.Append("<input id='titulo' name='titulo' type='hidden' value='" + titulo + "'/>");
                        sb.Append("<input id='tabla' name='tabla' type='hidden' value='" + tabla + "'/>");
                        sb.Append("<input id='modo' name='modo' type='hidden' value='" + modo + "'/>");
                        sb.Append("<input id='id1' name='id1' type='hidden' value='" + id1 + "'/>");
                        sb.Append("<input id='id2' name='id2' type='hidden' value='" + id2 + "'/>");
                        sb.Append("<input id='id3' name='id3' type='hidden' value='" + id3 + "'/>");
                        sb.Append("<input id='campoU' name='campoU' type='hidden' value='" + CampoUsuario + "'/>");
                        sb.Append("<input id='campoFA' name='campoFA' type='hidden' value='" + campoFAlata + "'/>");
                        sb.Append("<input id='campoFM' name='campoFM' type='hidden' value='" + campoFModif + "'/>");
                        sb.Append("</p>");

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
                    columnSpan = dr[14].ToString();         //Indica si hay que agregar un Column Span
                    terminaFila = dr[15].ToString();        //S=Agrega un </tr><tr> para comenzar una nueva linea
                    tagFijo = dr[16].ToString();            //Indica que es un valor fijo que debe ir sin tomar en cuenta ningun valor del campo
                    tooltip = dr[17].ToString();            //Indica si el campo tiene un tooltip
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
                            sb.Append("<div class='col container'>");
                            TRFlag = 1;
                        }
                        // FIN *** ARMA EL RENGLON ***


                        // *** ARMA EL LABEL ***
                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            texto = "labelRojo";
                        }
                        else
                        {
                            texto = "label";
                        }
                        //VER sb.Append("<td class='" + texto + "' style='text-align:right'>" + label + "</td>");
                        sb.Append("<label class='form-label'>" + label + "</label>");
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
                                combo = ArmaCombo(tabla, nombre, valorDato, modo);
                                texto = texto.Replace("@VAL@", combo);  // CARGA EL VALOR
                                break;
                            case "radio_si_no": //RADIO BUTTON
                                texto = ArmaRadio(texto, valorDato); // CARGA EL VALOR
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
                        // Verifica si es obligatorio
                        //if (obligatorio == "S") // Campo Obligatorio
                        //{
                        //    texto = texto + "<span class='labelRojo'>  *</span>";
                        //}

                        // Verifica ColumnmSpan
                        //if (columnSpan == "" || columnSpan == "0")
                        //{
                        //    columnSpan = "";
                        //}
                        //else
                        //{
                        //    columnSpan = " colspan='" + columnSpan + "' ";
                        //}

                        //VERRRRR
                        //if (obligatorio == "S") // Campo Obligatorio
                        //{
                        //    sb.Append("<td class='labelObligatorio'" + columnSpan + ">" + texto + "</td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td class='label'" + columnSpan + ">" + texto + "</td>");
                        //}

                        //ESCRIBE EL OBJETO
                        sb.Append(texto + "</div>");

                        // FIN - *** ARMA EL CAMPO ***

                        // *** FIN DEL RENGLON ***
                        //VERRR
                        //if (terminaFila == "S" || terminaFila == "") // Campo Obligatorio
                        //{
                        //    sb.Append("</tr>");
                        //    TRFlag = 0;
                        //}
                        // *** FIN DEL RENGLON ***
                    }
                    else    // Tiene TAG FIJO se escribe solo el tag, no es valor de una tabla
                    {
                        sb.Append(tagFijo);
                    }

                    fila++;
                }
                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********


                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********
                if (fila > 0)
                {
                    // *** ARMA EL FIN DE LA TABLA DE DATOS ***
                    if (TRFlag == 1) // Campo Obligatorio
                    {
                        sb.Append("<div class='col'></div></div></div>");
                    }
                    // FIN *** ARMA EL FIN DE LA TABLA DE DATOS ***

                    // ARMA EL CONTENEDOR DE LOS BOTONES
                    sb.Append("<div class='card-footer text-right'>");

                    //ARMA LA ULTIMA LINEA CON LOS BOTONES
                    // Volver
                    sb.Append("<button class='btn btn-primary mr-1' type='submit'><i class='fa fa-arrow-left' aria-hidden='true'></i>&nbsp;Atras</button>");

                    // Guardar
                    if (permisoEditar != "" || modo == "alta")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoEditar))) // Valida si permite editar o no el registro
                        {
                            //sb.Append("<td style='text-align:center'><a class='btnsubmit'><img src='imagenes/guardar_anim.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Guardar</div></td>");
                            sb.Append("<button class='btn btn-primary mr-1' type='submit'><i class='fa fa-save' aria-hidden='true'></i>&nbsp;Guardar</button>");
                        }
                        else
                        {
                            sb.Append("");
                        }
                    }

                    //Borrar
                    if (permisoBorrar != "" && modo == "edit")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoBorrar))) // Valida si permite editar o no el registro
                        {
                            texto = "FormBorrar.aspx?tabla=" + tabla.Trim() + "&titulo=" + titulo.Trim() + "&modo=borrar&ID1=" + id1.Trim();
                            if (id2 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID2=" + id2.Trim();
                            }
                            if (id3 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID3=" + id3.Trim();
                            }
                            //sb.Append("<td><div style='text-align:center'><a href='" + texto + "'><img src='imagenes/borrar.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Borrar</div></td>");
                            sb.Append("<button class='btn btn-danger  mr-1' type='submit'><i class='fa fa-trash' aria-hidden='true'></i>&nbsp;Borrar</button>");
                        }
                    }
                    sb.Append("</div>");
                }
                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********

                // FIN DE LA PAGINA
                //sb.Append("</form></body></html>");
                return sb;
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

                return sb;
            }
        }

       /* public StringBuilder ArmaPaginaAngular(string tabla, string modo, string titulo, string id1, string id2, string id3, string volver)
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
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                // FIN ********** INICIALIZA VARIABLES ********** 


                // ********** OBTIENE LOS DATOS PARA ARMAR LA PAGINA DE LA PAGINA, MUCHOS REGISTROS **********
                string comando = "execute usp_tabla @i_operacion = 'ARMA_PAGINA', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "'";
                SqlDataReader dr;
                Dato dato = new Dato();
                dr = dato.SpDataReader(comando);
                string[] datosTabla = new string[100];
                StringBuilder sb = new System.Text.StringBuilder("");
                System.Data.DataSet DS1 = new System.Data.DataSet();
                // FIN ********** OBTIENE LOS DATOS DE LA PAGINA **********


                //********** OBTIENE LOS PARAMETROS DE LA TABLA, 1 REGISTRO **********
                datosTabla = TablaParam(tabla);

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


                // ********** CABECERA DE LA PAGINA **********
                //sb.Append("<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'>");
                //sb.Append("<head id='Head1'><meta http-equiv='Content-Type' content='text/html; charset=windows-1252'/><meta http-equiv='X-UA-Compatible' content='IE=7,8,9,10,11'/><title>Sistema PegasoWebWeb</title><link href='estilo.css' rel='stylesheet' type='text/css' /></head>");
                //sb.Append("<body style='background-image:url(imagenes/fondo.jpg)'>");
                //sb.Append("<form method=post id='form1' action='FormGraba.aspx'>"); ********

                sb.Append("<div class='card'>");
                sb.Append("<div class='card-header bg-light'>");
                sb.Append("<h4>" + titulo + " - Pantalla de Edicion</h4></div><br>");
                sb.Append("<div class='container-fluid'>");
                sb.Append("<div class='row'>");
                sb.Append("<div class='col'>");
                sb.Append("<button class='btn btn-primary' style='align-items: center;' (click)='volver()'><i class='fa fa-user' aria-hidden='true'></i>&nbsp;Volver</button>&nbsp;&nbsp;");
                sb.Append("</div></div></div><div class='card-body'>");
                sb.Append("<div class='row container centrar-form'>");



                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********
                while (dr.Read()) //LEE CADA FILA
                {
                    // ********** LA PRIMERA VEZ ARMA LA CABECERA DE LA TABLA **********
                    if (cabecera == 0)
                    {
                        if (modo == "edit") //SI ES MODOD EDICION RECUPERA EL VALOR DEL REGISTRO COMPLETO
                        {
                            texto = "execute usp_tabla @i_operacion = 'SELECT', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
                            if (id2 != null)
                            {
                                texto = texto + ", @i_id2 = '" + id2 + "'";
                            }
                            if (id3 != null)
                            {
                                texto = texto + ", @i_id3 = '" + id3 + "'";
                            }
                            DS1 = dato.SpDataset(texto);
                            texto = "Pantalla de Edición";
                        }
                        else
                        {
                            texto = "Pantalla de Alta";
                        }

                        // ESCRIBE LOS CAMPOS OCULTOS
                        sb.Append("<input id='titulo' name='titulo' type='hidden' value='" + titulo + "'/>");
                        sb.Append("<input id='tabla' name='tabla' type='hidden' value='" + tabla + "'/>");
                        sb.Append("<input id='modo' name='modo' type='hidden' value='" + modo + "'/>");
                        sb.Append("<input id='id1' name='id1' type='hidden' value='" + id1 + "'/>");
                        sb.Append("<input id='id2' name='id2' type='hidden' value='" + id2 + "'/>");
                        sb.Append("<input id='id3' name='id3' type='hidden' value='" + id3 + "'/>");
                        sb.Append("<input id='campoU' name='campoU' type='hidden' value='" + CampoUsuario + "'/>");
                        sb.Append("<input id='campoFA' name='campoFA' type='hidden' value='" + campoFAlata + "'/>");
                        sb.Append("<input id='campoFM' name='campoFM' type='hidden' value='" + campoFModif + "'/>");
                        sb.Append("</p>");

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
                    columnSpan = dr[14].ToString();         //Indica si hay que agregar un Column Span
                    terminaFila = dr[15].ToString();        //S=Agrega un </tr><tr> para comenzar una nueva linea
                    tagFijo = dr[16].ToString();            //Indica que es un valor fijo que debe ir sin tomar en cuenta ningun valor del campo
                    tooltip = dr[17].ToString();            //Indica si el campo tiene un tooltip
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
                            sb.Append("<div class='col container'>");
                            TRFlag = 1;
                        }
                        // FIN *** ARMA EL RENGLON ***


                        // *** ARMA EL LABEL ***
                        if (obligatorio == "S") // Campo Obligatorio
                        {
                            texto = "labelRojo";
                        }
                        else
                        {
                            texto = "label";
                        }
                        //VER sb.Append("<td class='" + texto + "' style='text-align:right'>" + label + "</td>");
                        sb.Append("<label class='form-label'>" + label + "</label>");
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
                                combo = ArmaCombo(tabla, nombre, valorDato, modo);
                                texto = texto.Replace("@VAL@", combo);  // CARGA EL VALOR
                                break;
                            case "radio_si_no": //RADIO BUTTON
                                texto = ArmaRadio(texto, valorDato); // CARGA EL VALOR
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
                        // Verifica si es obligatorio
                        //if (obligatorio == "S") // Campo Obligatorio
                        //{
                        //    texto = texto + "<span class='labelRojo'>  *</span>";
                        //}

                        // Verifica ColumnmSpan
                        //if (columnSpan == "" || columnSpan == "0")
                        //{
                        //    columnSpan = "";
                        //}
                        //else
                        //{
                        //    columnSpan = " colspan='" + columnSpan + "' ";
                        //}

                        //VERRRRR
                        //if (obligatorio == "S") // Campo Obligatorio
                        //{
                        //    sb.Append("<td class='labelObligatorio'" + columnSpan + ">" + texto + "</td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td class='label'" + columnSpan + ">" + texto + "</td>");
                        //}

                        //ESCRIBE EL OBJETO
                        sb.Append(texto + "</div>");

                        // FIN - *** ARMA EL CAMPO ***

                        // *** FIN DEL RENGLON ***
                        //VERRR
                        //if (terminaFila == "S" || terminaFila == "") // Campo Obligatorio
                        //{
                        //    sb.Append("</tr>");
                        //    TRFlag = 0;
                        //}
                        // *** FIN DEL RENGLON ***
                    }
                    else    // Tiene TAG FIJO se escribe solo el tag, no es valor de una tabla
                    {
                        sb.Append(tagFijo);
                    }

                    fila++;
                }
                //********** RECORRE CADA REGISTRO DE LA TABLA PARA IR ARMANDO LAS FILAS CON LOS CAMPOS **********


                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********
                if (fila > 0)
                {
                    // *** ARMA EL FIN DE LA TABLA DE DATOS ***
                    if (TRFlag == 1) // Campo Obligatorio
                    {
                        sb.Append("<div class='col'></div></div></div>");
                    }
                    // FIN *** ARMA EL FIN DE LA TABLA DE DATOS ***

                    // ARMA EL CONTENEDOR DE LOS BOTONES
                    sb.Append("<div class='card-footer text-right'>");

                    //ARMA LA ULTIMA LINEA CON LOS BOTONES
                    // Volver
                    sb.Append("<button class='btn btn-primary mr-1' type='submit'><i class='fa fa-arrow-left' aria-hidden='true'></i>&nbsp;Atras</button>");

                    // Guardar
                    if (permisoEditar != "" || modo == "alta")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoEditar))) // Valida si permite editar o no el registro
                        {
                            //sb.Append("<td style='text-align:center'><a class='btnsubmit'><img src='imagenes/guardar_anim.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Guardar</div></td>");
                            sb.Append("<button class='btn btn-primary mr-1' type='submit'><i class='fa fa-save' aria-hidden='true'></i>&nbsp;Guardar</button>");
                        }
                        else
                        {
                            sb.Append("");
                        }
                    }

                    //Borrar
                    if (permisoBorrar != "" && modo == "edit")
                    {
                        if (negSeguridad.VerificaAcceso(Convert.ToInt16(permisoBorrar))) // Valida si permite editar o no el registro
                        {
                            texto = "FormBorrar.aspx?tabla=" + tabla.Trim() + "&titulo=" + titulo.Trim() + "&modo=borrar&ID1=" + id1.Trim();
                            if (id2 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID2=" + id2.Trim();
                            }
                            if (id3 != null) //Segunda Clave de Busqueda
                            {
                                texto = texto + "&ID3=" + id3.Trim();
                            }
                            //sb.Append("<td><div style='text-align:center'><a href='" + texto + "'><img src='imagenes/borrar.gif' style='border-color:#666666; border-width:2px; height:40px; width:40px'/></a><div>Borrar</div></td>");
                            sb.Append("<button class='btn btn-danger  mr-1' type='submit'><i class='fa fa-trash' aria-hidden='true'></i>&nbsp;Borrar</button>");
                        }
                    }
                    sb.Append("</div>");
                }
                //********** ARMA FIN DE LA PAGINA CON LOS BOTONES ***********

                // FIN DE LA PAGINA
                //sb.Append("</form></body></html>");
                return sb;
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

                return sb;
            }
        }*/


        public String[] TablaParam(string tabla)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute usp_tabla @i_operacion = 'TABLA_PARAM', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    resultado[i] = "0";
                }
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
        }

        private string ArmaRadio(string texto, string valorDato)
        {
            if (valorDato == "S")
            {
                texto = texto.Replace("@SI@", "checked");
                texto = texto.Replace("@NO@", "");
            }
            else
            {
                texto = texto.Replace("@SI@", "");
                texto = texto.Replace("@NO@", "checked");
            }

            return texto;
        }

        private string ArmaCombo(string tabla, string campo, string valorDato, string modo)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string texto = "";
            string linea = "";
            string reemplazo = "";
            valorDato = valorDato.Trim();

            string comando = "execute usp_tabla @i_operacion = 'ARMA_CATALOGO', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_id1", campo, "S");
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");


            SqlDataReader dr;
            //Dato dato = new Dato();
            dr = dato.SpDataReader(comando);

            //System.Data.DataSet DS1 = new System.Data.DataSet();
            //DS1 = dato.SpDataset(comando);

            //BUSCA EL VALOR DEL CAMPO DEL REGISTRO
            //foreach (System.Data.DataRow renglon in DS1.Tables["tabla"].Rows)
            //{
            while (dr.Read()) //LEE CADA FILA
            {
                // ARMA LA LINEA DEL COMBO
                //linea = renglon["valor"].ToString();
                linea = dr[0].ToString();

                // SI TIENE UN VALOR SELECCIONADO BUSCA LA 
                if (valorDato != null)
                {
                    if (valorDato != "")
                    {
                        if (reemplazo == "")
                        {
                            if (linea.Substring(15, valorDato.Length) == valorDato && linea.Substring(15 + valorDato.Length, 2) == "\">")
                            {
                                linea = linea.Replace("<option ", "<option selected ");
                                reemplazo = "S";
                            }
                        }
                    }
                }

                texto = texto + linea;
            }

            // SI TIENE UN VALOR ASIGNADO Y NO SE ENCUETRA EN LA LISTA LE COLOCA UN VALOR GENERICO
            if (valorDato != null && reemplazo == "")
            {
                linea = linea + "<option selected value='" + valorDato + "'>Dato no encontrado o borado</option>";
            }

            //DS1.Tables["tabla"].Clear();
            dr.Dispose();

            return texto;
        }

        public string GrabaRegistro(string tabla, string modo, string titulo, string id1, string id2, string id3, string sql1, string sql2)
        {
            string[] resultado = new string[100];

            try
            {
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute usp_tabla @i_operacion = 'GRABA_REGISTRO', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
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
                Dato dato = new Dato();
                resultado = dato.SpString(comando);

                return resultado[0];
            }

            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public string BorraRegistro(string tabla, string modo, string titulo, string id1, string id2, string id3)
        {
            string[] datosTabla = new string[100];
            //RECUPERA LOS VALORES DE LA LLAMADA
            datosTabla = TablaParam(tabla);

            string[] resultado = new string[100];

            try
            {
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute usp_tabla @i_operacion = 'BORRAR_REGISTRO', @i_usuario = '" + usuario.usuario + "', @i_tabla = '" + tabla + "', @i_id1 = '" + id1 + "'";
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

                Dato dato = new Dato();
                resultado = dato.SpString(comando);

                return resultado[0];
            }

            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public Boolean VerTabla(string tabla, ref System.Web.UI.WebControls.GridView gridView)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute usp_tabla @i_operacion='VER_TABLA', @i_usuario='" + usuario.usuario + "', @i_tabla='" + tabla + "'";

            NegSeguridad negSeguridad = new NegSeguridad();
            negSeguridad.ArmaGrid(comando, ref gridView);
            return true;
        }

        public string GrabaCatalogo(string tabla, string id, string valor, string estado, string obs)
        {
            string[] resultado = new string[100];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_catalogo @i_operacion = 'GRABA_REGISTRO', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
                comando = comando + dato.ArmarParametro("@i_g_tabla", tabla, "N");
                comando = comando + dato.ArmarParametro("@i_g_id", id, "N");
                comando = comando + dato.ArmarParametro("@i_g_valor", valor, "S");
                comando = comando + dato.ArmarParametro("@i_g_estado", estado, "S");
                comando = comando + dato.ArmarParametro("@i_g_obs", obs, "S");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public Boolean TablaCatalogo(string tabla, ref System.Web.UI.WebControls.GridView gridView)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_catalogo @i_operacion = 'SEL_CATALOGO', @i_usuario='" + usuario.usuario + "', @i_tabla='" + tabla + "'";
            NegSeguridad negSeguridad = new NegSeguridad();
            negSeguridad.ArmaGrid(comando, ref gridView);
            return true;
        }

        public StringBuilder ProductoGrid(string modo)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_producto @i_operacion = 'GRILLA PRODUCTOS', @i_modo='" + modo + "', @i_usuario = '" + usuario.usuario + "'";
            SqlDataReader dr;
            Dato dato = new Dato();
            dr = dato.SpDataReader(comando);
            StringBuilder sb = new System.Text.StringBuilder("");

            while (dr.Read()) //LEE CADA FILA
            {
                sb.Append(dr[0].ToString());
            }
            
            sb.Append("</tbody></table></div></form></body></html>");

            return sb;
        }

        public Boolean ProductoLee(string id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_producto @i_operacion='LEE PRODUCTO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prod = " + id;

            string[] resultado = new string[100];
            Dato dato = new Dato();
            resultado = dato.SpString(comando);

            Producto producto = new Producto();

            if (resultado[0] != null)
            {
                producto.prod = resultado[0];
                producto.descripcion = resultado[1];
                producto.nivel = resultado[2];
                producto.padre = resultado[3];
                producto.estado = resultado[4];
                producto.referencia = resultado[5];
                producto.prefijo = resultado[6];
                producto.obs = resultado[7];
                producto.usuario = resultado[8];
                producto.falta = resultado[9];
                producto.fmodif = resultado[10];
                producto.producto_bcra = resultado[11];
                producto.padreDesc = resultado[12];

                System.Web.HttpContext.Current.Session.Add("producto", producto);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("producto", producto);
                return false;
            }

            return true;
        }

        public string ProductoGraba(string operacion)
        {
            string[] resultado = new string[100];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                Producto producto = new Producto();
                producto = (Producto)System.Web.HttpContext.Current.Session["producto"];

                string comando = "execute sp_producto @i_operacion = '" + operacion + "', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_prod", producto.prod, "N");
                comando = comando + dato.ArmarParametro("@i_descripcion", producto.descripcion, "S");
                comando = comando + dato.ArmarParametro("@i_nivel", producto.nivel, "N");
                comando = comando + dato.ArmarParametro("@i_padre", producto.padre, "N");
                comando = comando + dato.ArmarParametro("@i_estado", producto.estado, "S");
                comando = comando + dato.ArmarParametro("@i_referencia", producto.referencia, "S");
                comando = comando + dato.ArmarParametro("@i_prefijo", producto.prefijo, "S");
                comando = comando + dato.ArmarParametro("@i_obs", producto.obs, "S");
                comando = comando + dato.ArmarParametro("@i_producto_bcra", producto.producto_bcra, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public Boolean RelWebCognitoLeer(string id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            Dato dato = new Dato();

            string comando = "execute sp_rel_web_cognito @i_operacion = 'LEER' ";
            comando = comando + dato.ArmarParametro("@i_codigo", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            RelWebCognito relWeb = new RelWebCognito();

            if (resultado[0] != null)
            {
                relWeb.codigo = resultado[0];
                relWeb.producto_web = resultado[1];
                relWeb.tipo_operacion = resultado[2];
                relWeb.producto = resultado[3];
                relWeb.subproducto = resultado[4];
                relWeb.motivo = resultado[5];
                relWeb.referencia = resultado[6];
                relWeb.mail_interno = resultado[7];
                relWeb.mail_cliente = resultado[8];
                relWeb.usuario = resultado[9];
                relWeb.falta = resultado[10];
                relWeb.fmodif = resultado[11];
                relWeb.sistema_externo = resultado[12];
                relWeb.sucursal_asignacion = resultado[13];

                System.Web.HttpContext.Current.Session.Add("relWeb", relWeb);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("relWeb", relWeb);
                return false;
            }

            return true;
        }

        public String RelWebCognitoGrabar()
        {
            string[] resultado = new string[100];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                RelWebCognito relWeb = new RelWebCognito();
                relWeb = (RelWebCognito)System.Web.HttpContext.Current.Session["relWeb"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_rel_web_cognito @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_codigo", relWeb.codigo, "N"); 
                comando = comando + dato.ArmarParametro("@i_producto_web", relWeb.producto_web, "S");
                comando = comando + dato.ArmarParametro("@i_tipo_operacion", relWeb.tipo_operacion, "N");
                comando = comando + dato.ArmarParametro("@i_producto", relWeb.producto, "N");
                comando = comando + dato.ArmarParametro("@i_subproducto", relWeb.subproducto, "N");
                comando = comando + dato.ArmarParametro("@i_motivo", relWeb.motivo, "N");
                comando = comando + dato.ArmarParametro("@i_referencia", relWeb.referencia, "S");
                comando = comando + dato.ArmarParametro("@i_mail_interno", relWeb.mail_interno, "S");
                comando = comando + dato.ArmarParametro("@i_mail_cliente", relWeb.mail_cliente, "S");
                comando = comando + dato.ArmarParametro("@i_sistema_externo", relWeb.sistema_externo, "S");
                comando = comando + dato.ArmarParametro("@i_sucursal_asignacion", relWeb.sucursal_asignacion, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String RelWebCognitoBorrar()
        {
            string[] resultado = new string[100];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                RelWebCognito relWeb = new RelWebCognito();
                relWeb = (RelWebCognito)System.Web.HttpContext.Current.Session["relWeb"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_rel_web_cognito @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_codigo", relWeb.codigo, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public void RegistraLog(string tipo, string accion, string sp, string comando, string error)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comandoSP = "execute sp_log @i_operacion = 'REGISTRA'";
            comandoSP = comandoSP + ", @i_tipo   = '" + tipo + "'";
            comandoSP = comandoSP + ", @i_accion = '" + accion + "'";
            comandoSP = comandoSP + ", @i_sp     = '" + sp + "'";
            comandoSP = comandoSP + ", @i_comando = '" + comando + "'";
            comandoSP = comandoSP + ", @i_error = '" + error + "'";
            comandoSP = comandoSP + ", @i_usuario = '" + usuario.usuario + "'";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comandoSP);
        }

        public String[] ObtieneUltimaEjecucion()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comandoSP = "execute sp_log @i_operacion = 'AJUSTES TARJ.DEBITO'";
            
            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comandoSP);

            return resultado;
        }

        public Boolean MailProductoEstadoLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_mail_producto_estado @i_operacion = 'LEER' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                
                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("mailProductoEstado", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("mailProductoEstado", resultado);
                return false;
            }
        }

        public String MailProductoEstadoGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["mailProductoEstado"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_mail_producto_estado @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_tipo_operacion", datos[1], "N");
                comando = comando + dato.ArmarParametro("@i_producto", datos[2], "N");
                comando = comando + dato.ArmarParametro("@i_subproducto", datos[3], "N");
                comando = comando + dato.ArmarParametro("@i_motivo", datos[4], "N");
                comando = comando + dato.ArmarParametro("@i_estado", datos[5], "S");
                comando = comando + dato.ArmarParametro("@i_tipo", datos[6], "S");
                comando = comando + dato.ArmarParametro("@i_mail", datos[7], "N");
                comando = comando + dato.ArmarParametro("@i_suc_origen", datos[8], "N");
                comando = comando + dato.ArmarParametro("@i_encuesta", datos[9], "N");
                comando = comando + dato.ArmarParametro("@i_cuestionario", datos[10], "N");
                comando = comando + dato.ArmarParametro("@i_copia_mail", datos[11], "S");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String MailProductoEstadoBorrar(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["mailProductoEstado"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_mail_producto_estado @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public Boolean AlarmaEstadoLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_alarma_estado @i_operacion = 'LEER' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("alarmaEstado", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("alarmaEstado", resultado);
                return false;
            }
        }

        public String AlarmaEstadoGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["alarmaEstado"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_alarma_estado @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_tipo_oper", datos[1], "N");
                comando = comando + dato.ArmarParametro("@i_producto", datos[2], "N");
                comando = comando + dato.ArmarParametro("@i_subproducto", datos[3], "N");
                comando = comando + dato.ArmarParametro("@i_motivo", datos[4], "N");
                comando = comando + dato.ArmarParametro("@i_estado", datos[5], "S");
                comando = comando + dato.ArmarParametro("@i_tipo_control", datos[6], "S");
                comando = comando + dato.ArmarParametro("@i_dias_aviso", datos[7], "N");
                comando = comando + dato.ArmarParametro("@i_dias_alarma", datos[8], "N");
                comando = comando + dato.ArmarParametro("@i_obs", datos[9], "S");
                comando = comando + dato.ArmarParametro("@i_dias_escala1", datos[13], "N");
                comando = comando + dato.ArmarParametro("@i_dias_escala2", datos[14], "N");
                comando = comando + dato.ArmarParametro("@i_dias_escala3", datos[15], "N");

                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String AlarmaEstadoBorrar(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["alarmaEstado"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_alarma_estado @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public Boolean EncuestaCuestionarioLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_encuesta @i_operacion = 'LEER_CUESTIONARIO' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("cuestionario", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("cuestionario", resultado);
                return false;
            }
        }

        public String EncuestaCuestionarioGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["cuestionario"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_encuesta @i_operacion = 'GRABAR_CUESTIONARIO', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_tipo", datos[1], "S");
                comando = comando + dato.ArmarParametro("@i_descripcion", datos[2], "S");
                comando = comando + dato.ArmarParametro("@i_estado", datos[3], "S");
                comando = comando + dato.ArmarParametro("@i_fecha_inicio", datos[4], "S");
                comando = comando + dato.ArmarParametro("@i_fecha_fin", datos[5], "S");
                comando = comando + dato.ArmarParametro("@i_sucursal", datos[6], "N");
                comando = comando + dato.ArmarParametro("@i_obs", datos[7], "S");

                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public System.Data.DataSet EncuestaPreguntaLeer(string id)
        {
            try
            {
                Dato dato = new Dato();
                string comando = "execute sp_encuesta @i_operacion = 'LEER_PREGUNTA' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                System.Data.DataSet ds = new System.Data.DataSet();
                ds = dato.SpQuery(comando);

                return ds;
            }
            catch
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                Dato dato = new Dato();
                ds = dato.SpQuery("execute sp_formulario @i_operacion = 'VACIO'");

                return ds;
            }
        }

        public String[] EncuestaPreguntaLeer1(string id)
        {
            try
            {
                Dato dato = new Dato();
                string comando = "execute sp_encuesta @i_operacion = 'LEER_UNA_PREGUNTA' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                string[] resultado = new string[100];
                resultado = dato.SpString(comando);

                return resultado;
            }
            catch
            {
                string[] resultado = new string[100];
                return resultado;
            }
        }

        public String EncuestaPreguntaGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["pregunta"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_encuesta @i_operacion = 'GRABAR_PREGUNTA', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_Cuestionario_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_id", datos[1], "N");
                comando = comando + dato.ArmarParametro("@i_orden", datos[2], "N");
                comando = comando + dato.ArmarParametro("@i_pregunta", datos[3], "S");
                comando = comando + dato.ArmarParametro("@i_tipoRespuesta", datos[4], "S");
                comando = comando + dato.ArmarParametro("@i_respuestaObligatoria", datos[5], "N");
                comando = comando + dato.ArmarParametro("@i_estado", datos[6], "S");

                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public Boolean MailPlantillaHtmlLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_mail_plantilla_html @i_operacion = 'LEER' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("mail_plantilla_html", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("mail_plantilla_html", resultado);
                return false;
            }
        }

        public String MailPlantillaHtmlGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["mail_plantilla_html"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_mail_plantilla_html @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_descripcion", datos[1], "S");
                comando = comando + dato.ArmarParametro("@i_plantilla", datos[2], "S");

                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String MailPlantillaHtmlBorrar(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["mail_plantilla_html"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_mail_plantilla_html @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public bool GrabaLogAuditoria(string tabla, string accion, string registro, string prefijo, string id)
        {
            string[] resultado = new string[100];
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            
            string comando = "execute sp_log_audit @i_operacion = 'REGISTRA', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_accion", accion, "S");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "S");

            resultado = dato.SpString(comando);

            if (resultado[0] == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LeeLogAuditoriaError(string tabla, string fecha, string filtro, ref System.Web.UI.WebControls.GridView gridView)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_log_audit @i_operacion = 'LEER ERROR', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_fecha", fecha, "S");
            comando = comando + dato.ArmarParametro("@i_registro", filtro, "S");

            NegSeguridad negSeguridad = new NegSeguridad();
            negSeguridad.ArmaGrid(comando, ref gridView);
            return;
        }

        public bool ActualizarReintentoAdintar(string tabla, string accion, string registro, string prefijo, string id)
        {
            string[] resultado = new string[100];
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_log_audit @i_operacion = 'REINTENTO', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_accion", accion, "S");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "S");

            resultado = dato.SpString(comando);

            if (resultado[0] == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Dado un Codigo Postal, devuelve los codigo de locallidad y provincia valios para adintar
        /// </summary>
        /// <param name="codPostal"></param>
        /// <returns></returns>
        public String[] CodPostalAdintar(string codPostal)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'COD_POSTAL_ADINTAR', @i_usuario = '" + usuario.usuario + "' ";
            comando = comando + dato.ArmarParametro("@i_motivo", codPostal, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                return resultado;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    resultado[i] = "0";
                }
                return resultado;
            }
        }

        /// <summary>
        /// Valida CUIL/CUIT Valido
        /// </summary>
        /// <param name="Cuit"></param>
        /// <returns></returns>
        public Boolean ValidaCUIT(string Cuit)
        {
            decimal mk_suma;
            Cuit = Cuit.Replace("-", "");
            if (Cuit.Length != 11)
            {
                return false;
            }
            else
            {
                if (Cuit.Substring(0, 2) == "00" && Cuit.Substring(10, 1) == "0")
                {
                    return true;
                }

                mk_suma = 0;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(0, 1)) * 5;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(1, 1)) * 4;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(2, 1)) * 3;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(3, 1)) * 2;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(4, 1)) * 7;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(5, 1)) * 6;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(6, 1)) * 5;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(7, 1)) * 4;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(8, 1)) * 3;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(9, 1)) * 2;
                mk_suma += System.Convert.ToDecimal(Cuit.Substring(10, 1)) * 1;
            }
            if (Math.Round(mk_suma / 11, 0) == (mk_suma / 11))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String[] CategoriaCobis(string tipo_cuenta, string tipo_cuenta_desc)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_tabla @i_operacion = 'CATEGORIA_COBIS', @i_usuario = '" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_tipo_cta", tipo_cuenta, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_cta_desc", tipo_cuenta_desc, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                return resultado;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    resultado[i] = "0";
                }
                return resultado;
            }
        }

        public String[] TablaAsociacion(string tabla)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla_dinamica @i_operacion = 'TABLA ASOCIACION'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    resultado[i] = "";
                }
                //System.Web.HttpContext.Current.Session.Add("tabla_clave", resultado);
                return resultado;
            }
        }

        public Boolean VerAsociados(string tabla, string modo, string valor, ref System.Web.UI.WebControls.GridView gridView)
        {
            Dato dato = new Dato();
            string comando = "execute sp_tabla_dinamica @i_operacion = '" + modo + "'";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_valor", valor, "S");

            NegSeguridad negSeguridad = new NegSeguridad();
            negSeguridad.ArmaGrid(comando, ref gridView);
            return true;
        }

        public Boolean Asociacion(string tabla, string modo, string valor, string columna)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_tabla_dinamica @i_operacion = '" + modo + "'";
            comando = comando + dato.ArmarParametro("@i_tabla", tabla, "S");
            comando = comando + dato.ArmarParametro("@i_valor", valor, "S");
            comando = comando + dato.ArmarParametro("@i_columna", columna, "S");
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");

            dato.SpString(comando);

            return true;
        }

        public Boolean EstadoInicialLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_estado_inicial @i_operacion = 'LEER' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("estado_inicial", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("estado_inicial", resultado);
                return false;
            }
        }

        public String EstadoInicialGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["estado_inicial"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_estado_inicial @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_tipo_operacion", datos[1], "N");
                comando = comando + dato.ArmarParametro("@i_producto", datos[2], "N");
                comando = comando + dato.ArmarParametro("@i_subproducto", datos[3], "N");
                comando = comando + dato.ArmarParametro("@i_motivo", datos[4], "N");
                comando = comando + dato.ArmarParametro("@i_estado", datos[5], "S");
                comando = comando + dato.ArmarParametro("@i_obs", datos[6], "S");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String EstadoInicialBorrar(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["estado_inicial"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_estado_inicial @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public Boolean SucursalDerivacionLeer(string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string[] resultado = new string[100];

            try
            {
                string comando = "execute sp_sucursal_derivacion @i_operacion = 'LEER' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");

                resultado = dato.SpString(comando);

                System.Web.HttpContext.Current.Session.Add("sucursal_derivacion", resultado);
                return true;
            }
            catch
            {
                resultado[0] = "";
                System.Web.HttpContext.Current.Session.Add("sucursal_derivacion", resultado);
                return false;
            }
        }

        public String SucursalDerivacionGrabar()
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["sucursal_derivacion"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_sucursal_derivacion @i_operacion = 'GRABAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", datos[0], "N");
                comando = comando + dato.ArmarParametro("@i_tipo_operacion", datos[1], "N");
                comando = comando + dato.ArmarParametro("@i_producto", datos[2], "N");
                comando = comando + dato.ArmarParametro("@i_subproducto", datos[3], "N");
                comando = comando + dato.ArmarParametro("@i_motivo", datos[4], "N");
                comando = comando + dato.ArmarParametro("@i_tipo", datos[5], "S");
                comando = comando + dato.ArmarParametro("@i_sucursal", datos[6], "S");
                comando = comando + dato.ArmarParametro("@i_obs", datos[7], "S");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL GRABAR";
                return resultado[0];
            }
        }

        public String SucursalDerivacion(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["sucursal_derivacion"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_sucursal_derivacion @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public String SucursalDerivacionBorrar(string id)
        {
            string[] resultado = new string[100];
            string[] datos = (String[])System.Web.HttpContext.Current.Session["sucursal_derivacion"];
            try
            {
                Dato dato = new Dato();
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

                string comando = "execute sp_sucursal_derivacion @i_operacion = 'BORRAR', @i_usuario = '" + usuario.usuario + "' ";
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
                resultado = dato.SpString(comando);

                return resultado[0];
            }
            catch
            {
                resultado[0] = "OCURRIO UN ERROR AL BORRAR EL REGISTRO";
                return resultado[0];
            }
        }

        public System.Data.DataSet SucursalDerivacion(string prefijo, string id)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_tabla @i_operacion = 'DERIVACION'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            return ds;
        }

    }
}