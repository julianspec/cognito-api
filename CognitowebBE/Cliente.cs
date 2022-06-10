using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cliente
/// </summary>
/// 

namespace CognitowebBE
{
    public class Cliente
    {
        Normaliza normaliza = new Normaliza();

        public string prefijo
        {
            get;
            set;
        }

        public string id
        {
            get;
            set;
        }

        public string tipoDoc

        {
            get;
            set;
        }

        private string _tipoDocDesc;
        public string tipoDocDesc
        {
            get
            {
                if (_tipoDocDesc == null)
                {
                    _tipoDocDesc = "DNI";
                }
                return _tipoDocDesc;
            }
            set
            {
                _tipoDocDesc = value;
            }
        }

        public string nroDoc

        {
            get;
            set;
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = normaliza.Convierte(value);
                //_nombre = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _apellido;
        public string apellido
        {
            get { return _apellido; }
            set
            {
                _apellido = normaliza.Convierte(value);
                //_apellido = value != null ? value.ToUpperInvariant() : value;
            }
        }

        public string cuit
            {
                get;
                set;
            }

        private string _dirCalle;
        public string dirCalle
        {
            get { return _dirCalle; }
            set
            {
                _dirCalle = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _dirNro;
        public string dirNro
        {
            get { return _dirNro; }
            set
            {
                _dirNro = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _pisoDto;
        public string pisoDto
        {
            get { return _pisoDto; }
            set
            {
                _pisoDto = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _cpostal;
        public string cpostal
        {
            get { return _cpostal; }
            set
            {
                _cpostal = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _localidad;
        public string localidad
        {
            get { return _localidad; }
            set
            {
                _localidad = value != null ? value.ToUpperInvariant() : value;
            }
        }

        public string provincia
            {
                get;
                set;
            }

        private string _telefono;
        public string telefono
        {
            get { return _telefono; }
            set
            {
                _telefono = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _celular;
        public string celular
        {
            get { return _celular; }
            set
            {
                _celular = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _telAlternativo;
        public string telAlternativo
        {
            get { return _telAlternativo; }
            set
            {
                _telAlternativo = value != null ? value.ToUpperInvariant() : value;
            }
        }

        public string email
            {
                get;
                set;
            }

        public string sucursal
            {
                get;
                set;
            }

        public string medio
            {
                get;
                set;
            }

        public string nroCuenta
            {
                get;
                set;
            }

        private string _modifDatos;
        public string modifDatos
        {   get {
                    if (_modifDatos == null)
                    {
                        _modifDatos = "N";
                    }
                    return _modifDatos;
                }
            set {
                    _modifDatos = value;
                }
        }

        private string _modifTelPart;
        public string modifTelPart
        {
            get
            {
                if (_modifTelPart == null)
                {
                    _modifTelPart = "N";
                }
                return _modifTelPart;
            }
            set
            {
                _modifTelPart = value;
            }
        }

        private string _modifCelular;
        public string modifCelular
        {
            get
            {
                if (_modifCelular == null)
                {
                    _modifCelular = "N";
                }
                return _modifCelular;
            }
            set
            {
                _modifCelular = value;
            }
        }

        private string _modifTelAlt;
        public string modifTelAlt
        {
            get
            {
                if (_modifTelAlt == null)
                {
                    _modifTelAlt = "N";
                }
                return _modifTelAlt;
            }
            set
            {
                _modifTelAlt = value;
            }
        }

        private string _modifDomicilio;
        public string modifDomicilio
        {
            get
            {
                if (_modifDomicilio == null)
                {
                    _modifDomicilio = "N";
                }
                return _modifDomicilio;
            }
            set
            {
                _modifDomicilio = value;
            }
        }

        public string usuario
            {
                get;
                set;
            }

        public string falta
            {
                get;
                set;
            }

        public string fmodif
        {
            get;
            set;
        }

        private string _esCliente;
        public string esCliente
        {
            get
            {
                if (_esCliente == null)
                {
                    _esCliente = "N";
                }
                return _esCliente;
            }
            set
            {
                _esCliente = value;
            }
        }

        public string fecNac
        {
            get;
            set;
        }

        private string _empleado;
        public string empleado
        {
            get
            {
                if (_empleado == null)
                {
                    _empleado = "NO";
                }
                return _empleado;
            }
            set
            {
                _empleado = value;
            }
        }

        public string actualizaDatos
        {
            get;
            set;
        }

        public string categoriaRentabilidad
        {
            get;
            set;
        }
    }
}