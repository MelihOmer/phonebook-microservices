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
            var query = await _context.ContactInformation
            .AsNoTracking()
            .Where(ci => ci.Type == ContactInfoType.Location)
            .GroupBy(ci => ci.InfoContent)
            .Select(g => new LocationStatisticDto
            {
                Location = g.Key,
                PersonCount = g.Select(ci => ci.ContactId).Distinct().Count(),
                PhoneCount = _context.ContactInformation
                    .Count(pi => pi.Type == ContactInfoType.Phone && g.Select(ci =>     ci.ContactId).Distinct().Contains(pi.ContactId))
            })
            .ToListAsync();

            return query;
        }
    }
}
