using MassTransit;
using ReportService.API.Contracts.Events;
using ReportService.Application.Interfaces.Services;

namespace ReportService.API.Consumers
{
    public class ReportRequestedConsumer : IConsumer<ReportRequestEvent>
    {
        
        private readonly IReportService _reportService;

        public ReportRequestedConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<ReportRequestEvent> context)
        {
            await _reportService.PrepareReportAsync(context.Message.ReportId);
        }
    }
}
