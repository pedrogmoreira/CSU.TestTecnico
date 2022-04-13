using CSU.EtapaTecnica.Domain.DTO;
using CSU.EtapaTecnica.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace CSU.EtapaTecnica.Exercicio_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        [HttpGet("NotasFicais/{mes}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de Notas Fiscais", typeof(IEnumerable<NotaFiscalDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public ActionResult GetNotasFiscais(int mes)
        {
            if (mes < 1 || mes > 12)
            {
                return BadRequest("O mês informado está inválido.");
            }

            var result = _notaFiscalService.GetNotasFiscais(mes);

            if (result.Count() == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
