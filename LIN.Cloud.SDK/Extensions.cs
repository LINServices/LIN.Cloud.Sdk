using Microsoft.Extensions.DependencyInjection;

namespace LIN.Cloud.SDK;

public static class Extensions
{
    public static IServiceCollection AddCloudSDK(this IServiceCollection services, string key, int minutes = 10)
    {

        // Opciones por defecto.
        var options = new Options()
        {
            Key = key,
            Url = "https://cloud.api.linplatform.com",
            MinutesTimeOut = minutes
        };

        // Agregar servicios al contenedor.
        services.AddSingleton(options);
        services.AddSingleton<Client>();

        return services;
    }
}