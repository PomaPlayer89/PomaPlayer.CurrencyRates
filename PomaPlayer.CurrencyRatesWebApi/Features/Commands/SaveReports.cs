using MediatR;
using Microsoft.AspNetCore.Mvc;
using PomaPlayer.CurrencyRates.Logic.Interfaces.Services;
using PomaPlayer.CurrencyRates.Storage.Database;
using PomaPlayer.CurrencyRates.WebApi.Features.DtoModels;
using System.ComponentModel.DataAnnotations;

namespace PomaPlayer.CurrencyRates.WebApi.Features.Commands;

public sealed class SaveReportsCommand : IRequest
{
    [Required]
    [FromBody]
    public SaveReportsRequestDto Report { get; init; }
}

public sealed class SaveReportsCommandHandler : IRequestHandler<SaveReportsCommand>
{
    private readonly DataContext _dataContext;
    private readonly ICronReportService _cronReportService;

    public SaveReportsCommandHandler(DataContext dataContext, ICronReportService cronReportService)
    {
        _dataContext = dataContext;
        _cronReportService = cronReportService;
    }

    public async Task Handle(SaveReportsCommand request, CancellationToken cancellationToken)
    {
        await _cronReportService.SaveReportsAsync(_dataContext, request.Report.StartDate, request.Report.EndDate, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return;
    }
}