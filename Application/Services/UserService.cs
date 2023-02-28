using Application.Interfaces;
using Domain.Models;

namespace Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<User> GetUserById(Guid id)
    {
        return await _repository.GetUserById(id);
    }

    public async Task<IEnumerable<User>> GetUsersByAge(int age)
    {
        var users = await _repository.GetUsersByAge(age);
        return users;
    }

    public async Task<IEnumerable<User>>? GetUsersByCountry(string country)
    {
        var users = await _repository.GetUsersByCountry(country.Trim().ToUpper());
        if (users != null)
        {
            return users;
        }
        return null;
    }

    public async Task<IEnumerable<User>> GetUsersByName(string name)
    {
        var users = await _repository.GetUsersByName(name.Trim().ToLower());
        if (users != null)
        {
            return users;
        }
        return null;
    }

    public async Task<IEnumerable<User>> GetUsersByPrefixName(string name)
    {
        return await _repository.GetUsersByPrefixName(name.Trim().ToLower());
    }

    public Task Remove(Guid id)
    {
        return _repository.Remove(id);
    }
}
