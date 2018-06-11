using Ninject;
using GameStore.StoreDomain.Abstract;
using GameStore.StoreDomain.Concrete;
using GameStore.StoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        //public Mock IProdutRepository { get; private set; }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { Name = "Football", Price = 25 },
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }
            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

            kernel.Bind<IGenericRepository<User>>().To<GenericRepository<User>>();
            kernel.Bind<IGenericRepository<Comment>>().To<GenericRepository<Comment>>();
            kernel.Bind<IGenericRepository<Game>>().To<GenericRepository<Game>>();
            kernel.Bind<IGenericRepository<GameAddition>>().To<GenericRepository<GameAddition>>();
            kernel.Bind<IGenericRepository<Admin>>().To<GenericRepository<Admin>>();
            kernel.Bind<IGenericRepository<Company>>().To<GenericRepository<Company>>();
            kernel.Bind<IGenericRepository<UserCabinet>>().To<GenericRepository<UserCabinet>>();

            /*
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            ///********************************************************///


        }
    }
}