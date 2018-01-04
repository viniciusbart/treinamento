using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ManipulacaoArqs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite um texto: ");
            string texto = Console.ReadLine();
            File.WriteAllText("C:\\temp\\file.txt", texto);



            //var b = Directory.Exists(@"C:\temp"); //usar @ para ignorar \t

            //if (b)
            //{
            //    var temp = new DirectoryInfo("C:\\temp");

            //    Console.WriteLine(b);

            //}


            //var drivers = DriveInfo.GetDrives();

            //foreach (var driverInfo in drivers)
            //{
            //    if (driverInfo.IsReady && driverInfo.DriveType == DriveType.Fixed)
            //    {
            //        Console.WriteLine($"{driverInfo.Name} - {driverInfo.TotalFreeSpace}");
            //    }
            //}

            Console.ReadLine();
        }
    }
}
