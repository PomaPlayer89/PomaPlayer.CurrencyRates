namespace PomaPlayer.CurrencyRates.WebApi.Features.DtoModels;

public sealed record SaveReportsRequestDto
{
    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }
}
