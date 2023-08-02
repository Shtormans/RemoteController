using Microsoft.EntityFrameworkCore;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.Entities;

namespace RemoteControllerApi.Infrastructure.Repositories;

internal class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

	public RoomRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
    }

    public void Add(Room room)
    {
        _dbContext.Set<Room>().Add(room);
    }

    public async Task<Room?> GetByIdAsync(string roomNumber, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<Room>()
            .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber, cancellationToken);
    }

    public async Task<Room?> GetByIdWithUserAsync(string roomNumber, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<Room>()
            .Include(r => r.UserId)
            .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber, cancellationToken);
    }
}
