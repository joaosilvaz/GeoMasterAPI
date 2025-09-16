using GeoMaster.Application.Abstractions;
using GeoMaster.Application.Factory;
using GeoMaster.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeoMaster.Api.Controllers;

/// <summary>
/// Endpoints de cálculos geométricos (2D e 3D).
/// </summary>
/// <remarks>
/// Formas suportadas nesta versão:
/// - 2D: <c>circulo</c>, <c>retangulo</c>
/// - 3D: <c>esfera</c> (para <c>/area-superficial</c> e <c>/volume</c>)
/// 
/// Observações:
/// - O endpoint <c>/area</c> é para formas 2D. Para esferas use <c>/area-superficial</c>.
/// - Quando a operação não for suportada para a forma, a API retorna <c>422 UnprocessableEntity</c>.
/// </remarks>
[ApiController]
[Route("api/v1/calculos")]
[Produces("application/json")]
public class CalculosController : BaseCalculosController
{
    /// <inheritdoc />
    public CalculosController(ICalculadoraService calculadoraService, IFormaFactory formaFactory)
      : base(calculadoraService, formaFactory) { }

    /// <summary>
    /// Calcula a <b>área</b> de uma forma geométrica 2D.
    /// </summary>
    /// <param name="request">Tipo da forma e suas propriedades.</param>
    /// <returns>Objeto com tipo da forma, resultado, operação e data do cálculo.</returns>
    /// <remarks>
    /// Exemplos de requisição:
    ///
    /// **Círculo**
    /// {
    ///   "tipoForma": "circulo",
    ///   "propriedades": { "raio": 10 }
    /// }
    ///
    /// **Retângulo**
    /// {
    ///   "tipoForma": "retangulo",
    ///   "propriedades": { "largura": 5, "altura": 8 }
    /// }
    ///
    /// <b>Observação:</b> para esferas use o endpoint <c>/area-superficial</c>.
    /// </remarks>
    /// <response code="200">Cálculo realizado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    /// <response code="422">Operação não suportada para o tipo de forma.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost("area")]
    [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CalcularArea([FromBody] FormaRequestDto request)
      => ExecutarCalculo(request, _calculadoraService.CalcularArea, "area", "circulo, retangulo");

    /// <summary>
    /// Calcula o <b>perímetro</b> de uma forma geométrica 2D.
    /// </summary>
    /// <param name="request">Tipo da forma e suas propriedades.</param>
    /// <remarks>
    /// **Círculo**
    /// {
    ///   "tipoForma": "circulo",
    ///   "propriedades": { "raio": 4 }
    /// }
    ///
    /// **Retângulo**
    /// {
    ///   "tipoForma": "retangulo",
    ///   "propriedades": { "largura": 8, "altura": 12 }
    /// }
    /// </remarks>
    /// <response code="200">Cálculo realizado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    /// <response code="422">Operação não suportada para o tipo de forma.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost("perimetro")]
    [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CalcularPerimetro([FromBody] FormaRequestDto request)
      => ExecutarCalculo(request, _calculadoraService.CalcularPerimetro, "perimetro", "circulo, retangulo");

    /// <summary>
    /// Calcula o <b>volume</b> de uma forma geométrica 3D.
    /// </summary>
    /// <param name="request">Tipo da forma e suas propriedades.</param>
    /// <remarks>
    /// **Esfera**
    /// {
    ///   "tipoForma": "esfera",
    ///   "propriedades": { "raio": 8 }
    /// }
    /// </remarks>
    /// <response code="200">Cálculo realizado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    /// <response code="422">Operação não suportada para o tipo de forma.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost("volume")]
    [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CalcularVolume([FromBody] FormaRequestDto request)
      => ExecutarCalculo(request, _calculadoraService.CalcularVolume, "volume", "esfera");

    /// <summary>
    /// Calcula a <b>área superficial</b> de uma forma geométrica 3D.
    /// </summary>
    /// <param name="request">Tipo da forma e suas propriedades.</param>
    /// <remarks>
    /// **Esfera**
    /// {
    ///   "tipoForma": "esfera",
    ///   "propriedades": { "raio": 8 }
    /// }
    /// </remarks>
    /// <response code="200">Cálculo realizado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    /// <response code="422">Operação não suportada para o tipo de forma.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost("area-superficial")]
    [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CalcularAreaSuperficial([FromBody] FormaRequestDto request)
      => ExecutarCalculo(request, _calculadoraService.CalcularAreaSuperficial, "area superficial", "esfera");
}
