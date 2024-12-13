using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace pz18Request.Services
{
    public static class RepoContainer
    {
        private static IUnityContainer _container;
        static RepoContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRequestRepository, RequestRepository>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ICommentRepository, CommentRepository>(
                new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
