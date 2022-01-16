
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
using CommandsLib;

namespace FileManager
{
    class Program
    {
        public static void ExecuteCommand(string[] Input)
        {
            Commands ConsoleCommand = new Commands();
            switch (Input[0])
            {
                case "cd":
                    string Path = Input[1];
                    ConsoleCommand.CheckAndChangeDirectory(Path);
                    break;
                case "getFiles":
                    if (Input.Length > 1)
                    {
                        ConsoleCommand.GetFiles(Input[1]);
                    }
                    else
                    {
                        ConsoleCommand.GetFiles();
                    }
                    break;
                case "read":
                    ConsoleCommand.ReadFile(Input[1]);
                    break;
                case "createDir":
                    ConsoleCommand.CreateDir(Input[1]);
                    break;
                case "createFile":
                    ConsoleCommand.CreateFile(Input[1]);
                    break;
                case "delDir":
                    if (Input.Length > 2)
                    {
                        ConsoleCommand.DeleteDir(Input[1], Input[2]);
                    }
                    else
                    {
                        ConsoleCommand.DeleteDir(Input[1]);
                    }
                    break;
                case "delFile":
                    ConsoleCommand.DeleteFile(Input[1]);
                    break;
                case "renameFile":
                    ConsoleCommand.RenameFile(Input[1], Input[2]);
                    break;
                case "renameDir":
                    ConsoleCommand.RenameDir(Input[1], Input[2]);
                    break;
                case "search":
                    ConsoleCommand.SearchFile(Input[1], Input[2]);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"Invalid command {Input[0]}");
                    break;
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                Console.Write(CurrentDirectory + "> ");
                string[] Input = Console.ReadLine().Split(' ').Where(e => e != "").ToArray();
                try
                {
                    ExecuteCommand(Input);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Missing arguments");
                }
            }
        }

    }
}

