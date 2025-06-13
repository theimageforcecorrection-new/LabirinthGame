using LabirinthGame.EntryPoint.Container;
using LabirinthGame.Runner;

namespace LabirinthGame.EntryPoint
{
    public static class Program
    {
        public static void Main()
        {
            var containerInitializer = new ContainerInitializer();

            var container = containerInitializer.Initialize();
            var gameRunner = container.Resolve<ILabirinthGameRunner>();

            gameRunner.Wait();
        }
    }
}
