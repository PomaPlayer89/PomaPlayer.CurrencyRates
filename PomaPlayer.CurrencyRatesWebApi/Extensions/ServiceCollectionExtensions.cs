using PomaPlayer.CurrencyRates.Logic.Extensions;

namespace PomaPlayer.CurrencyRates.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddLogicServices();
    }
}
