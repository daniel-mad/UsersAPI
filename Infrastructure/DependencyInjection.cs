using Application.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUsersDbContext, UsersDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
