using Application.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repository;
public class UserRepository : IUserRepository
{
    private readonly IUsersDbContext _context;

    public UserRepository(IUsersDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserById(Guid id)
    {
        if (_context.UserById.TryGetValue(id, out User user))
        {
            return user;
        }
        ;
        return null;
    }

    public async Task<IEnumerable<User>> GetUsersByAge(int age)
    {
        if (_context.UsersByAge.TryGetValue(age, out var users))
        {
            return await Task.FromResult<List<User>>(users.Values.ToList());
        }
        return await Task.FromResult<List<User>>(null);
    }

    public Task<IEnumerable<User>>? GetUsersByCountry(string country)
    {

        if (_context.UsersByCountry.TryGetValue(country, out var users))
        {
            return Task.Run(() => users.Values.AsEnumerable());
        }
        return Task.FromResult<IEnumerable<User>>(null);
    }

    public Task<IEnumerable<User>> GetUsersByName(string name)
    {
        if (_context.UsersByName.TryGetValue(name, out var users))
        {
            return Task.FromResult(users.Values.AsEnumerable());
        }
        return Task.FromResult<IEnumerable<User>>(null);
    }

    public async Task<IEnumerable<User>> GetUsersByPrefixName(string name)
    {
        return await Task.FromResult(_context.UsersByPrefix.StartWith(name));
    }

    public Task Remove(Guid id)
    {

        _context.Remove(id);

        return Task.CompletedTask;
    }
}
