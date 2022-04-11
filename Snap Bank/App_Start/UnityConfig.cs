using Snap_Bank.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Snap_Bank
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IAccountTableService, AccountTableService>();
            container.RegisterType<IPersonalDetailsService, PersonalDetailsService>();
            container.RegisterType<ISecurityQuestionsService, SecurityQuestionsService>();
            container.RegisterType<ITransactionsService, TransactionsService>();
        }
    }
}