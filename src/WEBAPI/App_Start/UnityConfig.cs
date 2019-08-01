using BLL.Implementation;
using BLL.Interface;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace WEBAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IPersonBLL, PersonBLL>();
            container.RegisterType<ICountryBLL, CountryBLL>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}