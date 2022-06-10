using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Domicilio
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

        private String _ctaSuc;
        public String ctaSuc
        {
            get { return _ctaSuc; }
            set
            {
                _ctaSuc = value;
            }
        }

        private String _ctaTipo;
        public String ctaTipo
        {
            get { return _ctaTipo; }
            set
            {
                _ctaTipo = value;
            }
        }

        private String _ctaNumero;
        public String ctaNumero
        {
            get { return _ctaNumero; }
            set
            {
                _ctaNumero = value;
            }
        }

        private String _dirCalle;
        public String dirCalle
        {
            get { return _dirCalle; }
            set
            {
                _dirCalle = value;
            }
        }

        private String _dirDirNro;
        public String dirDirNro
        {
            get { return _dirDirNro; }
            set
            {
                _dirDirNro = value;
            }
        }

        private String _dirPisoDto;
        public String dirPisoDto
        {
            get { return _dirPisoDto; }
            set
            {
                _dirPisoDto = value;
            }
        }

        private String _dirCpostal;
        public String dirCpostal
        {
            get { return _dirCpostal; }
            set
            {
                _dirCpostal = value;
            }
        }

        private String _dirLocalidad;
        public String dirLocalidad
        {
            get { return _dirLocalidad; }
            set
            {
                _dirLocalidad = value;
            }
        }

        private String _dirProvincia;
        public String dirProvincia
        {
            get { return _dirProvincia; }
            set
            {
                _dirProvincia = value;
            }
        }

        private String _dirTelefono;
        public String dirTelefono
        {
            get { return _dirTelefono; }
            set
            {
                _dirTelefono = value;
            }
        }
    }
}
