using api.DbFuntions;
using api.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace api.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasClientesController : ControllerBase
    {
        [HttpGet("clientes")]
        public string Get()
        {     
            return MethodClass.exec("select * from CuentasClientes"); 
        }

        [HttpGet("sp_busca_apellido/{razonsocial}")]
        public string GetRazonSocial(string razonsocial)
        {
            //GRUNBERG GRACIELA HILDA
            ModGlb.Query = @"exec sp_busca_apellido @i_apell = '"+ razonsocial + "'";

            return MethodClass.exec(ModGlb.Query);
        }
    }
}
