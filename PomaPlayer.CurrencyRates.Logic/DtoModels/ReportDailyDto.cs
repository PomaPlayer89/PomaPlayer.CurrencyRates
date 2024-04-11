namespace PomaPlayer.CurrencyRates.Logic.DtoModels;

public sealed record ReportDailyDto
{
    public DateOnly Date { get; init; }

    public IReadOnlyCollection<ReportDataDto> Reports { get; init; }
}
