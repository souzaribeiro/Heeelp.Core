﻿// ==============================================================================================================
// Microsoft patterns & practices
// CQRS Journey project
// ==============================================================================================================
// ©2012 Microsoft. All rights reserved. Certain content used with permission from contributors
// http://go.microsoft.com/fwlink/p/?LinkID=258575
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// ==============================================================================================================

namespace Heeelp.Core.Infrastructure.Sql.Database
{
    using System.Linq;
    using System;
    using System.Data.Entity;
    using Heeelp.Core.Infrastructure.Messaging;
    using Heeelp.Core.Infrastructure.Database;
    using System.Collections;
    using System.Collections.Generic;
    public class SqlDataContext<T> : IDataContext<T> where T : class, IAggregateRoot
    {
        private readonly IEventBus eventBus;
        private readonly DbContext context;

        public SqlDataContext(Func<DbContext> contextFactory, IEventBus eventBus)
        {
            this.eventBus = eventBus;
            this.context = contextFactory.Invoke();
        }

        public T Find(Guid id)
        {
            return this.context.Set<T>().Find(id);
        }
        public IEnumerable<T> List(Func<T, bool> condition)
        {
            return this.context.Set<T>().Where(condition);
        }
        public T Get(Func<T, bool> condition)
        {
            return this.context.Set<T>().Where(condition).FirstOrDefault();
        }
        public void Save(T aggregateRoot)
        {
            var entry = this.context.Entry(aggregateRoot);

            if (entry.State == EntityState.Detached)
                this.context.Set<T>().Add(aggregateRoot);

            // Can't have transactions across storage and message bus.
            this.context.SaveChanges();

            var eventPublisher = aggregateRoot as IEventPublisher;
            if (eventPublisher != null)
                this.eventBus.Publish(eventPublisher.Events);
        }

        public void Update(T aggregateRoot)
        {
            this.context.Set<T>().Attach(aggregateRoot);
            var entry = this.context.Entry(aggregateRoot);
            entry.State = EntityState.Modified;
            //entry.CurrentValues.SetValues(aggregateRoot); 
            this.context.SaveChanges();

            var eventPublisher = aggregateRoot as IEventPublisher;
            if (eventPublisher != null)
                this.eventBus.Publish(eventPublisher.Events);
        }
        public void AddEventToBus(T aggregateRoot)
        {
            var eventPublisher = aggregateRoot as IEventPublisher;
            if (eventPublisher != null)
                this.eventBus.Publish(eventPublisher.Events);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlDataContext()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }
    }
}
