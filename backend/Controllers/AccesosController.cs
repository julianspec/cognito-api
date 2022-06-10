using api.DbFuntions;
using api.Global;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/accesos")]

    public class AccesosController : ControllerBase
    {
        [HttpGet("sp_accesos/{usuario}")]
        public string GetRazonSocial(string usuario)
        {
            ModGlb.Query = @"execute sp_accesos @i_operacion = 'DOCUMENTOS', @i_usuario = '"+ usuario + "'";
            return MethodClass.exec(ModGlb.Query);
        }
    }
}
