using System;
using Xunit;
using CommandsLib;
using System.IO;

namespace UnitTests
{
    public class ConsoleCommandsTests
    {
        public Commands ConsoleCommand = new Commands();

        [Fact]
        public void CheckAndChangeDirectory_SomePath_ReturnNothing()
        {
            string expectedDir = Path.GetTempPath();
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());

            Assert.Equal(expectedDir, Environment.CurrentDirectory + '\\');
        }

        [Fact]
        public void ReadFile_SomeFile_ReturnString()
        {
            string file = Path.GetTempFileName();
            string expected = "";
            string actual = ConsoleCommand.ReadFile(file);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void GetFilesInAlphabeticalOrderTest()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string[] fileNames = Directory.GetFiles(Environment.CurrentDirectory);
            for (int i = 0; i < fileNames.Length; i++)
            {
                fileNames[i] = fileNames[i].Split('\\')[fileNames[i].Split('\\').Length - 1];
            }
            string expected = String.Join(", ", fileNames);
            string actual = ConsoleCommand.GetFiles();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetFilesOrderedBySizeTest()
        {
            string expected = "NewDoc.docx: 0 bytes, NewFile.txt: 23 bytes, TestFile.txt: 24 bytes";
            ConsoleCommand.CheckAndChangeDirectory(@"D:\C#Projects\FileManager\UnitTests\bin\Debug\net5.0\TestDirectory");
            string actual = ConsoleCommand.GetFiles("-s");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateFile_SomeFile_ReturnTrue()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string fileName = "NewTestFile.txt";
            bool expected = true;
            bool actual = ConsoleCommand.CreateFile(fileName);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void CreateFile_InvalidPath_ReturnFalse()
        {
            ConsoleCommand.CheckAndChangeDirectory(@"C:\");
            string fileName = "NewTestFile.txt";
            bool expected = false;
            bool actual = ConsoleCommand.CreateFile(fileName);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteFile_SomeFile_ReturnTrue()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string fileName = "NewTestFile.txt";
            ConsoleCommand.CreateFile(fileName);
            bool expected = true;
            bool actual = ConsoleCommand.DeleteFile(fileName);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteDir_SomeDirectory_ReturnTrue()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string dirName = "NewTestDirectory";
            ConsoleCommand.CreateDir(dirName);
            bool expected = true;
            bool actual = ConsoleCommand.DeleteDir(dirName);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchFile_SomeFile_ReturnTrue()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string subStr = "";
            string file = Path.GetTempFileName();
            file = file.Split('\\')[file.Split('\\').Length - 1];
            bool expected = true;
            bool actual = ConsoleCommand.SearchFile(file, subStr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RenameFile_SomeFile_ReturnTrue()
        {
            ConsoleCommand.CheckAndChangeDirectory(Path.GetTempPath());
            string file = Path.GetTempFileName().Split('\\')[Path.GetTempFileName().Split('\\').Length - 1];
            string newFileName = "NewTestFile.txt";
            bool expected = true;
            bool actual = ConsoleCommand.RenameFile(file, newFileName);

            Assert.Equal(expected, actual);

        }
    }
}
