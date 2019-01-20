using FuraFila.Repository.SQlite.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Repository.SQlite.DependencyInjection
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection WithSeed<TContext>(this IServiceCollection serviceCollection, Action<TContext> seedAction)
            where TContext : DbContext
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            if (seedAction == null)
                throw new ArgumentNullException(nameof(seedAction));

            seedAction(serviceCollection.BuildServiceProvider().GetService<TContext>());

            return serviceCollection;
        }
    }
}
