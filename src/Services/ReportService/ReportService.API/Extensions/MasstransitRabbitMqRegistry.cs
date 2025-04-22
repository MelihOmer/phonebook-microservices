using MassTransit;
using ReportService.API.Consumers;

namespace ReportService.API.Extensions
{
    public static class MasstransitRabbitMqRegistry
    {

        public static void AddMasstransit(this IServiceCollection services)
        {
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ReportRequestedConsumer>();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("report-requested-event-queue", e =>
                    {
                        e.ConfigureConsumer<ReportRequestedConsumer>(ctx);
                    });
                });
            });
        }
    }
}
