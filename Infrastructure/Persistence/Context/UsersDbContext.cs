using CsvHelper;
using Domain.Models;
using Infrastructure.Persistence.DataStructure;
using System.Globalization;

using System.Reflection;

namespace Infrastructure.Persistence.Context;
public class UsersDbContext : IUsersDbContext
{
    public Dictionary<Guid, User> UserById { get; } = new();
    public Dictionary<string, Dictionary<Guid, User>> UsersByCountry { get; } = new();
    public Dictionary<int, Dictionary<Guid, User>> UsersByAge { get; } = new();
    public Dictionary<string, Dictionary<Guid, User>> UsersByName { get; } = new();
    public Trie UsersByPrefix { get; } = new();

    public UsersDbContext()
    {
        InitDb();
    }

    private void InitDb()
    {

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data.csv";
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, culture: CultureInfo.GetCultureInfo("he-IL")))
        {
            var records = csv.GetRecords<User>();
            foreach (var record in records)
            {
                InitUserById(record);
                InitUsersByCountry(record);
                InitUsersByAge(record);
                InitUsersByName(record);
                InitUsersByPrefix(record);
            }
        }
    }

    private void InitUsersByPrefix(User record)
    {
        var name = record.Name.Trim().ToLower();
        foreach (var word in name.Split(" "))
        {
            UsersByPrefix.Insert(word, record);
        }
    }

    private void InitUsersByName(User record)
    {
        var name = record.Name.Trim().ToLower();
        SetUserByName(record, name);
        foreach (var key in name.Split(" "))
        {
            SetUserByName(record, key);
        }

    }

    private void SetUserByName(User record, string name)
    {
        if (!UsersByName.ContainsKey(name))
        {
            UsersByName[name] = new();
        }
        UsersByName[name].TryAdd(record.Id, record);
    }

    private void InitUsersByAge(User record)
    {
        if (!UsersByAge.ContainsKey(record.Age))
        {
            UsersByAge[record.Age] = new();
        }
        UsersByAge[record.Age].TryAdd(record.Id, record);
    }

    private void InitUserById(User record)
    {
        UserById.Add(record.Id, record);
    }

    private void InitUsersByCountry(User record)
    {
        if (!UsersByCountry.ContainsKey(record.Country))
        {
            UsersByCountry[record.Country] = new();
        }
        UsersByCountry[record.Country].TryAdd(record.Id, record);
    }

    public void Remove(Guid id)
    {
        var user = UserById[id];
        UserById.Remove(id);
        UsersByCountry[user.Country].Remove(id);
        UsersByAge[user.Age].Remove(id);
        UsersByName[user.Name.ToLower()].Remove(id);
        foreach (var name in user.Name.ToLower().Split(" "))
        {
            UsersByName[name].Remove(id);
            UsersByPrefix.Remove(name, id);
        }
    }
}
