namespace LabirinthGame.EntryPoint.Container
{
    public interface IContainer
    {
        TService Resolve<TService>() where TService : notnull;
    }
}
