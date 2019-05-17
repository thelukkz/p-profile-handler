using static System.Console;
using System.IO;
using System;

namespace PProfileProcessor
{
    internal class FileProcessor
    {
        public string InitialDirectoryPath { get; }
        public string ArchiveDirectoryName { get; }
        public string ArchiveDirectoryPath { get;  }

        public FileProcessor(string initialDir, string archiveDir)
        {
            InitialDirectoryPath = initialDir;
            ArchiveDirectoryName = archiveDir;

            ArchiveDirectoryPath = Path.Combine(InitialDirectoryPath, ArchiveDirectoryName);
        }

        public void Process()
        {           

        }

        public void CleanDirectory()
        {
            PrepareArchiveDirectory();

            var oldProfiles = GetAllPublishProfiles(InitialDirectoryPath);

            foreach (var profile in oldProfiles)
            {
                ArchiveFile(profile);
            }
        }

        private void PrepareArchiveDirectory()
        {
            var archivePath = Path.Combine(InitialDirectoryPath, ArchiveDirectoryName);
            WriteLine($"> Preparing archive directory: {archivePath}");
            Directory.CreateDirectory(archivePath);
        }

        private static string[] GetAllPublishProfiles(string directoryPath)
        {
            string[] allProfiles = Directory.GetFiles(directoryPath, "*.PublishSettings");
            return allProfiles;
        }

        private void ArchiveFile(string path)
        {
            string extension = Path.GetExtension(path);
            string filename = Path.GetFileNameWithoutExtension(path);

            var newFilename = $"{filename}-{Guid.NewGuid()}{extension}";

            var archivedFilePath = Path.Combine(ArchiveDirectoryPath, newFilename);

            WriteLine($"> Transfer file: {path} -> {archivedFilePath}");
            File.Move(path, archivedFilePath);
        }
    }
}