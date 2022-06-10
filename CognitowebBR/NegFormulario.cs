using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using CognitowebBE;
using CognitowebDA;

namespace CognitowebBR
{
    public class NegFormulario
    {
        private Usuario usuario = new Usuario();
        BaseDatos baseDatos = new BaseDatos();
        Dato dato = new Dato();
        NegFunciones negFunciones = new NegFunciones();
        NegTabla negTabla = new NegTabla();
        Lista lista = new Lista();

        public NegFormulario()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string TextoSolicitud()
        {
            try
            {
                Formulario formulario = new Formulario();
                formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];

                if (formulario.tipoOperacionDesc.IndexOf("RECLAMO") >= 0)
                {
                    return "Acepta Respuesta por Mail";
                }
                else
                {
                    return "Solicita Contacto";
                }
            }
            catch (Exception)
            {
                return "Acepta Respuesta por Mail";
            }
        }

        public System.Data.DataSet Pendiente(string tipoPend, string sucursal, Boolean otrosUsuarios, Boolean domicilio, Boolean importeDevolucion, string estadoDevolucion, Boolean entesExternos)
        {
            string comando = PendienteArmaComando(tipoPend, sucursal, otrosUsuarios, domicilio, importeDevolucion, estadoDevolucion, entesExternos);
            comando = comando + ", @i_destino='grilla' ";
            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            if (ds.Tables.Count == 0)
            {
                comando = "execute sp_pendiente @i_operacion = 'SALIDA'";
                comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
                ds = dato.SpQuery(comando);
            }

            return ds;
        }

        public System.Data.DataTable PendienteReporte(string tipoPend, string sucursal, Boolean otrosUsuarios, Boolean domicilio, Boolean importeDevolucion, string estadoDevolucion, Boolean entesExternos)
        {
            string comando = PendienteArmaComando(tipoPend, sucursal, otrosUsuarios, domicilio, importeDevolucion, estadoDevolucion, entesExternos);
            comando = comando + ", @i_destino='reporte' ";
            return ArmaDataTable(comando);
        }

        public String PendienteArmaComando(string tipoPend, string sucursal, Boolean otrosUsuarios, Boolean domicilio, Boolean importeDevolucion, string estadoDevolucion, Boolean entesExternos)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_pendiente @i_operacion = 'SELECT'";
            if (sucursal != String.Empty & tipoPend != String.Empty)
            {
                if (tipoPend == "0" | tipoPend == "1")
                {
                    comando = comando + ", @i_tipo = " + tipoPend + ", @i_sucursal = " + sucursal;
                }
                if (tipoPend == "2")
                {
                    comando = comando + ", @i_tipo = " + tipoPend;
                }
                if (tipoPend == "3")
                {
                    comando = comando + ", @i_tipo = " + tipoPend + ", @i_grupo_afinidad = '" + sucursal + "'";
                }
            }

            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            
            if (otrosUsuarios == true)
            {
                comando = comando + ", @i_otros_usuarios ='S'";
            }
            else
            {
                comando = comando + ", @i_otros_usuarios ='N'";
            }

            if (domicilio == true)
            {
                comando = comando + ", @i_domicilio ='S'";
            }
            else
            {
                comando = comando + ", @i_domicilio ='N'";
            }

            if (entesExternos == true)
            {
                comando = comando + ", @i_ver_externos ='S'";
            }
            else
            {
                comando = comando + ", @i_ver_externos ='N'";
            }
            
            if (importeDevolucion == true)
            {
                comando = comando + ", @i_importe_devolucion ='S', @i_estado_devolucion = '" + estadoDevolucion + "'";
            }
            else
            {
                comando = comando + ", @i_importe_devolucion ='N'";
            }

            return comando;
        }

        public Boolean ConsultaNovedadGrilla(string prefijo, string id, ref System.Web.UI.WebControls.GridView grid)
        {
            string comando = ArmaComandoNovedad(prefijo, id);
            comando = comando + ", @i_destino='grilla' ";
            ArmaGrid(comando, ref grid);
            return true ;
        }

        public System.Data.DataTable ConsultaNovedadReporte(string prefijo, string id)
        {
            string comando = ArmaComandoNovedad(prefijo,id);
            comando = comando + ", @i_destino='reporte' ";
            return ArmaDataTable(comando);
        }

        public string ArmaComandoNovedad(string prefijo, string id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_consulta @i_operacion = 'CONSULTA'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            
            // Consulta Base Normal o Historica
            Pagina pagina = (Pagina)(System.Web.HttpContext.Current.Session["pagina"]);
            comando = comando + ", @i_historico='" + pagina.historico + "'";

            if (prefijo != String.Empty && id != String.Empty)
            {
                comando = comando + ", @i_prefijo = '" + prefijo + "', @i_id = " + id;
            }

            return comando;
        }

        public Boolean ConsultaDocumentoGrilla(string documento, ref System.Web.UI.WebControls.GridView grid)
        {
            string comando = ArmaComandoDocumento(documento);
            comando = comando + ", @i_destino='grilla' ";
            ArmaGrid(comando, ref grid);
            return true;
        }

        public System.Data.DataTable ConsultaDocumentoReporte(string documento) //ref System.Web.UI.WebControls.GridView grid)
        {
            string comando = ArmaComandoDocumento(documento);
            comando = comando + ", @i_destino='reporte' ";
            return ArmaDataTable(comando);
        }

        public string ArmaComandoDocumento(string documento)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_consulta @i_operacion = 'CONSULTA'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";

            // Consulta Base Normal o Historica
            Pagina pagina = (Pagina)(System.Web.HttpContext.Current.Session["pagina"]);
            comando = comando + ", @i_historico='" + pagina.historico + "'";

            if (documento != String.Empty)
            {
                comando = comando + ", @i_documento = '" + documento + "' ";
            }
            return comando;
        }

        //public Boolean ConsultaFiltroGrilla(ref System.Web.UI.WebControls.GridView grid, String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente)
        //{
        //    string comando = ArmaComandoFiltro(origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente);
        //    comando = comando + ", @i_destino='grilla' ";
        //    ArmaGrid(comando, ref grid);
        //    return true;
        //}

        public System.Data.DataTable ConsultaFiltroGrilla(String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente)
        {
            string comando = ArmaComandoFiltro(origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente);
            comando = comando + ", @i_destino='grilla' ";
            return ArmaDataTable(comando);
        }

        public System.Data.DataTable ConsultaFiltroReporte(String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente)
        {
            string comando = ArmaComandoFiltro(origenActual, tipo, sucursal, fechaIngreso, fecDesde, fecHasta, apellido, estado, marcaHora, horaDesde, horaHasta, TipoOperacion, producto, subProducto, motivo, marcaCliente, cliente);
            comando = comando + ", @i_destino='reporte' ";
            return ArmaDataTable(comando);
        }

        public string ArmaComandoFiltro(String origenActual, String tipo, String sucursal, String fechaIngreso, String fecDesde, String fecHasta, String apellido, String estado, Boolean marcaHora, String horaDesde, String horaHasta, String TipoOperacion, String producto, String subProducto, String motivo, Boolean marcaCliente, String cliente)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_consulta @i_operacion = 'CONSULTA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_origen_actual", origenActual, "S");
            comando = comando + dato.ArmarParametro("@i_tipo", tipo, "N");
            comando = comando + dato.ArmarParametro("@i_sucursal", sucursal, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_ingreso", fechaIngreso, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_desde", fecDesde, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_hasta", fecHasta, "S");
            
            // Consulta Base Normal o Historica
            Pagina pagina = (Pagina)(System.Web.HttpContext.Current.Session["pagina"]);
            comando = comando + dato.ArmarParametro("@i_historico", pagina.historico, "S");

            if (apellido.Trim() != "")
            {
                comando = comando + dato.ArmarParametro("@i_apellido", apellido, "S");
            }
            if (estado.Trim() != "")
            {
                comando = comando + dato.ArmarParametro("@i_estado", estado, "S");
            }
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
                    System.Web.UI.WebControls.GridView grid1= new System.Web.UI.WebControls.GridView();
                    grid = grid1;
                }
            }
            catch
            {

            }
        }

        public System.Data.DataTable ArmaDataTable(string comando)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = dato.spTable(comando);
            return dt;
        }

        public System.Data.DataSet ReclamoAnterior(string NroDoc, string tipoDoc)
        {

            return ReclamoAnterior(NroDoc, "", tipoDoc);
        }

        public System.Data.DataSet ReclamoAnterior(string NroDoc, string subProducto, string tipoDoc)
        {
            string comando = "execute sp_formulario @i_operacion = 'RECLAMO_ANTERIOR', @i_s_nro_doc = '" + NroDoc + "'";
            comando = comando + dato.ArmarParametro("@i_n_subproducto", subProducto, "N");
            comando = comando + dato.ArmarParametro("@i_n_tipo_doc", tipoDoc, "N");
            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public String ReclamoAnteriorHoy(string NroDoc, string subProducto)
        {
            string fechaHoy = negFunciones.FechaHoy();
            string comando = "execute sp_formulario @i_operacion = 'RECLAMO_ANTERIOR', @i_s_nro_doc = '" + NroDoc + "'";
            comando = comando + dato.ArmarParametro("@i_n_subproducto", subProducto, "N");
            comando = comando + dato.ArmarParametro("@i_s_hoy", fechaHoy, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String ObtieneFechaHoy()
        {
            string comando = "execute sp_accesos @i_operacion = 'FECHA_HOY'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return  resultado[0];
        }

        public String PermiteFechaValor()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            if (VerificaAcceso(1008) == true)
            {
                usuario.permiteFechaValor = "S";
            }
            else
            {
                usuario.permiteFechaValor = "N";
            }
            System.Web.HttpContext.Current.Session.Add("usuario", usuario);

            return usuario.permiteFechaValor;
        }

        public Boolean VerificaAcceso(int acceso)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_accesos @i_operacion = 'ACCESO', @i_acceso = " + acceso + ", @i_usuario = '" + usuario.usuario + "'";

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

        public void LimpiarCliente()
        {
            Cliente cliente = new Cliente();
            lista.GrabaCliente(cliente);
        }

        public void LimpiarFormulario()
        {
            Formulario formulario = new Formulario();
            lista.GrabaFormulario(formulario);
            Empresa empresa = new Empresa();
            System.Web.HttpContext.Current.Session.Add("empresa", empresa);
            Domicilio domicilio = new Domicilio();
            System.Web.HttpContext.Current.Session.Add("domicilio", domicilio);
            Denuncia denuncia = new Denuncia();
            System.Web.HttpContext.Current.Session.Add("denuncia", denuncia);
            Circuito circuito = new Circuito();
            System.Web.HttpContext.Current.Session.Add("circuito", circuito);
            TarjetaAdicional tarjetaAdicional = new TarjetaAdicional();
            System.Web.HttpContext.Current.Session.Add("tarjetaAdicional", tarjetaAdicional);
            Judicial judicial = new Judicial();
            System.Web.HttpContext.Current.Session.Add("judicial", judicial);
            Mail mail = new Mail();
            System.Web.HttpContext.Current.Session.Add("mail", mail);
            SolicitudChequera solicitudChequera = new SolicitudChequera();
            System.Web.HttpContext.Current.Session.Add("solicitudChequera", solicitudChequera);
            TarjetaCanje tarjetaCanje = new TarjetaCanje();
            System.Web.HttpContext.Current.Session.Add("tarjetaCanje", tarjetaCanje);
            TarjetaPromo tarjetaPromo = new TarjetaPromo();
            System.Web.HttpContext.Current.Session.Add("tarjetaPromo", tarjetaPromo);
        }

        public void LimpiarPagina()
        {
            Pagina pagina = new Pagina();
            System.Web.HttpContext.Current.Session.Add("pagina", pagina);
        }

        public void CargarSesion()
        {
            Pagina pagina = new Pagina();
            pagina = (Pagina)(System.Web.HttpContext.Current.Session["pagina"]);
            pagina.cargarSesion = true;
            System.Web.HttpContext.Current.Session.Add("pagina", pagina);
        }

        public Boolean VerificaCargarSesion() 
        {
            Pagina pagina = new Pagina();
            pagina = (Pagina)(System.Web.HttpContext.Current.Session["pagina"]);
            if (pagina != null)
            {
                //VERIFICA SI TIENE QUE CARGAR O NO LOS DATOS DE LA SESION AL FORMULARIO
                if (pagina.cargarSesion == true)
                {
                    pagina.cargarSesion = false;
                    System.Web.HttpContext.Current.Session.Add("pagina", pagina);
                    return true;
                }
            }

            return false;
        }

        public Boolean LeerNovedad(string prefijo, string id, string historico)
        {
            Boolean resultado = false;

            if (historico != null)
            {
                historico = historico.Trim();
                if (historico != "S")
                {
                    historico = "N";
                }
            }

            resultado = LeerNovedadCliente(prefijo, id, historico);
            resultado = LeerNovedadFormulario(prefijo, id, historico);
            
            return resultado ;
        }

        public Boolean LeerNovedadCliente(string prefijo, string id, string historico)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='CLIENTE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id ;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Cliente cliente = new Cliente();
            
            if (resultado[0] != null)
            {
                cliente.tipoDoc = resultado[2];
                cliente.nroDoc = resultado[3];
                cliente.apellido = resultado[4];
                cliente.nombre = resultado[5];
                cliente.cuit = resultado[6];
                cliente.dirCalle = resultado[7];
                cliente.dirNro = resultado[8];
                cliente.pisoDto = resultado[9];
                cliente.cpostal = resultado[10];
                cliente.localidad = resultado[11];
                cliente.provincia = resultado[12];
                cliente.telefono = resultado[13];
                cliente.email = resultado[14];
                cliente.sucursal = resultado[15];
                cliente.medio = resultado[16];
                cliente.nroCuenta = resultado[17];
                cliente.modifDatos = resultado[18]; // Modificación de EMail
                cliente.usuario = resultado[19];
                cliente.falta = resultado[20];
                cliente.fmodif = resultado[21];
                cliente.esCliente = resultado[22];
                cliente.fecNac = resultado[23];
                cliente.celular = resultado[24];
                cliente.telAlternativo = resultado[25];
                cliente.modifTelPart = resultado[26];
                cliente.modifCelular = resultado[27];
                cliente.modifTelAlt = resultado[28];
                cliente.empleado = resultado[29];
                cliente.modifDomicilio = resultado[30];
                cliente.tipoDocDesc = resultado[31];

                lista.GrabaCliente(cliente);
            }
            else
            {
                lista.GrabaCliente(cliente);
                return false;
            }

            return true;
        }

        public Boolean LeerNovedadFormulario(string prefijo, string id, string historico)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='FORMULARIO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Formulario formulario = new Formulario();

            if (resultado[0] != null)
            {
                formulario.prefijo = prefijo;
                formulario.id = id;
                formulario.fecIngreso = resultado[2];
                formulario.sucursal = resultado[3];
                formulario.usuarioDeriv = resultado[4];
                formulario.estado = resultado[5];
                formulario.tipoOperacion = resultado[6];
                formulario.producto = resultado[7];
                formulario.subProducto = resultado[8];
                formulario.motivo = resultado[9];
                formulario.origenCaptura = resultado[10];
                formulario.fecOper = resultado[11];
                formulario.cliTipoProd = resultado[12];
                formulario.cliNroProd = resultado[13];
                formulario.cliCondicion = resultado[14];
                formulario.cliRespuesta = resultado[15];
                formulario.cliSucursal = resultado[16];
                formulario.tarjMarca = resultado[17];
                formulario.tarjNro = resultado[18];
                formulario.tarjCuenta = resultado[19];
                formulario.tarjFecCierre = resultado[20];
                formulario.tarjFecVto = resultado[21];
                formulario.tarjExcepcion = resultado[22];
                formulario.tarjCupon = resultado[23];
                formulario.tarjImporte = resultado[24];
                formulario.tarjRef = resultado[25];
                formulario.tarjAdicNombre = resultado[26];
                formulario.tarjAdicDoc = resultado[27];
                formulario.tarjCalle = resultado[28];
                formulario.tarjDirNro = resultado[29];
                formulario.tarjPisoDto = resultado[30];
                formulario.tarjCpostal = resultado[31];
                formulario.tarjLocalidad = resultado[32];
                formulario.tarjProvincia = resultado[33];
                formulario.tarjTelefono = resultado[34];
                formulario.formaPago = resultado[35];
                formulario.ctaSucursal = resultado[36];
                formulario.tipoCuenta = resultado[37];
                formulario.nroCuenta = resultado[38];
                formulario.preimpreso = resultado[39];
                formulario.importe = resultado[40];
                formulario.medio = resultado[41];
                formulario.banco = resultado[42];
                formulario.cbu = resultado[43];
                formulario.nroOper = resultado[44];
                formulario.fecDeposito = resultado[45];
                formulario.chequePlaza = resultado[46];
                formulario.chequeNro = resultado[47];
                formulario.solicitaContacto = resultado[48];
                formulario.incoveniente = resultado[49];
                formulario.referencia = resultado[50];
                formulario.usuario = resultado[51];
                formulario.falta = resultado[52];
                formulario.fmodif = resultado[53];
                formulario.resumenMail = resultado[54];
                formulario.infoComercial = resultado[55];
                formulario.sucursalDesc = resultado[56];
                formulario.usuarioNombre = resultado[57];
                formulario.tipoOperacionDesc = resultado[58];
                formulario.productoDesc  = resultado[59];
                formulario.subProductoDesc = resultado[60];
                formulario.motivoDesc = resultado[61];
                formulario.estadoDesc = resultado[62];
                formulario.adjunto = resultado[63];

                lista.GrabaFormulario(formulario);
            }
            else
            {
                lista.GrabaFormulario(formulario);
                return false;
            }

            return true;
        }

        public Boolean LeerNovedadCircuito(string prefijo, string id, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_formulario_lee @i_operacion = 'CIRCUITO'";

            if (prefijo != String.Empty && id != String.Empty)
            {
                comando = comando + ", @i_prefijo = '" + prefijo + "', @i_id = " + id;
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);
            string valor;
            int i = 0; 

            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                valor = row[6].ToString();
                ds.Tables[0].Rows[i][6] = negFunciones.TextoAcento(valor);
                i++;
            }

            grid.DataSource = ds;

            foreach (System.Web.UI.WebControls.GridViewRow fila in grid.Rows)
            {
                fila.Cells[6].Text = negFunciones.TextoAcento(fila.Cells[6].Text);
            }

            grid.DataBind();

            return true;
        }

        public System.Data.DataSet LeerNovedadCircuito(string prefijo, string id, string historico)
        {

            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='CIRCUITO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public void LeerDomicilio(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null) 
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='DOMICILIO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerTarjetaAdicional(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='TARJ_ADIC', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerDenuncia(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='DENUNCIA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerReclamo(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='RECLAMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerTarjetaPromo(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='TARJ_PROMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void BuscarPrestamo(string operacion, string documento, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_importa_prestamo @i_s_operacion='BUSCAR', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_operacion_nro", operacion, "N"); 
            comando = comando + dato.ArmarParametro("@i_documento_nro", documento, "N"); 

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public System.Data.DataSet ReporteCanje(string fecDesde, string fecHasta, string estado)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_consulta @i_operacion='CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_fecha_desde = '" + fecDesde + "'";
            comando = comando + ", @i_fecha_hasta = '" + fecHasta + "'";
            comando = comando + ", @i_estado = '"      + estado   + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public Boolean LeerCanje(string prefijo, string id, string historico)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            CognitowebBE.TarjetaCanje tarjetaCanje = new CognitowebBE.TarjetaCanje();

            if (resultado[0] != null)
            {
                tarjetaCanje.registro = resultado[0];
                tarjetaCanje.tipoDoc = resultado[1];
                tarjetaCanje.nroDoc = resultado[2];
                tarjetaCanje.apellido = resultado[3];
                tarjetaCanje.nombre = resultado[4];
                tarjetaCanje.condicion = resultado[5];
                tarjetaCanje.puntosAcum = resultado[7];
                tarjetaCanje.destino = resultado[8];
                tarjetaCanje.fecIda = resultado[9];
                tarjetaCanje.fecVuelta = resultado[10];

                System.Web.HttpContext.Current.Session.Add("tarjetaCanje", tarjetaCanje);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("tarjetaCanje", tarjetaCanje);
                return false;
            }

            return true;
        }

        public Boolean LeerCanje(string prefijo, string id, string registro, string historico)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_registro = " + registro;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            CognitowebBE.TarjetaCanje tarjetaCanje = new CognitowebBE.TarjetaCanje();

            if (resultado[0] != null)
            {
                tarjetaCanje.registro = resultado[0];
                tarjetaCanje.tipoDoc = resultado[1];
                tarjetaCanje.nroDoc = resultado[2];
                tarjetaCanje.apellido = resultado[3];
                tarjetaCanje.nombre = resultado[4];
                tarjetaCanje.condicion = resultado[5];
                tarjetaCanje.puntosAcum = resultado[7];
                tarjetaCanje.destino = resultado[8];
                tarjetaCanje.fecIda = resultado[9];
                tarjetaCanje.fecVuelta = resultado[10];

                System.Web.HttpContext.Current.Session.Add("tarjetaCanje", tarjetaCanje);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("tarjetaCanje", tarjetaCanje);
                return false;
            }

            return true;
        }

        public void LeerCanje(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='CANJE_GRILLA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public Boolean LeerSolicitudChequera(string prefijo, string id, string registro, string historico)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='SOLICITUD', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";
            if (registro != "")
            {
                comando = comando + ", @i_registro = " + registro;
            }

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            CognitowebBE.SolicitudChequera solicitudChequera = new CognitowebBE.SolicitudChequera();

            if (resultado[0] != null)
            {
                solicitudChequera.prefijo = prefijo;
                solicitudChequera.id = id;
                solicitudChequera.registro = resultado[0];
                solicitudChequera.tipoChequera = resultado[1];
                solicitudChequera.cantidadCheque = resultado[3];
                solicitudChequera.cuit = resultado[5];
                solicitudChequera.rsocial = resultado[6];
                

                System.Web.HttpContext.Current.Session.Add("solicitudChequera", solicitudChequera);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("solicitudChequera", solicitudChequera);
                return false;
            }

            return true;
        }

        public void LeerSolicitudChequera(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='SOLICITUD_GRILLA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerTotalTarjetaPromo(string prefijo, string id, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='TOTAL_TARJ_PROMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public Boolean LeerEmpresa(string prefijo, string id, string historico)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='FORM_EMPRESA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Empresa e = new Empresa();

            if (resultado[0] != null)
            {
                e.prefijo = prefijo;
                e.id = id;
                e.rsocial = resultado[2];
                e.tipo = resultado[3];
                e.cuit = resultado[4];
                e.responsable = resultado[5];
                e.factAnual = resultado[6];
                e.sucRef = resultado[7];
                e.direccion = resultado[8];
                e.cpostal = resultado[9];
                e.localidad = resultado[10];
                e.provincia = resultado[11];
                e.telefono = resultado[12];
                e.email = resultado[13];
                e.lineaCred = resultado[14];
                e.prodServ = resultado[15];
                e.solicitaContacto = resultado[16];
                e.incoveniente = resultado[17];
                e.usuario = resultado[18];
                e.falta = resultado[19];
                e.fmodif = resultado[20];
                
                System.Web.HttpContext.Current.Session.Add("empresa", e);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("empresa", e);
                return false;
            }

            return true;
        }

        public Boolean LeerJudicial(string prefijo, string id, string historico)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='JUDICIAL', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + ", @i_historico = '" + historico + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            CognitowebBE.Judicial judicial = new Judicial();

            if (resultado[0] != null)
            {
                judicial.prefijo = resultado[0];
                judicial.id = resultado[1];
                //judicial.producto = resultado[2];
                judicial.concepto = resultado[2];
                judicial.prefijo_original = resultado[3];
                judicial.id_original = resultado[4];
                judicial.monto_original = resultado[5];
                judicial.monto_final = resultado[6];
                judicial.monto_costas = resultado[7];
                judicial.fec_finalizacion = resultado[8];
                judicial.estado_judicial = resultado[9];
                judicial.nro_expediente = resultado[10];
                judicial.juridisccion = resultado[11];
                judicial.localidad = resultado[12];

                System.Web.HttpContext.Current.Session.Add("judicial", judicial);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("judicial", judicial);
                return false;
            }

            return true;
        }
        
        public String GrabarNovedad(Cliente cliente, Formulario formulario)
        {
            Empresa empresa = new Empresa();
            String resultado = GrabarNovedadCompleta(cliente, formulario, empresa);
            lista.GrabaCliente(cliente);
            lista.GrabaFormulario(formulario);
            return resultado;
        }

        public String GrabarNovedad(Cliente cliente, Formulario formulario, Empresa empresa)
        {
            String resultado = GrabarNovedadCompleta(cliente, formulario, empresa);
            lista.GrabaCliente(cliente);
            lista.GrabaFormulario(formulario);
            return resultado;
        }

        private String GrabarNovedadCompleta(Cliente cliente, Formulario formulario, Empresa empresa)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo"                  ,formulario.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id"                       ,formulario.id, "N");
            comando = comando + dato.ArmarParametro("@i_cli_tipo_doc"             ,cliente.tipoDoc, "S");
            comando = comando + dato.ArmarParametro("@i_cli_nro_doc"              ,cliente.nroDoc, "S");
            comando = comando + dato.ArmarParametro("@i_cli_apellido"             ,cliente.apellido, "S");
            comando = comando + dato.ArmarParametro("@i_cli_nombre"               ,cliente.nombre, "S");
            comando = comando + dato.ArmarParametro("@i_cli_cuit"                 ,cliente.cuit, "S");
            comando = comando + dato.ArmarParametro("@i_cli_dir_calle"            ,cliente.dirCalle, "S");
            comando = comando + dato.ArmarParametro("@i_cli_dir_nro"              ,cliente.dirNro, "S"); 
            comando = comando + dato.ArmarParametro("@i_cli_piso_dto"             ,cliente.pisoDto, "S");
            comando = comando + dato.ArmarParametro("@i_cli_cpostal"              ,cliente.cpostal, "S");
            comando = comando + dato.ArmarParametro("@i_cli_localidad"            ,cliente.localidad, "S");
            comando = comando + dato.ArmarParametro("@i_cli_provincia"            ,cliente.provincia, "S");
            comando = comando + dato.ArmarParametro("@i_cli_telefono"             ,cliente.telefono, "S");
            comando = comando + dato.ArmarParametro("@i_cli_celular"              ,cliente.celular, "S");
            comando = comando + dato.ArmarParametro("@i_cli_tel_alternativo"      ,cliente.telAlternativo, "S");
            comando = comando + dato.ArmarParametro("@i_cli_email"                ,cliente.email, "S");
            comando = comando + dato.ArmarParametro("@i_cli_sucursal"             ,cliente.sucursal, "S");
            comando = comando + dato.ArmarParametro("@i_cli_medio"                ,cliente.medio, "S");
            comando = comando + dato.ArmarParametro("@i_cli_nro_cuenta"           ,cliente.nroCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_cli_modif_datos"          ,cliente.modifDatos, "S");
            comando = comando + dato.ArmarParametro("@i_cli_modif_tel"            ,cliente.modifTelPart, "S");
            comando = comando + dato.ArmarParametro("@i_cli_modif_cel"            ,cliente.modifCelular, "S");
            comando = comando + dato.ArmarParametro("@i_cli_modif_tel_alt"        ,cliente.modifTelAlt, "S");
            comando = comando + dato.ArmarParametro("@i_cli_modif_domicilio"      , cliente.modifDomicilio, "S");
            comando = comando + dato.ArmarParametro("@i_cli_es_cliente"           ,cliente.esCliente, "S");
            comando = comando + dato.ArmarParametro("@i_cli_empleado"             ,cliente.empleado, "S");
            comando = comando + dato.ArmarParametro("@i_cli_fec_nac"              ,cliente.fecNac, "S");
            comando = comando + dato.ArmarParametro("@i_cli_actualiza_datos"      ,cliente.actualizaDatos, "S");

            comando = comando + dato.ArmarParametro("@i_form_fec_ingreso"         ,formulario.fecIngreso, "S");          
            comando = comando + dato.ArmarParametro("@i_form_sucursal"            ,formulario.sucursal, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_usuario_deriv"       ,formulario.usuarioDeriv, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_estado"              ,formulario.estado, "S");
            comando = comando + dato.ArmarParametro("@i_form_tipo_operacion"      ,formulario.tipoOperacion, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_producto"            ,formulario.producto, "S");
            comando = comando + dato.ArmarParametro("@i_form_subproducto"         ,formulario.subProducto, "S");
            comando = comando + dato.ArmarParametro("@i_form_motivo"              ,formulario.motivo, "S");
            comando = comando + dato.ArmarParametro("@i_form_origen_captura"      ,formulario.origenCaptura, "S");
            comando = comando + dato.ArmarParametro("@i_form_fec_oper"            ,formulario.fecOper, "S");
            comando = comando + dato.ArmarParametro("@i_form_cli_tipo_prod"       ,formulario.cliTipoProd, "S");
            comando = comando + dato.ArmarParametro("@i_form_cli_nro_prod"        ,formulario.cliNroProd, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_cli_condicion"       ,formulario.cliCondicion, "S");
            comando = comando + dato.ArmarParametro("@i_form_cli_respuesta"       ,formulario.cliRespuesta, "S");
            comando = comando + dato.ArmarParametro("@i_form_cli_sucursal"        ,formulario.cliSucursal, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_marca"          ,formulario.tarjMarca, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro"            ,formulario.tarjNro, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_cuenta"         ,formulario.tarjCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_fec_cierre"     ,formulario.tarjFecCierre, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_fec_vto"        ,formulario.tarjFecVto, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_tarj_excepcion"      ,formulario.tarjExcepcion, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_cupon"          ,formulario.tarjCupon, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_importe"        ,formulario.tarjImporte, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_ref"            ,formulario.tarjRef, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_adic_nombre"    ,formulario.tarjAdicNombre, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_tarj_adic_doc"       ,formulario.tarjAdicDoc, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_tarj_calle"          ,formulario.tarjCalle, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_dir_nro"        ,formulario.tarjDirNro, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_piso_dto"       ,formulario.tarjPisoDto, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_tarj_cpostal"        ,formulario.tarjCpostal, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_localidad"      ,formulario.tarjLocalidad, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_provincia"      ,formulario.tarjProvincia, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_telefono"       ,formulario.tarjTelefono, "S");
            comando = comando + dato.ArmarParametro("@i_form_forma_pago"          ,formulario.formaPago, "S");
            comando = comando + dato.ArmarParametro("@i_form_cta_sucursal"        ,formulario.ctaSucursal, "S");
            comando = comando + dato.ArmarParametro("@i_form_tipo_cuenta"         ,formulario.tipoCuenta, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta"          ,formulario.nroCuenta, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_preimpreso"          ,formulario.preimpreso, "S");
            comando = comando + dato.ArmarParametro("@i_form_importe"             ,formulario.importe, "S");
            comando = comando + dato.ArmarParametro("@i_form_medio"               ,formulario.medio, "S");
            comando = comando + dato.ArmarParametro("@i_form_banco"               ,formulario.banco, "S");
            comando = comando + dato.ArmarParametro("@i_form_cbu"                 ,formulario.cbu, "S");
            comando = comando + dato.ArmarParametro("@i_form_nro_oper"            ,formulario.nroOper, "S");
            comando = comando + dato.ArmarParametro("@i_form_fec_deposito"        ,formulario.fecDeposito, "S");
            comando = comando + dato.ArmarParametro("@i_form_cheque_plaza"        ,formulario.chequePlaza, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_cheque_nro"          ,formulario.chequeNro, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_solicita_contacto"   ,formulario.solicitaContacto, "S");   
            comando = comando + dato.ArmarParametro("@i_form_incoveniente"        ,formulario.incoveniente, "S"); 
            comando = comando + dato.ArmarParametro("@i_form_referencia"          ,formulario.referencia, "S");
            comando = comando + dato.ArmarParametro("@i_form_resumen_mail"        ,formulario.resumenMail, "S");
            comando = comando + dato.ArmarParametro("@i_form_info_comercial"      ,formulario.infoComercial, "S");

            if (empresa != null)
            {
                if (empresa.rsocial != null)
                {
                comando = comando + dato.ArmarParametro("@i_rsocial",       empresa.rsocial, "S");
                comando = comando + dato.ArmarParametro("@i_tipo",          empresa.tipo, "S");
                comando = comando + dato.ArmarParametro("@i_cuit",          empresa.cuit, "S");
                comando = comando + dato.ArmarParametro("@i_responsable",   empresa.responsable, "S");
                comando = comando + dato.ArmarParametro("@i_fact_anual",    empresa.factAnual, "S");
                comando = comando + dato.ArmarParametro("@i_suc_ref",       empresa.sucRef, "S");
                comando = comando + dato.ArmarParametro("@i_direccion",     empresa.direccion, "S");
                comando = comando + dato.ArmarParametro("@i_cpostal",       empresa.cpostal, "S");
                comando = comando + dato.ArmarParametro("@i_localidad",     empresa.localidad, "S");
                comando = comando + dato.ArmarParametro("@i_provincia",     empresa.provincia, "S");
                comando = comando + dato.ArmarParametro("@i_telefono",      empresa.telefono, "S");
                comando = comando + dato.ArmarParametro("@i_email",         empresa.email, "S");
                comando = comando + dato.ArmarParametro("@i_linea_cred",    empresa.lineaCred, "S");
                comando = comando + dato.ArmarParametro("@i_prod_serv",     empresa.prodServ, "S");
                comando = comando + dato.ArmarParametro("@i_solicita_contacto", empresa.solicitaContacto, "S");
                comando = comando + dato.ArmarParametro("@i_incoveniente",  empresa.incoveniente, "S");
                }
            }

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] == "OK")
            {
                formulario.prefijo = resultado[1];
                formulario.id = resultado[2];
            }
            else
            {
                baseDatos = (BaseDatos)(System.Web.HttpContext.Current.Session["baseDatos"]);
                baseDatos.mensaje = resultado[0];
            }
            return resultado[0];
        }
         
        public Boolean NovedadExiste()
        {
            Formulario formulario = new Formulario();
            formulario = (Formulario )System.Web.HttpContext.Current.Session["Formulario"];
            if (formulario.prefijo == null || formulario.prefijo == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }
        
        public String ObtienePaginaFormulario()
        {
            Formulario formulario = new Formulario();
            formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];
            string comando = "";

            if (formulario != null)
            {
                comando = "execute sp_tabla @i_operacion = 'PAGINA' ";
                if (formulario.tipoOperacion != null)
                {
                    comando = comando + ", @i_tipo_operacion = " + formulario.tipoOperacion;
                }
                if (formulario.producto != null)
                {
                    comando = comando + ", @i_producto = " + formulario.producto;
                }
                if (formulario.subProducto != null)
                {
                    comando = comando + ", @i_subproducto = " + formulario.subProducto;
                }
                if (formulario.motivo != null)
                {
                    comando = comando + ", @i_motivo = " + formulario.motivo;
                }
                if (formulario.referencia != null && formulario.referencia != "")
                {
                    comando = comando + ", @i_referencia = " + formulario.referencia;
                }

                if (comando == "")
                {
                    return "formIngresoCab.aspx";
                }
            }
            else
            {
                return "formIngresoCab.aspx";
            }

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public Boolean EsNumerico(string value)
        {
            try
            {
                value = value.Trim();
                long i = Convert.ToInt64(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public String GrabarCircuito(Circuito circuito, Formulario formulario)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_CIRCUITO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo",           formulario.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id",                formulario.id, "N");
            comando = comando + dato.ArmarParametro("@i_form_sucursal",     circuito.sucursal, "S");
            comando = comando + dato.ArmarParametro("@i_deriv",             circuito.deriv, "S");
            comando = comando + dato.ArmarParametro("@i_usuario_deriv",     circuito.usuarioDeriv, "S");
            comando = comando + dato.ArmarParametro("@i_form_estado",       circuito.estado, "S");
            comando = comando + dato.ArmarParametro("@i_form_incoveniente", circuito.obs, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] == "OK")
            {
                formulario.prefijo = resultado[1];
                formulario.id = resultado[2];
            }

            LeerNovedadFormulario(formulario.prefijo, formulario.id, "N");

            return resultado[0];
        }

        public String GrabarDomicilio(Domicilio domicilio)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_DOMICILIO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", domicilio.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", domicilio.id, "S");
            comando = comando + dato.ArmarParametro("@i_form_cta_sucursal", domicilio.ctaSuc, "S");
            comando = comando + dato.ArmarParametro("@i_form_tipo_cuenta", domicilio.ctaTipo, "S");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", domicilio.ctaNumero, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_calle", domicilio.dirCalle, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_dir_nro", domicilio.dirDirNro, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_piso_dto", domicilio.dirPisoDto, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_cpostal", domicilio.dirCpostal, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_localidad", domicilio.dirLocalidad, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_provincia", domicilio.dirProvincia, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_telefono", domicilio.dirTelefono, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarTarjetaAdicional(TarjetaAdicional tarjAdic)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_TARJETA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", tarjAdic.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", tarjAdic.id, "N");
            comando = comando + dato.ArmarParametro("@i_form_cta_sucursal", tarjAdic.ctaSucursal, "S");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", tarjAdic.nroCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_marca", tarjAdic.tarjMarca, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro", tarjAdic.tarjNro , "S");
            comando = comando + dato.ArmarParametro("@i_tipo", tarjAdic.tipoDoc, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_adic_doc", tarjAdic.nroDoc, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_adic_nombre", tarjAdic.nombre, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_ref", tarjAdic.solicitud, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarTarjetaAdicional(String prefijo, String id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_TARJETA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarDenuncia(Denuncia denuncia)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_DENUNCIA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", denuncia.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", denuncia.id, "N");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", denuncia.tarjCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro", denuncia.tarjNro, "S");

            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarDenuncia(String prefijo, String id, Denuncia denuncia)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_DENUNCIA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", denuncia.tarjCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro", denuncia.tarjNro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarTarjetaPromo(TarjetaPromo tarjetaPromo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_PROMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", tarjetaPromo.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", tarjetaPromo.id, "N");
            comando = comando + dato.ArmarParametro("@i_form_cta_sucursal", tarjetaPromo.ctaSucursal, "S");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", tarjetaPromo.nroCuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_marca", tarjetaPromo.tarjMarca, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro", tarjetaPromo.tarjNro, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_cierre", tarjetaPromo.fechaCierre, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_vencimiento", tarjetaPromo.fechaVencimiento, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_operacion", tarjetaPromo.fechaOperacion, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_importe", tarjetaPromo.importeOperacion, "S");
            comando = comando + dato.ArmarParametro("@i_rsocial", tarjetaPromo.comercio, "S");
            comando = comando + dato.ArmarParametro("@i_nro_comercio", tarjetaPromo.nroComercio, "S");
            comando = comando + dato.ArmarParametro("@i_tipo", tarjetaPromo.rubro, "S");
            comando = comando + dato.ArmarParametro("@i_form_importe", tarjetaPromo.montoDevolucion, "S");
            comando = comando + dato.ArmarParametro("@i_form_estado", tarjetaPromo.estado, "S");
            comando = comando + dato.ArmarParametro("@i_registro", tarjetaPromo.registro, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_sistema", tarjetaPromo.sistema.Trim(), "N");
            comando = comando + dato.ArmarParametro("@i_barrido", tarjetaPromo.barrido, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarTarjetaCanje(TarjetaCanje tarjetaCanje)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", tarjetaCanje.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", tarjetaCanje.id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", tarjetaCanje.registro, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_doc", tarjetaCanje.tipoDoc, "N");
            comando = comando + dato.ArmarParametro("@i_nro_doc", tarjetaCanje.nroDoc, "S");
            comando = comando + dato.ArmarParametro("@i_apellido", tarjetaCanje.apellido, "S");
            comando = comando + dato.ArmarParametro("@i_nombre", tarjetaCanje.nombre, "S");
            comando = comando + dato.ArmarParametro("@i_condicion", tarjetaCanje.condicion, "N");
            comando = comando + dato.ArmarParametro("@i_puntos_acum", tarjetaCanje.puntosAcum, "N");
            comando = comando + dato.ArmarParametro("@i_destino", tarjetaCanje.destino, "N");
            comando = comando + dato.ArmarParametro("@i_fec_ida", tarjetaCanje.fecIda, "S");
            comando = comando + dato.ArmarParametro("@i_fec_vuelta", tarjetaCanje.fecVuelta, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarSolicitudChequera(SolicitudChequera solicitudChequera)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_SOLICITUD', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", solicitudChequera.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", solicitudChequera.id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", solicitudChequera.registro, "S");
            comando = comando + dato.ArmarParametro("@i_condicion", solicitudChequera.tipoChequera, "N");
            comando = comando + dato.ArmarParametro("@i_destino", solicitudChequera.cantidadCheque, "N");
            comando = comando + dato.ArmarParametro("@i_cuit", solicitudChequera.cuit, "S");
            comando = comando + dato.ArmarParametro("@i_rsocial", solicitudChequera.rsocial, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarJudicial(Judicial judicial)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            
            Formulario formulario = new Formulario();
            formulario = (Formulario)System.Web.HttpContext.Current.Session["formulario"];
            judicial.prefijo = formulario.prefijo;
            judicial.id = formulario.id;

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_JUDICIAL', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", judicial.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", judicial.id, "N");
            //comando = comando + dato.ArmarParametro("@i_form_producto", judicial.producto, "S");
            comando = comando + dato.ArmarParametro("@i_form_motivo", judicial.concepto, "S");
            comando = comando + dato.ArmarParametro("@i_nro_doc", judicial.prefijo_original, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_doc", judicial.id_original, "N");
            comando = comando + dato.ArmarParametro("@i_form_importe", judicial.monto_original, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_importe", judicial.monto_final, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_ref", judicial.monto_costas, "S");
            comando = comando + dato.ArmarParametro("@i_fecha_cierre", judicial.fec_finalizacion, "S");
            comando = comando + dato.ArmarParametro("@i_condicion", judicial.estado_judicial, "N");
            comando = comando + dato.ArmarParametro("@i_form_nro_oper", judicial.nro_expediente, "S");
            comando = comando + dato.ArmarParametro("@i_form_medio", judicial.juridisccion, "S");
            comando = comando + dato.ArmarParametro("@i_localidad", judicial.localidad, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String GrabarArchivoAdjunto(string prefijo, string id, string archivo, string descripcion, string base64)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_ADJUNTO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "S");
            comando = comando + dato.ArmarParametro("@i_nombre_archivo", archivo, "S");
            comando = comando + dato.ArmarParametro("@i_descripcion", descripcion, "S");
            comando = comando + dato.ArmarParametro("@i_form_adjunto", base64, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarTarjetaPromo(String prefijo, String id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_PROMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarTarjetaCanje(String prefijo, String id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarSolicitudChequera(String prefijo, String id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_SOLICITUD', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String PermiteAgregarTarjetaPromo(String prefijo, String id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_lee @i_operacion='AGREGA_TARJ_PROMO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String PermiteAgregarCanje(String prefijo, String id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_lee @i_operacion='AGREGA_CANJE', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String UsuarioNombre(String usuario)
        {
            string comando = "execute sp_tabla @i_operacion = 'NOMBRE_USUARIO', @i_usuario = '" + usuario + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return  resultado[0];

        }

        public System.Data.DataTable DomicilioDatos(string prefijo, string id)
        {
            string comando = "execute sp_formulario_lee @i_operacion = 'DOMICILIO_IMPRIME', @i_prefijo = '" + prefijo + "', @i_id = " + id + ", @i_usuario='" + usuario.usuario + "'";
            return ArmaDataTable(comando);
        }

        public Boolean InterfazAdintar(string operacion, string modo, ref System.Web.UI.WebControls.GridView grid)
        {
            string interfazModo = (string)System.Web.HttpContext.Current.Session["interfazModo"];
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string estado = "";
            
            if (operacion == "CONSULTA")
            {
                estado = "M";
            }
            if (operacion == "CONSULTA_IDA")
            {
                estado = "X";
            }
            if (operacion == "CONSULTA DEBITO")
            {
                estado = modo;
            }

            string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "', @i_estado = '" + estado + "', @i_modo = '" + modo + "'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_interfaz_modo='" + interfazModo + "'";

            ArmaGrid(comando, ref grid);
            return true;
        }

        public String InterfazAdintar(string operacion, string s_accion, string prefijo, string id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string interfazModo = (string)System.Web.HttpContext.Current.Session["interfazModo"];
            
            string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "N");
            comando = comando + dato.ArmarParametro("@i_accion", s_accion, "S");
            comando = comando + ", @i_interfaz_modo='" + interfazModo + "'";
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public Boolean InterfazAdintar(string operacion, string modo, string fecha, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string interfazModo = (string)System.Web.HttpContext.Current.Session["interfazModo"];

            string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "'";
            comando = comando + ", @i_fecha='" + fecha + "'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_interfaz_modo='" + interfazModo + "'";
            ArmaGrid(comando, ref grid);
            return true;
        }

        public String[] InterfazAdintar(string operacion, string modo, string fecha, string prefijo, string id, ref System.Web.UI.WebControls.GridView grid)
        {
            try
            {
                usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
                string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "'";
                comando = comando + ", @i_fecha='" + fecha + "'";
                comando = comando + ", @i_usuario='" + usuario.usuario + "'";
                comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
                comando = comando + dato.ArmarParametro("@i_id", id, "N");
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
        
        public String MarcarRegistroInterfazAdintar(string pAccion, string pPrefijo, string pId, string pResultado)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_interfaz_adintar_online @i_operacion = 'MARCAR'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", pPrefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", pId, "N");
            comando = comando + dato.ArmarParametro("@i_resultado", pResultado, "S");
            comando = comando + dato.ArmarParametro("@i_accion", pAccion, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public Boolean InterfazAdintar(string operacion, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "' " ;
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";

            ArmaGrid(comando, ref grid);
            return true;
        }

        public string RegistroDebito(string operacion, string s_accion, string prefijo, string id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_actualiza_adintar @i_operacion = '" + operacion + "'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "N");
            comando = comando + dato.ArmarParametro("@i_accion", s_accion, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public void InterfaceLeePath()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_actualiza_adintar @i_operacion = 'OBTIENE PATH'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("PathGrabacion", resultado[0]);
            System.Web.HttpContext.Current.Session.Add("PathLectura", resultado[1]);
            System.Web.HttpContext.Current.Session.Add("NombreArchivoSalida", resultado[2]);
            System.Web.HttpContext.Current.Session.Add("NombreArchivoAceptado", resultado[3]);
            System.Web.HttpContext.Current.Session.Add("NombreArchivoRechazado", resultado[4]);
            System.Web.HttpContext.Current.Session.Add("PathProcesados", resultado[5]);
        }

        public void InterfaceGrabaPath(string grabacion, string lectura)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_actualiza_adintar @i_operacion = 'GRABA PATH'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_path1", grabacion, "S");
            comando = comando + dato.ArmarParametro("@i_path2", lectura, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("PathGrabacion", resultado[0]);
            System.Web.HttpContext.Current.Session.Add("PathLectura", resultado[1]);
        }

        public void InterfazGrabaDebito(string modo, string registro, string archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_actualiza_adintar @i_operacion = 'GRABA DEBITO'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");
            comando = comando + dato.ArmarParametro("@i_mensaje", registro, "S");
            comando = comando + dato.ArmarParametro("@i_path1", archivo, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);
        }

        public Boolean LeerAlarmas(string tipoBusqueda, string valorBusqueda, string tipoControl, string tipoGrupo, string tipoVista, string tipoNivel, string tipoOperacion, ref System.Web.UI.WebControls.GridView grid)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_alarma @i_operacion='CONSULTA_ALARMA', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_tipo_busqueda = " + tipoBusqueda; 
            comando = comando + ", @i_valor_busqueda = '" + valorBusqueda + "'";
            comando = comando + ", @i_tipo_control = '" + tipoControl + "'";
            comando = comando + ", @i_tipo_grupo = '" + tipoGrupo + "'";
            comando = comando + ", @i_tipo_vista = '" + tipoVista + "'";
            comando = comando + ", @i_tipo_nivel = '" + tipoNivel + "'";
            comando = comando + dato.ArmarParametro("@i_tipo_operacion", tipoOperacion, "S");
            ArmaGrid(comando, ref grid);
            return true;
        }

        public System.Data.DataSet AlarmaTipoOperacion(string tipoControl)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_alarma @i_operacion = 'TIPO_OPERACION', @i_usuario = '" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_tipo_control", tipoControl, "S");

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            return ds;
        }

        public void ConsultaSQL(string mensaje, ref System.Web.UI.WebControls.GridView grid)
        {
            string comando = "execute sp_actualiza_adintar @i_operacion = 'VER', @i_mensaje = '" + mensaje + "'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            ArmaGrid(comando, ref grid);
        }

        /// <summary>
        /// Busca si se debe enviar mail Interno o Externo a la Novedad
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="formulario"></param>
        /// <returns></returns>
        public Mail BuscaParametrosMail(Cliente cliente, Formulario formulario)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_mail @i_operacion = 'BUSCA PARAMETRO MAIL'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", formulario.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", formulario.id, "N");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Mail m = new Mail();

            m.enviaMail = resultado[0];
            m.tipo = resultado[1];
            m.tipoMail = resultado[2];
            m.asunto = resultado[3];
            m.mensaje = resultado[4];
            m.para = resultado[5];
            m.circuito = resultado[6];
            m.html = resultado[7];
            m.encuesta = resultado[8];
            m.cuestionario = resultado[9];
            m.copiaMail = resultado[10];

            return m;
        }

        /// <summary>
        /// Rutina para prueba de mail
        /// </summary>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        public Mail DatosMail(string plantilla)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_mail @i_operacion = 'DATOS MAIL'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_id", plantilla, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Mail m = new Mail();

            m.enviaMail = resultado[0];
            m.tipo = resultado[1];
            m.tipoMail = resultado[2];
            m.asunto = resultado[3];
            m.mensaje = resultado[4];
            m.para = resultado[5];
            m.circuito = resultado[6];
            m.html = resultado[7];
            m.encuesta = resultado[8];
            m.cuestionario = resultado[9];

            return m;
        }

        /// <summary>
        /// Devuelve los Datos de una Plantilla HTML
        /// </summary>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        public string DatosPlantillaHTML(string plantilla)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_mail @i_operacion = 'DATOS PLANTILLA HTML'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_id", plantilla, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public Mail MailAutogestion(Cliente cliente, Formulario formulario)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_mail @i_operacion = 'MAIL AUTOGESTION'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", formulario.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", formulario.id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Mail m = new Mail();

            m.enviaMail = resultado[0];
            m.tipo = resultado[1];
            m.tipoMail = resultado[2];
            m.asunto = resultado[3];
            m.mensaje = resultado[4];
            m.para = resultado[5];
            m.circuito = resultado[6];
            m.html = resultado[7];
            m.encuesta = resultado[8];
            m.cuestionario = resultado[9];

            return m;
        }

        public Mail MailAjusteDebito(Cliente cliente, Formulario formulario)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_mail @i_operacion = 'MAIL AUTOGESTION'";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", formulario.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", formulario.id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            Mail m = new Mail();

            m.enviaMail = resultado[0];
            m.tipo = resultado[1];
            m.tipoMail = resultado[2];
            m.asunto = resultado[3];
            m.mensaje = resultado[4];
            m.para = resultado[5];
            m.circuito = resultado[6];
            m.html = resultado[7];
            m.encuesta = resultado[8];
            m.cuestionario = resultado[9];

            return m;
        }

        public System.Data.DataTable DTConsultaInformeBCRA(string[] datos)
        {
            string nombreSp = negTabla.Parametro("NOMBRE SP PARA EJECUTAR INFORME BCRA");

            //dato[1]-Tipo Reporte. dato[2]-fecha desde. dato[3]-fecha hasta 
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute " + nombreSp  + " @i_operacion = '" + datos[1] + "' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_desde", datos[2], "S");
            comando = comando + dato.ArmarParametro("@i_hasta", datos[3], "S");
            comando = comando + dato.ArmarParametro("@i_reporte", datos[4], "N");
            comando = comando + dato.ArmarParametro("@i_concepto_cod", datos[6], "N");
            comando = comando + dato.ArmarParametro("@i_primer_informe", datos[7], "S");

            return ArmaDataTable(comando);
        }

        public string ConsultaInformeBCRA(string[] datos)
        {
            string nombreSp = negTabla.Parametro("NOMBRE SP PARA EJECUTAR INFORME BCRA");
            //dato[1]-Tipo Reporte. dato[2]-fecha desde. dato[3]-fecha hasta . dato[5]-tema
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute " + nombreSp + " @i_operacion = '" + datos[1] + "' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_desde", datos[2], "S");
            comando = comando + dato.ArmarParametro("@i_hasta", datos[3], "S");
            comando = comando + dato.ArmarParametro("@i_tema_cod", datos[5], "N");
            comando = comando + dato.ArmarParametro("@i_concepto_cod", datos[6], "N");
            comando = comando + dato.ArmarParametro("@i_primer_informe", datos[7], "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[1] != "")
            {
                System.Web.HttpContext.Current.Session.Add("resultado", resultado);
            }

            return resultado[0];
        }

        public System.Data.DataTable ConsultaInformeBCRAOtros(string[] datos)
        {
            string nombreSp = negTabla.Parametro("NOMBRE SP PARA EJECUTAR INFORME BCRA");
            //dato[1]-Tipo Reporte. dato[2]-fecha desde. dato[3]-fecha hasta . dato[5]-tema
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute " + nombreSp + " @i_operacion = '" + datos[1] + "' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_desde", datos[2], "S");
            comando = comando + dato.ArmarParametro("@i_hasta", datos[3], "S");
            comando = comando + dato.ArmarParametro("@i_tema_cod", datos[5], "N");
            comando = comando + dato.ArmarParametro("@i_concepto_cod", datos[6], "N");
            comando = comando + dato.ArmarParametro("@i_primer_informe", datos[7], "S");
            return ArmaDataTable(comando);
        }

        public System.Data.DataTable CargaComboBCRA()
        {
            string nombreSp = negTabla.Parametro("NOMBRE SP PARA EJECUTAR INFORME BCRA");
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute " + nombreSp + " @i_operacion = 'CARGA_COMBO' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            return ArmaDataTable(comando);
        }

        public string CargaArchivoTXT(string fecha, string operacion, string texto, string contador, string archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_importa_archivo @i_s_operacion = 'A' ";
            //comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_s_fecha", fecha, "S");
            comando = comando + dato.ArmarParametro("@i_s_texto", texto, "S");
            comando = comando + dato.ArmarParametro("@i_n_registro", contador, "N");
            comando = comando + dato.ArmarParametro("@i_s_archivo", archivo, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("resultado", resultado);

            return resultado[0];
        }

        public string CargaArchivoReclamoTarjeta(string fecha, string marca, string texto, string contador, string archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_importa_reclamo_tarjeta @i_s_operacion = 'PROCESA' ";
            comando = comando + dato.ArmarParametro("@i_s_marca", marca, "S"); 
            comando = comando + dato.ArmarParametro("@i_s_texto", texto, "S");
            comando = comando + dato.ArmarParametro("@i_n_registro", contador, "N");
            comando = comando + dato.ArmarParametro("@i_s_archivo", archivo, "S");
            comando = comando + dato.ArmarParametro("@i_s_fecha", fecha, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("resultado", resultado);

            return resultado[0];
        }

        public string CargaArchivoPrestamo(string fecha, string texto, string contador, string archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_importa_prestamo @i_s_operacion = 'PROCESA' ";
            comando = comando + dato.ArmarParametro("@i_s_texto", texto, "S");
            comando = comando + dato.ArmarParametro("@i_n_registro", contador, "N");
            comando = comando + dato.ArmarParametro("@i_s_archivo", archivo, "S");
            comando = comando + dato.ArmarParametro("@i_s_fecha", fecha, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("resultado", resultado);

            return resultado[0];
        }

        public string VerificaArchivoPrestamo(string operacion, string archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_importa_prestamo @i_s_operacion = '" + operacion + "' ";
            comando = comando + dato.ArmarParametro("@i_s_archivo", archivo, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            System.Web.HttpContext.Current.Session.Add("resultado", resultado);

            return resultado[0];
        }

        public String ConfirmarDatosCliente(string documento)
        {
            //Valida la última registración del Usuario y devuelve si debe o no solicitar confirmar datos personales
            string comando = "execute sp_consulta @i_operacion = 'CONFIRMAR_DATOS' ";
            comando = comando + ", @i_documento = '" + documento + "'";
                
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public string AutogestionEnviarMail()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_autogestion @i_s_operacion = 'ENVIAR_MAIL' ";
            comando = comando + ", @i_s_usuario='" + usuario.usuario + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[0] != null)
            {
                System.Web.HttpContext.Current.Session.Add("autogestionEnviarMail", resultado);
            }

            return resultado[4];  //0-No tiene que enviar   1-tiene que enviar
        }

        public string AjusteDebitoEnviarMail()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];
            string comando = "execute sp_actualiza_adintar @i_operacion = 'ENVIAR_MAIL' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            if (resultado[1] != null)
            {
                System.Web.HttpContext.Current.Session.Add("ajusteDebitoEnviarMail", resultado);
                return resultado[1];
            }
            else
            {
                return "0";
            }
            
        }

        public string Recategoriza(string[] parametro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_formulario_recategoriza @i_operacion = 'CAMBIO' ";
            comando = comando + ", @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", parametro[0], "S");
            comando = comando + dato.ArmarParametro("@i_id", parametro[1], "N");
            //comando = comando + dato.ArmarParametro("@i_estado", parametro[2], "S");
            comando = comando + dato.ArmarParametro("@i_tipo_operacion", parametro[3], "N");
            comando = comando + dato.ArmarParametro("@i_producto", parametro[4], "N");
            comando = comando + dato.ArmarParametro("@i_subproducto", parametro[5], "N");
            comando = comando + dato.ArmarParametro("@i_motivo", parametro[6], "N");
            comando = comando + dato.ArmarParametro("@i_obs", parametro[7], "S");
            comando = comando + dato.ArmarParametro("@i_cambia_estado", parametro[8], "S");
            comando = comando + dato.ArmarParametro("@i_cambia_categoria", parametro[9], "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BuscaDuplicadoPromocion(string documento, string monto, string fecha, string prefijo, string id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario @i_operacion='BUSCA_DUPLICADO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_s_nro_doc", documento, "S");
            comando = comando + dato.ArmarParametro("@i_n_importe", monto, "N");
            comando = comando + dato.ArmarParametro("@i_s_fecha", fecha, "S");
            comando = comando + dato.ArmarParametro("@i_s_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_n_id", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BuscaDuplicadoComision(string documento, string monto, string fecha, string prefijo, string id)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario @i_operacion='BUSCA_DUPLICADO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_s_nro_doc", documento, "S");
            comando = comando + dato.ArmarParametro("@i_n_importe", monto, "N");
            comando = comando + dato.ArmarParametro("@i_s_fecha", fecha, "S");
            comando = comando + dato.ArmarParametro("@i_s_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_n_id", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        /// <summary>
        /// Proceso que toma todos los Reclamos por desconocimiento de Cupon 
        /// que pasado el tope de dias siguen en estado 1 - PENDIENTE-INFO. CUPON
        /// y los pasa Finalizado Rechazado
        /// </summary>
        public void ProcesarRechazoAutomatico()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_procesa_desc_cupon @i_s_operacion = 'PROCESO DE RECHAZO'";
            comando = comando + ", @i_s_usuario='" + usuario.usuario + "'";

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);
        }

        /// <summary>
        /// Obtiene los reclamos procesados a Finalizado Rechazado
        /// para enviar Mail al Cliente de Notificacion
        /// </summary>
        /// <returns>Devuelve datatable con Reclamos a Enviar Mail al Cliente</returns>
        public System.Data.DataTable EnviarMailRechazoAutomatico()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_procesa_desc_cupon @i_s_operacion = 'ENVIAR MAIL'";
            comando = comando + ", @i_s_usuario='" + usuario.usuario + "'";
            return ArmaDataTable(comando);
        }

        /// <summary>
        /// Marca los Reclamos a los que se envio mail al Cliente con los Rechazos por Desconocimiento de Cupones
        /// </summary>
        /// <param name="pPrefijo">Prefijo del Reclamo</param>
        /// <param name="pId">ID del Reclamo</param>
        /// <returns>OK o ERROR</returns>
        public string MailEnviadoRechazoAutomatico(string pPrefijo, string pId)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_procesa_desc_cupon @i_s_operacion = 'MAIL ENVIADO'";
            comando = comando + ", @i_s_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_s_prefijo='" + pPrefijo + "'";
            comando = comando + ", @i_n_id=" + pId;

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        //public Boolean LeerTarjetaDebito(string prefijo, string id, string historico)
        //{
        //    if (id == "" || id == null)
        //    {
        //        id = "0";
        //    }
        //    usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
        //    string comando = "execute sp_formulario_lee @i_operacion='TARJETA_DEBITO', @i_usuario='" + usuario.usuario + "'";
        //    comando = comando + ", @i_prefijo = '" + prefijo + "'";
        //    comando = comando + ", @i_id = " + id;
        //    comando = comando + ", @i_historico='" + historico + "'";

        //    string[] resultado = new string[100];
        //    resultado = dato.SpString(comando);

        //    CognitowebBE.TarjetaDebito tarjetaDebito = new CognitowebBE.TarjetaDebito();

        //    if (resultado[0] != null)
        //    {
        //        tarjetaDebito.prefijo = prefijo;
        //        tarjetaDebito.id = id;
        //        tarjetaDebito.registro = resultado[0];
        //        tarjetaDebito.nro_tarjeta = resultado[1];
        //        tarjetaDebito.nro_cuenta = resultado[2];
        //        tarjetaDebito.tipo_cuenta = resultado[3];
        //        tarjetaDebito.tipo_cuenta_desc = resultado[4];
        //        tarjetaDebito.calidad = resultado[5];
        //        tarjetaDebito.calidad_desc = resultado[6];

        //        System.Web.HttpContext.Current.Session.Add("tarjetaDebito", tarjetaDebito);
        //    }
        //    else
        //    {
        //        System.Web.HttpContext.Current.Session.Add("tarjetaDebito", tarjetaDebito);
        //        return false;
        //    }

        //    return true;
        //}

        public void LeerTarjetaDebito(string prefijo, string id, string modo, string historico, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='TARJETA_DEBITO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            comando = comando + dato.ArmarParametro("@i_modo", modo, "S");
            comando = comando + dato.ArmarParametro("@i_historico", historico, "S");

            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void LeerArchivoAdjunto(string prefijo, string id, ref System.Web.UI.WebControls.GridView grid)
        {
            if (id == "" || id == null)
            {
                id = "0";
            }
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_formulario_lee @i_operacion='ARCHIVO_ADJUNTO'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + ", @i_prefijo = '" + prefijo + "'";
            comando = comando + ", @i_id = " + id;
            
            System.Data.DataSet ds = new System.Data.DataSet();
            ds = dato.SpQuery(comando);

            grid.DataSource = ds;
            grid.DataBind();
        }

        public string ObtenerArchivoAdjunto(string prefijo, string id, string orden)
        {
            Dato dato = new Dato();
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_formulario_lee @i_operacion = 'OBTENER_ADJUNTO'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_registro", orden, "N");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);
            return resultado[0];
        }

        public String GrabarTarjetaDebito(TarjetaDebito tarjDebito)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='GRABAR_TARJETA_DEBITO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", tarjDebito.prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", tarjDebito.id, "N");
            comando = comando + dato.ArmarParametro("@i_form_nro_cuenta", tarjDebito.nro_cuenta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_nro", tarjDebito.nro_tarjeta, "S");
            comando = comando + dato.ArmarParametro("@i_form_tipo_cuenta", tarjDebito.tipo_cuenta, "S");
            comando = comando + dato.ArmarParametro("@i_tipo", tarjDebito.tipo_cuenta_desc, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_ref", tarjDebito.calidad, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_marca", tarjDebito.calidad_desc, "S");
            comando = comando + dato.ArmarParametro("@i_form_medio", tarjDebito.cargo, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_cupon", tarjDebito.categoria_cobis, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_excepcion", tarjDebito.categoria_cobis_desc, "S");
            comando = comando + dato.ArmarParametro("@i_form_tarj_barrido", tarjDebito.clase, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String BorrarTarjetaDebito(String prefijo, String id, string registro)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_formulario_graba @i_operacion='BORRAR_TARJETA_DEBITO', @i_usuario='" + usuario.usuario + "'";
            comando = comando + dato.ArmarParametro("@i_prefijo", prefijo, "S");
            comando = comando + dato.ArmarParametro("@i_id", id, "N");
            comando = comando + dato.ArmarParametro("@i_registro", registro, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String VerificaHoraGeneracionArchivoReversaPrestamos()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_genera_archivo_rev_prestamo @i_operacion='VERIFICA HORA EJECUCION'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String VerificaHoraGeneracionArchivoCBU()
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_reporte_cbu @i_operacion='VERIFICA HORA EJECUCION'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String ControlAltaCuenta(string fechaAlta)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_reporte_cbu @i_operacion='CONTROL ALTA CUENTA'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_fec_desde", fechaAlta, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String ControlNovedadRepetida(string cbu)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            string comando = "execute sp_reporte_cbu @i_operacion='CONTROL INGRESO DOBLE'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_archivo", cbu, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public System.Data.DataTable GeneraArchivoReversaPrestamo(String archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_genera_archivo_rev_prestamo @i_operacion='GENERA_ARCHIVO'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_archivo", archivo, "S");

            return ArmaDataTable(comando);
        }

        public System.Data.DataTable GeneraArchivoCBU(String archivo)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string comando = "execute sp_reporte_cbu @i_operacion='GENERA_ARCHIVO'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_archivo", archivo, "S");

            return ArmaDataTable(comando);
        }

        public String NotificacionPorDocumento(string tipoDocumento, string numeroDocumento)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string notificacion = "";

            string comando = "execute sp_notificaciones @i_operacion = 'CONSULTA' ";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_documento", numeroDocumento, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_doc", tipoDocumento, "N");

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = ArmaDataTable(comando);

            foreach (System.Data.DataRow row in dt.Rows)
            {
                notificacion = "<div align='center'style='color:@COLOR@;font-weight:bold; font-size:medium;font-family:Arial;'>" + row["notificacion"].ToString() + " </div>";
                switch (row["semaforo"].ToString())
                {
                    case "ROJO":
                        notificacion = notificacion.Replace("@COLOR@", "#FF0000");
                        break;
                    case "AMARILLO":
                        notificacion = notificacion.Replace("@COLOR@", "#FFFF00");
                        break;
                    case "VERDE":
                        notificacion = notificacion.Replace("@COLOR@", "#009933");
                        break;
                    default:
                        notificacion = notificacion.Replace("@COLOR@", "#0000FF");
                        break;
                }
            }

            return notificacion;
        }

        public string ObtieneCategoriaRentabilidad(string tipoDoc, string documento)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_categoria_rentabilidad @i_operacion = 'CONSULTA'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_tipo_doc", tipoDoc, "S");
            comando = comando + dato.ArmarParametro("@i_nro_doc", documento, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public string GrabaCategoriaRentabilidad(string documento, string categoria)
        {
            usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];//  HttpContext.Current.Session. ["Usuario"];

            string comando = "execute sp_categoria_rentabilidad @i_operacion = 'GRABAR'";
            comando = comando + dato.ArmarParametro("@i_usuario", usuario.usuario, "S");
            comando = comando + dato.ArmarParametro("@i_categoria", categoria, "S");
            comando = comando + dato.ArmarParametro("@i_nro_doc", documento, "S");

            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }
    }
}