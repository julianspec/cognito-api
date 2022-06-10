using api.DbFuntions;
using api.Global;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[ApiController]
	[Route("api/pendientes")]
	public class PendientesController : ControllerBase
	{
	
		[HttpGet("sp_pendiente")]
		public string Get()
		{
			ModGlb.Query = @"execute sp_pendiente
			@i_operacion = 'SELECT',
			@i_tipo = 0,
			@i_sucursal = 5,
			@i_usuario='christian',
			@i_otros_usuarios ='N',
			@i_domicilio ='N',
			@i_ver_externos ='N',
			@i_importe_devolucion ='N',
			@i_destino='grilla'";
			return MethodClass.exec(ModGlb.Query);
		}
	}
}







