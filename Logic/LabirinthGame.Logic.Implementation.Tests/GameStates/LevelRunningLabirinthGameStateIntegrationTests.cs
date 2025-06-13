using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GameStates;
using LabirinthGame.Logic.Implementation.GamePlayActions;
using LabirinthGame.Logic.Implementation.GameStates;
using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.Implementation.GameField;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;
using LabirinthGame.Ui.MenuNavigation;
using LabirinthGame.Ui.TextUi;
using Moq;
using NUnit.Framework;

namespace LabirinthGame.Logic.Implementation.Tests.GameStates
{
    [TestFixture]
    public sealed class LevelRunningLabirinthGameStateIntegrationTests
    {
        [TestCase]
        public void ProcessInput_NoGamePlayAction_NoChanges()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new UpGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

            labirinthGame.VerifySet(g => g.State = It.IsAny<ILabirinthGameState>(), Times.Never());
        }

        [TestCase]
        public void ProcessInput_MoveUp_MoveToEmptyField_PlayerMovedUp()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new UpGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveUp_MoveToEnemyField_Lose()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new UpGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EnemyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<LoseLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EnemyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveUp_MoveToExitField_Win()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new UpGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new ExitGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<WinLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<ExitGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }


        [TestCase]
        public void ProcessInput_MoveUp_AlreadyOnTopLine_NoChanges()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new UpGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveDown_MoveToEmptyField_PlayerMovedUp()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new DownGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveDown_MoveToEnemyField_Lose()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new DownGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EnemyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<LoseLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EnemyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveDown_MoveToExitField_Win()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new DownGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new ExitGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<WinLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<ExitGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }


        [TestCase]
        public void ProcessInput_MoveDown_AlreadyOnBottomLine_NoChanges()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new DownGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveLeft_MoveToEmptyField_PlayerMovedLeft()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new LeftGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveLeft_MoveToEnemyField_Lose()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new LeftGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EnemyGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<LoseLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EnemyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveLeft_MoveToExitField_Win()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new LeftGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new ExitGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<WinLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<ExitGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }


        [TestCase]
        public void ProcessInput_MoveLeft_AlreadyOnLeftLine_NoChanges()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new LeftGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveRight_MoveToEmptyField_PlayerMovedRight()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new RightGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveRight_MoveToEnemyField_Lose()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new RightGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EnemyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<LoseLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EnemyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_MoveRight_MoveToExitField_Win()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new RightGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new ExitGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<WinLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<ExitGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }


        [TestCase]
        public void ProcessInput_MoveRight_AlreadyOnRightLine_NoChanges()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new RightGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void ProcessInput_ExitGamePlayAction_ExitToMenuGameState()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            gamePlayIO.Setup(g => g.ReadNextAction()).Returns(new ExitGamePlayAction());

            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            fieldBuilder.Setup(b => b.Build(It.IsAny<int>())).Returns(labirinthGameField);

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.ProcessInput(labirinthGame.Object);

            labirinthGame.VerifySet(g => g.State = It.IsAny<MenuLabirinthGameState>());

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [TestCase]
        public void GetDisplayingInfo_CorrectGameFieldPassed()
        {
            var gamePlayIO = new Mock<ILabirinthGamePlayIO>();

            var fieldBuilder = new Mock<ILabirinthGameFieldBuilder>();

            var menuActionApplier = new Mock<IMenuActionApplier>();

            var consoleGameUi = new Mock<IConsoleGameUI>();

            var gameState = new LevelRunningLabirinthGameState(gamePlayIO.Object, fieldBuilder.Object, menuActionApplier.Object, consoleGameUi.Object);

            var labirinthGame = new Mock<ILabirinthGame>();

            gameState.GetDisplayingInfo(labirinthGame.Object);
        }
    }
}
