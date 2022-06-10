using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class TarjetaDebito
    {
        private String S_prefijo; public string prefijo { get { return S_prefijo; } set { S_prefijo = value; } }
        private String N_id; public string id { get { return N_id; } set { N_id = value; } }
        private String N_registro; public string registro { get { return N_registro; } set { N_registro = value; } }
        private String S_nro_tarjeta; public string nro_tarjeta { get { return S_nro_tarjeta; } set { S_nro_tarjeta = value; } }
        private String S_nro_cuenta; public string nro_cuenta { get { return S_nro_cuenta; } set { S_nro_cuenta = value; } }
        private String S_tipo_cuenta; public string tipo_cuenta { get { return S_tipo_cuenta; } set { S_tipo_cuenta = value; } }
        private String S_tipo_cuenta_desc; public string tipo_cuenta_desc { get { return S_tipo_cuenta_desc; } set { S_tipo_cuenta_desc = value; } }
        private String S_calidad; public string calidad { get { return S_calidad; } set { S_calidad = value; } }
        private String S_calidad_desc; public string calidad_desc { get { return S_calidad_desc; } set { S_calidad_desc = value; } }
        private String S_cargo; public string cargo { get { return S_cargo; } set { S_cargo = value; } }
        private String S_categoria_cobis; public string categoria_cobis { get { return S_categoria_cobis; } set { S_categoria_cobis = value; } }
        private String S_categoria_cobis_desc; public string categoria_cobis_desc { get { return S_categoria_cobis_desc; } set { S_categoria_cobis_desc = value; } }
        private String S_clase; public string clase { get { return S_clase; } set { S_clase = value; } }
    }
}
