using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Mail
    {
        private String _smtp; public String smtp { get { return _smtp; } set { _smtp = value; } }
        private String _remitente; public String remitente { get { return _remitente; } set { _remitente = value; } }
        private String _puerto; public String puerto { get { return _puerto; } set { _puerto = value; } }
        private String _password; public String password { get { return _password; } set { _password = value; } }
        private String _seguro; public String seguro { get { return _seguro; } set { _seguro = value; } }
        private String _asunto; public String asunto { get { return _asunto; } set { _asunto = value; } }
        private String _mensaje; public String mensaje { get { return _mensaje; } set { _mensaje = value; } }
        private String _para; public String para { get { return _para; } set { _para = value; } }
        private String _enviaMail; public String enviaMail { get { return _enviaMail; } set { _enviaMail = value; } }
        private String _tipo; public String tipo { get { return _tipo; } set { _tipo = value; } }
        private String _tipoMail; public String tipoMail { get { return _tipoMail; } set { _tipoMail = value; } }
        private String _circuito; public String circuito { get { return _circuito; } set { _circuito = value; } }
        private String _html; public String html { get { return _html; } set { _html = value; } }
        private String _encuesta; public String encuesta { get { return _encuesta; } set { _encuesta = value; } }
        private String _cuestionario; public String cuestionario { get { return _cuestionario; } set { _cuestionario = value; } }
        private String _copiaMail; public String copiaMail { get { return _copiaMail; } set { _copiaMail = value; } }

    }
}
