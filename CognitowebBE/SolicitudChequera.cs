using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class SolicitudChequera
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

        private String _tipoChequera;
        public String tipoChequera
        {
            get { return _tipoChequera; }
            set
            {
                _tipoChequera = value;
            }
        }

        private String _cantidadCheque;
        public String cantidadCheque
        {
            get { return _cantidadCheque; }
            set
            {
                _cantidadCheque = value;
            }
        }

        private String _cuit;
        public String cuit
        {
            get { return _cuit; }
            set
            {
                _cuit = value;
            }
        }

        private String _rsocial;
        public String rsocial
        {
            get { return _rsocial; }
            set
            {
                _rsocial = value;
            }
        }
    }
}
