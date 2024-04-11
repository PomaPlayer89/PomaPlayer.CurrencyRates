using MediatR;
using Microsoft.AspNetCore.Mvc;
using PomaPlayer.CurrencyRates.WebApi.Features.Commands;
using PomaPlayer.CurrencyRates.WebApi.Features.DtoModels;
using PomaPlayer.CurrencyRates.WebApi.Features.Queries;

namespace PomaPlayer.CurrencyRates.WebApi.Controllers;

[ApiController]
[Route("{controller}")]
public class ManageController : Controller
{
    private readonly IMediator _mediator;

    public ManageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(nameof(GetReports), Name = nameof(GetReports))]
    public async Task<ActionResult<ReportsResponseDto>> GetReports(GetReportsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }


    [HttpPost(nameof(SaveReports), Name = nameof(SaveReports))]
    public async Task<ActionResult> SaveReports(SaveReportsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
}
