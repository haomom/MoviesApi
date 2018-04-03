using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Movie.ModelDto;
using Movie.Services;
using Movies.Interfaces;
using Repository;

namespace MoviesApi
{
    public class IoCConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MovieServices>()
                .As<IMovieServices>()
                .InstancePerRequest();

            builder.RegisterType<MovieRepository>()
                .As<IMovieRepository>()
                .InstancePerRequest();

            builder.RegisterType<MovieDTO>()
                .As<IMoviesDto>()
                .InstancePerRequest();

            builder.RegisterType<UserMovieRatingDto>()
                .As<IUserMovieRatingDto>()
                .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}