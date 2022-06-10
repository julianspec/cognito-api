using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class VistaTarjeta
    {
        private string _producto;
        public string producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
            }
        }

        private string _tarjeta;
        public string tarjeta
        {
            get { return _tarjeta; }
            set
            {
                _tarjeta = value;
            }
        }

        private string _cuenta;
        public string cuenta
        {
            get { return _cuenta; }
            set
            {
                _cuenta = value;
            }
        }

        private string _desc_adm;
        public string desc_adm
        {
            get { return _desc_adm; }
            set
            {
                _desc_adm = value.Replace("&nbsp;", ""); ;
            }
        }

        private string _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set
            {
                _sucursal = value.Replace("&nbsp;", ""); ;
            }
        }

        private string _sistema;
        public string sistema
        {
            get { return _sistema; }
            set
            {
                _sistema = value;
            }
        }
    }
}
