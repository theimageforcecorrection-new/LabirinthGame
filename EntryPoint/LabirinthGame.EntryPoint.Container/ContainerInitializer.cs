using LabirinthGame.Logic.GamePlayActions;
using LabirinthGame.Logic.Implementation;
using LabirinthGame.Logic.ConsoleUi.Implementation;
using LabirinthGame.Logic.Levels.Implementation;
using LabirinthGame.Runner.Implementation;
using LabirinthGame.Ui.MenuNavigation.GameMenuActions;
using LabirinthGame.Ui.MenuNavigation.Implementation;
using LabirinthGame.Ui.TextUi.Implementation;
using LabirinthGame.IO.Console.Implementation;
using LabirinthGame.IO.FileSystem;
using LabirinthGame.IO.FileSystem.Implementation;
using LabirinthGame.IO.Parsing.Implementation;
using System.Reflection;
using Autofac;

namespace LabirinthGame.EntryPoint.Container
{
    public sealed class ContainerInitializer
    {
        public LabirinthGame.EntryPoint.Container.IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            //TODO: Сделать более умный способ пропылесосить сборки через рефлексию
            var typesInReferenceAssemblies = new Type[]
            {
                typeof(ConsoleIO),
                typeof(LevelInfoDefaultFileCreator),
                typeof(LevelInfosInitializer),
                typeof(ConsoleGameUI),
                typeof(IGamePlayAction),
                typeof(GamePlayActionProvider),
                typeof(GameFieldElementCreatorProvider),
                typeof(LabirinthGameRunner),
                typeof(LabirinthGamePlayIO),
                typeof(LevelInfoFileLinesProviderCreator),
                typeof(IGameMenuAction),
                typeof(MenuActionApplier)
            };

            foreach (var typeInAssembly in typesInReferenceAssemblies)
            {
                RegisterAssemblyTypesByClass(builder, typeInAssembly);
            }

            var container = builder.Build();

            return new Container(container);
        }

        private void RegisterAssemblyTypesByClass(ContainerBuilder containerBuilder, Type type)
        {
            var assembly = Assembly.GetAssembly(type);

            containerBuilder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces();
        }
    }
}
