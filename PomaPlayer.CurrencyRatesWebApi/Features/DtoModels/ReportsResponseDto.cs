namespace PomaPlayer.CurrencyRates.WebApi.Features.DtoModels;

public sealed record ReportsResponseDto
{
    public string Code { get; set; }

    public decimal Avg { get; set; }

    public decimal Max { get; set; }

    public decimal Min { get; set; }
}
