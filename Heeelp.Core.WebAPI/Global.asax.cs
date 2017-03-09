using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Heeelp.Core.Infrastructure.Sql.Database;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Domain.ReadModel.Dao;
using Heeelp.Core.Infrastructure.Sql.Messaging;
using Heeelp.Core.Infrastructure.Sql.Messaging.Implementation;
using Heeelp.Core.Domain;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Serialization;
using FluentValidation.Mvc;


namespace Heeelp.Core.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        protected void Application_Start()
        {

            DatabaseSetup.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            this.container = CreateContainer();

            var cont = new UnityHttpControllerActivator(this.container);
            container.RegisterInstance<IHttpControllerActivator>(cont);

            DependencyResolver.SetResolver(new UnityServiceLocator(this.container));
            
            GlobalConfiguration.Configuration.Services.Replace(
                    typeof(IHttpControllerActivator),
                    cont);
            
        }

        
        private static UnityContainer CreateContainer()
        {


            var container = new UnityContainer();
            try
            {
                var serializer = new JsonTextSerializer();
                container.RegisterInstance<ITextSerializer>(serializer);
                container.RegisterType<IMessageSender, MessageSender>(
                "Commands", new TransientLifetimeManager(), new InjectionConstructor(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"));
                container.RegisterType<ICommandBus, CommandBus>(
                new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<IMessageSender>("Commands"), serializer));
                container.RegisterType<IDataContext<Expertise>, SqlDataContext<Expertise>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("Expertise"), typeof(IEventBus)));
                container.RegisterType<IDataContext<Person>, SqlDataContext<Person>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("Person"), typeof(IEventBus)));
                container.RegisterType<IDataContext<User>, SqlDataContext<User>>(
                   new TransientLifetimeManager(),
                   new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("User"), typeof(IEventBus)));

                new Heeelp.Core.Logging.LogManager();

                //todo: Realmente precisamos registrar no container esses DAos todos? A camada de API fara no maximo consultas para fins de validacao, nunca vai incluir nada em banco exceto Commands, usando o Bus.
                container.RegisterType<IExpertiseDao, ExpertiseDao>();
                container.RegisterType<IPersonDao, PersonDao>();
                container.RegisterType<IPersonTypeDao, PersonTypeDao>();
                container.RegisterType<IPersonOriginTypeDao, PersonOriginTypeDao>();
                container.RegisterType<IPersonStatusDao, PersonStatusDao>();
                container.RegisterType<IPersonProfileDao, PersonProfileDao>();
                container.RegisterType<IPersonAddressDao, PersonAddressDao>();
                container.RegisterType<IPersonExpertiseDao, PersonExpertiseDao>();
                container.RegisterType<IFileTempDao, FileTempDao>();
                container.RegisterType<ICurrencyDao, CurrencyDao>();
                container.RegisterType<ISecuritySourceDao, SecuritySourceDao>();
                container.RegisterType<IServerInstanceDao, ServerInstanceDao>();
                container.RegisterType<ICountryDao, CountryDao>();
                container.RegisterType<ICityDao, CityDao>();
                container.RegisterType<INeighbourhoodDao, NeighbourhoodDao>();
                container.RegisterType<IStateDao, StateDao>();
                container.RegisterType<IUserDao, UserDao>();
                container.RegisterType<IUserProfileDao, UserProfileDao>();
                container.RegisterType<IUserStatusDao, UserStatusDao>();
                container.RegisterType<IAutenticationUserDao, AutenticationUserDao>();
                container.RegisterType<ILanguageDao, LanguageDao>();
                container.RegisterType<IPersonSkinDao, PersonSkinDao>();
                container.RegisterType<IContractDao,  ContractDao>(); 



                container.RegisterType<global::Heeelp.Core.Domain.ReadModel.HeeelpReadDataContext>(new TransientLifetimeManager(), new InjectionConstructor("HeeelpConnection"));


                return container;
            }
            catch
            {
                container.Dispose();
                throw;
            }
        }


    }
}
