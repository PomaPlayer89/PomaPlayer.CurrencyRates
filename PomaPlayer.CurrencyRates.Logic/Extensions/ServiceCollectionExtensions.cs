using Microsoft.Extensions.DependencyInjection;
using PomaPlayer.CurrencyRates.Logic.Interfaces.Repositories;
using PomaPlayer.CurrencyRates.Logic.Repositories;

namespace PomaPlayer.CurrencyRates.Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLogicServices(this IServiceCollection services)
    {
        services.AddSingleton<IRepository, Repository>();
    }
}
