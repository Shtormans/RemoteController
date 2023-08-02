using Microsoft.EntityFrameworkCore;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.Entities;

namespace RemoteControllerApi.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByEmailWithRoomsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<User>()
            .Include(u => u.Rooms)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
