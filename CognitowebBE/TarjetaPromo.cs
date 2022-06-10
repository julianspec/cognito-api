using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class TarjetaPromo
    {
        private String _prefijo;
        public String prefijo
        {
            get { return _prefijo; }
            set
            {
                _prefijo = value;
            }
        }

        private String _id;
        public String id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        private String _registro;
        public String registro
        {
            get { return _registro; }
            set
            {
                _registro = value;
            }
        }
        
        private String _ctaSucursal;
        public String ctaSucursal
        {
            get { return _ctaSucursal; }
            set
            {
                _ctaSucursal = value;
            }
        }

        private String _nroCuenta;
        public String nroCuenta
        {
            get { return _nroCuenta; }
            set
            {
                _nroCuenta = value;
            }
        }

        private String _tarjMarca;
        public String tarjMarca
        {
            get { return _tarjMarca; }
            set
            {
                _tarjMarca = value;
            }
        }

        private String _tarjNro;
        public String tarjNro
        {
            get { return _tarjNro; }
            set
            {
                _tarjNro = value;
            }
        }

        private String _fechaCierre;
        public String fechaCierre
        {
            get { return _fechaCierre; }
            set
            {
                _fechaCierre = value;
            }
        }

        private String _fechaVencimiento;
        public String fechaVencimiento
        {
            get { return _fechaVencimiento; }
            set
            {
                _fechaVencimiento = value;
            }
        }

        private String _fechaOperacion;
        public String fechaOperacion
        {
            get { return _fechaOperacion; }
            set
            {
                _fechaOperacion = value;
            }
        }

        private String _importeOperacion;
        public String importeOperacion
        {
            get { return _importeOperacion; }
            set
            {
                _importeOperacion = value;
            }
        }

        private String _nroComercio;
        public String nroComercio
        {
            get { return _nroComercio; }
            set
            {
                _nroComercio = value;
            }
        }

        private String _comercio;
        public String comercio
        {
            get { return _comercio; }
            set
            {
                Normaliza normaliza = new Normaliza();
                _comercio = normaliza.Convierte(value);
            }
        }

        private String _rubro;
        public String rubro
        {
            get { return _rubro; }
            set
            {
                Normaliza normaliza = new Normaliza();
                _rubro = normaliza.Convierte(value);
            }
        }

        private String _montoDevolucion;
        public String montoDevolucion
        {
            get { return _montoDevolucion; }
            set
            {
                _montoDevolucion = value;
            }
        }

        private String _estado;
        public String estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
            }
        }

        private String _estadoDesc;
        public String estadoDesc
        {
            get { return _estadoDesc; }
            set
            {
                _estadoDesc = value;
            }
        }

        private String _sistema;
        public String sistema
        {
            get { return _sistema; }
            set
            {
                _sistema = value.Replace("&nbsp;", "");
            }
        }

        private String _barrido;
        public String barrido
        {
            get { return _barrido; }
            set
            {
                _barrido = value.Replace("&nbsp;", "");
            }
        }
    }
}
