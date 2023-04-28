using PocGPT.Core;
using PocGPT.Core.InterfaceService;

namespace PocGPT.Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ITicketsService, TicketService>();

            return services;

        }
    }
}
