using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IUserRepository
{
    Task<User> GetUserById(Guid id);
    Task<IEnumerable<User?>>? GetUsersByCountry(string country);
    Task<IEnumerable<User>> GetUsersByAge(int age);

    Task<IEnumerable<User>> GetUsersByName(string name);
    Task<IEnumerable<User>> GetUsersByPrefixName(string name);

    Task Remove(Guid id);
}
