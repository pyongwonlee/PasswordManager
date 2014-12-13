using System;
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
            kernel.Bind<IPasswordRepository>().To<EFPasswordtRepository>();
            kernel.Bind<IPasswordHelperRepository>().To<EFPasswordHelperRepository>();
        }
    }
}