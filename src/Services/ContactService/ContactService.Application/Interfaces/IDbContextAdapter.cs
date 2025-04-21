namespace ContactService.Application.Interfaces
{
    public interface IDbContextAdapter
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
