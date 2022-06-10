using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class ControlProducto
    {
        private String N_tipoOperacion; public string tipoOperacion { get { return N_tipoOperacion; } set { N_tipoOperacion = value; } }
        private String N_producto; public string producto { get { return N_producto; } set { N_producto = value; } }
        private String N_subProducto; public string subProducto { get { return N_subProducto; } set { N_subProducto = value; } }
        private String N_motivo; public string motivo { get { return N_motivo; } set { N_motivo = value; } }
    }
}
