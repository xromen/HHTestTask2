using HHTestTask2.Domain;
using HHTestTask2.Infrastructure.Database;
using HHTestTask2.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Extensions
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection SetupUnitOfWork([NotNullAttribute] this IServiceCollection serviceCollection)
        {
            //TODO: Find a way to inject the repositories and share the same context without creating a instance.
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var scopeFactory = f.GetRequiredService<IServiceScopeFactory>();
                var context = f.GetService<ApplicationContext>();
                return new UnitOfWork(
                    context,
                    new TreeRepository(context.Trees),
                    new NodeRepository(context.Nodes),
                    new RequestRepository(context.Requests),
                    new JournalRepository(context.Journal)
                );
            });
            return serviceCollection;
        }
    }
}
