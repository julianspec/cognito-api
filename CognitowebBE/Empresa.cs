using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Empresa
    {

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

        private string _rsocial;
        public string rsocial
        {
            get { return _rsocial; }
            set
            {
                _rsocial = value != null ? value.ToUpperInvariant() : value;
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

        private string _cuit;
        public string cuit
        {
            get { return _cuit; }
            set
            {
                _cuit = value;
            }
        }

        private string _responsable;
        public string responsable
        {
            get { return _responsable; }
            set
            {
                _responsable = value != null ? value.ToUpperInvariant() : value;
            }
        }

        private string _factAnual;
        public string factAnual
        {
            get { return _factAnual; }
            set
            {
                _factAnual = value;
            }
        }

        private string _sucRef;
        public string sucRef
        {
            get { return _sucRef; }
            set
            {
                _sucRef = value;
            }
        }

        private string _direccion;
        public string direccion
        {
            get { return _direccion; }
            set
            {
                _direccion = value != null ? value.ToUpperInvariant() : value;
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
        
        private string _provincia;
        public string provincia
        {
            get { return _provincia; }
            set
            {
                _provincia = value;
            }
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

        private string _email;
        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }
        
        private string _lineaCred;
        public string lineaCred
        {
            get { return _lineaCred; }
            set
            {
                _lineaCred = value;
            }
        }
        
        private string _prodServ;
        public string prodServ
        {
            get { return _prodServ; }
            set
            {
                _prodServ = value;
            }
        }

        private string _solicitaContacto;
        public string solicitaContacto
        {
            get 
            {
                if (_solicitaContacto == null)
                {
                    _solicitaContacto = "N";
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
            get { return _incoveniente; }
            set
            {
                _incoveniente = value;
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

    }
}
