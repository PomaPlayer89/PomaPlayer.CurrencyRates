using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PomaPlayer.CurrencyRates.Logic.Interfaces.Services;
using PomaPlayer.CurrencyRates.Storage.Database;
using PomaPlayer.CurrencyRates.WebApi.Features.DtoModels;
using System.ComponentModel.DataAnnotations;

namespace PomaPlayer.CurrencyRates.WebApi.Features.Queries;

public sealed class GetReportsQuery : IRequest<ReportsResponseDto[]>
{
    [Required]
    [FromQuery]
    public DateOnly StartDate { get; init; }

    [Required]
    [FromQuery]
    public DateOnly EndDate { get; init; }

    [Required]
    [FromBody]
    public string[] Codes { get; init; }
}

public sealed class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, ReportsResponseDto[]>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    private readonly ICronReportService _cronReportService;

    public GetReportsQueryHandler(
        IMapper mapper,
        DataContext dataContext,
        ICronReportService cronReportService)
    {
        _mapper = mapper;
        _dataContext = dataContext;
        _cronReportService = cronReportService;
    }

    public async Task<ReportsResponseDto[]> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var result = await _cronReportService.CalculateReportAsync(_dataContext, request.StartDate, request.EndDate, request.Codes, cancellationToken);
        var reports = _mapper.Map<ReportsResponseDto[]>(result);

        return reports;
    }
}
