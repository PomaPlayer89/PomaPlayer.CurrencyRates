using Microsoft.EntityFrameworkCore;
using PomaPlayer.CurrencyRates.Logic.DtoModels;
using PomaPlayer.CurrencyRates.Logic.Interfaces.Repositories;
using PomaPlayer.CurrencyRates.Storage.Database;
using PomaPlayer.CurrencyRates.Storage.Models;

namespace PomaPlayer.CurrencyRates.Logic.Repositories;

public sealed class Repository : IRepository
{
    public Repository()
    {

    }

    public async Task<IReadOnlyCollection<ReportDto>> GetReport(DataContext dataContext, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
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

    public async Task SaveReports(DataContext dataContext, ReportDaily report, CancellationToken cancellationToken = default)
    {
        if (!dataContext.Set<ReportDaily>().Any(x => x.Date == report.Date))
        {
            //dataContext.Set<ReportDaily>()
            //    .AddRange(report.Adapt<ReportDaily[]>());
            //
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
