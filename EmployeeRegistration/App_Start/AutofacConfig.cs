using Autofac;
using Autofac.Integration.Mvc;
using ERM.Core.Interface;
using ERM.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegistration.App_Start
{
    public class AutofacConfig
    {

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
        
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.Register(c => new RegistrationRepository()).As<IRegistrationRepository>().SingleInstance();
            builder.Register(c => new LoginRepository()).As<ILoginRepository>().SingleInstance();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //Set the MVC DependencyResolver      
        }
    }
}