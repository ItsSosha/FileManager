
/*
 * Для некоторых команд требуется перейти непосредственно в директорию, где находится нужный файл/директория, над которыми будет производиться операция. Но поддержку полного пути добавлю тоже.
 * cd [Путь] - изменить директорию
 * getFiles - вывод файлов директории (флаги: -a (по умолчанию) - в алфавитном порядке; -t - в виде древа с информацией; -s - сортировка по размеру)
 * read [Имя файла] - прочитать первые 200 символов файла из открытой директории
 * createDir [Имя директории] [Флаг -t или -f] - создать директорию в открытой директории. Флаг показ
 * createFile [Имя файла] - создать файл в открытой директории
 * delDir [Имя директории] - удалить дочернюю директорию открытой директории
 * delFile [Имя файла] - удалить файл из открытой директории
 * renameFile [Старое имя файла] [Новое имя файла] - переименовать файл в открытой директории
 * renameDir [Путь со старым именем] [Путь с новым именем] - переименовать директорию, принимает только полные пути, т. е. открытие родительской директории необязательно
 * search - поиск по файлу (выводит строки в которых есть нужная подстрока)
 * exit - завершить консольное приложение
 * Спасибо!
 */

using System;
using System.IO;
using System.Linq;

namespace FileSystem
{
    class Program
    {
        public static void CheckAndChangeDirectory(string Path)
        {
            if (Directory.Exists(Path))
            {
                Environment.CurrentDirectory = (Path);
            }
            else
            {
                Console.WriteLine($"Invalid directory path {Path}");
            }
        }

        public static void GetFileProperties(FileInfo File)
        {
            Console.WriteLine("---------");
            Console.WriteLine($"File name: {File.Name}");
            Console.WriteLine($"File size: {File.Length} bytes");
            Console.WriteLine($"File extension: {File.Extension}");
            Console.WriteLine($"File creation time: {File.CreationTime}");
        }

        public static void ReadFile(string F)
        {
            StreamReader streamr;
            try
            {
                streamr = new StreamReader($@"{Environment.CurrentDirectory}\{F}");
            }
            catch
            {
                Console.WriteLine($"Invalid file name {F}");
                return;
            }
            char[] read = new char[200];
            streamr.Read(read, 0, 200);
            read.PrintAllElements<char>('\0');
        }

        public static void GetFiles(string Mode = "-a")
        {
            string[] Files = Directory.GetFiles(Directory.GetCurrentDirectory());
            switch (Mode)
            {
                case "-a":
                    foreach (string File in Files)
                    {
                        Console.WriteLine(File.Split('\\')[File.Split('\\').Length - 1]);
                    }
                    break;
                case "-t":
                    foreach (string File in Files)
                    {
                        FileInfo F = new FileInfo(File);
                        GetFileProperties(F);
                    }
                    break;
                case "-s":
                    FileInfo[] Fs = new FileInfo[Files.Length];
                    for (int i = 0; i < Fs.Length; i++)
                    {
                        FileInfo F = new FileInfo(Files[i]);
                        Fs[i] = F;
                    }
                    for (int j = 1; j < Fs.Length; j++)
                    {
                        for (int i = 0; i < Fs.Length - j; i++)
                        {
                            if (Fs[i].Length > Fs[i + 1].Length)
                            {
                                FileInfo temp = Fs[i + 1];
                                Fs[i + 1] = Fs[i];
                                Fs[i] = temp;
                            }
                        }
                    }
                    foreach (FileInfo F in Fs)
                    {
                        Console.WriteLine($"{F.Name}: {F.Length} bytes");
                    }
                    break;
            }
        }

        public static void CreateDir(string NewDir)
        {
            string path = $@"{Environment.CurrentDirectory}\{NewDir}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
                Console.WriteLine($"The subdirectory {NewDir} already exists in the current directory");
        }

        public static void CreateFile(string NewFile)
        {
            string path = $@"{Environment.CurrentDirectory}\{NewFile}";
            if (!File.Exists(path))
                File.Create(path);
            else
                Console.WriteLine($"File {NewFile} already exists in the current directory");

        }

        public static void DeleteDir(string NewDir, string Flag = "-f")
        {
            bool Check = Flag == "-t" ? true : false;
            string path = $@"{Environment.CurrentDirectory}\{NewDir}";
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, Check);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"The subdirectory {path} is not empty. Use -t flag to confirm its deletion");
                }
            }
            else
            {
                Console.WriteLine($"No directory {NewDir} to delete in {Environment.CurrentDirectory}");
            }
        }

        public static void DeleteFile(string F)
        {
            string path = $@"{Environment.CurrentDirectory}\{F}";
            if (File.Exists(path))
                File.Delete(path);
            else
                Console.WriteLine($"No file {F} to delete in {Environment.CurrentDirectory}");
        }

        public static void RenameFile(string OldFile, string NewFile)
        {
            string path1 = $@"{Environment.CurrentDirectory}\{OldFile}";
            if (File.Exists(path1))
            {
                string path2 = $@"{Environment.CurrentDirectory}\{NewFile}";
                File.Move(path1, path2);
            }
            else
                Console.WriteLine($"No file {NewFile} to rename in {Environment.CurrentDirectory}");
        }

        public static void RenameDir(string OldPath, string NewDir)
        {
            string NewPath = $@"{Directory.GetParent(Environment.CurrentDirectory)}\{NewDir}";
            try
            {
                Directory.Move(OldPath, NewPath);
            }
            catch
            {
                Console.WriteLine($"The given directory {OldPath} can't be changed.");
            }
        }

        public static void SearchFile(string F, string ToSearch)
        {
            StreamReader streamr;
            try
            {
                streamr = new StreamReader($@"{Environment.CurrentDirectory}\{F}");
            }
            catch
            {
                Console.WriteLine($"Invalid file name {F}");
                return;
            }
            string[] Text = streamr.ReadToEnd().Split("\r\n");
            int[] Lines = new int[0];
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i].Contains(ToSearch))
                    Lines = Lines.Push<int>(i + 1);
            }
            Lines = Lines.Where(e => e != 0).ToArray();
            if (Lines.Length != 0)
                Console.WriteLine($"The substring was found in lines: {String.Join(", ", Lines)}");
            else
                Console.WriteLine($"The given file contains no substring {ToSearch}");
        }

        static void Main(string[] args)
        {

            while (true)
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                Console.Write(CurrentDirectory + "> ");
                string[] Input = Console.ReadLine().Split(' ').Where(e => e != "").ToArray();
                string GetCommand = Input[0];
                try
                {
                    switch (GetCommand)
                    {
                        case "cd":
                            string Path = Input[1];
                            CheckAndChangeDirectory(Path);
                            break;
                        case "getFiles":
                            if (Input.Length > 1)
                            {
                                GetFiles(Input[1]);
                            }
                            else
                            {
                                GetFiles();
                            }
                            break;
                        case "read":
                            ReadFile(Input[1]);
                            break;
                        case "createDir":
                            CreateDir(Input[1]);
                            break;
                        case "createFile":
                            CreateFile(Input[1]);
                            break;
                        case "delDir":
                            if (Input.Length > 2)
                            {
                                DeleteDir(Input[1], Input[2]);
                            }
                            else
                            {
                                DeleteDir(Input[1]);
                            }
                            break;
                        case "delFile":
                            DeleteFile(Input[1]);
                            break;
                        case "renameFile":
                            RenameFile(Input[1], Input[2]);
                            break;
                        case "renameDir":
                            RenameDir(Input[1], Input[2]);
                            break;
                        case "search":
                            SearchFile(Input[1], Input[2]);
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine($"Invalid command {GetCommand}");
                            break;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Missing arguments");
                }
            }
        }

    }
    public static class ArrayExtensions
    {
        public static T[] Push<T>(this T[] arr, T element) where T : IComparable<T>
        {
            int last = arr.Length;
            Array.Resize(ref arr, arr.Length + 20);
            arr[last] = element;
            return arr;
        }
        public static void PrintAllElements<T>(this T[] arr, T stop) where T : IComparable<T>
        {
            for (int i = 0; i < arr.Length && arr[i].CompareTo(stop) > 0; i++)
                Console.Write(arr[i]);
            Console.WriteLine();
        }
    }
}

