using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace TwentinonethApplication.ConsoleApp
{
    class Program
    {
        public static void Folder()
        {

            while (true)
            {
                try
                {

                    DirectoryInfo dirinfo = new(@"C:\Users\TheDarkKnight\Downloads\TestFolder");
                    if (dirinfo.Exists)
                    {
                        var files = dirinfo.GetFiles();
                        long sum = 0;
                        foreach (var item in files)
                        {
                            sum += item.Length;
                        }
                        long free = 0;
                        foreach (var file in files)
                        {

                            if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                            {
                                free += file.Length;
                                Console.WriteLine($"Исходный размер папки:{sum}");
                                file.Delete();
                                Console.WriteLine($"Освобождено:{free}");
                                Console.WriteLine($"Текущий размер папки:{sum - free}");

                            }
                            else
                            {
                                Console.WriteLine("Не чистим файлы");
                            }
                        }
                        var directories = dirinfo.GetDirectories().ToList();
                        directories.Add(dirinfo);
                        foreach (var dir in directories)
                        {

                            if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                            {
                                dir.Delete();
                                Console.WriteLine("Чистим папки");
                            }
                            else
                            {
                                Console.WriteLine("Не чистим папки");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally 
                { 

                    Thread.Sleep(TimeSpan.FromSeconds(30));
                }
            }
        }

        static void Main(string[] args)
        {
            Folder();
        }
    }
}
