using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            using (var inputFileWatcher = new FileSystemWatcher(initialDirectory))
            {
                inputFileWatcher.IncludeSubdirectories = false;
                inputFileWatcher.Filter = "*.PublishSettings";
                inputFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Attributes;

                inputFileWatcher.Renamed += ProfileCreated;

                inputFileWatcher.EnableRaisingEvents = true;
                 
                Console.WriteLine("Press enter to quit.");
                Console.ReadKey();
            }

            

        }

        private static void ProfileCreated(object sender, FileSystemEventArgs e)
        {
            WriteLine($"*** Profile created: {e.Name} - type: {e.ChangeType}");
        }
    }
}
