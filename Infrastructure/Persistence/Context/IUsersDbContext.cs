using Domain.Models;
using Infrastructure.Persistence.DataStructure;

namespace Infrastructure.Persistence.Context;
public interface IUsersDbContext
{
    Dictionary<Guid, User> UserById { get; }
    Dictionary<string, Dictionary<Guid, User>> UsersByCountry { get; }
    Dictionary<int, Dictionary<Guid, User>> UsersByAge { get; }
    Dictionary<string, Dictionary<Guid, User>> UsersByName { get; }
    public Trie UsersByPrefix { get; }

    void Remove(Guid id);
}