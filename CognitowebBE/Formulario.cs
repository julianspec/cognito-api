using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Formulario
    {
        private string _prefijo;
        public string prefijo
        {
            get { return _prefijo; }
            set
            {
                _prefijo = value;
            }
        }

        private string _id;
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        private string _fecIngreso;
        public string fecIngreso
        {
            get { return _fecIngreso; }
            set
            {
                _fecIngreso = value;
            }
        }

        private string _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set
            {
                _sucursal = value;
            }
        }

        private string _sucursalDesc;
        public string sucursalDesc
        {
            get { return _sucursalDesc; }
            set
            {
                _sucursalDesc = value;
            }
        }

        private string _usuarioDeriv;
        public string usuarioDeriv
        {
            get { return _usuarioDeriv; }
            set
            {
                _usuarioDeriv = value;
            }
        }

        private string _estado;
        public string estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
            }
        }

        private string _estadoDesc;
        public string estadoDesc
        {
            get { return _estadoDesc; }
            set
            {
                _estadoDesc = value;
            }
        }

        private string _tipoOperacion;
        public string tipoOperacion
        {
            get { return _tipoOperacion; }
            set
            {
                _tipoOperacion = value;
            }
        }

        private string _tipoOperacionDesc;
        public string tipoOperacionDesc
        {
            get { return _tipoOperacionDesc; }
            set
            {
                _tipoOperacionDesc = value;
            }
        }

        private string _producto;
        public string producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
            }
        }

        private string _productoDesc;
        public string productoDesc
        {
            get { return _productoDesc; }
            set
            {
                _productoDesc = value;
            }
        }

        private string _subProducto;
        public string subProducto
        {
            get { return _subProducto; }
            set
            {
                _subProducto = value;
            }
        }

        private string _subProductoDesc;
        public string subProductoDesc
        {
            get { return _subProductoDesc; }
            set
            {
                _subProductoDesc = value;
            }
        }

        private string _motivo;
        public string motivo
        {
            get { return _motivo; }
            set
            {
                _motivo = value;
            }
        }

        private string _motivoDesc;
        public string motivoDesc
        {
            get { return _motivoDesc; }
            set
            {
                _motivoDesc = value;
            }
        }

        private string _origenCaptura;
        public string origenCaptura
        {
            get { return _origenCaptura; }
            set
            {
                _origenCaptura = value;
            }
        }

        private string _origenCapturaDesc;
        public string origenCapturaDesc
        {
            get { return _origenCapturaDesc; }
            set
            {
                _origenCapturaDesc = value;
            }
        }

        private string _fecOper;
        public string fecOper
        {
            get { return _fecOper; }
            set
            {
                _fecOper = value;
            }
        }

        private string _cliTipoProd;
        public string cliTipoProd
        {
            get { return _cliTipoProd; }
            set
            {
                _cliTipoProd = value;
            }
        }

        private string _cliNroProd;
        public string cliNroProd
        {
            get { return _cliNroProd; }
            set
            {
                _cliNroProd = value;
            }
        }

        private string _cliCondicion;
        public string cliCondicion
        {
            get { return _cliCondicion; }
            set
            {
                _cliCondicion = value;
            }
        }

        private string _cliRespuesta;
        public string cliRespuesta
        {
            get { return _cliRespuesta; }
            set
            {
                _cliRespuesta = value;
            }
        }

        private string _cliSucursal;
        public string cliSucursal
        {
            get { return _cliSucursal; }
            set
            {
                _cliSucursal = value;
            }
        }

        private string _tarjMarca;
        public string tarjMarca
        {
            get { return _tarjMarca; }
            set
            {
                _tarjMarca = value;
            }
        }

        private string _tarjNro;
        public string tarjNro
        {
            get { return _tarjNro; }
            set
            {
                _tarjNro = value;
            }
        }

        private string _tarjCuenta;
        public string tarjCuenta
        {
            get { return _tarjCuenta; }
            set
            {
                _tarjCuenta = value;
            }
        }

        private string _tarjFecCierre;
        public string tarjFecCierre
        {
            get { return _tarjFecCierre; }
            set
            {
                _tarjFecCierre = value;
            }
        }

        private string _tarjFecVto;
        public string tarjFecVto
        {
            get { return _tarjFecVto; }
            set
            {
                _tarjFecVto = value;
            }
        }

        private string _tarjExcepcion;
        public string tarjExcepcion
        {
            get { return _tarjExcepcion; }
            set
            {
                _tarjExcepcion = value;
            }
        }

        private string _tarjCupon;
        public string tarjCupon
        {
            get { return _tarjCupon; }
            set
            {
                _tarjCupon = value;
            }
        }

        private string _tarjImporte;
        public string tarjImporte
        {
            get { return _tarjImporte; }
            set
            {
                if (value != null)
                {
                    _tarjImporte = value.Replace(",", ".");
                }
            }
        }

        private string _tarjRef;
        public string tarjRef
        {
            get { return _tarjRef; }
            set
            {
                _tarjRef = value;
            }
        }

        private string _tarjAdicNombre;
        public string tarjAdicNombre
        {
            get { return _tarjAdicNombre; }
            set
            {
                _tarjAdicNombre = value;
            }
        }

        private string _tarjAdicDoc;
        public string tarjAdicDoc
        {
            get { return _tarjAdicDoc; }
            set
            {
                _tarjAdicDoc = value;
            }
        }

        private string _tarjDirNro;
        public string tarjDirNro
        {
            get { return _tarjDirNro; }
            set
            {
                _tarjDirNro = value;
            }
        }

        private string _tarjPisoDto;
        public string tarjPisoDto
        {
            get { return _tarjPisoDto; }
            set
            {
                _tarjPisoDto = value;
            }
        }

        private string _tarjCpostal;
        public string tarjCpostal
        {
            get { return _tarjCpostal; }
            set
            {
                _tarjCpostal = value;
            }
        }

        private string _tarjLocalidad;
        public string tarjLocalidad
        {
            get { return _tarjLocalidad; }
            set
            {
                _tarjLocalidad = value;
            }
        }

        private string _tarjProvincia;
        public string tarjProvincia
        {
            get { return _tarjProvincia; }
            set
            {
                _tarjProvincia = value;
            }
        }

        private string _tarjTelefono;
        public string tarjTelefono
        {
            get { return _tarjTelefono; }
            set
            {
                _tarjTelefono = value;
            }
        }

        private string _formaPago;
        public string formaPago
        {
            get { return _formaPago; }
            set
            {
                _formaPago = value;
            }
        }

        private string _ctaSucursal;
        public string ctaSucursal
        {
            get { return _ctaSucursal; }
            set
            {
                _ctaSucursal = value;
            }
        }

        private string _tipoCuenta;
        public string tipoCuenta
        {
            get { return _tipoCuenta; }
            set
            {
                _tipoCuenta = value;
            }
        }

        private string _nroCuenta;
        public string nroCuenta
        {
            get { return _nroCuenta; }
            set
            {
                _nroCuenta = value;
            }
        }

        private string _preimpreso;
        public string preimpreso
        {
            get { return _preimpreso; }
            set
            {
                _preimpreso = value;
            }
        }

        private string _importe;
        public string importe
        {
            get { return _importe; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _importe = value.Replace(",", ".");
            }
        }

        private string _medio;
        public string medio
        {
            get { return _medio; }
            set
            {
                _medio = value;
            }
        }

        private string _banco;
        public string banco
        {
            get { return _banco; }
            set
            {
                _banco = value;
            }
        }

        private string _cbu;
        public string cbu
        {
            get { return _cbu; }
            set
            {
                _cbu = value;
            }
        }

        private string _nroOper;
        public string nroOper
        {
            get { return _nroOper; }
            set
            {
                _nroOper = value;
            }
        }

        private string _fecDeposito;
        public string fecDeposito
        {
            get { return _fecDeposito; }
            set
            {
                _fecDeposito = value;
            }
        }

        private string _chequePlaza;
        public string chequePlaza
        {
            get { return _chequePlaza; }
            set
            {
                _chequePlaza = value;
            }
        }

        private string _chequeNro;
        public string chequeNro
        {
            get { return _chequeNro; }
            set
            {
                _chequeNro = value;
            }
        }

        private string _solicitaContacto;
        public string solicitaContacto
        {
            get 
            {
                if (_solicitaContacto == null)
                {
                    _solicitaContacto = "S";
                }
                return _solicitaContacto; 
            }
            set
            {
                _solicitaContacto = value;
            }
        }

        private string _incoveniente;
        public string incoveniente
        {
            get 
            {
                Normaliza normaliza = new Normaliza();
                //return _incoveniente; 
                return normaliza.Convierte(_incoveniente);
            }
            set
            {
                Normaliza normaliza = new Normaliza();
                _incoveniente = normaliza.Convierte(value);
            }
        }

        private string _referencia;
        public string referencia
        {
            get { return _referencia; }
            set
            {
                _referencia = value;
            }
        }

        private string _usuario;
        public string usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
            }
        }

        private string _usuarioNombre;
        public string usuarioNombre
        {
            get { return _usuarioNombre; }
            set
            {
                _usuarioNombre = value;
            }
        }

        private string _falta;
        public string falta
        {
            get { return _falta; }
            set
            {
                _falta = value;
            }
        }

        private string _fmodif;
        public string fmodif
        {
            get { return _fmodif; }
            set
            {
                _fmodif = value;
            }
        }

        private string _resumenMail;
        public string resumenMail
        {
            get { return _resumenMail; }
            set
            {
                _resumenMail = value;
            }
        }

        private string _infoComercial;
        public string infoComercial
        {
            get { return _infoComercial; }
            set
            {
                _infoComercial = value;
            }
        }

        private string _tarjCalle;
        public string tarjCalle
        {
            get { return _tarjCalle; }
            set
            {
                _tarjCalle = value;
            }
        }

        private string _adjunto; public string adjunto {get { return _adjunto; } set {_adjunto = value;}}

    }
}
