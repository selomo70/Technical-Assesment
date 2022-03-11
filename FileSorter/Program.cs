using System;
using System.IO;

namespace FileSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get the directory
            DirectoryInfo place = new DirectoryInfo(@"C:\Users\confidences\Documents\Selomo mc\Haldan Consulting\Voluptates ab quod.-Sally Boyle V-Id dolorem voluptatem voluptate");

            string pathString = @"C:\Users\confidences\Documents\Selomo mc\Haldan Consulting\Top-level folder";
            // Using GetFiles() method to get list of all

            System.IO.Directory.CreateDirectory(pathString);
            FileInfo[] Files = place.GetFiles("*.mp3", SearchOption.TopDirectoryOnly);
            Console.WriteLine("The top-level folder has the name of the Artist.");
            Console.WriteLine();

            // Display the file names
            foreach (FileInfo inf in Files)
            {
                
               // Console.WriteLine(inf.Name);
                pathString = System.IO.Path.Combine(pathString, inf.Name);
                if (!System.IO.File.Exists(pathString))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                    {
                        for (byte i = 0; i < 100; i++)
                        {
                            fs.WriteByte(i);
                        }
                    }
                }
            }
            //place.MoveTo(@"C:\Users\confidences\Documents\Selomo mc\Haldan Consulting\Voluptates ab quod.-Sally Boyle V-Id dolorem voluptatem voluptate\Top Level");

        }
    }
}
