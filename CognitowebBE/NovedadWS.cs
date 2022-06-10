using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class NovedadWS
    {
        

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
            }

            return resultado;
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

        private string GrabarNovedad(Adintar.NovedadStr novedadStr)
        {
            string result = "";
            Adintar.ServiciosDispatcherWEBBcBaSoapClient wsAdintar = new CognitowebBE.Adintar.ServiciosDispatcherWEBBcBaSoapClient();
            result = wsAdintar.GrabarNovedad_Generico("COGNITO", "", novedadStr);

            return result;
        }
    }
}
