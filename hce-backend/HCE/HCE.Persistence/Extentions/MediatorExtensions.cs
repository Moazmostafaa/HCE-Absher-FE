using HCE.Domain.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Extentions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<EntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents.Clear());

            #region Excute all Tasks in multithreads at the same time
            // This caused an issue with db context NOT A GOOD IDEA / multiple request => same context instance

            //var tasks = domainEvents
            //    .Select(async (domainEvent) =>
            //    {
            //        await mediator.Publish(domainEvent);
            //    });

            //await Task.WhenAll(tasks); 
            #endregion

            // This excutes domain events based on their insertion order
            foreach (var @event in domainEvents)
            {
                await mediator.Publish(@event);
            }
        }
    }
}
