using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Producto
    {
        private String N_prod; public string prod { get { return N_prod; } set { N_prod = value; } }
        private String S_descripcion; public string descripcion { get { return S_descripcion; } set { S_descripcion = value; } }
        private String N_nivel; public string nivel { get { return N_nivel; } set { N_nivel = value; } }
        private String N_padre; public string padre { get { return N_padre; } set { N_padre = value; } }
        private String S_estado; public string estado { get { return S_estado; } set { S_estado = value; } }
        private String S_referencia; public string referencia { get { return S_referencia; } set { S_referencia = value; } }
        private String S_prefijo; public string prefijo { get { return S_prefijo; } set { S_prefijo = value; } }
        private String S_obs; public string obs { get { return S_obs; } set { S_obs = value; } }
        private String S_usuario; public string usuario { get { return S_usuario; } set { S_usuario = value; } }
        private String D_falta; public string falta { get { return D_falta; } set { D_falta = value; } }
        private String D_fmodif; public string fmodif { get { return D_fmodif; } set { D_fmodif = value; } }
        private String N_producto_bcra; public string producto_bcra { get { return N_producto_bcra; } set { N_producto_bcra = value; } }
        private String S_padreDesc; public string padreDesc { get { return S_padreDesc; } set { S_padreDesc = value; } }
    }
}
