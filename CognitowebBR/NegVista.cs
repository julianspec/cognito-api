using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using CognitowebBE;
using CognitowebDA;

namespace CognitowebBR
{
    public class NegVista
    {
        public NegVista()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //CARGA LOS DATOS DEL CLIENTE CUANDO MODIFICA EL NRO DE DOCUMENTO
        public Boolean  NexosDocumento(string tipoDoc, string nroDoc )
        {
            string comando = "execute sp_nexo @i_operacion = 'NEXOS', ";
            comando = comando + "@i_tipo_doc = '" + tipoDoc + "', @i_nro_doc = '" + nroDoc + "', @i_vista = 1";

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);
            Cliente cliente = new Cliente();

            if (resultado[0] == "" || resultado[0] == null)
            {
                cliente.esCliente = "N";
            }
            else
            {
                cliente.esCliente = "S";
                cliente.dirCalle = resultado[11];
                cliente.dirNro = resultado[12];
                cliente.pisoDto = resultado[13];
                cliente.cpostal = resultado[15];
                cliente.localidad = resultado[16];
                cliente.telefono = resultado[18];
                cliente.celular = resultado[21];
                cliente.telAlternativo = resultado[22];
                cliente.email = resultado[23];
                cliente.empleado = resultado[24];
                cliente.tipoDoc = tipoDoc;
                cliente.nroDoc = nroDoc;
                try
                {
                    int principio = resultado[10].IndexOf(" ");
                    int longitud = resultado[10].Length;
                    cliente.apellido = resultado[10].Substring(0, principio);

                    principio = principio + 1;
                    longitud = longitud - principio;
                    cliente.nombre = resultado[10].Substring(principio, longitud);
                }
                catch
                {
                    cliente.apellido = "";
                    cliente.nombre = "";
                }
            }

            Lista lista = new Lista();
            lista.GrabaCliente(cliente);

            if (cliente.esCliente == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Data.DataSet NexosApellido(string apellido)
        {
            try
            {
                string comando = "execute sp_nexo @i_operacion = 'BUSCA_APELLIDO', ";
                comando = comando + "@i_apellido = '" + apellido + "'";

                System.Data.DataSet ds = new System.Data.DataSet();
                Dato dato = new Dato();
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

        // CARGA LOS PRODUCTOS DEL CLIENTE CON LAS 4 VISTAS DISPONIBLES
        public System.Data.DataSet ClienteVista(string tipoDoc, string nroDoc, string vista)
        {
            return ClienteVista(tipoDoc, nroDoc, vista, "");
        }

        // CARGA LOS PRODUCTOS DEL CLIENTE CON LAS 4 VISTAS DISPONIBLES
        public System.Data.DataSet ClienteVista(string tipoDoc, string nroDoc, string vista, string modo)
        {
            try
            {
                Dato dato = new Dato();
                string comando = "execute sp_nexo @i_operacion = 'NEXOS', ";
                comando += "@i_tipo_doc = '" + tipoDoc + "', @i_nro_doc = '" + nroDoc + "', @i_vista = " + vista;
                comando += dato.ArmarParametro("@i_modo", modo, "S");

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

        public String ConvierteTipoDocumento(string tipoDoc)
        {
            string comando = "";

            if (tipoDoc != "")
            {
                comando = "execute sp_nexo @i_operacion = 'CONVIERTE_DOCUMENTO', @i_tipo_doc = '" + tipoDoc.Substring(0, 2) + "'";
            }
            else
            {
                return "";
            }

            Dato dato = new Dato();
            string[] resultado = new string[100];
            resultado = dato.SpString(comando);

            return resultado[0];
        }

        public String ConvierteNroDocumento(string nroDoc)
        {
            try {
            nroDoc = nroDoc.Substring(2, nroDoc.Length - 2);
            double numerico = Convert.ToDouble(nroDoc);
            nroDoc = Convert.ToString(numerico);
            }
            catch(Exception ex)
            {
            }

            return nroDoc;
        }
    }
}