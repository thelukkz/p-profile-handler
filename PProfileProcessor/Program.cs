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
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string initialDirectory = $"{userProfilePath}\\Downloads";
            string archiveDirectory = "00_PublishProfiles";

            var fileProcessor = new FileProcessor(initialDirectory, archiveDirectory);

            using (var inputFileWatcher = new FileSystemWatcher(initialDirectory))
            {
                inputFileWatcher.IncludeSubdirectories = false;
                inputFileWatcher.Filter = "*.PublishSettings";
                inputFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Attributes;

                inputFileWatcher.Renamed += ReadProfile;

                inputFileWatcher.EnableRaisingEvents = true;

                Console.WriteLine("Listening... press enter to quit.");
                Console.ReadKey();
            }
            fileProcessor.CleanDirectory();         
        }

        private static void ReadProfile(object sender, FileSystemEventArgs e)
        {
            OutputWriter ow = new OutputWriter();
            string content = File.ReadAllText(e.FullPath);

            var title = GetValue(content, "profileName");
            var url = GetValue(content, "destinationAppUrl");
            var connectionString = GetValue(content, " connectionString");


            var ftp = GetValue(StartFrom(content, "FTP"), "publishUrl");
            var ftpUser = GetValue(StartFrom(content, "FTP"), "userName");
            var ftpPassword = GetValue(StartFrom(content, "FTP"), "userPWD");

            WriteLine($"####################################");

            ow.WithKey("profileName").WithValue(title).WriteOutput();
            ow.WithKey("url").WithValue(url).WriteOutput();


            WriteLine($"> ----- DB -----");

            ow.WithKey("connectionString").WithValue(connectionString).WriteOutput();

            WriteLine($"> ----- FTP -----");

            ow.WithKey("FTP").WithValue(ftp).WriteOutput();
            ow.WithKey("user").WithValue(ftpUser).WriteOutput();
            ow.WithKey("pwd").WithValue(ftpPassword).WriteOutput();

        }

        private static string GetValue(string profile, string key)
        {
            int keyPositionIndex = profile.IndexOf($"{key}=\"");
            string profileFromKeyIndex = profile.Substring(keyPositionIndex);
            int keyLength = profileFromKeyIndex.IndexOf("\"") + 1;

            int valuePositionIndex = keyPositionIndex + keyLength;

            string tempFile = profile.Substring(valuePositionIndex);
            int valueLength = tempFile.IndexOf("\"");

            return profile.Substring(valuePositionIndex, valueLength);
        }

        private static string StartFrom(string profile, string key)
        {
            int keyPositionIndex = profile.IndexOf(key);
            return profile.Substring(keyPositionIndex);
        }
    }
}
