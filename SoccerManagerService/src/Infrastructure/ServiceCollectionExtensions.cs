namespace Soccer.Platform.Infrastructure.Extensions
{
    using System;
    using Framework.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Soccer.Infrastructure;
    using Soccer.Infrastructure.Repository.RDBRepository;
    using Soccer.Infrastructure.Repository.RDBRepository.DbContexts;
    using Soccer.Platform.Infrastructure.Core;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public static class ServiceCollectionExtensions
    {
        public static void AddCore(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped<IDispatcher, Dispatcher>();

            services.AddScoped<ICommandSender, CommandSender>();

            services.AddScoped<IReadWriteRepository, ReadWriteRepository<DbContext>>();
            services.AddScoped<IReadOnlyRepository, ReadOnlyRepository<DbContext>>();
            services.AddScoped<ISecurityContext, SecurityContext>();

            services.AddScoped<DbContext, SoccerDbContext>();
        }
    }
}
