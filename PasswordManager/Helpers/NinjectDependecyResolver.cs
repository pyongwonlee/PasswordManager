﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using PasswordManager.Models.Data;

namespace PasswordManager.Helpers
{
    public class NinjectDependecyResolver : IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependecyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }


        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        void AddBindings()
        {
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            kernel.Bind<ICompanyRepository>().To<EFCompanyRepository>();
            kernel.Bind<IPasswordRepository>().To<EFPasswordRepository>();
            kernel.Bind<IPasswordHelperRepository>().To<EFPasswordHelperRepository>();

            kernel.Bind<IDirectorRepository>().To<EFDirectorRepository>();
            kernel.Bind<IMovieRepository>().To<EFMovieRepository>();

            kernel.Bind<ICityRepository>().To<EFCityRepository>();
            kernel.Bind<IArtCenterRepository>().To<EFArtCenterRepository>();

            kernel.Bind<IBookRepository>().To<EFBookRepository>();

            kernel.Bind<IPreferenceRepository>().To<EFPreferenceRepository>();
        }
    }
}