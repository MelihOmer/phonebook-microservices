using MassTransit;
using ReportService.API.Contracts.Events;

namespace ReportService.API.Consumers
{
    public class ReportRequestedConsumer : IConsumer<ReportRequestEvent>
    {
        
        public Task Consume(ConsumeContext<ReportRequestEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
