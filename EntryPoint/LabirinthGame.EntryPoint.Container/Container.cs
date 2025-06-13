using Autofac;

namespace LabirinthGame.EntryPoint.Container
{
    public sealed class Container : LabirinthGame.EntryPoint.Container.IContainer
    {
        private readonly Autofac.IContainer _container;

        public Container(Autofac.IContainer container)
        {
            _container = container;
        }

        public TService Resolve<TService>() where TService : notnull
        {
            return _container.Resolve<TService>();
        }
    }
}
