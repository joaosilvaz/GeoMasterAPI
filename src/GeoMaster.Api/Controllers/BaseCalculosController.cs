using GeoMaster.Application.Abstractions;
using GeoMaster.Application.Factory;
using GeoMaster.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeoMaster.Api.Controllers;

public abstract class BaseCalculosController : ControllerBase
{
    protected readonly ICalculadoraService _calculadoraService;
    protected readonly IFormaFactory _formaFactory;

    protected BaseCalculosController(ICalculadoraService calc, IFormaFactory factory)
    {
        _calculadoraService = calc;
        _formaFactory = factory;
    }

    protected IActionResult ExecutarCalculo(
        FormaRequestDto? request,                     // <- nullable
        Func<object, double> operacao,
        string nomeOperacao,
        string tiposSuportados)
    {
        try
        {
            if (request is null)
                return BadRequest(new ProblemDetails
                {
                    Title = "Dados inválidos",
                    Detail = "Payload não pode ser nulo",
                    Status = StatusCodes.Status400BadRequest
                });

            if (string.IsNullOrWhiteSpace(request.TipoForma))
                return BadRequest(new ProblemDetails
                {
                    Title = "Dados inválidos",
                    Detail = "Campo 'tipoForma' é obrigatório",
                    Status = StatusCodes.Status400BadRequest
                });

            if (request.Propriedades is null)
                return BadRequest(new ProblemDetails
                {
                    Title = "Dados inválidos",
                    Detail = "Campo 'propriedades' é obrigatório",
                    Status = StatusCodes.Status400BadRequest
                });


            var shape = _formaFactory.CriarForma(request.TipoForma!, request.Propriedades!);
            var valor = operacao(shape);

            var opEnum = nomeOperacao.ToLowerInvariant() switch
            {
                "area" => TipoOperacao.Area,
                "perimetro" => TipoOperacao.Perimetro,
                "volume" => TipoOperacao.Volume,
                "area superficial" => TipoOperacao.AreaSuperficial,
                _ => TipoOperacao.Area
            };

            return Ok(ResultadoCalculoDto.From(request.TipoForma!, valor, opEnum));
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
        // se sua CalculadoraService lançar InvalidOperationException, trate junto:
        // catch (Exception ex) when (ex is NotSupportedException || ex is InvalidOperationException)
        catch (NotSupportedException ex)
        {
            return UnprocessableEntity(new ProblemDetails
            {
                Title = "Operação não suportada",
                Detail = ex.Message + $" Tipos suportados: {tiposSuportados}.",
                Status = StatusCodes.Status422UnprocessableEntity
            });
        }
        catch (Exception ex)
        {
            // em DEV, isso ajuda a debugar:
            // return StatusCode(500, new { error = ex.ToString() });

            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado ao processar a solicitação",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}

