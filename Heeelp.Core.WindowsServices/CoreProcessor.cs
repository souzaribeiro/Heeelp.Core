// ==============================================================================================================
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

namespace WorkerRoleCommandProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using Microsoft.Practices.Unity;
    using Heeelp.Core.Infrastructure;
    using Heeelp.Core.Infrastructure.Serialization;
    using Heeelp.Core.Infrastructure.Messaging;
    //using Heeelp.Core.Infrastructure.SQL.Messaging;
    using Heeelp.Core.Infrastructure.Messaging.Handling;
    // using Heeelp.Core.DataBase.SQL.Messaging.Handling;
    // using Heeelp.Core.DataBase.SQL.MessageLog; 


    using Heeelp.Core.ProcessManager.CommandHandlers.Expertise;
    using Heeelp.Core.Infrastructure.Sql.Messaging.Handling;
    using Heeelp.Core.Infrastructure.Sql.Messaging;
    using Heeelp.Core.Infrastructure.Sql.MessageLog;
    using Heeelp.Core.Infrastructure.Database;
    using Heeelp.Core.Infrastructure.Sql.Database;
    using Heeelp.Core.Domain;
    using Heeelp.Core.DataBase.SQL;
    using Heeelp.Core.ProcessManager.EventHandlers.Expertise;
    using Heeelp.Core.Infrastructure.Sql.EventSourcing;
    using Heeelp.Core.Infrastructure.EventSourcing;
    using Heeelp.Core.ProcessManager.CommandHandlers.User;
    using Heeelp.Core.Domain.ReadModel.Dao;
    using Heeelp.Core.Domain.ReadModel.Interfaces;
    using Heeelp.Core.ProcessManager.CommandHandlers.Person;
    using Heeelp.Core.ProcessManager.EventHandlers.User;
    using Heeelp.Core.ProcessManager.EventHandlers.Person;
    using log4net;
    using System.ServiceProcess;
    using Heeelp.Core.ProcessManager.CommandHandlers.ExternalModules;
    using Heeelp.Core.Process.CommandHandler.ExternalModules;

    public sealed partial class CoreProcessor : ServiceBase, IDisposable
    {
        private IUnityContainer container;
        private CancellationTokenSource cancellationTokenSource;
        private List<IProcessor> processors;
        private bool instrumentationEnabled;



        public CoreProcessor(bool instrumentationEnabled = false)
        {
            this.instrumentationEnabled = instrumentationEnabled;

            OnCreating();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.container = CreateContainer();

            this.processors = this.container.ResolveAll<IProcessor>().ToList();

            Start();
        }

        public void Start()
        {
            this.processors.ForEach(p => p.Start());
        }

#if DEBUG
        public void StartDebug(string[] args)
        {
            Start();   // simplesmente chama a rotina principal da classe
        }
#endif

        public void Stop()
        {
            this.cancellationTokenSource.Cancel();

            this.processors.ForEach(p => p.Stop());
        }

        public void Dispose()
        {
            this.container.Dispose();
            this.cancellationTokenSource.Dispose();
        }

        private UnityContainer CreateContainer()
        {
            //var logConfigFilePath = ConfigurationManager.AppSettings["LogPath"].ToString();

            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(logConfigFilePath));

            var container = new UnityContainer();
            try
            {
                container.RegisterInstance<ITextSerializer>(new JsonTextSerializer());
                container.RegisterInstance<IMetadataProvider>(new StandardMetadataProvider());


                var serializer = container.Resolve<ITextSerializer>();
                var metadata = container.Resolve<IMetadataProvider>();

                //container.RegisterType<IBlobStorage, SqlBlobStorage>(new ContainerControlledLifetimeManager(), new InjectionConstructor("BlobStorage"));

                var commandBus = new CommandBus(new Heeelp.Core.Infrastructure.Sql.Messaging.Implementation.MessageSender(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"), serializer);
                var eventBus = new EventBus(new Heeelp.Core.Infrastructure.Sql.Messaging.Implementation.MessageSender(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"), serializer);

                var commandProcessor = new CommandProcessor(new Heeelp.Core.Infrastructure.Sql.Messaging.Implementation.MessageReceiver(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"), serializer);
                var eventProcessor = new EventProcessor(new Heeelp.Core.Infrastructure.Sql.Messaging.Implementation.MessageReceiver(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"), serializer);

                new Heeelp.Core.Logging.LogManager();

                container.RegisterInstance<ICommandBus>(commandBus);
                container.RegisterInstance<IEventBus>(eventBus);
                container.RegisterInstance<ICommandHandlerRegistry>(commandProcessor);
                container.RegisterInstance<IProcessor>("CommandProcessor", commandProcessor);
                container.RegisterInstance<IEventHandlerRegistry>(eventProcessor);
                container.RegisterInstance<IProcessor>("EventProcessor", eventProcessor);

                // Event log database and handler.
                container.RegisterType<SqlMessageLog>(new InjectionConstructor("MessageLog", serializer, metadata));
                container.RegisterType<IEventHandler, SqlMessageLogHandler>("SqlMessageLogHandler");
                container.RegisterType<ICommandHandler, SqlMessageLogHandler>("SqlMessageLogHandler");

                container.RegisterType<ICommandHandler, ExpertiseCommandHandler>("ExpertiseCommandHandler");
                container.RegisterType<ICommandHandler, UserCommandHandler>("UserCommandHandler");
                container.RegisterType<ICommandHandler, UserFileCommandHandler>("UserFileCommandHandler");
                container.RegisterType<ICommandHandler, PersonCommandHandler>("PersonCommandHandler");
                container.RegisterType<ICommandHandler, PersonRulesCommandHandler>("PersonRulesCommandHandler");
                container.RegisterType<ICommandHandler, MarketingCommandHandler>("MarketingCommandHandler");
                container.RegisterType<ICommandHandler, NotificationGatewayCommandHandler>("NotificationGatewayCommandHandler");
                container.RegisterType<ICommandHandler, PromotionCommandHandler>("PromotionCommandHandler");

                container.RegisterType<IFileTempDao, FileTempDao>();
                container.RegisterType<IPersonDao, PersonDao>();
                container.RegisterType<IUserDao, UserDao>();
                container.RegisterType<IContractDao, ContractDao>();
                container.RegisterType<IPersonContractDao, PersonContractDao>();  


                container.RegisterType<ICommandHandler, ExpertisePhotoCommandHandler>("ExpertisePhotoCommandHandler");
                container.RegisterType<ICommandHandler, PromotionProspectAddCommandHandler>("PromotionProspectAddCommandHandler");
                container.RegisterType<ICommandHandler, PersonFileCommandHandler>("PersonFileCommandHandler");
                container.RegisterType<ICommandHandler, PersonAddressCommandHandler>("PersonAddressCommandHandler");
                container.RegisterType<ICommandHandler, PersonExpertiseCommandHandler>("PersonExpertiseCommandHandler");
                container.RegisterType<ICommandHandler, PersonDocumentCommandHandler>("PersonDocumentCommandHandler");
                

                container.RegisterType<DbContext, HeeelpDataContext>("HeeelpContext", new TransientLifetimeManager(), new InjectionConstructor("HeeelpCoreAzure"));

                container.RegisterType<IDataContext<Expertise>, SqlDataContext<Expertise>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<ExpertisePhoto>, SqlDataContext<ExpertisePhoto>>(
                new TransientLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<User>, SqlDataContext<User>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<UserFile>, SqlDataContext<UserFile>>(
                  new TransientLifetimeManager(),
                  new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<FileTemp>, SqlDataContext<FileTemp>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<Person>, SqlDataContext<Person>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<PersonAddress>, SqlDataContext<PersonAddress>>(
                  new TransientLifetimeManager(),
                  new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<SelfRegistration>, SqlDataContext<SelfRegistration>>(
                  new TransientLifetimeManager(),
                  new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));


                container.RegisterType<IDataContext<PersonFile>, SqlDataContext<PersonFile>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));
                container.RegisterType<IDataContext<PersonRules>, SqlDataContext<PersonRules>>(
                                  new TransientLifetimeManager(),
                                  new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));


                container.RegisterType<IDataContext<PersonExpertise>, SqlDataContext<PersonExpertise>>(
                  new TransientLifetimeManager(),
                  new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));

                container.RegisterType<IDataContext<PersonDocument>, SqlDataContext<PersonDocument>>(
                 new TransientLifetimeManager(),
                 new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("HeeelpContext"), typeof(IEventBus)));



                container.RegisterType<global::Heeelp.Core.Domain.ReadModel.HeeelpReadDataContext>(new TransientLifetimeManager(), new InjectionConstructor("HeeelpConnection"));




                RegisterRepository(container);
                RegisterEventHandlers(container, eventProcessor);
                RegisterCommandHandlers(container);
                return container;
            }
            catch
            {
                container.Dispose();
                throw;
            }




        }



        partial void OnCreating();
        partial void OnCreateContainer(UnityContainer container);





        private void RegisterEventHandlers(UnityContainer container, EventProcessor eventProcessor)
        {
            eventProcessor.Register(container.Resolve<UserEventHandler>());
            eventProcessor.Register(container.Resolve<ExpertiseEventHandler>());
            eventProcessor.Register(container.Resolve<PersonDocumentCreatedEventHandler>());
            eventProcessor.Register(container.Resolve<PersonEventHandler>());
            eventProcessor.Register(container.Resolve<SqlMessageLogHandler>());

        }

        private void RegisterRepository(UnityContainer container)
        {
            // repository
            container.RegisterType<EventStoreDbContext>(new TransientLifetimeManager(), new InjectionConstructor("EventStore"));
            container.RegisterType(typeof(IEventSourcedRepository<>), typeof(SqlEventSourcedRepository<>), new ContainerControlledLifetimeManager());
        }

        private static void RegisterCommandHandlers(IUnityContainer unityContainer)
        {
            var commandHandlerRegistry = unityContainer.Resolve<ICommandHandlerRegistry>();

            foreach (var commandHandler in unityContainer.ResolveAll<ICommandHandler>())
            {
                commandHandlerRegistry.Register(commandHandler);
            }
        }



    }

}
