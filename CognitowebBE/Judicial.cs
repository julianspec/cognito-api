using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Judicial
    {
        private String S_prefijo; public string prefijo { get { return S_prefijo; } set { S_prefijo = value; } }
        private String N_id; public string id { get { return N_id; } set { N_id = value; } }
        //private String N_producto; public string producto { get { return N_producto; } set { N_producto = value; } }
        private String N_concepto; public string concepto { get { return N_concepto; } set { N_concepto = value; } }
        private String S_prefijo_original; public string prefijo_original { get { return S_prefijo_original; } set { S_prefijo_original = value; } }
        private String N_id_original; public string id_original { get { return N_id_original; } set { N_id_original = value; } }
        private String N_monto_original; public string monto_original { get { return N_monto_original; } set { N_monto_original = value; } }
        private String N_monto_final; public string monto_final { get { return N_monto_final; } set { N_monto_final = value; } }
        private String N_monto_costas; public string monto_costas { get { return N_monto_costas; } set { N_monto_costas = value; } }
        private String S_fec_finalizacion; public string fec_finalizacion { get { return S_fec_finalizacion; } set { S_fec_finalizacion = value; } }
        private String N_estado_judicial; public string estado_judicial { get { return N_estado_judicial; } set { N_estado_judicial = value; } }
        private String S_nro_expediente; public string nro_expediente { get { return S_nro_expediente; } set { S_nro_expediente = value; } }
        private String N_juridisccion; public string juridisccion { get { return N_juridisccion; } set { N_juridisccion = value; } }
        private String N_localidad; public string localidad { get { return N_localidad; } set { N_localidad = value; } }
    }
}
