using LabirinthGame.IO.Common;
using LabirinthGame.IO.FileSystem;
using Moq;
using NUnit.Framework;

namespace LabirinthGame.IO.Parsing.Implementation.Tests
{
    [TestFixture]
    public sealed class LevelGameFieldProviderIntegrationTests
    {
        [TestCase]
        public void Provide_FileExist_FileNotCreating()
        {
            var levelInfoDefaultFileCreator = new Mock<ILevelInfoDefaultFileCreator>();

            levelInfoDefaultFileCreator.Setup(c => c.CheckIsFileExist()).Returns(true);

            var levelInfoFileLinesProviderCreator = new Mock<ILevelInfoFileLinesProviderCreator>();

            var levelInfoFileLinesProvider = new Mock<ILevelInfoFileLinesProvider>();

            var providingStrings = new string[]
            {
                "1",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000"
            };

            int lineIndex = 0;

            levelInfoFileLinesProvider.Setup(p => p.GetNextLine()).Returns(() =>
            {
                if (lineIndex < providingStrings.Length)
                {
                    return providingStrings[lineIndex++];
                }

                throw new LevelInfoFileReadingException();
            });

            levelInfoFileLinesProviderCreator.Setup(c => c.Create()).Returns(levelInfoFileLinesProvider.Object);

            var levelInfosInitializer = new LevelInfosInitializer(levelInfoDefaultFileCreator.Object, levelInfoFileLinesProviderCreator.Object);
            var gameLevelManager = new GameLevelManager(levelInfosInitializer);

            var fieldProvider = new LevelGameFieldProvider(gameLevelManager);

            levelInfoDefaultFileCreator.Verify(c => c.CreateFile(), Times.Never);
        }


        [TestCase]
        public void Provide_FileNotExist_CreateFile()
        {
            var levelInfoDefaultFileCreator = new Mock<ILevelInfoDefaultFileCreator>();

            levelInfoDefaultFileCreator.Setup(c => c.CheckIsFileExist()).Returns(false);

            var levelInfoFileLinesProviderCreator = new Mock<ILevelInfoFileLinesProviderCreator>();

            var levelInfoFileLinesProvider = new Mock<ILevelInfoFileLinesProvider>();

            var providingStrings = new string[]
            {
                "1",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000"
            };

            int lineIndex = 0;

            levelInfoFileLinesProvider.Setup(p => p.GetNextLine()).Returns(() =>
            {
                if (lineIndex < providingStrings.Length)
                {
                    return providingStrings[lineIndex++];
                }

                throw new LevelInfoFileReadingException();
            });

            levelInfoFileLinesProviderCreator.Setup(c => c.Create()).Returns(levelInfoFileLinesProvider.Object);

            var levelInfosInitializer = new LevelInfosInitializer(levelInfoDefaultFileCreator.Object, levelInfoFileLinesProviderCreator.Object);
            var gameLevelManager = new GameLevelManager(levelInfosInitializer);

            var fieldProvider = new LevelGameFieldProvider(gameLevelManager);

            levelInfoDefaultFileCreator.Verify(c => c.CreateFile(), Times.Once);
        }

        [TestCase]
        public void Provide_OneLevel_TryProvideSecondLevel_Throws()
        {
            var levelInfoDefaultFileCreator = new Mock<ILevelInfoDefaultFileCreator>();

            var levelInfoFileLinesProviderCreator = new Mock<ILevelInfoFileLinesProviderCreator>();

            var levelInfoFileLinesProvider = new Mock<ILevelInfoFileLinesProvider>();

            var providingStrings = new string[]
            {
                "1",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000"
            };

            int lineIndex = 0;

            levelInfoFileLinesProvider.Setup(p => p.GetNextLine()).Returns(() =>
            {
                if (lineIndex < providingStrings.Length)
                {
                    return providingStrings[lineIndex++];
                }

                throw new LevelInfoFileReadingException();
            });

            levelInfoFileLinesProviderCreator.Setup(c => c.Create()).Returns(levelInfoFileLinesProvider.Object);

            var levelInfosInitializer = new LevelInfosInitializer(levelInfoDefaultFileCreator.Object, levelInfoFileLinesProviderCreator.Object);
            var gameLevelManager = new GameLevelManager(levelInfosInitializer);

            var fieldProvider = new LevelGameFieldProvider(gameLevelManager);

            Assert.Throws<LevelNotExistException>(() => fieldProvider.Provide(1));
        }

        [TestCase]
        public void Provide_OneLevel_CorrectDataParsed()
        {
            var levelInfoDefaultFileCreator = new Mock<ILevelInfoDefaultFileCreator>();

            var levelInfoFileLinesProviderCreator = new Mock<ILevelInfoFileLinesProviderCreator>();

            var levelInfoFileLinesProvider = new Mock<ILevelInfoFileLinesProvider>();

            var providingStrings = new string[]
            {
                "1",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000"
            };

            var expectedParsedField = new int[10, 10]
            {
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 5,4,2,0,0,0,0,0,0,0 },
                { 3,3,3,3,3,3,3,3,3,3 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 }
            };

            int lineIndex = 0;

            levelInfoFileLinesProvider.Setup(p => p.GetNextLine()).Returns(() =>
            {
                if (lineIndex < providingStrings.Length)
                {
                    return providingStrings[lineIndex++];
                }

                throw new LevelInfoFileReadingException();
            });

            levelInfoFileLinesProviderCreator.Setup(c => c.Create()).Returns(levelInfoFileLinesProvider.Object);

            var levelInfosInitializer = new LevelInfosInitializer(levelInfoDefaultFileCreator.Object, levelInfoFileLinesProviderCreator.Object);
            var gameLevelManager = new GameLevelManager(levelInfosInitializer);

            var fieldProvider = new LevelGameFieldProvider(gameLevelManager);

            var (playerX, playerY, parsedField) = fieldProvider.Provide(0);

            const int expectedPlayerX = 3;
            const int expectedPlayerY = 1;

            Assert.That(playerX, Is.EqualTo(expectedPlayerX));
            Assert.That(playerY, Is.EqualTo(expectedPlayerY));

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.That(parsedField[i, j], Is.EqualTo(expectedParsedField[j, i]));
                }
            }
        }


        [TestCase]
        public void Provide_TwoLevels_CorrectDataParsed()
        {
            var levelInfoDefaultFileCreator = new Mock<ILevelInfoDefaultFileCreator>();

            var levelInfoFileLinesProviderCreator = new Mock<ILevelInfoFileLinesProviderCreator>();

            var levelInfoFileLinesProvider = new Mock<ILevelInfoFileLinesProvider>();

            var providingStrings = new string[]
            {
                "2",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000",
                "10 10",
                "3 1",
                "0000000000",
                "0001000000",
                "0000000000",
                "0000000000",
                "5420000000",
                "3333333333",
                "0000000000",
                "0000000000",
                "0000000000",
                "0000000000"
            };

            var expectedParsedField = new int[10, 10]
            {
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 5,4,2,0,0,0,0,0,0,0 },
                { 3,3,3,3,3,3,3,3,3,3 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 }
            };

            int lineIndex = 0;

            levelInfoFileLinesProvider.Setup(p => p.GetNextLine()).Returns(() =>
            {
                if (lineIndex < providingStrings.Length)
                {
                    return providingStrings[lineIndex++];
                }

                throw new LevelInfoFileReadingException();
            });

            levelInfoFileLinesProviderCreator.Setup(c => c.Create()).Returns(levelInfoFileLinesProvider.Object);

            var levelInfosInitializer = new LevelInfosInitializer(levelInfoDefaultFileCreator.Object, levelInfoFileLinesProviderCreator.Object);
            var gameLevelManager = new GameLevelManager(levelInfosInitializer);

            var fieldProvider = new LevelGameFieldProvider(gameLevelManager);

            var (playerX1, playerY1, parsedField1) = fieldProvider.Provide(0);

            const int expectedPlayerX = 3;
            const int expectedPlayerY = 1;

            Assert.That(playerX1, Is.EqualTo(expectedPlayerX));
            Assert.That(playerY1, Is.EqualTo(expectedPlayerY));

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.That(parsedField1[i, j], Is.EqualTo(expectedParsedField[j, i]));
                }
            }

            var (playerX2, playerY2, parsedField2) = fieldProvider.Provide(1);

            Assert.That(playerX2, Is.EqualTo(expectedPlayerX));
            Assert.That(playerY2, Is.EqualTo(expectedPlayerY));

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.That(parsedField2[i, j], Is.EqualTo(expectedParsedField[j, i]));
                }
            }

        }
    }
}

