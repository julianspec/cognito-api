using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Denuncia
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

        private String _tarjCuenta;
        public String tarjCuenta
        {
            get { return _tarjCuenta; }
            set
            {
                _tarjCuenta = value;
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
    }
}
