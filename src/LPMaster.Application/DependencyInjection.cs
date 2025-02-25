using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LPMaster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddLPMasterApplication(this IServiceCollection services) => services
        .AddAutoMapper(Assembly.GetExecutingAssembly())
        .AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        })
        .AddFluentValidation([Assembly.GetExecutingAssembly()]);
}
