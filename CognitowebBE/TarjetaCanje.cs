using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class TarjetaCanje
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

        private String _tipoDoc;
        public String tipoDoc
        {
            get { return _tipoDoc; }
            set
            {
                _tipoDoc = value;
            }
        }
        
        private String _nroDoc;
        public String nroDoc
        {
            get { return _nroDoc; }
            set
            {
                _nroDoc = value;
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

        private String _apellido;
        public String apellido
        {
            get { return _apellido; }
            set
            {
                _apellido = value;
            }
        }

        private String _nombre;
        public String nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
            }
        }

        private String _condicion;
        public String condicion
        {
            get { return _condicion; }
            set
            {
                _condicion = value;
            }
        }

        private String _puntosAcum;
        public String puntosAcum
        {
            get { return _puntosAcum; }
            set
            {
                _puntosAcum = value;
            }
        }

        private String _destino;
        public String destino
        {
            get { return _destino; }
            set
            {
                _destino = value;
            }
        }

        private String _fecIda;
        public String fecIda
        {
            get { return _fecIda; }
            set
            {
                _fecIda = value;
            }
        }

        private String _fecVuelta;
        public String fecVuelta
        {
            get { return _fecVuelta; }
            set
            {
                _fecVuelta = value;
            }
        }
    }
}

