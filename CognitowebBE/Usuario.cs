using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CognitowebBE
{
    public class Usuario
    {
        private string _usuario;
        public string usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
            }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
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

        private string _sucursalNombre;
        public string sucursalNombre
        {
            get { return _sucursalNombre; }
            set
            {
                _sucursalNombre = value;
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

        private string _estadoDescripcion;
        public string estadoDescripcion
        {
            get { return _estadoDescripcion; }
            set
            {
                _estadoDescripcion = value;
            }
        }

        private string _responsable;
        public string responsable
        {
            get { return _responsable; }
            set
            {
                _responsable = value;
            }
        }

        private string _fichaCensal;
        public string fichaCensal
        {
            get { return _fichaCensal; }
            set
            {
                _fichaCensal = value;
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

        private string _permiteFechaValor;
        public string permiteFechaValor
        {
            get { return _permiteFechaValor; }
            set
            {
                _permiteFechaValor = value;
            }
        }
    }
}
