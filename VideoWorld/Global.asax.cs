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
using VideoWorld.Utils;

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

            if(Feature.AdminAccount.IsEnabled())
            {
                customers.Add(Customer.CreateAdminUser("Admin", "admin", "pw"));
            }

            var movies = new List<Movie>
                             {
                                 AddMovie("Avatar", Movie.NEW_RELEASE, "James Cameron", "Sam Worthington", "Zoe Saldana", "Action"),
                                 AddMovie("Up in the Air", Movie.REGULAR, "Jason Reitman", "George Clooney", "Vera Farmiga", "Drama"),
                                 AddMovie("Finding Nemo", Movie.CHILDRENS, "Andrew Stanton", "Albert Brooks", "Ellen DeGeneres", "Animation")
                             };

            kernel.Bind<ICustomerRepository>().To(typeof (CustomerRepository)).InSingletonScope().OnActivation(repository => repository.Add(customers));
            kernel.Bind<IMovieRepository>().To(typeof (MovieRepository)).InSingletonScope().OnActivation(repository => repository.Add(movies));
            kernel.Bind<TransactionRepository>().To(typeof (TransactionRepository)).InSingletonScope();
            kernel.Bind<IRentalRepository>().To(typeof (RentalRepository)).InSingletonScope();

            return kernel;
        }

        private static Movie AddMovie(string title, IPrice price, string director, string actor, string actress, string category)
        {
            if (Feature.DetailedMovies.IsEnabled())
                return new DetailedMovie(title, price, director, actor, actress, category);
            return new Movie(title, price);
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