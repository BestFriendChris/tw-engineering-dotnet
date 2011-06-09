using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;
using VideoWorld.Configuration;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace VideoWorld
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new KnownUsersAuthorizationFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Logoff", // Route name
                "logoff", // URL with parameters
                new {controller = "Login", action = "Logoff"}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "HomePage", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var customers = new List<Customer>
                                {
                                    new Customer("James Madison", "jmadison", "jm-password"),
                                    new Customer("Zackery Taylor", "ztaylor", "zt-password"),
                                    new Customer("Benjamin Harrison", "bharrison", "bh-password")
                                };

            var movies = new List<Movie>
                             {
                                 new Movie("Avatar", Movie.NEW_RELEASE),
                                 new Movie("Up in the Air", Movie.REGULAR),
                                 new Movie("Finding Nemo", Movie.CHILDRENS)
                             };

            kernel.Bind<ICustomerRepository>().To(typeof (CustomerRepository)).InSingletonScope().OnActivation(repository => repository.Add(customers));
            kernel.Bind<IMovieRepository>().To(typeof (MovieRepository)).InSingletonScope().OnActivation(repository => repository.Add(movies));
            kernel.Bind<TransactionRepository>().To(typeof (TransactionRepository)).InSingletonScope();
            kernel.Bind<IRentalRepository>().To(typeof (RentalRepository)).InSingletonScope();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}