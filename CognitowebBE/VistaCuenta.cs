using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class VistaCuenta
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

        private string _cuenta;
        public string cuenta
        {
            get { return _cuenta; }
            set
            {
                _cuenta = value;
            }
        }

        private string _tipo;
        public string tipo
        {
            get { return _tipo; }
            set
            {
                _tipo = value;
            }
        }

        private string _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set
            {
                _sucursal = value.Replace("&nbsp;", ""); 
            }
        }

        private string _sucursalNro;
        public string sucursalNro
        {
            get { return _sucursalNro; }
            set
            {
                _sucursalNro = value.Replace("&nbsp;", "");
            }
        }

        private string _nombreSistema;
        public string nombreSistema
        {
            get { return _nombreSistema; }
            set
            {
                _nombreSistema = value;
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

        private string _calidad; public string calidad {get { return _calidad; } set {_calidad= value;} }
        private string _calidadDesc; public string calidadDesc { get { return _calidadDesc; } set { _calidadDesc = value; } }
        private string _tipoCuenta; public string tipoCuenta { get { return _tipoCuenta; } set { _tipoCuenta = value; } }
        private string _tipoCuentaDesc; public string tipoCuentaDesc { get { return _tipoCuentaDesc; } set { _tipoCuentaDesc = value; } }
        private string _cbu; public string cbu { get { return _cbu; } set { _cbu = value; } }
    }
}
