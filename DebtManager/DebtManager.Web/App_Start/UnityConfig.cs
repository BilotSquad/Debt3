using DebtManager.Application.Services;
using DebtManager.Application.Services.Interfaces;
using DebtManager.Infrastructure.EFCodeFirst;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace DebtManager.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IRepository, DebtManagerDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, DebtManagerDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDebtService, DebtService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentService, PaymentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAggregatedDebtService, AggregatedDebtService>(new HierarchicalLifetimeManager());
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}