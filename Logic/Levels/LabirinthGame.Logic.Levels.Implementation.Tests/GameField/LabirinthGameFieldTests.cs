using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.Implementation.GameField;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;
using NUnit.Framework;

namespace LabirinthGame.Logic.Levels.Implementation.Tests.GameField
{
    [TestFixture]
    public sealed class LabirinthGameFieldTests
    {
        [TestCase]
        public void Ctor_CorrectDataSet()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int PlayerX = 0;
            const int PlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new TestGameFieldElement1();
            field[0, 1] = new TestGameFieldElement2();
            field[1, 0] = new TestGameFieldElement3();
            field[1, 1] = new TestGameFieldElement4();

            var labirinthGameField = new LabirinthGameField(field, PlayerX, PlayerY);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<TestGameFieldElement1>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<TestGameFieldElement2>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<TestGameFieldElement3>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<TestGameFieldElement4>());

            Assert.That(labirinthGameField.MaxX, Is.EqualTo(XSize));
            Assert.That(labirinthGameField.MaxY, Is.EqualTo(YSize));
        }

        [Test]
        public void TryMovePlayerUp_MoveCheckReturnsTrue_PlayerMovedUp()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            const int NewPlayerX = 0;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerUp((f, x, y) =>
            {
                Assert.That(f[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return true;
            });

            Assert.That(result, Is.True);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerUp_MoveCheckReturnsTrueButPlayerAlreadyOnTopLine_NoChanges()
        {
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

            var isLambdaInvoked = false;

            var result = labirinthGameField.TryMovePlayerUp((f, x, y) =>
            {
                isLambdaInvoked = true;

                return true;
            });

            Assert.That(isLambdaInvoked, Is.False);

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerUp_MoveCheckReturnsFalse_NoChanges()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 1;

            const int NewPlayerX = 0;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerUp((f, x, y) =>
            {
                Assert.That(f[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return false;
            });

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerDown_MoveCheckReturnsTrue_PlayerMovedUp()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            const int NewPlayerX = 0;
            const int NewPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerDown((f, x, y) =>
            {
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return true;
            });

            Assert.That(result, Is.True);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerDown_MoveCheckReturnsTrueButPlayerAlreadyOnTopLine_NoChanges()
        {
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

            var isLambdaInvoked = false;

            var result = labirinthGameField.TryMovePlayerDown((f, x, y) =>
            {
                isLambdaInvoked = true;

                return true;
            });

            Assert.That(isLambdaInvoked, Is.False);

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerDown_MoveCheckReturnsFalse_NoChanges()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            const int NewPlayerX = 0;
            const int NewPlayerY = 1;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerDown((f, x, y) =>
            {
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return false;
            });

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerLeft_MoveCheckReturnsTrue_PlayerMovedLeft()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            const int NewPlayerX = 0;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerLeft((f, x, y) =>
            {
                Assert.That(f[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return true;
            });

            Assert.That(result, Is.True);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerLeft_MoveCheckReturnsTrueButPlayerAlreadyOnLeftLine_NoChanges()
        {
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

            var isLambdaInvoked = false;

            var result = labirinthGameField.TryMovePlayerLeft((f, x, y) =>
            {
                isLambdaInvoked = true;

                return true;
            });

            Assert.That(isLambdaInvoked, Is.False);

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerLeft_MoveCheckReturnsFalse_NoChanges()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 1;
            const int OldPlayerY = 0;

            const int NewPlayerX = 0;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[0, 0] = new EmptyGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerLeft((f, x, y) =>
            {
                Assert.That(f[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return false;
            });

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerRight_MoveCheckReturnsTrue_PlayerMovedRight()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            const int NewPlayerX = 1;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerRight((f, x, y) =>
            {
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return true;
            });

            Assert.That(result, Is.True);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerRight_MoveCheckReturnsTrueButPlayerAlreadyOnRightLine_NoChanges()
        {
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

            var isLambdaInvoked = false;

            var result = labirinthGameField.TryMovePlayerRight((f, x, y) =>
            {
                isLambdaInvoked = true;

                return true;
            });

            Assert.That(isLambdaInvoked, Is.False);

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[0, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        [Test]
        public void TryMovePlayerRight_MoveCheckReturnsFalse_NoChanges()
        {
            const int XSize = 2;
            const int YSize = 2;

            const int OldPlayerX = 0;
            const int OldPlayerY = 0;

            const int NewPlayerX = 1;
            const int NewPlayerY = 0;

            var field = new IGameFieldElement[XSize, YSize];

            field[OldPlayerX, OldPlayerY] = new PlayerGameFieldElement();
            field[0, 1] = new EmptyGameFieldElement();
            field[1, 0] = new EmptyGameFieldElement();
            field[1, 1] = new EmptyGameFieldElement();

            var labirinthGameField = new LabirinthGameField(field, OldPlayerX, OldPlayerY);

            var result = labirinthGameField.TryMovePlayerRight((f, x, y) =>
            {
                Assert.That(f[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
                Assert.That(f[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
                Assert.That(f[1, 1], Is.AssignableTo<EmptyGameFieldElement>());

                Assert.That(x, Is.EqualTo(NewPlayerX));
                Assert.That(y, Is.EqualTo(NewPlayerY));

                return false;
            });

            Assert.That(result, Is.False);

            Assert.That(labirinthGameField[OldPlayerX, OldPlayerY], Is.AssignableTo<PlayerGameFieldElement>());
            Assert.That(labirinthGameField[0, 1], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 0], Is.AssignableTo<EmptyGameFieldElement>());
            Assert.That(labirinthGameField[1, 1], Is.AssignableTo<EmptyGameFieldElement>());
        }

        private sealed class TestGameFieldElement1 : IGameFieldElement
        {
            public char ElementChar => '1';

            public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class TestGameFieldElement2 : IGameFieldElement
        {
            public char ElementChar => '2';

            public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class TestGameFieldElement3 : IGameFieldElement
        {
            public char ElementChar => '3';

            public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class TestGameFieldElement4 : IGameFieldElement
        {
            public char ElementChar => '4';

            public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
            {
                throw new NotImplementedException();
            }
        }
    }
}
