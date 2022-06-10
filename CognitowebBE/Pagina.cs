using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Pagina
    {

        private Boolean _cargarSesion;
        public Boolean cargarSesion
        {
            get { return _cargarSesion; }
            set
            {
                _cargarSesion = value;
            }
        }

        private String _resultado;
        public string resultado
        {
            get { return _resultado; }
            set
            {
                _resultado = value;
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

        private String _historico;
        public string historico
        {
            get {
                if (_historico == null)
                {
                    _historico = "N";
                }
                if (_historico != "S")
                {
                    _historico = "N";
                }
                return _historico; }
            set
            {
                _historico = value;
            }
        }

        private String _modo;
        public string modo
        {
            get { return _modo; }
            set
            {
                _modo = value;
            }
        }

    }
}
