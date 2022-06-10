using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.ServiceModel.Configuration;  

namespace CognitowebBE
{
    public class NovedadWS
    {

        public string servicioURL() {
            ClientSection clientSection = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
            ChannelEndpointElement endpoint = clientSection.Endpoints[0];

            string endpointStr = endpoint.Address.ToString();
            return endpointStr;
        }

        public string GrabarNovedad_baja_eresumen_tc(string cod_cue, string marcaTarjeta)
        {
            string resultado = "";
            Adintar.NovedadStr novedadStr = new Adintar.NovedadStr();

            novedadStr.Cod_cue = cod_cue;
            novedadStr.Codigo_ent = "29";
            novedadStr.Cod_pla = "";
            novedadStr.Codigo_tipdom = "";
            novedadStr.Nro_adic = 0;
            novedadStr.Nro_nov = "0";


            /*COMPLETO NOVEDADES SEGUN LA MARCA DE LA TARJETA*/

            switch (marcaTarjeta)
            {
                case "1":
                    novedadStr = baja_eresumen_tc_visa(novedadStr, cod_cue);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "2":
                    novedadStr = baja_eresumen_tc_master(novedadStr, cod_cue);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                default:
                    resultado = "Codigo de marca de tarjeta desconocido: " + marcaTarjeta;
                    break;
            }

            return resultado;
        }

        public string GrabarNovedad_alta_eresumen_tc(string cod_cue, string marcaTarjeta)
        {
            string resultado = "";
            Adintar.NovedadStr novedadStr = new Adintar.NovedadStr();

            novedadStr.Cod_cue = cod_cue;
            novedadStr.Codigo_ent = "29";
            novedadStr.Cod_pla = "";
            novedadStr.Codigo_tipdom = "";
            novedadStr.Nro_adic = 0;
            novedadStr.Nro_nov = "0";


            /*COMPLETO NOVEDADES SEGUN LA MARCA DE LA TARJETA*/

            switch (marcaTarjeta)
            {
                case "1":
                    novedadStr = alta_eresumen_tc_visa(novedadStr, cod_cue);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "2":
                    novedadStr = alta_eresumen_tc_master(novedadStr, cod_cue);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                default:
                    resultado = "Codigo de marca de tarjeta desconocido: " + marcaTarjeta;
                    break;
            }

            return resultado;
        }

        public string GrabarNovedad_cambio_domicilio_tc(string cod_cue, string marcaTarjeta, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono, string cod_localidad, string loc_ofi)
        {
            string resultado = "";
            Adintar.NovedadStr novedadStr = new Adintar.NovedadStr();

            novedadStr.Cod_cue = cod_cue;
            novedadStr.Codigo_ent = "29";
            novedadStr.Cod_pla = "";
            novedadStr.Codigo_tipdom = "";
            novedadStr.Nro_adic = 0;
            novedadStr.Nro_nov = "0";


            /*COMPLETO NOVEDADES SEGUN LA MARCA DE LA TARJETA*/

            switch (marcaTarjeta)
            {
                case "1":
                    novedadStr = dom_tc_visa(novedadStr, calle, n_calle, piso, depto, cp, localidad, provincia, telefono, cod_localidad, loc_ofi);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "2":
                    novedadStr = dom_tc_master(novedadStr, calle, n_calle, piso, depto, cp, localidad, provincia, telefono);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "3":
                    novedadStr = dom_tc_cabal1(novedadStr, calle, n_calle, piso, depto, cp, localidad, provincia, telefono);
                    resultado = GrabarNovedad(novedadStr);
                    novedadStr = dom_tc_cabal2(novedadStr, calle, n_calle, piso, depto, cp, localidad, provincia, telefono);
                    resultado = GrabarNovedad(novedadStr);
                    novedadStr = dom_tc_cabal3(novedadStr, calle, n_calle, piso, depto, cp, localidad, provincia, telefono);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                default:
                    resultado = "Codigo de marca de tarjeta desconocido: " + marcaTarjeta;
                    break;
            }

            return resultado;
        }

        public string GrabarNovedad_fpag_tc(string cod_cue, string marcaTarjeta, string t_ctaBancaria, string fdebito, string n_ctaBancaria, string sucBancaria)
        {
            string resultado = "";
            Adintar.NovedadStr novedadStr = new Adintar.NovedadStr();

            novedadStr.Cod_cue = cod_cue;
            novedadStr.Codigo_ent = "29";
            novedadStr.Cod_pla = "";
            novedadStr.Codigo_tipdom = "";
            novedadStr.Nro_adic = 0;
            novedadStr.Nro_nov = "0";
            

            /*COMPLETO NOVEDADES SEGUN LA MARCA DE LA TARJETA*/

            switch (marcaTarjeta)
            {
                case "1":
                    novedadStr = fpag_tc_visa(novedadStr, t_ctaBancaria, fdebito, n_ctaBancaria, sucBancaria);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "2":
                    novedadStr = fpag_tc_master(novedadStr,t_ctaBancaria, fdebito, n_ctaBancaria, sucBancaria);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                case "3":
                    novedadStr = fpag_tc_cabal1(novedadStr, t_ctaBancaria, fdebito, n_ctaBancaria, sucBancaria);
                    resultado = GrabarNovedad(novedadStr);
                    novedadStr = fpag_tc_cabal2(novedadStr, t_ctaBancaria, fdebito, n_ctaBancaria, sucBancaria);
                    resultado = GrabarNovedad(novedadStr);
                    break;
                default:
                    resultado = "Codigo de marca de tarjeta desconocido: " + marcaTarjeta;
                    break;
            }

            return resultado;
        }



        private Adintar.NovedadStr baja_eresumen_tc_visa(Adintar.NovedadStr novedadStr, string ctaBancaria)
        {

            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "3";
            novedadStr.Cod_tipnov = "9201";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = ctaBancaria;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = "";
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = "";
            listaNovedades[2] = novCampo3;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr baja_eresumen_tc_master(Adintar.NovedadStr novedadStr, string ctaBancaria)
        {

            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "1";
            novedadStr.Cod_tipnov = "9401";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = ctaBancaria;
            listaNovedades[0] = novCampo1;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr alta_eresumen_tc_visa(Adintar.NovedadStr novedadStr, string ctaBancaria)
        {

            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "3";
            novedadStr.Cod_tipnov = "9200";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = ctaBancaria;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = "";
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = "";
            listaNovedades[2] = novCampo3;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr alta_eresumen_tc_master(Adintar.NovedadStr novedadStr, string ctaBancaria)
        {

            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "1";
            novedadStr.Cod_tipnov = "9400";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = ctaBancaria;
            listaNovedades[0] = novCampo1;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }        


        
        private Adintar.NovedadStr fpag_tc_visa(Adintar.NovedadStr novedadStr, string t_ctaBancaria, string fdebito, string n_ctaBancaria, string sucBancaria)
        {
            
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "3";
            novedadStr.Cod_tipnov = "1200";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = t_ctaBancaria;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = fdebito;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = sucBancaria;
            listaNovedades[2] = novCampo3;

            Adintar.NovedadCampoStr novCampo4 = new Adintar.NovedadCampoStr();
            novCampo4.Secuencia = 4;
            novCampo4.Valor = n_ctaBancaria;
            listaNovedades[3] = novCampo4;

            Adintar.NovedadCampoStr novCampo5 = new Adintar.NovedadCampoStr();
            novCampo5.Secuencia = 5;
            novCampo5.Valor = "";
            listaNovedades[4] = novCampo5;

            Adintar.NovedadCampoStr novCampo6 = new Adintar.NovedadCampoStr();
            novCampo6.Secuencia = 6;
            novCampo6.Valor = "";
            listaNovedades[5] = novCampo6;

            Adintar.NovedadCampoStr novCampo7 = new Adintar.NovedadCampoStr();
            novCampo7.Secuencia = 7;
            novCampo7.Valor = "";
            listaNovedades[6] = novCampo7;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr fpag_tc_master(Adintar.NovedadStr novedadStr, string t_ctaBancaria, string fdebito, string n_ctaBancaria, string sucBancaria)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "1";
            novedadStr.Cod_tipnov = "2500";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = t_ctaBancaria;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = fdebito;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = n_ctaBancaria;
            listaNovedades[2] = novCampo3;

            Adintar.NovedadCampoStr novCampo4 = new Adintar.NovedadCampoStr();
            novCampo4.Secuencia = 4;
            novCampo4.Valor = sucBancaria;
            listaNovedades[3] = novCampo4;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr fpag_tc_cabal1(Adintar.NovedadStr novedadStr, string t_ctaBancaria, string fdebito, string n_ctaBancaria, string sucBancaria)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Cod_tipnov = "1010";
            novedadStr.Codigo_adm = "2";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = t_ctaBancaria;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = sucBancaria;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = n_ctaBancaria;
            listaNovedades[2] = novCampo3;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr fpag_tc_cabal2(Adintar.NovedadStr novedadStr, string t_ctaBancaria, string fdebito, string n_ctaBancaria, string sucBancaria)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Cod_tipnov = "1011";
            novedadStr.Codigo_adm = "2";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = fdebito;
            listaNovedades[0] = novCampo1;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }



        private Adintar.NovedadStr dom_tc_visa(Adintar.NovedadStr novedadStr, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono, string cod_localidad, string loc_ofi)
        {

            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "3";
            novedadStr.Cod_tipnov = "1021";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = calle;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = n_calle;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = piso;
            listaNovedades[2] = novCampo3;

            Adintar.NovedadCampoStr novCampo4 = new Adintar.NovedadCampoStr();
            novCampo4.Secuencia = 4;
            novCampo4.Valor = depto;
            listaNovedades[3] = novCampo4;

            Adintar.NovedadCampoStr novCampo5 = new Adintar.NovedadCampoStr();
            novCampo5.Secuencia = 5;
            novCampo5.Valor = loc_ofi;
            listaNovedades[4] = novCampo5;

            Adintar.NovedadCampoStr novCampo6 = new Adintar.NovedadCampoStr();
            novCampo6.Secuencia = 6;
            novCampo6.Valor = cp;
            listaNovedades[5] = novCampo6;

            Adintar.NovedadCampoStr novCampo7 = new Adintar.NovedadCampoStr();
            novCampo7.Secuencia = 7;
            novCampo7.Valor = localidad;
            listaNovedades[6] = novCampo7;
            
            Adintar.NovedadCampoStr novCampo8 = new Adintar.NovedadCampoStr();
            novCampo8.Secuencia = 8;
            novCampo8.Valor = provincia;
            listaNovedades[7] = novCampo8;

            Adintar.NovedadCampoStr novCampo9 = new Adintar.NovedadCampoStr();
            novCampo9.Secuencia = 9;
            novCampo9.Valor = cod_localidad;
            listaNovedades[8] = novCampo9;

            Adintar.NovedadCampoStr novCampo10 = new Adintar.NovedadCampoStr();
            novCampo10.Secuencia = 10;
            novCampo10.Valor = telefono;
            listaNovedades[9] = novCampo10;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr dom_tc_master(Adintar.NovedadStr novedadStr, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "1";
            novedadStr.Cod_tipnov = "2100";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = "6";
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = calle;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = n_calle;
            listaNovedades[2] = novCampo3;

            Adintar.NovedadCampoStr novCampo4 = new Adintar.NovedadCampoStr();
            novCampo4.Secuencia = 4;
            novCampo4.Valor = piso;
            listaNovedades[3] = novCampo4;

            Adintar.NovedadCampoStr novCampo5 = new Adintar.NovedadCampoStr();
            novCampo5.Secuencia = 5;
            novCampo5.Valor = depto;
            listaNovedades[4] = novCampo5;

            Adintar.NovedadCampoStr novCampo6 = new Adintar.NovedadCampoStr();
            novCampo6.Secuencia = 6;
            novCampo6.Valor = cp;
            listaNovedades[5] = novCampo6;

            Adintar.NovedadCampoStr novCampo7 = new Adintar.NovedadCampoStr();
            novCampo7.Secuencia = 7;
            novCampo7.Valor = localidad;
            listaNovedades[6] = novCampo7;

            Adintar.NovedadCampoStr novCampo8 = new Adintar.NovedadCampoStr();
            novCampo8.Secuencia = 8;
            novCampo8.Valor = provincia;
            listaNovedades[7] = novCampo8;

            Adintar.NovedadCampoStr novCampo9 = new Adintar.NovedadCampoStr();
            novCampo9.Secuencia = 9;
            novCampo9.Valor = telefono;
            listaNovedades[8] = novCampo9;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr dom_tc_cabal1(Adintar.NovedadStr novedadStr, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "2";
            novedadStr.Cod_tipnov = "1003";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = calle;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = n_calle;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = piso;
            listaNovedades[2] = novCampo3;

            Adintar.NovedadCampoStr novCampo4 = new Adintar.NovedadCampoStr();
            novCampo4.Secuencia = 4;
            novCampo4.Valor = depto;
            listaNovedades[3] = novCampo4;
            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr dom_tc_cabal2(Adintar.NovedadStr novedadStr, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "2";
            novedadStr.Cod_tipnov = "1004";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = localidad;
            listaNovedades[0] = novCampo1;

            Adintar.NovedadCampoStr novCampo2 = new Adintar.NovedadCampoStr();
            novCampo2.Secuencia = 2;
            novCampo2.Valor = cp;
            listaNovedades[1] = novCampo2;

            Adintar.NovedadCampoStr novCampo3 = new Adintar.NovedadCampoStr();
            novCampo3.Secuencia = 3;
            novCampo3.Valor = provincia;
            listaNovedades[2] = novCampo3;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }

        private Adintar.NovedadStr dom_tc_cabal3(Adintar.NovedadStr novedadStr, string calle, string n_calle, string piso, string depto, string cp, string localidad, string provincia, string telefono)
        {
            Adintar.NovedadCampoStr[] listaNovedades = new Adintar.NovedadCampoStr[10];
            novedadStr.Codigo_adm = "2";
            novedadStr.Cod_tipnov = "1005";

            Adintar.NovedadCampoStr novCampo1 = new Adintar.NovedadCampoStr();
            novCampo1.Secuencia = 1;
            novCampo1.Valor = telefono;
            listaNovedades[0] = novCampo1;

            novedadStr.ColCamposNovedad = listaNovedades;

            return novedadStr;
        }



        private string GrabarNovedad(Adintar.NovedadStr novedadStr)
        {
            string result = "";
            Adintar.ServiciosDispatcherWEBBcBaSoapClient wsAdintar = new CognitowebBE.Adintar.ServiciosDispatcherWEBBcBaSoapClient();
            result = wsAdintar.GrabarNovedad_Generico("COGNITO", "", novedadStr);

            return result;
        }
    }
}
