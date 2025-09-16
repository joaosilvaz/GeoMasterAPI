using GeoMaster.Application.Abstractions;
using GeoMaster.Application.Factory;
using GeoMaster.Domain.Abstractions;
using GeoMaster.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/v1/validacoes")]
[Produces("application/json")]
public class ValidacoesController : ControllerBase
{
    private readonly IValidacoesService _validacoesService;
    private readonly IFormaContivelFactory _formaFactory;

    public ValidacoesController(
        IValidacoesService validacoesService,
        IFormaContivelFactory formaFactory)
    {
        _validacoesService = validacoesService;
        _formaFactory = formaFactory;
    }

    /// <summary>
    /// Valida se uma forma geométrica (interna) pode ser contida dentro de outra (externa).
    /// </summary>
    /// <remarks>
    /// Tipos suportados: <c>circulo</c> (raio), <c>retangulo</c> (largura, altura)
    /// 
    /// Exemplo (círculo em retângulo):
    /// {
    ///   "formaExterna": { "tipoForma": "retangulo", "propriedades": { "largura": 10, "altura": 10 } },
    ///   "formaInterna": { "tipoForma": "circulo",   "propriedades": { "raio": 5 } }
    /// }
    /// </remarks>
    /// <response code="200">Validação realizada com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost("forma-contida")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult ValidarFormaContida([FromBody] FormaContidaRequestDto? request)
    {
        try
        {
            // --------- validações de payload (400) ---------
            if (request is null)
                return BadRequest(PD("Payload não pode ser nulo."));

            if (request.FormaExterna is null || request.FormaInterna is null)
                return BadRequest(PD("Campos 'formaExterna' e 'formaInterna' são obrigatórios."));

            if (string.IsNullOrWhiteSpace(request.FormaExterna.TipoForma) ||
                string.IsNullOrWhiteSpace(request.FormaInterna.TipoForma))
                return BadRequest(PD("Campos 'tipoForma' em externa e interna são obrigatórios."));

            if (request.FormaExterna.Propriedades is null || request.FormaExterna.Propriedades.Count == 0 ||
                request.FormaInterna.Propriedades is null || request.FormaInterna.Propriedades.Count == 0)
                return BadRequest(PD("Campos 'propriedades' de ambas as formas são obrigatórios."));

            if (request.FormaExterna.Propriedades.Any(kv => kv.Value <= 0) ||
                request.FormaInterna.Propriedades.Any(kv => kv.Value <= 0))
                return BadRequest(PD("Todas as propriedades devem ser números maiores que zero."));

            IFormaContivel formaExterna =
                _formaFactory.CriarFormaContivel(request.FormaExterna.TipoForma, request.FormaExterna.Propriedades);
            IFormaContivel formaInterna =
                _formaFactory.CriarFormaContivel(request.FormaInterna.TipoForma, request.FormaInterna.Propriedades);

            bool resultado = _validacoesService.ValidarFormaContida(formaExterna, formaInterna);
            return Ok(new { resultado });
        }
        catch (ArgumentException ex) 
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Dados de entrada inválidos",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado ao processar a validação",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    private static ProblemDetails PD(string detail) => new()
    {
        Title = "Dados inválidos",
        Detail = detail,
        Status = StatusCodes.Status400BadRequest
    };
}
