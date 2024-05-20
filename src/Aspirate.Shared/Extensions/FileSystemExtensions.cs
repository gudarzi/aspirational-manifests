namespace Aspirate.Shared.Extensions;

public static class FileSystemExtensions
{
    public static string NormalizePath(this IFileSystem fileSystem, string pathToTarget)
    {
        if (string.IsNullOrEmpty(pathToTarget))
        {
            return fileSystem.Directory.GetCurrentDirectory();
        }

        if (!pathToTarget.StartsWith('.'))
        {
            return pathToTarget;
        }

        var currentDirectory = fileSystem.Directory.GetCurrentDirectory();

        var normalizedProjectPath = pathToTarget.Replace('\\', fileSystem.Path.DirectorySeparatorChar);

        return fileSystem.Path.Combine(currentDirectory, normalizedProjectPath);
    }

    public static string GetFullPath(this IFileSystem fileSystem, string path)
    {
        if (fileSystem.Path.IsPathRooted(path))
        {
            return fileSystem.Path.GetFullPath(path);
        }

        string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return path.StartsWith($"~{fileSystem.Path.DirectorySeparatorChar}") ?
            // The path is relative to the user's home directory
            fileSystem.Path.Combine(homePath, path.TrimStart('~', fileSystem.Path.DirectorySeparatorChar)) :
            // The path is relative to the current working directory
            fileSystem.Path.GetFullPath(path);
    }

    public static string AspirateAppDataFolder(this IFileSystem fileSystem)
    {
        var appDataFolder = fileSystem.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AspirateLiterals.AspirateName);

        if (!Directory.Exists(appDataFolder))
        {
            fileSystem.Directory.CreateDirectory(appDataFolder);
        }

        return appDataFolder;
    }

    public static void RecursivelyCopyDirectory(this IFileSystem filesystem, string sourcePath, string targetPath, IAnsiConsole console)
    {
        if (!filesystem.Directory.Exists(sourcePath))
        {
            return;
        }

        if (!filesystem.Directory.Exists(targetPath))
        {
            filesystem.Directory.CreateDirectory(targetPath);
        }

        var entries = filesystem.Directory.GetFileSystemEntries(sourcePath);

        foreach (var entry in entries)
        {
            var name = filesystem.Path.GetFileName(entry);
            var source = filesystem.Path.Combine(sourcePath, name);
            var target = filesystem.Path.Combine(targetPath, name);

            if (filesystem.Directory.Exists(source))
            {
                filesystem.RecursivelyCopyDirectory(source, target, console);
                continue;
            }

            try
            {
                filesystem.File.Copy(source, target);
            }
            catch (IOException ex)
            {
                console.MarkupLine($"[red]Error copying file:[/] {source}");
                console.MarkupLine($"[red]{ex.Message}[/]");
                ActionCausesExitException.ExitNow();
            }
        }
    }

    public static void RecursivelyDeleteDirectory(this IFileSystem filesystem, string directoryPath)
    {
        if (!filesystem.Directory.Exists(directoryPath))
        {
            return;
        }

        var directoryInfo = filesystem.DirectoryInfo.New(directoryPath);
        directoryInfo.Delete(true);
    }
}
