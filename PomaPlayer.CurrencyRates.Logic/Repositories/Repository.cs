using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PomaPlayer.CurrencyRates.Cron.DtoModels;
using PomaPlayer.CurrencyRates.Logic.Interfaces.Repositories;
using PomaPlayer.CurrencyRates.Storage.Database;
using PomaPlayer.CurrencyRates.Storage.Models;

namespace PomaPlayer.CurrencyRates.Logic.Repositories;

public sealed class Repository : IRepository
{
    private readonly IMapper _mapper;

    public Repository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ReportDto[]> GetReportAsync(DataContext dataContext, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await dataContext.Set<ReportDaily>()
            .AsNoTracking()
            .Where(x => x.Date >= start && x.Date <= end)
            .GroupBy(x => x.Code)
            .Select(x => new ReportDto
            {
                Code = x.Key,
                Avg = x.Average(e => e.Rate),
                Max = x.Max(e => e.Rate),
                Min = x.Min(e => e.Rate)
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task SaveReportsAsync(DataContext dataContext, ReportDailyDto report, CancellationToken cancellationToken = default)
    {
        if (!dataContext.Set<ReportDaily>().Any(x => x.Date == report.Date))
        {
            var reportDaily = _mapper.Map<ReportDaily[]>(report.Reports);

            dataContext.Set<ReportDaily>()
                .AddRange(reportDaily);
        }
    }
}
