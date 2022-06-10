using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class BaseDatos
    {
        private String _resultado;
        public string resultado
        {
            get { return _resultado; }
            set
            {
                _resultado = value;
            }
        }

        private String _mensaje;
        public string mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
            }
        }

        private String _filas;
        public string filas
        {
            get { return _filas; }
            set
            {
                _filas = value;
            }
        }
    }
}
