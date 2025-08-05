using Microsoft.Practices.Unity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlledSubstancesApplication
{
    public static class IocConfig
    {
        public static void ConfigIoc()
        {
            IUnityContainer container = new UnityContainer();
            registerServices(container);
            DependencyResolver.SetResolver(new CtrlSubDependencyResolver(container));

        }

        private static void registerServices(IUnityContainer container)
        {
            container.RegisterType<IPagedList<Entry>, PagedList<Entry>>();
        }
    }
}