using GeoMaster.Application.Abstractions;
using GeoMaster.Application.Factory;
using GeoMaster.Domain.Abstractions;
using GeoMaster.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("forma-contida")]
    [ProducesResponseType(typeof(FormaContidaRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult ValidarFormaContida(
      [FromBody] FormaContidaRequestDto request)
    {
        try
        {
            IFormaContivel formaExterna = _formaFactory.CriarFormaContivel(request.FormaExterna.TipoForma, request.FormaExterna.Propriedades);
            IFormaContivel formaInterna = _formaFactory.CriarFormaContivel(request.FormaInterna.TipoForma, request.FormaInterna.Propriedades);

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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado ao processar a validação",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}