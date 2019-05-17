using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PProfileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string initialDirectory = @"C:\Users\pro-lqs\Downloads";
            string archiveDirectory = "00_PublishProfiles";

            var fileProcessor = new FileProcessor(initialDirectory, archiveDirectory);

            fileProcessor.CleanDirectory();

            Console.WriteLine("Press enter to quit.");
            Console.ReadKey();

        }
    }
}
