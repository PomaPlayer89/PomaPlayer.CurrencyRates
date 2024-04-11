using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}
