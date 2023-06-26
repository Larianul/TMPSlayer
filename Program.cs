using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class ProiectSO
{
    static int nr;
    static int countFiles = 0;
    static int countDir = 0;

    public static void Proc()
    {
        DirectoryInfo di2 = new DirectoryInfo("/tmp/");
        DirectoryInfo di = new DirectoryInfo("/var/tmp");
        int n = 1;
        while (n == 1)
        {
            try
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    string filePath = file.FullName;
                    DeleteFile(filePath);
                    countFiles++;
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    string dirPath = dir.FullName;
                    DeleteDirectory(dirPath);
                    countDir++;
                }

                foreach (FileInfo file in di2.GetFiles())
                {
                    string filePath = file.FullName;
                    DeleteFile(filePath);
                    countFiles++;
                }

                foreach (DirectoryInfo dir in di2.GetDirectories())
                {
                    string dirPath = dir.FullName;
                    DeleteDirectory(dirPath);
                    countDir++;
                }

                Console.WriteLine(countFiles + " fisiere sterse");
                Console.WriteLine(countDir + " directoare sterse");

                countDir = 0;
                countFiles = 0;
                nr = 2;
                Thread.Sleep(nr * 60000);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A aparut o exceptie: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
    }

    public static void DeleteFile(string filePath)
    {
        if (filePath.Contains("dnf"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.WriteLine("Fisierul " + filePath + " este in curs de editare. Incercati mai tarziu");
            Console.ForegroundColor = ConsoleColor.Black;
            throw new InvalidOperationException("Fisierul este in curs de editare.");
        }
        else if (filePath.Contains("systemd"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.WriteLine("Fisierul " + filePath + " are continut sensibil. Eu nu l-as sterge...");
            Console.ForegroundColor = ConsoleColor.Black;
            throw new InvalidOperationException("Fisierul are continut sensibil.");
        }
        else
        {
            File.Delete(filePath);
        }
    }

    public static void DeleteDirectory(string dirPath)
    {
        if (dirPath.Contains("dnf"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.WriteLine("Directorul " + dirPath + " este in curs de editare. Incercati mai tarziu");
            Console.ForegroundColor = ConsoleColor.Black;
            throw new InvalidOperationException("Directorul este in curs de editare.");
        }
        else if (dirPath.Contains("systemd"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.WriteLine("Directorul " + dirPath + " are continut sensibil. Eu nu l-as sterge...");
            Console.ForegroundColor = ConsoleColor.Black;
            throw new InvalidOperationException("Directorul are continut sensibil.");
        }
        else
        {
            Directory.Delete(dirPath, true);
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Curatarea directorului de fisiere temporare va incepe imediat:");

        for (int z = 0; z < 100; z++)
        {
            Thread.Sleep(20);
            Console.Write("*");
        }

        Thread t = new Thread(new ThreadStart(Proc));
        t.Start();
    }
}
