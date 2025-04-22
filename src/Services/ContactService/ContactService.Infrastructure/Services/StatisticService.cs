using ContactService.Application.DTOs.Statistic;
using ContactService.Application.Interfaces.Services;
using ContactService.Domain.Enums;
using ContactService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly AppDbContext _context;

        public StatisticService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationStatisticDto>> GetLocationStatisticAsync()
        {
            var query = await _context.Contacts
                .AsNoTracking()
                .Where(c => c.ContactInformations.Any(ci => ci.Type == ContactInfoType.Location))
                .Select(c => new
                {
                    ContactId = c.Id,
                    Location = c.ContactInformations.FirstOrDefault(ci => ci.Type == ContactInfoType.Location)!.InfoContent
                })
                .GroupBy(x => x.Location)
                .Select(g => new LocationStatisticDto
                {
                    Location = g.Key,
                    PersonCount = g.Count(),
                    PhoneCount = _context.ContactInformation
                    .Count(ci => ci.Type == ContactInfoType.Phone && g.Select(x => x.ContactId).Contains(ci.ContactId))
                })
                .ToListAsync();
            return query;
        }
    }
}
