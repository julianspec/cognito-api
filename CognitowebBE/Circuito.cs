using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Circuito
    {
        private String _id;
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        private String _prefijo;
        public string prefijo
        {
            get { return _prefijo; }
            set
            {
                _prefijo = value;
            }
        }

        private String _fecha;
        public string fecha
        {
            get { return _fecha; }
            set
            {
                _fecha = value;
            }
        }

        private String _usuario;
        public string usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
            }
        }

        private String _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set
            {
                _sucursal = value;
            }
        }

        private String _deriv;
        public string deriv
        {
            get { return _deriv; }
            set
            {
                _deriv = value;
            }
        }

        private String _usuarioDeriv;
        public string usuarioDeriv
        {
            get { return _usuarioDeriv; }
            set
            {
                _usuarioDeriv = value;
            }
        }

        private String _estado;
        public string estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
            }
        }

        private String _obs;
        public string obs
        {
            get { return _obs; }
            set
            {
                Normaliza normaliza = new Normaliza();
                _obs = normaliza.Convierte(value);
            }
        }

        private String _sucursalDesde;
        public string sucursalDesde
        {
            get { return _sucursalDesde; }
            set
            {
                _sucursalDesde = value;
            }
        }

        private String _mail;
        public string mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
            }
        }

    }
}
