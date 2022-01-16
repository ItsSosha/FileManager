using System;
using System.IO;
using System.Linq;

namespace CommandsLib
{
    public class Commands
    {
        public string Path { get; set; } = Environment.CurrentDirectory; 

        public static void GetFileProperties(FileInfo file)
        {
            Console.WriteLine("---------");
            Console.WriteLine($"File name: {file.Name}");
            Console.WriteLine($"File size: {file.Length} bytes");
            Console.WriteLine($"File extension: {file.Extension}");
            Console.WriteLine($"File creation time: {file.CreationTime}");
        }

        public void CheckAndChangeDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Environment.CurrentDirectory = (path);
            }
            else
            {
                Console.WriteLine($"Invalid directory path {path}");
            }
        }

        public string ReadFile(string F)
        {
            StreamReader streamr;
            try
            {
                streamr = new StreamReader($@"{this.Path}\{F}");
            }
            catch
            {
                Console.WriteLine($"Invalid file name {F}");
                return "";
            }
            char[] read = new char[200];
            try
            {
                streamr.Read(read, 0, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine("The given file can't be read");
            }
            read.PrintAllElements<char>('\0');
            string output = new string(read).Trim('\0');
            return output;
        }

        public string GetFiles(string mode = "-a")
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            switch (mode)
            {
                case "-a":
                    string[] fileNames = new string[1];
                    foreach (string file in files)
                    {
                        string fileName = file.Split('\\')[file.Split('\\').Length - 1];
                        Console.WriteLine(fileName);
                        if (fileNames[fileNames.Length - 1] != null)
                        {
                            fileNames = fileNames.Push<string>(fileName);
                        }
                        else
                        {
                            fileNames[Array.IndexOf(fileNames, null)] = fileName;
                        }
                    }
                    fileNames = fileNames.Where(e => !String.IsNullOrEmpty(e)).ToArray();
                    string result = String.Join(", ", fileNames);
                    return String.Join(", ", result);
                case "-t":
                    foreach (string file in files)
                    {
                        FileInfo f = new FileInfo(file);
                        GetFileProperties(f);
                    }
                    break;
                case "-s":
                    FileInfo[] fs = new FileInfo[files.Length];
                    for (int i = 0; i < fs.Length; i++)
                    {
                        FileInfo f = new FileInfo(files[i]);
                        fs[i] = f;
                    }

                    for (int j = 1; j < fs.Length; j++)
                    {
                        for (int i = 0; i < fs.Length - j; i++)
                        {
                            if (fs[i].Length > fs[i + 1].Length)
                            {
                                FileInfo temp = fs[i + 1];
                                fs[i + 1] = fs[i];
                                fs[i] = temp;
                            }
                        }
                    }

                    string[] res = new string[fs.Length];
                    int k = 0;
                    foreach (FileInfo f in fs)
                    {
                        Console.WriteLine($"{f.Name}: {f.Length} bytes");
                        res[k] = $"{f.Name}: {f.Length} bytes";
                        k++;
                    }
                    return String.Join(", ", res);
            }
            return "";
        }

        public void CreateDir(string newDir)
        {
            string path = $@"{this.Path}\{newDir}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                Console.WriteLine($"The subdirectory {newDir} already exists in the current directory");
            }
        }

        public bool CreateFile(string newFile)
        {
            string path = $@"{this.Path}\{newFile}";
            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"File {newFile} already exists in the current directory");
                return true;
            }

        }

        public bool DeleteDir(string newDir, string flag = "-f")
        {
            bool check = flag == "-t" ? true : false;
            string path = $@"{this.Path}\{newDir}";
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, check);
                    return true;
                }
                catch (IOException e)
                {
                    Console.WriteLine($"The subdirectory {path} is not empty. Use -t flag to confirm its deletion");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"No directory {newDir} to delete in {this.Path}");
                return true;
            }
        }

        public bool DeleteFile(string F)
        {
            string path = $@"{this.Path}\{F}";
            if (File.Exists(path))
            {
                try
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(path);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message, "\\", e.InnerException, "\\", e.Source);
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"No file {F} to delete in {this.Path}");
                return true;
            }
        }

        public bool RenameFile(string oldFile, string newFile)
        {
            string path1 = $@"{this.Path}\{oldFile}";
            if (File.Exists(path1))
            {
                string path2 = $@"{this.Path}\{newFile}";
                File.Move(path1, path2, true);
                return true;
            }
            else
            {
                Console.WriteLine($"No file {newFile} to rename in {this.Path}");
                return false;
            }
        }

        public void RenameDir(string oldPath, string newDir)
        {
            string newPath = $@"{Directory.GetParent(this.Path)}\{newDir}";
            try
            {
                Directory.Move(oldPath, newPath);
            }
            catch
            {
                Console.WriteLine($"The given directory {oldPath} can't be changed.");
            }
        }

        public bool SearchFile(string f, string subStr)
        {
            StreamReader streamr;
            try
            {
                streamr = new StreamReader($@"{this.Path}\{f}");
            }
            catch
            {
                Console.WriteLine($"Invalid file name {f}");
                return false;
            }

            string[] text = streamr.ReadToEnd().Split("\r\n");
            int[] lines = new int[0];
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains(subStr))
                {
                    lines = lines.Push<int>(i + 1);
                }
            }

            lines = lines.Where(e => e != 0).ToArray();
            if (lines.Length != 0)
            {
                Console.WriteLine($"The substring was found in lines: {String.Join(", ", lines)}");
                return true;
            }
            else
            {
                Console.WriteLine($"The given file contains no substring {subStr}");
                return false;
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
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
        }
    }
}
